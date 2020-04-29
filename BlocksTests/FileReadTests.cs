using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using PreEntregaProjecto1SoftwareDesign;

namespace BlocksTests
{
    [TestClass]
    public class FileReadTests
    {
        [TestMethod]
        public void BlockListNotEmpty()
        {
            string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\";
            string path = root + "marvin.blocks";

            List<Block> blocks = Program.GatherBlocks(path);

            Assert.IsFalse(blocks.Count.Equals(0), "The list of blocks is empty");
        }
    }
}
