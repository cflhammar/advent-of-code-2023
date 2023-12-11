using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec11;

public class Solver : ISolver
{
    public string Date { get; } = "Dec11";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var oa = new ObservatoryAnalysis(testInput,2);
        Console.WriteLine("Part 1: Test: " + oa.SumDistancesBetweenAllPoints() + " (374)");
        
        oa = new ObservatoryAnalysis(input,2);
        Console.WriteLine("Part 1: " + oa.SumDistancesBetweenAllPoints());
        
        oa = new ObservatoryAnalysis(testInput,100);
        Console.WriteLine("Part 2: Test: " + oa.SumDistancesBetweenAllPoints() + " (8410)");
        
        oa = new ObservatoryAnalysis(input,1000000);
        Console.WriteLine("Part 2: " + oa.SumDistancesBetweenAllPoints());
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