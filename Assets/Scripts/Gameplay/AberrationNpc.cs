using System.Collections.Generic;
using UnityEngine;

public class AberrationNpc : Unit
{
    [SerializeField] SpriteRenderer _DroolA;
    [SerializeField] SpriteRenderer _DroolB;
    [SerializeField] SpriteRenderer _DroolC;

    public override void CalculateValues()
    {
        if (IsDead)
            return;

        Sword = AttackCards[0].Sword;
        Shield = AttackCards[1].Shield;
        Magic = AttackCards[2].Magic;
        Sword += AttackCards[3].Sword;
        Shield += AttackCards[4].Shield;
        Magic += AttackCards[5].Magic;

        _TurnValuesDisplay.UpdateSword(Sword);
        _TurnValuesDisplay.UpdateShield(Shield);
        _TurnValuesDisplay.UpdateMagic(Magic);
    }

    public override void RandomiseAttackSelect()
    {
        // Same as other method, needs less random logic later (harder enemies)
        var _randomCardOne = Random.Range(0, Cards.Count);
        int _randomCardTwo = 0;
        int _randomCardThree = 0;
        int _randomCardFour = 0;
        int _randomCardFive = 0;
        int _randomCardSix = 0;

        do
        {
            _randomCardTwo = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardTwo);

        do
        {
            _randomCardThree = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardThree || _randomCardTwo == _randomCardThree);

        do
        {
            _randomCardFour = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardFour || _randomCardTwo == _randomCardFour || _randomCardThree == _randomCardFour);

        do
        {
            _randomCardFive = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardFive || _randomCardTwo == _randomCardFive || _randomCardThree == _randomCardFive || _randomCardFour == _randomCardFive);

        do
        {
            _randomCardSix = Random.Range(0, Cards.Count);
        } while (_randomCardOne == _randomCardSix || _randomCardTwo == _randomCardSix || _randomCardThree == _randomCardSix || _randomCardFour == _randomCardSix || _randomCardFive == _randomCardSix);


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

        var _cardDataFour = new CardData()
        {
            Colour = Cards[_randomCardFour].CardData.Colour,
            Shield = Cards[_randomCardFour].CardData.Shield,
            Sword = Cards[_randomCardFour].CardData.Sword,
            Magic = Cards[_randomCardFour].CardData.Magic,
            BoonType = Cards[_randomCardFour].CardData.BoonType
        };

        var _cardDataFive = new CardData()
        {
            Colour = Cards[_randomCardFive].CardData.Colour,
            Shield = Cards[_randomCardFive].CardData.Shield,
            Sword = Cards[_randomCardFive].CardData.Sword,
            Magic = Cards[_randomCardFive].CardData.Magic,
            BoonType = Cards[_randomCardFive].CardData.BoonType
        };

        var _cardDataSix = new CardData()
        {
            Colour = Cards[_randomCardSix].CardData.Colour,
            Shield = Cards[_randomCardSix].CardData.Shield,
            Sword = Cards[_randomCardSix].CardData.Sword,
            Magic = Cards[_randomCardSix].CardData.Magic,
            BoonType = Cards[_randomCardSix].CardData.BoonType
        };

        AttackCards.Add(_cardDataOne);
        AttackCards.Add(_cardDataTwo);
        AttackCards.Add(_cardDataThree);
        AttackCards.Add(_cardDataFour);
        AttackCards.Add(_cardDataFive);
        AttackCards.Add(_cardDataSix);

        ClearCard(Cards[_randomCardOne]);
        ClearCard(Cards[_randomCardTwo]);
        ClearCard(Cards[_randomCardThree]);
        ClearCard(Cards[_randomCardFour]);
        ClearCard(Cards[_randomCardFive]);
        ClearCard(Cards[_randomCardSix]);

    }

    public void TriggerDroolA()
    {
        _DroolA.gameObject.SetActive(true);
        _DroolB.gameObject.SetActive(false);
        _DroolC.gameObject.SetActive(false);
    }


    public void TriggerDroolB()
    {
        _DroolA.gameObject.SetActive(false);
        _DroolB.gameObject.SetActive(true);
        _DroolC.gameObject.SetActive(false);
    }


    public void TriggerDroolC()
    {
        _DroolA.gameObject.SetActive(false);
        _DroolB.gameObject.SetActive(false);
        _DroolC.gameObject.SetActive(true);
    }

    public override void Reset()
    {
        base.Reset();

        _DroolA.gameObject.SetActive(false);
        _DroolB.gameObject.SetActive(false);
        _DroolC.gameObject.SetActive(false);
    }
}
