using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Aberration : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private SpriteRenderer _ArmsRenderer;
    [SerializeField] private SpriteRenderer _MouthsRenderer;
    [SerializeField] private SpriteRenderer _RedRunes;
    [SerializeField] private Color _OriginalColor;
    [SerializeField] private Color _FlickerColor;
    [SerializeField] private Light2D _EyeLight2D;
    [SerializeField] private Light2D _RuneLight2D;

    private bool _IsFlickering = false;
    private bool _IsFlickered = false;
    private List<float> _FlickerIntervals = new List<float>();
    private float _FlickerLength = 0f;
    private float _FlickerTimeRemaining = 0;
    private float _InitialIntensity = 0;
    private float _FlickerIntensity = 0.1f;

    public void Start()
    {
        _RuneLight2D.color = Color.red;
    }

    public void FixedUpdate()
    {
        if (_IsFlickering)
        {
            _FlickerTimeRemaining += Time.deltaTime;
            if(_FlickerTimeRemaining >= _FlickerLength)
            {
                _IsFlickering = false;
                if(_RedRunes.gameObject.activeInHierarchy)
                    _RuneLight2D.intensity = _InitialIntensity;
                _EyeLight2D.intensity = _InitialIntensity;
                _IsFlickered = false;
                _FlickerIntervals.Clear();
                _SpriteRenderer.enabled = false;
            } else if(_FlickerIntervals.Any() && _FlickerTimeRemaining > _FlickerIntervals.First())
            {
                _FlickerIntervals.RemoveAt(0);
                Flicker();
            }
        }
    }

    public void TriggerFlicker(float flickerLength, List<float> flickerIntervals)
    {
        if (_RedRunes.gameObject.activeInHierarchy)
        {
            _RuneLight2D.intensity = _FlickerIntensity;
        }
        _FlickerIntervals = flickerIntervals;
        _IsFlickering = true;
        _SpriteRenderer.enabled = false;
        _FlickerTimeRemaining = 0;
        _FlickerLength = flickerLength;
    }

    private void Flicker()
    {
        _EyeLight2D.intensity = _IsFlickered ? _FlickerIntensity : _InitialIntensity;
        _SpriteRenderer.enabled = _IsFlickered;
        _IsFlickered = !_IsFlickered;
    }

    public void TriggerArms()
    {
        _ArmsRenderer.gameObject.SetActive(true);
    }

    public void TriggerMouths()
    {
        _MouthsRenderer.gameObject.SetActive(true);
    }

    public void TriggerRunes()
    {
        _RedRunes.gameObject.SetActive(true);
    }

    public void Reset()
    {
        _RedRunes.gameObject.SetActive(false);
        _ArmsRenderer.gameObject.SetActive(false);
        _MouthsRenderer.gameObject.SetActive(false);
    }
}
