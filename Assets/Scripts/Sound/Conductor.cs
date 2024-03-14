using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private SoundController _SoundController;
    [SerializeField] private MusicController _MusicController;

    public static Conductor Instance;

    private void Awake()
    {
        Instance = this;

        Conductor[] objs = FindObjectsOfType<Conductor>();

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(GameSound gameSound)
    {
        _SoundController.Play(gameSound);
    }

    public void PauseSong()
    {
        _MusicController.PauseSong();
    }

    public void ResumeSong()
    {
        _MusicController.ResumeSong();
    }

    public void PlaySong(GameSong gameSong)
    {
        _MusicController.UpdateSong(gameSong);
    }

    public void TriggerMute(bool isMuted)
    {
        _MusicController.TriggerMute(isMuted);
    }

    public void RemoveTyrellA()
    {
        _MusicController.RemoveTyrellA();
    }

    public void RemoveTyrellB()
    {
        _MusicController.RemoveTyrellB();
    }

    public void Reset()
    {
        _MusicController.Reset();
    }
}
