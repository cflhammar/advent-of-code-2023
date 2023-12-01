namespace aoc_2022.Days.Dec01;

public class CalorieCounter
{
    public int SummarizeCalories(List<List<string>> elfs, int numberOfElfs)
    {
        var caloriesByElf = new List<int>();

        foreach (var elf in elfs)
        {
            caloriesByElf.Add(elf.Select(Int32.Parse).Sum());
        }

        caloriesByElf.Sort();
        caloriesByElf.Reverse();

        return caloriesByElf.Take(numberOfElfs).Sum();
    }
}