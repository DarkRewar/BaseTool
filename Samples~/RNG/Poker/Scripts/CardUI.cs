using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseTool.Samples.RNG.Poker
{
    public class CardUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Card _card;

        public Card Card => _card;

        private bool _isSelected;
        
        public delegate void CardClicked(CardUI cardUI);
        public event CardClicked OnClicked;
        
        public static Color SelectedColor = new Color(1f, 1f, 1f, 0.5f);
        public static Color DisabledColor = new Color(1f, 1f, 1f, 0.2f);

        public void SetCard(Card card)
        {
            _card = card;
            _image.sprite = card.Sprite;
        }

        public void SetEnable(bool enable)
        {
            _image.color = enable ? Color.white : DisabledColor;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isSelected = !_isSelected;
            _image.color = _isSelected ? SelectedColor : Color.white;
            OnClicked?.Invoke(this);
        }
    }
}