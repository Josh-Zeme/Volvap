using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardHolderType
{
    Sword = 0, Shield = 1, Magic = 2
}

public class CardHolder : MonoBehaviour, IDropHandler
{
    public CardHolderType Type;
    public Card Card;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var _card =  eventData.pointerDrag.GetComponent<Card>();
            Card = _card;

        }
    }
}
