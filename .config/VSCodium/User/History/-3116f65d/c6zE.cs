using TMPro;
using UCM.IAV.Movimiento;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Metricas : MonoBehaviour
{

    [SerializeField] Text _time; //
    [SerializeField] Text _visit; //
    [SerializeField] Text _long; //
    [SerializeField] Text _map; //
    [SerializeField] Text _it; //
    [SerializeField] Text _cost; 
    
    private void Awake(){
        _map.text = "Mapa: "+ GameManager.instance.getSize()+"x"+GameManager.instance.getSize();
    }
    public void setTime(long aux){
        _time.text = "Tiempo: "+ aux;
    }
    public void setVisit(string aux){
        _visit.text = "Visitadas: " + aux;
    }
    public void setlong(string aux){
        _long.text = "Longitud: " + aux;
    }
    public void setIt(string aux){
        _it.text = "Iteradas" + aux; 
    }
    public void setCost(string aux){
        _cost.text = "Coste: " + aux;
    }
}
