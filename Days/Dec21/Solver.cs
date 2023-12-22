using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec21;

public class Solver : ISolver
{
    public string Date { get; } = "Dec21";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var gm = new GardenMap(testInput);
        gm.Steps(5000);
        
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