using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int UnitId;
    public List<Card> Cards = new List<Card>();

    protected virtual void Start()
    {

    }

    public virtual void AddCard(Card card)
    {

    }

    public virtual void Discard()
    {

    }
}
