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
    }
}
