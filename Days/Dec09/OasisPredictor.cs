namespace aoc_2022.Days.Dec09;

public class OasisPredictor
{
    public (int sumNext, int sumBefore) FindOasisSum(List<List<string>> input)
    {
        var sumNext = 0;
        var sumBefore = 0;
        foreach (var series in input)
        {
            var res = PredictNextValue(series.Select(int.Parse).ToList());
            sumNext += res.predictedNextValue;
            sumBefore += res.predictedBeforeValue;
        }
        
        return (sumNext, sumBefore) ;
    }

    private (int predictedNextValue, int predictedBeforeValue) PredictNextValue(List<int> series)
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
            allDiffs[i].Add(allDiffs[i].Last() + allDiffs[i + 1].Last());
            allDiffs[i].Insert(0,allDiffs[i].First() - allDiffs[i + 1].First());
        }

        var predictedNextValue = series.Last() + allDiffs.First().Last(); 
        var predictedBeforeValue = series.First() - allDiffs.First().First(); 

        return (predictedNextValue, predictedBeforeValue);
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