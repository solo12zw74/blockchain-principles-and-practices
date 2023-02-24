﻿using System.Text;

namespace BlockWithMultipleTransaction
{
    public class Transaction : ITransaction
    {
        public string ClaimNumber { get; set; }
        public decimal SettlementAmount { get; set; }
        public DateTimeOffset SettlementDate { get; set; }
        public string CarRegistration { get; set; }
        public int Mileage { get; set; }
        public ClaimType ClaimType { get; set; }

        public Transaction(string claimNumber,
                            decimal settlementAmount,
                            DateTime settlementDate,
                            string carRegistration,
                            int mileage,
                            ClaimType claimType)
        {
            ClaimNumber = claimNumber;
            SettlementAmount = settlementAmount;
            SettlementDate = settlementDate;
            CarRegistration = carRegistration;
            Mileage = mileage;
            ClaimType = claimType;
        }

        public string CalculateTransactionHash()
        {
            string txnHash = ClaimNumber + SettlementAmount + SettlementDate + CarRegistration + Mileage + ClaimType;
            return Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(txnHash)));
        }
    }
}
