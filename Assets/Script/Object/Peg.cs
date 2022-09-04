using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallbackDestroy(Peg peg);
public delegate void CallbackReflesh();
public class Peg : MonoBehaviour
{
    public CallbackDestroy callbackDestroy = null;
    public CallbackReflesh callbackReflesh = null;
    int collisioncount = 0;
    void start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (transform.tag)
        {
            case "OriPeg":
            case "CriPeg":
                if (callbackDestroy != null)
                    callbackDestroy(this);
                break;
            case "BomPeg":
                collisioncount++;
                if (collisioncount >= 2)
                {
                    OrbPool.Inst.GetBomb(); //플레이어한테
                    if (callbackDestroy != null)
                        callbackDestroy(this);
                }
                break;
            case "RefPeg":
                if (callbackReflesh != null)
                    callbackReflesh();
                if (callbackDestroy != null)
                    callbackDestroy(this);
                break;
        }
        
    }
}
