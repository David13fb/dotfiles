#!/bin/bash

function insertar(){
  NOMBRE = "";
  TELEFONO = "";
  MAIL = "";
  echo "Nombre"
  read NOMBRE;
  echo "Telefono"
  read TELEFONO
}


select opt in "listar" "buscar" "borrar" "añadir" "Salir"
do
    case $opt in
        "listar")
            ls -l
            ;;
        "buscar")
            date
            ;;
	 "borrar")
	    ;;
    	"añadir")
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

