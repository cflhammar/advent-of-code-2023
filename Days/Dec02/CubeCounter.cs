namespace aoc_2022.Days.Dec02;

public class CubeCounter
{
    public (int idSum, int power) PossibleGames(List<List<string>> games, (int red, int green, int blue) maxValue)
    {
        var idSum = 0;
        var power = 0;
        var id = 0;
        foreach (var game in games)
        {
            id++;
            bool gameIsOk = true;
            (int red, int green, int blue) minNeeded = (0, 0, 0);
            
            foreach (var reveal in game)
            {
                var s = reveal;
                if (reveal.StartsWith("Game"))
                {
                    var index = s.IndexOf(":", StringComparison.Ordinal) + 2;
                    s = s.Substring(index, s.Length - index);
                }
                var colors = s.Split(", ");

                foreach (var c in colors)
                {
                    var amountString = c.Split(" ")[0];
                    var color = c.Split(" ")[1];

                    var amount = int.Parse(amountString);
                    switch (color)
                    {
                        case "red":
                            if (amount > maxValue.red) gameIsOk =false;
                            if (amount > minNeeded.red) minNeeded.red = amount;
                            break;
                        case "green":
                            if (amount > maxValue.green) gameIsOk =false;
                            if (amount > minNeeded.green) minNeeded.green = amount;
                            break;
                        case "blue":
                            if (amount > maxValue.blue) gameIsOk =false;
                            if (amount > minNeeded.blue) minNeeded.blue = amount;
                            break;
                        default:
                            Console.WriteLine("something is wrong!");
                            break;
                    }
                }
            }

            if (gameIsOk) idSum += id;
            power += minNeeded.green * minNeeded.red * minNeeded.blue;
        }
        
        return (idSum, power);
    }
}