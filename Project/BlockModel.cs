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
            if (name == "newman1.blocks")
            {
                attributes.Add("type");
                attributes.Add("grade");
                attributes.Add("tonns");
                attributes.Add("tons");
                attributes.Add("tonn");
                attributes.Add("min_caf");
                attributes.Add("value_extracc");
                attributes.Add("value_proc");
                attributes.Add("apiori_process");
            }
            else if (name == "zuck_small.blocks" || name == "zuck_medium.blocks" || name == "zuck_large.blocks")
            {
                attributes.Add("cost");
                attributes.Add("value");
                attributes.Add("rock_tonnes");
                attributes.Add("ore_tonnes");
            }
            else if (name == "kd.blocks")
            {
                attributes.Add("tonns");
                attributes.Add("tonn");
                attributes.Add("blockvalue");
                attributes.Add("destination");
                attributes.Add("CU");
                attributes.Add("process_profit");
            }
            else if (name == "p4hd.blocks")
            {
                attributes.Add("tonn");
                attributes.Add("tonns");
                attributes.Add("blockvalue");
                attributes.Add("destination");
                attributes.Add("au");
                attributes.Add("ag");
                attributes.Add("Cu");
            }
            else if (name == "marvin.blocks")
            {
                attributes.Add("tonn");
                attributes.Add("tonns");
                attributes.Add("au");
                attributes.Add("cu");
                attributes.Add("proc_profit");
            }
            else if (name == "w23.blocks")
            {
                attributes.Add("dest");
                attributes.Add("destination");
                attributes.Add("phase");
                attributes.Add("AuRec");
                attributes.Add("AuFA");
                attributes.Add("tons");
                attributes.Add("co3");
                attributes.Add("orgc");
                attributes.Add("sulf");
                attributes.Add("Mcost");
                attributes.Add("Pcost");
                attributes.Add("Tcost");
                attributes.Add("Tvalue");
                attributes.Add("Bvalue");
                attributes.Add("rc_Stockpile");
                attributes.Add("rc_RockChar");
            }
            else if (name == "mclaughlin_limit.blocks" || name == "mclaughlin.blocks")
            {
                attributes.Add("blockvalue");
                attributes.Add("ton");
                attributes.Add("destination");
                attributes.Add("Au");
                attributes.Add("tonn");
                attributes.Add("tonns");
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