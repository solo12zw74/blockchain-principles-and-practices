// See https://aka.ms/new-console-template for more information

using BlockWithProofOfWork;

var txnPool = new TransactionPool();

var txnToBeChanged = SetupTransactions(txnPool);

var keyStore = new KeyStore(Hmac.GenerateKey());

IBlock block1 = new Block(0, keyStore);
IBlock block2 = new Block(1, keyStore);
IBlock block3 = new Block(2, keyStore);
IBlock block4 = new Block(3, keyStore);

FillBlocks(block1, block2, block3, block4);

var chain = new BlockChain();
chain.AcceptBlock(block1);
chain.AcceptBlock(block2);
chain.AcceptBlock(block3);
chain.AcceptBlock(block4);

chain.VerifyChain();

Console.WriteLine("");
Console.WriteLine("");

txnToBeChanged.Mileage = 12345;
chain.VerifyChain();

Console.WriteLine();

ITransaction SetupTransactions(TransactionPool transactionPool)
{
    ITransaction txn1 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
    ITransaction txn2 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
    ITransaction txn3 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
    ITransaction txn4 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
    ITransaction txn5 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
    ITransaction txn6 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
    ITransaction txn7 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
    ITransaction txn8 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);
    ITransaction txn9 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
    ITransaction txn10 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
    ITransaction txn11 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
    ITransaction txn12 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
    ITransaction txn13 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
    ITransaction txn14 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
    ITransaction txn15 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
    ITransaction txn16 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);

    transactionPool.AddTransaction(txn1);
    transactionPool.AddTransaction(txn2);
    transactionPool.AddTransaction(txn3);
    transactionPool.AddTransaction(txn4);
    transactionPool.AddTransaction(txn5);
    transactionPool.AddTransaction(txn6);
    transactionPool.AddTransaction(txn7);
    transactionPool.AddTransaction(txn8);
    transactionPool.AddTransaction(txn9);
    transactionPool.AddTransaction(txn10);
    transactionPool.AddTransaction(txn11);
    transactionPool.AddTransaction(txn12);
    transactionPool.AddTransaction(txn13);
    transactionPool.AddTransaction(txn14);
    transactionPool.AddTransaction(txn15);
    transactionPool.AddTransaction(txn16);
    return txn5;
}

void FillBlocks(IBlock block, IBlock block5, IBlock block6, IBlock block7)
{
    block.AddTransaction(txnPool.GetTransaction());
    block.AddTransaction(txnPool.GetTransaction());
    block.AddTransaction(txnPool.GetTransaction());
    block.AddTransaction(txnPool.GetTransaction());

    block5.AddTransaction(txnPool.GetTransaction());
    block5.AddTransaction(txnPool.GetTransaction());
    block5.AddTransaction(txnPool.GetTransaction());
    block5.AddTransaction(txnPool.GetTransaction());

    block6.AddTransaction(txnPool.GetTransaction());
    block6.AddTransaction(txnPool.GetTransaction());
    block6.AddTransaction(txnPool.GetTransaction());
    block6.AddTransaction(txnPool.GetTransaction());

    block7.AddTransaction(txnPool.GetTransaction());
    block7.AddTransaction(txnPool.GetTransaction());
    block7.AddTransaction(txnPool.GetTransaction());
    block7.AddTransaction(txnPool.GetTransaction());

    block.SetBlockHash(null);
    block5.SetBlockHash(block);
    block6.SetBlockHash(block5);
    block7.SetBlockHash(block6);
}
