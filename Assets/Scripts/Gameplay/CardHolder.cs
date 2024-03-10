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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var _card = eventData.pointerDrag.GetComponent<Card>();
            var _cardData = new CardData()
            {
                Colour = _card.CardData.Colour,
                Shield = _card.CardData.Shield,
                Sword = _card.CardData.Sword,
                Magic = _card.CardData.Magic,
                BoonType = _card.CardData.BoonType
            };

            

            if (_card._IsHolderCard)
            {
                // Dragging from one holder to another!
                var _parentCardA = _card.CardHolder.ParentCard;
                var _parentCardB = ParentCard;
                var _cardDataB = new CardData()
                {
                    Colour = ParentCard.CardData.Colour,
                    Shield = ParentCard.CardData.Shield,
                    Sword = ParentCard.CardData.Sword,
                    Magic = ParentCard.CardData.Magic,
                    BoonType = ParentCard.CardData.BoonType
                };
                ParentCard = _parentCardA;
                _card.CardHolder.ParentCard = _parentCardB;

                _card.Generate(_cardDataB, ref _GameController);
                _card.RawPopulate();
                _card.gameObject.SetActive(true);
                _card.Show();
            }
            else if (ParentCard != null)
            {
                ParentCard.Show();
                ParentCard.IsUsing = false;
                ParentCard = null;
            }

            if (!_card._IsHolderCard)
            {
                _card.Hide();
                ParentCard = _card;
                ParentCard.IsUsing = true;
            }
            
            Card.Generate(_cardData, ref _GameController);
            Card.RawPopulate();
            
            Card.gameObject.SetActive(true);

            _CardHolderTopper.gameObject.SetActive(ParentCard != null);

            if(ParentCard != null)
            {
                _CardHolderTopper.GenerateTopper(Card.CardData.Colour);
            }
            var _cardMultiplier = _GameController.GetMultiplier(Type, _cardData.Colour, true);
            Card.SetMultiplier(_cardMultiplier);
            CardDrag.Instance.Hide();
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
