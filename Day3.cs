using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2019
{
    public class Day3 : AbstractDay
    {
        private int _x;
        private int _y;
        private Point[,] _grid;
        private List<Point> _intersect = new List<Point>();

        public Day3() : base(3)
        {
            var wire1 = Data[0].Split(',').Select(d => new Move(d)).ToList();
            var wire2 = Data[1].Split(',').Select(d => new Move(d)).ToList();
            _grid = new Point[20000, 20000];
            _x = 8000;
            _y = 6000;
            
            DrawWire(wire1, '1');
            DrawWire(wire2, '2');
            //DrawGrid();
        }

        public override string Part1()
        {
            return _intersect.Min(p => p.Distance).ToString();
        }

        public override string Part2()
        {
            return _intersect.Min(p => p.Step).ToString();
        }

        private void DrawWire(List<Move> moves, char currentWire)
        {
            var x = _x;
            var y = _y;
            var step = 0;
            foreach(var move in moves)
            {
                var position = DrawLine(x, y, currentWire, step, move);
                x = position.x;
                y = position.y;
                step = position.step;
            }
        }

        private (int x, int y, int step) DrawLine(int x, int y, char currentWire, int step, Move move)
        {
            for(var i = 1; i <= move.Distance; i++)
            {
                switch(move.Direction)
                {
                    case 'U':
                        x += 1;
                        break;
                    case 'L':
                        y -= 1;
                        break;
                    case 'D':
                        x -= 1;
                        break;
                    case 'R':
                        y += 1;
                        break;
                }

                step += 1;

                _grid[x, y] = _grid[x, y] == null ? new Point(x, y, currentWire, step) : _grid[x, y].SetIntersect(currentWire, _x, _y, step);
                if(_grid[x, y].IsIntersect)
                    _intersect.Add(_grid[x, y]);
            }

            return (x, y, step);
        }
        
        private void DrawGrid()
        {
            int rowLength = _grid.GetLength(0);
            int colLength = _grid.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {                    
                    Console.Write(string.Format("{0} ", _grid[i, j] == null ? "0" : _grid[i, j].IsIntersect ? "X" : "1"));
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public class Move
        {
            private char _direction;
            private int _distance;

            public Move(string raw)
            {
                _direction = raw[0];
                _distance = int.Parse(raw.Replace(_direction.ToString(), ""));
            }

            public char Direction { get { return _direction; } }
            public int Distance { get { return _distance; } }
        }

        public class Point
        {
            private int _x;
            private int _y;
            private char _parent;
            private bool _isIntersect;
            private int? _distance;
            private int? _step;

            public Point(int x, int y, char parent, int step)
            {
                _x = x;
                _y = y;
                _parent = parent;
                _step = step;
            }

            public Point SetIntersect(char parent, int xOrigin, int yOrigin, int step)
            {
                if(parent != _parent)
                {
                    _isIntersect = true;
                    _distance = GetManhattanDistance(xOrigin, yOrigin);
                    _step += step;
                }

                return this;
            }

            private int GetManhattanDistance(int xOrigin, int yOrigin)
            {
                return Math.Abs(xOrigin - _x) + Math.Abs(yOrigin - _y);
            }

            public char Parent { get { return _parent; } }
            public bool IsIntersect { get { return _isIntersect; } }
            public int? Distance { get { return _distance; } }
            public int? Step { get { return _step; } }
        }
    }
}