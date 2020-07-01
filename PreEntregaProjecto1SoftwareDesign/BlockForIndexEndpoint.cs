using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDPreSubmissionNS
{
    public class BlockForIndexEndpoint
    {
        public int index { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Dictionary<string, double> grades = new Dictionary<string, double>();
        public double mass { get; set; }


        public BlockForIndexEndpoint(int index, int x, int y, int z, double mass, Dictionary<string, double> MassProportionalAttributes)
        {
            this.index = index;
            this.x = x;
            this.y = y;
            this.z = z;
            this.mass = mass;
            this.grades = MassProportionalAttributes;
        }

    }
}
