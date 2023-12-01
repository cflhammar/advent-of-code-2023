namespace aoc_2022.Helpers;

public class InputReader
{
    public string GetFileContent(string day, string fileName)
    { 
        var projectPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())!.ToString())!.ToString());
        var filePath = $"{projectPath}/Days/{day}/InputData/{fileName}.txt";
        var text = File.ReadAllText(filePath);

        return text;
    }
    
    public List<string> SplitByRow(string text)
    {
        var lines = text.Split("\n").ToList();

        return lines;
    }
    
    public List<string> SplitByEmptyRow(string text)
    {
        var lines = text.Split("\n\n").ToList();

        return lines;
    }
    
    public List<List<char>> SplitListOfStringToListListOfCharByNoDelimeter(List<string> input)
    {
        var output = input.Select(x => x.ToList()).ToList();
        return output;
    }
    
    public List<List<string>> SplitListOfStringToListListOfStringByDelimeter(List<string> input, string delimeter)
    {
        return input.Select(x => x.Split(delimeter).ToList()).ToList();
    }
    
    public List<List<string>> SplitListOfStringToListListOfStringByRow(List<string> input)
    {
        var output = new List<List<string>>();
        
        foreach (var line in input)
        {
            output.Add(line.Split("\n").ToList());
        }

        return output;
    }
    
    public List<string> SplitStringByDelimeterToListOfString(string s, string del)
    {
        return s.Split(del).ToList();
    }

}