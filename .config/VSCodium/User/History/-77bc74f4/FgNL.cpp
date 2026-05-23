
#include <iostream>
#include <fstream>
#include <filesystem>
using namespace std;

/*@ <answer>


 @ </answer> */


// ================================================================
// Escribe el c�digo completo de tu soluci�n aqu� debajo (despu�s de la marca)
//@ <answer>

bool resuelveCaso() {
  
  // leer algo para saber si hay caso o no
 
    int p;
    cin >> p;
    if (p == 0)
    return false;
    



  return true;
}

//@ </answer>
//  Lo que se escriba debajo de esta l�nea ya no forma parte de la soluci�n.

int main() {
  // ajustes para que cin extraiga directamente de un fichero
  std::cout << "Estamos en "<<std::filesystem::current_path();
#ifndef DOMJUDGE
  ifstream in("./../../casos.txt");
  if (!in.is_open())
    cout << "Error: no se ha podido abrir el archivo de entrada." << endl;
  auto cinbuf = cin.rdbuf(in.rdbuf());
#endif
  
  // Resolvemos
  while (resuelveCaso());
  
  // para dejar todo como estaba al principio
#ifndef DOMJUDGE
  cin.rdbuf(cinbuf);
#endif
  return 0;
}
