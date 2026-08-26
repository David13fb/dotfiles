// ClockWidget.qml
import QtQuick

Rectangle {
  id: clockContainer

  // Dimensiones dinámicas basadas en el tamaño del texto más un margen interno (padding)
  width: clockText.implicitWidth + 24
  height: 26
  radius: 12 // Bordes redondeados estilo píldora

  
  color : "transparent"

  Text {
    id: clockText
    anchors.centerIn: parent

    // Acceso directo a la propiedad de tiempo del Singleton Time
    text: Time.time

    // TEXTO: Usamos on_surface_variant (o on_background) para mantener un buen contraste sobre el fondo
    color: (typeof Colors !== "undefined" && Colors.md3 && Colors.md3.on_surface_variant !== "transparent")
           ? Colors.md3.on_surface_variant
           : "#cdd6f4"

    font.pixelSize: 14
    font.bold: true

    Behavior on color {
      ColorAnimation { duration: 150 }
    }
  }
}
