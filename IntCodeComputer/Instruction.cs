using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.IntCodeComputer
{
    public class Instruction
    {
        private long[] _program;
        private OpCodeEnum _opcode;
        private Parameter _p1;
        private Parameter _p2;
        private Parameter _p3;

        public Instruction(long[] program, string opCode, long p1, long p2, long p3)
        {
            _program = program;
            _opcode = (OpCodeEnum)int.Parse(string.Join("", opCode.TakeLast(2)));

            var opCodeCharList = opCode.Reverse();
            var p1Mode = GetMode(opCodeCharList.Skip(2).FirstOrDefault());
            _p1 = new Parameter(p1Mode, p1);

            if (_opcode == OpCodeEnum.ADD ||
                _opcode == OpCodeEnum.MULTIPLY ||
                _opcode == OpCodeEnum.NONZERO ||
                _opcode == OpCodeEnum.ZERO ||
                _opcode == OpCodeEnum.LESS ||
                _opcode == OpCodeEnum.EQUALS)
            {
                var p2Mode = GetMode(opCodeCharList.Skip(3).FirstOrDefault());
                _p2 = new Parameter(p2Mode, p2);
            }

            if (_opcode == OpCodeEnum.ADD ||
                _opcode == OpCodeEnum.MULTIPLY ||
                _opcode == OpCodeEnum.LESS ||
                _opcode == OpCodeEnum.EQUALS)
            {
                var p3Mode = GetMode(opCodeCharList.Skip(4).FirstOrDefault());
                _p3 = new Parameter(p3Mode, p3);
            }
        }

        private ModeEnum GetMode(char c)
        {
            switch(c)
            {
                case '1':
                    return ModeEnum.IMMEDIATE;
                case '2':
                    return ModeEnum.RELATIVE;
                default:
                    return ModeEnum.POSITION;
            }
        }

        public long[] Program { get { return _program; } }
        public OpCodeEnum OpCode { get { return _opcode; } }
        public Parameter P1 { get { return _p1; } }
        public Parameter P2 { get { return _p2; } }
        public Parameter P3 { get { return _p3; } }

        public long? Execute(int relativeIndex, long? input)
        {
            AdjustArraySize(relativeIndex);
            var input1 = ReadValue(P1, relativeIndex);
            var input2 = ReadValue(P2, relativeIndex);
            
            switch(OpCode)
            {
                case OpCodeEnum.ADD:
                    WriteValue(P3, relativeIndex, input1 + input2);
                    break;
                case OpCodeEnum.MULTIPLY:
                    WriteValue(P3, relativeIndex, input1 * input2);
                    break;
                case OpCodeEnum.INPUT:
                    WriteValue(P1, relativeIndex, input.Value);
                    break;
                case OpCodeEnum.OUTPUT:
                    return input1;
                case OpCodeEnum.LESS:
                    WriteValue(P3, relativeIndex, input1 < input2 ? 1 : 0);
                    break;
                case OpCodeEnum.EQUALS:
                    WriteValue(P3, relativeIndex, input1 == input2 ? 1 : 0);
                    break;
                case OpCodeEnum.INDEX:
                    return relativeIndex + input1;          
            }

            return null;
        }

        public int GetNextPointer(int currentPointer, int relativeIndex)
        {
            AdjustArraySize(relativeIndex);
            var input1 = ReadValue(P1, relativeIndex);
            var input2 = ReadValue(P2, relativeIndex);

            switch(OpCode)
            {
                case OpCodeEnum.ADD:
                case OpCodeEnum.MULTIPLY:
                case OpCodeEnum.LESS:
                case OpCodeEnum.EQUALS:
                    return currentPointer + 4;
                case OpCodeEnum.INPUT:
                case OpCodeEnum.OUTPUT:
                case OpCodeEnum.INDEX:
                    return currentPointer + 2;
                case OpCodeEnum.NONZERO:
                    return input1 != 0 ? (int)input2 : currentPointer + 3;
                case OpCodeEnum.ZERO:
                    return input1 == 0 ? (int)input2 : currentPointer + 3;
                default :
                    return currentPointer;
            }
        }

        private long ReadValue(Parameter p, int relativeIndex)
        {
            if(p == null)
                return 0L;

            switch(p.Mode)
            {
                case ModeEnum.POSITION:
                    return _program[p.Value];
                case ModeEnum.IMMEDIATE:
                    return p.Value;
                case ModeEnum.RELATIVE:
                    return _program[relativeIndex + p.Value];
            }

            return 0L;
        }

        private void WriteValue(Parameter p, int relativeIndex, long value)
        {
            if(p.Mode == ModeEnum.RELATIVE)
                _program[relativeIndex + p.Value] = value;
            else 
                _program[p.Value] = value;
        }

        private void AdjustArraySize(int relativeIndex)
        {   
            AdjustArraySizeForParameter(P1, relativeIndex);
            AdjustArraySizeForParameter(P2, relativeIndex);
            AdjustArraySizeForParameter(P3, relativeIndex);
        }

        private void AdjustArraySizeForParameter(Parameter p, int relativeIndex)
        {
            if(p == null)
                return;
            
            if(p.Mode == ModeEnum.POSITION && _program.LongLength <= p.Value)
                Array.Resize(ref _program, (int)p.Value + 1);

            if(p.Mode == ModeEnum.RELATIVE && _program.LongLength <= (relativeIndex + p.Value))
                Array.Resize(ref _program, (int)p.Value + relativeIndex + 1);             
        }

        public override string ToString()
        {
            return $"Instruction {_opcode} with ({_p1}, {_p2}, {_p3})";
        }
    }
}