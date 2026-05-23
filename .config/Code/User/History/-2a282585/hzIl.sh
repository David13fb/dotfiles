#!/bin/bash

function insertar(){
  NOMBRE ="";
  TELEFONO ="";
  MAIL ="";
  echo "Nombre"
  read NOMBRE;
  echo "Telefono"
  read TELEFONO;
  echo "Gmail";
  read MAIL;
  echo "$NOMBRE:$TELEFONO:$MAIL" >> contactos.txt;
}
function listar(){
    cat contactos.txt;
}
function buscar(){
N="";
echo "persona a buscar";
read N;
AUX=$(grep $N contactos.txt | head -n 1)
IFS=':' read -r NOMBRE TELEFONO MAIL <<< "$AUX"

echo "NOMBRE: $NOMBRE"
echo "TELEFONO: $TELEFONO"
echo "Mail: $MAIL"
}

function borrar(){
    echo "contacto a borrar";

    read NOMBRE;

    sed -i '/'$NOMBRE'/d' contactos.txt;
}
select opt in "listar" "buscar" "borrar" "añadir" "Salir"
do
    case $opt in
        "listar")
            listar
            ;;
        "buscar")
            buscar
            ;;
	 "borrar")
     borrar
	    ;;
    	"añadir")
        insertar
	    ;;
        "Salir")
            echo "Exiting..."
            break
            ;;
        *)
            echo "Error en opción"
            ;;
    esac
done

