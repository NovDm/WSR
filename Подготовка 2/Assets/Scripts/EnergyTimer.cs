using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyTimer : MonoBehaviour
{
    //instance необходим посколько через него GuiManager в старте подписывается на ивенты
    private static EnergyTimer instance;
    public static EnergyTimer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnergyTimer>();
            }
            return instance;
        }
        set
        {

        }
    }

    //текст содержащий представление таймера
    public Text timertext;
    //флаг управляющий состоянием таймера ( работает/нет)
    private bool work;

    void Start()
    {
        //если у нас энергия максимальна, то работа сразу останавливается
        if (Player.energy >= DataLoad.levels[Player.lvl].maxE)
        {
            work = false;
            Player.energy = DataLoad.levels[Player.lvl].maxE;
        }
        else
            work = true;
    }

    //управляет работой таймера
    void Update()
    {
        //получить значение текущих минут и секунд
        //параметры таймера прописаны у игрока, для того, чтобы их можно было сохранять
        //CurrEnergyTime - текущее значение таймера
        //EnergyTime - стартовое значение таймера, от которого идёт отсчёт
        int seconds = (int)Player.curr_energy_time;
        int min = seconds / 60;
        int sec = seconds % 60;

        //преобразовать числа в текст
        timertext.text = min.ToString() + ":";
        if (sec < 10)
            timertext.text += "0";
        timertext.text += sec.ToString();

        //если таймер запущен, то выполнить вычитание времени
        if (work)
        {
            if (Player.energy_time > 0)
            {
                Player.curr_energy_time -= Time.deltaTime;

            }
            //если таймер дошёл до нуля, то 
            if (Player.curr_energy_time <= 0)
            {
                //восстанавливаем исходное время
                Player.curr_energy_time = Player.energy_time;
                //увеличиваем энергию на 1
                GuiManager.Instance.EnergyChange(1);
            }
;
        }
    }
    //события вызываемые для управления состояниями таймера
    public void timerstop()
    {
        work = false;
    }
    public void timerstart()
    {
        work = true;
    }
}
