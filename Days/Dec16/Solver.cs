using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec16;

public class Solver : ISolver
{
    public string Date { get; } = "Dec16";
    
    public void Solve()
    {
        var testInput = ParseInput("test1");
        var input = ParseInput("input");

        var mmTest = new MirrorMaze(testInput);
        var mm = new MirrorMaze(input);
        
        Console.WriteLine("Part 1: Test: " + mmTest.LetThereBeLight(0,0, "right") + " (46)");
        Console.WriteLine("Part 1: " + mm.LetThereBeLight(0,0, "down"));

        Console.WriteLine("Part 2: Test: " + mmTest.FindBestStart()  + " (51)");
        Console.WriteLine("Part 2: " + mm.FindBestStart());
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);
        var t2 = reader.SplitByRow(temp);
        var t3 = reader.SplitListOfStringToListListOfCharByNoDelimeter(t2);

        return t3;
    }
}