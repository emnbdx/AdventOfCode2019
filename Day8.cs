using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day8 : AbstractDay
    {
        public Day8() : base(8)
        { }

        public override string Part1()
        {
            var layers = GetLayers(Data.First(), 25, 6);
            var layerWithMinZero = layers.OrderBy(l => l.Count(c => c == '0')).First();

            return "" + layerWithMinZero.Count(c => c == '1') * layerWithMinZero.Count(c => c == '2');
        }

        public override string Part2()
        {
            var layers = GetLayers(Data.First(), 25, 6);

            var output = string.Join("", layers.First().ToList().Select((c, idx) => GetBlackOrWhite(layers, idx)));
            Enumerable.Range(0, output.Length / 25).Select(i => output.Substring(i * 25, 25)).ToList().ForEach(i => Console.WriteLine(i));

            return null;
        }

        private List<string> GetLayers(string input, int wide, int tall)
        {
            var layerSize = wide * tall;
            var layers = Enumerable.Range(0, input.Length / layerSize).Select(i => input.Substring(i * layerSize, layerSize)).ToList();
            
            return layers;
        }

        private char GetBlackOrWhite(List<string> layers, int index)
        {
            foreach(var layer in layers)
            {
                if(layer[index] != '2')
                    return layer[index] == '0' ? ' ' : '▮';
            }

            return '2';
        }
    }
}