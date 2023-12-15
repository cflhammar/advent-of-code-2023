using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec12;

public class Solver : ISolver
{
    public string Date { get; } = "Dec12";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var ds = new DamagedSpringsFinder();
        Console.WriteLine("Part 1: Test: " + ds.SumCombinations(testInput) + " (21)");
        Console.WriteLine("Part 1: " + ds.SumCombinations(input));
        
        Console.WriteLine("Part 2: Test: " + ds.SumCombinationsTimesFive(testInput) + " (525152)");
        Console.WriteLine("Part 2: " + ds.SumCombinationsTimesFive(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, " ");

        return t3;
    }
}