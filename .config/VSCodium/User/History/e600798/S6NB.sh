if [ -z $1 ];then echo "Falta argumento"; exit 1
else 
    ps -eo pid,comm | tail -n $1
fi

# el condicional nos permite la gestion de errores -z revisa si $1 es vacio 
#(se podria sofisticar para revisar si es un número)
# ps los lista con el argumento -eo le especifico que solo quiero el pid y el comando
# | envia la salida del ps al tail que utiliza el argumento pasado al archivo para saber
# el número de lineas que debe imprimir en consola
# tail seleciona las linieas empezando por el final del archvo