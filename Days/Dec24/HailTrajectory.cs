namespace aoc_2022.Days.Dec24;

public class HailTrajectory
{
    private readonly List<Hail> _hails;

    public HailTrajectory(List<List<string>> input)
    {
        _hails = new();
        foreach (var h in input)
        {
            var pos = h[0].Split(",");
            var v = h[1].Split(",");

            _hails.Add(new Hail
            {
                X = long.Parse(pos[0]),
                Y = long.Parse(pos[1]),
                Z = long.Parse(pos[2]),
                Dx = long.Parse(v[0]),
                Dy = long.Parse(v[1]),
                Dz = long.Parse(v[2]),
            });
        }
    }

    public int CountIntersections((long min, long max) range)
    {
        var sum = 0;
        for (int i = 0; i < _hails.Count() - 1; i++)
        {
            for (int j = i + 1; j < _hails.Count(); j++)
            {
                 var point = GetLineIntersection(_hails[i], _hails[j]);
                 if (point.x > range.min && point.x < range.max && point.y > range.min && point.y < range.max)
                 {
                     if (IntersectInFuture(_hails[i], _hails[j], point))
                     {
                         sum++;
                     }
                 }
            }
        }

        return sum;
    }

    private bool IntersectInFuture(Hail a, Hail b, (double x, double y) point)
    {
        if (a.Dx > 0 && point.x < a.X)
        {
            return false;
        }
        
        if (a.Dx < 0 && point.x > a.X)
        {
            return false;
        }
        
        if (b.Dx > 0 && point.x < b.X)
        {
            return false;
        }
        
        if (b.Dx < 0 && point.x > b.X)
        {
            return false;
        }

        return true;


    }

    private (double x, double y) GetLineIntersection(Hail a, Hail b)
    {
        var x = (b.M - a.M) / (a.K - b.K);
        var y = a.K * x + a.M;
        // var z = a.Z + (b.Z - a.Z) * (x - a.X) / (b.X - a.X);

        return (x, y);
    }
}

public class Hail
{
    public long X;
    public long Y;
    public long Z;

    public double Dx;
    public double Dy;
    public double Dz;

    public double K => Dy/Dx;
    public double M => Y - K * X;
}