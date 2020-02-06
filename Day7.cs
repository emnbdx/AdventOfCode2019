using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.IntCodeComputer;

namespace AdventOfCode2019
{
    public class Day7 : AbstractDay
    {
        private List<List<int>> _sequences;
        private List<string> Amplifiers { get; set; } = new List<string>();

        public Day7() : base(7)
        { }

        public override string Part1()
        {
            _sequences = GenerateSequence('0', '1', '2', '3', '4');

            var maxOutput = 0L;
            foreach (var sequence in _sequences)
            {
                var output = 0L;
                foreach (var phase in sequence)
                {
                    var computer = new Computer(IntCodeData, phase);
                    var tmpOutput = computer.Run(output);
                    output = tmpOutput.HasValue ? tmpOutput.Value : 0;
                }

                maxOutput = Math.Max(maxOutput, output);
            }

            return maxOutput.ToString();
        }

        public override string Part2()
        {
            _sequences = GenerateSequence('5', '6', '7', '8', '9');

            var maxOutput = 0L;
            foreach (var sequence in _sequences)
            {
                var output = 0L;
                int count = 0;

                var amplifiers = new Dictionary<int, Computer>();
                amplifiers.Add(5, new Computer(Data.First().Split(',').Select(long.Parse).ToArray(), 5));
                amplifiers.Add(6, new Computer(Data.First().Split(',').Select(long.Parse).ToArray(), 6));
                amplifiers.Add(7, new Computer(Data.First().Split(',').Select(long.Parse).ToArray(), 7));
                amplifiers.Add(8, new Computer(Data.First().Split(',').Select(long.Parse).ToArray(), 8));
                amplifiers.Add(9, new Computer(Data.First().Split(',').Select(long.Parse).ToArray(), 9));

                while (!amplifiers.All(a => a.Value.Finished))
                {
                    var phase = sequence[count % 5];
                    var computer = amplifiers[phase];

                    var tmpOutput = computer.Run(output);
                    if (tmpOutput.HasValue)
                    {
                        output = tmpOutput.Value;
                        count++;
                    }

                    if (computer.Finished)
                    { 
                        count++;
                    }
                }

                maxOutput = Math.Max(maxOutput, output);
            }

            return maxOutput.ToString();
        }

        private List<List<int>> GenerateSequence(char c1, char c2, char c3, char c4, char c5)
        {
            var sequences = new List<List<int>>();

            for (var i = 0; i <= 99999; i++)
            {
                var si = i.ToString().PadLeft(5, '0');
                if (si.Contains(c1) &&
                    si.Contains(c2) &&
                    si.Contains(c3) &&
                    si.Contains(c4) &&
                    si.Contains(c5))
                {
                    var sequence = si.ToList().Select(c => c.ToString()).Select(int.Parse).ToList();
                    sequences.Add(sequence);
                }
            }

            return sequences;
        }
    }
}