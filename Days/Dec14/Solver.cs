using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec14;

public class Solver : ISolver
{
    public string Date { get; } = "Dec14";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var pl = new ParabolicLoad();
        
        var test = pl.CalculateLoad(testInput, 1000000000);
        var ans = pl.CalculateLoad(input, 1000000000);
        
        Console.WriteLine("Part 1: Test: " + test.Item1 + " (136)");
        Console.WriteLine("Part 1: " + ans.Item1);
        Console.WriteLine("Part 2: Test: " + test.Item2 + " (64)");
        Console.WriteLine("Part 1: " + ans.Item2);
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(t2);
        
        return t3;
    }
}