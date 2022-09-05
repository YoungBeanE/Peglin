using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallbackDestroy(Peg peg);
public delegate void CallbackReflesh();
public class Peg : MonoBehaviour
{
    public CallbackDestroy callbackDestroy = null;
    public CallbackReflesh callbackReflesh = null;
    
    SpriteRenderer spriteRenderer = null;
    Player player = null;

    int collisioncount = 0;
    private void Awake()
    {
        if (transform.tag == "BomPeg")
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            player = FindObjectOfType<Player>();
        }
    }
    private void OnEnable()
    {
        if (transform.tag == "BomPeg")
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("Hit bomb 3");
        }
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
                spriteRenderer.sprite = Resources.Load<Sprite>("Hit bomb 0");
                if (collisioncount >= 2) //폭탄은 두번 충돌하면 사라짐.
                {
                    player.GetBomb(); //플레이어한테
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
