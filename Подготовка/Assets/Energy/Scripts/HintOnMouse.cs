using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintOnMouse: MonoBehaviour
{
    float time = 1;
    bool Timeflag = false;
    bool MouseFlag = false;
    Text text;
    public GameObject panel;
    private bool flag = false;
    public string content = "Это энергия её\r\n зовут Вася";

    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //if (MouseFlag)
        //{
        //    time -= Time.deltaTime;
            
        //}
        //if (time <= 0)
        //{
        //    Timeflag = true;
        //    time = 1;
        //}
    }
    public void OnMouseEnter()
    {
        //MouseFlag = true;
        //if (Timeflag)
        //{
            text.text = "";
            panel.SetActive(true);
            flag = true;
            StartCoroutine("appear");
        //}
    }
    private void OnMouseExit()
    {
        //MouseFlag = false;
        panel.SetActive(false);
        flag = false;
        StopCoroutine("appear");
    }
    IEnumerator appear()
    {
        for (int i = 0; i < content.Length; i++)
        {
            text.text += content[i];

            yield return new WaitForSeconds(0.1f);
        }
        flag = false;
    }
}
