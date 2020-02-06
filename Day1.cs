using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day1 : AbstractDay
    {        
        public Day1() : base(1)
        { }

        public override string Part1()
        {
            return IntData.Select(GetFuelForMass).Sum().ToString();
        }

        public override string Part2()
        {
            return IntData.Select(GetFuelForFuel).Sum().ToString();
        }

        private int GetFuelForMass(int mass)
        {
            return mass / 3 - 2;
        }
        
        private int GetFuelForFuel(int fuel)
        {
            fuel = GetFuelForMass(fuel);
            return fuel > 0 ? (fuel + GetFuelForFuel(fuel)) : 0;
        }
    }
}