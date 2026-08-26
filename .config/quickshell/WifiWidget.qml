// WifiWidget.qml
import QtQuick
import QtQuick.Layouts
import Quickshell
import Quickshell.Io

Rectangle {
    id: wifiRoot

    implicitWidth: 40 // Le damos un poco más de aire para que los iconos Nerd Fonts entren bien
    height: 30
    radius: 20

    // Usamos el color de superficie variante de tu Matugen
    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent")
           ? Colors.md3.surface_variant
           : "#42474e"

    property string netStatus: "desconectado"
    property string activeSsid: ""

    // 1. Proceso para comprobar el estado de la red
    Process {
        id: statusChecker
        command: ["nmcli", "-t", "-f", "TYPE,STATE,CONNECTION", "device"]
        running: false

        stdout: StdioCollector {
            id: statusCollector

            onTextChanged: {
                var txt = statusCollector.text.trim();
                if (!txt) return;

                var lines = txt.split("\n");
                var isWifiConnected = false;
                var isEthernetConnected = false;
                var foundSsid = "";

                for (var i = 0; i < lines.length; i++) {
                    var parts = lines[i].split(":");
                    if (parts.length >= 2) {
                        var type = parts[0].trim();
                        var state = parts[1].trim();
                        var connection = parts.length > 2 ? parts[2].trim() : "";

                        // CORRECCIÓN: Comprobamos los índices del array de nmcli correctamente
                        if (state === "connected") {
                            if (type === "ethernet") {
                                isEthernetConnected = true;
                            }
                            if (type === "wifi") {
                                isWifiConnected = true;
                                foundSsid = connection;
                            }
                        }
                    }
                }

                wifiRoot.activeSsid = foundSsid;
                if (isEthernetConnected) {
                    wifiRoot.netStatus = "cable";
                } else if (isWifiConnected) {
                    wifiRoot.netStatus = "conectado";
                } else {
                    wifiRoot.netStatus = "desconectado";
                }
            }
        }
    }

    Timer {
        interval: 3000 // Bajamos a 3 segundos para que detecte el cable más rápido al arrancar
        running: true
        repeat: true
        onTriggered: {
            statusChecker.running = false;
            statusChecker.running = true;
        }
        Component.onCompleted: {
            statusChecker.running = true;
        }
    }

    // 2. Icono e interfaz visual del Widget
    Text {
        anchors.centerIn: parent
        font.pixelSize: 14
        font.family: "JetBrainsMono Nerd Font"

        // Color dinámico según tu paleta de Matugen
        color: {
            if (wifiRoot.netStatus === "desconectado") {
                return (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.error !== "transparent") ? Colors.md3.error : "#ffb4ab"
            }
            return (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.primary !== "transparent") ? Colors.md3.primary : "#9fcafc"
        }

        text: {
            if (wifiRoot.netStatus === "cable") return ""; // Icono de Cable / Ethernet
            if (wifiRoot.netStatus === "conectado") return ""; // Icono Wi-Fi Conectado
            return "d"; // Icono Desconectado
        }
    }

}


