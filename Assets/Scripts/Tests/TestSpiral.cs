using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;

namespace Tests
{
    public class TestSpiral
    {
        [Test]
        public void TestSpiralFlatInstantiation()
        {
            SpiralIterators.Flat(new Vector2Int(0, 0));
        }

        [Test]
        public void TestSpiralForEach()
        {
            var spi = SpiralIterators.Flat(new Vector2Int(1, 1));

            IList<Vector2Int> assume = new List<Vector2Int> {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(1, -1),
                new Vector2Int(0, -1),
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, 1),
                new Vector2Int(0, 1),
                new Vector2Int(1, 1),
            };
            IList<Vector2Int> test = new List<Vector2Int>();

            foreach (Vector2Int v in spi)
            {
                test.Add(v);
            }

            Assert.IsTrue(assume.SequenceEqual(test), $"Lists not equal:\n\tAssumed:{String.Join(", ", assume)}\n\tTest:{String.Join(", ", test)}");
        }

        [Test]
        public void TestSpiralForEach2()
        {
            var spi = SpiralIterators.Flat(new Vector2Int(2, 2), new Vector2Int(2, 1), new Vector2Int(1, 0));

                  


            IList<Vector2Int> assume = new List<Vector2Int> {
                new Vector2Int(2, 1),
                new Vector2Int(2, 0),
                new Vector2Int(2, -1),
                new Vector2Int(2, -2),
                new Vector2Int(1, -2),
                new Vector2Int(0, -2),
                new Vector2Int(-1, -2),
                new Vector2Int(-2, -2),
                new Vector2Int(-2, -1),
                new Vector2Int(-2, 0),
                new Vector2Int(-2, 1),
                new Vector2Int(-2, 2),
                new Vector2Int(-1, 2),
                new Vector2Int(0, 2),
                new Vector2Int(1, 2),
                new Vector2Int(2, 2)
            };
            IList<Vector2Int> test = new List<Vector2Int>();

            foreach (Vector2Int v in spi)
            {
                test.Add(v);
            }

            Assert.IsTrue(assume.SequenceEqual(test), $"Lists not equal:\n\tAssumed:{String.Join(", ", assume)}\n\tTest:{String.Join(", ", test)}");
        }

        [Test]
        public void TestSpiralForEach3d()
        {
            var spi = SpiralIterators.Cube(new Vector3Int(1, 1, 1));

            String assume = "(0, 0, 0), (1, 0, 0), (1, -1, 0), (0, -1, 0), (-1, -1, 0), (-1, 0, 0), (-1, 1, 0), (0, 1, 0), (1, 1, 0), (0, 0, -1), (1, 0, -1), (1, -1, -1), (0, -1, -1), (-1, -1, -1), (-1, 0, -1), (-1, 1, -1), (0, 1, -1), (1, 1, -1), (0, 0, 1), (1, 0, 1), (1, -1, 1), (0, -1, 1), (-1, -1, 1), (-1, 0, 1), (-1, 1, 1), (0, 1, 1), (1, 1, 1)";
            IList<Vector3Int> test = new List<Vector3Int>();

            foreach (Vector3Int v in spi)
            {
                test.Add(v);
            }

            string result = String.Join(", ", test);

            Assert.IsTrue(assume.SequenceEqual(result), $"Lists not equal:\n\tAssumed:{assume}\n\tTest:{result}");
        }
    }
}
