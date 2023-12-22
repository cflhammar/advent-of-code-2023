namespace aoc_2022.Days.Dec22;

public class BlockDisintegrator
{
    private List<Block> _blocks;

    public BlockDisintegrator(List<List<string>> input)
    {
        _blocks = new List<Block>();
        foreach (var block in input)
        {
            _blocks.Add(new Block(block));
        }

        FallToRest();
    }


    public void FindDisintegrateableBlocks()
    {
        var canBeRemoved = 0;
        foreach (var block in _blocks)
        {
            var blocksAboveSupportedByThisBlock = 
                _blocks.Where(b => b.Start.z == block.End.z + 1 && HasOverlap(b, block)).ToList();
            
            var otherBlocksOnSameLevel = 
                _blocks.Where(otherBlock => otherBlock.End.z == block.End.z && otherBlock.Start != block.Start && otherBlock.End != block.End).ToList();

            var hasSupport = true;
            foreach (var supportedBlock in blocksAboveSupportedByThisBlock)
            {
                if (otherBlocksOnSameLevel.Any(b => HasOverlap(b, supportedBlock))) continue;
                hasSupport = false;
            }
            
            if (hasSupport) canBeRemoved++;
        }
        Console.WriteLine(canBeRemoved);
    }


    private void FallToRest()
    {
        _blocks = _blocks.OrderBy(b => b.Start.z).ToList();
        var atRest = new List<Block>();
        
        foreach (var block in _blocks)
        {
            var restingPos = FindEmptySpace(block, atRest);
            atRest.Add(restingPos);
        }

        _blocks = atRest.OrderBy(b => b.End.z).ToList();
    }

    private Block FindEmptySpace(Block block, List<Block> existingBlocks)
    {
        var highestPoint = 0;

        foreach (var existingBlock in existingBlocks.Where(b => HasOverlap(block, b)))
        {
            if (existingBlock.End.z > highestPoint) highestPoint = existingBlock.End.z;
        }

        return new Block((block.Start.x, block.Start.y, highestPoint + 1),
            (block.End.x, block.End.y, highestPoint + 1 + (block.End.z - block.Start.z)));
    }
    
    private bool HasOverlap(Block block1, Block block2)
    {
        return block1.XRange.Intersect(block2.XRange).Any() && 
               block1.YRange.Intersect(block2.YRange).Any();
    }
}

public class Block
{
    public (int x, int y, int z) Start { get; set; }
    public (int x, int y, int z) End { get; set; }
    
    public List<int> XRange => Enumerable.Range(Start.x, End.x - Start.x + 1).ToList();
    public List<int> YRange => Enumerable.Range(Start.y, End.y - Start.y + 1).ToList();
    public List<int> ZRange => Enumerable.Range(Start.z, End.z - Start.z + 1).ToList();

    public Block(List<string> input)
    {
        var tempStart = input[0].Split(",");
        var tempEnd = input[1].Split(",");

        Start = (int.Parse(tempStart[0]), int.Parse(tempStart[1]), int.Parse(tempStart[2]));
        End = (int.Parse(tempEnd[0]), int.Parse(tempEnd[1]), int.Parse(tempEnd[2]));
    }

    public Block((int x, int y, int z) start, (int x, int y, int z) end)
    {
        Start = start;
        End = end;
    }
}