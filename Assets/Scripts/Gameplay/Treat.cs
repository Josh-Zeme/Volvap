using UnityEngine;

public class Treat : MonoBehaviour
{
    [SerializeField] SpriteRenderer _SpriteRenderer;

    public void Unlit()
    {
        _SpriteRenderer.material = GameSettings.GameFactory.UnlitMaterial;
    }
}
