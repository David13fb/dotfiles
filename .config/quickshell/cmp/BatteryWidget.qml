import QtQuick
import Quickshell
import Quickshell.Io
import QtQuick.Layouts
import "../srv"

Rectangle {
  id: batteryContainer

  // Inicializamos asumiendo falso hasta comprobar el archivo real
  property bool hasBattery: false
Layout.preferredWidth: hasBattery ? (batteryLayout.implicitWidth + 16) : 0 
  Layout.preferredHeight: 30
  Layout.fillWidth: false
  visible: hasBattery 

  // Quitamos los anclajes antiguos de width y height fijos que rompen el RowLayout
  radius: 20
  color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent")
          ? Colors.md3.surface_variant
          : "#45475a"

  property int currentPercentage: 0
  property string currentStatus: "Discharging"

  FileView {
    id: capacityFile
    path: "/sys/class/power_supply/BAT1/capacity"

    onDataChanged: {
      let content = capacityFile.text().trim();
      if (content) {
        batteryContainer.currentPercentage = parseInt(content);
        // Si el archivo responde con datos correctos, confirmamos que la batería existe
        if (!batteryContainer.hasBattery) {
          batteryContainer.hasBattery = true;
          sysfsPoller.start();
        }
      }
    }
  }

  FileView {
    id: statusFile
    path: "/sys/class/power_supply/BAT1/status"

    onDataChanged: {
      let content = statusFile.text().trim();
      if (content) {
        batteryContainer.currentStatus = content;
      }
    }
  }

  Timer {
    id: sysfsPoller
    interval: 50000
    repeat: true
    triggeredOnStart: false // Lo controlamos de manera manual tras la primera lectura exitosa
    onTriggered: {
      capacityFile.reload();
      statusFile.reload();
    }
  }

  Component.onCompleted: {
    // Forzamos la lectura inicial. Si el archivo existe en el sistema, disparará 'onDataChanged'
    capacityFile.reload();
    statusFile.reload();
  }

  Row {
    id: batteryLayout
    anchors.centerIn: parent
    spacing: 6
    visible: batteryContainer.hasBattery

    Text {
      id: batteryIcon
      font.family: "JetBrainsMono Nerd Font"
      font.pixelSize: 14
      font.bold: true

      color: batteryContainer.currentStatus === "Charging"
             ? "#a6e3a1"
             : (batteryContainer.currentPercentage < 20 ? "#f38ba8" : fallbackColor.color)

      text: {
        if (batteryContainer.currentPercentage === 0) return "\uf244! "
        if (batteryContainer.currentStatus === "Charging") return "\uf240 \udb85\udc0b"
                 
        let pct = batteryContainer.currentPercentage
        if (pct >= 90) return "\uf240"
        if (pct >= 70) return "\uf241"
        if (pct >= 50) return "\uf242"
        if (pct >= 30) return "\uf243"
        if (pct >= 15) return "\uf244"
        return "  "
      }
    }

    Text {
      id: batteryText
      font.family: "JetBrainsMono Nerd Font"
      font.pixelSize: 14
      font.bold: true
      color: fallbackColor.color

      text: batteryContainer.currentPercentage > 0 ? batteryContainer.currentPercentage + "%" : "..."

      Behavior on color {
        ColorAnimation { duration: 150 }
      }
    }
  }

  QtObject {
    id: fallbackColor
    property color color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.on_surface_variant !== "transparent")
                          ? Colors.md3.on_surface_variant
                          : "#cdd6f4"
  }
}
