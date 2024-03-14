using System.Collections.Generic;
using UnityEngine;

public class Drool : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] SpriteRenderer _SpriteRenderer;

    public void Reset()
    {
        _SpriteRenderer.enabled = false;
    }

    public void TriggerDroolA()
    {
        _SpriteRenderer.enabled = true;
        _Animator.SetTrigger("TriggerA");
    }

    public void TriggerDroolB()
    {
        _SpriteRenderer.enabled = true;
        _Animator.SetTrigger("TriggerB");
    }
}
