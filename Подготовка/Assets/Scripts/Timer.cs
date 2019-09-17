using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    public GameObject panel;
    private float gameTime = 15*60f;
    public GameObject Schet;
    private int i = 0;
    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
    }

   
    void Update()
    {
       
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;

        }

        int seconds = (int)gameTime;
        int min = seconds / 60;
        int sec = seconds % 60;


        text.text = min.ToString() + ":";
        if (sec < 10)
            text.text += "0";
        text.text += sec.ToString();

        if (gameTime <= 0)
            gameTime = 60 * 15f;

        //string tmp = gameTime.ToString();
        //text.text += (tmp.Split(','))[0];
        //text.text += ":" + (tmp.Split(','))[1][0]+ (tmp.Split(','))[1][1];
        
    }
}
