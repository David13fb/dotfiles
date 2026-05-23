using UnityEngine;
using UnityEngine.UI;

public class Metricas : MonoBehaviour
{

    [SerializeField] Text _time;
    [SerializeField] Text _visit;
    [SerializeField] Text _long;
    [SerializeField] Text _map;
    [SerializeField] Text _it;
    [SerializeField] Text _cost;
    
    public void setTime(ref string aux){
        _time.text = "Tiempo: "+ aux;
    }
    public void setVisit(ref string aux){
        _visit.text = "Visitadas" + aux;
    }
    public void setlong(ref string aux){

    }
    public void setMap(ref string aux){

    }
    public void setIt(ref string aux){

    }
    public void setCost(ref string aux){

    }
}
