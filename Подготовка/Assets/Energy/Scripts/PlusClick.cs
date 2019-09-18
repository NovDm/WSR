using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlusClick : MonoBehaviour
{
    public UnityEvent Clik;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        Clik.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
