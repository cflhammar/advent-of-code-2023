namespace aoc_2022.Days;

public interface ISolver
{
    public string Date { get; }
    public void Solve();

    public dynamic ParseInput(string fileName);

}