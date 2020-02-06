using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public abstract class AbstractDay
    {
        internal int DayCount;
        internal List<string> Data;

        internal AbstractDay(int dayCount)
        {
            DayCount = dayCount;
            Data = File.ReadAllLines(Path.Combine("input", $"day{dayCount}")).ToList();
        }

        public void Compute()
        {
            Console.WriteLine($"Day {DayCount} part 1 result: {Part1()}");
            Console.WriteLine($"Day {DayCount} part 2 result: {Part2()}");
        }

        public long[] IntCodeData { get { return Data.First().Split(',').Select(long.Parse).ToArray(); } }
        public List<int> IntData { get { return Data.Select(int.Parse).ToList(); } }
        public abstract string Part1();
        public abstract string Part2();
    }
}
