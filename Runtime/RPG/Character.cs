using BaseTool.RPG.Datas;
using BaseTool.RPG.Items;
using UnityEngine;

namespace BaseTool.RPG
{
    [CreateAssetMenu(fileName = "New Character", menuName = "BaseTool/RPG/Character")]
    public class Character : ScriptableObject
    {
        public string Nickname;

        [SerializeField]
        public Inventory<ItemInventory> Inventory;

        public EasyStats Stats;
    }
}
