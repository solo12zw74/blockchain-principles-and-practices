using System.Diagnostics;
using System.Text;
using BlockWithProofOfWork.Merkle;

namespace BlockWithProofOfWork;

public class Block : IBlock
{
    private MerkleTree _merkleTree = new MerkleTree();

    public List<ITransaction> Transactions { get; set; }
    public int BlockNumber { get; set; }
    public IKeyStore KeyStore { get; }
    public DateTimeOffset CreatedDate { get; set; }
    public string BlockHash { get; set; }
    public string BlockSignature { get; set; }
    public int Difficulty { get; }
    public int Nonce { get; private set; }

    public string PreviousBlockHash { get; set; }

    public Block(int blockNumber, IKeyStore keyStore, int difficulty)
    {
        BlockNumber = blockNumber;
        KeyStore = keyStore;
        Difficulty = difficulty;
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

        BlockHash = CalculateProofOfWork(CalculateBlockHash(PreviousBlockHash));

        if (KeyStore != null)
        {
            BlockSignature = KeyStore.SignBlock(BlockHash);
        }
    }

    public string CalculateProofOfWork(string blockHash)
    {
        string difficulty = DifficultyString();
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while (true)
        {
            var hashedData =
                Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + blockHash)));

            if (hashedData.StartsWith(difficulty, StringComparison.Ordinal))
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                Console.WriteLine("Difficulty Level " + Difficulty + " - Nonce = " + Nonce + " - Elapsed = " +
                                  elapsedTime + " - " + hashedData);
                return hashedData;
            }

            Nonce++;
        }
    }

    private string DifficultyString()
    {
        string difficultyString = string.Empty;

        for (int i = 0; i < Difficulty; i++)
        {
            difficultyString += "0";
        }

        return difficultyString;
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

    public IBlock NextBlock { get; set; }

    public bool IsValidChain(string prevBlockHash, bool verbose)
    {
        var result = true;

        BuildMerkleTree();

        bool isValidSignature = KeyStore.VerifyBlock(BlockHash, BlockSignature);

        var newBlockHash =
            Convert.ToBase64String(
                HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + CalculateBlockHash(prevBlockHash))));

        isValidSignature = KeyStore.VerifyBlock(newBlockHash, BlockSignature);

        if (newBlockHash != BlockHash)
        {
            result = false;
        }
        else
        {
            result |= PreviousBlockHash == prevBlockHash;
        }

        PrintVerificationMessage(verbose, result, isValidSignature);

        if (NextBlock != null)
        {
            return NextBlock.IsValidChain(newBlockHash, verbose);
        }

        return result;
    }

    public string CalculateBlockHash(string previousBlockHash)
    {
        string blockheader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
        string combined = _merkleTree.RootNode + blockheader;

        string completeBlockHash;

        if (KeyStore == null)
        {
            completeBlockHash = Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
        }
        else
        {
            completeBlockHash = Convert.ToBase64String(HashUtil.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
        }

        return completeBlockHash;
    }


    private void PrintVerificationMessage(bool verbose, bool isValid, bool validSignature)
    {
        if (!isValid)
        {
            Console.WriteLine("Block Number " + BlockNumber + " : FAILED VERIFICATION");
        }
        else
        {
            Console.WriteLine("Block Number " + BlockNumber + " : PASS VERIFICATION");
        }

        if (!validSignature)
        {
            Console.WriteLine("Block Number " + BlockNumber + " : Invalid Digital Signature");
        }
    }
}