using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0, Menu = 1, TutorialRound = 2,
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIHandler _UIHandler;
    [SerializeField] private CinemachineVirtualCamera _PreGameCamera;
    [SerializeField] private CinemachineVirtualCamera _InGameCamera;

    [SerializeField] private List<Unit> _Units;
    [SerializeField] private Deck _Deck;

    public GameState GameState = GameState.None;
    

    public void Start()
    {
        TriggerGameState();
        _InGameCamera.Priority = 0;
        _PreGameCamera.Priority = 1;
    }

    public void TriggerGameState()
    {
        switch (GameState)
        {
            case GameState.None:
                GameState = GameState.Menu;
                break;
            case GameState.Menu:
                GameState = GameState.TutorialRound;
                // trigger clock move
                // trigger saliva
                // light move
                // lighting flicer
                _InGameCamera.Priority = 1;
                _PreGameCamera.Priority = 0;
                // trigger card drawing
                break;
            default:
                Debug.Log("How the hell did you get here?");
                break;
        }
        // fade the text rather than disable it.
        _UIHandler.TriggerGameState(GameState);
    }
}
