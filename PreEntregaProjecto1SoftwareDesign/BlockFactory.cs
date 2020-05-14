using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace SDPreSubmissionNS {
    [Serializable]
    public class BlockFactory
    {
        public static List<BlockType> BlockTypes = new List<BlockType>();
        public static BlockModel BlockModel;

        public static BlockType getBlockType(double weight, Dictionary<string, double> continuousAttributes,
            Dictionary<string, double> massProportionalAttributes, Dictionary<string, string> categoricalAttributes)
        {
            BlockType type = BlockTypes.Where(r =>
                    r.MassProportionalAttributes.Count == massProportionalAttributes.Count &&
                    !r.MassProportionalAttributes.Except(massProportionalAttributes).Any())
                .Where(r => r.ContinuousAttributes.Count == continuousAttributes.Count &&
                            !r.ContinuousAttributes.Except(continuousAttributes).Any())
                .Where(r => r.CategoricalAttributes.Count == categoricalAttributes.Count &&
                            !r.CategoricalAttributes.Except(categoricalAttributes).Any())
                .FirstOrDefault(r => r.Weight == weight);
            if (type == null)
            {
                type = new BlockType(weight, continuousAttributes, massProportionalAttributes, categoricalAttributes);
                BlockTypes.Add(type);
            }

            return type;
        }
    }
}
