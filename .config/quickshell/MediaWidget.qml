// MediaWidget.qml
import QtQuick
import QtQuick.Layouts
import Quickshell

RowLayout {
    id: integratedMedia
    spacing: 6

    visible: MediaService.playbackStatus !== "Stopped" && MediaService.playbackStatus !== "Paused"

    // Dimensión máxima para que no empuje el resto de elementos de la barra
    Layout.maximumWidth: 180

    // Icono sutil indicador de reproducción
    Text {
        text: MediaService.playbackStatus === "Playing" ? "\uede9" : ""
        font.family: "JetBrainsMono Nerd Font"
        font.pixelSize: 12
        color: Colors.md3.primary
        Layout.alignment: Qt.AlignVCenter
    }

    // Contenedor del carrusel de texto del título del vídeo/canción
    Item {
        id: titleContainer
        implicitWidth: Math.min(titleText.implicitWidth, 150) // Límite del ancho del texto
        implicitHeight: 16
        clip: true 
        Layout.alignment: Qt.AlignVCenter

        Text {
            id: titleText
            text: MediaService.title
            font.pixelSize: 12
            font.bold: true
            color: Colors.md3.on_surface_variant
            anchors.verticalCenter: parent.verticalCenter

            readonly property bool isTooLong: implicitWidth > 150

            // Animación del carrusel continuo
            NumberAnimation on x {
                            id: marqueeAnimation
                            from: 0
                            to: -(titleText.implicitWidth - titleContainer.width + 15)
                            duration: Math.max(3000, titleText.implicitWidth * 30) 
                            loops: Animation.Infinite
                            // 🔥 CORREGIDO: Ahora solo depende de si el texto es largo, ignorando si está pausado
                            running: titleText.isTooLong 
                            easing.type: Easing.Linear
                        }

            onTextChanged: {
                marqueeAnimation.stop();
                x = 0;
                if (isTooLong && MediaService.playbackStatus === "Playing") {
                    marqueeAnimation.start();
                }
            }
        }
    }

}
