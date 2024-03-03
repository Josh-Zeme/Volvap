using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Animator _Animator;
    [SerializeField] private Card BaseCard;
    private List<Card> _Cards = new List<Card>();

    private void Start()
    {
        for (int i = 0; i < GameSettings.CardsPerDeck; i++)
        {
            var _card = Instantiate(BaseCard, transform);
            _card.Randomise();
            _Cards.Add(_card);
        }
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
        //Fill Deck
        _Animator.SetTrigger("RemoveCards");
    }
}
