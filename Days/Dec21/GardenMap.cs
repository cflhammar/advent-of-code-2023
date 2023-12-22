namespace aoc_2022.Days.Dec21;

public class GardenMap
{
    private List<List<char>> _map;
    private List<(int r, int c)> _current = new();

    public GardenMap(List<List<char>> map)
    {
        _map = map;
        _current.Add(FindStart());

        // IsOpen(12, 0);
        // IsOpen(11, 0);
        // IsOpen(5, 0);
        // IsOpen(0, 0);
        // IsOpen(-1, 0);
        // IsOpen(-10, 0);
        // IsOpen(-11, 0);
        // IsOpen(-12, 0);
        // IsOpen(-19, 0);
        // IsOpen(-22, 0);
  
    }

    private (int r, int c) FindStart()
    {
        for (int i = 0; i < _map.Count(); i++)
        {
            for (int j = 0; j < _map[i].Count(); j++)
            {
                if (_map[i][j] == 'S') return (i, j);
            }
        }

        throw new Exception("hmmm");
    }

    public void Steps(int i)
    {
        for (var j = 0; j < i; j++)
        {
            Step();
        }
        
        Console.WriteLine(_current.Count);
    }

    private void Step()
    {
        var next = new List<(int r, int c)>();
        foreach (var tuple in _current)
        {
            var neighbors = GetOpenTiles(tuple);
            foreach (var neighbor in neighbors)
            {
                if (next.Contains(neighbor)) continue;
                next.Add(neighbor);
            }
        }

        _current = next;
    }

    private List<(int r, int c)> GetOpenTiles((int r, int c) pos)
    {
        var open = new List<(int r, int c)>();
        
        if (IsOpen(pos.r - 1, pos.c))
        {
            open.Add((pos.r - 1, pos.c));
        }
        if (IsOpen(pos.r + 1, pos.c))
        {
            open.Add((pos.r + 1, pos.c));
        }
        if (IsOpen(pos.r, pos.c - 1))
        {
            open.Add((pos.r, pos.c - 1));
        }
        if (IsOpen(pos.r, pos.c + 1))
        {
            open.Add((pos.r, pos.c + 1));
        }

        return open;
    }

    bool IsOpen(int r, int c)
    {
        var normalizeRow = 0;
        var normalizeCol = 0;
        
        if (r >= 0) normalizeRow = r % _map.Count;
        else if (r < 0 && r > -_map.Count)
        {
            normalizeRow =  _map.Count() - Math.Abs(-r % _map.Count);    
        }
        else
        {
            normalizeRow = (_map.Count - Math.Abs(-r % _map.Count)) % _map.Count ;
        }
        
        if (c >= 0) normalizeCol = c % _map[0].Count;
        else if (c < 0 && c > -_map[0].Count)
        {
            normalizeCol =  _map[0].Count() - Math.Abs(-c % _map[0].Count);    
        }
        else
        {
            normalizeCol = (_map[0].Count - Math.Abs(-c % _map[0].Count)) % _map[0].Count ;
        }
        
        // Console.WriteLine(r +" -> "+ normalizeRow );
        
        return _map[normalizeRow][normalizeCol] != '#';
    }
}