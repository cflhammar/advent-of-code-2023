using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec03;

public class Solver : ISolver
{
    public string Date { get; } = "Dec03";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var testEs = new EngineSchematic(testInput);
        var testSum = testEs.SumPartNumbers();
        var testGearSum = testEs.SumGearProducts();
        
        var es = new EngineSchematic(input);
        var sum = es.SumPartNumbers();
        var gearSum = es.SumGearProducts();
        
        Console.WriteLine($"Part 1: Test: {testSum} (4361)");
        Console.WriteLine($"Part 1: {sum}");

        Console.WriteLine($"Part 2: Test: {gearSum} (467835)");
        Console.WriteLine($"Part 2: {testGearSum}");
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        var temp2 = reader.SplitByRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(temp2);

        return temp3;
    }
}