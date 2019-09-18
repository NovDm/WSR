using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransformOnMouseUp : MonoBehaviour
{
    bool MouseDOWN = false;
    bool ret;
    public float Distance = 10f;
    public Transform T;
    Vector3 startpos;
    

    void OnMouseDown()
    {
        MouseDOWN = true;
    }

     void OnMouseUp()
    {
        MouseDOWN = false;  
        ret = true;
    }

    private void Start()
    {
        startpos = this.transform.position;
    }

    void Update()
    {

        Vector3 Cursor = Input.mousePosition;
        Cursor = Camera.main.ScreenToWorldPoint(Cursor);
        Cursor.z = 0;
        Vector3 L = T.position;


        if (MouseDOWN)
        {
            this.transform.position = Cursor;
        }
        if (ret)
        {
            this.transform.position = startpos;
            ret = false;
        }
    }

    public void ColliderEnter(Vector3 target)
    {
        startpos = target;
    }
}
