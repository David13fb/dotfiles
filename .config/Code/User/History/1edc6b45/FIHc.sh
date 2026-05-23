#!/bin/bash
PATH="";
echo "Ruta del archivo a consultar";
read PATH;
INODO=ls -i PATH;
echo "inodo: " $INODO;