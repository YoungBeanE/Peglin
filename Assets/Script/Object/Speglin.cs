using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speglin : MonoBehaviour
{
    Vector2 startpos;
    Vector2 endpos;
    Vector2 move;
    
    // Start is called before the first frame update
    void Start()
    {
        startpos = new Vector2(-25f, 1.5f);
        endpos = new Vector2(-15.5f, 1.3f);
        move = new Vector2(1f, 0.1f);
        transform.position = startpos;
    }

    // Update is called once per frame
    void Update()
    {
        move.y = Random.Range(-0.2f, 0.2f);
        transform.Translate(move * 3f * Time.deltaTime);
        if(transform.position.x >= -15.5f)
        {
            transform.position = endpos;
        }
    }
}
