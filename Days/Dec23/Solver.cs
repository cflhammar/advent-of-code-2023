using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec23;

public class Solver : ISolver
{
    public string Date { get; } = "Dec23";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var lpp = new LongestPathFinder(input);
        lpp.Find();
        
        Console.WriteLine();
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