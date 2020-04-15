using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS
{
    public class Block
    {
        //<id> <x> <y> <z> <tonn> <au [ppm]> <cu %> <proc_profit>
        public int id { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int z { get; set; }
        public double tonn { get; set; }
        public double au { get; set; }
        public double cu { get; set; }
        public double porc_profit { get; set; }

        public Block() { }
        public Block(int id, int x, int y, int z , double tonn, double au, double cu, double porc_profit)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
            this.tonn = tonn;
            this.au = au;
            this.cu = cu;
            this.porc_profit = porc_profit;
        }
    }
}
