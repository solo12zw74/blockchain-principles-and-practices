namespace BlockWithProofOfWork;

public interface IBlockChain
{
    void AcceptBlock(IBlock block);
    void VerifyChain();
}