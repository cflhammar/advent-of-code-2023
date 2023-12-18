using System.Diagnostics;
using aoc_2022.Days.Dec17;

namespace aoc_2022.Days.Dec18;

public class TrenchDigger
{
    HashSet<(long r, long c)> positions = new();
    HashSet<(long r, long c)> digged = new();
    public long Dig(List<string> input)
    {
        (long r, long c) current = (0,0);
        positions.Add(current);

        foreach (var line in input)
        {
            var temp = line.Split(" ");
            var direction = temp[0];
            var distance = long.Parse(temp[1]);
            var hexColor = temp[2];

            var hexDistance = new string(hexColor.Skip(2).Take(5).ToArray()) ;
            distance = Convert.ToInt64(hexDistance, 16);
            
            var hexDirection = new string(hexColor.Skip(7).Take(1).ToArray());
            switch (hexDirection)
            {
                case "0": direction = "R"; break;
                case "1": direction = "D"; break;
                case "2": direction = "L"; break;
                case "3": direction = "U"; break;

            }
            
            
            switch (direction)
            {
                case "U":
                    for (int r = 1; r <= distance; r++)
                    {
                        positions.Add((current.r - r, current.c));
                    }
                    current.r -= distance;
                break;
                case "D":
                    for (int r = 1; r <= distance; r++)
                    {
                        positions.Add((current.r + r, current.c));
                    }
                    current.r += distance;
                    break;
                case "R":
                    for (int c = 1; c <= distance; c++)
                    {
                        positions.Add((current.r, current.c + c));
                    }
                    current.c += distance;
                    break;
                case "L":
                    for (int c = 1; c <= distance; c++)
                    {
                        positions.Add((current.r, current.c - c));
                    }
                    current.c -= distance;
                    break;
            }
        }
        
        var size = FloodFill((1,1));
        // Print();
        return positions.Count + size;
    }

    private long FloodFill((long r, long c) pos)
    {
        if (positions.Contains(pos)) return 0;
        if (digged.Contains(pos)) return 0;
        digged.Add(pos);

        long sum = 1;
        if (!positions.Contains((pos.r+1,pos.c)) && !digged.Contains((pos.r+1,pos.c))) sum += FloodFill((pos.r + 1, pos.c));
        if (!positions.Contains((pos.r-1,pos.c)) && !digged.Contains((pos.r-1,pos.c))) sum += FloodFill((pos.r - 1, pos.c));
        if (!positions.Contains((pos.r,pos.c+1)) && !digged.Contains((pos.r,pos.c+1))) sum += FloodFill((pos.r, pos.c+1));
        if (!positions.Contains((pos.r,pos.c-1)) && !digged.Contains((pos.r,pos.c-1))) sum += FloodFill((pos.r, pos.c-1));

        return sum;
    }
    
    private void Print()
    {
        for (long r = positions.Min(e => e.r); r <= positions.Max(e => e.r); r++)
        {
            string row = "";
            for (long c = positions.Min(e => e.c); c <= positions.Max(e => e.c); c++)
            {
                if (r == 0  && c == 0) row += "0";
                else if (positions.Contains((r,c))) row += "#";
                else row += ".";
            }
            Console.WriteLine(row);
        }
    }
}