using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayCard : Card
{
    [SerializeField] private SpriteRenderer _FlipSideSpriteRenderer;
    public bool IsFlipside;
    public float Transparency;

    protected override void Start()
    {
        base.Start();
        _IsInteractable = false;
    }

    public override void RawPopulate()
    {
        base.RawPopulate();
        _FlipSideSpriteRenderer.enabled = IsFlipside;
    }

    public override void Show()
    {
        if(_GameController.GameState == GameState.TutorialRound)
        {
            _SpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
            _SwordSpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
            _ShieldSpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
            _MagicSpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
        }
        else
        {
            _SpriteRenderer.material = GameSettings.GameFactory.LitMaterial;
            _SwordSpriteRenderer.material = GameSettings.GameFactory.LitMaterial;
            _ShieldSpriteRenderer.material = GameSettings.GameFactory.LitMaterial;
            _MagicSpriteRenderer.material = GameSettings.GameFactory.LitMaterial;
        }
        PopulateCard();
        _SpriteRenderer.enabled = true;
        _SwordSpriteRenderer.enabled = true;
        _ShieldSpriteRenderer.enabled = true;
        _MagicSpriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }
}
