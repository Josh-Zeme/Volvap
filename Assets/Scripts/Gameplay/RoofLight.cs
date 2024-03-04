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

    private bool _IsFlickering = false;
    private bool _IsFlickered = false;
    private List<float> _FlickerIntervals = new List<float>();
    private float _FlickerLength = 0f;
    private float _FlickerTimeRemaining = 0;
    private float _InitialIntensity = 0;
    
    public void Start()
    {
        _InitialIntensity = _Light2D.intensity;
    }

    public void FixedUpdate()
    {
        if (_IsFlickering)
        {
            _FlickerTimeRemaining += Time.deltaTime;
            if(_FlickerTimeRemaining >= _FlickerLength)
            {
                _IsFlickering = false;
                _Light2D.intensity = _InitialIntensity;
                _IsFlickered = false;
                _FlickerIntervals.Clear();
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

    public void TriggerFlicker(float flickerLength, List<float> flickerIntervals)
    {
        _FlickerIntervals = flickerIntervals;
        _IsFlickering = true;
        _FlickerTimeRemaining = 0;
        _FlickerLength = flickerLength;
    }

    private void Flicker()
    {
        _Light2D.intensity = _IsFlickered ? 0 : _InitialIntensity;
        _IsFlickered = !_IsFlickered;
    }

    public void TriggerLightChange()
    {
        
    }
}
