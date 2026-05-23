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
NOMBRE="";
echo "persona a buscar";
read NOMBRE;
AUX=$(grep NOMBRE contactos.txt| head -n 1);
echo "NOMBRE: $(cut -d ':' -f1 $aux)";
echo "TELEFONO: $(cut -d ':' -f2 $aux)";
echo "Mail: $(cut -d ':' -f3 $aux)";
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

