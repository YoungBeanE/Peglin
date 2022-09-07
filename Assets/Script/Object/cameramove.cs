using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    Mpeglin mpeglin;
    private void Awake()
    {
        mpeglin = FindObjectOfType<Mpeglin>();
        transform.position = new Vector3(-20f, 1f, -10f); // start
    }
    
    public void gamstart()
    {
        transform.position = new Vector3(-20f, 1f, -10f); // start
    }
    public void mapmove(int dir)
    {
        transform.position = new Vector3(30f, -5f, -10f); // map
        StartCoroutine(map(dir));
    }
    IEnumerator map(int dir)
    {
        while (true)
        {
            transform.Translate(Vector3.up * 35f * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
            if (transform.position.y > 2f)
            {
                transform.position = new Vector3(30f, 2f, -10f); // map
                yield return new WaitForSeconds(0.5f);
                mpeglin.Move(dir);
                yield break;
            }
        }
        
    }
    public void mainmove()
    {
        transform.position = new Vector3(0f, 0f, -10f); // main
    }
    
    
}
