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
        public bool AreBlocksEqual(Block block1, Block block2)
        {
            if (!block1.Id.Equals(block2.Id) || !block1.X.Equals(block2.X) || !block1.Y.Equals(block2.Y) || !block1.Z.Equals(block2.Z))
            {
                return false;
            }
            foreach (KeyValuePair<string, double> keyValuePair in block1.ContinuousAttributes)
            {
                if (block1.ContinuousAttributes[keyValuePair.Key] != block2.ContinuousAttributes[keyValuePair.Key])
                {
                    return false;
                }
            }
            foreach (KeyValuePair<string, double> keyValuePair in block1.MassProportionalAttributes)
            {
                if (block1.MassProportionalAttributes[keyValuePair.Key] != block2.MassProportionalAttributes[keyValuePair.Key])
                {
                    return false;
                }
            }
            foreach (KeyValuePair<string, string> keyValuePair in block1.CategoricalAttributes)
            {
                if (block1.CategoricalAttributes[keyValuePair.Key] != block2.CategoricalAttributes[keyValuePair.Key])
                {
                    return false;
                }
            }

            return true;
        }

        
        //Method Tested: GatherBlocksDefault
        //Context: Passing a file containing:
        //1 13 17 10
        //2 22 30 50
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksDefaultTest()
        {
            string attributesString = "id x y z";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksDefaultTestFile.blocks";
            Block block1 = new Block(1, 13, 17, 10);
            Block block2 = new Block(2, 22, 30, 50);

            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!blocks[0].Id.Equals(block1.Id) || !blocks[0].X.Equals(block1.X) || !blocks[0].Y.Equals(block1.Y) || !blocks[0].Z.Equals(block1.Z))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!blocks[1].Id.Equals(block2.Id) || !blocks[1].X.Equals(block2.X) || !blocks[1].Y.Equals(block2.Y) || !blocks[1].Z.Equals(block2.Z))
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
            string attributesString = "id x y z type:cat grade:prop tonns:weight min_caf:cont value_extracc:cont value_proc:cont apriori_process:cat";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);

            List<double> contAttr1 = new List<double> { 1.04, -5890.56, 24829.116 };
            List<double> massAttr1 = new List<double> { 1.375353107 };
            List<string> catAttr1 = new List<string> { "FROR", "1" };
            Block block1 = new Block(1, 1, 1, 15, 5664, contAttr1, massAttr1, catAttr1,testBlockModel);
            if (!AreBlocksEqual(blocks[0], block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }

            List<double> contAttr2 = new List<double> { 1.03, -5339.52, 34347.213 };
            List<double> massAttr2 = new List<double> { 0.913001543 };
            List<string> catAttr2 = new List<string> { "OXOR", "0" };
            Block block2 = new Block(2, 1, 1, 16, 5184, contAttr2, massAttr2, catAttr2, testBlockModel);
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block1 is different than the estimated block.");
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
            string attributesString = "id x y z cost:cont value:cont rock_tonnes:cont ore_tonnes:cont";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);


            List<double> contAttr1 = new List<double> { 20122.8000000000, 0, 5528.2500000000, 0 };
            List<double> massAttr1 = new List<double> { };
            List<string> catAttr1 = new List<string> { };
            Block block1 = new Block(8, 0, 32, 28, 0, contAttr1, massAttr1, catAttr1, testBlockModel);

            //9 0 33 28 20122.8000000000 0 5528.2500000000 0
            List<double> contAttr2 = new List<double> { 20122.8000000000, 0, 5528.2500000000, 0 };
            List<double> massAttr2 = new List<double> { };
            List<string> catAttr2 = new List<string> { };
            Block block2 = new Block(9, 0, 33, 28, 0, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0],block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }

            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
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
            string attributesString = "id x y z tonn:weight blockvalue:cont destination:cat CU:prop process_profit:prop";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);


            List<double> contAttr1 = new List<double> { -12285 };
            List<double> massAttr1 = new List<double> { 0,0 };
            List<string> catAttr1 = new List<string> { "2" };
            Block block1 = new Block(6, 15, 1, 18, 16380, contAttr1, massAttr1, catAttr1, testBlockModel);

            List<double> contAttr2 = new List<double> { -12285 };
            List<double> massAttr2 = new List<double> { 0,0 };
            List<string> catAttr2 = new List<string> { "2" };
            Block block2 = new Block(7, 16, 1, 18, 16380, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0],block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
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
            string attributesString = "id x y z tonn:weight blockvalue:cont destination:cat Au:prop Ag:prop Cu:prop";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);
            List<double> contAttr1 = new List<double> { -5116 };
            List<double> massAttr1 = new List<double> { 0, 1.108, 0 };
            List<string> catAttr1 = new List<string> { "2" };
            Block block1 = new Block(14, 59, 13, 64, 3120, contAttr1, massAttr1, catAttr1, testBlockModel);

            List<double> contAttr2 = new List<double> { -5116 };
            List<double> massAttr2 = new List<double> { 0, 1.108, 0 };
            List<string> catAttr2 = new List<string> { "2" };
            Block block2 = new Block(15, 60, 13, 64, 3120, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0], block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
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
            string attributesString = "id x y z tonn:weight au:prop cu:prop proc_profit:cont";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);

            List<double> contAttr1 = new List<double> { -4 };
            List<double> massAttr1 = new List<double> { 0, 0 };
            List<string> catAttr1 = new List<string> { };
            Block block1 = new Block(26, 0, 1, 12, 61200.012, contAttr1, massAttr1, catAttr1, testBlockModel);

            List<double> contAttr2 = new List<double> { -4 };
            List<double> massAttr2 = new List<double> { 0, 0 };
            List<string> catAttr2 = new List<string> { };
            Block block2 = new Block(27, 0, 1, 13, 54720.242, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0], block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
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
            string attributesString = "id x y z dest:cat phase:cat AuRec:prop AuFA:prop tons:weight co3:prop orgc:prop sulf:prop Mcost:cont Pcost:cont Tcost:cont Tvalue:cont Bvalue:cont rc_Stockpile:cat rc_RockChar:cat";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);

            List<double> contAttr1 = new List<double> { 1.901, 28.28, 0.18, -14.66, -35272 };
            List<double> massAttr1 = new List<double> { 0.37, 0.04715, 3.7, 0.6, 2.6 };
            List<string> catAttr1 = new List<string> { "1", "2", "4", "ssf" };
            Block block1 = new Block(16, 27, 18, 37, 2406.01492, contAttr1, massAttr1, catAttr1, testBlockModel);

            List<double> contAttr2 = new List<double> { 1.901, 28.28, 0.18, -10.198, -24537 };
            List<double> massAttr2 = new List<double> { 0.43, 0.0521, 3.7, 0.6, 2.6 };
            List<string> catAttr2 = new List<string> { "1", "2", "4", "lsf" };
            Block block2 = new Block(17, 27, 19, 37, 2406.01492, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0], block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
            }
        }


        //Method Tested: GatherBlocksMcLaughlin
        //Context: Passing a file containing:
        //84 50 267 66 -468 354.17 0 0
        //85 51 267 66 -357 270.83 0 0
        //Expectation: Should create two blocks with the same attributes given and add them to a Block list that will be returned.
        [TestMethod]
        public void GatherBlocksMcLaughlinTest()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\BlockLoaderTestsFiles\GatherBlocksMcLaughlinTestFile.blocks";
            string attributesString = "id x y z blockvalue:cont ton:weight destination:cat Au:prop";
            List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
            List<string> continuous_att = new List<string>();
            List<string> mass_proportional_att = new List<string>();
            List<string> categorical_att = new List<string>();
            foreach (string attribute in attributesSplit)
            {
                string[] att = attribute.Split(":");
                if (att.Length <= 1) continue;
                if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
            }
            BlockModel testBlockModel = new BlockModel("Test", continuous_att, mass_proportional_att, categorical_att);
            List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, testBlockModel);

            List<double> contAttr1 = new List<double> { -468 };
            List<double> massAttr1 = new List<double> { 0 };
            List<string> catAttr1 = new List<string> { "0" };
            Block block1 = new Block(84, 50, 267, 66, 354.17, contAttr1, massAttr1, catAttr1, testBlockModel);

            List<double> contAttr2 = new List<double> { -357 };
            List<double> massAttr2 = new List<double> { 0 };
            List<string> catAttr2 = new List<string> { "0" };
            Block block2 = new Block(85, 51, 267, 66, 270.83, contAttr2, massAttr2, catAttr2, testBlockModel);

            //Assert if the two Blocks are Equal
            if (!AreBlocksEqual(blocks[0], block1))
            {
                Assert.Fail("block1 is different than the estimated block.");
            }
            if (!AreBlocksEqual(blocks[1], block2))
            {
                Assert.Fail("block2 is different than the estimated block.");
            }
        }
        
    }
}
