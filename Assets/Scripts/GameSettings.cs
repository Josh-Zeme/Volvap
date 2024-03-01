using UnityEngine;

public static class GameSettings
{
    public static GameFactory GameFactory = Object.FindObjectOfType<GameFactory>();
    public static Conductor Conductor = Object.FindObjectOfType<Conductor>();

    #region Volume Region

    public static float SoundVolume = 0.25f;
    public static float MusicVolume = 0.10f;

    #endregion

    #region Game Logic

    public static int CardsPerDeck = 55;
    public static int CardsPerPlayer = 6;
    public static int CardsDiscardedPerTurn = 3;

    #endregion
}
