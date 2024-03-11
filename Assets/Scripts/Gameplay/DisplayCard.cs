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
        PopulateCard();
        _SpriteRenderer.enabled = true;
        _SwordSpriteRenderer.enabled = true;
        _ShieldSpriteRenderer.enabled = true;
        _MagicSpriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }
}
