using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Game : MonoBehaviour
{

    //переменная Instance для общения с нестатическими методами этого класса
    private static Game instance;
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Game>();
            }
            return instance;
        }
        set
        {

        }
    }
    //кнопка далее
    public GameObject next;

    //параметры задающиеся через юнити

        //объекты 
    //Ведущий
    public GameObject character;
    public Sprite[] characterSprites;
    //Текст бокс для вопроса
    public Text QuestionTextBox;
    //Контейнеры для букв
    public GameObject AnswerBox;
    public GameObject LetterBox;
    //префаб для макета буквы в строке ответов
    public GameObject AnsLetterModel;
    public Texture2D curs;
    private static List<DataLoad.question> Questions; //запрос текущих вопросов по выбранной теме


    private List<GameObject> AnsBox;  //буквы ответа    
    private List<GameObject> ChoiseObj;   //буквы выбора
    public int cur_quest;//количество отвеченных вопросов в текущей сессии


    void Start()
    {
        Cursor.SetCursor(curs, Vector3.zero,CursorMode.Auto);
        //добавляем фукнцию кнопке далее
        Button tmp = next.GetComponent<Button>();
        tmp.onClick.AddListener(NextQUestion);

        //игра начинаестя с 1 вопроса, он имеет порядковый номер 0
        cur_quest = 0;

        Image img = character.GetComponent<Image>();
        img.sprite = characterSprites[(int)(Random.Range(0, 5))];

        //этот блок буду доделывать, тут слишком много неточности
        //загрузить список вопросов по этой теме
        Questions = LoadQuestions();
               
        //задать первый вопрос в текст бокс
        QuestionTextBox.text = Questions[cur_quest].text;
        //Объявление массива под ответ
        AnsBox = new List<GameObject>();
        //найти 30 объектов и добавить в выбор
        ChoiseObj = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Letter"))
        {
            ChoiseObj.Add(obj);
        }
        
        //создать нужное количество макетов с вопросами
        for(int i=0;i < Questions[cur_quest].ans.Length;i++)
        {
            AnsBox.Add(NewModel());
        }
        //определить квы ответа в выборе и остальные буквы
        Reshuffle();
    }

    //создание новой буквы с вопросом
    private GameObject NewModel()
    {
        var obj = Instantiate
                (
                AnsLetterModel,
                transform
                );
        obj.transform.SetParent(AnswerBox.transform);
        obj.transform.localScale = Vector3.one;
        obj.tag = "LetterBack";
        return obj;
    }

    private List<DataLoad.question> LoadQuestions()
    {
        //tmp хранит в себе все вопросы по выбранной теме. Из этих вопросов будем выбирать
        //необходимые нам 10 штук.
        List<DataLoad.question> tmp = new List<DataLoad.question>();
        if (Player.selected_theme != 7)
            tmp.AddRange(DataLoad.questions[Player.selected_theme]);
        else
        {
            //для последней темы(случайные) добавляем в пул все впоросы которые есть
            for (int i = 0; i < 7; i++)
                tmp.AddRange(DataLoad.questions[i]);
        }
        //после загрузки пула вопросов необходимо объявить переменную под результат
        List<DataLoad.question> res = new List<DataLoad.question>();
        //берём случайный индекс
        float rand = Random.Range(0, tmp.Count);
        //пока не взяли 10 вопросов 
        while (res.Count != 10)
        {
            //если полученный случайный вопрос ещё не содержится в res то мы его добавляем
            //а если этот вопрос уже есть, то мы его пропускаем и берём следующее случайное число
            if (res.Contains(tmp[(int)rand]) == false)
            {
                res.Add(tmp[(int)rand]);
            }
            rand = Random.Range(0, tmp.Count);
        }
        return res;
    }

    //перераспределение букв для нового вопроса
    private void Reshuffle()
    {
        //для того, чтобы отличить уже заданные буквы от ещё незаданных
        //мы делаем их все отключёнными
        //и будем включать по мере определения
        int i = 0;
        for (i = 0; i < 30; i++)
            ChoiseObj[i].SetActive(false);
        i = 0;
        //сначала определяем куда попадут буквы которые должны быть в ответе
        //найти места буквам из ответа
        while (i < Questions[cur_quest].ans.Length)
        {
            //определили позицию для буквы
            int pos = Random.Range(0, 30);
            //если эта буква ещё не определена
            if (ChoiseObj[pos].activeSelf != true)
            {
                //то задать ей букву из ответа и активировать
                Text text = ChoiseObj[pos].GetComponentInChildren<Text>();
                text.text = Questions[cur_quest].ans[i].ToString();
                ChoiseObj[pos].SetActive(true);
                i++;
            }
            //повторять, пока не будут выставлены все буквы из ответа
        }
        //запонить оставшиеся места случайными буквами
        for (i = 0; i < ChoiseObj.Count; i++)
        {
            //всем неактивировнным буквам задать случайные значения
            if (ChoiseObj[i].activeSelf == false)
            {
                Text text = ChoiseObj[i].GetComponentInChildren<Text>();
                //случайный символ
                char sym = (char)((int)'а' + (int)(Random.Range(0f, 32f) * 10000 / 10000));
                text.text = sym.ToString();
                //активировать
                ChoiseObj[i].SetActive(true);
            }
        }

    }

    //добавить выбранную букву на определённую позицию ответа
    public void AddLetterToAns(GameObject inp,GameObject target)
    {
        //переместить букву в контейнер с буквами ответа и поменять тег.
        inp.transform.SetParent(AnswerBox.transform);
        inp.tag = "AnsLetter";
        //переместить из одного массива в другой
        inp.transform.position = target.transform.position;
        for(int i= 0;i < AnsBox.Count;i++)
        {
            if (AnsBox[i] == target)
                AnsBox[i] = inp;
        }
        Destroy(target);
        ChoiseObj.Remove(inp);
        ReOrderAnswer();
    }

    //добавить букву на место первого слева макета
    public void AddLetterToAns(GameObject inp)
    {
        GameObject target_place;
        foreach(GameObject obj in AnsBox)
        {
            Text txt = obj.GetComponentInChildren<Text>();
            if(txt.text == "?")
            {
                target_place = obj;
                AddLetterToAns(inp, target_place);
                break;
            }
        }

    }

    //удалить выбранную букву из ответа
    public void RemoveLetterFromAns(GameObject inp)
    {
        for(int i=0;i<AnsBox.Count;i++)
        {
            //найти букву в ответе и переместить
            if( AnsBox[i] == inp)
            {
                //На её место поставить макет
                ChoiseObj.Add(AnsBox[i]);
                inp.transform.SetParent(LetterBox.transform);
                inp.tag = "Letter";
                Letter scr = inp.GetComponent<Letter>();
                scr.Return();
                AnsBox[i] = NewModel();
                break;
            }
        }
    }
    //удалить самую правую клавишу
    public void RemoveLetterFromAns()
    {
        //пробежаться по массиву с конца найти первую добавленную
        for (int i = AnsBox.Count - 1; i >= 0; i--)
        {
            if (AnsBox[i].tag == "AnsLetter")
            {
                ChoiseObj.Add(AnsBox[i]);
                AnsBox[i].transform.SetParent(LetterBox.transform);
                AnsBox[i].tag = "Letter";
                Letter scr = AnsBox[i].GetComponent<Letter>();
                scr.Return();
                AnsBox[i] = NewModel();
                break;
            }
        }
    }

    //в AnsBox элементы должны идти в соответсвии с массивом AnsObj
    public void ReOrderAnswer()
    {
        //смена контейнера на канвас
        foreach (GameObject obj in AnsBox)
        {
            obj.transform.SetParent(transform);
        }
        //возврат букв в AnsBox
        foreach (GameObject obj in AnsBox)
        {
            obj.transform.SetParent(AnswerBox.transform);
        }

    }

    //если дан правильный ответ
    public bool Ans_status()
    {
        //нужный ответ хранится в Questions[cur_quest].ans
        //то, что у нас получается сбором букв объектов из массива AnsObj
        string str = "";
        for (int i = 0; i < AnsBox.Count; i++)
        {
            Text text = AnsBox[i].GetComponentInChildren<Text>();
            str += text.text;
        }
        //если совпали, то верно, иначе нет
        return str == Questions[cur_quest].ans;

    }

    //возвращает номер буквы в среди нижних букв, если она там есть
    //или -1 если её там нет
    private int HasLetterInChoise(string sym)
    {
        int num = -1;
        for(int i= 0; i< ChoiseObj.Count;i++)
        {
            //берём компонент текста и смотрим есть ли в нём нужный символ
            Text text = ChoiseObj[i].GetComponentInChildren<Text>();
            if (text.text == sym)
            {
                num = i;
                break;
            }
        }
        return num;
    }

    //очистить буквы ответа
    private void ClearAnsw()
    {
        //если это обычная буква, то венуть её на место
        for (int i = 0; i < AnsBox.Count; i++)
        {
            //найти букву в ответе и переместить
            if (AnsBox[i].tag == "AnsLetter")
            {
                AnsBox[i].transform.SetParent(LetterBox.transform);
                AnsBox[i].tag = "Letter";
                ChoiseObj.Add(AnsBox[i]);
                Letter scr = AnsBox[i].GetComponent<Letter>();
                scr.MomentReturn();
                AnsBox[i] = NewModel();
            }
        }
        foreach (GameObject obj in ChoiseObj)
        {
            obj.SetActive(true);
        }
    }

    //поменять значения под новый вопрос
    public void Corretction()
    {
        QuestionTextBox.text = Questions[cur_quest].text;
        ClearAnsw();

        foreach (GameObject obj in AnsBox)
            Destroy(obj);
        AnsBox.Clear();
        //создать нужное количество макетов с вопросами
        for (int i = 0; i < Questions[cur_quest].ans.Length; i++)
        {
            AnsBox.Add(NewModel());
        }


        Reshuffle();
        GameTimer.restart();
    }

    //ищет нажатю букву среди букв выбора и добавляет в ответ, если есть такая
    private void KeyDown()
    {
        string sym = Input.inputString;
        //если была нажата клавиша клавиатуры
        if (sym != "")
        {
            //если это русский алфавит
            if (sym[0] - 'а' >= 0 && sym[0] - 'а' <= 32 || sym[0] =='ё')
            {
                //ищем букву среди нижних букв
                int num = HasLetterInChoise(sym);
                if (num != -1) //и если нашли, то добавляем
                    AddLetterToAns(ChoiseObj[num]);
            }
            //если это цифра
            if(sym[0] - '0' >= 0 && sym[0] - '0' <= 4)
            {
                int num = int.Parse(sym[0].ToString());
                switch(num)
                {
                    case 0:
                        GuiManager.Instance.HintOpenAnswer();
                        break;
                    case 1:
                        GuiManager.Instance.HintTime();
                        break;
                    case 2:
                        GuiManager.Instance.HintSkipWord();
                        break;
                    case 3:
                        GuiManager.Instance.HintRemoveLetters();
                        break;
                    case 4:
                        GuiManager.Instance.HintOpenLetter();
                        break;
                }

            }
        }
        //если это бекспейс то стираем последнюю добавленную
        if( sym =="\b")
        {
            if( GameTimer.Work)
                RemoveLetterFromAns();
        }
    }
    void Update()
    {


        if (Input.anyKeyDown)
            KeyDown();

        //если дан ответ на вопрос 
        bool status = Ans_status();
        if (status == true)
        {
            GameTimer.stop();
            next.SetActive(true);
        }
    }
    //курутина необходимая для задержки ответа на экране
    //дабы игрок мог его увидеть
    void NextQUestion()
    {
        GameTimer.stop();
        Player.stars += Questions[cur_quest].ans.Length;
        //сделать следующий вопрос
        cur_quest++;
        //перезагрузить исходные данные
        //если это последний вопрос, то вызвать меню вин
        if (cur_quest == 10)
        {
            GuiManager.Instance.WinMenu();
        }
        else
            Corretction();
        //выключить кнопку далее
        next.SetActive(false);
        //для каждого вопроса свой ведущий
        Image img = character.GetComponent<Image>();
        int rand = (Random.Range(0, 5));
        while (img.sprite == characterSprites[rand])
            rand = (Random.Range(0, 5));
        img.sprite = characterSprites[rand];
        
    }



    //убрать лишние буквы
    public void RemoveLetters()
    {
        //очистить поле для ответа
        ClearAnsw();
        //сначала скрыть все буквы, потом оставить лишь отрытые
        foreach (GameObject obj in ChoiseObj)
        {
            obj.SetActive(false);
        }
        //теперь открыть те из них, которые относятся к ответу
        for (int i = 0; i < Questions[cur_quest].ans.Length; i++)
        {
            //для каждого символа
            string sym = Questions[cur_quest].ans[i].ToString();
            foreach (GameObject obj in ChoiseObj)
            {
                //ищем первый такой символ среди неактивированных букв и открываем
                if (obj.activeSelf == false)
                {
                    //для каждой буквы смотрим её содержимое
                    Text text = obj.GetComponentInChildren<Text>();
                    if (text.text == sym)
                    {
                        //если нашли то активируем букву и выходим из цикла foreach
                        obj.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
    //пропускает слово
    public void SkipWord()
    {
        //прибавить счётчик вопроса и провести корректировку букв
        cur_quest++;
        Corretction();
    }
    //Открыть случайную букву
    public void OpenLetter()
    {
        //работает только если ещё не дан ответ
        bool flag = false;
        flag = Ans_status();
        if (!flag)
        {
            //определить позицию куда установили букву
            int pos = -1;
            while (pos == -1)
            {
                //рандомим символ который надо поставить
                int rand = Random.Range(0, Questions[cur_quest].ans.Length);
                string sym = Questions[cur_quest].ans[rand].ToString();

                Text text = AnsBox[rand].GetComponentInChildren<Text>();
                if (text.text == "?" || text.text != sym) //если ячейка не пустая, то проверить не стоит ли там уже этот символ
                {
                    if (text.text != "?") //если на этой позиции что-то стоит, то убрать
                    {
                        RemoveLetterFromAns(AnsBox[rand]);
                    }
                    //поставить нужную букву найдя первую такую среди списка нижних букв
                    //просмотреть все буквы
                    for (pos = 0; pos < ChoiseObj.Count; pos++)
                    {
                        //для каждой буквы смотрим её содержимое
                        text = ChoiseObj[pos].GetComponentInChildren<Text>();
                        if (text.text == sym)
                        {
                            break;
                        }
                    }
                    AddLetterToAns(ChoiseObj[pos], AnsBox[rand]);
                }
            }
        }
    }
    //открыть ответ
    public void OpenAnswer()
    {
        //очистить поле ответа
        ClearAnsw();
        //для всех букв
        for(int i=0; i< Questions[cur_quest].ans.Length;i++)
        {
            string sym = Questions[cur_quest].ans[i].ToString();
            //ищем соответсвующую букву среди букв снизу
            for(int j=0; j< ChoiseObj.Count;j++)
            {   
                Text text = ChoiseObj[j].GetComponentInChildren<Text>();
                //ищем нужную букву ещё не задействованную в ответе
                if (text.text == sym)
                {
                    AddLetterToAns(ChoiseObj[j], AnsBox[i]);
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }

}
