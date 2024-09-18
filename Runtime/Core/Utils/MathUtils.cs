using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
    public static class MathUtils
    {
        /// <summary>
        /// The modulo function because using % causes troubles.
        /// If you want "-1 % 2", better using this method.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Modulo(int index, int count)
        {
            return (index % count + count) % count;
        }

        /// <summary>
        /// Work quite like the <see cref="UnityEngine.Mathf.Approximately(float, float)"/>
        /// method but allows a third arguments to change the tolerance.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Approximately(float a, float b, float tolerance = 0.001f)
        {
            return Mathf.Abs(b - a) < Mathf.Max(Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), tolerance);
        }

        /// <summary>
        /// Check if a point is inside a polygon (list of <see cref="Vector2"/>).
        /// <br/> 2D usage only.
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <param name="polygon">List of <see cref="Vector2"/> which forms the polygon.</param>
        /// <returns></returns>
        /// <inheritdoc cref="https://codereview.stackexchange.com/a/108903"/>
        public static bool IsPointInsidePolygon(Vector2 point, Vector2[] polygon)
        {
            int polygonLength = polygon.Length, i=0;
            bool inside = false;
            // x, y for tested point.
            float pointX = point.x, pointY = point.y;
            // start / end point for the current polygon segment.
            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonLength-1];           
            endX = endPoint.x; 
            endY = endPoint.y;
            while (i < polygonLength) 
            {
                startX = endX;           startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x;       endY = endPoint.y;
                //
                inside ^= ( endY > pointY ^ startY > pointY ) /* ? pointY inside [startY;endY] segment ? */
                          && /* if so, test if it is under the segment */
                          ( (pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY) ) ;
            }
            return inside;
        }
        
        /// <inheritdoc cref="IsPointInsidePolygon(UnityEngine.Vector2,UnityEngine.Vector2[])"/>
        public static bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
            => IsPointInsidePolygon(point, polygon.ToArray());
        
        /// <summary>
        /// Check if a point is inside a polygon (list of <see cref="Vector2Int"/>).
        /// <br/> 2D usage only.
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <param name="polygon">List of <see cref="Vector2"/> which forms the polygon.</param>
        /// <returns></returns>
        /// <inheritdoc cref="https://codereview.stackexchange.com/a/108903"/>
        public static bool IsPointInsidePolygon(Vector2Int point, Vector2Int[] polygon)
        {
            int polygonLength = polygon.Length, i=0;
            bool inside = false;
            // x, y for tested point.
            float pointX = point.x, pointY = point.y;
            // start / end point for the current polygon segment.
            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonLength-1];           
            endX = endPoint.x; 
            endY = endPoint.y;
            while (i < polygonLength) 
            {
                startX = endX;           startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x;       endY = endPoint.y;
                //
                inside ^= ( endY > pointY ^ startY > pointY ) /* ? pointY inside [startY;endY] segment ? */
                          && /* if so, test if it is under the segment */
                          ( (pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY) ) ;
            }
            return inside;
        }
        
        /// <inheritdoc cref="IsPointInsidePolygon(UnityEngine.Vector2Int,UnityEngine.Vector2Int[])"/>
        public static bool IsPointInsidePolygon(Vector2Int point, List<Vector2Int> polygon)
            => IsPointInsidePolygon(point, polygon.ToArray());
    }
}
