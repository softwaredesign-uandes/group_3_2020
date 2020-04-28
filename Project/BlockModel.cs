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
        public List<string> other_attributes_names;
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
            foreach (string attribute_name in other_attributes_names)
            {
                attributes.Add(attribute_name);
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