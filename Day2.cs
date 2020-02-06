using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2019
{
    public class Day2 : AbstractDay
    {        
        private long[] _numbers;
        
        public Day2() : base(2)
        { }

        public override string Part1()
        {
            return OpCodeCompute(12, 2).ToString();
        }

        public override string Part2()
        {
            for(var noun = 0; noun < 100; noun++)
            {
                for(var verb = 0; verb < 100; verb++)
                {
                    if(OpCodeCompute(noun, verb) == 19690720)
                        return (100 * noun + verb).ToString();
                }
            }

            return null;
        }

        private long OpCodeCompute(long noun, long verb)
        {
            _numbers = IntCodeData.ToArray();

            _numbers[1] = noun;
            _numbers[2] = verb;
            
            var currentIdx = 0;
            while(_numbers[currentIdx] != 99)
            {
                ExecuteInstruction(_numbers[currentIdx], _numbers[currentIdx + 1], _numbers[currentIdx + 2], _numbers[currentIdx + 3]);
                currentIdx += 4;
            }

            return _numbers[0];
        }

        private void ExecuteInstruction(long instruction, long input1Idx, long input2Idx, long outputIdx)
        {
            if(instruction == 1)
            {
                _numbers[outputIdx] = _numbers[input1Idx] + _numbers[input2Idx];
            }
            else if(instruction == 2)
            {
                _numbers[outputIdx] = _numbers[input1Idx] * _numbers[input2Idx];
            }
        }
    }
}