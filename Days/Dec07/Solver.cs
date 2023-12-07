using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec07;

public class Solver : ISolver
{
    public string Date { get; } = "Dec07";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var cc = new CamelCard();
        Console.WriteLine("Part 1: Test:  " + cc.CalculateWinnings(testInput) + " (6440)");
        Console.WriteLine("Part 1: " + cc.CalculateWinnings(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfStringByDelimeter(temp2, " ");

        return temp3;
    }
}