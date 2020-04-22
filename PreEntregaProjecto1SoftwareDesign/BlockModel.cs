using System;
using System.Collections.Generic;
using System.Text;

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

        public int GetNumberOfBlocks()
        {
            return blocks.Count;
        }
    }
}