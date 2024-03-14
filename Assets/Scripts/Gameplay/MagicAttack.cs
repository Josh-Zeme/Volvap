using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private Animator _Animator;

    public void TriggerAttack(GameState gameState)
    {
        if (gameState == GameState.TutorialRound)
        {
            _SpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
        }
        else
        {
            _SpriteRenderer.material = GameSettings.GameFactory.LitMaterial;
        }

        _Animator.SetTrigger("Attack");
    }
}
