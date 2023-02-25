namespace BlockWithTransactionPool;

public interface ITransaction
{
    // Transaction data
    string ClaimNumber { get; }
    decimal SettlementAmount { get; }
    DateTimeOffset SettlementDate { get; }
    string CarRegistration { get; }
    int Mileage { get; set; }
    ClaimType ClaimType { get; }

    string CalculateTransactionHash();
}