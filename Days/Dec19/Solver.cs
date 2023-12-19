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
        
        var rulesEngineTest = new RulesEngine(testInput);
        var rulesEngine = new RulesEngine(input);
        
        Console.WriteLine("Part 1: Test: " + rulesEngineTest.SortAll() + " (19114)");
        Console.WriteLine("Part 1: " + rulesEngine.SortAll());

        Console.WriteLine("Part 2: Test: " + rulesEngineTest.CalculateCombinations()+ " (167409079868000)");
        Console.WriteLine("Part 2: " + rulesEngine.CalculateCombinations());
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