namespace aoc_2022.Days.Dec13;

public class MirrorAligner
{
    List<List<List<char>>> _mirrors = new();

    public MirrorAligner(List<List<string>> input)
    {
        foreach (var i in input)
        {
            _mirrors.Add(i.Select(x => x.ToCharArray().ToList()).ToList());
        }
    }

    public (int part1, int part2) FindAndCalculateReflections()
    {
        var totalSum = 0;
        var totalSum2 = 0;
        var m = -1;
        foreach (var mirror in _mirrors)
        {
            m++;
            var row = FindRowReflection(mirror);
            var col = FindColReflection(mirror);
            var sum =  row > -1 ? row * 100 : col;

            totalSum += sum;
            totalSum2 += FindAndCalculateReflectionWithSmudge(mirror, row, col);
        }

        return (totalSum, totalSum2);
    }

    private int FindAndCalculateReflectionWithSmudge(List<List<char>> mirror, int row, int col)
    {
        var next = false;
        for (int r = 0; r < mirror.Count; r++)
        {
            for (int c = 0; c < mirror[r].Count(); c++)
            {
                var copyOfMirror = mirror.Select(x => x.ToList()).ToList();
                copyOfMirror[r][c] = copyOfMirror[r][c] == '#' ? '.' : '#';

                var row2 = FindRowReflection(copyOfMirror, row);
                var col2 = FindColReflection(copyOfMirror, col);
                    
                if (row2 > 0 && row2 != row)
                {
                    return row2 * 100;
                }
                if (col2 > 0 && col2 != col)
                {
                    return col2;
                }
            }
            if (next) break;
        }

        return -1;
    }

    private int FindColReflection(List<List<char>> mirror, int skip = -1)
    {
        skip -= 1;
        for (int reflectionCol = 0; reflectionCol  < mirror[0].Count; reflectionCol ++)
        {        
            if (skip > -1 && reflectionCol == skip) continue;
            var reflection = true;
            for (int i = 1; i < mirror[0].Count; i++)
            {
                var colLeft = reflectionCol - i + 1;
                var colRight = reflectionCol + i;
                
                if (colLeft >= 0 && colRight < mirror[0].Count)
                {
                    var colLeftString = string.Join("", mirror.Select(x => x[colLeft]));
                    var colRightString = string.Join("", mirror.Select(x => x[colRight]));
                    
                    if (colLeftString != colRightString)
                    {
                        reflection = false;
                        break;
                    }
                }
            }

            if (reflection && reflectionCol < mirror[0].Count - 1)
            {
                return reflectionCol + 1;
            }
        }

        return -1;
    }

    private int FindRowReflection(List<List<char>> mirror, int skip = -1)
    {
        skip -= 1;
        for (int reflectionRow = 0; reflectionRow  < mirror.Count; reflectionRow ++)
        {        
            if (skip > -1 && reflectionRow == skip) continue;
            
            var reflection = true;
            for (int i = 1; i < mirror.Count; i++)
            {
                var rowUp = reflectionRow - i + 1;
                var rowDown = reflectionRow + i;
                
                if (rowUp >= 0 && rowDown < mirror.Count)
                {
                    var rowUpString = string.Join("", mirror[rowUp]);
                    var rowDownString = string.Join("", mirror[rowDown]);
                    
                    if (rowUpString != rowDownString)
                    {
                        reflection = false;
                        break;
                    }
                }
            }

            if (reflection && reflectionRow < mirror.Count - 1)
            {
                return reflectionRow + 1;
            }
        }

        return -1;
    }
}