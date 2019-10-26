using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusText : MonoBehaviour
{
    //управляет текстом, который находится на понели ежедневного бонуса
    public int id; //номер дня (начинается с 0)
    public Text day;
    public Text energy;
    public Text money;
    public GameObject status;
    //спрайты для отображения бонуса
    public Sprite[] sprites;
    
    void Start()
    {
        if(id<Player.day)
        {
            status.SetActive(true);
        }
        else
            status.SetActive(false);


        Image img = GetComponent<Image>();
        //при старте опредлить какой день обозначает конкретный бонус
        if (id == Player.day) 
        {
            //если день равен текущему дню у класса игрок, то это сегодняшний день
            img.sprite = sprites[0];
            day.text = "Сегодня";
        }
        else
        {
            //если нет, то это может быть день следующий, тогда назовём его "завтра"
            if (id == (Player.day + 1)%7)
            {
                img.sprite = sprites[1];
                day.text = "Завтра";
            }
            else
            {
                //ну и в остальных случаях день просто записывается с цифрой его номера
                img.sprite = sprites[2];
                day.text = "День " + (id+1).ToString();
            }
        }
        //в соответсвующие поля подписываем сколько бонусов получит игрок
        energy.text = DataLoad.bonuses[id].en.ToString();
        money.text = DataLoad.bonuses[id].mon.ToString();

    }
}
