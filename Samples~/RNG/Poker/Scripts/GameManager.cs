using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BaseTool.Samples.RNG.Poker
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CardUI _cardPrefab;
        [SerializeField] private RectTransform _handTransform;
        [SerializeField] private DeckBehaviour _deckBehaviour;
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Text _handResultText;

        private List<CardUI> _cardsToDiscard = new();
        private List<CardUI> _hand = new();

        void Start()
        {
            _endTurnButton.onClick.AddListener(EndTurn);
            DrawCards();
        }

        private void DrawCards(int count = 5)
        {
            foreach (int i in ..count)
            {
                DrawCard();
            }
        }

        private void DrawCard()
        {
            var cardUI = Instantiate(_cardPrefab, _handTransform);
            cardUI.SetCard(_deckBehaviour.Draw());
            cardUI.OnClicked += FlagToDiscard;
            _hand.Add(cardUI);
        }

        private void FlagToDiscard(CardUI cardUI)
        {
            if (_cardsToDiscard.Contains(cardUI))
                _cardsToDiscard.Remove(cardUI);
            else
                _cardsToDiscard.Add(cardUI);
        }

        private void EndTurn()
        {
            StartCoroutine(DoEndTurn());
        }

        private IEnumerator DoEndTurn()
        {
            _endTurnButton.gameObject.SetActive(false);

            var cardsToDraw = _cardsToDiscard.Count;
            foreach (var cardUI in _cardsToDiscard)
            {
                _hand.Remove(cardUI);
                Destroy(cardUI.gameObject);
            }

            _cardsToDiscard.Clear();

            DrawCards(cardsToDraw);

            yield return new WaitForSeconds(1f);

            var hand = PokerCalculator.GetHandResult(_hand.Select(h => h.Card).ToList());
            _handResultText.text = hand.Hand.ToString();
            foreach (var cardUI in _hand)
            {
                bool inResult = hand.Cards.Contains(cardUI.Card);
                cardUI.SetEnable(inResult);
                float newY = cardUI.transform.localPosition.y;
                newY += inResult ? 25f : 0;
                cardUI.transform.localPosition = cardUI.transform.localPosition.ChangeY(newY);
            }

            yield return new WaitForSeconds(5f);

            _handTransform.Clear();
            _hand.Clear();
            _handResultText.text = null;

            yield return new WaitForSeconds(1f);
            _deckBehaviour.Fill();
            DrawCards();

            _endTurnButton.gameObject.SetActive(true);
        }
    }
}