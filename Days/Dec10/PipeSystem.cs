namespace aoc_2022.Days.Dec10;

public class PipeSystem
{
    private List<List<char>> _map;
    private List<(int, int)> _visited = new();
    private (int r, int c) _oneWay = new();
    private (int r, int c) _otherWay = new();
    private int _steps = 1;

    
    public PipeSystem(List<List<char>> map, char startChar)
    {
        _map = map;
        FindStartPoint(startChar);

    }

    public int FindFurthestPoint()
    {
        while (true)
        {
            TakeNextStep();
            _steps++;
            if (_oneWay == _otherWay) break;
        }

        return _steps;
    }

    private void TakeNextStep()
    {
        _visited.Add(_oneWay);
        _visited.Add(_otherWay);
        _oneWay = FindPossibleWays(_oneWay.r, _oneWay.c).Single();
        _otherWay = FindPossibleWays(_otherWay.r, _otherWay.c).Single();
    }

    private void FindStartPoint(char startChar)
    {
        for (int r = 0; r < _map.Count; r++)
        {
            for (int c = 0; c < _map[r].Count; c++)
            {
                if (_map[r][c] == 'S')
                {
                    SetStartingPointsForEachDirection(r,c, startChar);
                    return;
                }
            }
        }
    }

    private void SetStartingPointsForEachDirection(int r, int c, char startChar)
    {
        var options = FindPossibleWays(r, c, startChar);
        _oneWay = options[0];
        _otherWay = options[1];
        _visited.Add((r,c));
    }

    private List<(int r, int c)> FindPossibleWays(int r, int c, char startChar = '.')
    {
        var possible = new List<(int r, int c)>();
        var current = _map[r][c];
        if (startChar != '.') current = startChar;
        switch (current)
        {
            case '|':
                possible.Add((r-1,c));
                possible.Add((r+1,c));
                break;
            case '7':
                possible.Add((r,c-1));
                possible.Add((r+1,c));
                break;
            case 'J':
                possible.Add((r-1,c));
                possible.Add((r,c-1));
                break;
            case 'F':
                possible.Add((r+1,c));
                possible.Add((r,c+1));
                break;
            case 'L':
                possible.Add((r-1,c));
                possible.Add((r,c+1));
                break;
            case '-':
                possible.Add((r,c-1));
                possible.Add((r,c+1));
                break;
        }

        return possible.Where(e => !_visited.Contains(e) && e.r >= 0 && e.r < _map.Count && e.c >= 0 && e.c <= _map[e.r].Count).ToList();
    }
}