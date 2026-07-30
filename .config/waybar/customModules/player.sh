#!/usr/bin/env bash

# Configuración del ancho del texto en la barra
ANCHO=25

# Marcos de la animación del ecualizador
animation_frames=("▂▄▆" "▄▂▆" "▄▆▂" "▆▄▂" "▆▂▄")
num_frames=${#animation_frames[@]}
frame_index=0

while true; do
    # Comprobamos el estado actual del reproductor
    STATUS=$(playerctl status 2>/dev/null)

    # Si no está sonando nada (Paused, Stopped o vacío), vaciamos la barra o mostramos pausa
    if [ "$STATUS" == "Paused" ]; then
        echo ""
        sleep 1
        continue
    elif [ "$STATUS" != "Playing" ]; then
        echo ""
        sleep 1
        continue
    fi

    # Si está sonando, obtenemos el texto de la canción
    TEXTO=$(playerctl metadata --format '{{title}} - {{artist}}' 2>/dev/null)
    TEXTO="$TEXTO       "
    LONGITUD=${#TEXTO}

    # Bucle para realizar el efecto de scroll combinado con la animación del icono
    for ((i=0; i<LONGITUD; i++)); do
        # Verificación instantánea en cada letra por si se pausa o detiene el audio
        CURRENT_STATUS=$(playerctl status 2>/dev/null)
        if [ "$CURRENT_STATUS" != "Playing" ]; then
            if [ "$CURRENT_STATUS" == "Paused" ]; then
                echo ""
            else
                echo ""
            fi
            break
        fi

        # Seleccionamos el frame actual de la animación
        FRAME="${animation_frames[$frame_index]}"
        
        # Avanzamos al siguiente frame para la próxima letra
        frame_index=$(( (frame_index + 1) % num_frames ))

        # Cortamos el texto según la posición actual del scroll
        CORTADO="${TEXTO:$i:$ANCHO}"
        
        # Rellena el espacio si llega al final del texto
        if [ ${#CORTADO} -lt $ANCHO ]; then
            FALTA=$((ANCHO - ${#CORTADO}))
            CORTADO="$CORTADO${TEXTO:0:$FALTA}"
        fi
        
        # Imprimimos el icono animado seguido del texto en scroll
        echo "$FRAME $CORTADO"
        
        # El sleep de 0.25 funciona perfecto tanto para la velocidad de lectura como para la animación
        sleep 0.25
    done
done
