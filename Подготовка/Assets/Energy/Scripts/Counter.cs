using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    Text text;
    public GameObject panel;
    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
        text.text = "0";
    }

    public void CounterInc()
    {
        int tmp = int.Parse(text.text);
        tmp++;
        text.text = tmp.ToString();
    }
   
    void Update()
    {
    }
}
