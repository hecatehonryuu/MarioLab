using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScreen : MonoBehaviour
{
    public CanvasGroup c;

    void Start()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        for (float alpha = 0.0f; alpha <= 1.0f; alpha += 0.02f)
        {
            c.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // once done, go to next scene
        SceneManager.LoadSceneAsync("World-1-1", LoadSceneMode.Single);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync("MainScreen", LoadSceneMode.Single);
    }
}
