using System.Security.Cryptography;
using System.Text;
using BlockWithTransactionPool.Merkle;

namespace BlockWithTransactionPool;

public class Block : IBlock
{
    private MerkleTree _merkleTree = new MerkleTree();

    public List<ITransaction> Transactions { get; set; }
    public int BlockNumber { get; set; }
    public IKeyStore KeyStore { get; }
    public DateTimeOffset CreatedDate { get; set; }
    public string BlockHash { get; set; }
    public string BlockSignature { get; set; }
    public string PreviousBlockHash { get; set; }

    public Block(int blockNumber, IKeyStore keyStore)
    {
        BlockNumber = blockNumber;
        KeyStore = keyStore;
        CreatedDate = DateTimeOffset.UtcNow;
        Transactions = new List<ITransaction>();
    }

    public void AddTransaction(ITransaction transaction)
    {
        Transactions.Add(transaction);
    }

    public string CalculatedBlockHash { get; set; }

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

        BuildMerkleTree();

        BlockHash = CalculateBlockHash(PreviousBlockHash);

        if (KeyStore != null)
        {
            BlockSignature = KeyStore.SignBlock(BlockHash);
        }
    }

    private void BuildMerkleTree()
    {
        _merkleTree = new MerkleTree();

        foreach (var transaction in Transactions)
        {
            _merkleTree.AppendLeaf(MerkleHash.Create(transaction.CalculateTransactionHash()));
        }

        _merkleTree.BuildTree();
    }

    public string CalculateBlockHash(string previousBlockHash)
    {
        var blockHeader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
        string combined = _merkleTree.RootNode + blockHeader;

        string completedBlockHash;
        if (KeyStore == null)
        {
            completedBlockHash = Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
        }
        else
        {
            completedBlockHash = Convert.ToBase64String(Hmac.ComputeHmacSha256(Encoding.UTF8.GetBytes(combined), KeyStore.AuthenticatedHashKey));
        }
        
        return completedBlockHash;
    }

    public IBlock NextBlock { get; set; }

    public bool IsValidChain(string prevBlockHash, bool verbose)
    {
        var result = true;
        
        BuildMerkleTree();

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