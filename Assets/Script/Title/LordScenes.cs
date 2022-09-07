using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LordScenes : MonoBehaviour
{
    public void onClick()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        
        AsyncOperation a1 = SceneManager.LoadSceneAsync("Main");
        

        yield return new WaitForSeconds(2f);
        
    }
}
