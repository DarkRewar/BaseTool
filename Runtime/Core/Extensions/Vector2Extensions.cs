using UnityEngine;

namespace BaseTool
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Change the X value of a <see cref="Vector2"/>
        /// and returns it immediately
        /// </summary>
        /// <returns></returns>
        public static Vector2 ChangeX(this Vector2 value, float x)
        {
            value.x = x;
            return value;
        }

        /// <summary>
        /// Change the Y value of a <see cref="Vector2"/>
        /// and returns it immediately
        /// </summary>
        public static Vector2 ChangeY(this Vector2 value, float y)
        {
            value.y = y;
            return value;
        }

        /// <summary>
        /// Clamp each axis of a <see cref="Vector2"/> based on two others
        /// <see cref="Vector2"/> min and max.
        /// </summary>
        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y)
            );
        }

        /// <summary>
        /// Lerp from two <see cref="Vector2"/> using the current one as the ratio.
        /// Will lerp on each axis seperately.
        /// </summary>
        public static Vector2 Lerp(this Vector2 ratio, Vector2 begin, Vector2 end)
        {
            return new Vector2(
                Mathf.Lerp(begin.x, end.x, ratio.x),
                Mathf.Lerp(begin.y, end.y, ratio.y)
            );
        }

        /// <summary>
        /// Inverse lerp from two <see cref="Vector2"/> using the current one as the value.
        /// Will lerp on each axis seperately.
        /// </summary>
        public static Vector2 InverseLerp(this Vector2 value, Vector2 begin, Vector2 end)
        {
            return new Vector2(
                Mathf.InverseLerp(begin.x, end.x, value.x),
                Mathf.InverseLerp(begin.y, end.y, value.y)
            );
        }

        /// <summary>
        /// Calculates and returns the point that is the closest to the line.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector2 GetClosestPointOnVector(this Vector2 pos, Vector2 begin, Vector2 end)
        {
            float tempDistance = pos.RatioOnVector2(begin, end);

            Vector2 tempClosestPoint;
            Vector2 tempBeginToEnd = end - begin;

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
        /// Returns the ratio of the point upon the line.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns>The ratio between 0 and 1</returns>
        public static float RatioOnVector2(this Vector2 pos, Vector2 begin, Vector2 end)
        {
            Vector2 tempCenter = pos;

            Vector2 tempBeginToCenter = tempCenter - begin;
            Vector2 tempBeginToEnd = end - begin;

            float tempMagnitudeBeginEnd = tempBeginToEnd.sqrMagnitude;
            float tempDotProduct = Vector2.Dot(tempBeginToCenter, tempBeginToEnd);
            float tempDistance = tempDotProduct / tempMagnitudeBeginEnd;

            return tempDistance;
        }

        /// <summary>
        /// Normalize the vector to the passed <paramref name="length"/>.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Vector2 LimitLength(this Vector2 pos, float length = 1f)
        {
            if (pos.magnitude < length) return pos;
            return pos.normalized * length;
        }
    }
}