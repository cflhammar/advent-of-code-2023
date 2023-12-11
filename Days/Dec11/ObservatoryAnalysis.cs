namespace aoc_2022.Days.Dec11;

public class ObservatoryAnalysis
{
    private readonly List<List<char>> _map;
    private List<(int r, int c)> _satellites = new();

    public ObservatoryAnalysis(List<List<char>> map, int size)
    {
        _map = map;
        size -= 1;
        AssignSatellites();
        EnlargeSpaceBetweenSatelliteRowsByFactor(size);
        EnlargeSpaceBetweenSatelliteColumnsByFactor(size);
    }

    public long SumDistancesBetweenAllPoints()
    {
        long sum = 0;
        for (int i = 0; i < _satellites.Count; i++)
        {
            var satellite = _satellites[i];
            for (int j = i + 1; j < _satellites.Count; j++)
            {
                var otherSatellite = _satellites[j];
                sum += CountManhattanDistanceInFromPointToOtherPoint(satellite, otherSatellite);
            }
        }

        return sum;
    }
    
    private int CountManhattanDistanceInFromPointToOtherPoint((int r, int c) point1, (int r, int c) point2)
    {
        return Math.Abs(point1.r - point2.r) + Math.Abs(point1.c - point2.c);
    }
    
    private void EnlargeSpaceBetweenSatelliteColumnsByFactor(int size)
    {
        var emptyColumns = new List<int>();
        var offset = 0;
        for (int c = _satellites.Min(s => s.c); c < _satellites.Max(s => s.c); c++)
        {
            if (_satellites.Select(s => s.c).Contains(c)) continue;
            emptyColumns.Add(c + offset);
            offset += size;
        }

        foreach (var emptyColumn in emptyColumns)
        {
            var newSatellites = new List<(int r, int c)>();
            foreach (var satellite in _satellites)
            {
                newSatellites.Add(satellite.c > emptyColumn ? (satellite.r, satellite.c + size) : satellite);
            }
            _satellites = newSatellites;
        }
    }

    private void EnlargeSpaceBetweenSatelliteRowsByFactor(int size)
    {
        var emptyRows = new List<int>();
        var offset = 0;
        for (int r = _satellites.Min(s => s.r); r < _satellites.Max(s => s.r); r++)
        {
            if (_satellites.Select(s => s.r).Contains(r)) continue;
            emptyRows.Add(r+offset);
            offset += size;
        }

        foreach (var emptyRow in emptyRows)
        {
            var newSatellites = new List<(int r, int c)>();
            foreach (var satellite in _satellites)
            {
                newSatellites.Add(satellite.r > emptyRow ? (satellite.r + size, satellite.c) : satellite);
            }
            _satellites = newSatellites;
        }
    }

    private void AssignSatellites()
    {
        for (var r = 0; r < _map.Count; r++)
        {
            var row = _map[r];
            for (int c = 0; c < row.Count; c++)
            {
                if (row[c] == '#')
                {
                    _satellites.Add((r, c));
                }
            }
        }
    }
}