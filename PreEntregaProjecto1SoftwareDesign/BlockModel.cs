using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class BlockModel
    {
        public string name;
        public List<Block> blocks { get; set; }

        public BlockModel(List<Block> blocks, string name)
        {
            this.name = name;
            this.blocks = blocks;
        }

        public Block GetBlock(int x, int y, int z)
        {
            return blocks.Where(i => i.x == x).Where(i => i.y == y).First(i => i.z == z);
        }

        public int GetNumberOfBlocks()
        {
            return blocks.Count;
        }
    }
}