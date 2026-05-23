#!/bin/bash

echo inodo: ;
ls -i $1;
echo numero del dispositivo;
echo tipo de archivo;
ls --file-type $1;
 echo Último cambio de estado;
 ls --time mtime $1;
  echo Último acceso;
 ls --time atime $1;