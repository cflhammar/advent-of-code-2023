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

        var ds = new DamagedSprings();
        // Console.WriteLine("Part 1: Test: " + ds.FindCombinationsForAllSprings(testInput) + " (21)");
        // Console.WriteLine("Part 1: " + ds.FindCombinationsForAllSprings(input));
        
        Console.WriteLine("Part 2: Test: " + ds.FindTripleCombinationsForAllSprings(testInput) + " ()");
        // Console.WriteLine("Part 2: " + ds.FindTripleCombinationsForAllSprings(input));
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