using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BaseTool.RPG.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "BaseTool/RPG/Item")]
    public class Item : ScriptableObject
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Title;

        /// <summary>
        /// The maximum of that item that can be stacked in inventory.
        /// </summary>
        public uint MaxStack = 1;

        /// <summary>
        /// The size of the object in the inventory.
        /// </summary>
        public Vector2Int Size = Vector2Int.one;
    }
}
