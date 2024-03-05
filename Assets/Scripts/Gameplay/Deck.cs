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
        for (int i = 0; i < GameSettings.CardsPerDeck; i++)
        {
            var _card = new CardData();
            _card.Randomise();
            _Cards.Add(_card);
        }
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

    public void Shuffle()
    {

    }

    public void AddCard()
    {

    }

    public void GetTopCard()
    {

    }

    public void UpdateDeckAmount()
    {
       
        //_Animator.SetTrigger("Fill Cards");
        if (_Cards.Count < GameSettings.CardsPerDeck / 2 && _PreviousCount >= GameSettings.CardsPerDeck / 2)
        {
            _Animator.SetTrigger("RemoveCards");
        }

        if (_Cards.Count < GameSettings.CardsPerDeck / 3 && _PreviousCount >= GameSettings.CardsPerDeck / 3)
        {
            _Animator.SetTrigger("RemoveCards");
        }

        if (_Cards.Count < (GameSettings.CardsPerDeck / 4) && _PreviousCount >= GameSettings.CardsPerDeck / 4)
        {
            _Animator.SetTrigger("RemoveCards");
        }
        _PreviousCount = _Cards.Count;
    }
}
