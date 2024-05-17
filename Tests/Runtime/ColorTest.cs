using NUnit.Framework;
using UnityEngine;

namespace BaseTool.Tests
{
    public class ColorTest
    {
        [Test]
        public void ColorRedToHex()
        {
            var red = Color.red.ToHex();
            Assert.IsTrue(red.Equals("#FF0000"));
        }

        [Test]
        public void ColorGreenToHex()
        {
            var green = Color.green.ToHex();
            Assert.IsTrue(green.Equals("#00FF00"));
        }

        [Test]
        public void ColorBlueToHex()
        {
            var blue = Color.blue.ToHex();
            Assert.IsTrue(blue.Equals("#0000FF"));
        }

        [Test]
        public void ColorMagentaToHexAlpha()
        {
            var magenta = new Color(1, 0, 1, 0.5f).ToHexAlpha();
            Assert.IsTrue(magenta.Equals("#FF00FF80"));
        }
    }
}
