namespace BlockWithSingleTransaction;

public class BlockChain : IBlockChain
{
    public IBlock CurrentBlock { get; private set; }
    public IBlock HeadBlock { get; private set; }

    public List<IBlock> Blocks { get; }

    public BlockChain()
    {
        Blocks = new List<IBlock>();
    }
    public void AcceptBlock(IBlock block)
    {
        if (HeadBlock == null)
        {
            HeadBlock = block;
            HeadBlock.PreviousBlockHash = null;
        }

        CurrentBlock = block;
        Blocks.Add(block);
    }

    public void VerifyChain()
    {
        if (HeadBlock == null)
        {
            throw new InvalidOperationException("genesis block not set.");
        }

        var isValid = HeadBlock.IsValidChain(null, true);

        if (isValid)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Blockchain integrity intact.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Blockchain integrity is NOT intact.");
            Console.ResetColor();
        }
    }
}