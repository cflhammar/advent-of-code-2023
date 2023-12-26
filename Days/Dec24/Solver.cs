using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec24;

public class Solver : ISolver
{
    public string Date { get; } = "Dec24";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var test = new HailTrajectory(testInput);
        Console.WriteLine("Part 1: Test: " + test.CountIntersections((7, 27)) + " (2)");
        
        var ht = new HailTrajectory(input);
        Console.WriteLine("Part 1: " + ht.CountIntersections((200000000000000, 400000000000000)));
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, " @ ");
        
        return t3;
    }
}