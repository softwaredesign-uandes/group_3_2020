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
        //Context: Passing a file containing:
        //1 13 17 10
        //2 22 30 50
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
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

        //Method Tested: GatherBlocksNewMan
        //Context: Passing a file containing:
        //1 1 1 15 FROR 1.375353107 5664 1.04 -5890.56 24829.116 1
        //2 1 1 16 OXOR 0.913001543 5184 1.03 -5339.52 34347.213 0
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksNewManTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksNewManTestFile.blocks";
            Block block1 = new Block
            {
                id = 1,
                x = 1,
                y = 1,
                z = 15,
                type = "FROR",
                grade = 1.375353107,
                tonn = 5664,
                min_caf = 1.04,
                value_extracc = -5890.56,
                value_proc = 24829.116,
                apriori_process = 1
            };
            Block block2 = new Block
            {
                id = 2,
                x = 1,
                y = 1,
                z = 16,
                type = "OXOR",
                grade = 0.913001543,
                tonn = 5184,
                min_caf = 1.03,
                value_extracc = -5339.52,
                value_proc = 34347.213,
                apriori_process = 0
            };
            List<Block> blocks = BlockLoaders.GatherBlocksNewMan(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z) || !blocks[0].type.Equals(block1.type) || !blocks[0].grade.Equals(block1.grade) || !blocks[0].tonn.Equals(block1.tonn) || !blocks[0].min_caf.Equals(block1.min_caf) || !blocks[0].value_extracc.Equals(block1.value_extracc) || !blocks[0].value_proc.Equals(block1.value_proc) || !blocks[0].apriori_process.Equals(block1.apriori_process))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z) || !blocks[1].type.Equals(block2.type) || !blocks[1].grade.Equals(block2.grade) || !blocks[1].tonn.Equals(block2.tonn) || !blocks[1].min_caf.Equals(block2.min_caf) || !blocks[1].value_extracc.Equals(block2.value_extracc) || !blocks[1].value_proc.Equals(block2.value_proc) || !blocks[1].apriori_process.Equals(block2.apriori_process))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }

        //Method Tested: GatherBlocksZuck
        //Context: Passing a file containing:
        //8 0 32 28 20122.8000000000 0 5528.2500000000 0
        //9 0 33 28 20122.8000000000 0 5528.2500000000 0
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksZuckTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksZuckTestFile.blocks";
            Block block1 = new Block
            {
                id = 8,
                x = 0,
                y = 32,
                z = 28,
                cost = 20122.8000000000,
                value = 0,
                rock_tonnes = 5528.2500000000,
                ore_tonnes = 0,
            };
            Block block2 = new Block
            {
                id = 9,
                x = 0,
                y = 33,
                z = 28,
                cost = 20122.8000000000,
                value = 0,
                rock_tonnes = 5528.2500000000,
                ore_tonnes = 0,
            };
            List<Block> blocks = BlockLoaders.GatherBlocksZuck(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z) || !blocks[0].cost.Equals(block1.cost) || !blocks[0].value.Equals(block1.value) || !blocks[0].rock_tonnes.Equals(block1.rock_tonnes) || !blocks[0].ore_tonnes.Equals(block1.ore_tonnes))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z) || !blocks[1].cost.Equals(block2.cost) || !blocks[1].value.Equals(block2.value) || !blocks[1].rock_tonnes.Equals(block2.rock_tonnes) || !blocks[1].ore_tonnes.Equals(block2.ore_tonnes))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }

        //Method Tested: GatherBlocksKD
        //Context: Passing a file containing:
        //6 15 1 18 16380 -12285 2 0 0
        //7 16 1 18 16380 -12285 2 0 0
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksKDTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksKDTestFile.blocks";
            Block block1 = new Block
            {
                id = 6,
                x = 15,
                y = 1,
                z = 18,
                blockvalue = 16380,
                tonn = -12285,
                destination = 2,
                cu = 0,
                porc_profit = 0
            };
            Block block2 = new Block
            {
                id = 7,
                x = 16,
                y = 1,
                z = 18,
                blockvalue = 16380,
                tonn = -12285,
                destination = 2,
                cu = 0,
                porc_profit = 0
            };
            List<Block> blocks = BlockLoaders.GatherBlocksKD(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z)
                || !blocks[0].blockvalue.Equals(block1.blockvalue) || !blocks[0].tonn.Equals(block1.tonn) || !blocks[0].destination.Equals(block1.destination) ||
                !blocks[0].cu.Equals(block1.cu) || !blocks[0].porc_profit.Equals(block1.porc_profit))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z)
                || !blocks[1].blockvalue.Equals(block2.blockvalue) || !blocks[1].tonn.Equals(block2.tonn) || !blocks[1].destination.Equals(block2.destination) ||
                !blocks[1].cu.Equals(block2.cu) || !blocks[1].porc_profit.Equals(block2.porc_profit))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }
    }
}
