using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private StartMenuHandler _StartMenuHandler;

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

    #endregion


    [SerializeField] private CardHolder _SwordCardHolder;
    [SerializeField] private CardHolder _ShieldCardHolder;
    [SerializeField] private CardHolder _MagicCardHolder;

    public void TriggerGameState(GameState gameState, RoundState roundState, AttackPhase attackPhase)
    {
        DisableAllElements();
        switch (gameState)
        {
            case GameState.None:
                _YouLoseTryAgain.gameObject.SetActive(false);
                break;
            case GameState.Menu:
                _StartMenuHandler.gameObject.SetActive(true);
                _YouLoseTryAgain.gameObject.SetActive(false);
                break;
            case GameState.TutorialRound:
                _YouLoseTryAgain.gameObject.SetActive(false);
                switch (roundState)
                {
                    case RoundState.None:
                        break;
                    case RoundState.Exchange:
                        _TutorialInstructions.gameObject.SetActive(true);
                        break;
                    case RoundState.Select:
                        _TutorialStart.gameObject.SetActive(true);
                        TriggerCardHolders(true);
                        break;
                    case RoundState.Attack:
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
                switch (roundState)
                {
                    case RoundState.Select:
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
        _StartMenuHandler.gameObject.SetActive(false);
        _TutorialInstructions.gameObject.SetActive(false);
        _TutorialStart.gameObject.SetActive(false);
        _TutorialPlaceCard.gameObject.SetActive(false);
        _TutorialDisplay.gameObject.SetActive(false);
        _TutorialSword.gameObject.SetActive(false);
        _TutorialMagic.gameObject.SetActive(false);
        _TutorialEnd.gameObject.SetActive(false);
        TriggerCardHolders(false);
    }

    public void EnableGameOver()
    {
        _YouLoseTryAgain.gameObject.SetActive(true);
    }

    public void ShowInsult()
    {
        _TutorialInstructionInsult.gameObject.SetActive(true);
    }
}
