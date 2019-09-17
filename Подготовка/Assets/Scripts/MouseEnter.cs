using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEnter : MonoBehaviour
{
    Text text;
    public GameObject panel;
    private bool flag = false;
    string content = "Это энергия её\r\n зовут Вася";

    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
    }
    public void OnMouseEnter()
    {
        text.text = "";
        panel.SetActive(true);
        flag = true;
        StartCoroutine("appear");
    }
    private void OnMouseExit()
    {

        panel.SetActive(false);
        flag = false;
        StopCoroutine("appear");
    }
    IEnumerator appear()
    {
        for (int i = 0; i < content.Length; i++)
        {
            text.text += content[i];

            yield return new WaitForSeconds(0.1f);
        }
        flag = false;
    }
}
