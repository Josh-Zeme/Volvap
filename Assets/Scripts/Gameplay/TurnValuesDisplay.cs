using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnValuesDisplay : MonoBehaviour
{
    public int Sword;
    public int Shield;
    public int Magic;
    [SerializeField] SpriteRenderer _SwordSpriteRenderer;
    [SerializeField] SpriteRenderer _ShieldSpriteRenderer;
    [SerializeField] SpriteRenderer _MagicSpriteRenderer;
    [SerializeField] SpriteRenderer _BrokenShieldSpriteRenderer;

    [SerializeField] SpriteRenderer _TensSwordNumberSpriteRenderer;
    [SerializeField] SpriteRenderer _SinglesSwordNumberSpriteRenderer;

    [SerializeField] SpriteRenderer _TensShieldNumberSpriteRenderer;
    [SerializeField] SpriteRenderer _SinglesShieldNumberSpriteRenderer;

    [SerializeField] SpriteRenderer _TensMagicNumberSpriteRenderer;
    [SerializeField] SpriteRenderer _SinglesMagicNumberSpriteRenderer;

    public void ForcedHide()
    {
        _SwordSpriteRenderer.enabled = false;
        _ShieldSpriteRenderer.enabled = false;
        _MagicSpriteRenderer.enabled = false;
        _BrokenShieldSpriteRenderer.enabled = false; ;

        _TensSwordNumberSpriteRenderer.enabled = false;
        _SinglesSwordNumberSpriteRenderer.enabled = false;

        _TensShieldNumberSpriteRenderer.enabled = false;
        _SinglesShieldNumberSpriteRenderer.enabled = false;

        _TensMagicNumberSpriteRenderer.enabled = false;
        _SinglesMagicNumberSpriteRenderer.enabled = false; ;
    }

    public void UpdateShield(int shield)
    {
        Shield = shield;
        _ShieldSpriteRenderer.enabled = Shield > 0;
        _BrokenShieldSpriteRenderer.enabled = Shield == 0;
        var _tens = Shield / 10;
        var _singles = Shield - (_tens * 10);

        if (_tens > 0)
        {
            _TensShieldNumberSpriteRenderer.enabled = true;
            _TensShieldNumberSpriteRenderer.sprite = GetNumberSprite(_tens);
        }

        if (Shield > 0)
        {
            _SinglesShieldNumberSpriteRenderer.enabled = true;
            _SinglesShieldNumberSpriteRenderer.sprite = GetNumberSprite(_singles);
        }

        if (Shield == 0)
        {
            _TensShieldNumberSpriteRenderer.enabled = false;
            _SinglesShieldNumberSpriteRenderer.enabled = false;
        }
    }

    public void UpdateSword(int sword)
    {
        Sword = sword;
        _SwordSpriteRenderer.enabled = Sword > 0;
        var _tens = Sword / 10;
        var _singles = Sword - (_tens * 10);

        if (_tens > 0)
        {
            _TensSwordNumberSpriteRenderer.enabled = true;
            _TensSwordNumberSpriteRenderer.sprite = GetNumberSprite(_tens);
        }

        if (Sword > 0)
        {
            _SinglesSwordNumberSpriteRenderer.enabled = true;
            _SinglesSwordNumberSpriteRenderer.sprite = GetNumberSprite(_singles);
        }
    }

    public void UpdateMagic(int magic)
    {
        Magic = magic;
        _MagicSpriteRenderer.enabled = Magic > 0;
        var _tens = Magic / 10;
        var _singles = Magic - (_tens * 10);

        if (_tens > 0)
        {
            _TensMagicNumberSpriteRenderer.enabled = true;
            _TensMagicNumberSpriteRenderer.sprite = GetNumberSprite(_tens);
        }

        if (Magic > 0)
        {
            _SinglesMagicNumberSpriteRenderer.enabled = true;
            _SinglesMagicNumberSpriteRenderer.sprite = GetNumberSprite(_singles);
        }

    }

    public Sprite GetNumberSprite(int number)
    {
        switch (number)
        {

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
            default:
            case 0:
                return GameSettings.GameFactory.BlackZero;
        }


    }
}
