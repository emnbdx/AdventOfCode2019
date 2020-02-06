using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2019
{
    public class Day4 : AbstractDay
    {
        private int _min;
        private int _max;

        public Day4() : base(4)
        {
            _min = Data.First().Split('-').Select(int.Parse).First();
            _max = Data.First().Split('-').Select(int.Parse).Last();
        }

        public override string Part1()
        {
            var candidate = 0;

            for(var i = _min; i <= _max; i++)
            {
                var explodedNumber = ExplodeNumber(i);
                
                if(SameAdjacent(explodedNumber) && NeverDecrease(explodedNumber))
                    candidate++;
            }

            return candidate.ToString();
        }

        public override string Part2()
        {
            var candidate = 0;

            for(var i = _min; i <= _max; i++)
            {
                var explodedNumber = ExplodeNumber(i);
                
                if(SameAdjacentImproved(explodedNumber) && NeverDecrease(explodedNumber))
                    candidate++;
            }

            return candidate.ToString();
        }

        private bool SameAdjacent((int n1, int n2, int n3, int n4, int n5, int n6) explodedNumber)
        {
            return explodedNumber.n1 == explodedNumber.n2 || 
                explodedNumber.n2 == explodedNumber.n3 || 
                explodedNumber.n3 == explodedNumber.n4 || 
                explodedNumber.n4 == explodedNumber.n5 || 
                explodedNumber.n5 == explodedNumber.n6;
        }

        private bool SameAdjacentImproved((int n1, int n2, int n3, int n4, int n5, int n6) explodedNumber)
        {
            return explodedNumber.n1 == explodedNumber.n2 && explodedNumber.n1 != explodedNumber.n3 || 
                explodedNumber.n2 == explodedNumber.n3 && explodedNumber.n2 != explodedNumber.n1 && explodedNumber.n2 != explodedNumber.n4 || 
                explodedNumber.n3 == explodedNumber.n4 && explodedNumber.n3 != explodedNumber.n2 && explodedNumber.n3 != explodedNumber.n5 || 
                explodedNumber.n4 == explodedNumber.n5 && explodedNumber.n4 != explodedNumber.n3 && explodedNumber.n4 != explodedNumber.n6 || 
                explodedNumber.n5 == explodedNumber.n6 && explodedNumber.n5 != explodedNumber.n4;
        }

        private bool NeverDecrease((int n1, int n2, int n3, int n4, int n5, int n6) explodedNumber)
        {
            return explodedNumber.n2 >= explodedNumber.n1 && 
                explodedNumber.n3 >= explodedNumber.n2 && 
                explodedNumber.n4 >= explodedNumber.n3 && 
                explodedNumber.n5 >= explodedNumber.n4 && 
                explodedNumber.n6 >= explodedNumber.n5;
        }

        private (int n1, int n2, int n3, int n4, int n5, int n6) ExplodeNumber(int number)
        {
            var s = number.ToString();

            return (int.Parse(s[0].ToString()), 
                int.Parse(s[1].ToString()), 
                int.Parse(s[2].ToString()), 
                int.Parse(s[3].ToString()), 
                int.Parse(s[4].ToString()), 
                int.Parse(s[5].ToString()));
        }
    }
}