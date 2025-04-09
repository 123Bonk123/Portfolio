using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public IEnumerator LoadRoutine(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);
        waitLoading.allowSceneActivation = false;

        // next block of code aims to start game (moving platforms, timer, ...) only when the player is ready (will be changed)
        while (waitLoading.progress < 0.9f)
            yield return null;
        
        Time.timeScale = 0f; 
        waitLoading.allowSceneActivation = true;

        yield return new WaitUntil(() => waitLoading.isDone);
        //yield return new WaitForSeconds(1);

    }
}
