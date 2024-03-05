using System.Collections.Generic;
using UnityEngine;

public class Drool : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] SpriteRenderer _SpriteRenderer;

    public void HideDrool()
    {
        _SpriteRenderer.enabled = false;
    }

    public void TriggerDrool()
    {
        _SpriteRenderer.enabled = true;
        _Animator.SetTrigger("TriggerDrool");
    }
}
