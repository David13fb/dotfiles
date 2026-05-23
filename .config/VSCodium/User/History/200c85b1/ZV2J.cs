using TMPro;
using UnityEngine;

public class ResUIController : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI deaths;
    [SerializeField]TextMeshProUGUI kills;

    [SerializeField]TextMeshProUGUI points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deaths.text = "DEATHS = " + DataResManager.Instance.numDeaths;
        kills.text = "KILLS = " + DataResManager.Instance.numkills;
    }

}
