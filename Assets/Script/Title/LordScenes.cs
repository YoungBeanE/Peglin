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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main"); //비동기 로딩 - coroutine
        while(asyncOperation.isDone == false) //Loading...
        {
            Debug.Log("Progress : " + asyncOperation.progress);
            yield return null;
        }
    }
}
