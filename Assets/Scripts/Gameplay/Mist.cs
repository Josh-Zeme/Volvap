using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mist : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _MistOne;
    [SerializeField] private SpriteRenderer _MistTwo;
    [SerializeField] private SpriteRenderer _MistThree;
    [SerializeField] private SpriteRenderer _MistFour;
    private float _TimeMistOneStart = 0.1f;
    private float _TimeMistOneRemaining = 0;

    private float _TimeMistTwoStart = 0.2f;
    private float _TimeMistTwoRemaining = 0;

    private float _TimeMistThreeStart = 0.5f;
    private float _TimeMistThreeRemaining = 0;

    private float _TimeMistFourStart = 0.3f;
    private float _TimeMistFourRemaining = 0;

    private float _MistOneUpperRange = 0.5f;
    private float _MistOneLowerRange = 0.3f;

    private float _MistTwoUpperRange = 0.6f;
    private float _MistTwoLowerRange = 0.2f;

    private float _MistThreeUpperRange = 0.7f;
    private float _MistThreeLowerRange = 0.1f;

    private float _MistFourUpperRange = 0.2f;
    private float _MistFourLowerRange = 0.1f;


    public void FixedUpdate()
    {
        _TimeMistOneRemaining += Time.deltaTime;
        _TimeMistTwoRemaining += Time.deltaTime;
        _TimeMistThreeRemaining += Time.deltaTime;
        _TimeMistFourRemaining += Time.deltaTime;

        if (_TimeMistOneRemaining >= _TimeMistOneStart)
        {
            _TimeMistOneRemaining = 0f;
            TriggerMist(_MistOne, _MistOneLowerRange, _MistOneUpperRange);
        }

        if (_TimeMistTwoRemaining >= _TimeMistTwoStart)
        {
            _TimeMistTwoRemaining = 0f;
            TriggerMist(_MistTwo, _MistTwoLowerRange, _MistTwoUpperRange);
        }

        if (_TimeMistThreeRemaining >= _TimeMistThreeStart)
        {
            _TimeMistThreeRemaining = 0f;
            TriggerMist(_MistThree, _MistThreeLowerRange, _MistThreeUpperRange);
        }

        if (_TimeMistFourRemaining >= _TimeMistFourStart)
        {
            _TimeMistFourRemaining = 0f;
            TriggerMist(_MistFour, _MistFourLowerRange, _MistFourUpperRange);
        }

    }

    private void TriggerMist(SpriteRenderer spriteRenderer, float lowerRange, float upperRange)
    {
        var _alpha = Random.Range(lowerRange, upperRange);
        var _tempColour = spriteRenderer.color;
        _tempColour.a = _alpha;
        spriteRenderer.color = _tempColour;
    }
}
