using System.Diagnostics;
using aoc_2022.Days.Dec17;

namespace aoc_2022.Days.Dec18;

public class TrenchDigger
{

    public double Dig(List<string> input, bool part2 = false)
    {
        List<(long r, long c)> positions = new();
        double circumference = 0;
        (long r, long c) current = (0,0);
        positions.Add(current);

        foreach (var line in input)
        {
            var temp = line.Split(" ");
            var direction = temp[0];
            var distance = long.Parse(temp[1]);
            var hexColor = temp[2];

            if (part2)
            {
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
            circumference += distance;
        }
        
        var area = PolygonArea(positions);
        
        return area + circumference/2 + 1;
    }
    
    static double PolygonArea(List<(long Row, long Col)> polygon)
    {
        var n = polygon.Count;
        var result = 0.0;
        for (var i = 0; i < n - 1; i++)
        {
            result += polygon[i].Row * polygon[i + 1].Col - polygon[i + 1].Row * polygon[i].Col;
        }
 
        result = Math.Abs(result + polygon[n - 1].Row * polygon[0].Col - polygon[0].Row * polygon[n - 1].Col) / 2.0;
        return result;
    }
    
}