// ControlCenter.qml
import QtQuick
import QtQuick.Layouts
import QtQuick.Controls
import Quickshell
import Quickshell.Io

PopupWindow {
    id: controlCenter

    // La ventana se mantiene abierta si el botón dice que se abra, O si todavía está corriendo la animación de salida
    visible: isOpen || ccContainer.opacity > 0

    // Propiedad que controlará tu botón desde la barra
    property bool isOpen: false

    property bool isWifiActive: true
    property bool isBluetoothActive: true
    Process {
        id: toggleWifi
        command: ["nmcli", "radio", "wifi", isWifiActive ? "on" : "off"]
    }

    // Usamos rfkill para interactuar de forma nativa con Blueman
    Process {
        id: toggleBluetooth
        command: ["rfkill", isBluetoothActive ? "unblock" : "block", "bluetooth"]
    }

    // Abre la interfaz gráfica de Blueman con clic derecho
    Process {
        id: openBluemanManager
        command: ["blueman-manager"]
    }

    Process {
        id: systemPoweroff
        command: ["wlogout", 
        "-b", "4", 
        "-l", "/home/david/.config/wlogout/layout", 
        "-C", "/home/david/.config/wlogout/style.css"]
    }
    
 Process {
        id: brightnessProc
        command: ["brightnessctl", "set", Math.max(5, Math.round(brightnessSlider.value * 100)) + "%"]
    }


    FileView {
        id: currentBrightnessFile
        path: "/sys/class/backlight/intel_backlight/brightness"
        watchChanges: true

        onDataChanged: {
            let current = parseInt(text().trim());
            let max = parseInt(maxBrightnessFile.text().trim() || "1");
            if (current && max && !brightnessSlider.pressed) {
                brightnessSlider.value = current / max;
            }
        }
    }

    FileView {
        id: maxBrightnessFile
        path: "/sys/class/backlight/intel_backlight/max_brightness"
    }

    Component.onCompleted: {
        currentBrightnessFile.reload();
        maxBrightnessFile.reload();
    }

    anchor.window: topBar
    anchor.rect: {
        var barWidth = topBar.width;
        return Qt.rect(barWidth - 360 - 40, 25, 360, 12);
    }
    anchor.edges: Edges.Bottom

    implicitWidth: 360
    implicitHeight: 360
    color: "transparent"

    Rectangle {
        id: ccContainer
        anchors.fill: parent
        radius: 24
        color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_container !== "transparent") ? Colors.md3.surface_container : "#1d2024"
        border.width: 1
        border.color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.outline_variant !== "transparent") ? Colors.md3.outline_variant : "#42474e"

        transformOrigin: Item.TopRight

        // Vinculamos la animación a la nueva propiedad isOpen
        opacity: controlCenter.isOpen ? 1.0 : 0.0
        scale: controlCenter.isOpen ? 1.0 : 0.85

        Behavior on opacity {
            NumberAnimation {
                duration: 150
                easing.type: Easing.OutQuad
            }
        }

        Behavior on scale {
            NumberAnimation {
                duration: 200
                easing.type: Easing.OutBack
            }
        }

        Flickable {
            anchors.fill: parent
            anchors.margins: 20
            contentHeight: contentLayout.implicitHeight
            clip: true

            ColumnLayout {
                id: contentLayout
                anchors.left: parent.left
                anchors.right: parent.right
                spacing: 16

                // ==========================================
                // HEADER: HORA Y FECHA GRANDE
                // ==========================================
                RowLayout {
                    Layout.fillWidth: true
                    Column {
                        Text {
                            text: (typeof Time !== "undefined") ? Time.time : "12:00"
                            font.pixelSize: 28
                            font.bold: true
                            color: Colors.md3.on_surface
                        }
                        Text {
                            text: "Martes, 9 de Diciembre"
                            font.pixelSize: 12
                            color: Colors.md3.on_surface_variant
                        }
                    }

                    // CORRECCIÓN: Separador nativo compatible en lugar del componente custom
                    Item {
                        Layout.fillWidth: true
                    }

                    // Botón de apagado/ajustes sutil
                    Rectangle {
                        width: 32
                        height: 32
                        radius: 16
                        color: Colors.md3.surface_variant
                        Text {
                            anchors.centerIn: parent
                            text: "\uf011"
                            font.family: "JetBrainsMono Nerd Font"
                            color: Colors.md3.error
                        }
                        MouseArea {
                            anchors.fill: parent
                            cursorShape: Qt.PointingHandCursor
                            onClicked: {
                                // Ejecuta el apagado del sistema en Fedora
                                systemPoweroff.running = true; 
                            }
                        }
                    }
                }

                // ==========================================
                // BOTONES RÁPIDOS (QUICK TOGGLES) - CUADRÍCULA 2x2
                // ==========================================
                GridLayout {
                    columns: 2
                    Layout.fillWidth: true
                    columnSpacing: 10
                    rowSpacing: 10

                    // BOTÓN WI-FI
                    Rectangle {
                        Layout.fillWidth: true
                        height: 60
                        radius: 16
                        // Cambia de color según el estado global
                        color: controlCenter.isWifiActive ? Colors.md3.primary_container : Colors.md3.surface_variant

                        RowLayout {
                            anchors.fill: parent
                            anchors.margins: 12
                            Text {
                                text: controlCenter.isWifiActive ? " \uf1eb" : " \udb81\uddaa" // Glifos Nerd Font reales para WiFi
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 18
                                color: controlCenter.isWifiActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                            }
                            Column {
                                Text {
                                    text: "Wi-Fi"
                                    font.bold: true
                                    font.pixelSize: 12
                                    color: controlCenter.isWifiActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                                }
                                Text {
                                    text: controlCenter.isWifiActive ? "Conectado" : "Desconectado"
                                    font.pixelSize: 10
                                    color: controlCenter.isWifiActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                                }
                            }
                        }

                        MouseArea {
                            anchors.fill: parent
                            cursorShape: Qt.PointingHandCursor
                            onClicked: {
                                controlCenter.isWifiActive = !controlCenter.isWifiActive;
                                toggleWifi.running = true;
                            }
                        }
                    }

                    // BOTÓN BLUETOOTH (Configurado para Blueman)
                    Rectangle {
                        Layout.fillWidth: true
                        height: 60
                        radius: 16
                        // Usa el color del contenedor primario al estar encendido, variante si está apagado
                        color: controlCenter.isBluetoothActive ? Colors.md3.primary_container : Colors.md3.surface_variant

                        RowLayout {
                            anchors.fill: parent
                            anchors.margins: 12
                            Text {
                                text: controlCenter.isBluetoothActive ? "\udb80\udcaf" : "\udb80\udcb2" // Glifos Nerd Font reales para Bluetooth
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 18
                                color: controlCenter.isBluetoothActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                            }
                            Column {
                                Text {
                                    text: "Bluetooth"
                                    font.bold: true
                                    font.pixelSize: 12
                                    color: controlCenter.isBluetoothActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                                }
                                Text {
                                    text: controlCenter.isBluetoothActive ? "Encendido" : "Apagado"
                                    font.pixelSize: 10
                                    color: controlCenter.isBluetoothActive ? Colors.md3.on_primary_container : Colors.md3.on_surface_variant
                                }
                            }
                        }

                        MouseArea {
                            anchors.fill: parent
                            cursorShape: Qt.PointingHandCursor
                            acceptedButtons: Qt.LeftButton | Qt.RightButton
                            onClicked: (mouse) => {
                                if (mouse.button === Qt.LeftButton) {
                                    controlCenter.isBluetoothActive = !controlCenter.isBluetoothActive;
                                    toggleBluetooth.running = true;
                                } else if (mouse.button === Qt.RightButton) {
                                    openBluemanManager.running = true;
                                }
                            }
                        }
                    }
                }

                // ==========================================
                // SLIDERS: VOLUMEN Y BRILLO
                // ==========================================
                // ==========================================
                
                ColumnLayout {
                    Layout.fillWidth: true
                    spacing: 12

                    // CONTROL VOLUMEN
                    RowLayout {
                        spacing: 10
                        Text {
                            text: ""
                            font.family: "JetBrainsMono Nerd Font"
                            font.pixelSize: 16
                            color: Colors.md3.primary
                        }
                        Slider {
                            id: volSlider
                            Layout.fillWidth: true
                            from: 0.0
                            to: 1.0
                            value: AudioService.volumeLevel

                            onMoved: {
                                AudioService.setAbsoluteVolume(value);
                            }

                            // Barra de fondo adaptativa
                            background: Rectangle {
                                x: volSlider.leftPadding
                                y: volSlider.topPadding + volSlider.availableHeight / 2 - height / 2
                                implicitWidth: 200
                                implicitHeight: 6
                                width: volSlider.availableWidth
                                height: implicitHeight
                                radius: 3
                                color: Colors.md3.surface_variant

                                // Progreso relleno activo
                                Rectangle {
                                    width: volSlider.visualPosition * parent.width
                                    height: parent.height
                                    color: Colors.md3.primary
                                    radius: 3
                                }
                            }

                            // Botón / Manejador circular
                            handle: Rectangle {
                                x: volSlider.leftPadding + volSlider.visualPosition * (volSlider.availableWidth - width)
                                y: volSlider.topPadding + volSlider.availableHeight / 2 - height / 2
                                implicitWidth: 16
                                implicitHeight: 16
                                radius: 8
                                color: Colors.md3.primary
                            }
                        }
                    }

                                        // CONTROL BRILLO NATIVO
                    RowLayout {
                        spacing: 10
                        Text {
                            text: " "
                            font.family: "JetBrainsMono Nerd Font"
                            font.pixelSize: 16
                            color: Colors.md3.primary
                        }
                        Slider {
                            id: brightnessSlider
                            Layout.fillWidth: true
                            value: 0.6 // Fallback temporal antes de leer el hardware

                            // Ejecuta el comando en Fedora al arrastrar
                            onMoved: {
                                let pct = Math.round(brightnessSlider.value * 100);
                                brightnessProc.running = true
                            }

                            background: Rectangle {
                                x: brightnessSlider.leftPadding
                                y: brightnessSlider.topPadding + brightnessSlider.availableHeight / 2 - height / 2
                                implicitWidth: 200
                                implicitHeight: 6
                                width: brightnessSlider.availableWidth
                                height: implicitHeight
                                radius: 3
                                color: Colors.md3.surface_variant

                                Rectangle {
                                    width: brightnessSlider.visualPosition * parent.width
                                    height: parent.height
                                    color: Colors.md3.primary
                                    radius: 3
                                }
                            }

                            handle: Rectangle {
                                x: brightnessSlider.leftPadding + brightnessSlider.visualPosition * (brightnessSlider.availableWidth - width)
                                y: brightnessSlider.topPadding + brightnessSlider.availableHeight / 2 - height / 2
                                implicitWidth: 16
                                implicitHeight: 16
                                radius: 8
                                color: Colors.md3.primary
                            }
                        }
                    }

                }


                // ==========================================
                // REPRODUCTOR MULTIMEDIA (MEDIA PLAYER)
                // ==========================================

                // Reemplaza el bloque del REPRODUCTOR MULTIMEDIA en tu ControlCenter.qml por este:
                Rectangle {
                    Layout.fillWidth: true
                    height: 80
                    radius: 16
                    color: Colors.md3.surface_container_high
                    border.width: 1
                    border.color: Colors.md3.outline_variant

                    RowLayout {
                        anchors.fill: parent
                        anchors.margins: 12
                        spacing: 12

                        // Miniatura / Caja de Portada dinámica
                        Rectangle {
                            width: 56
                            height: 56
                            radius: 8
                            // Cambia sutilmente de color si está reproduciendo música o no
                            color: MediaService.playbackStatus === "Playing" ? Colors.md3.primary : Colors.md3.surface_variant

                            Text {
                                anchors.centerIn: parent
                                text: " " // Icono de nota musical de Nerd Fonts
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 20
                                color: MediaService.playbackStatus === "Playing" ? Colors.md3.on_primary : Colors.md3.on_surface_variant
                            }
                        }

                        // Título y Artista en tiempo real extraídos de playerctl con Carrusel
                        ColumnLayout {
                            Layout.fillWidth: true
                            spacing: 2

                            // Contenedor del Carrusel (Marquee) con geometría corregida para Layouts
                            Item {
                                id: titleContainer
                                Layout.fillWidth: true
                                implicitHeight: 18
                                clip: true // Recorta el texto excedente estilo ventana invisible

                                Text {
                                    id: titleText
                                    text: MediaService.title
                                    font.bold: true
                                    font.pixelSize: 13
                                    color: Colors.md3.on_surface
                                    anchors.verticalCenter: parent.verticalCenter

                                    // Compara de forma matemática el largo del string contra el ancho real de la tarjeta
                                    readonly property bool isTooLong: implicitWidth > titleContainer.width

                                    // Animación horizontal fluida y automatizada
                                    NumberAnimation on x {
                                        id: marqueeAnimation
                                        from: 0
                                        to: -(titleText.implicitWidth - titleContainer.width + 20)
                                        duration: Math.max(3000, titleText.implicitWidth * 30) // Velocidad proporcional
                                        loops: Animation.Infinite
                                        running: titleText.isTooLong && MediaService.playbackStatus === "Playing"
                                        easing.type: Easing.Linear
                                    }

                                    // Reset de posición inmediato al saltar a un nuevo video/canción en Zen Browser o TIDAL
                                    onTextChanged: {
                                        marqueeAnimation.stop();
                                        x = 0;
                                        if (isTooLong && MediaService.playbackStatus === "Playing") {
                                            marqueeAnimation.running == true;
                                        }
                                    }
                                }
                            }

                            // Subtítulo del Artista
                            Text {
                                Layout.fillWidth: true
                                text: MediaService.artist
                                font.pixelSize: 11
                                color: Colors.md3.on_surface_variant
                                elide: Text.ElideRight
                            }
                        }

                        // BOTONES DE CONTROL INTERACTIVOS (CON ICONOS REALES NERD FONTS)
                        RowLayout {
                            spacing: 14
                            Layout.alignment: Qt.AlignVCenter

                            // Botón Anterior
                            Text {
                                text: "◀" // Icono Back Skip
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 18
                                color: Colors.md3.on_surface
                                MouseArea {
                                    anchors.fill: parent
                                    cursorShape: Qt.PointingHandCursor
                                    onClicked: MediaService.previous()
                                }
                            }

                            // Botón Play / Pausa Dinámico
                            Text {
                                // Alterna de forma reactiva el glifo según el estado del D-Bus de Quickshell
                                text: MediaService.playbackStatus === "Playing" ? "∥" : "▶" // Pausa o Play
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 22
                                color: Colors.md3.primary
                                MouseArea {
                                    anchors.fill: parent
                                    cursorShape: Qt.PointingHandCursor
                                    onClicked: MediaService.playPause()
                                }
                            }

                            // Botón Siguiente
                            Text {
                                text: "▶" // Icono Next Skip
                                font.family: "JetBrainsMono Nerd Font"
                                font.pixelSize: 18
                                color: Colors.md3.on_surface
                                MouseArea {
                                    anchors.fill: parent
                                    cursorShape: Qt.PointingHandCursor
                                    onClicked: MediaService.next()
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
