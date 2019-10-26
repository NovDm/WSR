using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    //номер открываемого кубка
    public int prizeID;
    //маска для перетаскивания
    public GameObject mask;
    //замок
    public GameObject Lock;

    private Vector3 end_pos;
    public Transform startPos;
    private float progress;

    // Start is called before the first frame update
    void Start()
    {
        end_pos = mask.transform.position;
        mask.transform.position = startPos.position;

        int pr = 0;
        //найти прогресс кубка 
        if (prizeID < 7)
        {
            pr = Player.correct_answers_in_theme[prizeID];
            if (pr > 0)
                Lock.SetActive(false);

        }
        else
        {
            pr = 0;
            for(int i=0;i < 6;i++)
            {
                if (Player.correct_answers_in_theme[i] == 10)
                    pr++;
            }
            if (pr > 0)
                Lock.SetActive(false);
        }
        //передвинуть панель
        progress = (pr * 1f) / 10f;
        mask.transform.position = Vector3.Lerp(startPos.transform.position, end_pos, progress);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
