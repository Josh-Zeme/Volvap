using UnityEngine;
using UnityEngine.EventSystems;

public class Bell : MonoBehaviour
{
    [SerializeField] private GameController _GameController;
    [SerializeField] private Animator _Animator;

    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private Material _Unlit;
    [SerializeField] private Material _Lit;

    public void OnMouseDown()
    {
        if (_GameController.IsAllowedToRingBell())
        {
            RingStandard();
            _GameController.TriggerGameState();
        }
    }

    public void RingStandard()
    {
        Debug.Log("Bell dinged");
        _Animator.SetTrigger("TriggerBell");
        GameSettings.Conductor.PlaySound(GameSound.HitBell);
    }

    public void MakeLit()
    {
        _SpriteRenderer.material = _Lit;
    }

    public void MakeUnlit()
    {
        _SpriteRenderer.material = _Unlit;
    }
}
