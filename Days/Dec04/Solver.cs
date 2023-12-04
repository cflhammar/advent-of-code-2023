using System;
using System.Text.RegularExpressions;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec04;

public class Solver : ISolver
{
    public string Date { get; } = "Dec04";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        Console.WriteLine("Part 1: Test: " + ScratchCards.CountPoints(testInput) + " (13)");
        Console.WriteLine("Part 1: " + ScratchCards.CountPoints(input));

        Console.WriteLine("Part 2: Test: " + ScratchCards.CountCards(testInput) + " (30)");
        Console.WriteLine("Part 2: " + ScratchCards.CountCards(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByRow(temp);
        var temp25 = temp2.Select(s => Regex.Replace(s, "Card .*: ", "")).ToList();
        var temp3 = reader.SplitListOfStringToListListOfStringByDelimeter(temp25," | ");
        
        return temp3;
    }
}