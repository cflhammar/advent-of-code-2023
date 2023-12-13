using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec13;

public class Solver : ISolver
{
    public string Date { get; } = "Dec13";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var ma = new MirrorAligner(testInput);
        var testResult = ma.FindAndCalculateReflections();
        
        ma = new MirrorAligner(input);
        var result = ma.FindAndCalculateReflections();
        
        Console.WriteLine("Part 1: Test : " + testResult.part1 + " (405)");
        Console.WriteLine("Part 1: " + result.part1);
        
        Console.WriteLine("Part 2: Test : " + testResult.part2 + " (400)");
        Console.WriteLine("Part 2: " + result.part2);
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