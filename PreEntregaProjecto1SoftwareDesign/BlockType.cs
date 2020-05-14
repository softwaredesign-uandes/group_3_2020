using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS {
    [Serializable]
    public class BlockType {
        public double Weight { get; set; }

        public Dictionary<string, double> ContinuousAttributes = new Dictionary<string, double>();

        public Dictionary<string, double> MassProportionalAttributes = new Dictionary<string, double>();

        public Dictionary<string, string> CategoricalAttributes = new Dictionary<string, string>();

        public BlockType(double weight, Dictionary<string, double> continuousAtt, Dictionary<string, double> massProportionalAtt, Dictionary<string, string> categoricalAtt)
        {
            Weight = weight;
            ContinuousAttributes = continuousAtt;
            MassProportionalAttributes = massProportionalAtt;
            CategoricalAttributes = categoricalAtt;
        }
    }
}
