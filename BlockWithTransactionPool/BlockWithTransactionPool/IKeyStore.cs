namespace BlockWithTransactionPool;

public interface IKeyStore
{
    byte[] AuthenticatedHashKey { get; set; }
    string SignBlock(string blockHash);
    bool VerifyBlock(string blockHash, string signature);
}