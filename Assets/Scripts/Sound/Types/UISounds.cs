using UnityEngine;

public class UISounds : MonoBehaviour
{
    #region SoundClips

    [SerializeField] private AudioClip _HitBellOne;
    [SerializeField] private AudioClip _HitBellTwo;
    [SerializeField] private AudioClip _HitBellThree;
    [SerializeField] private AudioClip _HitBellFour;

    #endregion

    public AudioClip GetHitBell()
    {
        var _number = Random.Range(0, 4);
        return _number switch
        {
            1 => _HitBellTwo,
            2 => _HitBellThree,
            3 => _HitBellFour,
            _ => _HitBellOne,
        };
    }
}