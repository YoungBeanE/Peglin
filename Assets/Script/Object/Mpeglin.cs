using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mpeglin : MonoBehaviour
{
    Vector2 myPos = new Vector2(30f, 4f);
    Vector2 dir = new Vector2(0, -1);

    WaitForSeconds pause = new WaitForSeconds(0.1f);

    // myPos is called before the first frame update
    private void Start()
    {
        this.transform.position = myPos;
    }

    public void Move(int stagedir)
    {
        switch (stagedir)
        {
            case 0: //down
                dir.x = 0;
                break;
            case 1: //right
                dir.x = 1;
                break;
            case 2: //left
                dir.x = -1;
                break;
        }

        StartCoroutine(MoveStage());
    }

    IEnumerator MoveStage()
    {
        while (myPos.y - this.transform.position.y < 2f)
        {
            transform.Translate(dir * 15f * Time.deltaTime);
            yield return pause;
        }
        myPos.x += 2 * dir.x;
        myPos.y -= 2;
        this.transform.position = myPos;

        yield return pause;
        yield return pause;
        GameMgr.Inst.StageStart();
    }
}
