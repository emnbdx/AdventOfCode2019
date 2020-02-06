using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Which puzzle you want to solve ?");
                var puzzle = Console.ReadLine();

                AbstractDay puzzleSolver = null;
                
                try
                {
                    puzzleSolver = (AbstractDay)Activator.CreateInstance(Type.GetType($"AdventOfCode2019.Day{puzzle}"));
                }
                catch
                {
                    Console.WriteLine("Bad puzzle number");
                }
                
                if(puzzleSolver != null)
                    try
                    {
                        puzzleSolver.Compute();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }             
            }
        }
    }
}
