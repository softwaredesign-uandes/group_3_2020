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

        public Dictionary<string, double> other_attributes;

        public Block() { }

        public double? GetMassInKg()
        {
            if (this.tonn == null) return tonn * 1000;
            else return 0;
        }
    }
}
