using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec15;

public class HolidayHasher
{

    public int ArrangeLenses(List<string> input)
    {
        var boxes = Enumerable.Range(0,256).Select(_ => new Box()).ToList();
        
        foreach (var instruction in input)
        {
            var data = Regex.Split(instruction, "=|-");
            var assignment = instruction.Contains("=");
            var lens = data[0];
            var box = Hash(data[0]);
            var focalLength = data[1].Length > 0 ? int.Parse(data[1]) : 0;


            if (assignment)
            {
                // replace old lens
                if (boxes[box].Lenses.Any(x => x.Item1 == lens))
                {
                    var oldLens = boxes[box].Lenses.First(x => x.Item1 == lens);
                    var index = boxes[box].Lenses.IndexOf(oldLens);
                    boxes[box].Lenses[index] = (lens ,focalLength);
                }
                // add new lens
                else
                {
                    boxes[box].Lenses.Add((lens, focalLength));
                }
            }
            else
            {
                // remove lens
                if (boxes[box].Lenses.Any(x => x.Item1 == lens))
                {
                    var oldLens = boxes[box].Lenses.First(x => x.Item1 == lens);
                    boxes[box].Lenses.Remove(oldLens);
                }
            }
        }
        
        var sum = CalculateSum(boxes);

        return sum;
    }

    private int CalculateSum(List<Box> boxes)
    {
        var sum = 0;
        for (var index = 0; index < boxes.Count; index++)
        {
            var box = boxes[index];
            for (var lensIndex = 0; lensIndex < box.Lenses.Count; lensIndex++)
            {
                var lens = box.Lenses[lensIndex];
                var boxSum = (index + 1) * (lensIndex + 1) * lens.Item2;
                sum += boxSum;
            }
        }

        return sum;
    }


    public int SumHashes(List<string> input)
    {
        var sum = 0;
        foreach (var s in input)
        {
            sum += Hash(s);
        }

        return sum;
    }
    
    public int Hash(string input)
    {
        var current = 0;
        
        foreach (var c in input)
        {
            var ascii = (int) c;
            current += ascii;
            current *= 17;
            current %= 256;
        }

        return current;
    }
}


public class Box
{
    public List<(string, int)> Lenses = new();

}