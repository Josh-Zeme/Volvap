using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoofLight : MonoBehaviour
{
    [SerializeField] private GameController _GameController;
    [SerializeField] private Bell _Bell;
    [SerializeField] private Color _OriginalColour;
    [SerializeField] private Color _BossColour;
    [SerializeField] private Color _FlickerColour;
    [SerializeField] private Rigidbody2D _Rigidbody2D;
    [SerializeField] private Light2D _Light2D;

    public bool IsFlickering = false;
    private bool _IsFlickered = false;
    private bool _IsBossFight = false;
    private List<float> _FlickerIntervals = new List<float>();
    private float _FlickerLength = 0f;
    private float _FlickerTimeRemaining = 0;
    private float _InitialIntensity = 0;
    
    public void Start()
    {
        _InitialIntensity = _Light2D.intensity;
        _OriginalColour = _Light2D.color;
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
                _Light2D.color = _IsBossFight ? _BossColour : _OriginalColour;
            } else if(_FlickerIntervals.Any() && _FlickerTimeRemaining > _FlickerIntervals.First())
            {
                _FlickerIntervals.RemoveAt(0);
                Flicker();
            }
        }
    }

    public void SetIntensity(float intensity)
    {
        _InitialIntensity = intensity;
        _Light2D.intensity = _IsFlickered ? 0 : _InitialIntensity;
    }

    public void TriggerBossFight()
    {
        _Light2D.color = _BossColour;
        _IsBossFight = true;
    }

    public void Reset()
    {
        _IsBossFight = false;
        _Light2D.color = _OriginalColour;
    }

    public void TriggerForce(Vector2 force)
    {
        _Rigidbody2D.AddForce(force);
    }

    public void TriggerFlicker(Color flickerColour, float flickerLength, List<float> flickerIntervals)
    {
        _FlickerColour = flickerColour;
        _Light2D.color = _FlickerColour;
        _FlickerIntervals = flickerIntervals;
        IsFlickering = true;
        _FlickerTimeRemaining = 0;
        _FlickerLength = flickerLength;
    }

    private void Flicker()
    {
        _IsFlickered = !_IsFlickered;
        if (_IsFlickered && _GameController.GameState == GameState.TutorialRound)
        {
            _Bell.MakeUnlit();
        }
        else if (_GameController.GameState == GameState.TutorialRound)
        {
            _Bell.MakeLit();
        }
        _Light2D.intensity = _IsFlickered ? 0 : _InitialIntensity;
    }
}
