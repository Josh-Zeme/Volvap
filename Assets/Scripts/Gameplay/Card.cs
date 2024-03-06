using UnityEngine;

public enum CardBoonType
{
    None = 0, Multipy = 1, LoseACard = 2, Reflect = 3, Zero = 4
}

public class CardData
{
    public Unit Owner;
    public int Damage;
    public int Shield;
    public CardBoonType BoonType;

    public void Randomise()
    {
        //Debug.Log("Randomise the card values");
    }
}

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public CardData CardData = null;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
