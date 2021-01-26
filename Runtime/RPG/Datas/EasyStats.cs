using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BaseTool.RPG.Datas
{
    [Serializable]
    public class EasyStats : Stats
    {
        [SerializeField]
        private Dictionary<string, string> _datas;
    }
}
