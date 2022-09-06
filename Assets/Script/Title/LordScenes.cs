using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LordScenes : MonoBehaviour
{
    private void Start()
    {
       
    }

    public void onClick()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation a1 = SceneManager.LoadSceneAsync("Main");
        

        yield return new WaitForSeconds(2f);
        
    }
}
