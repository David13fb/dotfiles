
if [ -z $1 ];then 
    cd /run;
    ls -al |  cut -c9
    exit 0
else 
    echo "Este programa no necesita argumentos"; 
    exit 1
fi
# Con cd nos dirigimos al directorio que nos piden ls -al nos da el listado de los archivos,
# tail nos elimina el total x al inicio de la salida del ls, cut -c1 deja solamente el primer caracter
# de la cada linea, sort nos los ordena dejando asi todas las lineas iguales juntas y por ultimo uniq
# elimina las lineas adyacentes duplicadas