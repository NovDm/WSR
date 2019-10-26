using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuy : MonoBehaviour
{
    //количество и тип
    public int count;
    public string type;
    
    void Start()
    {
        //подпсать кнопку магазина на нужную функцию в зависимости от её типа
        Button a = GetComponent<Button>();
        if (type == "Money")
            a.onClick.AddListener(() => { GuiManager.Instance.MoneyChange(count); });
        if (type == "Energy")
            a.onClick.AddListener(() => { GuiManager.Instance.EnergyChange(count); });
    }
}
