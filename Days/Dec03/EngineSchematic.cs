namespace aoc_2022.Days.Dec03;

public class EngineSchematic
{
    private readonly List<List<char>> _schematic;
    private Dictionary<string, List<int>> _memory = new();


    public EngineSchematic(List<List<char>> schematic)
    {
        _schematic = schematic;
    }

    public int SumGearProducts()
    {
        var sum = 0;
        foreach (var keyValuePair in _memory)
        {
            if (keyValuePair.Value.Count == 2) sum += keyValuePair.Value.Aggregate(1, (x, y) => x * y);
        }

        return sum;
    }
    
    public int SumPartNumbers()
    {
        var sum = 0;
        
        for (int row = 0; row < _schematic.Count; row++)
        {
            for (int col = 0; col < _schematic[row].Count; col++)
            {
                if (Char.IsDigit(_schematic[row][col]))
                {
                    var len = FindLengthOfNumber(row, col);
                    var number = FindNumber(row, col, len);
                    var sign = FindAdjacentSign(row, col, len, number);
                    col += len-1;

                    if (sign != '.')
                    {
                        sum += number;
                        
                    }
                }
            }
        }

        return sum;
    }

    private int FindNumber(int row, int col, int len)
    {
        var number = "";
        for (int c = col; c < col + len; c++)
        {
            number += _schematic[row][c];
        }

        return int.Parse(number);
    }

    private char FindAdjacentSign(int row, int col, int len, int number)
    {
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + len; c++)
            {
                if (r >= 0 && r < _schematic.Count && c >= 0 && c < _schematic[row].Count)
                {
                    char ch = _schematic[r][c];
                    if (ch != '.' && !Char.IsDigit(ch))
                    {
                        if (ch == '*') Memorize(r,c, number);
                        return ch;
                    }
                }
            }
        }

        return '.';
    }

    private void Memorize(int row, int col, int number)
    {
        var key = $"{row}-{col}";
        if (_memory.TryGetValue(key, out var value))
        {
            value.Add(number);
        }
        else
        {
            var list = new List<int>();
            list.Add(number);
            _memory.Add(key, list);
        }
    }

    private int FindLengthOfNumber(int row, int col)
    {
        var length = 1;
        for (int i = col + 1; i < _schematic[row].Count; i++)
        {
            if (!Char.IsDigit(_schematic[row][i]))
            {
                break;
            }
            length++;
        }

        return length;
    }
}