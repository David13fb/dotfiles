if [ -n $1 ];then echo "Falta argumento \n"
else 
    ps -eo pid,comm | tail -n $1
fi
# ps los lista con el argumento -eo le especifico que solo quiero el pid y el comando
# | envia la salida del ps al tail que utiliza el argumento pasado al archivo para saber
# el número de lineas que debe imprimir en consola
# tail seleciona las linieas empezando por el final del archvo