using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    //to Inject
    private CoroutinePerformer _coroutinePerformer;
    private SceneLoader _sceneLoader;
    private LoadingSqreen _loadingSqreen;


    // this field will be Dictionary<string, SceneConfig> in future (when SceneConfig will be implemented)
    private List<int> _scenesIndexList;
    
    public int CurrentSceneIndex {get; private set; }
    private string _loadingEndText;

    //Inject
    public SceneController(CoroutinePerformer coroutinePerformer, SceneLoader sceneLoader, LoadingSqreen loadingSqreen)
    {
        _coroutinePerformer = coroutinePerformer;
        _sceneLoader = sceneLoader;
        _loadingSqreen = loadingSqreen;

        _scenesIndexList = new List<int>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            _scenesIndexList.Add(i);
        }
        
        _loadingEndText = "Game is ready!";
    }

    public void LoadSceneByIndex(int index)
    {
        _coroutinePerformer.StartCoroutine(LoadSceneByIndexProcess(_scenesIndexList[index]));
    }

    private IEnumerator LoadSceneByIndexProcess(int index)
    {
        _loadingSqreen.ChangeLoadingText("Loading...");
        _loadingSqreen.Show();

        yield return _sceneLoader.LoadRoutine(index);
        
        if(index != 0)
        {
            _loadingSqreen.ChangeLoadingText(_loadingEndText);
            yield return new WaitUserInteraction();
        }
            
        Time.timeScale = 1f;
        _loadingSqreen.Hide();

        CurrentSceneIndex = index;
    }


}
