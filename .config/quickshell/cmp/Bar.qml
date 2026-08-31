// Bar.qml
import QtQuick
import QtQuick.Layouts
import Quickshell
import Quickshell.Hyprland
import "../srv"

Scope {
    Variants {
        model: Quickshell.screens

        PanelWindow {
            id: topBar
            required property var modelData
            screen: modelData

            // 1. Alineación básica
            anchors {
                top: true
                left: true
                right: true
            }

            // 2. MÁRGENES ESTILO CAELESTIA: Separación de los bordes físico de la pantalla
            margins {
                top: 10     // Separación del techo de la pantalla
                left: 12    // Separación izquierda
                right: 12   // Separación derecha
            }

            implicitHeight: 30

            GlobalShortcut {
                name: "toggle_control_center" // El nombre identificador para Hyprland

                onPressed: {
                    controlCenterMenu.isOpen = !controlCenterMenu.isOpen;
                }
            }
            property bool wallpaperMode: false

            GlobalShortcut {
                name: "toggle_wallpapers"
                onPressed: wallpaperMode = !wallpaperMode
            }

            // 3. FONDO FLOTANTE: Contenedor con esquinas redondeadas y color de Matugen
            color: "transparent" // Hacemos la ventana transparente para que el Rectangle defina la forma

            Rectangle {
                anchors.fill: parent
                radius: 12 // Bordes muy redondeados estilo píldora/Caelestia

                // Color de fondo usando el contenedor de superficie de Matugen
                //color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface !== "transparent") ? Colors.md3.surface : "#1e1e2e" // Fallback oscuro (ej: Catppuccin Mocha)
                color: "transparent"
                // Contenedor interno de los elementos
                Item {
                    anchors.fill: parent
                    anchors.leftMargin: 0
                    anchors.rightMargin: 0

                    ///########
                    ///left modules
                    ///########
                    // Workspaces alineados a la izquierda

                    Workspaces {
                        anchors.verticalCenter: parent.verticalCenter
                        anchors.left: parent.left
                    }

                    ///########
                    ///Center modules
                    ///########
                    Rectangle {
                        anchors.centerIn: parent
                        width: centerLayout.implicitWidth
                        height: 30
                        radius: 12

                        // Usamos tu formato original con "Colors" directo
                        color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent") ? Colors.md3.surface_variant : "#45475a"

                        Behavior on width {
                            NumberAnimation {
                                duration: 250
                                easing.type: Easing.OutCubic
                            }
                        }

                        RowLayout {
                            id: centerLayout
                            anchors.centerIn: parent
                            spacing: 0

                            ClockWidget {
                                Layout.alignment: Qt.AlignVCenter
                            }
                            MediaWidget {
                                Layout.alignment: Qt.AlignVCenter
                            }
                        }
                    }

                    ///########
                    ///Right modules
                    ///########
                    // Dentro del layout derecho de tu Bar.qml
                    RowLayout {
                        anchors.verticalCenter: parent.verticalCenter
                        anchors.right: parent.right
                        spacing: 8

                        // monitor de audio
                        AudioWidget {}

                        // Monitor de Wi-Fi
                        WifiWidget {}

                        BatteryWidget {}
                        Rectangle {
                            id: controlCenterButton
                            implicitWidth: 32
                            height: 30
                            radius: 15

                            // Color dinámico usando el tono de la superficie variante
                            color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent") ? Colors.md3.surface_variant : "#42474e"

                            Text {
                                anchors.centerIn: parent
                                text: "" // Icono de panel de control / cuadrícula de Nerd Fonts
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 14

                                // Color de acento usando el color primario de tu wallpaper
                                color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.primary !== "transparent") ? Colors.md3.primary : "#9fcafc"
                            }

                            MouseArea {
                                anchors.fill: parent
                                cursorShape: Qt.PointingHandCursor

                                // Al hacer clic, cambia la visibilidad del menú
                                onClicked: controlCenterMenu.isOpen = !controlCenterMenu.isOpen
                            }
                        }
                    }
                    ControlCenter {
                        id: controlCenterMenu
                    }
                    WallpaperWidget {
                        id: wallpaperPopup
                    }

                    
                    HyprlandFocusGrab {
                        
                        active: topBar.wallpaperMode
                        windows: [wallpaperPopup]
                    }
                }
            }
        }
    }
}
