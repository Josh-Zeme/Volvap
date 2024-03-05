using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int UnitId;
    public List<Card> Cards = new List<Card>();
    [SerializeField] private Pipe _Pipe;
    [SerializeField] private Bowl _Bowl;

    protected virtual void Start()
    {

    }

    public virtual void AddCard(CardData cardData)
    {
        if (Cards.Count == 0)
        {
            Debug.Log("There is no cards there to be found");
            return;
        }

        for(int i = 0; i < Cards.Count; i++)
        {
            var _card = Cards[i];
            if(_card.CardData == null)
            {
                _card.CardData = cardData;
                _card.Show();
            }
        }
    }

    public virtual void Discard()
    {

    }

    public void AddTreats(int count)
    {
        for (int _i = 0; _i < count; _i++)
        {
            _Bowl.AddTreat();
        }
    }

    public void TriggerSmoking()
    {
        if (_Pipe != null)
        {
            _Pipe.TriggerPipe();
        }
    }
}
