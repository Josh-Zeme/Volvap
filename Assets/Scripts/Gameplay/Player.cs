using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{


    public override void DiscardAllCards()
    {
        for (int _i = 0; _i < Cards.Count; _i++)
        {
            var _card = Cards[_i];
            _card.CardData.ClearKeepOwner();
        }

        AttackCards.Clear();
    }

}
