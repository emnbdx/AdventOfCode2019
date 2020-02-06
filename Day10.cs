using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day10 : AbstractDay
    {
        private List<Point> _asteroids = new List<Point>();
        
        public Day10() : base(10)
        {
            InputToPoints();
        }

        public override string Part1()
        {
            var output = GetViewedAsteroids();

            var maxDetection = output.Max(a => a.Value.Count);
            var item = output.FirstOrDefault(a => a.Value.Count == maxDetection);

            return item.Key.ToString() + item.Value.Count;
        }

        public override string Part2()
        {   
            var output = GetViewedAsteroids();

            var maxDetection = output.Max(a => a.Value.Count);
            var item = output.FirstOrDefault(a => a.Value.Count == maxDetection);

            var selected = item.Value.Values.OrderBy(v => v).Reverse().Skip(200 - 2).Take(1).First();
            var lastVaporized = item.Value.FirstOrDefault(v => v.Value == selected).Key;

            return (lastVaporized.X * 100 + lastVaporized.Y).ToString();
        }

        private void InputToPoints()
        {
            var x = 0;
            var y = 0;
            foreach(var line in Data)
            {
                x = 0;
                foreach(var column in line)
                {
                    if(column == '#')
                        _asteroids.Add(new Point(x, y));

                    x++;
                }

                y++;
            }
        }

        private double Angle(Point start, Point end)
        {
            var realAngle = Math.Atan2(start.Y - end.Y, end.X - start.X) * 180 / Math.PI;
            return ((realAngle < 0 ? (360 + realAngle) : realAngle) + 270) % 360;
        }

        private Dictionary<Point, Dictionary<Point, double>> GetViewedAsteroids()
        {
            var output = new Dictionary<Point, Dictionary<Point, double>>();
            foreach(var newBase in _asteroids)
            {
                var angles = new Dictionary<Point, double>();
                foreach(var asteroid in _asteroids)
                {
                    if(newBase != asteroid)
                    {
                        var angle = Angle(newBase, asteroid);

                        if(!angles.Values.Contains(angle))
                            angles.Add(asteroid, angle);
                    }
                }

                output.Add(newBase, angles);
            }

            return output;
        }
    }
}