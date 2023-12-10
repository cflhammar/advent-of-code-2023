namespace aoc_2022.Days.Dec10;

public class PipeSystem
{
    private readonly List<List<char>> _map;
    private List<List<char>> _simpleMap = null!;
    private List<List<char>> _highResMap = null!;
    private readonly List<(int, int)> _visited = new();
    private (int r, int c) _oneWay;
    private (int r, int c) _otherWay;

    // higher resolution
    private (int r, int c) _highResWay;
    private readonly List<(int, int)> _highResVisited = new();
    private int _connectingSteps;
    private (int r, int y) _start;
    private bool _done;
    
    // fill
    private readonly List<(int r, int c)> _filled = new();

    public PipeSystem(List<List<char>> map, char startChar)
    {
        _map = map;
        FindStartPoint(startChar);
    }

    public (int length, int insideSize) Solve()
    {
        var length = FindFurthestPoint();
        
        Simplify();
        // Print(_simpleMap);

        IncreaseResolution();
        // Print(_highResMap);

        ConnectAllPipes();
        // Print(_highResMap);

        FloodFillOutsideSpace();
        // Print(_highResMap);

        ConvertToNormalResolution();
        // Print(_highResMap);

        var insideSize = CountInside();

        return (length, insideSize);
    }
    
    private int FindFurthestPoint()
    {
        var steps = 1;
        while (true)
        {
            TakeNextStep();
            steps++;
            if (_oneWay == _otherWay)
            {
                _visited.Add(_oneWay);
                break;
            }
        }
        
        return steps;
    }
    
    private void TakeNextStep()
    {
        _visited.Add(_oneWay);
        _visited.Add(_otherWay);
        _oneWay = FindPossibleWays(_oneWay.r, _oneWay.c).Single();
        _otherWay = FindPossibleWays(_otherWay.r, _otherWay.c).Single();
    }
    
    private void Simplify()
    {
        var paddedAndSimplified = new List<List<char>>();

        for (int r = 0; r < _map.Count(); r++)
        {
            paddedAndSimplified.Add(new List<char>());
            for (int c = 0; c < _map[r].Count; c++)
            {
                paddedAndSimplified.Last().Add(_visited.Contains((r, c)) ? _map[r][c] : '.');
            }
        }

        _simpleMap = paddedAndSimplified;
    }
    
    private void IncreaseResolution()
    {
        List<List<char>> highResMap = new();
        foreach (var r in _simpleMap)
        {
            highResMap.Add(new List<char>());
            foreach (var ch in r)
            {
                highResMap.Last().Add('.');
                highResMap.Last().Add(ch);
            }
            highResMap.Add(highResMap.Last().Select(_ => '.').ToList());
        }

        _highResMap = highResMap;
    }
    
    private void ConnectAllPipes()
    {
        FindHighResStart();

        while (true)
        {
            ConnectPipes();
            _connectingSteps++;
            if (_done) break;
        }
    }

    private void FindHighResStart()
    {
        for (int r = 0; r < _highResMap.Count; r++)
        {
            for (int c = 0; c < _highResMap[r].Count; c++)
            {
                if (_highResMap[r][c] == 'S')
                {
                    _start = (r, c);
                    _highResWay = _start;
                }
            }
        }
    }
    
    private void ConnectPipes()
    {
        _highResVisited.Add(_highResWay);

        var current = _highResWay;
        var nextList = FindNextConnectedPipe(current.r, current.c);

        (int r, int c) next = (0, 0);
        if (nextList.Any())
        {
            next = nextList.First();

            if (current.r < next.r) _highResMap[current.r + 1][current.c] = '*';
            if (current.r > next.r) _highResMap[current.r - 1][current.c] = '*';
            if (current.c < next.c) _highResMap[current.r][current.c + 1] = '*';
            if (current.c > next.c) _highResMap[current.r][current.c - 1] = '*';
        }
        else
        {
            _done = true;
        }

        _highResWay = next;
    }
    
    private void ConvertToNormalResolution()
    {
        List<List<char>> reduced = new();

        for (int r = 0; r < _highResMap.Count; r++)
        {
            if (r % 2 == 0)
            {
                var newRow = new List<char>();
                for (int c = 0; c < _highResMap[r].Count; c++)
                {
                    if (c % 2 != 0)
                    {
                        newRow.Add(_highResMap[r][c]);
                    }
                }
                reduced.Add(newRow);
            }
        }
        _highResMap = reduced;
    }
    
    private void FloodFillOutsideSpace()
    {
        for (int r = 0; r < _highResMap.Count; r++)
        {
            for (int c = 0; c < _highResMap[r].Count; c++)
            {
                if (c == 0 || c == _highResMap[r].Count-1 && _highResMap[r][c] == '.')
                {
                    Fill(r, c);
                }
            }
        }
    }

    private int CountInside()
    {
        var count = 0;
        foreach (var row in _highResMap)
        {
            for (var c = 0; c < _highResMap[0].Count; c++)
            {
                if (row[c] == '.') count++;
            }
        }

        return count;
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

    private List<(int r, int c)> FindNextConnectedPipe(int r, int c)
    {
        var possible = new List<(int r, int c)>();

        switch (_highResMap[r][c])
        {
            case '|':
                possible.Add((r-2,c));
                possible.Add((r+2,c));
                break;
            case '7':
                possible.Add((r,c-2));
                possible.Add((r+2,c));
                break;
            case 'J':
                possible.Add((r-2,c));
                possible.Add((r,c-2));
                break;
            case 'F':
                possible.Add((r+2,c));
                possible.Add((r,c+2));
                break;
            case 'S':
                possible.Add((r-2,c));
                possible.Add((r,c+2));
                break;
            case 'L':
                possible.Add((r-2,c));
                possible.Add((r,c+2));
                break;
            case '-':
                possible.Add((r,c-2));
                possible.Add((r,c+2));
                break;
        }

        if (possible.Contains(_start) && _connectingSteps > 10) return new List<(int r, int c)>(){_start};
        return possible.Where(e => !_highResVisited.Contains(e) && e.r >= 0 && e.r < _highResMap.Count && e.c >= 0 && e.c <= _highResMap[e.r].Count).ToList();
    }
    
    private void Fill(int r, int c)
    {
        if (r >= _highResMap.Count || c >= _highResMap[0].Count) return;
        _filled.Add((r,c));
        _highResMap[r][c] = 'o';
        if (r + 1 >= 0 && r + 1 < _highResMap.Count && _highResMap[r+1][c] == '.' && !_filled.Contains((r + 1, c))) 
            Fill(r+1,c);
        if (r - 1 >= 0 && r - 1 < _highResMap.Count && _highResMap[r-1][c] == '.' && !_filled.Contains((r - 1, c))) 
            Fill(r-1,c);
        if (c + 1 >= 0 && c + 1 < _highResMap[0].Count && _highResMap[r][c+1] == '.' && !_filled.Contains((r, c + 1))) 
            Fill(r,c+1);
        if (c - 1 >= 0 && c - 1 < _highResMap[0].Count && _highResMap[r][c-1] == '.' && !_filled.Contains((r, c - 1))) 
            Fill(r,c-1);
    }
    
    private void SetStartingPointsForEachDirection(int r, int c, char startChar)
    {
        var options = FindPossibleWays(r, c, startChar);
        _oneWay = options[0];
        _otherWay = options[1];
        _visited.Add((r,c));
    }

    private void Print(List<List<char>> toPrint)
    {
        foreach (var r in toPrint)
        {
            var row = string.Join("", r);
            Console.WriteLine(row);
        }
    }
}