namespace BlockWithMultipleTransaction;

public interface ITransaction
{
    // Transaction data
    string ClaimNumber { get; set; }
    decimal SettlementAmount { get; set; }
    DateTimeOffset SettlementDate { get; set; }
    string CarRegistration { get; set; }
    int Mileage { get; set; }
    ClaimType ClaimType { get; set; }

    string CalculateTransactionHash();
}