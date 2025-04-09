using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : PopupMenu
{
    //UnityEvents are used to avoid unsubscribe actions on destroy.
    //if optimization is suddenly needed, they will be replaced with c# events
    [HideInInspector] public UnityEvent ResumeButtonPressed;
    [HideInInspector] public UnityEvent RestartButtonPressed;
    [HideInInspector] public UnityEvent MainMenuButtonPressed;
    


    [Inject]
    private void Construct()
    {
        gameObject.SetActive(false);
    }

    public void ResumeButton()
    {
        ResumeButtonPressed.Invoke();
    }

    public void OptionsButton()
    {

    }

    public void RestartButton()
    {
        RestartButtonPressed.Invoke();
    }

    public void MainMenuButton()
    {
        MainMenuButtonPressed.Invoke();
    }
    
    public void OnPauseStart()
    {
        gameObject.SetActive(true);
    }

    public void OnPauseEnd()
    {
        gameObject.SetActive(false);
    }
}
