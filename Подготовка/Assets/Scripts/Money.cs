using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    Text text;
    public GameObject panel;

    void Start()
    {
        text = panel.GetComponentInChildren<Text>();
        text.text = "0";
    }
    public void MoneyInc50()
    {
        int coin = int.Parse(text.text);
        coin +=50;
        text.text = coin.ToString();
    }
    public void MoneyInc100()
    {
        int coin = int.Parse(text.text);
        coin += 100;
        text.text = coin.ToString();
    }
    public void MoneyInc300()
    {
        int coin = int.Parse(text.text);
        coin += 300;
        text.text = coin.ToString();
    }
    public void MoneyInc1000()
    {
        int coin = int.Parse(text.text);
        coin += 1000;
        text.text = coin.ToString();
    }
    public void Money100()
    {
        int coin = int.Parse(text.text);
        coin -= 100;
        text.text = coin.ToString();
    }
    public void Money50()
    {
        int coin = int.Parse(text.text);
        coin -= 50;
        text.text = coin.ToString();
    }
    public void Money10()
    {
        int coin = int.Parse(text.text);
        coin -= 10;
        text.text = coin.ToString();
    }

}
