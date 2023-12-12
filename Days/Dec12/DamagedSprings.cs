using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec12;

public class DamagedSprings
{

    public int FindCombinationsForAllSprings(List<List<string>> springs)
    {
        foreach (var spring in springs)
        {
            var combination = spring[0];
            var groups = spring[1].Split(",").ToList();
            var isOk = ValidateCombinationMatchesGroups(combination, groups);
        }

        return 1;
    }

    private bool ValidateCombinationMatchesGroups(string combination, List<string> groups)
    {
        var startGroupRegex = $"(\\.*#*)";
        var oneGroupRegex = "(\\.*#*\\.*)";
        var multipleGroupsRegex = String.Concat(Enumerable.Repeat(oneGroupRegex, groups.Count-2));
        var endGroupRegex = "(#*\\.*)";
        var regex = new Regex(startGroupRegex + multipleGroupsRegex + endGroupRegex);
        var match = regex.Match(combination);
        
        if (!match.Success) return false;
        if (match.Groups.Count - 1 != groups.Count) return false;
        for (int i = 1; i < match.Groups.Count; i++)
        {
            var m = match.Groups[i].Value.Replace(".", "");
            var len = m.Length;
            if (len != int.Parse(groups[i - 1])) return false;
        }

        return true;
    }
}