using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public string loadingSceneName = "LoadingScene";

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ChangeSceneTo(string sceneName)
    {
        // Change to loading screen
        SceneManager.LoadScene(loadingSceneName);
        // Load the actual scene
        StartCoroutine(LoadSceneAsync(sceneName));

        //SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Delay to make sure the loading screen is done loading
        yield return new WaitForSeconds(0.5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until scene fully loads (fully loaded is 0.9 because unity is retarded)
        while (asyncLoad.progress < 0.9)
        {
            yield return null;
        }
    }
}
