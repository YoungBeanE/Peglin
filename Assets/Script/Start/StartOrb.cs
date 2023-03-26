using System.Collections;
using UnityEngine;

public class StartOrb : MonoBehaviour
{
    Rigidbody2D orbRigidbody;
    CircleCollider2D orbCollider;
    Vector2 orbPos;
    Vector2 dir;

    WaitUntil fall = null;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.TryGetComponent<Rigidbody2D>(out orbRigidbody);
        this.transform.TryGetComponent<CircleCollider2D>(out orbCollider);
        orbRigidbody.gravityScale = 0f;
        orbCollider.enabled = true; //enable component 

        fall = new WaitUntil(() => this.transform.position.y <= -5f);
    }
    private void Start()
    {
        StartCoroutine("Orb");
    }

    IEnumerator Orb()
    {
        while (true)
        {
            orbPos = new Vector2(Random.Range(1.4f, 8f), Random.Range(4f, 4.5f));  //set random position
            dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.4f, -1f)); // set random direction
            dir.Normalize();

            transform.position = orbPos;
            orbRigidbody.velocity = dir;
            orbRigidbody.gravityScale = 0.2f;

            yield return fall;

            orbRigidbody.velocity = Vector2.zero; //velocity initialization
            orbRigidbody.gravityScale = 0f;
        }

    }

}

