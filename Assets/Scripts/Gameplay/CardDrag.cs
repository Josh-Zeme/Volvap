using UnityEngine;
using UnityEngine.InputSystem;

public class CardDrag : MonoBehaviour
{
    public static CardDrag Instance;
    [SerializeField] private Camera _Camera;
    [SerializeField] public Card Card;
    [SerializeField] private GameController _GameController;
    public Card PlayerCard;

    public void Start()
    {
        Instance = this;
        Hide();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var _mousePos = Mouse.current.position.ReadValue();
        if (_Camera != null && _mousePos != null)
        {
            var _localMousePos = _Camera.ScreenToWorldPoint(_mousePos);
            _localMousePos.z = transform.position.z;
            transform.position = _localMousePos;
        }
    }

    public void Hide()
    {
        Card.gameObject.SetActive(false);
    }

    public void Show(ref Card card)
    {
        PlayerCard = card;
        var _cardData = new CardData()
        {
            Colour = card.CardData.Colour,
            Shield = card.CardData.Shield,
            Sword = card.CardData.Sword,
            Magic = card.CardData.Magic,
            BoonType = card.CardData.BoonType
        };
        Card.Generate(_cardData, ref _GameController);
        Card.RawPopulate();
        UpdatePosition();
        Card.gameObject.SetActive(true);
    }
}
