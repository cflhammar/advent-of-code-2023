namespace aoc_2022.Days.Dec25;

public class Rewire
{

    public int SeparateGroups(List<List<string>> input)
    {
        var groups = ParseInput(input);

        var connectionsCounter = new Dictionary<string, int>();
        for (int i = 0; i < 500; i++)
        {
            var r = new Random();
            var rand = r.Next(0, groups.Count);
            var start = groups.ElementAt(rand).Key;
            rand = r.Next(0, groups.Count);
            var end = groups.ElementAt(rand).Key;
            
            var visited = GoFromGroupToGroup(start,end, groups);

            for (int j = 1; j < visited.Count; j++)
            {
                var connection = visited[j - 1] + "-" + visited[j];
                var connection2 = visited[j] + "-" + visited[j - 1];
                
                if (connectionsCounter.ContainsKey(connection))
                {
                    connectionsCounter[connection]++;
                }
                else if (connectionsCounter.ContainsKey(connection2))
                {
                    connectionsCounter[connection2]++;
                }
                else
                {
                    connectionsCounter.Add(connection, 1);
                }
            }
            
        }

        var top3 = connectionsCounter.OrderByDescending(e => e.Value).Take(3).Select(kvp => kvp.Key).Select(s => s.Split("-").ToList()).ToList();
        
        groups[top3[0][0]].Remove(top3[0][1]);
        groups[top3[0][1]].Remove(top3[0][0]);
        
        groups[top3[1][0]].Remove(top3[1][1]);
        groups[top3[1][1]].Remove(top3[1][0]);
        
        groups[top3[2][0]].Remove(top3[2][1]);
        groups[top3[2][1]].Remove(top3[2][0]);
        
        var numberOfGroupsReached = CanReachNumberOfGroups(groups);
        return numberOfGroupsReached * (groups.Count - numberOfGroupsReached);
    }

    private List<string> GoFromGroupToGroup(string groupOne, string groupTwo, Dictionary<string, List<string>> groups)
    { 
        var visited = new HashSet<string>();
        var queue = new Queue<(string group, List<string> visited)>();
        queue.Enqueue((groupOne, new List<string>()));
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.group == groupTwo)
            {
                return current.visited;
            }
            visited.Add(current.group);
            foreach (var connection in groups[current.group])
            {
                if (visited.Contains(connection)) continue;
                var newVisited = new List<string>(current.visited) { connection };
                queue.Enqueue((connection, newVisited));
            }
        }

        throw new Exception();
    }
    
    private static int CanReachNumberOfGroups(Dictionary<string, List<string>> groups)
    { 
        var visited = new HashSet<string>();
        var queue = new Queue<string>();
        queue.Enqueue(groups.First().Key);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current)) continue;
            visited.Add(current);
            foreach (var connection in groups[current])
            {
                queue.Enqueue(connection);
            }
        }

        return visited.Count;
    }

    private Dictionary<string,List<string>> ParseInput(List<List<string>> input)
    {
        var groups = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var name = line[0];
            var connections = line[1].Split(" ").ToList();
            if (groups.ContainsKey(name))
            {
                groups[name].AddRange(connections);
            }
            else
            {
                groups.Add(name, new List<string>(connections));
            }

            foreach (var connection in connections)
            {
                if (groups.ContainsKey(connection))
                {
                    groups[connection].Add(name);
                }
                else
                {
                    groups.Add(connection, new List<string> {name});
                }
            }
        }

        return groups;
    }
}