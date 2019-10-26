using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    //Этот таймер пришлось сделать полностью статичным, чтобы он адекванто работал
    // таймер хранит данные о том, куда он выводит текст, о текущем
    //значении времени, стартовом времени
    public Text timertext;
    private static float CurrTime;
    public static float MaxTime;
    //флаг управляющий состоянием таймера ( работает/нет)
    private static bool work;
    public static bool Work
    {
        get
        {
            return work;
        }
    }       
    
    void Start()
    {
        //изначально установлен в положение работать.
        MaxTime = 30;
        work = true;
        //при старте игры инициализируется максимальным значением
        CurrTime = MaxTime;
    }

    //события для старта/остановки таймера
    //рестарта таймера с максимальным временем
    //увеличения времени таймера на +30 секунд
    public static void start()
    {
        work = true;
    }
    public static void restart()
    {
        CurrTime = MaxTime;
        work = true;
    }
    public static void stop()
    {
        work = false;
    }
    public static void increase()
    {
        CurrTime += 30;
    }

    void Update()
    {
        if (work) //если включён
        {
            if (CurrTime > 0)
            {
                CurrTime -= Time.deltaTime;

            }
            //обрабатывает время
            int seconds = (int)CurrTime;
            int min = seconds / 60;
            int sec = seconds % 60;

            //выводит на экран
            timertext.text = min.ToString() + ":";
            if (sec < 10)
                timertext.text += "0";
            timertext.text += sec.ToString();

            //если время истекло, то вызывается меню луз
            //таймер останавливается и устанавливается максимальным значением.
            if (CurrTime <= 0)
            {
                work = false;
                GuiManager.Instance.Lose();
            }
        }


    }
}
