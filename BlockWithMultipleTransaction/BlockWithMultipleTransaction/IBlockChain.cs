namespace BlockWithMultipleTransaction;

public interface IBlockChain
{
    void AcceptBlock(IBlock block);
    void VerifyChain();
}