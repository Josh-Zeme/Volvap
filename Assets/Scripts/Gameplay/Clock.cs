using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Drool _Drool;
    [SerializeField] private Aberration _Aberration;

    protected virtual void Start()
    {

    }

    public virtual void AddCard(Card card)
    {

    }

    public virtual void Discard()
    {

    }

    public void TriggerAberration(float flickerLength, List<float> flickerIntervals)
    {
        if (_Aberration != null)
        {
            _Aberration.TriggerFlicker(flickerLength, flickerIntervals);
        }
    }

    public void TriggerDrool()
    {
        if (_Drool != null)
        {
            _Drool.TriggerDrool();
        }
    }
}
