using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

    public int SelectedCardCount()
    {
        var _count = Cards.Count(x => x.IsSelected);
        return _count;
    }

    public virtual void AddCard(CardData cardData, ref GameController gameController)
    {
        if (Cards.Count == 0)
        {
            Debug.Log("There is no cards there to be found");
            return;
        }

        var _card = Cards.Where(x => x.CardData?.Owner == null).FirstOrDefault();
        if(_card != null)
        {
            cardData.Owner = this;
            _card.Generate(cardData, ref gameController);
            _card.Show();
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

    public List<CardData> SelectedCards()
    {
        var _cardData = Cards.Where(x => x.IsSelected).Select(n => new CardData()
        {
            Colour = n.CardData.Colour,
            Shield = n.CardData.Shield,
            Sword = n.CardData.Sword,
            Magic = n.CardData.Magic,
            BoonType = n.CardData.BoonType
        }
        );
        return _cardData.ToList();
    }

    public void RandomiseSelected()
    {
        var _randomCardOne = Random.Range(0, Cards.Count);
        int _randomCardTwo = 0;
        int _randomCardThree = 0;

        do
        {
            _randomCardTwo = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardTwo);

        do
        {
            _randomCardThree = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardThree || _randomCardTwo == _randomCardThree);

        Cards[_randomCardOne].ExchangeSelectCard();
        Cards[_randomCardTwo].ExchangeSelectCard();
        Cards[_randomCardThree].ExchangeSelectCard();
    }

    public void RemoveSelectedCards()
    {
        var _cards = Cards.Where(x => x.IsSelected).ToList();
        for (int _i = 0; _i < _cards.Count; _i++)
        {
            var _card = _cards[_i];
            _card.Hide();
            _card.Reset();
        }
        
    }
}
