cd /run;
ls -al | tail -n +2 | cut -c1 | sort | uniq

# Con cd nos dirigimos al directorio que nos piden ls -al nos da el listado de los archivos,
# tail nos elimina el total x al inicio de la salida del ls, cut -c1 deja solamente el primer caracter
# de la cada linea, sort nos los ordena dejando asi todas las lineas iguales juntas y por ultimo uniq
# elimina las lineas adyacentes duplicadas