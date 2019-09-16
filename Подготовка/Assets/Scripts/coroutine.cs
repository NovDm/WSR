using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coroutine : MonoBehaviour
{
    Text text;
    bool flag=false;
    string content = "Привет меня \r\n зовут Вася"; //что должно быть записанов кнопку
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = "";
    }

    private void OnMouseEnter()
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine("appear");
        }
    }

    IEnumerator appear()
    {        
        for (int i = 0; i < content.Length; i++)
        {
            text.text += content[i];

            yield return new WaitForSeconds (0.1f);
        }
        flag = false;
    }
}
