namespace aoc_2022.Days.Dec16;

public class MirrorMaze
{
    private readonly List<List<char>> _map;

    private record Beam((int r, int c) Pos, string Direction);

    private List<Beam> _beams = new();
    private readonly HashSet<(int r, int c, string direction)> _visited = new();

    public MirrorMaze(List<List<char>> map)
    {
        _map = map;
    }

    public int FindBestStart()
    {
        var max = 0;
        for (int r = 0; r < _map.Count; r++)
        {
            for (int c = 0; c < _map[0].Count; c++)
            {
                if (r == 0)
                {
                    var count = LetThereBeLight(r, c, "down");
                    if (count > max)
                    {
                        max = count;
                    }

                    _visited.Clear();
                    _beams.Clear();
                }

                if (r == _map.Count - 1)
                {
                    var count = LetThereBeLight(r, c, "up");
                    if (count > max)
                    {
                        max = count;
                    }

                    _visited.Clear();
                    _beams.Clear();
                }

                if (c == 0)
                {
                    var count = LetThereBeLight(r, c, "right");
                    if (count > max)
                    {
                        max = count;
                    }

                    _visited.Clear();
                    _beams.Clear();
                }

                if (c == _map[0].Count - 1)
                {
                    var count = LetThereBeLight(r, c, "left");
                    if (count > max)
                    {
                        max = count;
                    }

                    _visited.Clear();
                    _beams.Clear();
                }
            }
        }

        return max;
    }

    public int LetThereBeLight(int r, int c, string direction)
    {
        StartNewBeam(r, c, direction);
        while (_beams.Count > 0)
        {
            MoveAllBeams();
        }

        return CountLitUpTiles();
    }

    private int CountLitUpTiles()
    {
        var uniqueTiles = new HashSet<(int r, int c)>();
        foreach (var beam in _visited)
        {
            uniqueTiles.Add((beam.r, beam.c));
        }

        return uniqueTiles.Count;
    }

    private void MoveAllBeams()
    {
        var nextBeams = new List<Beam>();
        foreach (var beam in _beams)
        {
            var nextBeamsForThisBeam = MoveBeam(beam);
            if (nextBeamsForThisBeam != null) nextBeams.AddRange(nextBeamsForThisBeam);
        }

        _beams = nextBeams;
    }

    private List<Beam>? MoveBeam(Beam beam)
    {
        if (_visited.Contains((beam.Pos.r, beam.Pos.c, beam.Direction)))
        {
            return null;
        }

        _visited.Add((beam.Pos.r, beam.Pos.c, beam.Direction));

        (int r, int c) nextPos = beam.Pos;
        switch (beam.Direction)
        {
            case "up":
                nextPos = (beam.Pos.r - 1, beam.Pos.c);
                if (!IsInRange(nextPos)) return null;
                if (_map[nextPos.r][nextPos.c] == '-')
                {
                    return new List<Beam>() { new Beam(nextPos, "left"), new Beam(nextPos, "right") };
                }

                if (_map[nextPos.r][nextPos.c] == '/')
                {
                    return new List<Beam>() { new Beam(nextPos, "right") };
                }

                if (_map[nextPos.r][nextPos.c] == '\\')
                {
                    return new List<Beam>() { new Beam(nextPos, "left") };
                }

                break;
            case "down":
                nextPos = (beam.Pos.r + 1, beam.Pos.c);
                if (!IsInRange(nextPos)) return null;

                if (_map[nextPos.r][nextPos.c] == '-')
                {
                    return new List<Beam>() { new Beam(nextPos, "left"), new Beam(nextPos, "right") };
                }

                if (_map[nextPos.r][nextPos.c] == '/')
                {
                    return new List<Beam>() { new Beam(nextPos, "left") };
                }

                if (_map[nextPos.r][nextPos.c] == '\\')
                {
                    return new List<Beam>() { new Beam(nextPos, "right") };
                }

                break;
            case "right":
                nextPos = (beam.Pos.r, beam.Pos.c + 1);
                if (!IsInRange(nextPos)) return null;

                if (_map[nextPos.r][nextPos.c] == '|')
                {
                    return new List<Beam>() { new Beam(nextPos, "up"), new Beam(nextPos, "down") };
                }

                if (_map[nextPos.r][nextPos.c] == '/')
                {
                    return new List<Beam>() { new Beam(nextPos, "up") };
                }

                if (_map[nextPos.r][nextPos.c] == '\\')
                {
                    return new List<Beam>() { new Beam(nextPos, "down") };
                }

                break;
            case "left":
                nextPos = (beam.Pos.r, beam.Pos.c - 1);
                if (!IsInRange(nextPos)) return null;

                if (_map[nextPos.r][nextPos.c] == '|')
                {
                    return new List<Beam>() { new Beam(nextPos, "up"), new Beam(nextPos, "down") };
                }

                if (_map[nextPos.r][nextPos.c] == '/')
                {
                    return new List<Beam>() { new Beam(nextPos, "down") };
                }

                if (_map[nextPos.r][nextPos.c] == '\\')
                {
                    return new List<Beam>() { new Beam(nextPos, "up") };
                }

                break;
        }

        return new List<Beam>() { new Beam(nextPos, beam.Direction) };
    }

    private bool IsInRange((int r, int c) nextPos)
    {
        return nextPos.r >= 0 && nextPos.r < _map.Count && nextPos.c >= 0 && nextPos.c < _map[0].Count;
    }

    private void StartNewBeam(int r, int c, string direction)
    {
        _beams.Add(new Beam((r, c), direction));
    }
}