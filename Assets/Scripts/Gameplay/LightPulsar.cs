using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulsar : MonoBehaviour
{
    private Light2D _Light2D;
    private float _TimePulsarTrigger = 0.1f;
    private float _TimePulsarTriggerRemaining = 0;

    private float _BlueUpperRange = 250f;
    private float _BlueLowerRange = 50f;

    private float _RedUpperRange = 500f;
    private float _RedLowerRange = 200f;

    public void Start()
    {
        _Light2D = GetComponent<Light2D>();
    }

    public void FixedUpdate()
    {
        _TimePulsarTriggerRemaining += Time.deltaTime;

        if (_TimePulsarTriggerRemaining >= _TimePulsarTrigger)
        {
            _TimePulsarTriggerRemaining = 0f;
            Pulse();
        }
    }

    private void Pulse()
    {
        var _intensity = 0f;
        if (_Light2D.color == Color.blue)
        {
            _intensity = Random.Range(_BlueLowerRange, _BlueUpperRange);
        }

        if (_Light2D.color == Color.red)
        {
            _intensity = Random.Range(_RedLowerRange, _RedUpperRange);
        }
        _Light2D.intensity = _intensity;
    }
}
