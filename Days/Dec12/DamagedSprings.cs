using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec12;

public class DamagedSprings
{

    public int FindCombinationsForAllSprings(List<List<string>> springs)
    {
        var validCombinations = 0;
        foreach (var spring in springs)
        {
            var groups = spring[1].Split(",").Select(int.Parse).ToList();
            var totalHashes = groups.Sum();
            var combinations = GetAllPossibleCombinations(spring[0], totalHashes);
            foreach (var combination in combinations)
            {
                if (ValidateCombinationMatchesGroups(combination, groups)) validCombinations++;
            }
        }

        return validCombinations;
    }

    private List<string> GetAllPossibleCombinations(string s, int total)
    {
        var combinations = new List<string>();
        var length = s.Count(c => c == '?');
        var numberOfCombinations = Math.Pow(2, length);
        
        var binaryCombinations = new List<string>();
        for(int i=0; i < numberOfCombinations; i++)
        {
            string binary = Convert.ToString(i, 2);
            string leading_zeroes = "0000000000000000000000000000000000000000000000000000000000000000000000000".Substring(0,length-binary.Length);
            string final = leading_zeroes + binary;
            binaryCombinations.Add(final);
        }

        foreach (var binaryCombination in binaryCombinations)
        {
            var combination = "";
            var index = 0;
            foreach (var ch in s)
            {
                if (ch != '?') combination += ch;
                else
                {
                    if (binaryCombination[index] == '0') combination += '.';
                    else combination += '#';
                    index++;
                }
            }

            var hashes = combination.Count(c => c == '#');
            if (hashes == total) combinations.Add(combination);
        }
        
        return combinations;
    }

    private bool ValidateCombinationMatchesGroups(string combination, List<int> groups)
    {
        var startGroupRegex = $"(\\.*#*)";
        var oneGroupRegex = "(\\.*#*\\.*)";
        var multipleGroupsRegex = String.Concat(Enumerable.Repeat(oneGroupRegex, groups.Count-2));
        var endGroupRegex = "(#*\\.*)";

        string regexPattern = "";
        if (groups.Count == 1) regexPattern = oneGroupRegex;
        if (groups.Count == 2) regexPattern = startGroupRegex + oneGroupRegex;
        if (groups.Count > 2) regexPattern = startGroupRegex + multipleGroupsRegex + endGroupRegex;
        
        var regex = new Regex(regexPattern);
        var match = regex.Match(combination);
        
        if (!match.Success) return false;
        if (match.Groups.Count - 1 != groups.Count) return false;
        for (int i = 1; i < match.Groups.Count; i++)
        {
            var m = match.Groups[i].Value.Replace(".", "");
            var len = m.Length;
            if (len != groups[i - 1]) return false;
        }

        return true;
    }

    public int FindTripleCombinationsForAllSprings(List<List<string>> input)
    {
        foreach (var row in input)
        {  
            row[0] = row[0] + "?" + row[0]  + "?" + row[0] + "?" + row[0] + "?" + row[0];
            row[1] = row[1] + "," + row[1]  + "," + row[1] + "," + row[1] + "," + row[1];
        }

        return FindCombinationsForAllSprings(input);
    }
}