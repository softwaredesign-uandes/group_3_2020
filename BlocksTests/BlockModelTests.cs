﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SDPreSubmissionNS;
using System.Linq;
using Microsoft.VisualBasic;

namespace BlocksTests
{
    [TestClass]
    public class BlockModelTests
    {
        //Method Tested: SetBlocks
        //Context: Passing 2 Blocks with attributes:
        //Id=1, X=10, Y=10, Z=10, weight=1000
        //Id=2, X=12, Y=12, Z=12, weight=2000
        //and a BlockModel with name "Block Model"
        //Expectation: Should return a list of blocks equal to the Blocks attribute of BlockModel
        [TestMethod]
        public void SetBlocksTest()
        {
            Block block1 = new Block(1, 10, 10, 10, 1000);
            Block block2 = new Block(2, 12, 12, 12, 2000);
            List<Block> blocks = new List<Block>();
            blocks.Add(block1);
            blocks.Add(block2);
            BlockModel blockModel = new BlockModel("Block Model");
            blockModel.SetBlocks(blocks);
            Assert.AreEqual(blocks, blockModel.Blocks);
        }

        //Method Tested: GetPossibleAttributes
        //Context: Passing 2 Blocks with attributes:
        //Id=1, X=10, Y=10, Z=10, weight=1000, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=12, Y=12, Z=12, weight=2000, [contest1=400, contest2=500], [mptest1=50, mptest2=60], [cattest1=vbm, cattest2=jkl]
        //and a BlockModel with name "Block Model"
        //Expectation: Should return [Id, X, Y, Z, weight, contest1, contest2, mptest1, mptest2, cattest1, cattest2]
        [TestMethod]
        public void GetPossibleAttributesTest()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            List<double> cons_v2 = new List<double>(new double[] { 400, 500 });
            List<double> mps_v2 = new List<double>(new double[] { 50, 60 });
            List<string> cats_v2 = new List<string>(new string[] { "vbm", "jkl" });

            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(1, 10, 10, 10, 1000, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(2, 12, 12, 12, 2000, cons_v2, mps_v2, cats_v2, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2 });
            blockModel.SetBlocks(blocks);
            List<string> givenValues = blockModel.GetPossibleAttributes();
            List<string> expected = new List<string>(new string[] { "id", "x", "y", "z", "Weight", "contest1", "mptest1", "cattest1", "contest2", "mptest2", "cattest2" });

            if (!CompareLists<string>(expected, givenValues) || expected.Count() != givenValues.Count())
            {
                Assert.Fail("list of attributes is different from expected");
            }
        }

        //Method Tested: GetBlock
        //Context: Passing 2 Blocks with attributes:
        //Id=1, X=10, Y=10, Z=10, weight=1000
        //Id=2, X=12, Y=12, Z=12, weight=2000
        //and a BlockModel with name "Block Model" and both Blocks in it
        //passing coordinates X=10, Y=10, Z=10
        //Expectation: Should return block1
        [TestMethod]
        public void GetBlockTest()
        {
            Block block1 = new Block(1, 10, 10, 10, 1000);
            Block block2 = new Block(2, 12, 12, 12, 2000);
            List<Block> blocks = new List<Block>();
            blocks.Add(block1);
            blocks.Add(block2);
            BlockModel blockModel = new BlockModel("Block Model");
            blockModel.SetBlocks(blocks);
            Assert.AreEqual(block1, blockModel.GetBlock(10, 10, 10));
        }
        [TestMethod]
        public void FailGetBlockTest()
        {
            Block block1 = new Block(1, 10, 10, 10, 1000);
            Block block2 = new Block(2, 12, 12, 12, 2000);
            List<Block> blocks = new List<Block>();
            blocks.Add(block1);
            blocks.Add(block2);
            BlockModel blockModel = new BlockModel("Block Model");
            blockModel.SetBlocks(blocks);
            Block failTestBlock = blockModel.GetBlock(0, 0, 0);
            Assert.AreEqual(failTestBlock, null);
        }

        //Method Tested: GetNumberOfBlocks
        //Context: Passing 2 Blocks with attributes:
        //Id=1, X=10, Y=10, Z=10, weight=1000
        //Id=2, X=12, Y=12, Z=12, weight=2000
        //and a BlockModel with name "Block Model"
        //Expectation: Should return the amount of blocks of BlockModel, 2
        [TestMethod]
        public void GetNumberOfBlocksTest()
        {
            Block block1 = new Block(1, 10, 10, 10, 1000);
            Block block2 = new Block(2, 12, 12, 12, 2000);
            List<Block> blocks = new List<Block>();
            blocks.Add(block1);
            blocks.Add(block2);
            BlockModel blockModel = new BlockModel("Block Model");
            blockModel.SetBlocks(blocks);
            Assert.AreEqual(2, blockModel.GetNumberOfBlocks());
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=2, ry=2, rz=2
        //Passing a BlockModel that has 8 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return the amount of blocks of the reblocked BlockModel = 1
        [TestMethod]
        public void ReblockTestNumberOfBlocks()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            Assert.AreEqual(1, blockModel.Blocks.Count);
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=2, ry=2, rz=2
        //Passing a BlockModel that has 8 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return 800 as the total weight of the block, in tonnes.
        [TestMethod]
        public void ReblockTestTotalWeight()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            double sumOfWeights = 0;
            foreach (Block block in blockModel.Blocks)
            {
                sumOfWeights += block.Weight;
            }
            Assert.AreEqual(800, sumOfWeights);
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=2, ry=2, rz=2
        //Passing a BlockModel that has 8 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return a list of blocks with one block with coordinates X=0, Y=0, Z=0.
        [TestMethod]
        public void ReblockTestPosition()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            if (blockModel.Blocks[0].X != 0 || blockModel.Blocks[0].Y != 0 || blockModel.Blocks[0].Z != 0)
            {
                Assert.Fail("final blocks coordinates are misplaced.");
            }
        }
        [TestMethod]
        public void ReblockTestCategories()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            List<string> cats_v2 = new List<string>(new string[] { "asd", "fda" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v2, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v2, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v2, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            string expectedCategory = "def";
            foreach (Block block in blockModel.Blocks)
            {
                if (block.CategoricalAttributes["cattest2"] != expectedCategory)
                {
                    Assert.Fail("Final block category is wrong.");
                }
                
            }
        }
        [TestMethod]
        public void ReblockTestMassProportional()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 100, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            List<double> mps_v2 = new List<double>(new double[] { 100, 100 });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v2, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v2, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v2, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v2, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            double expectedValue = 55;
            double expectedValue2 = 60;
            foreach (Block block in blockModel.Blocks)
            {
                if (block.MassProportionalAttributes["mptest1"] != expectedValue && block.MassProportionalAttributes["mptest2"] != expectedValue2)
                {
                    Assert.Fail("Final block does not have proportional mass attribute.");
                }
            }
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=2, ry=2, rz=2
        //Passing a BlockModel that has 8 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=200, [contest1=200, contest2=100], [mptest1=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=300, [contest1=200, contest2=100], [mptest1=30], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=400, [contest1=200, contest2=100], [mptest1=40], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=500, [contest1=200, contest2=100], [mptest1=50], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=600, [contest1=200, contest2=100], [mptest1=60], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=700, [contest1=200, contest2=100], [mptest1=70], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=800, [contest1=200, contest2=100], [mptest1=80], [cattest1=asd, cattest2=def]
        //Expectation: Should return a list of blocks with one block with coordinates X=0, Y=0, Z=0.
        [TestMethod]
        public void ReblockTestProportion()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10 });
            List<double> mps_v2 = new List<double>(new double[] { 20 });
            List<double> mps_v3 = new List<double>(new double[] { 30 });
            List<double> mps_v4 = new List<double>(new double[] { 40 });
            List<double> mps_v5 = new List<double>(new double[] { 50 });
            List<double> mps_v6 = new List<double>(new double[] { 60 });
            List<double> mps_v7 = new List<double>(new double[] { 70 });
            List<double> mps_v8 = new List<double>(new double[] { 80 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 200, cons_v1, mps_v2, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 300, cons_v1, mps_v3, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 400, cons_v1, mps_v4, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 500, cons_v1, mps_v5, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 600, cons_v1, mps_v6, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 700, cons_v1, mps_v7, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 800, cons_v1, mps_v8, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] { block1, block2, block3, block4, block5, block6, block7, block8 });
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(2, 2, 2);
            double expectedValue = 204000 / 3600;
            double substraction = expectedValue - blockModel.Blocks[0].MassProportionalAttributes["mptest1"];
            if (substraction > 1 || substraction < -1)
            {
                Assert.Fail("The mass proportion values on the new BlockModel are off, after Reblock.");
            }
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=3, ry=3, rz=3
        //Passing a BlockModel that has 21 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=8, X=2, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=9, X=0, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=10, X=0, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=11, X=2, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=12, X=2, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=13, X=0, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=14, X=2, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=15, X=1, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=16, X=2, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=17, X=2, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=18, X=1, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=19, X=1, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=20, X=2, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return the amount of blocks of the reblocked BlockModel = 1
        [TestMethod]
        public void ReblockTestNumberOfBlocks2()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block9 = new Block(8, 2, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block10 = new Block(9, 2, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block11 = new Block(10, 0, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block12 = new Block(11, 0, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block13 = new Block(12, 2, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block14 = new Block(13, 2, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block15 = new Block(14, 0, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block16 = new Block(15, 2, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block17 = new Block(16, 1, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block18 = new Block(17, 1, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block19 = new Block(18, 2, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block20 = new Block(19, 2, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block21 = new Block(20, 1, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            
            List<Block> blocks = new List<Block>(new Block[] { 
                block1, block2, block3, block4, block5, block6, block7, block8, block9, block10, block11, block12, block13,
            block14, block15, block16, block17, block18, block19, block20, block21});
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(3, 3, 3);
            Assert.AreEqual(1, blockModel.Blocks.Count);
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=3, ry=3, rz=3
        //Passing a BlockModel that has 21 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=8, X=2, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=9, X=0, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=10, X=0, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=11, X=2, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=12, X=2, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=13, X=0, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=14, X=2, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=15, X=1, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=16, X=2, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=17, X=2, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=18, X=1, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=19, X=1, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=20, X=2, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return 2100 as the total weight of the block, in tonnes.
        [TestMethod]
        public void ReblockTestTotalWeight2()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block9 = new Block(8, 2, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block10 = new Block(9, 2, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block11 = new Block(10, 0, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block12 = new Block(11, 0, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block13 = new Block(12, 2, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block14 = new Block(13, 2, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block15 = new Block(14, 0, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block16 = new Block(15, 2, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block17 = new Block(16, 1, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block18 = new Block(17, 1, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block19 = new Block(18, 2, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block20 = new Block(19, 2, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block21 = new Block(20, 1, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);

            List<Block> blocks = new List<Block>(new Block[] {
                block1, block2, block3, block4, block5, block6, block7, block8, block9, block10, block11, block12, block13,
            block14, block15, block16, block17, block18, block19, block20, block21});
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(3, 3, 3);
            double sumOfWeights = 0;
            foreach (Block block in blockModel.Blocks)
            {
                sumOfWeights += block.Weight;
            }
            Assert.AreEqual(2100, sumOfWeights);
        }

        //Method Tested: Reblock
        //Context: Passing the variables rx=3, ry=3, rz=3
        //Passing a BlockModel that has 21 Blocks with attributes:
        //Id=0, X=0, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=1, X=1, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=2, X=0, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=3, X=0, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=4, X=1, Y=1, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=5, X=1, Y=0, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=6, X=0, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=7, X=1, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=8, X=2, Y=0, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=9, X=0, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=10, X=0, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=11, X=2, Y=2, Z=0, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=12, X=2, Y=0, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=13, X=0, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=14, X=2, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=15, X=1, Y=2, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=16, X=2, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=17, X=2, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=18, X=1, Y=1, Z=2, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=19, X=1, Y=2, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Id=20, X=2, Y=1, Z=1, weight=100, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //Expectation: Should return a list of blocks with one block with coordinates X=0, Y=0, Z=0.
        [TestMethod]
        public void ReblockTestTotalPosition2()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(4, 1, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block6 = new Block(5, 1, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block7 = new Block(6, 0, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block8 = new Block(7, 1, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block9 = new Block(8, 2, 1, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block10 = new Block(9, 2, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block11 = new Block(10, 0, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block12 = new Block(11, 0, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block13 = new Block(12, 2, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block14 = new Block(13, 2, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block15 = new Block(14, 0, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block16 = new Block(15, 2, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block17 = new Block(16, 1, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block18 = new Block(17, 1, 2, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block19 = new Block(18, 2, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block20 = new Block(19, 2, 2, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block21 = new Block(20, 1, 1, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);

            List<Block> blocks = new List<Block>(new Block[] {
                block1, block2, block3, block4, block5, block6, block7, block8, block9, block10, block11, block12, block13,
            block14, block15, block16, block17, block18, block19, block20, block21});
            blockModel.SetBlocks(blocks);
            blockModel.Reblock(3, 3, 3);
            if (blockModel.Blocks[0].X != 0 || blockModel.Blocks[0].Y != 0 || blockModel.Blocks[0].Z != 0)
            {
                Assert.Fail("final blocks coordinates are misplaced.");
            }
        }

        public void ReblockTestYCoordinate()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 0, 1, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 2, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 3, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(3, 0, 4, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] {
                block1, block2, block3, block4, block5});
            blockModel.SetBlocks(blocks);
            var max_y = blockModel.GetMaxCoordinates()[1];
            blockModel.Reblock(2, 2, 2);
            if(!(blockModel.GetMaxCoordinates()[1] == max_y / 2 || blockModel.GetMaxCoordinates()[1] == (max_y / 2)+1)) Assert.Fail("max Y coordinate bigger than expected.");

        }

        public void ReblockTestXCoordinate() {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 1, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 2, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 3, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(3, 4, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] {
                block1, block2, block3, block4, block5});
            blockModel.SetBlocks(blocks);
            var max_x = blockModel.GetMaxCoordinates()[0];
            blockModel.Reblock(2, 2, 2);
            if (!(blockModel.GetMaxCoordinates()[0] == max_x / 2 || blockModel.GetMaxCoordinates()[0] == (max_x / 2) + 1)) Assert.Fail("max X coordinate bigger than expected.");

        }

        public void ReblockTestZCoordinate() {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2" });
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] { 200, 100 });
            List<double> mps_v1 = new List<double>(new double[] { 10, 20 });
            List<string> cats_v1 = new List<string>(new string[] { "asd", "def" });
            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(0, 0, 0, 0, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(1, 0, 0, 1, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block3 = new Block(2, 0, 0, 2, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block4 = new Block(3, 0, 0, 3, 100, cons_v1, mps_v1, cats_v1, blockModel);
            Block block5 = new Block(3, 0, 0, 4, 100, cons_v1, mps_v1, cats_v1, blockModel);
            List<Block> blocks = new List<Block>(new Block[] {
                block1, block2, block3, block4, block5});
            blockModel.SetBlocks(blocks);
            var max_z = blockModel.GetMaxCoordinates()[2];
            blockModel.Reblock(2, 2, 2);
            if (!(blockModel.GetMaxCoordinates()[2] == max_z / 2 || blockModel.GetMaxCoordinates()[2] == (max_z / 2) + 1)) Assert.Fail("max Z coordinate bigger than expected.");

        }

        //Method for comparing equality between two lists
        //Source: https://answers.unity.com/questions/1307074/how-do-i-compare-two-lists-for-equality-not-caring.html
        public static bool CompareLists<T>(List<T> aListA, List<T> aListB)
        {
            if (aListA == null || aListB == null || aListA.Count != aListB.Count)
                return false;
            if (aListA.Count == 0)
                return true;
            Dictionary<T, int> lookUp = new Dictionary<T, int>();
            // create index for the first list
            for (int i = 0; i < aListA.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListA[i], out count))
                {
                    lookUp.Add(aListA[i], 1);
                    continue;
                }
                lookUp[aListA[i]] = count + 1;
            }
            for (int i = 0; i < aListB.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListB[i], out count))
                {
                    // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                    return false;
                }
                count--;
                if (count <= 0)
                    lookUp.Remove(aListB[i]);
                else
                    lookUp[aListB[i]] = count;
            }
            // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
            return lookUp.Count == 0;
        }
    }
}
