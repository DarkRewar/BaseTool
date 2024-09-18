using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BaseTool.Tests
{
    public class MathUtilsTest
    {
        #region MathUtils.Modulo

        [Test(Author = "DarkRewar", Description = "Test a simple modulo calc")]
        public void MathUtils1Modulo6()
        {
            Assert.IsTrue(MathUtils.Modulo(1, 6) == 1);
        }

        [Test(Author = "DarkRewar", Description = "Test a negative modulo calc")]
        public void MathUtilsMinus1Modulo6()
        {
            Assert.IsTrue(MathUtils.Modulo(-1, 6) == 5);
        }

        [Test(Author = "DarkRewar", Description = "Test an overflow positive modulo calc")]
        public void MathUtils7Modulo6()
        {
            Assert.IsTrue(MathUtils.Modulo(7, 6) == 1);
        }

        [Test(Author = "DarkRewar", Description = "Test an overflow negative modulo calc")]
        public void MathUtilsMinus9Modulo6()
        {
            Assert.IsTrue(MathUtils.Modulo(-9, 6) == 3);
        }

        #endregion

        #region MathUtils.Approximately

        [Test(Author = "DarkRewar", Description = "Test if two numbers are near")]
        public void MathUtilsDefaultApproximately()
        {
            Assert.IsTrue(MathUtils.Approximately(0.01f, 0.001f));
        }

        [Test(Author = "DarkRewar", Description = "Test if two numbers are near")]
        public void MathUtilsApproximatelyWithVector3()
        {
            float value = 1E-16f;
            var smallVector = new Vector3(value, value, value);
            Assert.IsFalse(Mathf.Approximately(smallVector.magnitude, 0));
            Assert.IsTrue(MathUtils.Approximately(smallVector.magnitude, 0));
        }

        [Test(Author = "DarkRewar", Description = "Test if two numbers are near")]
        public void MathUtilsApproximatelyWithVector3AndTooSmallTolerence()
        {
            float value = 1E-16f;
            var smallVector = new Vector3(value, value, value);
            Assert.IsFalse(Mathf.Approximately(smallVector.magnitude, 0));
            Assert.IsFalse(MathUtils.Approximately(smallVector.magnitude, 0, 1E-24f));
        }

        #endregion
        
        #region MathUtils.IsPointInsidePolygon

        private List<Vector2> GenerateSquare() => new()
        {
            new(0, 0),
            new(0, 1),
            new(1, 1),
            new(1, 0)
        };
        
        private List<Vector2> GenerateOctagon() => new()
        {
            new(1, 0),
            new(2, 0),
            new(3, 1),
            new(3, 2),
            new(2, 3),
            new(1, 3),
            new(0, 2),
            new(0, 1)
        };
        
        private List<Vector2> GenerateV() => new()
        {
            new(0, 0),
            new(3, 3),
            new(2, 3),
            new(0, 1),
            new(-2, 3),
            new(-3, 3)
        };
        
        [Test(Author = "DarkRewar", Description = "Test if a point is inside a square")]
        public void MathUtilsPointIsInsideSquare()
        {
            List<Vector2> points = GenerateSquare();
            Assert.IsTrue(MathUtils.IsPointInsidePolygon(new Vector2(0.5f, 0.5f), points));
        }
        
        [Test(Author = "DarkRewar", Description = "Test if a point is not inside a square")]
        public void MathUtilsPointIsOutsideSquare()
        {
            List<Vector2> points = GenerateSquare();
            Assert.IsFalse(MathUtils.IsPointInsidePolygon(new Vector2(-1, -1), points));
        }
        
        [Test(Author = "DarkRewar", Description = "Test if a point is inside an octagon")]
        public void MathUtilsPointIsInsideOctagon()
        {
            List<Vector2> points = GenerateOctagon();
            Assert.IsTrue(MathUtils.IsPointInsidePolygon(new Vector2(2.5f, 1.5f), points));
        }
        
        [Test(Author = "DarkRewar", Description = "Test if a point is outside an octagon")]
        public void MathUtilsPointIsOutsideOctagon()
        {
            List<Vector2> points = GenerateOctagon();
            Assert.IsFalse(MathUtils.IsPointInsidePolygon(new Vector2(3, 3), points));
        }
        
        [Test(Author = "DarkRewar", Description = "Test if a point is inside a complex polygon")]
        public void MathUtilsPointIsInsideComplexPolygon()
        {
            List<Vector2> points = GenerateV();
            Assert.IsTrue(MathUtils.IsPointInsidePolygon(new Vector2(-1f, 1.5f), points));
        }
        
        [Test(Author = "DarkRewar", Description = "Test if a point is outside a complex polygon")]
        public void MathUtilsPointIsOutsideComplexPolygon()
        {
            List<Vector2> points = GenerateV();
            Assert.IsFalse(MathUtils.IsPointInsidePolygon(new Vector2(0, 2), points));
        }
        
        #endregion
    }
}
