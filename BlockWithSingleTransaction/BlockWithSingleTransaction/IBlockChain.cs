namespace BlockWithSingleTransaction;

public interface IBlockChain
{
    void AcceptBlock(IBlock block);
    void VerifyChain();
}