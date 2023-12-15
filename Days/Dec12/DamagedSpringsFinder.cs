namespace aoc_2022.Days.Dec12;

public class DamagedSpringsFinder
{ 
    Dictionary<string,long> cache = new();
    
    public long SumCombinations(List<List<string>> springs)
    {
        long validCombinations = 0;
        foreach (var spring in springs)
        {
            validCombinations += Calculate(spring[0], spring[1].Split(",").Select(int.Parse).ToList());
        }

        return validCombinations;
    }

    public long SumCombinationsTimesFive(List<List<string>> springs)
    {
        var fiveTimes = springs.Select(s => new List<string>
        {
            string.Join('?', Enumerable.Repeat(s[0], 5)),
            string.Join(",", Enumerable.Repeat(s[1], 5))
        }).ToList();

        return SumCombinations(fiveTimes);
    }

    private long Calculate(string springs, List<int> groups)
    {
        var key = springs + string.Join(',', groups);
        
        if (cache.TryGetValue(key, out var value))
        {
            return value;
        }

        value = CountCombination(springs, groups);
        cache.Add(key, value);
        return value;
    }

    private long CountCombination(string springs, List<int> groups)
    {
        while (true)
        {
            // no groups are left, if no # remains return 1 as spring is valid
            if (groups.Count == 0)
            {
                return springs.Contains('#') ? 0 : 1;
            }

            // if there are groups but no springs, return 0 as spring is invalid
            if (string.IsNullOrEmpty(springs))
            {
                return 0;
            }

            // trim all starting . and try again 
            if (springs.StartsWith('.'))
            {
                springs = springs.Trim('.');
                continue;
            }

            // Assign possible char use recursion
            if (springs.StartsWith('?'))
            {
                return Calculate('.' + springs[1..], new List<int>(groups)) +
                       Calculate('#' + springs[1..], new List<int>(groups));
            }

            if (springs.StartsWith('#'))
            {
                // if there are less springs than than in the group, return 0 as spring is invalid
                if (springs.Length < groups[0])
                {
                    return 0;
                }

                // if a . exist in the next group, return 0 as spring is invalid
                if (springs[..groups[0]].Contains('.'))
                {
                    return 0;
                }
                
                if (groups.Count > 1)
                {
                    // As there are more than 1 group left, spring cannot be shorter than next group plus 1
                    if (springs.Length < groups[0] + 1)
                    {
                        return 0;
                    }

                    // if there is a # after the group spring cannot be valid as group must be bigger to be valid
                    if (springs[groups[0]] == '#')
                    {
                        return 0;
                    }

                    // everything is OK so far
                    // remove the next group from spring and from groups
                    springs = springs[(groups[0] + 1)..];
                    groups.RemoveAt(0);
                    continue;
                }

                // this is the last group
                springs = springs[groups[0]..];
                groups.RemoveAt(0);
                continue;
            }

            throw new Exception();
        }
    }
}