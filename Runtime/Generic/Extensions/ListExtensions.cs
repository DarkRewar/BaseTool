using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BaseTool.Generic.Extentions
{
    public static class ListExtensions 
    {
        public static T GetRandom<T>(this List<T> list)
        {
            int random = Random.Range(0, list.Count);
            return list[random];
        }

        public static T ExtractRandom<T>(this List<T> list)
        {
            int random = Random.Range(0, list.Count);
            T val = list[random];
            list.RemoveAt(random);
            return val;
        }
    }
}