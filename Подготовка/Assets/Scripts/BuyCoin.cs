using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyCoin : MonoBehaviour
{
    public UnityEvent Money50;
    public UnityEvent Money100;
    public UnityEvent Money300;
    public UnityEvent Money1000;

    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Text str;
        str = GetComponentInChildren<Text>();

        int num = int.Parse(str.text);
        if (num == 50)
        {
            Money50.Invoke();
        }
        if (num == 100)
        {
            Money100.Invoke();
        }
        if (num == 300)
        {
            Money300.Invoke();
        }
        if (num == 1000)
        {
            Money1000.Invoke();
        }


    }
}
