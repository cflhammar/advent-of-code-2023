using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec09;

public class Solver : ISolver
{
    public string Date { get; } = "Dec09";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var op = new OasisPredictor();
        var test = op.FindOasisSum(testInput);
        var ans = op.FindOasisSum(input);

        Console.WriteLine("Part 1: Test: " + test.Item1 + " (114)");
        Console.WriteLine("Part 1: " + ans.Item1);
        
        Console.WriteLine("Part 2: Test: " + test.Item2 + " (2)");
        Console.WriteLine("Part 2: " + ans.Item2);
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfStringByDelimeter(t2," ");

        return t3;
    }
}