using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardBoonType
{
    None = 0, Multipy = 1, LoseACard = 2, Reflect = 3, Zero = 4
}

public enum CardColour
{
    Red = 0, Blue = 1, Yellow = 2, Black = 3
}

public class Card : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool IsSelected = false;
    public bool IsUsing = false;
    public bool IsHolderCard = false;
    [SerializeField] public CardHolder CardHolder;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private SpriteRenderer _SwordSpriteRenderer;
    [SerializeField] private SpriteRenderer _ShieldSpriteRenderer;
    [SerializeField] private SpriteRenderer _MagicSpriteRenderer;
    [SerializeField] private GameObject _Outline;
    [SerializeField] private bool _IsInteractable = false;
    [SerializeField] private bool _IsDragCard = false;
    
    private Vector3 _OriginalPosition;
    private Quaternion _OriginalRotation;
    public CardData CardData = null;
    private GameController _GameController;

    public void Start()
    {
        _OriginalPosition = transform.position;
        _OriginalRotation = transform.rotation;
    }

    public void Generate(CardData cardData, ref GameController gameController)
    {
        CardData = cardData;
        _GameController = gameController;
    }

    public void OnMouseDown()
    {
        if (!_IsInteractable)
            return;

        if (_GameController.GameState == GameState.TutorialCardSelect)
        {
            SelectCard();
        }
    }

    public void Reset()
    {
        IsSelected = false;
        if (_Outline != null)
        {
            _Outline.SetActive(IsSelected);
        }
        CardData.Owner = null;
        CardData.Sword = 0;
        CardData.Shield = 0;
        CardData.Magic = 0;
        CardData.BoonType = CardBoonType.None;
    }

    public void SelectCard()
    {
        if (IsSelected)
        {
            IsSelected = false;
            if (CardData != null && CardData.Owner is Player)
                GameSettings.Conductor.PlaySound(GameSound.HitBell);
        }
        else
        {
            if (CardData.Owner.SelectedCardCount() < 3)
            {
                IsSelected = true;
                if (CardData != null && CardData.Owner is Player)
                    GameSettings.Conductor.PlaySound(GameSound.HitBell);
            }
        }

        if (_Outline != null)
        {
            _Outline.SetActive(IsSelected);
        }
    }

    public void Show()
    {
        PopulateCard();
        if (_IsInteractable)
        {
            _SpriteRenderer.enabled = true;
            _SwordSpriteRenderer.enabled = true;
            _ShieldSpriteRenderer.enabled = true;
            _MagicSpriteRenderer.enabled = true;
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void PopulateCard()
    {
        if(CardData != null && CardData.Owner is Player)
        {
            RawPopulate();
        }
    }

    public void RawPopulate()
    {
        if (CardData == null)
            return;

        switch (CardData.Colour)
        {
            case CardColour.Blue:
                _SpriteRenderer.sprite = GameSettings.GameFactory.BluePlayerCard;
                break;
            case CardColour.Black:
                _SpriteRenderer.sprite = GameSettings.GameFactory.BlackPlayerCard;
                break;
            case CardColour.Yellow:
                _SpriteRenderer.sprite = GameSettings.GameFactory.YellowPlayerCard;
                break;
            case CardColour.Red:
                _SpriteRenderer.sprite = GameSettings.GameFactory.RedPlayerCard;
                break;
        }
        _SwordSpriteRenderer.sprite = GetNumberSprite(CardData.Sword);
        _ShieldSpriteRenderer.sprite = GetNumberSprite(CardData.Shield);
        _MagicSpriteRenderer.sprite = GetNumberSprite(CardData.Magic);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_IsInteractable)
            return;
        // MUST be implemented for the other two to work
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_IsInteractable)
            return;

        if (_GameController.GameState == GameState.TutorialStart && !IsUsing)
        {
            _SpriteRenderer.enabled = false;
            _SwordSpriteRenderer.enabled = false;
            _ShieldSpriteRenderer.enabled = false;
            _MagicSpriteRenderer.enabled = false;
            var _this = this;
            CardDrag.Instance.Show(ref _this);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_IsInteractable)
            return;

        if (!IsUsing)
        {
            _SpriteRenderer.enabled = true;
            _SwordSpriteRenderer.enabled = true;
            _ShieldSpriteRenderer.enabled = true;
            _MagicSpriteRenderer.enabled = true;
        }

        if (IsHolderCard)
        {
            var _isHolding = eventData.pointerDrag.GetComponent<CardHolder>();
            if (_isHolding == null)
            {
                CardHolder.ParentCard.Show();
                CardHolder.ParentCard.IsUsing = false;
                CardHolder.ParentCard = null;
                Hide();

            }
        }
        CardDrag.Instance.Hide();
    }

    public Sprite GetNumberSprite(int number)
    {
        // I know.. this method is absolutely dogshit attrocious... ignore it.
        // i couldn't be bothered messing with it in another way.. i just went blunt force approach cause im lazy
        if ((CardData != null && CardData.Owner is Player) || (CardData != null && _IsDragCard))
        {
            switch (CardData.Colour)
            {
                case CardColour.Blue:
                    switch (number)
                    {
                        case 0:
                            return GameSettings.GameFactory.BlueZero;
                        case 1:
                            return GameSettings.GameFactory.BlueOne;
                        case 2:
                            return GameSettings.GameFactory.BlueTwo;
                        case 3:
                            return GameSettings.GameFactory.BlueThree;
                        case 4:
                            return GameSettings.GameFactory.BlueFour;
                        case 5:
                            return GameSettings.GameFactory.BlueFive;
                        case 6:
                            return GameSettings.GameFactory.BlueSix;
                        case 7:
                            return GameSettings.GameFactory.BlueSeven;
                        case 8:
                            return GameSettings.GameFactory.BlueEight;
                        case 9:
                            return GameSettings.GameFactory.BlueNine;
                        case 10:
                            return GameSettings.GameFactory.BlueTen;

                    }
                    break;
                case CardColour.Black:
                    switch (number)
                    {
                        case 0:
                            return GameSettings.GameFactory.BlackZero;
                        case 1:
                            return GameSettings.GameFactory.BlackOne;
                        case 2:
                            return GameSettings.GameFactory.BlackTwo;
                        case 3:
                            return GameSettings.GameFactory.BlackThree;
                        case 4:
                            return GameSettings.GameFactory.BlackFour;
                        case 5:
                            return GameSettings.GameFactory.BlackFive;
                        case 6:
                            return GameSettings.GameFactory.BlackSix;
                        case 7:
                            return GameSettings.GameFactory.BlackSeven;
                        case 8:
                            return GameSettings.GameFactory.BlackEight;
                        case 9:
                            return GameSettings.GameFactory.BlackNine;
                        case 10:
                            return GameSettings.GameFactory.BlackTen;

                    }
                    break;
                case CardColour.Yellow:
                    switch (number)
                    {
                        case 0:
                            return GameSettings.GameFactory.YellowZero;
                        case 1:
                            return GameSettings.GameFactory.YellowOne;
                        case 2:
                            return GameSettings.GameFactory.YellowTwo;
                        case 3:
                            return GameSettings.GameFactory.YellowThree;
                        case 4:
                            return GameSettings.GameFactory.YellowFour;
                        case 5:
                            return GameSettings.GameFactory.YellowFive;
                        case 6:
                            return GameSettings.GameFactory.YellowSix;
                        case 7:
                            return GameSettings.GameFactory.YellowSeven;
                        case 8:
                            return GameSettings.GameFactory.YellowEight;
                        case 9:
                            return GameSettings.GameFactory.YellowNine;
                        case 10:
                            return GameSettings.GameFactory.YellowTen;

                    }
                    break;
                case CardColour.Red:
                    switch (number)
                    {
                        case 0:
                            return GameSettings.GameFactory.RedZero;
                        case 1:
                            return GameSettings.GameFactory.RedOne;
                        case 2:
                            return GameSettings.GameFactory.RedTwo;
                        case 3:
                            return GameSettings.GameFactory.RedThree;
                        case 4:
                            return GameSettings.GameFactory.RedFour;
                        case 5:
                            return GameSettings.GameFactory.RedFive;
                        case 6:
                            return GameSettings.GameFactory.RedSix;
                        case 7:
                            return GameSettings.GameFactory.RedSeven;
                        case 8:
                            return GameSettings.GameFactory.RedEight;
                        case 9:
                            return GameSettings.GameFactory.RedNine;
                        case 10:
                            return GameSettings.GameFactory.RedTen;

                    }
                    break;
            }
        }
        return null;
    }

}


[Serializable]
public class CardData
{
    public Unit Owner;
    public int Sword;
    public int Shield;
    public int Magic;
    public CardBoonType BoonType; // Not Decided if i'll use this. the rest should be fine though
    public CardColour Colour;


    public void Randomise()
    {
        var _colour = UnityEngine.Random.Range(0, 4);
        Colour = (CardColour)_colour;

        var _boonRange = UnityEngine.Random.Range(0, 500);
        if (_boonRange < 350)
        {
            BoonType = CardBoonType.None;
        }
        else if (_boonRange < 400)
        {
            BoonType = CardBoonType.Multipy;
        }
        else if (_boonRange < 425)
        {
            BoonType = CardBoonType.LoseACard;
        }
        else if (_boonRange < 450)
        {
            BoonType = CardBoonType.LoseACard;
        }
        else
        {
            BoonType = CardBoonType.Zero;
        }

        var _cardType = UnityEngine.Random.Range(0, 3);
        // May need to balance the types.
        // As if you are only defending 10, you may get hit for 20 each round. so it's a "who can out damage? but i guess multiple is important"
        switch (_cardType)
        {
            // Sword Main
            case 0:
                Sword = UnityEngine.Random.Range(2, 10);
                Shield = UnityEngine.Random.Range(0, 5);
                Magic = UnityEngine.Random.Range(0, 1);
                break;
            // Shield Main
            case 1:
                Sword = UnityEngine.Random.Range(0, 5);
                Shield = UnityEngine.Random.Range(2, 10);
                Magic = UnityEngine.Random.Range(0, 1);
                break;
            // Magic Main
            case 2:
                Sword = UnityEngine.Random.Range(0, 5);
                Shield = UnityEngine.Random.Range(0, 5);
                Magic = UnityEngine.Random.Range(1, 4);
                break;
            default:
                Debug.Log("Somehow got a card type that doesn't exist");
                break;
        }
    }
}
