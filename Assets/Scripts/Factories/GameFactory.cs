using UnityEngine;

public class GameFactory : MonoBehaviour
{
    [SerializeField] public SoundFactory SoundFactory;
    [SerializeField] public MusicFactory MusicFactory;
    [SerializeField] public Card BaseCard;

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
