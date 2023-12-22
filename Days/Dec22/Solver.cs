using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec22;

public class Solver : ISolver
{
    public string Date { get; } = "Dec22";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var disintegration = new BlockDisintegrator(input);
        disintegration.FindDisintegrateableBlocks();
        
        Console.WriteLine();
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, "~");

        return t3;
    }
}