using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GoL
{
    public class Universe
    {
        private List<Point> _inputGeneration;
        private readonly List<Point> _nextGeneration;

        public Universe()
        {
            _nextGeneration = new List<Point>();
        }

        public IList<Point> Tick(params Point[] inputGeneration)
        {
            _inputGeneration = new List<Point>(inputGeneration);

            foreach (var cell in inputGeneration)
                ProcessCell(cell);

            return _nextGeneration;
        }

        private void ProcessCell(Point cell)
        {
            KeepCellAlive(cell);

            ResurectDeadNeighbours(cell);
        }

        private void ResurectDeadNeighbours(Point cell)
        {
            var deadNeighbours = GetDeadNeighbours(cell);

            foreach (var deadNeighbour in deadNeighbours)
                ResurectDeadCell(deadNeighbour);
        }

        private void ResurectDeadCell(Point deadNeighbour)
        {
            var aliveNeighbours = GetAlliveNeighbours(deadNeighbour)
                .ToList();
            
            if (IsGoodWillToGetAllive(aliveNeighbours))
                _nextGeneration.Add(deadNeighbour);
        }

        private void KeepCellAlive(Point cell)
        {
            if (HasNumberOfValidNeighbours(cell))
                _nextGeneration.Add(cell);
        }

        private static bool IsGoodWillToGetAllive(
            ICollection aliveNeighbours)
        {
            return (aliveNeighbours.Count == 3);
        }

        private IEnumerable<Point> GetDeadNeighbours(Point cell)
        {
            var allNeighbours = GetAllNeighbours(cell);
            var aliveNeighbours = GetAlliveNeighbours(cell);
            
            return allNeighbours.Where(
                c => !aliveNeighbours.Contains(c));
        }

        private static IEnumerable<Point> GetAllNeighbours(Point cell)
        {
            for(var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1 ; y++)
                    yield return new Point(cell.X + x, cell.Y + y);
        }

        private bool HasNumberOfValidNeighbours(Point cell)
        {
            var neighbours = GetAlliveNeighbours(cell).ToList();

            return (neighbours.Count == 2) || (neighbours.Count == 3);
        }

        private IEnumerable<Point> GetAlliveNeighbours(Point cell)
        {
            return _inputGeneration.Where(
                c => (Math.Abs(cell.X - c.X) < 2) && 
                     (Math.Abs(cell.Y - c.Y) < 2) && 
                     (c != cell));
        }
    }
}
