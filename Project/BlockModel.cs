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

        public List<string> GetPossibleAtrributes()
        {
            List<string> attributes = new List<string>();
            attributes.Add("x");
            attributes.Add("y");
            attributes.Add("z");
            attributes.Add("id");
            Block block = blocks.FirstOrDefault(); //We get the first block to get the dictionary for the other variables
            foreach (KeyValuePair<string, double> attribute in block.other_attributes) {
                attributes.Add(attribute.Key);
            }
            return attributes;
        }
        public Block GetBlock(int x, int y, int z)
        {
            return blocks.Where(i => i.x == x).Where(i => i.y == y).FirstOrDefault(i => i.z == z);
        }

        public int GetNumberOfBlocks()
        {
            return blocks.Count;
        }
    }
}