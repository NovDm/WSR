using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONOFBUTT : MonoBehaviour
{
    public GameObject panel;
    private bool flag=false;
    public void OnOf()
    {
        if (!flag)
        {
            panel.SetActive(true);
            flag = true;
        }
        else
        {
            panel.SetActive(false);
            flag = false;
        }
    }

   
}
