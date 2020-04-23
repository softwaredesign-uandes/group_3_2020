using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDPreSubmissionNS;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BlocksTests
{
    [TestClass]
    public class BlockLoadersTests
    {
        //Method Tested: GatherBlocksDefault
        //Context: Passing string "1 13 17 10" and "2 22 30 50"
        //Expectation: Should create two blocks with attributes id:1 2, x:13 22, y:17 30, z:10 50 and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksDefaultTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksDefaultTestFile.blocks";
            Block block1 = new Block 
            {
                id = 1,
                x = 13,
                y = 17,
                z = 10
            };
            Block block2 = new Block
            {
                id = 2,
                x = 22,
                y = 30,
                z = 50
            };
            List<Block> blocks = BlockLoaders.GatherBlocksDefault(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }
    }
}
