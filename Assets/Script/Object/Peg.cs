using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallbackDestroy(Peg peg);
public class Peg : MonoBehaviour
{
    public CallbackDestroy callbackDestroy = null;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (callbackDestroy != null)
            callbackDestroy(this);
    }
}
