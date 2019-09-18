using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyHint : MonoBehaviour
{
    public GameObject panel;
    public UnityEvent Money100;
    public UnityEvent Money50;
    public UnityEvent Money10;

    private void OnMouseDown()
    {
        Text str;
        str = panel.GetComponentInChildren<Text>();

        int num = int.Parse(str.text);
        if (num == 100)
        {
            Money100.Invoke();
        }
        if (num == 50)
        {
            Money50.Invoke();
        }
        if (num == 10)
        {
            Money10.Invoke();
        }
        


    }
}
