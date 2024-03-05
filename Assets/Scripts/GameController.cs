using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0, Menu = 1, TutorialSetup = 2, TutorialRound = 3
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIHandler _UIHandler;
    [SerializeField] private CinemachineVirtualCamera _PreGameCamera;
    [SerializeField] private CinemachineVirtualCamera _InGameCamera;

    [SerializeField] private Player _Player;
    [SerializeField] private List<Unit> _Units;
    [SerializeField] private Deck _Deck;
    [SerializeField] private RoofLight _RoofLight;
    [SerializeField] private Clock _Clock;

    public GameState GameState = GameState.None;



    public void Start()
    {
        TriggerGameState();
        _InGameCamera.Priority = 0;
        _PreGameCamera.Priority = 1;
    }

    public void FixedUpdate()
    {
        switch (GameState)
        {
            case GameState.TutorialSetup:
                if (!_RoofLight.IsFlickering)
                {
                    _Clock.TriggerDrool();
                    for (int _i = 0; _i < _Units.Count; _i++)
                    {
                        //Start smoking again
                        _Units[_i].TriggerSmoking();
                    }
                    DealCards();
                    TriggerGameState();
                }
                break;
            default:
                break;
        }
    }

    public void TriggerGameState()
    {
        switch (GameState)
        {
            case GameState.None:
                GameState = GameState.Menu;
                break;
            case GameState.Menu:
                GameState = GameState.TutorialSetup;
                //MockTreats();
                // trigger clock move
                for (int _i = 0; _i < _Units.Count; _i++)
                {
                    //Should stop all units from smoking
                    _Units[_i].TriggerSmoking();
                }
                _RoofLight.TriggerForce(new Vector2(500, 0));
                _RoofLight.TriggerFlicker(1.5f, new List<float>() { 0.2f, 0.25f, 0.4f, 0.45f, 0.5f, 0.55f, 0.6f, 0.61f });
                
                //_Clock.TriggerAberration(1.5f, new List<float>() { 0.6f, 0.61f });
                _InGameCamera.Priority = 1;
                _PreGameCamera.Priority = 0;
                break;
            case GameState.TutorialSetup:
                GameState = GameState.TutorialRound;
                break;
            default:
                Debug.Log("How the hell did you get here?");
                break;
        }
        // fade the text rather than disable it.
        _UIHandler.TriggerGameState(GameState);
    }

    private void DealCards()
    {
        for (int _i = 0; _i < _Units.Count; _i++)
        {
            var _unit = _Units[_i];
            for(int _d = 0; _d < GameSettings.CardsPerPlayer; _d++)
            {
                _unit.AddCard(_Deck.TakeCard());
            }
        }
    }

    private void SwapCards()
    {

    }

    private void RefillUsed()
    {

    }

    private void DevourUnit()
    {

    }

    private void MockTreats()
    {
        for(int _i = 0; _i < _Units.Count; _i++)
        {
            var _random = Random.Range(5, 10);
            var _unit = _Units[_i];
            _unit.AddTreats(50);
        }
    }
}
