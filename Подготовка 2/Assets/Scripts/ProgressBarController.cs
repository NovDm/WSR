using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    //звёзды для отображения прогресса уровня
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    //для отображения бара прогресса
    public GameObject mask;
    public Transform targetPos;

    private float progress = 0;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = mask.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        progress = (Game.Instance.cur_quest * 1f) / 10f;
        if (Game.Instance.cur_quest >= 2)
            star1.SetActive(true);
        if (Game.Instance.cur_quest >= 5)
            star2.SetActive(true);
        if (Game.Instance.cur_quest >= 10)
            star3.SetActive(true);
        mask.transform.position = Vector3.Lerp(startPos, targetPos.position, progress+0.005f);
    }
}
