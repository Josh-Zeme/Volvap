using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private StartMenuHandler _StartMenuHandler;
    [SerializeField] private Light2D _GlobalLight;
    [SerializeField] private RoofLight _RoofLight;
    [SerializeField] private Bell _Bell;
    #region Tutorial Instructions

    [SerializeField] private GameObject _TutorialInstructions;
    [SerializeField] private GameObject _TutorialInstructionInsult;
    [SerializeField] private GameObject _TutorialStart;
    [SerializeField] private GameObject _TutorialPlaceCard;
    [SerializeField] private GameObject _TutorialDisplay;
    [SerializeField] private GameObject _TutorialSword;
    [SerializeField] private GameObject _TutorialMagic;
    [SerializeField] private GameObject _TutorialEnd;
    [SerializeField] private GameObject _YouLoseTryAgain;
    [SerializeField] private GameObject _GoodBoy;

    #endregion

    #region Gameplay instructions

    [SerializeField] private GameObject _GamePlaySelect;
    [SerializeField] private GameObject _GamePlayDrag;
    [SerializeField] private GameObject _GamePlaySelectInsult;

    #endregion

    [SerializeField] private CardHolder _SwordCardHolder;
    [SerializeField] private CardHolder _ShieldCardHolder;
    [SerializeField] private CardHolder _MagicCardHolder;

    public void TriggerGameState(GameState gameState, RoundState roundState, AttackPhase attackPhase)
    {
        DisableAllElements();
        switch (gameState)
        {
            case GameState.EndGame:
                break;
            case GameState.None:
                _RoofLight.SetIntensity(GameSettings.TutorialRoofLight);
                _Bell.MakeUnlit();
                _GlobalLight.intensity = GameSettings.TutorialGlobalLight;
                _YouLoseTryAgain.gameObject.SetActive(false);
                break;
            case GameState.Menu:
                _GlobalLight.intensity = GameSettings.TutorialGlobalLight;
                _RoofLight.SetIntensity(GameSettings.TutorialRoofLight);
                _Bell.MakeUnlit();
                _StartMenuHandler.gameObject.SetActive(true);
                _YouLoseTryAgain.gameObject.SetActive(false);
                break;
            case GameState.TutorialRound:
                _RoofLight.SetIntensity(GameSettings.TutorialRoofLight);
                
                _YouLoseTryAgain.gameObject.SetActive(false);
                switch (roundState)
                {
                    case RoundState.None:
                        break;
                    case RoundState.Exchange:
                        _GlobalLight.intensity = GameSettings.TutorialGlobalLight;
                        _TutorialInstructions.gameObject.SetActive(true);
                        _Bell.MakeUnlit();
                        break;
                    case RoundState.Select:
                        _TutorialStart.gameObject.SetActive(true);
                        _Bell.MakeUnlit();
                        TriggerCardHolders(true);
                        break;
                    case RoundState.Attack:
                        _Bell.MakeLit();
                        switch (attackPhase)
                        {
                            case AttackPhase.None:
                                break;
                            case AttackPhase.Setup:
                                break;
                            case AttackPhase.PlaceCard:
                                _TutorialPlaceCard.gameObject.SetActive(true);
                                break;
                            case AttackPhase.Display:
                                _TutorialDisplay.gameObject.SetActive(true);
                                break;
                            case AttackPhase.SwordAttack:
                                _TutorialSword.gameObject.SetActive(true);
                                break;
                            case AttackPhase.MagicAttack:
                                _TutorialMagic.gameObject.SetActive(true);
                                break;
                            case AttackPhase.RandomiseCards:
                                _TutorialEnd.gameObject.SetActive(true);
                                break;
                            case AttackPhase.CleanupUnits:
                                _TutorialEnd.gameObject.SetActive(true);
                                break;
                            case AttackPhase.DrawCards:
                                _TutorialEnd.gameObject.SetActive(true);
                                break;
                        }

                        break;
                }
                break;
            default:
                
                _RoofLight.SetIntensity(GameSettings.BaseRoofLight);
                _Bell.MakeLit();
                switch (roundState)
                {
                    case RoundState.Exchange:
                        _GamePlaySelect.gameObject.SetActive(true);
                        break;
                    case RoundState.Select:
                        _GlobalLight.intensity = GameSettings.BaseGlobalLight;
                        _GamePlayDrag.gameObject.SetActive(true);
                        TriggerCardHolders(true);
                        break;
                    case RoundState.Attack:
                        _YouLoseTryAgain.gameObject.SetActive(false);
                        switch (attackPhase)
                        {
                            case AttackPhase.None:
                                break;
                            case AttackPhase.Setup:
                                break;
                            case AttackPhase.PlaceCard:
                                
                                break;
                            case AttackPhase.Display:
                                
                                break;
                            case AttackPhase.SwordAttack:
                                
                                break;
                            case AttackPhase.MagicAttack:
                                
                                break;
                            case AttackPhase.RandomiseCards:
                                
                                break;
                            case AttackPhase.CleanupUnits:
                                
                                break;
                            case AttackPhase.DrawCards:
                                
                                break;
                        }

                        break;
                }
                break;
        }
    }

    public void TriggerCardHolders(bool isVisible)
    {
        _SwordCardHolder.gameObject.SetActive(isVisible);
        _ShieldCardHolder.gameObject.SetActive(isVisible);
        _MagicCardHolder.gameObject.SetActive(isVisible);
    }

    public void DisableAllElements() {
        _GamePlaySelect.gameObject.SetActive(false);
        _GoodBoy.gameObject.SetActive(false);
        _GamePlayDrag.gameObject.SetActive(false);
        _StartMenuHandler.gameObject.SetActive(false);
        _TutorialInstructions.gameObject.SetActive(false);
        _TutorialStart.gameObject.SetActive(false);
        _TutorialPlaceCard.gameObject.SetActive(false);
        _TutorialDisplay.gameObject.SetActive(false);
        _TutorialSword.gameObject.SetActive(false);
        _TutorialMagic.gameObject.SetActive(false);
        _TutorialEnd.gameObject.SetActive(false);
        _GamePlaySelectInsult.gameObject.SetActive(false);

        TriggerCardHolders(false);
    }

    public void EnableGameOver()
    {
        _YouLoseTryAgain.gameObject.SetActive(true);
    }

    public void EnableGoodBoy()
    {
        _GoodBoy.gameObject.SetActive(true);
    }

    public void ShowTutorialInsult()
    {
        _TutorialInstructionInsult.gameObject.SetActive(true);
    }

    public void ShowGameplayInsult()
    {
        _GamePlaySelectInsult.gameObject.SetActive(true);
    }
}
