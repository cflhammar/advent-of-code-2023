using System;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec06;

public class Solver : ISolver
{
    public string Date { get; } = "Dec06";

    public void Solve()
    {
        List<List<(long raceTime, long record) >> inputs = new()
        {
            new(){ (7, 9), (15, 40), (30, 200)},
            new(){ (45, 295), (98, 1734), (83, 1278),(73,1210)},
            new(){ (71530, 940200)},
            new(){ (45988373, 295173412781210)},
        };
        
        Console.WriteLine("Part 1: Test: " + BoatRace.RaceAllRaces(inputs[0]) + " (288)");
        Console.WriteLine("Part 1:" + BoatRace.RaceAllRaces(inputs[1]));

        Console.WriteLine("Part 2: Test: " + BoatRace.RaceAllRaces(inputs[2]) + " (71503)");
        Console.WriteLine("Part 2: " + BoatRace.RaceAllRaces(inputs[3]));
    }

    public dynamic ParseInput(string fileName)
    {
        var reader = new InputReader();
        var temp = reader.GetFileContent(Date,fileName);

        return temp;
    }
}