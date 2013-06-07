using System.Drawing;
using NUnit.Framework;

namespace GoL.Tests
{
    public class UniverseTests
    {
        private Universe _universe;

        [SetUp]
        public void TestInitialize()
        {
            _universe = new Universe();
        }
        
        [Test]
        public void Tick_returns_empty_generation_when_cell_has_1_neighbour()
        {
            var cell = new Point(0, 0);
            var neighbour = new Point(1, 0);

            var generation = _universe.Tick(cell, neighbour);

            CollectionAssert.DoesNotContain(generation, cell);
        }

        [Test]
        public void Tick_returns_generation_with_cell_when_cell_has_2_neighbours()
        {
            var cell = new Point(0, 0);
            var firstNeighbour = new Point(-1, 0);
            var secondNeighbour = new Point(1, 1);

            var generation = _universe.Tick(cell, firstNeighbour, secondNeighbour);

            CollectionAssert.Contains(generation, cell);
        }

        [Test]
        public void Tick_returns_generation_with_cell_when_cell_has_3_neighbours()
        {
            var cell = new Point(0, 0);
            var firstNeighbour = new Point(-1, -1);
            var secondNeighbour = new Point(-1, 1);
            var thirdNeighbour = new Point(1, 0);

            var generation = _universe.Tick(cell, firstNeighbour, secondNeighbour, thirdNeighbour);

            CollectionAssert.Contains(generation, cell);
        }

        [Test]
        public void Tick_returns_generation_without_cell_when_cell_has_4_neighbours()
        {
            var cell = new Point(0, 0);
            var firstNeighbour = new Point(-1, -1);
            var secondNeighbour = new Point(-1, 1);
            var thirdNeighbour = new Point(1, 0);
            var fourthNeighbour = new Point(1, 1);

            var generation = _universe.Tick(cell, firstNeighbour, secondNeighbour, thirdNeighbour, fourthNeighbour);

            CollectionAssert.DoesNotContain(generation, cell);
        }

        [Test]
        public void Tick_returns_generation_with_allive_cell_when_cell_was_dead_and_had_3_allive_neighbours()
        {
            var cell = new Point(0, 0);
            var firstNeighbour = new Point(-1, -1);
            var secondNeighbour = new Point(0, -1);
            var thirdNeighbour = new Point(1, -1);

            var generation = _universe.Tick(firstNeighbour, secondNeighbour, thirdNeighbour);

            CollectionAssert.Contains(generation, cell);
        }

        // This test is not necessary but I just wanted to prove Blinker works as expected.
        [Test]
        public void Tick_returns_blinker()
        {
            var cell = new Point(0, 0);
            var firstNeighbour = new Point(-1, 0);
            var secondNeighbour = new Point(1, 0);

            var generation = _universe.Tick(cell, firstNeighbour, secondNeighbour);

            CollectionAssert.Contains(generation, cell);
            CollectionAssert.Contains(generation, new Point(0, -1));
            CollectionAssert.Contains(generation, new Point(0, 1));
            CollectionAssert.DoesNotContain(generation, firstNeighbour);
            CollectionAssert.DoesNotContain(generation, secondNeighbour);
        }
    }
}
