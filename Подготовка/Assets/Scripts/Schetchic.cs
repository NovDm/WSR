using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Schetchic : MonoBehaviour
{
    Text text;
    private int Num = 0;
    public GameObject panel;
    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
    }

   
    void Update()
    {
        text.text = Num.ToString();
    }
}
