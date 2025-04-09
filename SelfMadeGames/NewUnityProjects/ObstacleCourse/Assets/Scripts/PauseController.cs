using UnityEngine;

public class PauseController: IPauseUnpause
{
    //to Inject
    private SceneController _sceneController;
    private PauseMenu _pauseMenu;
    private CoroutinePerformer _coroutinePerformer;


    public bool IsPaused { get; private set; }

    // Inject
    private PauseController(SceneController sceneController, PauseMenu pauseMenu, CoroutinePerformer coroutinePerformer)
    {
        _sceneController = sceneController;
        _pauseMenu = pauseMenu;
        _coroutinePerformer = coroutinePerformer;

        IsPaused = false;

        _pauseMenu.ResumeButtonPressed.AddListener(Unpause);
        _pauseMenu.RestartButtonPressed.AddListener(Restart);
        _pauseMenu.MainMenuButtonPressed.AddListener(LoadMainMenu);
    }

    public void Pause()
    {
        IsPaused = true;
        _pauseMenu.OnPauseStart();
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        IsPaused = false;
        _pauseMenu.OnPauseEnd();
        Time.timeScale = 1f;
    }

    private void Restart()
    {
        _sceneController.LoadSceneByIndex(_sceneController.CurrentSceneIndex);
    }

    private void LoadMainMenu()
    {
        _sceneController.LoadSceneByIndex(0);
    }

}
