using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderManager : MonoBehaviour
{
    //для обращения к сущности из других скриптов
    private static LeaderManager instance;
    public static LeaderManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LeaderManager>();
            }
            return instance;
        }
        set
        {

        }
    }


    //список спрайтов персонажей
    public List<Sprite> player_icons;
    //список геймобджектов которые видны на экране
    public GameObject[] leaders;
    //клавиши влево-вправо
    public Button left;
    public Button right;

    //сдвиг от начала массива
    private int firstnum;

    // Start is called before the first frame update
    void Start()
    {
        //изначаль но свдига нет
        firstnum = 0;
        leaders = GameObject.FindGameObjectsWithTag("leader");

        //добавляем действия на стрелочки
        left.onClick.AddListener(LeftClick);
        right.onClick.AddListener(rightclick);
        //добавляем информацию о игроке в таблицу лидеров
        UpdatePlayerInfo();
        //пересортируем
        LeaderSort();
        //обновим элементы на экране
        refresh();
    }

    //добавить текущую информацию о игроке в список лидеров
    public void UpdatePlayerInfo()
    {
        DataLoad.leader me = Player.GetInfo();
        //пытаемся понять, добавили ли мы уже текущего игрока к списку лидеров
        bool flag = false;
        for( int i=0;i<DataLoad.leaders.Count;i++)
        {
            if (DataLoad.leaders[i].name == me.name)
            {
                flag = true;
                DataLoad.leaders[i] = me;
                break;
            }
        }
        //если нас ещё нет в списке лидеров, то добавить нас
        if (flag == false)
            DataLoad.leaders.Add(me);
    }

    //сортировка списка лидеров
    //выполняется с помощью лямбда выражения
    public void LeaderSort()
    {
        //лямбда выражение определяет то, как будут сравниваться экземпляра класса
        DataLoad.leaders.Sort((a,b) => a.count.CompareTo(b.count));
        DataLoad.leaders.Reverse();
    }
    //выводит список лидеров на экран в соответствии со сдвигом firstnum от начала массива
    public void refresh()
    {
        for (int i = 0; i < 6; i++)
        {
            //берём компонент скрипта и через него вывзываем функцию для записи информации
            LeaderInfo a = leaders[i].GetComponent<LeaderInfo>();
            a.Setinfo(DataLoad.leaders[i + firstnum]);
        }
    }
    //стрелка вправо - сдвиг по списку вверх
    public void rightclick()
    {
        if (firstnum + 6 < DataLoad.leaders.Count)
            firstnum++;
        refresh();
    }
    //сдвиг по списку вниз
    public void LeftClick()
    {
        if (firstnum > 0)
            firstnum--;
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
