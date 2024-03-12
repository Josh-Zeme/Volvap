using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
        None = 0, Menu = 1, TutorialRound = 2
        , FirstRoundA = 3, FirstRoundB = 4, FirstRoundC = 5
        , SecondRoundA = 6, SecondRoundB = 7, SecondRoundC = 8
        , ThirdRoundA = 9, ThirdRoundB = 10, ThirdRoundC = 11
        , BossRoundA = 12, BossRoundB = 13, BossRoundC = 14
        , EndGame = 15, GameOver = 16
}

// This is the state of the current round. Then we can use the "update gamestate at the end of each turn so the turns are easier to sort out and the games are on a loop and logically much easier)
public enum RoundState
{
    None = 0, Draw = 1, Exchange = 2, Select = 3, Attack =4
}

public enum AttackPhase
{
    None = 0, Setup = 1, PlaceCard = 2, Display = 3, SwordAttack = 4, MagicAttack = 5, RandomiseCards = 6, CleanupUnits = 7, DrawCards = 8
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIHandler _UIHandler;
    [SerializeField] private CinemachineVirtualCamera _PreGameCamera;
    [SerializeField] private CinemachineVirtualCamera _InGameCamera;

    [SerializeField] private Aberration _Aberration;
    [SerializeField] private Player _Player;
    [SerializeField] private List<Unit> _Units;
    [SerializeField] private Deck _Deck;
    [SerializeField] private RoofLight _RoofLight;
    [SerializeField] private Clock _Clock;

    [SerializeField] private CardHolder _SwordCardHolder;
    [SerializeField] private CardHolder _ShieldCardHolder;
    [SerializeField] private CardHolder _MagicCardHolder;

    [SerializeField] private List<DisplayCard> PlayerDisplayCards;
    [SerializeField] private List<DisplayCard> Npc1DisplayCards;
    [SerializeField] private List<DisplayCard> Npc2DisplayCards;
    [SerializeField] private List<DisplayCard> Npc3DisplayCards;

    [SerializeField] private List<DisplayCard> RandomDisplayCards;

    // Need to use this so that it checks if shit is going and THEN does stuff.
    // so i probably dont need the flicker check cause it'll always have a flicker check then get out cause it's "Animating"
    // this may need to be a method rather than a variable.
    // see if flicker, see if aberation moving, see if attacking etc!
    private bool _IsGameOver;

    public GameState GameState = GameState.None;
    public RoundState RoundState = RoundState.None;
    public AttackPhase AttackPhase = AttackPhase.None;
    public int Round = 0;
    private int _CurrentRandomCard = 0;

    public void Start()
    {
        TriggerGameState();
        _InGameCamera.Priority = 0;
        _PreGameCamera.Priority = 1;
    }

    public void FixedUpdate()
    {
        if (_Clock.IsWaiting)
            return;

        if (_IsGameOver)
            FinishGameOver();

        switch (GameState)
        {
            case GameState.TutorialRound:
                switch (RoundState)
                {
                    case RoundState.None:
                        if (!_RoofLight.IsFlickering)
                        {
                            RoundState = RoundState.Draw;
                            //_Clock.TriggerDrool();
                            for (int _i = 0; _i < _Units.Count; _i++)
                            {
                                //Start smoking again
                                _Units[_i].TriggerSmoking();
                            }
                            DealCards();
                            TriggerRoundState();
                        }
                        break;
                }
                break;
            case GameState.FirstRoundA:
                break;
            default:
                break;
        }

        // Just so that it always does the attack state rather than shitting itself
        switch (RoundState)
        {
            case RoundState.Attack:
                DoAttackPhase();
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
                GameState = GameState.TutorialRound;
                TriggerPlushies();
                RoundState = RoundState.None;
                //MockTreats();
                // trigger clock move
                for (int _i = 0; _i < _Units.Count; _i++)
                {
                    //Should stop all units from smoking
                    _Units[_i].TriggerSmoking();
                }
                _RoofLight.TriggerForce(new Vector2(500, 0));
                _RoofLight.TriggerFlicker(Color.white,1.5f, new List<float>() { 0.2f, 0.25f, 0.4f, 0.45f, 0.5f, 0.55f, 0.6f, 0.61f });

                //_Clock.TriggerAberration(1.5f, new List<float>() { 0.6f, 0.61f });
                _InGameCamera.Priority = 1;
                _PreGameCamera.Priority = 0;
                break;
            case GameState.TutorialRound:
                TriggerRoundState();
                break;
            case GameState.FirstRoundA:
                TriggerRoundState();
                break;
            case GameState.FirstRoundB:
                TriggerRoundState();
                break;
            case GameState.FirstRoundC:
                TriggerRoundState();
                break;
            default:
                Debug.Log("How the hell did you get here?");
                break;
        }

        // fade the text rather than disable it.
        _UIHandler.TriggerGameState(GameState, RoundState, AttackPhase);
    }

    public void TriggerRoundState()
    {

        switch (RoundState)
        {
            case RoundState.None:
                RoundState = RoundState.Draw;
                break;
            case RoundState.Draw:
                RoundState = RoundState.Exchange;
                break;
            case RoundState.Exchange:
                ExchangeCards();
                RoundState = RoundState.Select;
                for (int _i = 0; _i < _Units.Count; _i++)
                {
                    //Should stop all units from smoking
                    _Units[_i].TriggerSmoking();
                }
                _RoofLight.TriggerForce(new Vector2(450, 0));
                //_RoofLight.TriggerFlicker(0.5f, new List<float>() { 0.1f, 0.11f, 0.15f, 0.20f, 0.3f, 0.35f, 0.4f, 0.45f });
                //_Clock.TriggerAberration(0.5f, new List<float>() { 0.15f, 0.20f });
                break;
            case RoundState.Select:
                RoundState = RoundState.Attack;
                AttackPhase = AttackPhase.Setup;
                break;
            case RoundState.Attack:
                AttackPhaseOver();
                break;
        }
        _UIHandler.TriggerGameState(GameState, RoundState, AttackPhase);
    }

    public void AttackPhaseOver()
    {
        if (GameState == GameState.TutorialRound)
        {
            EndRound();

            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.FirstRoundA;
        }
        else

        // First Round
        if (GameState == GameState.FirstRoundA)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.FirstRoundB;
            StartGameOver();
        }
        else if (GameState == GameState.FirstRoundB)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.FirstRoundC;
        } else if (GameState == GameState.FirstRoundC)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.SecondRoundA;

            CalculateNextDead();
            EndRound();
        }
        else if (GameState == GameState.SecondRoundA)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.SecondRoundB;
        }

        else if (GameState == GameState.SecondRoundB)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.SecondRoundC;
        }

        else if (GameState == GameState.SecondRoundC)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.ThirdRoundA;

            CalculateNextDead();
            EndRound();
        }

        // Third Round
        else if (GameState == GameState.ThirdRoundA)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.ThirdRoundB;
        }

        else if (GameState == GameState.ThirdRoundB)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.ThirdRoundC;
        }

        else if (GameState == GameState.ThirdRoundC)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.BossRoundA;

            CalculateNextDead();
            EndRound();
        }

        // Boss Round

        else if (GameState == GameState.BossRoundA)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.BossRoundB;
        }

        else if (GameState == GameState.BossRoundB)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.BossRoundC;
        }

        else if (GameState == GameState.BossRoundC)
        {
            AttackPhase = AttackPhase.None;
            RoundState = RoundState.Exchange;
            GameState = GameState.EndGame;

            CalculateBossDead();
        }

        TriggerPlushies();
    }

    public void EndRound()
    {
        _Units[0].ClearTreats();
        _Units[1].ClearTreats();
        _Units[2].ClearTreats();
        _Player.ClearTreats();
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
        var _nonDeadUnits = _Units.Where(x => !x.IsDead).ToList();
        for (int _i = 0; _i < _nonDeadUnits.Count; _i++)
        {
            var _unit = _nonDeadUnits[_i];
            GameSettings.Conductor.PlaySound(GameSound.CardDeal);
            var _cardCount = _unit.Cards.Where(x => x.CardData?.Owner == null).Count();
            while (_cardCount != 0)
            {
                _unit.AddCard(_Deck.TakeCard(), ref _this);
                _cardCount = _unit.Cards.Where(x => x.CardData?.Owner == null).Count();
            }
        }
    }

    private void ExchangeCards()
    {
        switch (GameState)
        {
            case GameState.TutorialRound:
            case GameState.FirstRoundA:
            case GameState.FirstRoundB:
            case GameState.FirstRoundC:
            case GameState.SecondRoundA:
            case GameState.SecondRoundB:
            case GameState.SecondRoundC:
            case GameState.ThirdRoundA:
            case GameState.ThirdRoundB:
            case GameState.ThirdRoundC:
                RotateCardRound();
                break;
            case GameState.BossRoundA:
            case GameState.BossRoundB:
            case GameState.BossRoundC:
                RotateCardRoundBoss();
                break;
            default:
                break;
        }
    }

    private void DoAttackPhase()
    {
        switch (AttackPhase)
        {
            case AttackPhase.None:
                Debug.Log("Entered the AttackPhase without forcing setup.");
                break;
            case AttackPhase.Setup:
                AttackPhase = AttackPhase.PlaceCard;
                AIAttackSelect();
                AddPlayerAttackCards();
                _Clock.AddTargetTime(1, 0, 1);
                break;
            case AttackPhase.PlaceCard:
                AttackPhase = AttackPhase.Display;
                _Clock.AddTargetTime(2, 0, 3);
                PutCardsOnTable();
                break;
            case AttackPhase.Display:
                AttackPhase = AttackPhase.SwordAttack;
                _Clock.AddTargetTime(20, 0, 4);
                DisplayTurnValues();
                break;
            case AttackPhase.SwordAttack:
                AttackPhase = AttackPhase.MagicAttack;
                _Clock.AddTargetTime(6, 0, 3);
                SwordAttack();
                break;
            case AttackPhase.MagicAttack:
                AttackPhase = AttackPhase.RandomiseCards;
                _Clock.AddTargetTime(1, 0, 1);
                MagicAttack();
                break;
            case AttackPhase.RandomiseCards:
                ForceHideDisplay();
                AttackPhase = AttackPhase.CleanupUnits;
                _Clock.AddTargetTime(2, 0, 1);
                HideCardsOnTable();
                if (GameState != GameState.TutorialRound)
                {
                    PlaceRandomisedCards();
                }
                break;
            case AttackPhase.CleanupUnits:
                AttackPhase = AttackPhase.DrawCards;
                _Clock.AddTargetTime(2, 0, 1);
                CleanupUnits();
                break;
            case AttackPhase.DrawCards:
                if (GameState == GameState.TutorialRound)
                    _Deck.RefillDeck();
                _Clock.AddTargetTime(2, 0, 1);
                DealCards();
                TriggerGameState();
                break;
        }
        _UIHandler.TriggerGameState(GameState, RoundState, AttackPhase);
    }

    private void DisplayTurnValues()
    {
        _Units[0].CalculateValues();
        _Units[1].CalculateValues();
        _Units[2].CalculateValues();
        _Player.CalculateValues();
    }

    private void ForceHideDisplay()
    {
        _Units[0].ForceHideDisplay();
        _Units[1].ForceHideDisplay();
        _Units[2].ForceHideDisplay();
        _Player.ForceHideDisplay();
    }

    private void AIAttackSelect()
    {
        _Units[0].RandomiseAttackSelect();
        _Units[1].RandomiseAttackSelect();
        _Units[2].RandomiseAttackSelect();
    }

    private void CleanupUnits()
    {
        _Player.DiscardAfterAttack();
        _Units[0].DiscardAfterAttack();
        _Units[1].DiscardAfterAttack();
        _Units[2].DiscardAfterAttack();
    }

    #region Attacks

    private void AddPlayerAttackCards()
    {
        _Player.AddAttackCard(_SwordCardHolder.Card.CardData);
        _Player.AddAttackCard(_ShieldCardHolder.Card.CardData);
        _Player.AddAttackCard(_MagicCardHolder.Card.CardData);
    }

    private void SwordAttack()
    {
        _Units[1].SwordAttack(_Units[0].Sword);
        _Units[2].SwordAttack(_Units[0].Sword);
        _Player.SwordAttack(_Units[0].Sword);

        _Units[0].SwordAttack(_Units[1].Sword);
        _Units[2].SwordAttack(_Units[1].Sword);
        _Player.SwordAttack(_Units[1].Sword);

        _Units[1].SwordAttack(_Units[2].Sword);
        _Units[0].SwordAttack(_Units[2].Sword);
        _Player.SwordAttack(_Units[2].Sword);

        _Units[0].SwordAttack(_Player.Sword);
        _Units[1].SwordAttack(_Player.Sword);
        _Units[2].SwordAttack(_Player.Sword);
    }

    private void MagicAttack()
    {
        // Show unit 1s, plush
        _Units[1].MagicAttack(_Units[0].Magic);
        _Units[2].MagicAttack(_Units[0].Magic);
        _Player.MagicAttack(_Units[0].Magic);

        // show unit 2s, plush
        _Units[0].MagicAttack(_Units[1].Magic);
        _Units[2].MagicAttack(_Units[1].Magic);
        _Player.MagicAttack(_Units[1].Magic);

        // show unit 3s, plush
        _Units[1].MagicAttack(_Units[2].Magic);
        _Units[0].MagicAttack(_Units[2].Magic);
        _Player.MagicAttack(_Units[2].Magic);

        // show player, plush
        _Units[0].MagicAttack(_Player.Magic);
        _Units[1].MagicAttack(_Player.Magic);
        _Units[2].MagicAttack(_Player.Magic);
    }

    #endregion

    private void PutCardsOnTable()
    {
        var _gameController = this;

        for(int _i = 0; _i < _Units[0].AttackCards.Count; _i++)
        {
            Npc1DisplayCards[_i].Generate(_Units[0].AttackCards[_i], ref _gameController);
            Npc1DisplayCards[_i].Show();
        }

        for (int _i = 0; _i < _Units[1].AttackCards.Count; _i++)
        {
            Npc2DisplayCards[_i].Generate(_Units[2].AttackCards[_i], ref _gameController);
            Npc2DisplayCards[_i].Show();
        }

        for (int _i = 0; _i < _Units[2].AttackCards.Count; _i++)
        {
            Npc3DisplayCards[_i].Generate(_Units[2].AttackCards[_i], ref _gameController);
            Npc3DisplayCards[_i].Show();
        }

        for (int _i = 0; _i < _Player.AttackCards.Count; _i++)
        {
            PlayerDisplayCards[_i].Generate(_Player.AttackCards[_i], ref _gameController);
            PlayerDisplayCards[_i].Show();
        }

        _SwordCardHolder.ClearCard();
        _ShieldCardHolder.ClearCard();
        _MagicCardHolder.ClearCard();
    }

    private void HideCardsOnTable()
    {
        for (int _i = 0; _i < Npc1DisplayCards.Count; _i++)
        {
            Npc1DisplayCards[_i].Hide();
        }

        for (int _i = 0; _i < Npc2DisplayCards.Count; _i++)
        {
            Npc2DisplayCards[_i].Hide();
        }

        for (int _i = 0; _i < Npc3DisplayCards.Count; _i++)
        {
            Npc3DisplayCards[_i].Hide();
        }

        for (int _i = 0; _i < PlayerDisplayCards.Count; _i++)
        {
            PlayerDisplayCards[_i].Hide();
        }
    }

    private void PlaceRandomisedCards()
    {
        var _gameController = this;
        
        for (int _i = 0; _i < _Units[0].AttackCards.Count; _i++)
        {
            RandomDisplayCards[_CurrentRandomCard].Generate(_Units[0].AttackCards[_i], ref _gameController);
            RandomDisplayCards[_CurrentRandomCard].Show();
            _CurrentRandomCard++;
        }

        for (int _i = 0; _i < _Units[1].AttackCards.Count; _i++)
        {
            RandomDisplayCards[_CurrentRandomCard].Generate(_Units[2].AttackCards[_i], ref _gameController);
            RandomDisplayCards[_CurrentRandomCard].Show();
            _CurrentRandomCard++;
        }

        for (int _i = 0; _i < _Units[2].AttackCards.Count; _i++)
        {
            RandomDisplayCards[_CurrentRandomCard].Generate(_Units[2].AttackCards[_i], ref _gameController);
            RandomDisplayCards[_CurrentRandomCard].Show();
            _CurrentRandomCard++;
        }

        for (int _i = 0; _i < _Player.AttackCards.Count; _i++)
        {
            RandomDisplayCards[_CurrentRandomCard].Generate(_Player.AttackCards[_i], ref _gameController);
            RandomDisplayCards[_CurrentRandomCard].Show();
            _CurrentRandomCard++;
        }
    }


    #region Card Rotate Region
    public void RotateCardRound()
    {
        var _this = this;
        var _chickenUnit = _Units.FirstOrDefault(x => x.IsChicken);
        var _ballUnitOne = _Units.FirstOrDefault(x => x.IsBall);
        var _ballUnitTwo = _Units.LastOrDefault(x => x.IsBall);

        if(_chickenUnit != null)
        {
            _chickenUnit.RandomiseSelected();
            var _playerSelectedCards = _Player.SelectedCards();
            _Player.RemoveSelectedCards();
            var _unitChickenSelectedCards = _chickenUnit.SelectedCards();
            _chickenUnit.RemoveSelectedCards();
            for (int _i = 0; _i < _playerSelectedCards.Count; _i++)
            {
                var _card = _playerSelectedCards[_i];
                _chickenUnit.AddCard(_card, ref _this);
            }

            for (int _i = 0; _i < _unitChickenSelectedCards.Count; _i++)
            {
                var _card = _unitChickenSelectedCards[_i];
                _Player.AddCard(_card, ref _this);
            }
        }

        if(_ballUnitTwo != null)
        {
            _ballUnitOne.RandomiseSelected();
            _ballUnitTwo.RandomiseSelected();


            var _unitOneSelectedCards = _ballUnitOne.SelectedCards();
            _ballUnitOne.RemoveSelectedCards();
            var _unitTwoSelectedCards = _ballUnitTwo.SelectedCards();
            _ballUnitTwo.RemoveSelectedCards();


            for (int _i = 0; _i < _unitOneSelectedCards.Count; _i++)
            {
                var _card = _unitOneSelectedCards[_i];
                _ballUnitTwo.AddCard(_card, ref _this);
            }

            for (int _i = 0; _i < _unitTwoSelectedCards.Count; _i++)
            {
                var _card = _unitTwoSelectedCards[_i];
                _ballUnitOne.AddCard(_card, ref _this);
            }
        }
    }

    public void RotateCardRoundBoss()
    {
        var _this = this;
        var _chickenUnit = _Units.FirstOrDefault(x => x.IsChicken);
        var _ballUnitOne = _Units.FirstOrDefault(x => x.IsBall);
        var _ballUnitTwo = _Units.LastOrDefault(x => x.IsBall);

        _chickenUnit.RandomiseSelected();
        _ballUnitTwo.RandomiseSelected();
        _ballUnitTwo.RandomiseSelected();

        var _playerSelectedCards = _Player.SelectedCards();
        _Player.RemoveSelectedCards();
        var _unitChickenSelectedCards = _chickenUnit.SelectedCards();
        _chickenUnit.RemoveSelectedCards();
        var _unitOneSelectedCards = _chickenUnit.SelectedCards();
        _chickenUnit.RemoveSelectedCards();
        var _unitTwoSelectedCards = _ballUnitTwo.SelectedCards();
        _ballUnitTwo.RemoveSelectedCards();

        for (int _i = 0; _i < _playerSelectedCards.Count; _i++)
        {
            var _card = _playerSelectedCards[_i];
            _chickenUnit.AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitChickenSelectedCards.Count; _i++)
        {
            var _card = _unitChickenSelectedCards[_i];
            _Player.AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitOneSelectedCards.Count; _i++)
        {
            var _card = _unitOneSelectedCards[_i];
            _ballUnitTwo.AddCard(_card, ref _this);
        }

        for (int _i = 0; _i < _unitTwoSelectedCards.Count; _i++)
        {
            var _card = _unitTwoSelectedCards[_i];
            _ballUnitTwo.AddCard(_card, ref _this);
        }
    }

    #endregion

    public bool IsAllowedToRingBell()
    {
        if (GameState == GameState.Menu)
            return true;

        if(RoundState == RoundState.Exchange && GameState == GameState.TutorialRound && _Player.SelectedCardCount() != 3)
        {
            _UIHandler.ShowInsult();
        }

        if (RoundState == RoundState.Exchange && _Player.SelectedCardCount() != 3)
        {
            return false;
        }

        if (RoundState == RoundState.Exchange && _Player.SelectedCardCount() == 3)
            return true;

        if (RoundState == RoundState.Select && IsHoldersFull())
            return true;

        return false;
    }

    private bool IsHoldersFull()
    {
        return _SwordCardHolder.ParentCard != null && _ShieldCardHolder.ParentCard != null && _MagicCardHolder.ParentCard != null;
    }

    private void CalculateNextDead()
    {
        var _topUnit = _Units.Where(x => !x.IsDead).OrderBy(x => x.Treats).FirstOrDefault();
        if(_Player.Treats > _topUnit.Treats)
        {
            StartGameOver();
        }
        else
        {
            _topUnit.Kill();
        }
    }

    private void StartGameOver()
    {
        EndRound();

        _RoofLight.TriggerForce(new Vector2(-500, 0));
        _RoofLight.TriggerFlicker(Color.blue, 3f, new List<float>() { 0.2f, 0.25f, 0.4f, 0.45f, 0.5f, 0.55f, 0.6f, 0.61f });
        _Clock.AddTargetTime(0, -2, -60);
        _IsGameOver = true;

    }

    private void FinishGameOver() {
        _IsGameOver = false;
        _CurrentRandomCard = 0;
        for (int _i = 0; _i < RandomDisplayCards.Count; _i++)
        {
            RandomDisplayCards[_i].Reset();
            RandomDisplayCards[_i].Hide();
        }

        for (int _i = 0; _i < _Units.Count; _i++)
        {
            var _unit = _Units[_i];
            _unit.Reset();
        }
        if (_Aberration != null)
        {
            _Aberration.Reset();
        }
        _Deck.RefillDeck();
        DealCards();
        GameState = GameState.FirstRoundA;

        Debug.Log("Show blood");
        Debug.Log("Make noise");
        Debug.Log("Make spook");
        _UIHandler.EnableGameOver();
    }

    private void CalculateBossDead()
    {
        Debug.Log("Trigger Credits!");
    }

    private void TriggerPlushies()
    {
        // Player is always chicken.. never ball!
        _Player.TriggerChicken(true);
        _Player.TriggerBall(false);
        switch (GameState)
        {
            case GameState.TutorialRound:
            case GameState.FirstRoundA:
                _Units[0].TriggerChicken(true);
                _Units[1].TriggerChicken(false);
                _Units[2].TriggerChicken(false);
                _Units[0].TriggerBall(false);
                _Units[1].TriggerBall(true);
                _Units[2].TriggerBall(true);
                break;
            case GameState.FirstRoundB:
                // Rotate Left
                _Units[0].TriggerChicken(true);
                _Units[1].TriggerChicken(false);
                _Units[2].TriggerChicken(true);

                _Units[0].TriggerBall(false);
                _Units[1].TriggerBall(true);
                _Units[2].TriggerBall(false);
                break;
            case GameState.FirstRoundC:
                // Rotate Right
                _Units[0].TriggerChicken(true);
                _Units[1].TriggerChicken(false);
                _Units[2].TriggerChicken(true);

                _Units[0].TriggerBall(false);
                _Units[1].TriggerBall(true);
                _Units[2].TriggerBall(false);
                break;
            case GameState.SecondRoundA:
                if (_Units[0].IsDead)
                {
                    _Units[1].TriggerChicken(true);
                    _Units[1].TriggerBall(false);
                    _Units[2].TriggerChicken(false);
                    _Units[2].TriggerBall(true);
                }
                if (_Units[1].IsDead)
                {
                    _Units[0].TriggerChicken(true);
                    _Units[0].TriggerBall(false);
                    _Units[2].TriggerChicken(false);
                    _Units[2].TriggerBall(true);
                }
                if (_Units[2].IsDead)
                {
                    _Units[0].TriggerChicken(true);
                    _Units[0].TriggerBall(false);
                    _Units[1].TriggerChicken(false);
                    _Units[1].TriggerBall(true);
                }
                break;
            case GameState.SecondRoundB:
                if (_Units[0].IsDead)
                {
                    _Units[1].TriggerChicken(false);
                    _Units[1].TriggerBall(true);
                    _Units[2].TriggerChicken(true);
                    _Units[2].TriggerBall(false);
                }
                if (_Units[1].IsDead)
                {
                    _Units[0].TriggerChicken(false);
                    _Units[0].TriggerBall(true);
                    _Units[2].TriggerChicken(true);
                    _Units[2].TriggerBall(false);
                }
                if (_Units[2].IsDead)
                {
                    _Units[0].TriggerChicken(false);
                    _Units[0].TriggerBall(true);
                    _Units[1].TriggerChicken(true);
                    _Units[1].TriggerBall(false);
                }
                break;
            case GameState.SecondRoundC:
                if (_Units[0].IsDead)
                {
                    _Units[1].TriggerChicken(false);
                    _Units[1].TriggerBall(true);
                    _Units[2].TriggerChicken(false);
                    _Units[2].TriggerBall(true);
                }
                if (_Units[1].IsDead)
                {
                    _Units[0].TriggerChicken(false);
                    _Units[0].TriggerBall(true);
                    _Units[2].TriggerChicken(false);
                    _Units[2].TriggerBall(true);
                }
                if (_Units[2].IsDead)
                {
                    _Units[0].TriggerChicken(false);
                    _Units[0].TriggerBall(true);
                    _Units[1].TriggerChicken(false);
                    _Units[1].TriggerBall(true);
                }
                break;
            case GameState.ThirdRoundA:
            case GameState.ThirdRoundB:
            case GameState.ThirdRoundC:
                if (_Units[0].IsDead && _Units[1].IsDead)
                {
                    _Units[2].TriggerChicken(true);
                    _Units[2].TriggerBall(false);
                }
                if (_Units[0].IsDead && _Units[2].IsDead)
                {
                    _Units[1].TriggerChicken(true);
                    _Units[1].TriggerBall(false);
                }
                if (_Units[1].IsDead && _Units[2].IsDead)
                {
                    _Units[0].TriggerChicken(true);
                    _Units[0].TriggerBall(false);
                }
                break;
            case GameState.BossRoundA:
                Debug.Log("Haven't done the boss plush yet!");
                break;
            case GameState.BossRoundB:
                Debug.Log("Haven't done the boss plush yet!");
                break;
            case GameState.BossRoundC:
                Debug.Log("Haven't done the boss plush yet!");
                break;
            default:
                break;
        }
    }
}
