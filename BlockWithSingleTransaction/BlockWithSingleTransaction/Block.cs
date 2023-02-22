using System.Text;

namespace BlockWithSingleTransaction;

public class Block : IBlock
{
    public Block(int blockNumber, string claimNumber, decimal settlementAmount, DateTimeOffset settlementDate,
        string carRegistration,
        int mileage, ClaimType claimType, IBlock parent)
    {
        ClaimNumber = claimNumber;
        SettlementAmount = settlementAmount;
        SettlementDate = settlementDate;
        CarRegistration = carRegistration;
        Mileage = mileage;
        ClaimType = claimType;
        BlockNumber = blockNumber;
        SetBlockHash(parent);
    }

    public string ClaimNumber { get; set; }
    public decimal SettlementAmount { get; set; }
    public DateTimeOffset SettlementDate { get; set; }
    public string CarRegistration { get; set; }
    public int Mileage { get; set; }
    public ClaimType ClaimType { get; set; }
    public int BlockNumber { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string BlockHash { get; set; }
    public string PreviousBlockHash { get; set; }
    public string CalculatedBlockHash { get; set; }

    public string CalculateBlockHash(string previousBlockHash)
    {
        string txnHashOrigin = string.Concat(ClaimNumber, SettlementAmount, SettlementDate, CarRegistration, Mileage,
            ClaimType);
        string blockHeader = string.Concat(BlockNumber, CreatedDate, previousBlockHash);
        string combined = txnHashOrigin + blockHeader;

        return Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
    }

    public void SetBlockHash(IBlock parent)
    {
        if (parent != null)
        {
            PreviousBlockHash = parent.BlockHash;
            parent.NextBlock = this;
        }
        else
        {
            PreviousBlockHash = null;
        }
        
        BlockHash = CalculateBlockHash(PreviousBlockHash);
    }

    public IBlock NextBlock { get; set; }

    public bool IsValidChain(string prevBlockHash, bool verbose)
    {
        var result = true;

        string newBlockHash = CalculateBlockHash(prevBlockHash);
        if (newBlockHash != BlockHash)
        {
            result = false;
        }
        else
        {
            result |= PreviousBlockHash == prevBlockHash;
        }

        PrintVerificationMessage(verbose, result);

        if (NextBlock != null)
        {
            return NextBlock.IsValidChain(newBlockHash, verbose);
        }

        return result;
    }

    private void PrintVerificationMessage(bool verbose, bool isValid)
    {
        if (!verbose)
        {
            return;
        }

        if (!isValid)
        {
            Console.WriteLine($"BlockNumber {BlockNumber} : FAILED VERIFICATION");
        }
        else
        {
            Console.WriteLine($"BlockNumber {BlockNumber} : PASSED VERIFICATION");
        }
    }
}