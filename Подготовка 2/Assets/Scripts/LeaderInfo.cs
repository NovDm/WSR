using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderInfo : MonoBehaviour
{
    public Text LeaderName;
    public Text Count;
    public Image img;

    public List<Sprite> sprites;

    
    //записываем в соответсвующие поля нужные нам значения
    public void Setinfo(string name, int col, int spr)
    {
        LeaderName.text = name;
        Count.text = col.ToString();
        img.sprite = sprites[spr];
    }
    public void Setinfo(DataLoad.leader inp)
    {
        LeaderName.text = inp.name;
        Count.text = inp.count.ToString();
        img.sprite = sprites[inp.SpriteNum];
    }

}
