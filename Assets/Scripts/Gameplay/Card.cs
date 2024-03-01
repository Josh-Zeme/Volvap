using UnityEngine;

public enum CardBoonType
{
    None = 0, Multipy = 1, LoseACard = 2, Reflect = 3, Zero = 4
}

public class Card : MonoBehaviour
{
    public Unit Owner;
    public int Damage;
    public int Shield;
    public CardBoonType BoonType;

    public void Randomise()
    {
        Debug.Log("Randomise the card values");
    }
}
