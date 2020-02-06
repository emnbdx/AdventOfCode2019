using System;
using AdventOfCode2019.IntCodeComputer;

namespace AdventOfCode2019
{
    public class Day9 : AbstractDay
    {
        public Day9() : base(9)
        { }

        public override string Part1()
        {
            var computer = new Computer(IntCodeData, 1);
  
            while(!computer.Finished)
            {
                var output = computer.Run();
                if(output.HasValue)
                    return output.Value.ToString();
            }

            return null;
        }

        public override string Part2()
        {
            var computer = new Computer(IntCodeData, 2);
  
            while(!computer.Finished)
            {
                var output = computer.Run();
                if(output.HasValue)
                    return output.Value.ToString();
            }

            return null;
        }
    }
}