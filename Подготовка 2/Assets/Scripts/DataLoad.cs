using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataLoad : MonoBehaviour
{
    //для хранения данных о всех уровнях игрока сделан класс lvl
    public class lvl
    {
        public string name; //название звания
        public int maxE; //максимальное значение энергии
        public int maxS; //сколько звёзд до следующего уровня
        public lvl()
        {
            name = ""; maxE = 0; maxS = 0;
        }
    }
    //массив хранящий данные о всех уровнях
    public static List<lvl> levels;

    //для хранения данных о задаваемых вопросах сделан класс question
    public class question
    {
        public string text; //текст вопроса
        public string ans; //ответ на вопрос
        public question() { text = ""; ans = ""; }
    }
    //двумерный массив хранящий данные о вопросах по всем темамам
    //каждый одномерный массив (строка двумерного) соотвествует вопросам по одной из тем
    public static List<List<question>> questions;

    //для получения ежедневных бонусов необходимо хранить информацию о них
    //класс bonus хранит информацию о одном бонусе
    public class bonus
    {
        public int en;
        public int mon;
        public bonus(int e,int m) { en = e; mon = m; }
    }
    //массив bonuses хранит информацию о всех бонусах
    public static List<bonus> bonuses;
    

    public class leader
    {
        public string name;
        public int SpriteNum;
        public int count;
    }
    public static List<leader> leaders;

   private static void load()
    {
        //объявляем файл и файловый поток.
        //для работы с файлами подключить System.Text;
        //информация о вопросах хранится в файле questions.txt
        TextAsset bindata = Resources.Load("questions") as TextAsset;
        //читаем информацию до конца файла и разбиваем на строки по символу '\n'
        string[] read = (bindata.text).Split('\n');
        //при чтении из файла переход на новую строку состоит не только из '\n'
        //но и из '\r'
        //эту /r необходимо убрать, чтобы она не мешала корректной обработке информации
        //она всегда стоит на последнем месте в строке, поэтому удаляем последний символ
        //в последнем слове её нет.
        for (int i = 0; i < read.Length - 1; i++)
            read[i] = read[i].Remove(read[i].Length - 1);

        //инициализируем массив
        questions = new List<List<question>>();
        for (int i = 0; i < 7; i++)
            questions.Add(new List<question>());

        //обрабатываем полученную ранее последовательность строк
        //поскольку данные о вопросе содержатся в 3х строках сразу, то используется i+=3
        for ( int i= 0; i< read.Length;i+=3 )
        {
            question tmp = new question();
            //первая строка - номер темы. Парсим её в число
            int type = int.Parse(read[i]);
            //вторая строка это текст вопроса
            tmp.text = read[i+1];
            //третья строка это ответ на вопрос
            tmp.ans = read[i+2];
            //не забываем добавлять в массив
            questions[type].Add(tmp);
        }

        bindata = Resources.Load("lvls") as TextAsset;

        //читаем информацию до конца файла и разбиваем на строки по символу '\n'
        read = (bindata.text).Split('\n');
        //удаляем символ /r
        for (int i = 0; i < read.Length - 1; i++)
            read[i] = read[i].Remove(read[i].Length - 1);
        //объявляем массив
        levels = new List<lvl>();
        //данные об одном уровне хранятся в пределах одной строки поэтому i++
        for (int i = 0; i < read.Length; i++)
        {
            //разбиваем строку на слова разделённые пробелом
            string[] temp = read[i].Split(' ');
            lvl tmp = new lvl();
            tmp.name = temp[0]; //первое слово это звание
            tmp.maxS = int.Parse(temp[1]); //второе это число звёзд нужное для следующего звания
            tmp.maxE = int.Parse(temp[2]); //третье это число максимальное значение энергии
            //не забываем добавлять в массив
            levels.Add(tmp);
        }

        bindata = Resources.Load("leaders") as TextAsset;
        //читаем информацию до конца файла и разбиваем на строки по символу '\n'
        read = (bindata.text).Split('\n');
        //удаляем символ /r
        for (int i = 0; i < read.Length - 1; i++)
            read[i] = read[i].Remove(read[i].Length - 1);
        leaders = new List<leader>();
        //данные об одном лидере хранятся в пределах одной строки поэтому i++
        for (int i = 0; i < read.Length; i++)
        {
            //разбиваем строку на слова разделённые пробелом
            string[] temp = read[i].Split(' ');
            leader tmp = new leader();
            tmp.name = temp[0]; //первое слово это имя
            tmp.count = int.Parse(temp[1]); //второе это число звёзд 
            tmp.SpriteNum = int.Parse(temp[2]); //третье это номер аватарки
            //не забываем добавлять в массив
            leaders.Add(tmp);
        }
    }

    // awake потому, что данные, загружаемые здесь используются в функциях start других скриптов
    void Awake()
    {
        //добавляем данные о бонусах
        bonuses = new List<bonus>();
        bonuses.Add(new bonus(0, 5));
        bonuses.Add(new bonus(0, 10));
        bonuses.Add(new bonus(0, 15));
        bonuses.Add(new bonus(1, 20));
        bonuses.Add(new bonus(3, 25));
        bonuses.Add(new bonus(3, 25));
        bonuses.Add(new bonus(3, 25));
        //загружаем информацию из файлов
        load();
    }

}
