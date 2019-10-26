using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text money;
    public Text energy;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        money.text = (10 * (Game.Instance.cur_quest)).ToString();
        energy.text = (3 * (Game.Instance.cur_quest)).ToString();
        if (Game.Instance.cur_quest>=1)
        {
            star1.SetActive(true);
            if (Game.Instance.cur_quest >= 5)
            {
                star2.SetActive(true);
                if (Game.Instance.cur_quest >= 10)
                    star3.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
