using UnityEngine;

namespace BaseTool.UI
{
    public class BackBehaviour : MonoBehaviour
    {
        [SerializeField] private KeyCode Key = KeyCode.Escape;

        void Update()
        {
            if (Input.GetKeyDown(Key))
                Navigation.Back();
        }
    }
}
