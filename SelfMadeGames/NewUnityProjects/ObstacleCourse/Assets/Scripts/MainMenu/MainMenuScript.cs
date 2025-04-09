using UnityEngine;
using Zenject;

public class MainMenuScript : MonoBehaviour
{
    //to Inject
    private SceneController _sceneController;

    [Inject]
    private void Construct(SceneController sceneController)
    {
        _sceneController = sceneController;
    }

    public void PlayButton()
    {
        _sceneController.LoadSceneByIndex(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
