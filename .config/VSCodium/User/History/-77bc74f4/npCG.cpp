
#include <iostream>
#include <fstream>
#include <climits>
#include <algorithm>
#include <vector>
using namespace std;

/*@ <answer>


 @ </answer> */


// ================================================================
// Escribe el c�digo completo de tu soluci�n aqu� debajo (despu�s de la marca)
//@ <answer>

vector<int> minimos(vector<int> const& D, int C) {
    int n = D.size();
    vector<int> act(C+1, INT_MAX-1);
    act[0] = 0;
    for (int i = 1; i <= n; ++i) {
        for (int j = D[i - 1]; j <= C; ++j) {
            act[j] = min(act[j], act[j - D[i - 1]] + 1);
        }
    }
    vector<int> sol;
    if (act[C] != INT_MAX) {
        int i = n, j = C;
        while (j > 0) { 
            if (i == 0) { 
                j = 0;
                sol.clear();
            }
            else if (D[i - 1] <= j && act[j] == act[j - D[i - 1]] + 1) {
                sol.push_back(D[i - 1]);
                j = j - D[i - 1];
            }
            else 
                --i;
        }
    }
    return sol;
}
bool resuelveCaso() {
  
  // leer algo para saber si hay caso o no
 
    int v;
    int s;
    cin >> v;
    if (!cin)
    return false;
    cin >> s;
    vector<int> dianas = vector<int>();
    for (int i = 0; i < s;i++) {
        int aux;
        cin >> aux;
        dianas.push_back(aux);
    }
    std::sort(dianas.begin(), dianas.end());
   
    vector<int> aux = minimos(dianas, v);
    if (aux.size() == 0) {
        cout << "Imposible" << "\n";
        return true;
    }
    cout << aux.size() << ": ";
    for (int i = 0; i < aux.size();i++) {
        cout << aux[i] << " ";
    }
    cout << "\n";
  return true;
}

//@ </answer>
//  Lo que se escriba debajo de esta l�nea ya no forma parte de la soluci�n.

int main() {
  // ajustes para que cin extraiga directamente de un fichero
#ifndef DOMJUDGE
  ifstream in("casos.txt");
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
