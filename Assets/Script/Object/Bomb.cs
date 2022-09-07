using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rigidbody;
    CircleCollider2D CircleCollider;
    Vector2 dir;
    [SerializeField] GameObject bombEff;
    int bombpower = 50;
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
            dir = new Vector2(Random.Range(0.1f, 4f), Random.Range(-0.2f, -1f)); // set random direction
            rigidbody.velocity = dir;
            rigidbody.gravityScale = 0.1f;
            yield return new WaitForSeconds(0.1f);
            GameObject eff = Instantiate(bombEff, transform.position, Quaternion.identity);
            GameMgr.Inst.Monsterdamage(bombpower);
            Destroy(this);
        }

    }
}
