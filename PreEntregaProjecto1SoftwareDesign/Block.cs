using System;
using System.Collections.Generic;
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

        #region Marvin
        //<id> <x> <y> <z> <tonn> <au [ppm]> <cu %> <proc_profit>
        public double? tonn { get; set; }
        public double? au { get; set; }
        public double? cu { get; set; }
        public double? porc_profit { get; set; }
        #endregion

        #region newman1
        //id x y z type grade tonns min_caf value_extracc value_proc apriori_process
        public string? type { get; set; }
        public double? grade { get; set; }
        //public double tonns { get; set; } usar tonn de Marvin
        public double? min_caf { get; set; }
        public double? value_extracc { get; set; }
        public double? value_proc { get; set; }
        public double? apriori_process { get; set; }
        #endregion

        #region zuck
        //id x y z cost value rock_tonnes ore_tonnes
        public double? cost { get; set; }
        public double? value { get; set; }
        public double? rock_tonnes { get; set; }
        public double? ore_tonnes { get; set; }
        #endregion

        #region p4hd
        //p4hd ocupa cosas que ya estaban antes
        //<id> <x> <y> <z> <tonn> <blockvalue> <destination> <Au (oz/ton)> <Ag (oz/ton)> <Cu %>
        public double? blockvalue { get; set; }
        public double? destination { get; set; }
        public double? ag { get; set; }
        #endregion

        #region w23
        //id x y z dest phase AuRec AuFA tons co3 orgc sulf Mcost Pcost Tcost Tvalue Bvalue rc_Stockpile rc_RockChar
        public double? phase { get; set; }
        public double? AuRec { get; set; }
        public double? AuFA { get; set; }
        //public double tons { get; set; } Usar "tonns" de marvin
        public double? co3 { get; set; }
        public double? orgc { get; set; }
        public double? sulf { get; set; }
        public double? Mcost { get; set; }
        public double? Pcost { get; set; }
        public double? Tcost { get; set; }
        public double? Tvalue { get; set; }
        public double? Bvalue { get; set; }
        public double? rc_Stockpile { get; set; }
        public double? rc_RockChar { get; set; }

        #endregion

        #region mclaughin
        //id x y z blockvalue ton destination Au(oz/ton)
        //todo está antes
        #endregion

        public Block() { }

        public double? GetMassInKg()
        {
            if (tonn!=null) return tonn * 1000;
            else return 0;
        }

        public double? GetGrade(string mineral)
        {
            if (tonn!=null)
            {
                var prop = this.GetType().GetProperty(mineral);
                return prop.GetValue(this, null) * 100 / tonn;
            }
            else return 0;
        }
    }
}
