using System.Text;

namespace BlockWithMultipleTransaction
{
    public record Transaction(
        string ClaimNumber,
        decimal SettlementAmount,
        DateTimeOffset SettlementDate,
        string CarRegistration,
        ClaimType ClaimType) : ITransaction
    {
        public int Mileage { get; set; }

        public string CalculateTransactionHash()
        {
            string txnHash = ClaimNumber + SettlementAmount + SettlementDate + CarRegistration + Mileage + ClaimType;
            return Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(txnHash)));
        }
    }
}