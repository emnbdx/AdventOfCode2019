using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode2019.IntCodeComputer;

namespace AdventOfCode2019
{
    public class Day11 : AbstractDay
    {
        public Day11() : base(11)
        { }

        public override string Part1()
        {
            var robot = new Robot(new char[68,84], new Point(38, 72));
            RunRobot(robot, Color.BLACK);
            robot.DrawGrid();
            
            return robot.UpdatedPanelsCount.ToString();
        }

        public override string Part2()
        {
            var robot = new Robot(new char[118,81], new Point(75, 75));
            RunRobot(robot, Color.WHITE);
            robot.DrawGrid();

            return null;
        }

        private void RunRobot(Robot robot, Color initialColor)
        {   
            var computer = new Computer(IntCodeData, (long)initialColor);
            var outputIdx = 0;
            while(!computer.Finished)
            {
                var currentColor = robot.GetCurrentColor();
                var output = computer.Run((long)currentColor);
                if(output.HasValue)
                {
                    if(outputIdx % 2 == 0)
                    {                        
                        robot.Paint((Color)Enum.Parse(typeof(Color), output.Value.ToString()));
                    }
                    else
                    {
                        robot.NextMove((Turn)Enum.Parse(typeof(Turn), output.Value.ToString()));
                    }

                    outputIdx++;
                }
            }

            Console.WriteLine($"{robot.UpdatedPanels.Min(p => p.X)},{robot.UpdatedPanels.Min(p => p.Y)}");
            Console.WriteLine($"{robot.UpdatedPanels.Max(p => p.X)},{robot.UpdatedPanels.Max(p => p.Y)}");
        }

        public enum Direction
        {
            UP,
            LEFT,
            DOWN,
            RIGHT
        }

        public enum Turn
        {
            LEFT = 0,
            RIGHT = 1
        }

        public enum Color
        {
            BLACK = 0,
            WHITE = 1
        }

        public class Robot
        {
            private char[,] _grid;
            private Point _position;
            private Direction _direction = Direction.UP;
            private List<Point> _updatedPanels = new List<Point>();

            public Robot(char[,] grid, Point position)
            {
                _grid = grid;
                _position = position;
            }

            public Color GetCurrentColor()
            {
                //Console.WriteLine($"{_grid.GetLength(0)}{_grid.GetLength(1)} : {_position.X}{_position.Y}");
                return _grid[_position.X,_position.Y] == '▮' ? Color.WHITE : Color.BLACK;
            }

            public void Paint(Color color)
            {
                //Console.WriteLine($"Paint ({color}) at ({_position.X},{_position.Y})");
                _grid[_position.X,_position.Y] = color == Color.WHITE ? '▮' : ' ';
                if(!_updatedPanels.Contains(_position))
                    _updatedPanels.Add(_position);
            }

            public Point NextMove(Turn turn)
            {
                //Console.Write($"Update position with {turn} from ({_position.X},{_position.Y})");
                
                if(_direction == Direction.UP)
                    if(turn == Turn.LEFT)
                    {
                        _position.X = _position.X - 1;
                        _direction = Direction.LEFT;
                    }
                    else 
                    {
                        _position.X = _position.X + 1;
                        _direction = Direction.RIGHT;
                    }
                else if(_direction == Direction.LEFT)
                    if(turn == Turn.LEFT)
                    {
                        _position.Y = _position.Y + 1;
                        _direction = Direction.DOWN;
                    }
                    else 
                    {
                        _position.Y = _position.Y - 1;
                        _direction = Direction.UP;
                    }
                else if(_direction == Direction.DOWN)
                    if(turn == Turn.LEFT)
                    {
                        _position.X = _position.X + 1;
                        _direction = Direction.RIGHT;
                    }
                    else 
                    {
                        _position.X = _position.X - 1;
                        _direction = Direction.LEFT;
                    }
                else if(_direction == Direction.RIGHT)
                    if(turn == Turn.LEFT)
                    {
                        _position.Y = _position.Y - 1;
                        _direction = Direction.UP;
                    }
                    else 
                    {
                        _position.Y = _position.Y + 1;
                        _direction = Direction.DOWN;
                    }
                
                //Console.WriteLine($" to ({_position.X},{_position.Y})");
                return _position;
            }

            public void DrawGrid()
            {
                int rowLength = _grid.GetLength(0);
                int colLength = _grid.GetLength(1);
                for (int i = rowLength - 1; i >= 0; i--)
                {
                    for (int j = 0; j < colLength; j++)
                    {                    
                        Console.Write(_grid[i, j]);
                    }

                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
            }

            public Point CurrentPosition { get { return _position; } }

            public List<Point> UpdatedPanels { get { return _updatedPanels; } }

            public int UpdatedPanelsCount { get { return _updatedPanels.Count(); } }
        }
    }
}