using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb3 : MonoBehaviour
{
    Rigidbody2D rigidbody;
    CircleCollider2D CircleCollider;
    Vector2 ranpos;
    Vector2 dir;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider = GetComponent<CircleCollider2D>();
        rigidbody.gravityScale = 0f;
        CircleCollider.enabled = true; //enable component 
    }
    private void Start()
    {
        StartCoroutine("Orb");
    }
    IEnumerator Orb()
    {
        while (true)
        {
            ranpos = new Vector2(Random.Range(1.4f, 8f), Random.Range(4f, 4.5f));  //set random position
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, -1f)); // set random direction
            transform.position = ranpos;
            rigidbody.velocity = dir;
            rigidbody.gravityScale = 0.2f;

            //WaitUntil(Func<bool> predicate) 
            //delegate bool predicate - satisfy a condition(true)
            yield return new WaitUntil(() => transform.position.y <= -5f);

            rigidbody.velocity = Vector2.zero; //velocity initialization
            rigidbody.gravityScale = 0f;
        }
        
    }

}
