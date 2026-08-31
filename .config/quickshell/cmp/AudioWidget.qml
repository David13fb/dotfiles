// AudioWidget.qml
import QtQuick
import QtQuick.Layouts
import QtQuick.Controls
import Quickshell
import "../srv"

Rectangle {
    id: audioRoot

    implicitWidth: 40
    height: 30
    radius: 20

    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent") ? Colors.md3.surface_variant : "#42474e"

    Text {
        anchors.centerIn: parent
        font.pixelSize: 14
        font.family: "JetBrainsMono Nerd Font"

        color: AudioService.isMuted ? ((typeof Colors !== "undefined" && Colors.md3 && Colors.md3.error !== "transparent") ? Colors.md3.error : "#ffb4ab") : ((typeof Colors !== "undefined" && Colors.md3 && Colors.md3.primary !== "transparent") ? Colors.md3.primary : "#9fcafc")

        text: {
            if (AudioService.isMuted)
                return "";
            if (AudioService.volumeLevel === 0)
                return " ";
            if (AudioService.volumeLevel < 0.4)
                return "";
            return "";
        }
    }

    MouseArea {
        anchors.fill: parent
        cursorShape: Qt.PointingHandCursor
        acceptedButtons: Qt.LeftButton | Qt.RightButton

        onClicked: mouse => {
            if (mouse.button === Qt.LeftButton) {
                audioMenu.visible = !audioMenu.visible;
            } else if (mouse.button === Qt.RightButton) {
                AudioService.openVolumeMixer();
            }
        }

        onWheel: wheel => {
            AudioService.stepVolume(wheel.angleDelta.y > 0);
        }
    }

    PopupWindow {
        id: audioMenu
        visible: false
        anchor.window: topBar

        anchor.rect: {
            var barWidth = topBar.width;
            var targetX = barWidth - 280 - 24;
            return Qt.rect(targetX, 12, audioRoot.width, audioRoot.height);
        }

        anchor.edges: Edges.Bottom

        implicitWidth: 280
        implicitHeight: Math.min(400, 70 + (appsRepeater.count > 0 ? 30 + (appsRepeater.count * 45) : 35))
        color: "transparent"

        Rectangle {
            anchors.fill: parent
            radius: 12
            color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_container !== "transparent") ? Colors.md3.surface_container : "#1d2024"
            border.width: 1
            border.color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.outline_variant !== "transparent") ? Colors.md3.outline_variant : "#42474e"

            ColumnLayout {
                anchors.fill: parent
                anchors.margins: 12
                spacing: 10

                Text {
                    text: "Volumen General"
                    font.pixelSize: 11
                    font.bold: true
                    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.primary !== "transparent") ? Colors.md3.primary : "#9fcafc"
                }

                RowLayout {
                    Layout.fillWidth: true
                    spacing: 8

                    Text {
                        text: Math.round(AudioService.volumeLevel * 100) + "%"
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
                        onMoved: AudioService.setAbsoluteVolume(value)

                        background: Rectangle {
                            x: volumeSlider.leftPadding
                            y: volumeSlider.topPadding + volumeSlider.availableHeight / 2 - height / 2
                            implicitWidth: 200
                            implicitHeight: 4
                            width: volumeSlider.availableWidth
                            height: implicitHeight
                            radius: 2
                            color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.surface_variant : "#45475a"

                            Rectangle {
                                width: volumeSlider.visualPosition * parent.width
                                height: parent.height
                                color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc"
                                radius: 2
                            }
                        }

                        handle: Rectangle {
                            x: volumeSlider.leftPadding + volumeSlider.visualPosition * (volumeSlider.availableWidth - width)
                            y: volumeSlider.topPadding + volumeSlider.availableHeight / 2 - height / 2
                            implicitWidth: 12
                            implicitHeight: 12
                            radius: 6
                            color: volumeSlider.pressed ? ((typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc") : ((typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.on_primary_container : "#00325b")
                            border.color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc"
                            border.width: 1
                        }
                    }
                }

                Rectangle {
                    Layout.fillWidth: true
                    height: 1
                    color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.outline_variant : "#42474e"
                    visible: appsRepeater.count > 0
                }

                Text {
                    text: "Aplicaciones"
                    font.pixelSize: 11
                    font.bold: true
                    color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.on_surface_variant : "#c1c7ce"
                    visible: appsRepeater.count > 0
                }

                ScrollView {
                    id: appsScrollView
                    Layout.fillWidth: true
                    Layout.fillHeight: true
                    clip: true
                    visible: appsRepeater.count > 0
                    contentWidth: appsScrollView.width

                    ColumnLayout {
                        id: appsContainer
                        width: appsScrollView.width
                        spacing: 12

                        Repeater {
                            id: appsRepeater
                            model: AudioService.appStreams

                            delegate: RowLayout {
                                id: appRow
                                Layout.fillWidth: true
                                spacing: 12

                                Text {
                                    text: modelData.name
                                    color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.on_surface : "#e1e2e8"
                                    font.pixelSize: 11
                                    elide: Text.ElideRight
                                    Layout.preferredWidth: 80
                                    Layout.alignment: Qt.AlignVCenter
                                }

                                Slider {
    id: appSlider
    Layout.fillWidth: true
    Layout.alignment: Qt.AlignVCenter
    from: 0.0
    to: 1.0
    
    // Propiedad interna para congelar la ID de la aplicación
    property int appId: 0
    
    Component.onCompleted: {
        appSlider.appId = modelData.id;
        appSlider.value = modelData.volume;
    }

    Connections {
        target: AudioService
        function onAppStreamsChanged() {
            if (!appSlider.pressed) {
                for (var i = 0; i < AudioService.appStreams.length; i++) {
                    if (AudioService.appStreams[i].id === appSlider.appId) {
                        appSlider.value = AudioService.appStreams[i].volume;
                        break;
                    }
                }
            }
        }
    }

    onMoved: {
        AudioService.setAppVolume(appSlider.appId, appSlider.value);
    }

    // Estilo estético unificado con el Volumen General
    background: Rectangle {
        x: appSlider.leftPadding
        y: appSlider.topPadding + appSlider.availableHeight / 2 - height / 2
        implicitWidth: 200
        implicitHeight: 4
        width: appSlider.availableWidth
        height: implicitHeight
        radius: 2
        color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.surface_variant : "#45475a"

        Rectangle {
            width: appSlider.visualPosition * parent.width
            height: parent.height
            color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc"
            radius: 2
        }
    }

    handle: Rectangle {
        x: appSlider.leftPadding + appSlider.visualPosition * (appSlider.availableWidth - width)
        y: appSlider.topPadding + appSlider.availableHeight / 2 - height / 2
        implicitWidth: 12
        implicitHeight: 12
        radius: 6
        color: appSlider.pressed ? ((typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc") : ((typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.on_primary_container : "#00325b")
        border.color: (typeof Colors !== "undefined" && Colors.md3) ? Colors.md3.primary : "#9fcafc"
        border.width: 1
    }
}

                            }
                        }
                    }
                }
            }
        }
    }
}
