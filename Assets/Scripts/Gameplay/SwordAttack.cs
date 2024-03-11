using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    public void TriggerAttack()
    {
        _Animator.SetTrigger("Attack");
    }
}
