using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SDPreSubmissionNS;
using System.Linq;

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

        //Method Tested: GetPossibleAttributes
        //Context: Passing 2 Blocks with attributes:
        //id=1, x=10, y=10, z=10, weight=1000, [contest1=200, contest2=100], [mptest1=10, mptest2=20], [cattest1=asd, cattest2=def]
        //id=2, x=12, y=12, z=12, weight=2000, [contest1=400, contest2=500], [mptest1=50, mptest2=60], [cattest1=vbm, cattest2=jkl]
        //and a BlockModel with name "Block Model"
        //Expectation: Should return [id, x, y, z, weight, contest1, contest2, mptest1, mptest2, cattest1, cattest2]
        [TestMethod]
        public void GetPossibleAttributesTest()
        {
            List<string> cons = new List<string>(new string[] { "contest1", "contest2"});
            List<string> mps = new List<string>(new string[] { "mptest1", "mptest2" });
            List<string> cats = new List<string>(new string[] { "cattest1", "cattest2" });
            List<double> cons_v1 = new List<double>(new double[] {200, 100 });
            List<double> mps_v1 = new List<double>(new double[] {10, 20 });
            List<string> cats_v1 = new List<string>(new string[] {"asd", "def" });
            List<double> cons_v2 = new List<double>(new double[] {400, 500 });
            List<double> mps_v2 = new List<double>(new double[] {50, 60 });
            List<string> cats_v2 = new List<string>(new string[] {"vbm", "jkl" });

            BlockModel blockModel = new BlockModel("Block Model", cons, mps, cats);
            Block block1 = new Block(1, 10, 10, 10, 1000, cons_v1, mps_v1, cats_v1, blockModel);
            Block block2 = new Block(2, 12, 12, 12, 2000, cons_v2, mps_v2, cats_v2, blockModel);
            List<Block> blocks = new List<Block>(new Block[] {block1, block2 });
            blockModel.SetBlocks(blocks);
            List<string> givenValues = blockModel.GetPossibleAttributes();
            List<string> expected = new List<string>(new string[] {"id","x","y","z","Weight","contest1","mptest1","cattest1","contest2","mptest2","cattest2" });

            if (!CompareLists<string>(expected, givenValues) || expected.Count() != givenValues.Count())
            {
                Assert.Fail("list of attributes is different from expected");
            }
        }

        //Method Tested: GetBlock
        //Context: Passing 2 Blocks with attributes:
        //id=1, x=10, y=10, z=10, weight=1000
        //id=2, x=12, y=12, z=12, weight=2000
        //and a BlockModel with name "Block Model" and both Blocks in it
        //passing coordinates x=10, y=10, z=10
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
            Assert.AreEqual(block1, blockModel.GetBlock(10,10,10));
        }

        //Method Tested: GetNumberOfBlocks
        //Context: Passing 2 Blocks with attributes:
        //id=1, x=10, y=10, z=10, weight=1000
        //id=2, x=12, y=12, z=12, weight=2000
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
