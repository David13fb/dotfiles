// Bar.qml
import QtQuick
import QtQuick.Layouts
import Quickshell
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
                    // Reloj alineado al centro exacto
                    ///########
                    ///Center modules
                    ///########

                    Rectangle {
                        anchors.centerIn: parent
                        
                        // Dimensiones automáticas basadas en los widgets de adentro + un margen extra
                        width: centerLayout.implicitWidth + (MediaWidget.visible ? 30 : 0) // Ajuste dinámico de padding
                        height: 30
                        radius: 12

                        color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent") 
                               ? Colors.md3.surface_variant 
                               : "#45475a"

                        // ANIMACIÓN ELASTICA: Hace que el contenedor cambie de tamaño suavemente
                        Behavior on width {
                            NumberAnimation {
                                duration: 250
                                easing.type: Easing.OutCubic
                            }
                        }

                        // El layout ahora va dentro del Rectangle de manera segura
                        RowLayout {
                            id: centerLayout
                            anchors.centerIn: parent
                            spacing: MediaWidget.visible ? 8 : 0 // Spacing dinámico para que no deje un hueco vacío

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

                        BatteryWidget{
                        }
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
                }
            }
        }
    }
}
