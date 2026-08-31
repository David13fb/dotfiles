// AudioService.qml
pragma Singleton
import QtQuick
import Quickshell
import Quickshell.Io

Scope {
    id: audioService

    // Propiedades accesibles desde cualquier componente
    property real volumeLevel: 0.0 // Almacena el volumen de 0.0 a 1.0
    property bool isMuted: false

    // Proceso maestro para cambiar el volumen o mutear
    Process { id: volumeSetter }

    // Proceso cíclico para leer el volumen actual de PipeWire (wpctl)
    Process {
        id: volumeReader
        command: ["wpctl", "get-volume", "@DEFAULT_AUDIO_SINK@"]
        running: false
        
        stdout: StdioCollector {
            id: volumeCollector
            onTextChanged: {
                var txt = volumeCollector.text.trim();
                if (!txt) return;

                audioService.isMuted = txt.includes("[MUTED]"); //
                
                var match = txt.match(/Volume:\s+([0-9.]+)/);
                if (match && match[1]) {
                    audioService.volumeLevel = parseFloat(match[1]); //
                }
            }
        }
    }

    Process {
        id:openVoluneMenu
        command: ["pavucontrol"]
    }

    function openVolumeMixer() {
        openVoluneMenu.running = true // O openVoluneMenu.start()
    }

    // Actualiza el volumen de fondo de forma automatizada cada 2 segundos
    Timer {
        interval: 2000
        running: true
        repeat: true
        onTriggered: {
            volumeReader.running = false; //
            volumeReader.running = true; //
        }
        Component.onCompleted: volumeReader.running = true; //
    }

    // Funciones auxiliares para alterar el volumen cómodamente desde QML
    function stepVolume(up) {
        volumeSetter.command = ["wpctl", "set-volume", "@DEFAULT_AUDIO_SINK@", up ? "5%+" : "5%-"]; //
        volumeSetter.running = true; //
        refresh();
    }

    function setAbsoluteVolume(value) {
        var targetVol = Math.min(1.0, value).toFixed(2); //
        volumeSetter.command = ["wpctl", "set-volume", "@DEFAULT_AUDIO_SINK@", targetVol]; //
        volumeSetter.running = true; //
        refresh();
    }

    function refresh() {
        volumeReader.running = false;
        volumeReader.running = true;
    }
}
