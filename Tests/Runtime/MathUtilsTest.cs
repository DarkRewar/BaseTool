using NUnit.Framework;

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
    }
}
