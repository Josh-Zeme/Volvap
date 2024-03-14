using UnityEngine;

public enum GameSong
{
    None = 0, MainTheme = 1
}

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _TyrellA;
    [SerializeField] private AudioSource _TyrellB;
    private bool _IsMuted;
    private float _CurrentVolume;
    private GameSong _CurrentSong;

    public void Start()
    {
        _CurrentVolume = GameSettings.MusicVolume;
        _CurrentSong = GameSong.None;
        UpdateSong(GameSong.None);
    }

    public void PauseSong()
    {

        if (_TyrellA.isPlaying)
        {
            _TyrellA.Pause();
        }

        if (_TyrellB.isPlaying)
        {
            _TyrellB.Pause();
        }
    }

    public void ResumeSong()
    {
        if (_TyrellA.isPlaying)
        {
            _TyrellA.UnPause();
        }

        if (_TyrellB.isPlaying)
        {
            _TyrellB.UnPause();
        }
    }

    public void UpdateSong(GameSong gameSong)
    {
        if (gameSong == GameSong.None)
        {
            _TyrellA.Stop();
            _TyrellB.Stop();
            return;
        }

        if (_CurrentSong == gameSong)
            return;

        _CurrentSong = gameSong;
        _TyrellA.loop = true;
        _TyrellA.Play();
        _TyrellB.loop = true;
        _TyrellB.Play();
    }


    public void TriggerMute(bool isMute)
    {
        _IsMuted = isMute;
        _CurrentVolume = _IsMuted ? 0 : GameSettings.MusicVolume;

        _TyrellA.volume = _CurrentVolume;
        _TyrellB.volume = _CurrentVolume;
    }

    public void RemoveTyrellA()
    {
        _TyrellA.Stop();
    }

    public void RemoveTyrellB()
    {
        _TyrellB.Stop();
    }

    public void Reset()
    {
        _CurrentVolume = _IsMuted ? 0 : GameSettings.MusicVolume;
        TriggerMute(_IsMuted);
        UpdateSong(GameSong.None);
        UpdateSong(GameSong.MainTheme);
    }
}
