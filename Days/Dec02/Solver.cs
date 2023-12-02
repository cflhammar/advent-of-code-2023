using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec02;

public class Solver : ISolver
{
    public string Date { get; } = "Dec02";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");
        
        var cc = new CubeCounter();

        var test = cc.PossibleGames(testInput, (12,13,14));
        var actual = cc.PossibleGames(input, (12,13,14));

        Console.WriteLine("Part 1: Test: " + test.Item1 + " (8)");
        Console.WriteLine("Part 1: " + actual.Item1);

        Console.WriteLine("Part 2: Test: " + test.Item2 + " (2286)");
        Console.WriteLine("Part 2: " + actual.Item2);
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfStringByDelimeter(temp2,"; ");

        return temp3;
    }
}