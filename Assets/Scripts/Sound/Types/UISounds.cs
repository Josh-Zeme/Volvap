using UnityEngine;

public class UISounds : MonoBehaviour
{
    #region SoundClips

    [SerializeField] private AudioClip _HitBellOne;
    [SerializeField] private AudioClip _HitBellTwo;
    [SerializeField] private AudioClip _HitBellThree;
    [SerializeField] private AudioClip _HitBellFour;


    [SerializeField] private AudioClip _GruntOne;
    [SerializeField] private AudioClip _GruntTwo;
    
    [SerializeField] private AudioClip _CardDealOne;
    

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

    public AudioClip GetGrunt()
    {
        var _number = Random.Range(0, 4);
        return _number switch
        {
            1 => _GruntTwo,
            _ => _GruntOne,
        };
    }

    public AudioClip GetCardDeal()
    {
        var _number = Random.Range(0, 4);
        return _number switch
        {
            _ => _CardDealOne,
        };
    }
}