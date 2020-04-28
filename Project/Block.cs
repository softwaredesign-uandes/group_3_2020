using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class Block
    {
        public BlockModel block_model;

        public int id { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int z { get; set; }

        public Dictionary<string, string> other_attributes;

        public Block(int id, int x, int y, int z, List<string> attributes_values)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
            for (int i = 0; i < attributes_values.Count; i++)
            {
                other_attributes.Add(block_model.other_attributes_names[i], attributes_values[i]);
            }
        }

        public double? GetMassInKg()
        {
            if (this.tonn == null) return tonn * 1000;
            else return 0;
        }
    }
}
