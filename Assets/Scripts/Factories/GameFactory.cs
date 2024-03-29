using UnityEngine;

public class GameFactory : MonoBehaviour
{
    [SerializeField] public Material UnlitMaterial;
    [SerializeField] public Material LitMaterial;

    [SerializeField] public SoundFactory SoundFactory;
    [SerializeField] public MusicFactory MusicFactory;
    [SerializeField] public Card BaseCard;
    [SerializeField] public Sprite RedPlayerCard;
    [SerializeField] public Sprite YellowPlayerCard;
    [SerializeField] public Sprite BluePlayerCard;
    [SerializeField] public Sprite BlackPlayerCard;

    [SerializeField] public Sprite RedZero;
    [SerializeField] public Sprite RedOne;
    [SerializeField] public Sprite RedTwo;
    [SerializeField] public Sprite RedThree;
    [SerializeField] public Sprite RedFour;
    [SerializeField] public Sprite RedFive;
    [SerializeField] public Sprite RedSix;
    [SerializeField] public Sprite RedSeven;
    [SerializeField] public Sprite RedEight;
    [SerializeField] public Sprite RedNine;
    [SerializeField] public Sprite RedTen;

    [SerializeField] public Sprite BlackZero;
    [SerializeField] public Sprite BlackOne;
    [SerializeField] public Sprite BlackTwo;
    [SerializeField] public Sprite BlackThree;
    [SerializeField] public Sprite BlackFour;
    [SerializeField] public Sprite BlackFive;
    [SerializeField] public Sprite BlackSix;
    [SerializeField] public Sprite BlackSeven;
    [SerializeField] public Sprite BlackEight;
    [SerializeField] public Sprite BlackNine;
    [SerializeField] public Sprite BlackTen;

    [SerializeField] public Sprite YellowZero;
    [SerializeField] public Sprite YellowOne;
    [SerializeField] public Sprite YellowTwo;
    [SerializeField] public Sprite YellowThree;
    [SerializeField] public Sprite YellowFour;
    [SerializeField] public Sprite YellowFive;
    [SerializeField] public Sprite YellowSix;
    [SerializeField] public Sprite YellowSeven;
    [SerializeField] public Sprite YellowEight;
    [SerializeField] public Sprite YellowNine;
    [SerializeField] public Sprite YellowTen;

    [SerializeField] public Sprite BlueZero;
    [SerializeField] public Sprite BlueOne;
    [SerializeField] public Sprite BlueTwo;
    [SerializeField] public Sprite BlueThree;
    [SerializeField] public Sprite BlueFour;
    [SerializeField] public Sprite BlueFive;
    [SerializeField] public Sprite BlueSix;
    [SerializeField] public Sprite BlueSeven;
    [SerializeField] public Sprite BlueEight;
    [SerializeField] public Sprite BlueNine;
    [SerializeField] public Sprite BlueTen;

    [SerializeField] public Sprite BlueSword;
    [SerializeField] public Sprite RedSword;
    [SerializeField] public Sprite BlackSword;
    [SerializeField] public Sprite YellowSword;


    [SerializeField] public Sprite BlueShield;
    [SerializeField] public Sprite RedShield;
    [SerializeField] public Sprite BlackShield;
    [SerializeField] public Sprite YellowShield;

    [SerializeField] public Sprite BlueMagic;
    [SerializeField] public Sprite RedMagic;
    [SerializeField] public Sprite BlackMagic;
    [SerializeField] public Sprite YellowMagic;

    [SerializeField] public Sprite BlueTwoTimes;
    [SerializeField] public Sprite BlueThreeTimes;

    [SerializeField] public Sprite YellowTwoTimes;
    [SerializeField] public Sprite YellowThreeTimes;

    [SerializeField] public Sprite BlackTwoTimes;
    [SerializeField] public Sprite BlackThreeTimes;

    [SerializeField] public Sprite RedTwoTimes;
    [SerializeField] public Sprite RedThreeTimes;


    void Awake()
    {
        GameFactory[] objs = FindObjectsOfType<GameFactory>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        //UnityEngine.Cursor.visible = false;

        DontDestroyOnLoad(gameObject);
    }
}
