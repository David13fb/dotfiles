pragma Singleton
import QtQuick
import Quickshell
import Quickshell.Io

Scope {
    id: audioService

    // Propiedades del volumen maestro
    property real volumeLevel: 0.0
    property bool isMuted: false

    // Aquí guardaremos la lista de aplicaciones sonando
    property var appStreams: []

    // PROCESO MAESTRO REPARADO: Ahora se gestiona de forma atómica
    Process {
        id: volumeSetter
    }

    // Proceso cíclico para leer el volumen maestro
    Process {
        id: volumeReader
        command: ["wpctl", "get-volume", "@DEFAULT_AUDIO_SINK@"]
        running: false

        stdout: StdioCollector {
            id: volumeCollector
            onTextChanged: {
                var txt = volumeCollector.text.trim();
                if (!txt)
                    return;

                audioService.isMuted = txt.includes("[MUTED]");

                var match = txt.match(/Volume:\s+([0-9.]+)/);
                if (match && match[1]) {
                    audioService.volumeLevel = parseFloat(match[1]);
                }
            }
        }
    }

    Process {
        id: streamsReader
        command: ["pactl", "-f", "json", "list", "sink-inputs"]
        running: false

        stdout: StdioCollector {
            id: streamsCollector
            waitForEnd: true 

            onStreamFinished: {
                var txt = streamsCollector.text.trim();
                if (!txt || txt === "[]") {
                    audioService.appStreams = [];
                    return;
                }

                try {
                    var data = JSON.parse(txt);
                    var streams = [];
                    var appsArray = Array.isArray(data) ? data : [data];

                    for (var i = 0; i < appsArray.length; i++) {
                        var app = appsArray[i];
                        if (!app || !app.properties) continue;

                        var appName = app.properties["application.name"] || 
                                      app.properties["media.name"] || 
                                      "Aplicación";
                        
                        if (appName.toLowerCase().includes("zen")) appName = "Zen Browser";

                        var rawId = app.index !== undefined ? app.index : app.id;
                        if (rawId === undefined) continue;
                        var cleanId = parseInt(rawId.toString().replace("#", "").trim());

                        var vol = 1.0;
                        if (app.volume) {
                            var channel = app.volume["front-left"] || app.volume["mono"];
                            if (channel) {
                                if (channel.value_percent) {
                                    vol = parseFloat(channel.value_percent.toString().replace("%", "")) / 100.0;
                                } else if (channel.volume) {
                                    vol = parseFloat(channel.volume) / 65536.0;
                                }
                            }
                        }

                        streams.push({
                            "id": cleanId, 
                            "name": appName,
                            "volume": Math.min(1.0, Math.max(0.0, vol))
                        });
                    }
                    audioService.appStreams = streams;
                } catch(e) {
                    console.log("Error parseando audio JSON: " + e);
                }
            }
        }
    }

    // ==========================================
    // FUNCIÓN DE BACKEND REPARADA AL 100%
    // ==========================================
    function setAppVolume(id, value) {
        var safeValue = Math.min(1.0, Math.max(0.0, value));
        var percentVol = Math.round(safeValue * 100) + "%";
        var idString = id.toString().trim();

        // 1. Apagamos el lector de flujos para que el bucle reactivo no congele el valor
        streamsReader.running = false;
        appTimer.running = false;

        // 2. DETECCIÓN DE INSTANCIA ACTIVA: Forzamos la detención manual del volumeSetter anterior
        // Si no se hace esto, QuickShell ignora el cambio de ".command" si se arrastra rápido
        volumeSetter.running = false;

        // 3. Pasamos los argumentos de manera atómica
        volumeSetter.command = ["pactl", "set-sink-input-volume", idString, percentVol];
        
        // 4. Encendemos el proceso
        volumeSetter.running = true;

        // 5. Reiniciamos el recuperador asíncrono para volver a leer el sistema en un instante
        restoreTimer.restart();
    }

    // Temporizador para restaurar las lecturas en segundo plano de forma segura
    Timer {
        id: restoreTimer
        interval: 350
        running: false
        repeat: false
        onTriggered: {
            appTimer.running = true;
            refresh();
        }
    }

    Process {
        id: openVolumeMenu
        command: ["pavucontrol"]
    }

    function openVolumeMixer() {
        openVolumeMenu.running = true;
    }

    // Cronómetro principal de sincronización
    Timer {
        id: appTimer
        interval: 2000
        running: true
        repeat: true
        onTriggered: refresh()
        Component.onCompleted: refresh()
    }

    function stepVolume(up) {
        volumeSetter.running = false;
        volumeSetter.command = ["wpctl", "set-volume", "@DEFAULT_AUDIO_SINK@", up ? "5%+" : "5%-"];
        volumeSetter.running = true;
        refresh();
    }

    function setAbsoluteVolume(value) {
        var targetVol = Math.min(1.0, value).toFixed(2);
        volumeSetter.running = false;
        volumeSetter.command = ["wpctl", "set-volume", "@DEFAULT_AUDIO_SINK@", targetVol];
        volumeSetter.running = true;
        refresh();
    }

    function refresh() {
        volumeReader.running = false;
        volumeReader.running = true;

        streamsReader.running = false;
        streamsReader.running = true;
    }
}
