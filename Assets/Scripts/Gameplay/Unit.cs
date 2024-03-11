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
    [SerializeField] private TurnValuesDisplay _TurnValuesDisplay;
    [SerializeField] private SwordAttack _SwordAttack;
    public List<CardData> AttackCards = new List<CardData>();
    public int Sword;
    public int Shield;
    public int Magic;
    public int Treats;

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

    public virtual void DiscardAfterAttack()
    {
        for(int _i = 0; _i < AttackCards.Count; _i++)
        {
            var _attackCard = AttackCards[_i];
            var _card = Cards.Where(x => x.CardData != null
            && x.CardData.Sword == _attackCard.Sword
            && x.CardData.Shield == _attackCard.Shield
            && x.CardData.Magic == _attackCard.Magic
            && x.CardData.Colour == _attackCard.Colour
            && x.CardData.BoonType == _attackCard.BoonType

            ).FirstOrDefault();

            if(_card != null)
                _card.CardData.Clear();
        }
        AttackCards.Clear();
    }

    public void AddTreats(int count)
    {
        Treats += count;
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

    public void AddAttackCard(CardData cardData)
    {
        var _cardData = new CardData()
        {
            Colour = cardData.Colour,
            Shield = cardData.Shield,
            Sword = cardData.Sword,
            Magic = cardData.Magic,
            BoonType = cardData.BoonType
        };

        AttackCards.Add(_cardData);
    }

    public void RandomiseAttackSelect()
    {
        // Same as other method, needs less random logic later (harder enemies)
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

        var _cardDataOne = new CardData()
        {
            Colour = Cards[_randomCardOne].CardData.Colour,
            Shield = Cards[_randomCardOne].CardData.Shield,
            Sword = Cards[_randomCardOne].CardData.Sword,
            Magic = Cards[_randomCardOne].CardData.Magic,
            BoonType = Cards[_randomCardOne].CardData.BoonType
        };

        var _cardDataTwo = new CardData()
        {
            Colour = Cards[_randomCardTwo].CardData.Colour,
            Shield = Cards[_randomCardTwo].CardData.Shield,
            Sword = Cards[_randomCardTwo].CardData.Sword,
            Magic = Cards[_randomCardTwo].CardData.Magic,
            BoonType = Cards[_randomCardTwo].CardData.BoonType
        };

        var _cardDataThree = new CardData()
        {
            Colour = Cards[_randomCardThree].CardData.Colour,
            Shield = Cards[_randomCardThree].CardData.Shield,
            Sword = Cards[_randomCardThree].CardData.Sword,
            Magic = Cards[_randomCardThree].CardData.Magic,
            BoonType = Cards[_randomCardThree].CardData.BoonType
        };

        AttackCards.Add(_cardDataOne);
        AttackCards.Add(_cardDataTwo);
        AttackCards.Add(_cardDataThree);

        ClearCard(Cards[_randomCardOne]);
        ClearCard(Cards[_randomCardTwo]);
        ClearCard(Cards[_randomCardThree]);

    }

    public void ForceHideDisplay()
    {
        _TurnValuesDisplay.ForcedHide();
    }

    public void CalculateValues()
    {
        AttackCards[0].CalculateMultiplier(AttackCards[1].Colour, AttackCards[2].Colour);
        AttackCards[1].CalculateMultiplier(AttackCards[0].Colour, AttackCards[2].Colour);
        AttackCards[2].CalculateMultiplier(AttackCards[1].Colour, AttackCards[0].Colour);

        Sword = AttackCards[0].Sword * AttackCards[0].Multiplier;
        Shield = AttackCards[1].Shield * AttackCards[1].Multiplier;
        Magic = AttackCards[2].Magic * AttackCards[2].Multiplier;

        _TurnValuesDisplay.UpdateSword(Sword);
        _TurnValuesDisplay.UpdateShield(Shield);
        _TurnValuesDisplay.UpdateMagic(Magic);
    }

    public void SwordAttack(int damage)
    {
        Shield -= damage;
        _SwordAttack.TriggerAttack();
        if(Shield < 0)
        {
            var _damageTaken = Mathf.Abs(Shield);
            Shield = 0;
            AddTreats(_damageTaken);
        }
        _TurnValuesDisplay.UpdateShield(Shield);
    }


    public void MagicAttack(int damage)
    {
        Debug.Log("Do the magic animation");
        //_SwordAttack.TriggerAttack();
        AddTreats(damage);
    }

    public void ClearTreats()
    {
        Treats = 0;
        _Bowl.DestroyTreats();
    }

    public void ClearCard(Card card)
    {
        card.Hide();
        card.Reset();
    }

    public void RemoveSelectedCards()
    {
        var _cards = Cards.Where(x => x.IsSelected).ToList();
        for (int _i = 0; _i < _cards.Count; _i++)
        {
            var _card = _cards[_i];
            ClearCard(_card);
        }
        
    }
}
