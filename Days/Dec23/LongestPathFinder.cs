namespace aoc_2022.Days.Dec23;

public class LongestPathFinder
{
    private List<List<char>> _map;
    private int _max = 0;

    public LongestPathFinder(List<List<char>> input)
    {
        _map = input;
    }

    public void Find()
    {
        var state = new State { Current = (0, 1), Visited = new(), Direction = "down"};
        FollowPath(state);

    }

    private int FollowPath(State state)
    {
        if (state.Current.r == _map.Count - 2 && state.Current.c == _map[0].Count - 2)
        {
            if (state.Visited.Count > _max)
            {
                _max = state.Visited.Count;
                Console.WriteLine(_max+1);
            }
            return 0;
        }
        
        if (IsWalkable((state.Current.r + 1, state.Current.c), state, "down"))
        {
            var nextState = new State(state);
            nextState.Direction = "down";
            nextState.Visited.Add(state.Current);
            nextState.Current = (state.Current.r + 1, state.Current.c);
            FollowPath(nextState);
        }
        
        if (IsWalkable((state.Current.r - 1, state.Current.c), state, "up"))
        {
            var nextState = new State(state);
            nextState.Direction = "up";
            nextState.Visited.Add(state.Current);
            nextState.Current = (state.Current.r - 1, state.Current.c);
            FollowPath(nextState);
        }
        
        if (IsWalkable((state.Current.r, state.Current.c + 1), state, "right"))
        {
            var nextState = new State(state);
            nextState.Direction = "right";
            nextState.Visited.Add(state.Current);
            nextState.Current = (state.Current.r, state.Current.c + 1);
            FollowPath(nextState);
        }
        
        if (IsWalkable((state.Current.r, state.Current.c - 1), state, "left"))
        {
            var nextState = new State(state);
            nextState.Direction = "left";
            nextState.Visited.Add(state.Current);
            nextState.Current = (state.Current.r, state.Current.c - 1);
            FollowPath(nextState);
        }
        
        return 0;
    }

    private bool IsWalkable((int r, int c) pos, State state, string direction)
    {
        if (state.Visited.Contains(pos)) return false;
        
        if (pos.r > 0 && pos.r < _map.Count && pos.c > 0 && pos.c < _map[pos.r].Count)
        {
            if (_map[pos.r][pos.c] == '.') return true;
            
            switch (direction)
            {
                case "down": 
                    if (_map[pos.r][pos.c] == 'v') return true; 
            
                    break;
                case "up": 
                    if (_map[pos.r][pos.c] == '^') return true; 
                    break;
                case "right": 
                    if (_map[pos.r][pos.c] == '>') return true; 
                    break;
                case "left": 
                    if (_map[pos.r][pos.c] == '<') return true; 
                    break;
            }
        }

        return false;
    }
    
}

internal class State
{
    public (int r, int c) Current;
    public string Direction;
    public List<(int r, int c)> Visited;

    public State(State state)
    {
        Current = state.Current;
        Direction = state.Direction;
        Visited = new List<(int r, int c)>(state.Visited);
    }

    public State()
    {
        
    }
}