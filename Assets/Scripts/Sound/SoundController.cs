using UnityEngine;

public enum GameSound
{
    None = 0, HitBell = 1
}

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private AudioSource _AudioSource;

    public void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    public void Play(GameSound gameSound)
    {
        var _isPlaySound = true;

        AudioClip _audioClip = null;

        switch (gameSound)
        {
            case GameSound.HitBell:
                _audioClip = GameSettings.GameFactory.SoundFactory.UISounds.GetHitBell();
                break;
            default:
                PlayNothing(gameSound);
                _isPlaySound = false;
                break;
        }

        if (_isPlaySound && _audioClip != null)
        {
            PlayGlobalAudioClip(_audioClip);
        }
    }

    private void PlayNothing(GameSound gameSound)
    {
        // Deliberately Nothing
        Debug.Log($"Nothing was when the following sound was called: {gameSound}");
    }

    public void PlayGlobalAudioClip(AudioClip clip)
    {
        _AudioSource.PlayOneShot(clip, GameSettings.SoundVolume);
    }
}
