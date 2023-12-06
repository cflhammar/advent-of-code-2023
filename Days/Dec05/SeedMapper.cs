using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec05;

public class SeedMapper
{
    private List<long> _seeds;
    private readonly List<List<(long destStart,long sourceStart,long range)>> _mappers = new();

    public SeedMapper(List<List<string>> input)
    {
        ParseInput(input);
    }

    public void MapLayers()
    {
        for (int layerNumber = 0; layerNumber < _mappers.Count; layerNumber++)
        {
            MapAllNumbersThroughLayer(layerNumber);
        }
        Console.WriteLine(_seeds.Min());
    }

    private void MapAllNumbersThroughLayer(int layerNumber)
    {
        var mappedNumbers = new List<long>();
        foreach (var number in _seeds)
        {
            mappedNumbers.Add(MapNumber(number, layerNumber));;
        }

        _seeds = mappedNumbers;
    }

    private long MapNumber(long number, int layerNumber)
    {
        foreach (var func in _mappers[layerNumber])
        {
            if (number >= func.sourceStart && number < func.sourceStart + func.range)
            {
                var newNumber = number + (func.destStart - func.sourceStart);
                return newNumber;
            }
        }

        return number;
    }


    private void ParseInput(List<List<string>> input)
    {
        _seeds = input[0][0].Replace("seeds: ", "").Split(" ").Select(s => long.Parse(s)).ToList();

        for (int i = 1; i < input.Count; i++)
        { 
            _mappers.Add(new List<(long, long, long)>());
            for (int k = 1; k < input[i].Count; k++)
            {
                var numbers = input[i][k].Split(" ").ToList().Select(long.Parse).ToList();
                _mappers.Last().Add((numbers[0],numbers[1], numbers[2]));
            }
        }
    }
}