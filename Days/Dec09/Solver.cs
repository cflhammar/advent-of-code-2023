using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec09;

public class Solver : ISolver
{
    public string Date { get; } = "Dec09";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var op = new OasisPredictor();

        Console.WriteLine(op.FindOasisSum(testInput));
        Console.WriteLine(op.FindOasisSum(input));

    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2," ");

        return t3;
    }
}