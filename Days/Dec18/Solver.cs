using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec18;

public class Solver : ISolver
{
    public string Date { get; } = "Dec18";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var digger = new TrenchDigger();
        Console.WriteLine("Part 1: Test: "+ digger.Dig(testInput) + " (62)");
        Console.WriteLine("Part 1: "+ digger.Dig(testInput, true));
        Console.WriteLine("Part 2: Test: "+ digger.Dig(input) + " (952408144115)");
        Console.WriteLine("Part 2: "+ digger.Dig(input, true));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return reader.SplitByRow(temp);
    }
}