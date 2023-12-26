namespace aoc_2022.Days.Dec25;

public class Rewire
{

    public int SeparateGroups(List<List<string>> input)
    {
        var groups = ParseInput(input);
        var allConnections = GetAllConnections(groups);

        for (int i = 0; i < allConnections.Count(); i++)
        {
            for (int j = i + 1; j < allConnections.Count(); j++)
            {
                for (int k = j + 1; k < allConnections.Count(); k++)
                {
                    var con1 = allConnections.ElementAt(i);
                    var con2 = allConnections.ElementAt(j);
                    var con3 = allConnections.ElementAt(k);
                    
                    groups[con1.a].Connections.Remove(con1.b);
                    groups[con1.b].Connections.Remove(con1.a);
                    
                    groups[con2.a].Connections.Remove(con2.b);
                    groups[con2.b].Connections.Remove(con2.a);
                    
                    groups[con3.a].Connections.Remove(con3.b);
                    groups[con3.b].Connections.Remove(con3.a);
                    
                    var numberOfGroupsReached = CanReachNumberOfGroups(groups);
                    
                    if (numberOfGroupsReached != groups.Count)
                    {
                        Console.WriteLine($"Found it! {con1.a}-{con1.b}, {con2.a}-{con2.b}, {con3.a}-{con3.b}");
                        return numberOfGroupsReached * (groups.Count - numberOfGroupsReached);
                    }
                    else
                    {
                        Console.WriteLine($"Did not found it! {con1.a}-{con1.b}, {con2.a}-{con2.b}, {con3.a}-{con3.b}");
                        
                    }

                    groups[con1.a].Connections.Add(con1.b);
                    groups[con1.b].Connections.Add(con1.a);
                    
                    groups[con2.a].Connections.Add(con2.b);
                    groups[con2.b].Connections.Add(con2.a);
                    
                    groups[con3.a].Connections.Add(con3.b);
                    groups[con3.b].Connections.Add(con3.a);

                }
            }
        }

        return -1;
    }

    private int CanReachNumberOfGroups(Dictionary<string, Group> groups)
    { 
        var visited = new HashSet<string>();
        var queue = new Queue<string>();
        queue.Enqueue(groups.First().Key);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current)) continue;
            visited.Add(current);
            foreach (var connection in groups[current].Connections)
            {
                queue.Enqueue(connection);
            }
        }

        return visited.Count;
    }

    private HashSet<(string a, string b)> GetAllConnections(Dictionary<string, Group> groups)
    {
        var allConnections = new HashSet<(string a, string b)>();
        foreach (var group in groups)
        {
            foreach (var connection in group.Value.Connections)
            {
                if (allConnections.Contains((group.Key, connection)) ||
                    allConnections.Contains((connection, group.Key))) continue;
                allConnections.Add((group.Key, connection));
            }
        }

        return allConnections;
    }

    private Dictionary<string,Group> ParseInput(List<List<string>> input)
    {
        var groups = new Dictionary<string, Group>();
        foreach (var line in input)
        {
            var name = line[0];
            var connections = line[1].Split(" ").ToList();
            if (groups.ContainsKey(name))
            {
                groups[name].Connections.AddRange(connections);
            }
            else
            {
                groups.Add(name, new Group {Name = name, Connections = connections});
            }

            foreach (var connection in connections)
            {
                if (groups.ContainsKey(connection))
                {
                    groups[connection].Connections.Add(name);
                }
                else
                {
                    groups.Add(connection, new Group {Name = connection, Connections = new List<string> {name}});
                }
            }
            
        }

        return groups;
    }
}


public class Group
{
    public string Name;
    public List<string> Connections;
}