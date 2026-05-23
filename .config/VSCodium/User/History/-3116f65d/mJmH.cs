using TMPro;
using UCM.IAV.Movimiento;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Metricas : MonoBehaviour
{

    [SerializeField] Text _time;
    [SerializeField] Text _visit;
    [SerializeField] Text _long;
    [SerializeField] Text _map;
    [SerializeField] Text _it;
    [SerializeField] Text _cost;
    
    private void Start(){
        _map.text = "Mapa: "+ GameManager.instance.getSize()+"x"+GameManager.instance.getSize();
    }
    public void setTime(long aux){
        _time.text = "Tiempo: "+ aux;
    }
    public void setVisit(ref string aux){
        _visit.text = "Visitadas: " + aux;
    }
    public void setlong(ref string aux){
        _long.text = "Longitud: " + aux;
    }
    public void setIt(ref string aux){
        _it.text = "Iteradas" + aux; 
    }
    public void setCost(ref string aux){
        _cost.text = "Coste: " + aux;
    }
}
