using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PatienceTimer : MonoBehaviour
{

    public Image patienceWheel;

    public RatController Rat;


    private void Start()
    {
        Rat = GetComponentInParent<RatController>();
    }


    void Update()
    {
        patienceWheel.fillAmount = Rat.patience; //updates the wheel

        if(Rat.patience == 0)
        {
            patienceWheel.enabled = false;
        }
    }
}
