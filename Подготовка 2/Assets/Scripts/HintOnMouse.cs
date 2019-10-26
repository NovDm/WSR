using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintOnMouse : MonoBehaviour
{
    //курутина для вывода на экран подсказок 

    //текст куда выводим
    Text text;
    //панелька на которой расположен текст
    public GameObject panel;
    //строка которая должна выводиться
    public string content;
    public GameObject canvas;

    private bool SpaceInp = false; //флаг нажатия пробела моментального завершения курутины

    // Start is called before the first frame update
    void Start()
    {
        //получили текст
        text = panel.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //отслеживаем пробел
        if (Input.GetKeyDown("space"))
        {
            SpaceInp = true;
        }
    }

    public void OnMouseEnter()
    {
        text.text = "";
        panel.SetActive(true);
        StartCoroutine("appear");
        panel.transform.SetParent(canvas.transform);
    }
    private void OnMouseExit()
    {
        panel.SetActive(false);
        StopCoroutine("appear");
        panel.transform.SetParent(gameObject.transform);
    }

    IEnumerator appear()
    {
        for (int i = 0; i < content.Length; i++)
        {
            if (!SpaceInp) //продолжаем работать если пробел не нажат. Если нажат то сразу заканчиваем.
            {
                text.text += content[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
        text.text = content;
        SpaceInp = false;
    }

}
