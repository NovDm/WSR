using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    Text text;
    public GameObject panel;
    public float gameTime = 15*60f;
    public float time2;

    public UnityEvent CounterTik;

    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
        time2 = gameTime;
    }

   
    void Update()
    {
       
        if (gameTime > 0)
        {
            time2 -= Time.deltaTime;

        }

        int seconds = (int)time2;
        int min = seconds / 60;
        int sec = seconds % 60;


        text.text = min.ToString() + ":";
        if (sec < 10)
            text.text += "0";
        text.text += sec.ToString();

        if (time2 <= 0)
        {
            time2 = gameTime;
            CounterTik.Invoke();
        }

        //string tmp = gameTime.ToString();
        //text.text += (tmp.Split(','))[0];
        //text.text += ":" + (tmp.Split(','))[1][0]+ (tmp.Split(','))[1][1];
        
    }
}
