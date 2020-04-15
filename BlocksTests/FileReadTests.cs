using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDPreSubmissionNS;
using System.Collections.Generic;
using System.IO;

namespace BlocksTests
{
    [TestClass]
    public class FileReadTests
    {
        [TestMethod]
        public void BlockListNotEmpty()
        {
            string root = Directory.GetCurrentDirectory() + @"\";
            string path = root + "marvin.blocks";

            List<Block> blocks = Program.GatherBlocks(path);
            
        }
    }
}
