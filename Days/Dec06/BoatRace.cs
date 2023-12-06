namespace aoc_2022.Days.Dec06;

public static class BoatRace
{
    private static List<(long raceTime, long record)> _races;
    
    public static int RaceAllRaces(List<(long raceTime, long record)> input)
    {
        _races = input;
        var product = 1;
        for (int i = 0; i < _races.Count; i++)
        {
            var winners = Race(i);
            product *= winners;
        }
        return product;
    }

    private static int Race(int i)
    {
        var setting = _races[i];
        var winningLoadtimes = new List<int>();

        for (int loadTime = 0; loadTime < setting.raceTime; loadTime++)
        {
            var goTime = setting.raceTime - loadTime;
            var dist = goTime * loadTime;

            if (dist > setting.record) 
                winningLoadtimes.Add(loadTime);
        }
        return winningLoadtimes.Count;
    }
}