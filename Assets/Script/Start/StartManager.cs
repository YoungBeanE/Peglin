using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void onClick_Start()
    {
        StartCoroutine(Scene());
    }

    IEnumerator Scene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        while (asyncOperation.isDone == false)
        {
            //Debug.Log("Progress : " + asyncOperation.progress);
            yield return null;
        }
    }
}
