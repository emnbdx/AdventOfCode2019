using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.IntCodeComputer;

namespace AdventOfCode2019
{
    public class Day5 : AbstractDay
    {
        public Day5() : base(5)
        { }

        public override string Part1()
        {
            var computer = new Computer(IntCodeData, 1);
            var output = computer.Run();

            return output.ToString();
        }

        public override string Part2()
        {
            var computer = new Computer(IntCodeData, 5);
            var output = computer.Run();

            return output.ToString();
        }
    }
}