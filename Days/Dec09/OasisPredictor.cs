namespace aoc_2022.Days.Dec09;

public class OasisPredictor
{
    public int FindOasisSum(List<List<string>> input)
    {
        var sum = 0;
        foreach (var series in input)
        {
            sum += PredictNextValue(series.Select(int.Parse).ToList());
        }
        
        return sum;
    }

    private int PredictNextValue(List<int> series)
    {
        var allDiffs = new List<List<int>>();
        var diff = series;
        while (true)
        {
            diff = GetDiff(diff);
            allDiffs.Add(diff);
            if (allDiffs.Last().All(e => e == 0)) break;
        }

        for (int i = allDiffs.Count-2; i >= 0; i--)
        {
            var after = allDiffs[i].First();
            var under = allDiffs[i + 1].Last();
            allDiffs[i].Insert(0,after - under);
        }

        var predictedValue = series.First() - allDiffs.First().First(); 
        
        return predictedValue;
    }

    private List<int> GetDiff(List<int> series)
    {

        var diff = new List<int>();
        for (int i = 0; i < series.Count - 1; i++)
        {
            diff.Add(series[i+1]-series[i]);
        }

        return diff;
    }
}