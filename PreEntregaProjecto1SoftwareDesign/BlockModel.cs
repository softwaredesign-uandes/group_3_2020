using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class BlockModel
    {
        public string Name;
        public List<string> ContinuousAttributesNames;
        public List<string> MassProportionalAttributesNames;
        public List<string> CategoricalAttributesNames;
        public List<Block> Blocks { get; set; }

        public BlockModel(string name, List<string> continuousAttributesNames,
            List<string> massProportionalAttributesNames, List<string> categoricalAttributesNames)
        {
            Name = name;
            ContinuousAttributesNames = continuousAttributesNames;
            MassProportionalAttributesNames = massProportionalAttributesNames;
            CategoricalAttributesNames = categoricalAttributesNames;
        }

        public BlockModel(string name)
        {
            Name = name;
        }

        public void SetBlocks(List<Block> blocks)
        {
            Blocks = blocks;
        }

        public List<string> GetPossibleAttributes()
        {
            List<string> attributes = new List<string>();
            attributes.Add("x");
            attributes.Add("y");
            attributes.Add("z");
            attributes.Add("id");
            attributes.Add("Weight");
            foreach (string attribute_name in ContinuousAttributesNames)
            {
                attributes.Add(attribute_name);
            }
            foreach (string attribute_name in MassProportionalAttributesNames) {
                attributes.Add(attribute_name);
            }
            foreach (string attribute_name in CategoricalAttributesNames) {
                attributes.Add(attribute_name);
            }
            return attributes;
        }
        public Block GetBlock(int x, int y, int z)
        {
            return Blocks.Where(i => i.X == x).Where(i => i.Y == y).FirstOrDefault(i => i.Z == z);
        }

        public int GetNumberOfBlocks()
        {
            return Blocks.Count;
        }

        public void Reblock(int rx, int ry, int rz)
        {

        }
    }
}