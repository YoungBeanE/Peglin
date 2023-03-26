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
    Sprite nomalBomb = null;
    Sprite readyBomb = null;

    int collisioncount = 0;

    private void OnEnable()
    {
        if (transform.tag == "BomPeg")
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            if (player == null) player = FindObjectOfType<Player>();
            if (nomalBomb == null) nomalBomb = Resources.Load<Sprite>("Hit bomb 3");
            if (readyBomb == null) readyBomb = Resources.Load<Sprite>("Hit bomb 0");
            spriteRenderer.sprite = nomalBomb;
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
                spriteRenderer.sprite = readyBomb;
                if (collisioncount >= 2) //ÆøÅºÀº µÎ¹ø Ãæµ¹ÇÏ¸é »ç¶óÁü.
                {
                    player.GetBomb();
                    if (callbackDestroy != null) callbackDestroy(this);
                }
                break;
            case "RefPeg":
                if (callbackReflesh != null) callbackReflesh();
                if (callbackDestroy != null) callbackDestroy(this);
                break;
        }
        
    }
    
}
