namespace BlockWithMultipleTransaction;

public interface IBlock
{
    // Block data
    IEnumerable<ITransaction> Transactions { get; set; }

    // Block metadata
    int BlockNumber { get; set; }
    DateTimeOffset CreatedDate { get; set; }
    string BlockHash { get; set; }
    string PreviousBlockHash { get; set; }

    // Utility functions and properties
    void AddTransaction(ITransaction transaction);
    string CalculatedBlockHash { get; set; }
    void SetBlockHash(IBlock parent);
    IBlock NextBlock { get; set; }
    bool IsValidChain(string prevBlockHash, bool verbose);
}