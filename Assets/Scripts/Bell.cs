using UnityEngine;
using UnityEngine.EventSystems;

public class Bell : MonoBehaviour
{
    [SerializeField] private GameController _GameController;
    [SerializeField] private Animator _Animator;

    public void OnMouseDown()
    {
        if(_GameController.GameState == GameState.Menu)
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
}
