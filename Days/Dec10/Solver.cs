using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec10;

public class Solver : ISolver
{
    public string Date { get; } = "Dec10";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var testInput2 = ParseInput("test2");
        var testInput3 = ParseInput("test3");

        var input = ParseInput("input");

        // var ps = new PipeSystem(testInput, 'F');
        // Console.WriteLine("Part 1: Test: " +  ps.FindFurthestPoint() + " (8)");
        
        // ps = new PipeSystem(input, 'L');
        // Console.WriteLine("Part 1: " + ps.FindFurthestPoint());
        
        // var ps = new PipeSystem(testInput2, 'F'); 
        // var ps = new PipeSystem(testInput3, '7'); 
        
        
        var ps = new PipeSystem(input, 'L');
        var solution = ps.Solve();
        Console.WriteLine("Part 1: " + solution.length);
        Console.WriteLine("Part 2: " + solution.insideSize);
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