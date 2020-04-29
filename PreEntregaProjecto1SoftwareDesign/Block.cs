using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class Block
    {

        public int id { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int z { get; set; }

        public int weight { get; set; }

        public Dictionary<string, string> other_attributes = new Dictionary<string, string>();

        public Block(int id, int x, int y, int z, int weight, List<string> attributes_values, BlockModel block_model)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
            this.weight = weight;
            for (int i = 0; i < attributes_values.Count; i++)
            {
                other_attributes.Add(block_model.other_attributes_names[i], attributes_values[i]);
            }
        }

        public double? GetMassInKg()
        {
            return weight;
        }
    }
}
