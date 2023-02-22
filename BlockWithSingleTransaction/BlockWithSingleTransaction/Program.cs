// See https://aka.ms/new-console-template for more information


using BlockWithSingleTransaction;

var blockChain = new BlockChain();
var block0 = new Block(0, "ABC123", 1_000, DateTimeOffset.Now.AddDays(-10), "CA", 10_000, ClaimType.TotalLoss, null);
var block1 = new Block(1, "DEF456", 2_000, DateTimeOffset.Now.AddDays(-9), "TX", 20_000, ClaimType.TotalLoss, block0);
var block2 = new Block(2, "GHI789", 3_000, DateTimeOffset.Now.AddDays(-8), "WY", 30_000, ClaimType.TotalLoss, block1);
var block3 = new Block(3, "JKL012", 4_000, DateTimeOffset.Now.AddDays(-7), "DA", 40_000, ClaimType.TotalLoss, block2);
var block4 = new Block(4, "MNO345", 5_000, DateTimeOffset.Now.AddDays(-6), "DA", 50_000, ClaimType.TotalLoss, block3);
var block5 = new Block(5, "PQR678", 6_000, DateTimeOffset.Now.AddDays(-5), "DA", 60_000, ClaimType.TotalLoss, block4);
var block6 = new Block(6, "STU901", 7_000, DateTimeOffset.Now.AddDays(-4), "DA", 70_000, ClaimType.TotalLoss, block5);
var block7 = new Block(7, "VWX234", 8_000, DateTimeOffset.Now.AddDays(-3), "DA", 80_000, ClaimType.TotalLoss, block6);
var block8 = new Block(8, "XYZ567", 9_000, DateTimeOffset.Now.AddDays(-2), "DA", 90_000, ClaimType.TotalLoss, block7);

blockChain.AcceptBlock(block0);
blockChain.AcceptBlock(block1);
blockChain.AcceptBlock(block2);
blockChain.AcceptBlock(block3);
blockChain.AcceptBlock(block4);
blockChain.AcceptBlock(block5);
blockChain.AcceptBlock(block6);
blockChain.AcceptBlock(block7);
blockChain.AcceptBlock(block8);

blockChain.VerifyChain();
