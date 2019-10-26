using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThemeSelection : MonoBehaviour, IPointerDownHandler
{
    //номер темы
    public int id;

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.selected_theme = id;
    }


}
