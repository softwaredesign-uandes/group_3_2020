using System;
using System.Collections.Generic;

namespace PreEntregaProjecto1SoftwareDesign
{
    [Serializable]
    public class Block
    {

        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public Dictionary<string, int> ContinuousAttributes = new Dictionary<string, int>();

        public Dictionary<string, int> MassProportionalAttributes = new Dictionary<string, int>();

        public Dictionary<string, int> CategoricalAttributes = new Dictionary<string, int>();

        public Block(int id, int x, int y, int z,
            List<int> continuousAtt, List<int> massProportionalAtt, List<int> categoricalAtt, BlockModel blockModel)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            for (var i = 0; i < continuousAtt.Count; i++)
            {
                ContinuousAttributes.Add(blockModel.ContinuousAttributesNames[i], continuousAtt[i]);
            }
            for (var i = 0; i < massProportionalAtt.Count; i++) {
                MassProportionalAttributes.Add(blockModel.ContinuousAttributesNames[i], massProportionalAtt[i]);
            }
            for (var i = 0; i < categoricalAtt.Count; i++) {
                CategoricalAttributes.Add(blockModel.ContinuousAttributesNames[i], categoricalAtt[i]);
            }
        }

        public double? GetMassInKg()
        {
            return ContinuousAttributes["tonn"]*1000;
        }
    }
}
