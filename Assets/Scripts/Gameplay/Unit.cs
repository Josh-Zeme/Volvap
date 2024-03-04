using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int UnitId;
    public List<Card> Cards = new List<Card>();
    [SerializeField] private Pipe _Pipe;


    protected virtual void Start()
    {

    }

    public virtual void AddCard(Card card)
    {

    }

    public virtual void Discard()
    {

    }

    public void TriggerSmoking()
    {
        if (_Pipe != null)
        {
            _Pipe.TriggerPipe();
        }
    }
}
