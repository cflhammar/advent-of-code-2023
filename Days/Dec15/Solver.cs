using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec15;

public class Solver : ISolver
{
    public string Date { get; } = "Dec15";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var hh = new HolidayHasher();
        
        Console.WriteLine("Part 1: Test: " + hh.SumHashes(testInput) + " (1320)");
        Console.WriteLine("Part 1: " + hh.SumHashes(input));

        Console.WriteLine("Part 2: Test: " + hh.ArrangeLenses(testInput) + " (145)");
        Console.WriteLine("Part 2: " + hh.ArrangeLenses(input));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = temp.Replace("\n", "");
        var t3 = reader.SplitStringByDelimeterToListOfString(t2, ",");

        return t3;
    }
}