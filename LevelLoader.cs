using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            StartCoroutine(PlaySplashScreen());
        }
    }

    IEnumerator PlaySplashScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadLevel(float timeDelay)
    {
        StartCoroutine(ReloadLevelCoroutine(timeDelay));
    }

    IEnumerator ReloadLevelCoroutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene(currentSceneIndex);
    }

}
