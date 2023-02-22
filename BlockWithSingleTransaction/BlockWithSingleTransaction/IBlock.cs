namespace BlockWithSingleTransaction;

public interface IBlock
{
    // Block data
    string ClaimNumber { get; set; }
    decimal SettlementAmount { get; set; }
    DateTimeOffset SettlementDate { get; set; }
    string CarRegistration { get; set; }
    int Mileage { get; set; }
    ClaimType ClaimType { get; set; }

    // Block metadata
    int BlockNumber { get; set; }
    DateTimeOffset CreatedDate { get; set; }
    string BlockHash { get; set; }
    string PreviousBlockHash { get; set; }

    // Utility functions and properties
    string CalculatedBlockHash { get; set; }
    void SetBlockHash(IBlock parent);
    IBlock NextBlock { get; set; }
    bool IsValidChain(string prevBlockHash, bool verbose);
}