using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec17;

public class Solver : ISolver
{
    public string Date { get; } = "Dec17";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        // var pathFinderTest = new LavaTransporter(testInput);
        // Console.WriteLine(pathFinderTest.FindPath());
        // Console.WriteLine(pathFinderTest.FindPath(true));
        //
        var pathFinder = new LavaTransporter(input);
        Console.WriteLine(pathFinder.FindPath());
        Console.WriteLine(pathFinder.FindPath(true));
        
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