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
            var regex = "(.*){(.*)}";
            var match = Regex.Match(rawRule, regex);
            var name = match.Groups[1].Value;
            var rules = match.Groups[2].Value.Split(",");
            var workflow = new Workflow { Name = name };
            foreach (var rule in rules)
            {
                var parsedRule = new Rule();

                if (rule.Contains(">") || rule.Contains("<"))
                {
                    parsedRule.GreaterThan = rule.Contains(">");
                    parsedRule.ValueName = new string(rule.Take(1).ToArray());
                    var comparison = new string(rule.Skip(2).ToArray());
                    var temp = comparison.Split(":");
                    parsedRule.Value = int.Parse(temp[0]);
                    parsedRule.Destination = temp[1];
                    parsedRule.NoComparison = false;
                }
                else
                {
                    parsedRule.Destination = rule;
                    parsedRule.NoComparison = true;
                }

                workflow.Rules.Add(parsedRule);
            }

            _workflows.Add(workflow);
        }

        foreach (var metal in input[1])
        {
            var regex = "{x=(.*),m=(.*),a=(.*),s=(.*)}";
            var match = Regex.Match(metal, regex);
            var sm = new ScrapMetal();
            for (int i = 1; i < 5; i++)
            {
                sm.Xmas.Add("xmas"[i - 1].ToString(), int.Parse(match.Groups[i].Value));
            }

            _scrapMetals.Add(sm);
        }
    }


    public int SortAll()
    {
        foreach (var metal in _scrapMetals)
        {
            Sort(metal);
        }

        var sum = _sorted["A"].Sum(m => m.Xmas.Sum(v => v.Value));
        
        return sum;
    }


    private void Sort(ScrapMetal metal)
    {
        var workflow = _workflows.SingleOrDefault(w => w.Name == "in");

        while (true)
        {
            foreach (var rule in workflow!.Rules)
            {
                if (rule.NoComparison)
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
}

internal class Workflow
{
    public string Name = "";
    public readonly List<Rule> Rules = new();
}

internal class Rule
{
    public string ValueName = "";
    public bool GreaterThan;
    public int Value;
    public string Destination = "";
    public bool NoComparison;
}