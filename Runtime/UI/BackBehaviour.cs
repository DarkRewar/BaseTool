using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTool.UI
{
    public class BackBehaviour : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Navigation.Back();
        }
    }
}
