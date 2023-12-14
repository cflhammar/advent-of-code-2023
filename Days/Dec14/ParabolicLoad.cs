namespace aoc_2022.Days.Dec14;

public class ParabolicLoad
{
    private int maxRow = 0;
    private int maxCol = 0;

    public (int part1, int part2) CalculateLoad(List<List<char>> input, int totalCycles)
    {
        maxRow = input.Count;
        maxCol = input[0].Count;
        bool found = false;
        (int part1, int part2) result = (0,0);
        
        (List<(int,int)> movingStones, List<(int,int)> fixedStones) = GetCoordinatesOfStones(input);
        var memory = new Dictionary<int, int>();

        List<(int, int)> tiltedStones = movingStones;
        for (int i = 0; i < totalCycles; i++)
        {
            tiltedStones = TiltNorth(tiltedStones, fixedStones);
            if (i == 0) result.part1 = CountLoad(tiltedStones, input.Count);
            tiltedStones = TiltWest(tiltedStones, fixedStones);
            tiltedStones = TiltSouth(tiltedStones, fixedStones);
            tiltedStones = TiltEast(tiltedStones, fixedStones);
            
            // if cycle is found no need to use the memory
            if (!found)
            {
                // sort state before creating hash
                tiltedStones.Sort((x, y) => {
                    int result = x.Item1.CompareTo(y.Item1);
                    return result == 0 ? x.Item2.CompareTo(y.Item2) : result;
                });

                var hashKey = string.Join(",", tiltedStones.Select(x => string.Format("{0}-{1}", x.Item1, x.Item2))).GetHashCode();
                if (memory.ContainsKey(hashKey))
                {
                    found = true;
                    var cycle = i - memory[hashKey];
                    var remaining = totalCycles - i;
                    var cycles = remaining / cycle;
                    i += cycles * cycle;
                    continue;
                }

                memory.Add(hashKey, i);
            }
        }
        
        result.part2 = CountLoad(tiltedStones, input.Count);
        return result;
    }

    private int CountLoad(List<(int r, int c)> tiltedStones, int maxRow)
    {
        var load = 0;
        foreach (var stone in tiltedStones)
        {
            load += maxRow-stone.r;
        }

        return load;
    }

    private List<(int r, int c)> TiltNorth(List<(int r, int c)> movingStones, List<(int r, int c)> fixedStones)
    {
        var newPositions = new List<(int,int)>();
        movingStones.Sort((x, y) => x.Item1.CompareTo(y.Item1));
        foreach (var stone in movingStones)
        {
            var newRow = stone.r;
            var column = stone.c;
            for (int r = stone.r - 1; r >= 0; r--)
            {
                if (fixedStones.Contains((r, column)))
                {
                    break;
                }

                if (newPositions.Contains((r, column)))
                {
                    break;
                }

                newRow = r;
            }
            newPositions.Add((newRow,column));
        }

        return newPositions;
    }

    private List<(int r, int c)> TiltWest(List<(int r, int c)> movingStones, List<(int r, int c)> fixedStones)
    {
        var newPositions = new List<(int,int)>();
        movingStones.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        foreach (var stone in movingStones)
        {
            var row = stone.r;
            var newColumn = stone.c;
            for (int c = stone.c - 1; c >= 0; c--)
            {
                if (fixedStones.Contains((row, c)))
                {
                    break;
                }

                if (newPositions.Contains((row, c)))
                {
                    break;
                }

                newColumn = c;
            }
            newPositions.Add((row,newColumn));
        }

        return newPositions;
    }

    private List<(int r, int c)> TiltSouth(List<(int r, int c)> movingStones, List<(int r, int c)> fixedStones)
    {
        var newPositions = new List<(int,int)>();
        movingStones.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        foreach (var stone in movingStones)
        {
            var newRow = stone.r;
            var column = stone.c;
            for (int r = stone.r + 1; r < maxRow; r++)
            {
                if (fixedStones.Contains((r, column)))
                {
                    break;
                }

                if (newPositions.Contains((r, column)))
                {
                    break;
                }

                newRow = r;
            }
            newPositions.Add((newRow,column));
        }

        return newPositions;
    }
    
    private List<(int r, int c)> TiltEast(List<(int r, int c)> movingStones, List<(int r, int c)> fixedStones)
    {
        var newPositions = new List<(int,int)>();
        movingStones.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        foreach (var stone in movingStones)
        {
            var row = stone.r;
            var newColumn = stone.c;
            for (int c = stone.c + 1; c < maxCol; c++)
            {
                if (fixedStones.Contains((row, c)))
                {
                    break;
                }

                if (newPositions.Contains((row, c)))
                {
                    break;
                }

                newColumn = c;
            }
            newPositions.Add((row,newColumn));
        }

        return newPositions;
    }

    private (List<(int r,int c)>, List<(int r,int c)>) GetCoordinatesOfStones(List<List<char>> input)
    {
        var movingStones = new List<(int x, int y)>();
        var fixedStones = new List<(int x, int y)>();

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == 'O')
                {
                    movingStones.Add((i,j));
                }
                if (input[i][j] == '#')
                {
                    fixedStones.Add((i,j));
                }
            }
        }

        return (movingStones, fixedStones);
    }
    
    // private void Print(List<(int r, int c)> movingStones, List<(int r, int c)> fixedStones)
    // {
    //     Console.WriteLine("---------------------");
    //     
    //     for (int r = 0; r < Math.Max(fixedStones.Max(e => e.r), movingStones.Max(e => e.r)) ; r++)
    //     {
    //         var row = "";
    //         for (int c = 0; c < Math.Max(fixedStones.Max(e => e.c), movingStones.Max(e => e.c)) ; c++)
    //         {
    //             if (movingStones.Contains((r,c)))
    //             {
    //                 row += "O";
    //             }
    //             else if (fixedStones.Contains((r,c)))
    //             {
    //                 row += "#";
    //             }
    //             else
    //             {
    //                 row += ".";
    //             }
    //         }       
    //         Console.WriteLine(row);
    //     }
    // }
}