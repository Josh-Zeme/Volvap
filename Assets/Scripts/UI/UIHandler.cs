using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private StartMenuHandler _StartMenuHandler;
    [SerializeField] private GameObject _TutorialInstructions;

    public void TriggerGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.None:
                _TutorialInstructions.gameObject.SetActive(false);
                break;
            case GameState.Menu:
                _StartMenuHandler.gameObject.SetActive(true);
                _TutorialInstructions.gameObject.SetActive(false);
                break;
            case GameState.TutorialSetup:
                _StartMenuHandler.gameObject.SetActive(false);
                _TutorialInstructions.gameObject.SetActive(false);
                break;
            case GameState.TutorialCardSelect:
                _TutorialInstructions.gameObject.SetActive(true);
                break;
            case GameState.TutorialStart:
                _TutorialInstructions.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Oh yeah show the UI that doesn't exist.. dickhead");
                break;
        }
    }
}
