using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mpeglin : MonoBehaviour
{
    Vector2 start = new Vector2(30f, 3.5f);
    Vector2 stage1 = new Vector2(30f, 2f); //main
    Vector2 stage2 = new Vector2(32f, 0f); //right
    Vector2 stage3 = new Vector2(28f, 0f); //left
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start;
    }

    public void Move(int dir)
    {
        switch (dir)
        {
            case 0: //down
                StartCoroutine("down");
                break;
            case 1: //right
                StartCoroutine("right");
                break;
            case 2: //left
                StartCoroutine("left");
                break;
        }
    }
    IEnumerator down()
    {
        while (true)
        {
            transform.Translate(Vector2.down * 15f * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
            if(transform.position.y <= 2.3f)
            {
                yield return new WaitForSeconds(0.2f);
                GameMgr.Inst.main();
                yield break;
            }
        }
        
    }
    IEnumerator right()
    {
        Vector2 movement = (stage2 - stage1).normalized;
        while (true)
        {
            transform.Translate(movement * 15f * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
            if (transform.position.y <= 0.1f)
            {
                yield return new WaitForSeconds(0.2f);
                GameMgr.Inst.main();
                yield break;
            }
        }

    }
    IEnumerator left()
    {
        while (true)
        {
            Vector2 movement = (stage3 - stage1).normalized;
            while (true)
            {
                transform.Translate(movement * 15f * Time.deltaTime);
                yield return new WaitForSeconds(0.1f);
                if (transform.position.y <= 0.1f)
                {
                    yield return new WaitForSeconds(0.2f);
                    GameMgr.Inst.main();
                    yield break;
                }
            }
        }

    }
}
