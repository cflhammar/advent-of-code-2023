namespace aoc_2022.Days.Dec17;

public class LavaTransporter
{
    private readonly List<List<int>> _map;
    private readonly HashSet<string> _visited = new();
    readonly PriorityQueue<Path, int> _queue = new();

    public LavaTransporter(List<List<char>> map)
    {
        _map = map.Select(r => r.Select(c => int.Parse(c.ToString())).ToList()).ToList();
    }

    public int FindPath(bool part2 = false)
    {
        _visited.Clear();
        _queue.Clear();
        var totalHeat = 0;
        _queue.Enqueue(new Path(new(0, 0), Direction.Right, 0), 0);

        while (_queue.Count > 0)
        {
            var path = _queue.Dequeue();

            if (path.Position.Row == _map.Count - 1 && path.Position.Col == _map[0].Count - 1)
            {
                return path.Heat;
            }

            if (!part2)
            {
                if (path.StraightLineLength < 3) TryMove(path, path.Direction);
                TryMove(path, path.Direction.TurnLeft());
                TryMove(path, path.Direction.TurnRight());
            }
            else
            {
                if (path.StraightLineLength < 10) TryMove(path, path.Direction);
                if (path.StraightLineLength >= 4)
                {
                    TryMove(path, path.Direction.TurnLeft());
                    TryMove(path, path.Direction.TurnRight());                    
                }
            }
        }

        return totalHeat;
    }

    private void TryMove(Path path, Direction direction)
    {
        var candidate = new Path(path.Position.Move(direction), direction,
            direction == path.Direction ? path.StraightLineLength + 1 : 1);

        if (candidate.Position.Row < 0 || candidate.Position.Row >= _map.Count ||
            candidate.Position.Col < 0 || candidate.Position.Col >= _map[0].Count)
        {
            return;
        }

        var key =
            $"({candidate.Position.Row},{candidate.Position.Col}),({candidate.Direction.Row},{candidate.Direction.Col}),{candidate.StraightLineLength}";
        if (_visited.Contains(key))
        {
            return;
        }


        _visited.Add(key);


        candidate.Heat = path.Heat + _map[candidate.Position.Row][candidate.Position.Col];
        // Console.WriteLine(key + "---" + candidate.Heat);
        _queue.Enqueue(candidate, candidate.Heat);
    }
}

internal class Path
{
    public Path(Position position, Direction direction, int straightLineLength)
    {
        Position = position;
        Direction = direction;
        StraightLineLength = straightLineLength;
    }

    public readonly Position Position;
    public readonly Direction Direction;
    public readonly int StraightLineLength;
    public int Heat { get; set; }
}

internal class Position
{
    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public readonly int Row;
    public readonly int Col;

    public Position Move(Direction dir)
    {
        return new Position(Row + dir.Row, Col + dir.Col);
    }
}

internal class Direction
{
    private Direction(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public readonly int Row;
    public readonly int Col;

    public Direction TurnLeft()
    {
        return new Direction(-Col, Row);
    }

    public Direction TurnRight()
    {
        return new Direction(Col, -Row);
    }

    public static Direction Up = new(-1, 0);
    public static Direction Down = new(1, 0);
    public static Direction Left = new(0, -1);
    public static Direction Right = new(0, 1);
}