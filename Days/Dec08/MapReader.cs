using System.Text.RegularExpressions;
using aoc_2022.Helpers;

namespace aoc_2022.Days.Dec08;

public class MapReader
{
    private readonly Dictionary<string, (string left, string right)> _map = new();
    private readonly List<char> _directions;
    private int _directionCounter = -1;
    private readonly List<string> _current = new();
    private readonly Dictionary<int, long> _cycles = new();
    private long _steps;
    private readonly bool _ghostMode;

    public MapReader(List<List<string>> input, bool ghostMode)
    {
        _ghostMode = ghostMode;
        foreach (var node in input[1])
        {
            var parsed = Regex.Match(node, "(.*) = \\((.*), (.*)\\)");
            _map.Add(parsed.Groups[1].Value, (parsed.Groups[2].Value,parsed.Groups[3].Value));
        }

        _directions = input[0][0].ToList();

        // Add starting nodes to current state
        if (ghostMode)
        {
            foreach (var node in _map)
            {
                if (node.Key.EndsWith("A")) _current.Add(node.Key);
            }    
        }
        else
        {
            _current.Add("AAA");
        }
    }

    public long Navigate()
    {
        // loop until all parallel ghosts have found a end 
        while (_cycles.Count != _current.Count)
        {
            TakeNextStep();
        }
        return _ghostMode ? Numerics.Lcm(_cycles.Select(k => k.Value).ToList()) : _cycles.First().Value;
    }

    private void TakeNextStep()
    {
        var direction = GetNextDirection();
        var next = "";
        _steps++;
        switch (direction)
        {
            // move each ghost to new node and update current state
            case 'R':
                for (int i = 0; i < _current.Count(); i++)
                {
                    next = _map[_current[i]].right;
                    _current[i] = next;
                    if (_current[i].EndsWith("Z")) _cycles.TryAdd(i, _steps);
                }
                break;  
            case 'L':
                for (int i = 0; i < _current.Count(); i++)
                {
                    next = _map[_current[i]].left;
                    _current[i] = next;
                    if (_current[i].EndsWith("Z")) _cycles.TryAdd(i, _steps);
                }
                break;    
            default:
                throw new Exception("Something is wrong!");
        }
    }

    private char GetNextDirection()
    {
        _directionCounter++;
        if (_directionCounter >= _directions.Count) _directionCounter = 0;
        return _directions[_directionCounter];
    }
}