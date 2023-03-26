using System.Collections;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    Mpeglin mpeglin;
    [SerializeField] float moveSpeed = 35f;

    Vector3 startPos = new Vector3(-20f, 1f, -10f);
    Vector3 mapPos = new Vector3(30f, -5f, -10f);
    Vector3 mainPos = new Vector3(0f, 0f, -10f);

    int stage = 0;
    int[] stageY = { 2, 0, -2, -4, -6 };

    WaitForSeconds pause = new WaitForSeconds(0.1f);
    WaitForSeconds half1 = new WaitForSeconds(0.5f);

    private void Awake()
    {
        mpeglin = FindObjectOfType<Mpeglin>();
        this.transform.position = startPos;
    }
    
    public void MoveMap(int dir) //0 - down / 1 - right / 2 - left
    {
        this.transform.position = mapPos;
        if(stage == 0) StartCoroutine(StartMap());
        else StartCoroutine("MoveStage", dir);
    }

    IEnumerator StartMap()
    {
        mapPos.y = stageY[stage];
        
        while (this.transform.position.y < stageY[stage])
        {
            this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return pause;
        }
        this.transform.position = mapPos;

        yield return half1;
        stage++;
        mpeglin.Move(0);
    }

    IEnumerator MoveStage(int dir)
    {
        mapPos.y = stageY[stage];
        stage++;
        mpeglin.Move(dir);

        while (this.transform.position.y > stageY[stage])
        {
            this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return pause;
            if (mpeglin.transform.position.y <= stageY[stage] + 0.4f) yield break;
        }
        this.transform.position = mapPos;
    }

    public void MoveMain()
    {
        transform.position = mainPos;
    }

}
