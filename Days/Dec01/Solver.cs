using System;
using System.Diagnostics;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec01;

public class Solver : ISolver
{
    public string Date { get; } = "Dec01";

    public void Solve()
    {
        var testData = ParseInput("test1");
        var testData2 = ParseInput("test2");
        var inputData = ParseInput("input");

        var worker = new Calibration();

        Console.WriteLine("Part 1: Test: " + worker.FindDigit(testData, false) + " (142)");
        Console.WriteLine("Part 1: " + worker.FindDigit(inputData, false));

        Console.WriteLine("Part 2: Test: " + worker.FindDigit(testData2, true) + " (281)");
        Console.WriteLine("Part 2: " + worker.FindDigit(inputData, true));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();

        var temp = reader.GetFileContent(Date, fileName);
        var temp2 = reader.SplitByRow(temp);

        return temp2;
    }
}