// AudioWidget.qml
import QtQuick
import QtQuick.Layouts
import QtQuick.Controls
import Quickshell

Rectangle {
    id: audioRoot
    
    implicitWidth: 40  
    height: 30
    radius: 20
    
    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent")
           ? Colors.md3.surface_variant
           : "#42474e"

    // ICONOGRAFÍA DINÁMICA VINCULADA AL SINGLETON
    Text {
        anchors.centerIn: parent
        font.pixelSize: 14
        font.family: "JetBrainsMono Nerd Font"
        
        color: AudioService.isMuted 
               ? ((typeof Colors !== "undefined" && Colors.md3 && Colors.md3.error !== "transparent") ? Colors.md3.error : "#ffb4ab")
               : ((typeof Colors !== "undefined" && Colors.md3 && Colors.md3.primary !== "transparent") ? Colors.md3.primary : "#9fcafc")
        
        text: {
            if (AudioService.isMuted) return ""; //
            if (AudioService.volumeLevel === 0) return " "; //
            if (AudioService.volumeLevel < 0.4) return ""; //
            return ""; //
        }
    }

    MouseArea {
        anchors.fill: parent
        cursorShape: Qt.PointingHandCursor
        onClicked: audioMenu.visible = !audioMenu.visible
        
        // Control rápido mediante rueda del ratón delegando al Singleton
        onWheel: (wheel) => {
            AudioService.stepVolume(wheel.angleDelta.y > 0);
        }
    }

    PopupWindow {
        id: audioMenu
        visible: false
        anchor.window: topBar
        
        anchor.rect: {
            var barWidth = topBar.width;
            var targetX = barWidth - 200 - 24;
            return Qt.rect(targetX, 12, audioRoot.width, audioRoot.height); //
        }
        
        anchor.edges: Edges.Bottom
        implicitWidth: 200
        implicitHeight: 50
        color: "transparent"

        Rectangle {
            anchors.fill: parent
            radius: 8
            color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_container !== "transparent") ? Colors.md3.surface_container : "#1d2024"
            border.width: 1
            border.color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.outline_variant !== "transparent") ? Colors.md3.outline_variant : "#42474e"

            RowLayout {
                anchors.fill: parent
                anchors.margins: 10
                spacing: 8

                Text {
                    text: Math.round(AudioService.volumeLevel * 100) + "%" //
                    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.on_surface !== "transparent") ? Colors.md3.on_surface : "#e1e2e8"
                    font.bold: true
                    font.pixelSize: 11
                    Layout.preferredWidth: 30
                }

                Slider {
                    id: volumeSlider
                    Layout.fillWidth: true
                    from: 0.0
                    to: 1.0
                    value: AudioService.volumeLevel

                    onMoved: {
                        AudioService.setAbsoluteVolume(value);
                    }

                    background: Rectangle {
                        x: volumeSlider.leftPadding
                        y: volumeSlider.topPadding + volumeSlider.availableHeight / 2 - height / 2
                        implicitWidth: 200
                        implicitHeight: 4
                        width: volumeSlider.availableWidth
                        height: implicitHeight
                        radius: 2
                        color: Colors.md3.surface_variant

                        Rectangle {
                            width: volumeSlider.visualPosition * parent.width
                            height: parent.height
                            color: Colors.md3.primary
                            radius: 2
                        }
                    }

                    handle: Rectangle {
                        x: volumeSlider.leftPadding + volumeSlider.visualPosition * (volumeSlider.availableWidth - width)
                        y: volumeSlider.topPadding + volumeSlider.availableHeight / 2 - height / 2
                        implicitWidth: 12
                        implicitHeight: 12
                        radius: 6
                        color: volumeSlider.pressed ? Colors.md3.primary : Colors.md3.on_primary_container
                        border.color: Colors.md3.primary
                        border.width: 1
                    }
                }
            }
        }
    }
}
