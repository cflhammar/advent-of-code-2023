namespace aoc_2022.Helpers;

public class MatrixOperations
{
    public List<List<T>> Rotate90<T>(List<List<T>> input)
    {
        var rotated = new List<List<T>>();
        for (int i = 0; i < input[0].Count; i++)
        {
            rotated.Add(new List<T>());
        }

        for (int i = input.Count - 1; i >= 0; i--)
        {
            var row = input[i];
            for (int j = 0; j < rotated.Count; j++)
            {
                rotated[j].Add(row[j]);
            }
        }

        return rotated;
    }

    public List<List<T>> Flip<T>(List<List<T>> input)
    {
        foreach (var row in input)
        {
            row.Reverse();
        }

        return input;
    }
}