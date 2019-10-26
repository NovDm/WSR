using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject letter;

    [HideInInspector]
    //для возврата объектов при драг-н-дроп
    public Vector3 StartPos;
    private Vector3 TmpPos;
    //флажки для состояния букв
    private bool is_returning;
    private bool is_droped;

    //картинка для анимации
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = (GetComponentInChildren<Image>());
        Button a = GetComponentInChildren<Button>();
        a.onClick.AddListener(MouseDown);
        is_returning = false;
        is_droped = false;
        StartPos = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (!is_dragable)
            letter.transform.position = letter.transform.position + new Vector3(0, 0.1f, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (!is_dragable)
            letter.transform.position = letter.transform.position - new Vector3(0, 0.1f, 0);
    }

    //если буква перетаскивается, то поменять её координаты.
    //если буква возвращается, то нельзя с ней взаимодействовать
    public void OnDrag(PointerEventData data)
    {
        if (!is_returning)
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(MousePos.x, MousePos.y, transform.position.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!is_returning && !is_droped)
        {
            if(tag =="Letter")
                StartCoroutine("ReturnToStartPos");
            if(tag == "AnsLetter")
                StartCoroutine(ReturnToTmpPos());
            is_returning = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //луч в точку броска
        RaycastHit2D target;
        //выключить рейкаст для попадания в то, куда кинул
        img.raycastTarget = false;
        target = Physics2D.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //потом обратно включить рейкаст
        img.raycastTarget = true;
        if ( (target != null) && (target.collider != null))
        {
            if(target.collider.gameObject.tag == "LetterBack")
            {
                is_droped = true;
                Game.Instance.AddLetterToAns(gameObject,target.collider.gameObject);
            }
            if (target.collider.gameObject.tag == "Letter" || target.collider.gameObject.tag == "ABCbox")
            {
                is_droped = true;
                Game.Instance.RemoveLetterFromAns(gameObject);
                StartCoroutine(ReturnToStartPos());
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TmpPos = transform.position;
        is_droped = false;
    }

    private void MouseDown()
    {
        if (!is_returning)
        {
            //в зависимости от того, какая эта буква - вызываем две разных функции
            if (tag == "Letter")
                //либо мы перемещаем букву в ответ и это тег "Letter"
                Game.Instance.AddLetterToAns(gameObject);
            else
            {
                //либо мы убираем букву из ответ и это был тег "AnsLetter"
                Game.Instance.RemoveLetterFromAns(gameObject);
            }
        }
    }


    private float progress;

    IEnumerator ReturnToStartPos()
    {
        progress = 0;
        while (progress < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, StartPos, progress);
            progress += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        is_returning = false;
    }

    IEnumerator Fly(Vector3 target)
    {
        progress = 0;
        while (progress < 1f)
        {
            transform.position = Vector3.Lerp(TmpPos, target, progress);
            progress += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        is_returning = false;
    }

    IEnumerator ReturnToTmpPos()
    {
        float progress = 0;
        while (progress < 1)
        {
            transform.position = Vector3.Lerp(transform.position, TmpPos, progress);
            progress += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        is_returning = false;
    }
    public void Return()
    {
        //transform.position = StartPos;
       StartCoroutine(ReturnToStartPos());
    }
    public void MomentReturn()
    {
        transform.position = StartPos;
    }

    public void FlyToTarget(Vector3 target)
    {
        TmpPos = transform.position;
        StartCoroutine(Fly(target));
    }
    
}
