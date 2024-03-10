using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0, Menu = 1, TutorialSetup = 2, TutorialCardSelect = 3, TutorialStart = 4,
}

// This is the state of the current round. Then we can use the "update gamestate at the end of each turn so the turns are easier to sort out and the games are on a loop and logically much easier)
public enum RoundState
{
    None = 0, Draw = 1, Discard = 2, Select = 3, Attack =4
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

    [SerializeField] private CardHolder _SwordCardHolder;
    [SerializeField] private CardHolder _ShieldCardHolder;
    [SerializeField] private CardHolder _MagicCardHolder;

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
                GameState = GameState.TutorialCardSelect;
                break;
            case GameState.TutorialCardSelect:
                ExchangeCards();
                GameState = GameState.TutorialStart;
                for (int _i = 0; _i < _Units.Count; _i++)
                {
                    //Should stop all units from smoking
                    _Units[_i].TriggerSmoking();
                }
                _RoofLight.TriggerForce(new Vector2(450, 0));
                _RoofLight.TriggerFlicker(0.5f, new List<float>() { 0.1f, 0.11f, 0.15f, 0.20f, 0.3f, 0.35f, 0.4f, 0.45f });

                _Clock.TriggerAberration(0.5f, new List<float>() { 0.15f, 0.20f });
                break;
            case GameState.TutorialStart:
                Debug.Log("Do attack round");
                // clock logic - https://www.youtube.com/watch?v=oWEiYuVkVOw
                // may need to animate it though to do it properly. as in. we need a fixed update over time to get it to the time we want.
                // that means i can force the clock to be FUCKKKY
                DoAttackPhase();

                Debug.Log("Move to the next phase - real game begins.");
                break;
            default:
                Debug.Log("How the hell did you get here?");
                break;
        }
        // fade the text rather than disable it.
        _UIHandler.TriggerGameState(GameState);
    }

    public int GetMultiplier(CardHolderType holderType, CardColour colour, bool isIterate)
    {
        var _multiplier = 1;

        switch (holderType)
        {
            case CardHolderType.Sword:
                if (_MagicCardHolder.CardColour != null && _MagicCardHolder.CardColour.Value == colour)
                    _multiplier++;
                if (_ShieldCardHolder.CardColour != null && _ShieldCardHolder.CardColour.Value == colour)
                    _multiplier++;

                if (isIterate)
                {
                    _MagicCardHolder.RefreshMultipler();
                    _ShieldCardHolder.RefreshMultipler();
                }
                break;
            case CardHolderType.Shield:
                if (_MagicCardHolder.CardColour != null && _MagicCardHolder.CardColour.Value == colour)
                    _multiplier++;
                if (_SwordCardHolder.CardColour != null && _SwordCardHolder.CardColour.Value == colour)
                    _multiplier++;

                if (isIterate)
                {
                    _MagicCardHolder.RefreshMultipler();
                    _SwordCardHolder.RefreshMultipler();
                }
                break;
            case CardHolderType.Magic:
                if (_SwordCardHolder.CardColour != null && _SwordCardHolder.CardColour.Value == colour)
                    _multiplier++;
                if (_ShieldCardHolder.CardColour != null && _ShieldCardHolder.CardColour.Value == colour)
                    _multiplier++;

                if (isIterate)
                {
                    _SwordCardHolder.RefreshMultipler();
                    _ShieldCardHolder.RefreshMultipler();
                }
                break;
        }

        return _multiplier;
    }

    private void DealCards()
    {
        var _this = this;
        for (int _i = 0; _i < _Units.Count; _i++)
        {
            var _unit = _Units[_i];
            GameSettings.Conductor.PlaySound(GameSound.CardDeal);
            for (int _d = 0; _d < GameSettings.CardsPerPlayer; _d++)
            {
                _unit.AddCard(_Deck.TakeCard(), ref _this);
            }
        }
    }

    private void DrawCards()
    {
        Debug.Log("This is similar to deal cards except that it only gives what is missing (which is 3)");
    }

    private void ExchangeCards()
    {
        switch (GameState)
        {
            case GameState.TutorialCardSelect:
                RotateCardStandard();
                break;
            default:
                break;
        }
    }

    private void DoAttackPhase()
    {
        // Similar to this, but for attacking and defending
        _Units[0].RandomiseSelected();
        _Units[1].RandomiseSelected();
        _Units[2].RandomiseSelected();

        // Show cards above head


        // Show defence points of all. with multiplier! (Loop through all)

        // Show attack damage and shield breaks etc.

        // add up any damage.

        // throw cards on the table somewhere (there will be a "collection" of cards invisible on the table and they will get populated slowly as the game goes on to make it look like a discard pile!
        // it will then be reset as the round ends!
        // Also populated randomly

        // force all to draw their cards again.
        DrawCards();
    }

    public void RotateCardStandard()
    {
        var _this = this;
        _Units[0].RandomiseSelected();
        _Units[1].RandomiseSelected();
        _Units[2].RandomiseSelected();

        var _playerSelectedCards = _Player.SelectedCards();
        _Player.RemoveSelectedCards();
        var _unitZeroSelectedCards = _Units[0].SelectedCards();
        _Units[0].RemoveSelectedCards();
        var _unitOneSelectedCards = _Units[1].SelectedCards();
        _Units[1].RemoveSelectedCards();
        var _unitTwoSelectedCards = _Units[2].SelectedCards();
        _Units[2].RemoveSelectedCards();

        for(int _i = 0; _i < _playerSelectedCards.Count; _i++)
        {
            var _card = _playerSelectedCards[_i];
            _Units[0].AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitZeroSelectedCards.Count; _i++)
        {
            var _card = _unitZeroSelectedCards[_i];
            _Player.AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitOneSelectedCards.Count; _i++)
        {
            var _card = _unitOneSelectedCards[_i];
            _Units[2].AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitTwoSelectedCards.Count; _i++)
        {
            var _card = _unitTwoSelectedCards[_i];
            _Units[1].AddCard(_card, ref _this);
        }
    }

    public bool IsAllowedToRingBell()
    {
        if (GameState == GameState.Menu)
            return true;

        if(GameState == GameState.TutorialCardSelect && _Player.SelectedCardCount() != 3)
        {
            _UIHandler.ShowInsult();
            //GameSettings.Conductor.PlaySound(GameSound.Grunt);
            return false;
        }

        if (GameState == GameState.TutorialCardSelect && _Player.SelectedCardCount() == 3)
            return true;

        if (GameState == GameState.TutorialStart && IsHoldersFull())
            return true;

        return false;
    }

    private bool IsHoldersFull()
    {
        return _SwordCardHolder.ParentCard != null && _ShieldCardHolder.ParentCard != null && _MagicCardHolder.ParentCard != null;
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
            var _random = Random.Range(25, 75);
            var _unit = _Units[_i];
            _unit.AddTreats(_random);
        }
    }
}
