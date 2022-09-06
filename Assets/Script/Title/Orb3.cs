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
        CircleCollider.enabled = true;
    }
    private void Start()
    {
        StartCoroutine("Orb");
    }
    IEnumerator Orb()
    {
        while (true)
        {
            ranpos = new Vector2(Random.Range(1.4f, 8f), Random.Range(4f, 4.5f));
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, -1f));
            transform.position = ranpos;
            rigidbody.velocity = dir;
            rigidbody.gravityScale = 0.2f;
            yield return new WaitUntil(() => transform.position.y <= -5f);
            rigidbody.velocity = Vector2.zero;
            rigidbody.gravityScale = 0f;
        }
        
    }

}
