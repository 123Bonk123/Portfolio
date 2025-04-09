using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class FinishCanvas : PopupMenu
{
    //UnityEvents are used to avoid unsubscribe actions on destroy.
    //if optimization is suddenly needed, they will be replaced with c# events
    [HideInInspector] public UnityEvent RestartButtonPressed;
    [HideInInspector] public UnityEvent MainMenuButtonPressed;

    [Inject]
    private void Construct()
    {
        gameObject.SetActive(false);
    }

    public void FinishActionsUI()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        RestartButtonPressed.Invoke();
    }

    public void MainMenuButton()
    {
        MainMenuButtonPressed.Invoke();
    }
}
