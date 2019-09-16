using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtAnimation : MonoBehaviour
{
    void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
     void OnMouseDown()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

}
 