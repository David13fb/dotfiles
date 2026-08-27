import QtQuick
import Quickshell
import Quickshell.Io

Rectangle {
  id: batteryContainer

  // Detectamos si la batería existe en el sistema operativo
  property bool hasBattery: capacityFile.exists

  // Si no hay batería, colapsamos el tamaño a 0 para que no ocupe espacio
  width: hasBattery ? (batteryLayout.implicitWidth + 24) : 0
  height: hasBattery ? 30 : 0
  visible: hasBattery // Oculta el componente visualmente

  radius: 20
  color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.surface_variant !== "transparent")
          ? Colors.md3.surface_variant
          : "#45475a"

  property int currentPercentage: 0
  property string currentStatus: "Discharging"

  // Lector nativo del archivo de capacidad del Kernel (BAT1)
  FileView {
    id: capacityFile
    path: "/sys/class/power_supply/BAT1/capacity"

    onDataChanged: {
      let content = capacityFile.text().trim();
      if (content) {
        batteryContainer.currentPercentage = parseInt(content);
      }
    }
  }

  // Lector nativo del archivo de estado de carga (BAT1)
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

  // Temporizador: Solo se activa e inicia si detecta una batería
  Timer {
    id: sysfsPoller
    interval: 50000 
    running: batteryContainer.hasBattery
    repeat: true
    triggeredOnStart: true
    onTriggered: {
      capacityFile.reload();
      statusFile.reload();
    }
  }

  Component.onCompleted: {
    // Solo intentamos recargar si el archivo realmente existe
    if (batteryContainer.hasBattery) {
      capacityFile.reload();
      statusFile.reload();
    }
  }

  Row {
    id: batteryLayout
    anchors.centerIn: parent
    spacing: 6
    // Solo renderiza los elementos internos si hay batería
    visible: batteryContainer.hasBattery

    // ICONO DINÁMICO
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

    // TEXTO DEL PORCENTAJE
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
