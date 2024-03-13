using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardHolderType
{
    Sword = 0, Shield = 1, Magic = 2
}

public class CardHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameController _GameController;
    [SerializeField] private CardHolderTopper _CardHolderTopper;
    public CardHolderType Type;
    public Card Card;
    public Card ParentCard;
    public CardColour? CardColour => ParentCard?.CardData?.Colour;

    public void ClearCard()
    {
        ParentCard.IsUsing = false;
        ParentCard = null;
        RefreshTopper();
        Card.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var _card = eventData.pointerDrag.GetComponent<Card>();

            if (_card == Card)
            {
                // It just needs to exit out early cause is being dragged onto itself
                return;
            }

            var _cardData = new CardData()
            {
                Colour = _card.CardData.Colour,
                Shield = _card.CardData.Shield,
                Sword = _card.CardData.Sword,
                Magic = _card.CardData.Magic,
                BoonType = _card.CardData.BoonType
            };

            

            if (_card.IsHolderCard)
            {
                // Dragging from one holder to another!
                _card.IsSwappingHolder = true;
                var _parentCardA = _card.CardHolder.ParentCard;
                Card _parentCardB = null;
                CardData _cardDataB = null;
                if (ParentCard != null) { 
                    _parentCardB = ParentCard;
                    _cardDataB = new CardData()
                    {
                        Colour = ParentCard.CardData.Colour,
                        Shield = ParentCard.CardData.Shield,
                        Sword = ParentCard.CardData.Sword,
                        Magic = ParentCard.CardData.Magic,
                        BoonType = ParentCard.CardData.BoonType
                    };
                }
                ParentCard = _parentCardA;

                if(_cardDataB == null)
                {
                    _card.CardHolder.ClearCard();
                }
                else
                {
                    _card.CardHolder.ParentCard = _parentCardB;
                    _card.Generate(_cardDataB, ref _GameController);
                    _card.RawPopulate();
                    _card.gameObject.SetActive(true);
                    _card.Show();
                    _card.CardHolder.RefreshTopper();
                }
            }
            else if (ParentCard != null)
            {
                ParentCard.Show();
                ParentCard.IsUsing = false;
                ParentCard = null;
            }

            if (!_card.IsHolderCard)
            {
                _card.Hide();
                ParentCard = _card;
                ParentCard.IsUsing = true;
            }
            
            Card.Generate(_cardData, ref _GameController);
            Card.RawPopulate();
            
            Card.gameObject.SetActive(true);

            RefreshTopper();


            var _cardMultiplier = _GameController.GetMultiplier(Type, _cardData.Colour, true);
            Card.SetMultiplier(_cardMultiplier);
            CardDrag.Instance.Hide();
        }
    }

    public void RefreshTopper()
    {
        _CardHolderTopper.gameObject.SetActive(ParentCard != null);
        if (ParentCard != null)
        {
            _CardHolderTopper.GenerateTopper(Card.CardData.Colour);
        }
    }

    public void RefreshMultipler()
    {
        if (ParentCard == null)
            return;

        var _cardMultiplier = _GameController.GetMultiplier(Type, Card.CardData.Colour, false);
        Card.SetMultiplier(_cardMultiplier);
    }
}
