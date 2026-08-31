// cmp/WallpaperWidget.qml
import QtQuick
import QtQuick.Layouts
import QtQuick.Controls
import Quickshell
import Quickshell.Io
import Quickshell.Widgets
import "../srv"

PopupWindow {
    id: wallpaperPopup

    visible: typeof topBar !== "undefined" && topBar.wallpaperMode

    anchor.window: topBar
    anchor.rect.x: (topBar.width / 2) - (implicitWidth / 2)
    anchor.rect.y: topBar.height + 8

    implicitWidth: 700
    implicitHeight: 150
    color: "transparent"

    onVisibleChanged: {
        if (visible) {
            horizontalList.forceActiveFocus();
        }
    }

    Rectangle {
        id: menuContent
        anchors.fill: parent
        radius: 16
        color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface !== "transparent") ? Colors.md3.surface : "#1e1e2e"

        transformOrigin: Item.Top
        scale: (typeof topBar !== "undefined" && topBar.wallpaperMode) ? 1.0 : 0.0
        opacity: (typeof topBar !== "undefined" && topBar.wallpaperMode) ? 1.0 : 0.0

        Behavior on scale {
            NumberAnimation {
                duration: 220
                easing.type: Easing.OutCubic
            }
        }
        Behavior on opacity {
            NumberAnimation {
                duration: 180
            }
        }

        property var wpModel: ListModel {
            id: wpModelInstance
        }

        Process {
            id: imageReader
            command: ["bash", "-c", "find ~/dotfiles/wallpapers -type f \\( -name '*.jpg' -o -name '*.png' -o -name '*.jpeg' -o -name '*.webp' \\)"]
            running: true
            stdout: SplitParser {
                onRead: data => {
                    if (data.trim() !== "") {
                        let parts = data.split('/');
                        let fileName = parts[parts.length - 1];
                        let displayName = fileName.substring(0, fileName.lastIndexOf('.')) || fileName;
                        wpModelInstance.append({
                            name: displayName,
                            file: data.trim()
                        });
                    }
                }
            }
        }

        Process {
            id: swwwRunner
        }

        function applyWallpaper(filePath) {
            swwwRunner.command = ["bash", "-c", "swww img '" + filePath + "' --transition-type any --transition-step 63 --transition-angle 0 --transition-duration 2 --transition-fps 60 ; matugen image '" + filePath + "' --prefer saturation"];
            swwwRunner.startDetached();

            if (typeof topBar !== "undefined") {
                topBar.wallpaperMode = false;
            }
        }

        // LE AÑADIMOS EL FOCO INTERNO AL LISTVIEW
        ListView {
            id: horizontalList
            anchors.fill: parent
            anchors.margins: 10
            model: wpModelInstance
            orientation: ListView.Horizontal
            spacing: 20
            clip: true

            // Propiedades esenciales de navegación activa
            focus: true
            activeFocusOnTab: true

            snapMode: ListView.SnapToItem
            highlightRangeMode: ListView.ApplyRange
            preferredHighlightBegin: (horizontalList.width / 2) - 75
            preferredHighlightEnd: (horizontalList.width / 2) + 75
            keyNavigationEnabled: true

            Keys.onReturnPressed: {
                if (horizontalList.currentItem) {
                    menuContent.applyWallpaper(horizontalList.currentItem.myFilePath);
                }
            }

            delegate: Item {
                id: wrapperItem
                property string myFilePath: model.file
                readonly property bool isCentered: ListView.isCurrentItem

                width: isCentered ? 150 : 125
                height: 125 // Fijamos la altura máxima aquí para evitar saltos verticales bruscos

                // Alineación vertical perfecta respecto al centro de la lista
                anchors.verticalCenter: parent.verticalCenter
                ClippingRectangle {
                    anchors.centerIn: parent
                    width: wrapperItem.isCentered ? 150 : 125
                    height: wrapperItem.isCentered ? 125 : 100

                    color: "#313244"
                    radius: 12
                    clip: true

                    border.color: wrapperItem.isCentered ? ((typeof Colors !== "undefined" && Colors.md3.primary) ? Colors.md3.primary : "#f5c2e7") : "transparent"
                    border.width: wrapperItem.isCentered ? 3 : 0

                    Behavior on width {
                        NumberAnimation {
                            duration: 150
                            easing.type: Easing.OutCubic
                        }
                    }
                    Behavior on height {
                        NumberAnimation {
                            duration: 150
                            easing.type: Easing.OutCubic
                        }
                    }

                    Image {
                        anchors.fill: parent
                        source: "file://" + model.file
                        fillMode: Image.PreserveAspectCrop
                        asynchronous: true
                    }

                    MouseArea {
                        anchors.fill: parent
                        cursorShape: Qt.PointingHandCursor
                        onClicked: {
                            horizontalList.currentIndex = index;
                            menuContent.applyWallpaper(model.file);
                        }
                    }
                }
            }
        }
    }
}
