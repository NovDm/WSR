using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// класс игрок содержит данные о текущем игроке
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// переменная отвечающая за текущее значение счётчика увеличения энергии
    /// </summary>
    public static float curr_energy_time;
    /// <summary>
    /// переменная отвечающая за уровень игрока
    /// </summary>
    public static int lvl;
    /// <summary>
    /// какой день подряд игрок заходит в игру
    /// </summary>    
    public static int day;
    /// <summary>
    /// с какого значения берёт начало счётчик энергии
    /// </summary>
    public static float energy_time;
    /// <summary>
    /// текущее значение энергии игрока
    /// </summary>
    public static int energy;
    /// <summary>
    /// текущее значение денег игрока
    /// </summary>
    public static int money;
    /// <summary>
    /// текущее значение звёзд игрока
    /// </summary>
    public static int stars;
    /// <summary>
    /// какая тема была выбрана для викторины
    /// </summary>
    public static int selected_theme;
    /// <summary>
    /// число правильных ответов по каждой теме
    /// </summary>
    public static List<int> correct_answers_in_theme;

    /// <summary>
    /// переменная помогающая выполнить скрипт awake только единожды
    /// </summary>
    private static Transform playerTransform;

    public static AudioSource audio;

    void Awake()
    {
        //берём компонент аудио у плеера
        audio = GetComponent<AudioSource>();
        //говорим, что он должен быт бесконечно проигрываем
        audio.loop = true;

        //при переходе между сценами создаётся дополнительный объект игрок. Для того, чтобы этого
        //не происходило требуется первый иф
        if (playerTransform != null) //уничтожение дополнительного объекта при переходе между сценами
        {
            Destroy(gameObject);
            return;
        }

        Screen.SetResolution(1366, 768, true);
        Player.audio.Play();
        //Инициализация начальных параметров для player.pref, если они уже записаны в системе
        if (PlayerPrefs.HasKey("CorrectAnswers"))
        {//если есть, то загружаем оттуда
            energy = PlayerPrefs.GetInt("Energy");
            stars = PlayerPrefs.GetInt("Stars");
            money = PlayerPrefs.GetInt("Money");
            lvl = PlayerPrefs.GetInt("LvL");
            day = PlayerPrefs.GetInt("Day");
            energy_time = 60;
            curr_energy_time = PlayerPrefs.GetFloat("CurrEnergyTime");
            //получаем данные о ответах хранящие в строке и делим её на числа
            correct_answers_in_theme = new List<int>();
            string[] tmp = PlayerPrefs.GetString("CorrectAnswers").Split(' ');
            for (int i = 0; i < 8; i++)
                correct_answers_in_theme.Add(int.Parse(tmp[i]));
        }
        else
        {
            //если нет, то инициализируем начальные значения
            energy = 20;
            stars = 0;
            money = 0;
            lvl = 0;
            day = 0;
            energy_time = 60;
            curr_energy_time = energy_time;
            //если это первый старт, то ни на один вопрос мы ещё не отвечали
            correct_answers_in_theme = new List<int>();
            for (int i = 0; i < 8; i++)
                correct_answers_in_theme.Add(0);

            PlayerPrefs.SetInt("Energy", 100);
            PlayerPrefs.SetInt("Stars", 0);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("LvL", 0);
            PlayerPrefs.SetInt("Day", 0);
            PlayerPrefs.SetFloat("CurrEnergyTime", 60);

            //загрузить число правлиных ответов в одну строковую переменную
            string tmp = "";
            for (int i = 0; i < 8; i++)
                tmp += correct_answers_in_theme[i].ToString() + " ";
            PlayerPrefs.SetString("CorrectAnswers", tmp);
            PlayerPrefs.Save();
        }
        //для сохранения данных между сценами - не уничтожать объект
        DontDestroyOnLoad(this);
        playerTransform = transform;
    }

    // во время апдейта проверяем, достигли ли мы необходимого для нового уровня числа звёзд
    void Update()
    {
        if (stars >= DataLoad.levels[lvl].maxS && lvl <= 7)
            lvl++;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    //перед закрытием игры мы должны сохранить информацию о текущем игроке
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Energy", energy);
        PlayerPrefs.SetInt("Stars", stars);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("LvL", lvl);
        PlayerPrefs.SetFloat("CurrEnergyTime", curr_energy_time);
        PlayerPrefs.SetInt("Day", day);
        string tmp = "";
        for (int i = 0; i < 8; i++)
            tmp += correct_answers_in_theme[i].ToString() + " ";
        PlayerPrefs.SetString("CorrectAnswers", tmp);
        PlayerPrefs.Save();
    }

    //включить/выключить воспроизведение
    public static void MuteMusic()
    {
        audio.mute = !audio.mute;
    }

    public static void SetVol(float inp)
    {
        audio.volume = inp;
    }
    public static DataLoad.leader GetInfo()
    {
        DataLoad.leader me = new DataLoad.leader();
        me.name = "вы";
        me.count = stars;
        me.SpriteNum = 0;
        return me;
    }

}
