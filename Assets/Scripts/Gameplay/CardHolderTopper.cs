using UnityEngine;
using UnityEngine.EventSystems;

public class CardHolderTopper : MonoBehaviour
{
    [SerializeField] private CardHolderType _Type;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    private CardColour _Colour;

    public void GenerateTopper(CardColour color)
    {
        _Colour = color;
        switch (_Colour)
        {
            case CardColour.Blue:
                switch (_Type)
                {
                    case CardHolderType.Sword:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlueSword;
                        break;
                    case CardHolderType.Shield:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlueShield;
                        break;
                    case CardHolderType.Magic:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlueMagic;
                        break;
                }
                break;
            case CardColour.Black:
                switch (_Type)
                {
                    case CardHolderType.Sword:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlackSword;
                        break;
                    case CardHolderType.Shield:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlackShield;
                        break;
                    case CardHolderType.Magic:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.BlackMagic;
                        break;
                }
                break;
            case CardColour.Red:
                switch (_Type)
                {
                    case CardHolderType.Sword:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.RedSword;
                        break;
                    case CardHolderType.Shield:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.RedShield;
                        break;
                    case CardHolderType.Magic:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.RedMagic;
                        break;
                }
                break;
            case CardColour.Yellow:
                switch (_Type)
                {
                    case CardHolderType.Sword:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.YellowSword;
                        break;
                    case CardHolderType.Shield:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.YellowShield;
                        break;
                    case CardHolderType.Magic:
                        _SpriteRenderer.sprite = GameSettings.GameFactory.YellowMagic;
                        break;
                }
                break;
        }
    }
}
