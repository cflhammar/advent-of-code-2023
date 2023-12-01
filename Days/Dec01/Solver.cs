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
        var inputData = ParseInput("input");
        
        var calorieCounter = new CalorieCounter();
        
        Console.WriteLine("Part 1: Test: " + calorieCounter.SummarizeCalories(testData,1) + " (24000)");
        Console.WriteLine("Part 1: " + calorieCounter.SummarizeCalories(inputData,1));
        
        Console.WriteLine("Part 2: Test: " + calorieCounter.SummarizeCalories(testData,3) + " (45000)");
        Console.WriteLine("Part 2: " + calorieCounter.SummarizeCalories(inputData,3));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();

        var temp = reader.GetFileContent(Date,fileName);
        var temp2 = reader.SplitByEmptyRow(temp);
        var temp3 = reader.SplitListOfStringToListListOfStringByRow(temp2);
        
        return temp3;
    }
}