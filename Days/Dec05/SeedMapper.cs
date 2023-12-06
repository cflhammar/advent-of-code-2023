namespace aoc_2022.Days.Dec05;

public class SeedMapper
{
    private List<long> _seeds = new();
    private readonly List<List<(long destStart,long sourceStart,long range)>> _mappers = new();
    // private Dictionary<string, int> _memory = new();
 
    public SeedMapper(List<List<string>> input)
    {
        ParseInput(input);
    }

    public long MapSeedsAsRangesThroughAllLayers()
    {
        var min = long.MaxValue;
        for (int seedNumber = 0; seedNumber < _seeds.Count; seedNumber++)
        {
            var seed = _seeds[seedNumber];
            var result = MapThroughAllLayers(seed);
            if (result < min) min = result;
        }
        
        return min;
    }
    
    public long MapSeedsThroughAllLayers()
    {
        var min = long.MaxValue;
        for (int seedNumber = 0; seedNumber < _seeds.Count; seedNumber++)
        {
            var rangeStart = _seeds[seedNumber];
            var range = _seeds[seedNumber + 1];
            for (long n = rangeStart; n < rangeStart + range; n++)
            {
                var result = MapThroughAllLayers(n);
                if (result < min) min = result;   
            }

            seedNumber++;
        }
        
        return min;
    }

    private long MapThroughAllLayers(long seed)
    {
        var current = seed;
        for (int layerNumber = 0; layerNumber < _mappers.Count; layerNumber++)
        {
            current = MapAllNumberThroughLayer(current, layerNumber);
        }

        return current;
    }

    private long MapAllNumberThroughLayer(long current,int layerNumber)
    {
        return MapNumber(current, layerNumber);
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