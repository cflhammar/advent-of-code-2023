using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec20;

public class Solver : ISolver
{
    public string Date { get; } = "Dec20";
    
    public void Solve()
    {
        var testInput1 = ParseInput("test1");
        var testInput2 = ParseInput("test2");
        var input = ParseInput("input");
        
        var pm = new PulseModules(testInput1);
        Console.WriteLine("Part 1: Test1: " + pm.HitButtonXTimes(1000) + " (32000000)");
                
        pm = new PulseModules(testInput2);
        Console.WriteLine("Part 1: Test2: " + pm.HitButtonXTimes(1000) + " (11687500)");
        
        pm = new PulseModules(input);
        Console.WriteLine("Part 1: " + pm.HitButtonXTimes(1000));
        pm = new PulseModules(input);
        Console.WriteLine("Part 2: " + pm.HitButtonUntil());
    }
    

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2, " -> ");
        return t3;
    }
}