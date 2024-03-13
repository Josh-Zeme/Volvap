using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public static class GameSettings
{
    public static GameFactory GameFactory = Object.FindObjectOfType<GameFactory>();
    public static Conductor Conductor = Object.FindObjectOfType<Conductor>();

    #region Volume Region

    public static float SoundVolume = 0.25f;
    public static float MusicVolume = 0.10f;

    #endregion

    #region Light Region

    public static float BaseGlobalLight = 0.1f;
    public static float BaseRoofLight = 0.56f;
    public static float TutorialGlobalLight = 0f;
    public static float TutorialRoofLight = 0.1f;
    public static float WinGameGlobalLight = 2500f;

    #endregion

    #region TimeRegion

    public static int HoursInDay = 24;
    public static int MinutesInHour = 60;
    public static float HoursToDegrees = 360 / 12;
    public static float MinutesToDegrees = 360 / 60;
    public static float DayDuration = 9000f;

    #endregion

    #region Game Logic

    public static int CardsPerDeck = 55;
    public static int CardsPerPlayer = 6;
    public static int CardsDiscardedPerTurn = 3;

    #endregion
}
