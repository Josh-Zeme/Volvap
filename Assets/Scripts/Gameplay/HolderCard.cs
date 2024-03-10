using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolderCard : Card
{
    [SerializeField] private CardHolderType _CardHolderType;
    [SerializeField] private SpriteRenderer _BigNumber;
    [SerializeField] private SpriteRenderer _Multiplier;

    public override void RawPopulate()
    {
        if (CardData == null)
            return;

        switch (_CardHolderType)
        {
            case CardHolderType.Sword:
                _BigNumber.sprite = GetNumberSprite(CardData.Sword);
                break;
            case CardHolderType.Shield:
                _BigNumber.sprite = GetNumberSprite(CardData.Shield);
                break;
            case CardHolderType.Magic:
                _BigNumber.sprite = GetNumberSprite(CardData.Magic);
                break;
        }
        _BigNumber.enabled = true;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!_IsInteractable)
            return;

        if (_GameController.RoundState == RoundState.Select && !IsUsing)
        {
            _SpriteRenderer.enabled = false;
            _BigNumber.enabled = false;
            _Multiplier.enabled = false;
            var _this = (Card)this;
            CardDrag.Instance.Show(ref _this);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!_IsInteractable)
            return;

        if (!IsUsing)
        {
            _SpriteRenderer.enabled = true;
            _BigNumber.enabled = true;
            _Multiplier.enabled = true;
        }

        if (_IsHolderCard)
        {
            var _isHolding = eventData.pointerDrag.GetComponent<CardHolder>();
            if (_isHolding == null && CardHolder.ParentCard != null)
            {
                CardHolder.ParentCard.Show();
                CardHolder.ParentCard.IsUsing = false;
                CardHolder.ParentCard = null;
                var _cardMultiplier = _GameController.GetMultiplier(CardHolder.Type, CardData.Colour, true);
                SetMultiplier(_cardMultiplier);
                Hide();

            }
        }
        CardDrag.Instance.Hide();
    }

    public override void SetMultiplier(int multiplier)
    {
        if (CardData == null)
            return;

        switch (CardData.Colour)
        {
            case CardColour.Blue:
                switch (multiplier)
                {
                    case 1:
                        _Multiplier.gameObject.SetActive(false);
                        break;
                    case 2:
                        _Multiplier.sprite = GameSettings.GameFactory.BlueTwoTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                    case 3:
                        _Multiplier.sprite = GameSettings.GameFactory.BlueThreeTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                }
                break;
            case CardColour.Black:
                switch (multiplier)
                {
                    case 1:
                        _Multiplier.gameObject.SetActive(false);
                        break;
                    case 2:
                        _Multiplier.sprite = GameSettings.GameFactory.BlackTwoTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                    case 3:
                        _Multiplier.sprite = GameSettings.GameFactory.BlackThreeTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                }
                break;
            case CardColour.Yellow:
                switch (multiplier)
                {
                    case 1:
                        _Multiplier.gameObject.SetActive(false);
                        break;
                    case 2:
                        _Multiplier.sprite = GameSettings.GameFactory.YellowTwoTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                    case 3:
                        _Multiplier.sprite = GameSettings.GameFactory.YellowThreeTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                }
                break;
            case CardColour.Red:
                switch (multiplier)
                {
                    case 1:
                        _Multiplier.gameObject.SetActive(false);
                        break;
                    case 2:
                        _Multiplier.sprite = GameSettings.GameFactory.RedTwoTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                    case 3:
                        _Multiplier.sprite = GameSettings.GameFactory.RedThreeTimes;
                        _Multiplier.gameObject.SetActive(true);
                        break;
                }
                break;
        }
    }
}
