namespace aoc_2022.Days.Dec11;

public class ObservatoryAnalysis
{
    private List<List<char>> _map;
    private List<(int r, int c)> _satellites = new();

    public ObservatoryAnalysis(List<List<char>> map)
    {
        _map = AddExtraRowForEachEmptyRow(map);
        _map = Helpers.MatrixOperations.Rotate90(_map);
        _map = AddExtraRowForEachEmptyRow(_map);
        _map = Helpers.MatrixOperations.Rotate90(_map);
        _map = Helpers.MatrixOperations.Rotate90(_map);
        _map = Helpers.MatrixOperations.Rotate90(_map);
        AssignSatellites();
        PrintMap();
        SumDistancesBetweenAllPoints();
    }
    
    public void SumDistancesBetweenAllPoints()
    {
        var sum = 0;
        for (int i = 0; i < _satellites.Count; i++)
        {
            var satellite = _satellites[i];
            for (int j = i + 1; j < _satellites.Count; j++)
            {
                Console.WriteLine(i +"->" +j);
                var otherSatellite = _satellites[j];
                sum += CountManhattanDistanceInFromPointToOtherPoint(satellite, otherSatellite);
            }
        }
        Console.WriteLine(sum);
        
    }

    private void AssignSatellites()
    {
        var counter = 1;
        for (var r = 0; r < _map.Count; r++)
        {
            var row = _map[r];
            for (int c = 0; c < row.Count; c++)
            {
                if (row[c] == '#')
                {
                    row[c] = counter.ToString()[0];
                    _satellites.Add((r, c));
                    counter++;
                }
            }
        }
    }
    
    private int CountManhattanDistanceInFromPointToOtherPoint((int r, int c) point1, (int r, int c) point2)
    {
        return Math.Abs(point1.r - point2.r) + Math.Abs(point1.c - point2.c);
    }

    private List<List<char>> AddExtraRowForEachEmptyRow(List<List<char>> map)
    {
        var newMap = new List<List<char>>();
        foreach (var row in map)
        {
            if (row.All(e => e == '.')) newMap.Add(map.First().Select(_ => '.').ToList());
            newMap.Add(row);
        }
        return newMap;
    }

    private void PrintMap()
    {  Console.WriteLine("-.-.-.-.-.-.-.-.-.-");
        foreach (var row in _map)
        {
            var s = string.Join("", row);
            Console.WriteLine(s);
        }
    }
}