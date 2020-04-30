using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SDPreSubmissionNS;

namespace BlocksTests
{
    [TestClass]
    public class BlockModelTests
    {
        //Method Tested: SetBlocks
        //Context: Passing 2 Blocks with attributes:
        //id=1, x=10, y=10, z=10, weight=1000
        //id=2, x=12, y=12, z=12, weight=2000
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
    }
}
