using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoadingSqreen : MonoBehaviour
{
    [SerializeField] private Image _loadingAnimationImage;
    [SerializeField] private TMP_Text _loadingText;

    private Tween _loadingAnimation;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        Hide();
        _loadingAnimation = _loadingAnimationImage.transform.DORotate(new Vector3(0, 0, 360), 2, RotateMode.LocalAxisAdd)
        .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _loadingAnimation.Play();
    }

    public void Hide()
    {
        _loadingAnimation.Pause();
        gameObject.SetActive(false);
        
    }
    public void ChangeLoadingText(string text) => _loadingText.text = text;

    private void OnDestroy()
    {
        _loadingAnimation.Kill();
    }
}
