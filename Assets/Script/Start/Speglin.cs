using System.Collections;
using UnityEngine;

public class Speglin : MonoBehaviour
{
    Vector2 startpos = new Vector2(-25f, 1.5f);
    Vector2 endpos = new Vector2(-15.5f, 1.3f);
    Vector2 dir = new Vector2(1f, 0.1f);

    [SerializeField] float moveSpeed = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        this.transform.position = startpos;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        while (this.transform.position.x < -15.5f)
        {
            dir.y = Random.Range(-0.2f, 0.2f);
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = endpos;
    }

}
