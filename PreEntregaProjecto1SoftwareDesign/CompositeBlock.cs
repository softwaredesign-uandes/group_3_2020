using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SDPreSubmissionNS {
    public class CompositeBlock:IComponent
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockModel BlockModel;


        public List<IComponent> Components = new List<IComponent>();

        public CompositeBlock(int id, int x, int y, int z, BlockModel blockModel) {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            BlockModel = blockModel;
        }
        public double GetWeight()
        {
            double total_weight = 0;
            foreach (var component in Components)
            {
                total_weight += component.GetWeight();
            }

            return total_weight;
        }

        public Dictionary<string, double> GetContinuousAtt() {
            Dictionary<string, double> newDictionary = new Dictionary<string, double>();
            foreach (var continuousAttributesName in BlockModel.ContinuousAttributesNames) {
                newDictionary.Add(continuousAttributesName, 0);
            }
            foreach (var component in Components) 
            {
                foreach (var ValueTuple in component.GetContinuousAtt())
                {
                    newDictionary[ValueTuple.Key] += ValueTuple.Value;
                }
            }
            return newDictionary;
        }

        public Dictionary<string, double> GetProportionalAtt() {
            Dictionary<string, double> newDictionary = new Dictionary<string, double>();
            foreach (var blockModelMassProportionalAttributesName in BlockModel.MassProportionalAttributesNames)
            {
                newDictionary.Add(blockModelMassProportionalAttributesName,0);
            }
            foreach (var component in Components)
            {
                foreach (var ValueTuple in component.GetProportionalAtt())
                {
                    newDictionary[ValueTuple.Key] += ValueTuple.Value * component.GetWeight();
                }
            }
            foreach (var blockModelMassProportionalAttributesName in BlockModel.MassProportionalAttributesNames)
            {
                newDictionary[blockModelMassProportionalAttributesName] /= GetWeight();
            }
            return newDictionary;
        }

        public Dictionary<string, string> GetCategoricalAtt() {
            Dictionary<string, string> newDictionary = new Dictionary<string, string>();

            Dictionary<string, List<string>> listsOfCategoricalAttributes = new Dictionary<string, List<string>>();

            foreach (var blockModelCategoricalAttributesName in BlockModel.CategoricalAttributesNames)
            {
                listsOfCategoricalAttributes.Add(blockModelCategoricalAttributesName, new List<string>());
            }
            foreach (var component in Components)
            {
                foreach (var ValueTuple in component.GetCategoricalAtt())
                {
                    if (ValueTuple.Value != "")
                    {
                        listsOfCategoricalAttributes[ValueTuple.Key].Add(ValueTuple.Value);
                    }
                }
            }
            foreach (var listOfCategoricalAttribute in listsOfCategoricalAttributes) {
                if (listOfCategoricalAttribute.Value.Count != 0) {
                    newDictionary[listOfCategoricalAttribute.Key] =
                        listOfCategoricalAttribute.Value.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                            .Select(grp => grp.Key).First();
                }
            }
            return newDictionary;
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
        }


    }
}
