using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec19;

public class RulesEngine
{
    private readonly List<Workflow> _workflows = new();
    private readonly List<ScrapMetal> _scrapMetals = new();
    private readonly Dictionary<string, List<ScrapMetal>> _sorted = new() { { "A", new() }, { "R", new() } };

    public RulesEngine(List<List<string>> input)
    {
        foreach (var rawRule in input[0])
        {
            var match = Regex.Match(rawRule, "(.*){(.*)}");
            var workflow = new Workflow(match.Groups[1].Value, match.Groups[2].Value.Split(","));
            _workflows.Add(workflow);
        }

        foreach (var metal in input[1])
        {
            var sm = new ScrapMetal(metal);
            _scrapMetals.Add(sm);
        }
    }

    // part 2
    public long CalculateCombinations()
    {
        var ranges = new Dictionary<string, (int min, int max)>()
        {
            { "x", (1, 4000) },
            { "m", (1, 4000) },
            { "a", (1, 4000) },
            { "s", (1, 4000) }
        };

        return FindRanges("in", ranges);
    }

    // part 2
    public long FindRanges(string workflowName, Dictionary<string, (int min, int max)> ranges)
    {
        // return size of 4D spaces
        if (workflowName == "A")
        {
            long sum = 1;
            foreach (var range in ranges)
            {
                sum *= range.Value.max - range.Value.min + 1;
            }
            return sum;
        }
        
        // invalid ranges
        if (workflowName == "R") return 0;
        
        // continue to split ranges until we reach the end
        long result = 0;
        var workflow = _workflows.SingleOrDefault(w => w.Name == workflowName);
        foreach (var rule in workflow.Rules)
        {
            
            if (rule.HasComparison && !rule.GreaterThan)
            {
                var (min, max) =  ranges[rule.ValueName];
                
                // entire range fits
                if (max < rule.Value)
                {
                    result += FindRanges(rule.Destination, ranges);
                    return result;
                }

                // range needs to be split
                if (min < rule.Value)
                {
                    var newRanges = new Dictionary<string, (int min, int max)>(ranges)
                    {
                        [rule.ValueName] = (min, rule.Value - 1)
                    };
                    
                    result += FindRanges(rule.Destination, newRanges);
                    ranges[rule.ValueName] = (rule.Value, max);
                }
            }
            else if (rule.HasComparison && rule.GreaterThan)
            {
                var (min, max) =  ranges[rule.ValueName];
                
                // entire range fits
                if (min > rule.Value)
                {
                    result += FindRanges(rule.Destination, ranges);
                    return result;
                }

                // split range as it does not fit
                if (max > rule.Value)
                {
                    var newRanges = new Dictionary<string, (int min, int max)>(ranges)
                    {
                        [rule.ValueName] = (rule.Value + 1, max)
                    };
                    
                    result += FindRanges(rule.Destination, newRanges);
                    ranges[rule.ValueName] = (min, rule.Value);
                }
            }
            else
            {
                // no comparison, just proceed
                result += FindRanges(rule.Destination, ranges);
            }
        }

        return result;
    }

    // part 1
    public int SortAll()
    {
        foreach (var metal in _scrapMetals)
        {
            Sort(metal);
        }
        return _sorted["A"].Sum(m => m.Xmas.Sum(v => v.Value));
    }

// part 1
    private void Sort(ScrapMetal metal)
    {
        var workflow = _workflows.SingleOrDefault(w => w.Name == "in");

        while (true)
        {
            foreach (var rule in workflow!.Rules)
            {
                if (!rule.HasComparison)
                {
                    if (rule.Destination == "A" || rule.Destination == "R")
                    {
                        _sorted[rule.Destination].Add(metal);
                        return;
                    }

                    workflow = _workflows.SingleOrDefault(w => w.Name == rule.Destination);
                    break;
                }

                var value = metal.Xmas[rule.ValueName];
                if (rule.GreaterThan)
                {
                    if (value > rule.Value)
                    {
                        if (rule.Destination == "A" || rule.Destination == "R")
                        {
                            _sorted[rule.Destination].Add(metal);
                            return;
                        }

                        workflow = _workflows.SingleOrDefault(w => w.Name == rule.Destination);
                        break;
                    }
                }
                else if (value < rule.Value)
                {
                    if (rule.Destination == "A" || rule.Destination == "R")
                    {
                        _sorted[rule.Destination].Add(metal);
                        return;
                    }

                    workflow = _workflows.SingleOrDefault(w => w.Name == rule.Destination);
                    break;
                }
            }
        }
    }
}

internal class ScrapMetal
{
    public readonly Dictionary<string, int> Xmas = new();

    public ScrapMetal(string metal)
    {
        var match = Regex.Match(metal, "{x=(.*),m=(.*),a=(.*),s=(.*)}");
        for (int i = 1; i < 5; i++)
        {
            Xmas.Add("xmas"[i - 1].ToString(), int.Parse(match.Groups[i].Value));
        }
    }
}

internal class Workflow
{
    public readonly string Name;
    public readonly List<Rule> Rules = new();

    public Workflow(string value, string[] rules)
    {
        Name = value;
        foreach (var rule in rules)
        {
            Rules.Add(new Rule(rule));    
        }
    }
}

internal class Rule
{
    public readonly string ValueName = "";
    public readonly bool GreaterThan;
    public readonly int Value;
    public readonly string Destination;
    public readonly bool HasComparison;

    public Rule(string input)
    {
        if (input.Contains(">") || input.Contains("<"))
        {
            GreaterThan = input.Contains(">");
            ValueName = new string(input.Take(1).ToArray());
            var comparison = new string(input.Skip(2).ToArray());
            var temp = comparison.Split(":");
            Value = int.Parse(temp[0]);
            Destination = temp[1];
            HasComparison = true;
        }
        else
        {
            Destination = input;
            HasComparison = false;
        }
    }
}