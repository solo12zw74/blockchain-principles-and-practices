using System.Text;

namespace BlockWithProofOfWork
{
    public record Transaction(
        string ClaimNumber,
        decimal SettlementAmount,
        DateTimeOffset SettlementDate,
        string CarRegistration,
        ClaimType ClaimType) : ITransaction
    {
        public int Mileage { get; set; }

        public Transaction(string claimNumber,
            decimal settlementAmount,
            DateTimeOffset settlementDate,
            string carRegistration,
            int mileage,
            ClaimType claimType) : this(claimNumber,
            settlementAmount,
            settlementDate,
            carRegistration,
            claimType)
        {
            Mileage = mileage;
        }

        public string CalculateTransactionHash()
        {
            string txnHash = ClaimNumber + SettlementAmount + SettlementDate + CarRegistration + Mileage + ClaimType;
            return Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(txnHash)));
        }
    }
}