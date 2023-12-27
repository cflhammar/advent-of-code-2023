using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec25;

public class Solver : ISolver
{
    public string Date { get; } = "Dec25";
    
    public void Solve()
    {
        // does not work on the test data
        // var testInput = ParseInput("test1");
        
        var input = ParseInput("input");
        var rw = new Rewire();
        Console.WriteLine("Part 1: " + rw.SeparateGroups(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, ": ");

        return t3;
    }
}