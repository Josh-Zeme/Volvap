using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private Card BaseCard;
    private List<CardData> _Cards = new List<CardData>();
    private int _PreviousCount;

    private void Start()
    {
        RefillDeck();
        Debug.Log($"There are {_Cards.Count} cards");
        _PreviousCount = _Cards.Count;
    }

    public CardData TakeCard()
    {
        var _card = _Cards.FirstOrDefault();
        if(_card == null)
        {
            Debug.LogError("There was no cards in the deck. This is going to fail hard!");
        } else
        {
            _Cards.Remove(_card);
        }
        UpdateDeckAmount();
        return _card;
    }

    public void RefillDeck()
    {
        while(_Cards.Count < GameSettings.CardsPerDeck)
        {
            var _card = new CardData();
            _card.Randomise();
            _Cards.Add(_card);
        }
        _PreviousCount = _Cards.Count;
    }

    public void UpdateDeckAmount()
    {
        var _threeQuarter = (int)(GameSettings.CardsPerDeck * 0.75f);
        var _half = (int)(GameSettings.CardsPerDeck * 0.5f);
        var _quarter = (int)(GameSettings.CardsPerDeck * 0.25f);
        //_Animator.SetTrigger("Fill Cards");
        if (_Cards.Count < _threeQuarter && _PreviousCount >= _threeQuarter)
        {
            _Animator.SetTrigger("RemoveCards");
        }

        if (_Cards.Count < _half && _PreviousCount >= _half)
        {
            _Animator.SetTrigger("RemoveCards");
        }

        if (_Cards.Count < _quarter && _PreviousCount >= _quarter)
        {
            _Animator.SetTrigger("RemoveCards");
        }
        _PreviousCount = _Cards.Count;
    }
}
