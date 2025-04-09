using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private string _attachedEntityTag = "Player";
    [SerializeField] private float _fadeDuration = 2f;

    private Material _platformMaterial;
    private Color _platformColor;
    private bool _isActive;

    private bool _isFading;
    private float _fadeTimer = 0f;

    private void Start()
    {

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            _platformMaterial = renderer.material;
            _platformColor = renderer.material.color;
            
        }
        else
        {
            Debug.LogError("There is no Renderer on the object.");
        }

        _isFading = false;
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(_attachedEntityTag) && !_isFading && _isActive)
        {
            StartCoroutine(FadeAndDisappear());
        }
    }

    private IEnumerator FadeAndDisappear()
    {
        _isFading = true;
        
        while (_fadeTimer < _fadeDuration)
        {
            _fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, _fadeTimer / _fadeDuration);

            if (_platformMaterial != null)
            {
                _platformColor.a = alpha;
                _platformMaterial.color = _platformColor;
            }

            yield return null;
        }

        gameObject.SetActive(false);

        _isActive = false;
        _isFading = false;
        _fadeTimer = 0f;
    }

    public void Reset()
    {
        Color color = _platformMaterial.color;
        color.a = 1f;
        _platformMaterial.color = color;
        gameObject.SetActive(true);
        _isActive = true;
    }
}
