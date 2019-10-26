using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    //для обращения к сущности из других скриптов
    private static GuiManager instance;
    public static GuiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GuiManager>();
            }
            return instance;
        }
        set
        {

        }
    }

    //панели с элементами
    public GameObject shop;
    public GameObject lose;
    public GameObject win;
    public GameObject daybonus;

    //ивенты управления таймерами
    private UnityEvent en_timer_stop;
    private UnityEvent en_timer_start;

    //для определения первого запуска скрипта
    private static bool firststart = true;

    // Start is called before the first frame update
    void Start()
    {

        en_timer_stop = new UnityEvent();
        en_timer_stop.AddListener(EnergyTimer.Instance.timerstop);
        en_timer_start = new UnityEvent();
        en_timer_start.AddListener(EnergyTimer.Instance.timerstart);
        //если приложение только запущено, то вывести окно бонуса
        if (firststart == true)
        {
            BonusClick();
            firststart = false;
        }

    }

    //перейти на главную сцену
    public void HomeClick()
    {
        SceneManager.LoadScene(0);
    }
    //включить/выключить меню магазина
    public void ShopClick()
    {
        shop.SetActive(!shop.activeSelf);
    }
    //закрыть меню получения бонуса, без его взятия
    public void BonusClick()
    {
        daybonus.SetActive(!daybonus.activeSelf);
    }
    //перейти на сцену с игрой, согласно выбранной теме и тратя энергию
    public void GameClick()
    {
        if (Player.energy >= 5)
        {
            EnergyChange(-5);
            SceneManager.LoadScene(1);
        }
        else
        {
            Instance.ShopClick();
        }
    }
    //перейти а сцену с трофеями
    public void HallClick()
    {
        SceneManager.LoadScene(2);
    }
    //открыть меню проигрыша игры
    public void Lose()
    {
        if (Game.Instance.cur_quest <= 1)
        {
            lose.SetActive(true);
            if (shop.activeSelf)
                shop.SetActive(false);
        }
        else
        {
            WinMenu();
            if (shop.activeSelf)
                shop.SetActive(false);
        }
    }
    //переиграть игру при проигрыше. Тратит 10 энергии. 
    public void RestartGame()
    {
        if (Player.energy >= 10)
        {
            EnergyChange(-10);
            SceneManager.LoadScene(1);
        }
        else
        {
            Instance.ShopClick();
        }

    }
    //включить панель выйгрыша, если требуется
    public void WinMenu()
    {
        win.SetActive(!win.activeSelf);
        //кроме того, необходимо запомнить на сколько вопросов максимально 
        //ответил игрок в данной теме
        if(Player.correct_answers_in_theme[Player.selected_theme] <= Game.Instance.cur_quest)
        {
            Player.correct_answers_in_theme[Player.selected_theme] = Game.Instance.cur_quest;
        }
    }
    //нажатие на кнопку выйгрыша
    //энергии 3*количество ответов
    //денег 10*количество ответов
    public void GetReward()
    {
        MoneyChange(10 * (Game.Instance.cur_quest));
        EnergyChange(3 * (Game.Instance.cur_quest));
        SceneManager.LoadScene(0);
    }
    //событие для изменения количества монет
    public void MoneyChange(int count)
    {
        Player.money += count;
    }
    //событие для изменения количества энергии
    public void EnergyChange(int count)
    {
        if (Player.energy + count >= DataLoad.levels[Player.lvl].maxE)
        {
            //если энергии больше максимума
            Player.energy = DataLoad.levels[Player.lvl].maxE;
            en_timer_stop.Invoke();
        }
        else
        {
            //если энергии меньше максимума
            Player.energy += count;
            en_timer_start.Invoke();
        }
    }
    //плюс время цена 50
    public void HintTime()
    {
        if (Game.Instance.Ans_status() != true)
        {
            if (Player.money >= 50)
            {
                GameTimer.increase();
                MoneyChange(-50);
            }
            else
            {
                Instance.ShopClick();
            }
        }
    }
    //пропускает слово цена 100
    public void HintSkipWord()
    {
        if (Game.Instance.Ans_status() != true)
        {
            if (Player.money >= 100)
            {
                //изменить сцену
                Game.Instance.SkipWord();
                //вычесть деньги
                MoneyChange(-100);

            }
            else
            {
                Instance.ShopClick();
            }
        }
    }
    //убрать лишние буквы цена 50
    public void HintRemoveLetters()
    {
        if (Game.Instance.Ans_status() != true)
        {
            if (Player.money >= 50)
            {
                Game.Instance.RemoveLetters();
                MoneyChange(-50);
            }
            else
            {
                Instance.ShopClick();
            }
        }
    }
    //Открыть случайную букву цена 50
    public void HintOpenLetter()
    {
        if (Game.Instance.Ans_status() != true)
        {
            if (Player.money >= 50)
            {
                Game.Instance.OpenLetter();
                MoneyChange(-50);
            }
            else
            {
                Instance.ShopClick();
            }
        }
    }
    //открыть ответ бесплатно но один раз
    public void HintOpenAnswer()
    {
        if (Game.Instance.Ans_status() != true)
        {
            Game.Instance.OpenAnswer();
            //после этого отключаем эту подсказку до новой игры
            GameObject obj = GameObject.FindGameObjectWithTag("SkipWord");
            (obj.GetComponent<Button>()).enabled = false;
        }
    }
    //получить ежедневный бонус
    public void GetBonus()
    {
        MoneyChange(DataLoad.bonuses[Player.day].mon);
        EnergyChange(DataLoad.bonuses[Player.day].en);
        Player.day++;
        if (Player.day == 7)
            Player.day = 0;
        daybonus.SetActive(false);
    }


}
