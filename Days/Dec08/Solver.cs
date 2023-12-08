using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec08;

public class Solver : ISolver
{
    public string Date { get; } = "Dec08";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var testInput2 = ParseInput("test2");
        var input = ParseInput("input");

        var mr = new MapReader(testInput, false);
        Console.WriteLine("Part 1: " + mr.Navigate() + " (6)");
        mr = new MapReader(input, false);
        Console.WriteLine("Part 1: Test: " + mr.Navigate());
        mr = new MapReader(testInput2, true);
        Console.WriteLine("Part 2: " + mr.Navigate() + " (6)");
        mr = new MapReader(input, true);
        Console.WriteLine("Part 2: Test: " + mr.Navigate());
        
        
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByEmptyRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfStringByRow(temp2);

        return temp3;
    }
}