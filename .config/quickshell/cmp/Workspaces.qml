import QtQuick
import QtQuick.Layouts
import Quickshell
import Quickshell.Hyprland
import Quickshell.Widgets
import "../srv"


Rectangle {
    id: backgroundContainer
    
    // Configuración del fondo del contenedor principal
    color: Colors.md3.surface !== "transparent" ? Colors.md3.surface : "#1e1e2e"
    radius: 15
    
    // Ajusta el tamaño dinámicamente según el contenido del RowLayout más el padding
    implicitWidth: workspacesBar.implicitWidth +16
    implicitHeight: workspacesBar.implicitHeight +10

    RowLayout {
        id: workspacesBar
        spacing: 10
        
        // Centramos el RowLayout dentro del fondo contenedor
        anchors.centerIn: parent
    
        Repeater {
            model: Hyprland.workspaces
            
            delegate: Rectangle {
                required property var modelData
                
                readonly property int workspaceId: modelData.id
                readonly property bool isFocused: Hyprland.focusedWorkspace === modelData

                implicitWidth: isFocused ? 36 : 20
                height: 20
                radius: 30

                color: {
                    if (isFocused) {
                        return Colors.md3.primary !== "transparent" ? Colors.md3.primary : "#39baec"
                    } else {
                        return Colors.md3.surface_variant !== "transparent" ? Colors.md3.surface_variant : "#45475a"
                    }
                }

                Behavior on implicitWidth {
                    NumberAnimation { duration: 200; easing.type: Easing.InOutQuad }
                }

                Behavior on color {
                    ColorAnimation { duration: 150 }
                }

                Text {
                    anchors.centerIn: parent
                    text: workspaceId
                    font.pixelSize: 13
                    font.bold: true
                    visible: isFocused
                    color: Colors.md3.on_primary !== "transparent" ? Colors.md3.on_primary : "#11111b"
                }

                MouseArea {
                    anchors.fill: parent
                    cursorShape: Qt.PointingHandCursor
                    onClicked: {
                        Hyprland.dispatch("workspace " + workspaceId);
                    }
                }
            }
        }
    }
}
