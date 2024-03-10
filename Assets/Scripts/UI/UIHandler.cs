using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private StartMenuHandler _StartMenuHandler;
    [SerializeField] private GameObject _TutorialInstructions;
    [SerializeField] private GameObject _TutorialInstructionInsult;
    [SerializeField] private GameObject _TutorialStart;
    [SerializeField] private CardHolder _SwordCardHolder;
    [SerializeField] private CardHolder _ShieldCardHolder;
    [SerializeField] private CardHolder _MagicCardHolder;

    public void TriggerGameState(GameState gameState, RoundState roundState)
    {
        switch (gameState)
        {
            case GameState.None:
                _TutorialInstructions.gameObject.SetActive(false);
                _TutorialStart.gameObject.SetActive(false);
                TriggerCardHolders(false);
                break;
            case GameState.Menu:
                _StartMenuHandler.gameObject.SetActive(true);
                _TutorialInstructions.gameObject.SetActive(false);
                _TutorialStart.gameObject.SetActive(false);
                TriggerCardHolders(false);
                break;
            case GameState.TutorialRound:
                switch (roundState)
                {
                    case RoundState.None:
                        _StartMenuHandler.gameObject.SetActive(false);
                        _TutorialInstructions.gameObject.SetActive(false);
                        _TutorialStart.gameObject.SetActive(false);
                        TriggerCardHolders(false);
                        break;
                    case RoundState.Exchange:
                        _TutorialInstructions.gameObject.SetActive(true);
                        _TutorialStart.gameObject.SetActive(false);
                        TriggerCardHolders(false);
                        break;
                    case RoundState.Select:
                        _TutorialInstructions.gameObject.SetActive(false);
                        _TutorialStart.gameObject.SetActive(true);
                        TriggerCardHolders(true);
                        break;
                    case RoundState.Attack:

                        break;
                }
                break;
            default:
                Debug.Log("Oh yeah show the UI that doesn't exist.. dickhead");
                break;
        }
    }

    public void TriggerCardHolders(bool isVisible)
    {
        _SwordCardHolder.gameObject.SetActive(isVisible);
        _ShieldCardHolder.gameObject.SetActive(isVisible);
        _MagicCardHolder.gameObject.SetActive(isVisible);
    }


    public void ShowInsult()
    {
        _TutorialInstructionInsult.gameObject.SetActive(true);
    }
}
