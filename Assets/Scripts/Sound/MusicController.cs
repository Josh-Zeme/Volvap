using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSong
{
    None = 0, MainTheme = 1
}

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private float _CurrentVolume;
    private GameSong _CurrentSong;
    private AudioSource _AudioSource;

    public void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _CurrentVolume = GameSettings.MusicVolume;
        _CurrentSong = GameSong.None;
        UpdateSong(GameSong.None);
    }

    public void PauseSong()
    {
        if (_AudioSource.isPlaying)
        {
            _AudioSource.Pause();
        }
    }

    public void ResumeSong()
    {
        if (!_AudioSource.isPlaying)
        {
            _AudioSource.UnPause();
        }
    }

    public void UpdateSong(GameSong gameSong)
    {
        if (gameSong == GameSong.None)
        {
            _AudioSource.Stop();
            return;
        }

        if (_CurrentSong == gameSong)
            return;

        var _clip = GetSongs(gameSong);

        // Don't try and play songs that we haven't done!
        if (_clip == null)
            return;

        _CurrentSong = gameSong;

        _AudioSource.clip = _clip;
        _AudioSource.loop = true;
        _AudioSource.Play();
    }


    public void TriggerMute(bool isMute)
    {
        _CurrentVolume = isMute ? 0 : GameSettings.MusicVolume;

        _AudioSource.volume = _CurrentVolume;
    }

    public AudioClip GetSongs(GameSong gameSong)
    {
        switch (gameSong)
        {
            case GameSong.MainTheme:
                return GameSettings.GameFactory.MusicFactory.MainThemeFirst;
        }

        return null;
    }
}
