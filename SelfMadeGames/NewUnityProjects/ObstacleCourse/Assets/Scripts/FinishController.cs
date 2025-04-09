using UnityEngine;

public class FinishController
{
    //to Inject
    private SceneController _sceneController;
    private FinishCanvas _finishCanvas;
    private CoroutinePerformer _coroutinePerformer;
    private Canvas _hudCanvas;
    private IPlayerFinishHandler _playerFinishHandler;

    public bool IsFinished { get; private set; }

    // Inject
    private FinishController(Canvas hudCanvas, SceneController sceneController, FinishCanvas finishCanvas, CoroutinePerformer coroutinePerformer, IPlayerFinishHandler playerFinishHandler)
    {
        _sceneController = sceneController;
        _finishCanvas = finishCanvas;
        _coroutinePerformer = coroutinePerformer;
        _hudCanvas = hudCanvas;
        _playerFinishHandler = playerFinishHandler;

        IsFinished = false;

        _finishCanvas.RestartButtonPressed.AddListener(Restart);
        _finishCanvas.MainMenuButtonPressed.AddListener(LoadMainMenu);

        _playerFinishHandler.PlayerFinished.AddListener(FinishActions);
    }

    public void FinishActions()
    {
        IsFinished = true;
        _hudCanvas.gameObject.SetActive(false);
        _finishCanvas.FinishActionsUI();
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
