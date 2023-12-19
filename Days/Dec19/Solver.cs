using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec19;

public class Solver : ISolver
{
    public string Date { get; } = "Dec19";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var rulesEngine = new RulesEngine(input);
        
        Console.WriteLine(rulesEngine.SortAll());
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByEmptyRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByRow(t2);

        return t3;
    }
}