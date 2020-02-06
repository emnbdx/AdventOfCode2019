namespace AdventOfCode2019.IntCodeComputer
{
    public class Parameter
    {
        private ModeEnum _mode;
        private long _value;

        public Parameter(ModeEnum mode, long value)
        {
            _mode = mode;
            _value = value;
        }

        public ModeEnum Mode { get { return _mode; } }
        public long Value { get { return _value; } }

        public override string ToString()
        {
            return $"[m:{_mode}, v:{_value}]";
        } 
    }
}