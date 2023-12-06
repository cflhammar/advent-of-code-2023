using System;
using System.Text.RegularExpressions;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec05;

public class Solver : ISolver
{
    public string Date { get; } = "Dec05";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var smTest = new SeedMapper(testInput);
        Console.WriteLine(smTest.MapSeedsThroughAllLayers());
        Console.WriteLine(smTest.MapSeedsAsRangesThroughAllLayers());

        var sm = new SeedMapper(input);
        Console.WriteLine(sm.MapSeedsThroughAllLayers());
        Console.WriteLine(sm.MapSeedsAsRangesThroughAllLayers()); // takes some minutes, 
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByEmptyRow(temp);
        var temp3 = temp2.Select(s => s.Split("\n").ToList()).ToList();

        return temp3;
    }
}