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
                blockvalue = -12285,
                tonn = 16380,
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
                blockvalue = -12285,
                tonn = 16380,
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

        //Method Tested: GatherBlocksP4HD
        //Context: Passing a file containing:
        //14 59 13 64 3120 -5116 2 0 1.108 0
        //15 60 13 64 3120 -5116 2 0 1.108 0
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksP4HDTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksP4HDTestFile.blocks";
            Block block1 = new Block
            {
                id = 14,
                x = 59,
                y = 13,
                z = 64,
                tonn = 3120,
                blockvalue = -5116,
                destination = 2,
                au = 0,
                ag = 1.108,
                cu = 0,
            };
            Block block2 = new Block
            {
                id = 15,
                x = 60,
                y = 13,
                z = 64,
                tonn = 3120,
                blockvalue = -5116,
                destination = 2,
                au = 0,
                ag = 1.108,
                cu = 0,
            };
            List<Block> blocks = BlockLoaders.GatherBlocksP4HD(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z)
                || !blocks[0].tonn.Equals(block1.tonn) || !blocks[0].blockvalue.Equals(block1.blockvalue) || !blocks[0].destination.Equals(block1.destination) ||
                !blocks[0].au.Equals(block1.au) || !blocks[0].ag.Equals(block1.ag) || !blocks[0].cu.Equals(block1.cu))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z)
                || !blocks[1].blockvalue.Equals(block2.blockvalue) || !blocks[1].tonn.Equals(block2.tonn) || !blocks[1].destination.Equals(block2.destination) ||
                !blocks[1].cu.Equals(block2.cu) || !blocks[1].au.Equals(block2.au) || !blocks[1].ag.Equals(block2.ag))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }

        //Method Tested: GatherBlocksMarvin
        //Context: Passing a file containing:
        //26 0 1 12 61200.012 0 0 -4
        //27 0 1 13 54720.242 0 0 -4
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksMarvinTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksMarvinTestFile.blocks";
            Block block1 = new Block
            {
                id = 26,
                x = 0,
                y = 1,
                z = 12,
                tonn = 61200.012,
                au = 0,
                cu = 0,
                porc_profit = -4,
            };
            Block block2 = new Block
            {
                id = 27,
                x = 0,
                y = 1,
                z = 13,
                tonn = 54720.242,
                au = 0,
                cu = 0,
                porc_profit = -4,
            };
            List<Block> blocks = BlockLoaders.GatherBlocksMarvin(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z)
                || !blocks[0].tonn.Equals(block1.tonn) || !blocks[0].au.Equals(block1.au) || !blocks[0].porc_profit.Equals(block1.porc_profit) || !blocks[0].cu.Equals(block1.cu))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z)
                || !blocks[1].tonn.Equals(block2.tonn) || !blocks[1].cu.Equals(block2.cu) || !blocks[1].au.Equals(block2.au) || !blocks[1].porc_profit.Equals(block2.porc_profit))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }

        //Method Tested: GatherBlocksW23
        //Context: Passing a file containing:
        //16 27 18 37 1 2 0.37 0.04715 2406.01492 3.7 0.6 2.6 1.901 28.28 0.18 -14.66 -35272 4 ssf
        //17 27 19 37 1 2 0.43 0.0521 2406.01492 3.7 0.6 2.6 1.901 28.28 0.18 -10.198 -24537 4 lsf
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksW23Test()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksW23TestFile.blocks";
            Block block1 = new Block
            {
                id = 16,
                x = 27,
                y = 18,
                z = 37,
                destination = 1,
                phase = 2,
                AuRec = 0.37,
                AuFA = 0.04715,
                tonn = 2406.01492,
                co3 = 3.7,
                orgc = 0.6,
                sulf = 2.6,
                Mcost = 1.901,
                Pcost = 28.28,
                Tcost = 0.18,
                Tvalue = -14.66,
                Bvalue = -35272,
                rc_Stockpile = 4,
                rc_RockChar = "ssf",
            };
            Block block2 = new Block
            {
                id = 17,
                x = 27,
                y = 19,
                z = 37,
                destination = 1,
                phase = 2,
                AuRec = 0.43,
                AuFA = 0.0521,
                tonn = 2406.01492,
                co3 = 3.7,
                orgc = 0.6,
                sulf = 2.6,
                Mcost = 1.901,
                Pcost = 28.28,
                Tcost = 0.18,
                Tvalue = -10.198,
                Bvalue = -24537,
                rc_Stockpile = 4,
                rc_RockChar = "lsf",
            };
            List<Block> blocks = BlockLoaders.GatherBlocksW23(path);

            //Assert if the two Blocks are Equal
            if (!blocks[0].id.Equals(block1.id) || !blocks[0].x.Equals(block1.x) || !blocks[0].y.Equals(block1.y) || !blocks[0].z.Equals(block1.z)
                || !blocks[0].destination.Equals(block1.destination) || !blocks[0].phase.Equals(block1.phase) || !blocks[0].AuRec.Equals(block1.AuRec)
                || !blocks[0].AuFA.Equals(block1.AuFA) || !blocks[0].tonn.Equals(block1.tonn) || !blocks[0].co3.Equals(block1.co3)
                || !blocks[0].orgc.Equals(block1.orgc) || !blocks[0].sulf.Equals(block1.sulf) || !blocks[0].Mcost.Equals(block1.Mcost)
                || !blocks[0].Pcost.Equals(block1.Pcost) || !blocks[0].Tcost.Equals(block1.Tcost) || !blocks[0].Tvalue.Equals(block1.Tvalue)
                || !blocks[0].Bvalue.Equals(block1.Bvalue) || !blocks[0].rc_Stockpile.Equals(block1.rc_Stockpile) || !blocks[0].rc_RockChar.Equals(block1.rc_RockChar))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].id.Equals(block2.id) || !blocks[1].x.Equals(block2.x) || !blocks[1].y.Equals(block2.y) || !blocks[1].z.Equals(block2.z)
                || !blocks[1].destination.Equals(block2.destination) || !blocks[1].phase.Equals(block2.phase) || !blocks[1].AuRec.Equals(block2.AuRec)
                || !blocks[1].AuFA.Equals(block2.AuFA) || !blocks[1].tonn.Equals(block2.tonn) || !blocks[1].co3.Equals(block2.co3)
                || !blocks[1].orgc.Equals(block2.orgc) || !blocks[1].sulf.Equals(block2.sulf) || !blocks[1].Mcost.Equals(block2.Mcost)
                || !blocks[1].Pcost.Equals(block2.Pcost) || !blocks[1].Tcost.Equals(block2.Tcost) || !blocks[1].Tvalue.Equals(block2.Tvalue)
                || !blocks[1].Bvalue.Equals(block2.Bvalue) || !blocks[1].rc_Stockpile.Equals(block2.rc_Stockpile) || !blocks[1].rc_RockChar.Equals(block2.rc_RockChar))
            {
                Assert.Fail("block2 is different that the estimated block");
            }
        }
    }
}
