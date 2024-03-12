using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoofLight : MonoBehaviour
{
    [SerializeField] private Color _OriginalColor;
    [SerializeField] private Color _FlickerColor;
    [SerializeField] private Rigidbody2D _Rigidbody2D;
    [SerializeField] private Light2D _Light2D;

    public bool IsFlickering = false;
    private bool _IsFlickered = false;
    private List<float> _FlickerIntervals = new List<float>();
    private float _FlickerLength = 0f;
    private float _FlickerTimeRemaining = 0;
    private float _InitialIntensity = 0;
    
    public void Start()
    {
        _InitialIntensity = _Light2D.intensity;
        _OriginalColor = _Light2D.color;
    }

    public void FixedUpdate()
    {
        if (IsFlickering)
        {
            _FlickerTimeRemaining += Time.deltaTime;
            if(_FlickerTimeRemaining >= _FlickerLength)
            {
                IsFlickering = false;
                _Light2D.intensity = _InitialIntensity;
                _IsFlickered = false;
                _FlickerIntervals.Clear();
                _Light2D.color = _OriginalColor;
            } else if(_FlickerIntervals.Any() && _FlickerTimeRemaining > _FlickerIntervals.First())
            {
                _FlickerIntervals.RemoveAt(0);
                Flicker();
            }
        }
    }

    public void TriggerForce(Vector2 force)
    {
        _Rigidbody2D.AddForce(force);
    }

    public void TriggerFlicker(Color flickerColour, float flickerLength, List<float> flickerIntervals)
    {
        _FlickerColor = flickerColour;
        _Light2D.color = _FlickerColor;
        _FlickerIntervals = flickerIntervals;
        IsFlickering = true;
        _FlickerTimeRemaining = 0;
        _FlickerLength = flickerLength;
    }

    private void Flicker()
    {
        _Light2D.intensity = _IsFlickered ? 0 : _InitialIntensity;
        _IsFlickered = !_IsFlickered;
    }
}
