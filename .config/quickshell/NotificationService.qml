// NotificationService.qml
pragma Singleton
import QtQuick
import Quickshell
import Quickshell.Io

Scope {
    id: notificationService

    // El modelo dinámico real conectado a tu ControlCenter.qml
    readonly property ListModel notifications: ListModel {}

    Process {
        id: dbusMonitor
        command: ["dbus-monitor", "interface='org.freedesktop.Notifications',member='Notify'"]
        running: true

        stdout: StdioCollector {
            id: monitorCollector

            onTextChanged: {
                var txt = monitorCollector.text;
                if (!txt || txt.trim() === "") return;

                var lines = txt.split("\n");
                var stringsFound = [];

                // Recorremos las líneas acumuladas buscando los textos entre comillas
                for (var i = 0; i < lines.length; i++) {
                    var line = lines[i].trim();
                    
                    if (line.indexOf("string \"") !== -1) {
                        var firstQuote = line.indexOf("\"");
                        var lastQuote = line.lastIndexOf("\"");
                        
                        if (firstQuote !== -1 && lastQuote !== -1 && firstQuote !== lastQuote) {
                            var extractedStr = line.substring(firstQuote + 1, lastQuote);
                            stringsFound.push(extractedStr);
                        }
                    }
                }

                // D-Bus envía: AppName, Summary (Título), Body (Mensaje)
                if (stringsFound.length >= 2) {
                    var appName = stringsFound[0] !== "x" ? stringsFound[0] : "Sistema";
                    var summary = stringsFound[1];
                    
                    var body = stringsFound.length > 2 ? stringsFound[2] : "x";

                    // Filtramos el nombre de la propia interfaz de D-Bus para evitar ruidos
                    if (summary && summary.trim() !== "" && summary !== "org.freedesktop.Notifications") {
                        
                        // Evitamos duplicar exactamente la misma notificación idéntica de golpe
                        if (notifications.count === 0 || notifications.get(0).summary !== summary) {
                            notificationService.notifications.insert(0, {
                                "appName": appName,
                                "summary": summary,
                                "body": body !== "x" ? body : "Notificación sin texto adicional"
                            });
                        }
                        
                        // CORRECCIÓN: Reiniciamos el proceso de forma reactiva para limpiar el búfer nativamente
                        dbusMonitor.running = false;
                        dbusMonitor.running = true;
                    }
                }
            }
        }
    }

    // Funciones de control dinámicas para tu ControlCenter.qml
    function dismiss(index) {
        if (index >= 0 && index < notifications.count) {
            notifications.remove(index);
        }
    }

    function clearAll() {
        notifications.clear();
    }
}
