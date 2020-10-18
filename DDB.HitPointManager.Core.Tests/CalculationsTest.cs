using System.Collections.Generic;
using NUnit.Framework;

namespace DDB.HitPointManager.Core.Tests
{
    [TestFixture]
    public class CalculationsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAvgHitPoints_ReturnsCorrectValues()
        {
            var values = new Dictionary<int, int>
            {
                {6, 4},
                {8, 5},
                {10, 6},
                {12, 7}
            };
            foreach (var (key, value) in values)
            {
                var result = Calculations.GetAvgHitPoints(key);
                Assert.AreEqual(value, result);
            }
        }

        [Test]
        public void HalfRoundDown_ReturnsCorrectValues()
        {
            var values = new Dictionary<int, int>
            {
                {6, 3},
                {8, 4},
                {10, 5},
                {12, 6},
                {21, 10},
                {7, 3},
                {1, 0},
                {-1, -1},
                {-15, -8}
            };
            foreach (var (key, value) in values)
            {
                var result = Calculations.HalfRoundDown(key);
                Assert.AreEqual(value, result);
            }
        }

        [Test]
        public void GetStatModifier_ReturnsCorrectValues()
        {
            var values = new Dictionary<int, int>
            {
                {1, -5},
                {2, -4},
                {3, -4},
                {4, -3},
                {5, -3},
                {6, -2},
                {7, -2},
                {8, -1},
                {9, -1},
                {10, 0},
                {11, 0},
                {12, 1},
                {13, 1},
                {14, 2},
                {15, 2},
                {16, 3},
                {17, 3},
                {18, 4},
                {19, 4},
                {20, 5},
                {21, 5},
                {22, 6},
                {23, 6},
                {24, 7},
                {25, 7},
                {26, 8},
                {27, 8},
                {28, 9},
                {29, 9},
                {30, 10},
            };
            foreach (var (key, value) in values)
            {
                var result = Calculations.GetStatModifier(key);
                Assert.AreEqual(value, result);
            }
        }
    }
}