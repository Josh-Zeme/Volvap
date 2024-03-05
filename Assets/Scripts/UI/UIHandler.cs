using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private StartMenuHandler _StartMenuHandler;

    public void TriggerGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.None:
                break;
            case GameState.Menu:
                _StartMenuHandler.gameObject.SetActive(true);
                break;
            case GameState.TutorialSetup:
                _StartMenuHandler.gameObject.SetActive(false);
                break;
            case GameState.TutorialRound:
                
                break;
            default:
                Debug.Log("Oh yeah show the UI that doesn't exist.. dickhead");
                break;
        }
    }
}
