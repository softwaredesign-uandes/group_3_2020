using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class BlockModel
    {
        public List<Block> blocks { get; set; }

        public BlockModel(List<Block> blocks)
        {
            this.blocks = blocks;
        }

        public int GetNumberOfBlocks()
        {
            return blocks.count;
        }
    }
}