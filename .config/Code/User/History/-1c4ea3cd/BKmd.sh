#!/bin/bash

echo inodo: ;
ls -i $1;
echo numero del dispositivo;
echo tipo de archivo;
ls --file-type $1;
 echo Último cambio de estado;
 ls --time -m $1;
  echo Último acceso;
 ls --time -a $1;