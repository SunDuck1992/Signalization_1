using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detect : MonoBehaviour
{
    public event UnityAction Entered;
    public event UnityAction CameOut;

    public void OnTriggerEnter2D(Collider2D collision)
    {       
            Entered?.Invoke();       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {              
            CameOut?.Invoke();        
    }
}
