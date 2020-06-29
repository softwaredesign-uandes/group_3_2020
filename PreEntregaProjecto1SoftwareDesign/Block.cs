using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class Block
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public double Weight { get; set; }

        public Dictionary<string, double> ContinuousAttributes = new Dictionary<string, double>();

        public Dictionary<string, double> MassProportionalAttributes = new Dictionary<string, double>();

        public Dictionary<string, string> CategoricalAttributes = new Dictionary<string, string>();

        public Block(int id, int x, int y, int z)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
        }
        public Block(int id, int x, int y, int z, double weight)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Weight = weight;
        }

        public Block(int id, int x, int y, int z, double weight,
            List<double> continuousAtt, List<double> massProportionalAtt, List<string> categoricalAtt, BlockModel blockModel)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Weight = weight;
            for (var i = 0; i < continuousAtt.Count; i++)
            {
                ContinuousAttributes.Add(blockModel.ContinuousAttributesNames[i], continuousAtt[i]);
            }
            for (var i = 0; i < massProportionalAtt.Count; i++) {
                MassProportionalAttributes.Add(blockModel.MassProportionalAttributesNames[i], massProportionalAtt[i]);
            }
            for (var i = 0; i < categoricalAtt.Count; i++) {
                CategoricalAttributes.Add(blockModel.CategoricalAttributesNames[i], categoricalAtt[i]);
            }
        }


        public double? GetMassInKg()
        {
            return Weight*1000;
        }
    }
}
