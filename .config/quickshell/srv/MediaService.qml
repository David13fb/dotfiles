// MediaService.qml
pragma Singleton

import QtQuick
import Quickshell
import Quickshell.Io
import Quickshell.Services.Mpris

Singleton {
    id: mediaService

    // 1. PROPIEDADES ACCESIBLES POR TU CENTRO DE CONTROL
    property string title: "No hay reproducción"
    property string artist: "Desconocido"
    property string playbackStatus: "Stopped" // Playing, Paused, Stopped

    // Mapeo interno del reproductor actualmente seleccionado como prioritario
    property var activePlayer: null
    
    // Obtenemos de forma nativa la lista de reproductores D-Bus en Linux
    readonly property var playerList: Mpris.players.values

    // 2. DETECTOR DE EVENTOS NATIVO (Reacciona instantáneamente sin bucles de consola)
    Connections {
        target: Mpris.players
        
        function onValuesChanged() {
            mediaService.updateActivePlayer()
        }
    }

    // Evalúa cuál de todos los reproductores (Zen, TIDAL, etc.) está sonando
    function updateActivePlayer() {
        var newActive = null
        
        // Buscamos el primer reproductor que tenga música en reproducción
        for (var i = 0; i < playerList.length; i++) {
            if (playerList[i]?.isPlaying) {
                newActive = playerList[i]
                break
            }
        }
        
        // Si no hay ninguno sonando, tomamos el primero de la lista como fallback pasivo
        if (!newActive && playerList.length > 0) {
            newActive = playerList[0]
        }

        mediaService.activePlayer = newActive
        mediaService.refreshMetadata()
    }

     // Transforma los metadatos estructurados de MPRIS a tu UI
    function refreshMetadata() {
        if (mediaService.activePlayer) {
            // En Quickshell, los metadatos se guardan en el objeto 'metadata'
            // usando las claves estándar del protocolo xesam
            var mData = mediaService.activePlayer.metadata;
            
            var rawTitle = "";
            var rawArtist = "";

            if (mData) {
                rawTitle = mData["xesam:title"] ?? mData["title"] ?? "";
                
                // Los artistas suelen venir dentro de una lista/array, extraemos el primero
                var artistVal = mData["xesam:artist"] ?? mData["artist"] ?? "";
                if (Array.isArray(artistVal) && artistVal.length > 0) {
                    rawArtist = artistVal[0];
                } else {
                    rawArtist = String(artistVal);
                }
            }

            // Si el reproductor no da título o está vacío, intentamos usar el fallback limpio
            if (!rawTitle || rawTitle.trim() === "") {
                rawTitle = "Pista de Audio / Vídeo";
            }
            
            if (!rawArtist || rawArtist.trim() === "" || rawArtist === "Unknown" || rawArtist === "undefined") {
                // Obtenemos el nombre de la app (ej. "Zen Browser")
                rawArtist = mediaService.activePlayer.identity ?? "Zen Browser";
            }

            // Inyectamos los datos reales a las variables de tu Centro de Control
            mediaService.title = rawTitle;
            mediaService.artist = rawArtist;
            mediaService.playbackStatus = mediaService.activePlayer.isPlaying ? "Playing" : "Paused";
        } else {
            mediaService.title = "No hay reproducción";
            mediaService.artist = "Desconocido";
            mediaService.playbackStatus = "Stopped";
        }
    }

    // Monitorizamos reactivamente el objeto de metadatos entero
    Connections {
        target: mediaService.activePlayer
        ignoreUnknownSignals: true
        
        // Cuando cambia cualquier metadato (título, carátula, etc.), refrescamos
        function onMetadataChanged() { mediaService.refreshMetadata() }
        function onIsPlayingChanged() { mediaService.refreshMetadata() }
        function onPlaybackStatusChanged() { mediaService.refreshMetadata() }
    }

    Component.onCompleted: updateActivePlayer()

    // 3. MÉTODOS DE CONTROL COMPATIBLES BASADOS EN TU SCRIPT
    function playPause() {
        if (mediaService.activePlayer && mediaService.activePlayer.busName) {
            // Le pasamos el busName exacto de D-Bus (ej: org.mpris.MediaPlayer2.firefox.instance_1_42)
            ctrlProc.command = ["playerctl", "--player", mediaService.activePlayer.busName, "play-pause"];
            ctrlProc.running = true;
        } else {
            ctrlProc.command = ["playerctl", "play-pause"];
            ctrlProc.running = true;
        }
    }

    function next() {
        if (mediaService.activePlayer && mediaService.activePlayer.busName) {
            ctrlProc.command = ["playerctl", "--player", mediaService.activePlayer.busName, "next"];
            ctrlProc.running = true;
        } else {
            ctrlProc.command = ["playerctl", "next"];
            ctrlProc.running = true;
        }
    }

    function previous() {
        if (mediaService.activePlayer && mediaService.activePlayer.busName) {
            ctrlProc.command = ["playerctl", "--player", mediaService.activePlayer.busName, "previous"];
            ctrlProc.running = true;
        } else {
            ctrlProc.command = ["playerctl", "previous"];
            ctrlProc.running = true;
        }
    }

    function getArtUrl() {
        if (activePlayer && activePlayer.trackArtUrl) {
            return activePlayer.trackArtUrl;
        }
        // Retorna un fallback local si no hay miniatura o reproductor activo
        return "rc/default_cover.png"; 
    }

    Process {
        id: ctrlProc
    }
}
