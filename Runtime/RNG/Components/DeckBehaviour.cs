using UnityEngine;

namespace BaseTool.RNG
{
    [AddComponentMenu("BaseTool/RNG/Deck Behaviour")]
    public class DeckBehaviour : MonoBehaviour
    {
        [SerializeField] private Deck<GameObject> _deck;
    }
}