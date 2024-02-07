using UnityEngine;

namespace BaseTool.Generic.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Change la valeur X d'un vecteur3
        /// </summary>
        public static Vector3 ChangeX(this Vector3 value, float x)
        {
            value.x = x;
            return value;
        }

        /// <summary>
        /// Change la valeur Y d'un vecteur3
        /// </summary>
        public static Vector3 ChangeY(this Vector3 value, float y)
        {
            value.y = y;
            return value;
        }

        /// <summary>
        /// Change la valeur Z d'un vecteur3
        /// </summary>
        public static Vector3 ChangeZ(this Vector3 value, float z)
        {
            value.z = z;
            return value;
        }

        /// <summary>
        /// Clamp un à un les axes X, Y et Z par les axes des vectors begin et end.
        /// Cela permet à un vector3 d'être limité à un cube déterminé par les points begin et end
        /// </summary>
        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );
        }

        /// <summary>
        /// Fait un Lerp sur les trois axes du vector, indépendamment les uns des autres
        /// Cela permet notamment de faire des minimaps en répliquant des positions réels sur un écran
        /// </summary>
        public static Vector3 Lerp(this Vector3 ratio, Vector3 begin, Vector3 end)
        {
            return new Vector3(
                Mathf.Lerp(begin.x, end.x, ratio.x),
                Mathf.Lerp(begin.y, end.y, ratio.y),
                Mathf.Lerp(begin.z, end.z, ratio.z)
            );
        }

        /// <summary>
        /// Fait un InverseLerp sur les trois axes du vector, indépendamment les uns des autres
        /// Cela permet de pouvoir reporter ces valeurs sur une minimap par exemple
        /// </summary>
        public static Vector3 InverseLerp(this Vector3 value, Vector3 begin, Vector3 end)
        {
            return new Vector3(
                Mathf.InverseLerp(begin.x, end.x, value.x),
                Mathf.InverseLerp(begin.y, end.y, value.y),
                Mathf.InverseLerp(begin.z, end.z, value.z)
            );
        }

        /// <summary>
        /// Renvoie le point de la droite qui est le plus proche de la position passée en paramètre
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3 GetClosestPointOnVector(this Vector3 pos, Vector3 begin, Vector3 end)
        {
            float tempDistance = pos.RatioOnVector3(begin, end);

            Vector3 tempClosestPoint;
            Vector3 tempBeginToEnd = end - begin;

            if (tempDistance <= 0)
            {
                tempClosestPoint = begin;
            }
            else if (tempDistance >= 1)
            {
                tempClosestPoint = end;
            }
            else
            {
                tempClosestPoint = begin + tempBeginToEnd * tempDistance;
            }
            return tempClosestPoint;
        }

        /// <summary>
        /// Renvoi le ratio en float d'un point sur un vector3
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float RatioOnVector3(this Vector3 pos, Vector3 begin, Vector3 end)
        {
            Vector3 tempCenter = pos;

            Vector3 tempBeginToCenter = tempCenter - begin;
            Vector3 tempBeginToEnd = end - begin;

            float tempMagnitudeBeginEnd = tempBeginToEnd.sqrMagnitude;
            float tempDotProduct = Vector3.Dot(tempBeginToCenter, tempBeginToEnd);
            float tempDistance = tempDotProduct / tempMagnitudeBeginEnd;

            return tempDistance;
        }
    }
}