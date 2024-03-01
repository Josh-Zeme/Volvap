using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0, Menu = 1, TutorialRound = 2,
}

public class GameController : MonoBehaviour
{
    public GameState GameState = GameState.None;

}
