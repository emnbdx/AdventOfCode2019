using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    public class Computer
    {
        private long[] _program;
        private long _setupInput;
        private bool _debug;
        private bool _initiated;
        private int _currentPointer;
        private bool _finished;
        private int _relativeIndex;

        public Computer(long[] program, long setupInput, bool debug = false)
        {
            _program = program;
            _setupInput = setupInput;
            _debug = debug;
            _initiated = false;
            _currentPointer = 0;
            _finished = false;
            _relativeIndex = 0;
        }

        public long? Run(long? input = null)
        {
            if(_debug) Console.WriteLine("Input : " + string.Join(',', _program.Select((d, i) => i + ":" + d).ToArray()));

            while (true)
            {
                var instruction = new Instruction(_program, 
                    _program.ElementAtOrDefault(_currentPointer).ToString(),
                    _program.ElementAtOrDefault(_currentPointer + 1),
                    _program.ElementAtOrDefault(_currentPointer + 2),
                    _program.ElementAtOrDefault(_currentPointer + 3));

                if (instruction.OpCode == OpCodeEnum.END)
                {
                    _finished = true;
                    break;
                }

                if(_debug) Console.WriteLine($"Execute {instruction}");
                var output = instruction.Execute(_relativeIndex, GetCurrentInput(input));
                _program = instruction.Program;

                if (instruction.OpCode == OpCodeEnum.INPUT)
                    _initiated = true;

                if(instruction.OpCode == OpCodeEnum.INDEX)
                {
                    _relativeIndex = (int)output.Value;
                    if(_debug) Console.WriteLine($"New value for relative index {_relativeIndex}");
                }

                _currentPointer = instruction.GetNextPointer(_currentPointer, _relativeIndex);

                if (instruction.OpCode == OpCodeEnum.OUTPUT && output.HasValue)
                {
                    return output;
                }

                if(_debug) Console.WriteLine("Output : " + string.Join(',', _program.Select((d, i) => i + ":" + d).ToArray()));
            }

            return null;
        }

        private long GetCurrentInput(long? input)
        {
            return _initiated && input.HasValue ? input.Value : _setupInput;
        }

        public bool Finished { get { return _finished; } }
    }
}