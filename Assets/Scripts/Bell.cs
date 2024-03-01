using UnityEngine;
using UnityEngine.EventSystems;

public class Bell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameController _GameController;
    [SerializeField] private Animator _Animator;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_GameController.GameState == GameState.Menu)
        {
            RingStandard();
            _GameController.UpdateGameState();
        }
        
    }

    public void RingStandard()
    {
        Debug.Log("Bell dinged");
        _Animator.SetTrigger("TriggerBell");
        GameSettings.Conductor.PlaySound(GameSound.HitBell);
    }
}
