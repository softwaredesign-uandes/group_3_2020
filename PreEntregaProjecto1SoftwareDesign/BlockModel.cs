using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.XPath;
using PreEntregaProjecto1SoftwareDesign;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class BlockModel:Publisher
    {
        public string Name;
        public List<string> ContinuousAttributesNames;
        public List<string> MassProportionalAttributesNames;
        public List<string> CategoricalAttributesNames;
        public List<Block> Blocks { get; set; }

        public BlockModel(string name, List<string> continuousAttributesNames,
            List<string> massProportionalAttributesNames, List<string> categoricalAttributesNames)
        {
            Name = name;
            ContinuousAttributesNames = continuousAttributesNames;
            MassProportionalAttributesNames = massProportionalAttributesNames;
            CategoricalAttributesNames = categoricalAttributesNames;
        }

        public BlockModel(string name)
        {
            Name = name;
        }

        public void SetBlocks(List<Block> blocks)
        {
            Blocks = blocks;
        }

        public List<string> GetPossibleAttributes()
        {
            List<string> attributes = new List<string>();
            attributes.Add("x");
            attributes.Add("y");
            attributes.Add("z");
            attributes.Add("id");
            attributes.Add("Weight");
            foreach (string attribute_name in ContinuousAttributesNames)
            {
                attributes.Add(attribute_name);
            }
            foreach (string attribute_name in MassProportionalAttributesNames) {
                attributes.Add(attribute_name);
            }
            foreach (string attribute_name in CategoricalAttributesNames) {
                attributes.Add(attribute_name);
            }
            return attributes;
        }
        public Block GetBlock(int x, int y, int z)
        {
            return Blocks.Where(i => i.X == x).Where(i => i.Y == y).FirstOrDefault(i => i.Z == z);
        }

        public int GetNumberOfBlocks()
        {
            return Blocks.Count;
        }

        public int[] GetMaxCoordinates()
        {
            return new[] {Blocks.Max(block => block.X), Blocks.Max(block => block.Y) , Blocks.Max(block => block.Z) };
        }

        public void Reblock(int rx, int ry, int rz)
        {
            new Trace("block_model_reblocked", Name, true);

            List<Block> newBlocks = new List<Block>();
            int[] maximos = GetMaxCoordinates();
            int num_blocks = GetNumberOfBlocks();
            int progress = 1;
            int newId = 0;
            int newIndexX = 0;
            for (int indexGroupX = 0; indexGroupX <= maximos[0]; indexGroupX += rx)
            {
                int newIndexY = 0;
                for (int indexGroupY = 0; indexGroupY <= maximos[1]; indexGroupY += ry)
                {
                    int newIndexZ = 0;
                    for (int indexGroupZ = 0; indexGroupZ <= maximos[2]; indexGroupZ += rz)
                    {
                        Block nuevoBloque = new Block(newId,newIndexX,newIndexY,newIndexZ);
                        foreach (var continuousAttributesName in ContinuousAttributesNames) {
                            nuevoBloque.ContinuousAttributes.Add(continuousAttributesName, 0);
                        }
                        foreach (var massProportionalAttributesName in MassProportionalAttributesNames) {
                            nuevoBloque.MassProportionalAttributes.Add(massProportionalAttributesName, 0);
                        }
                        newId++;
                        Dictionary<string,List<string>> listsOfCategoricalAttributes = new Dictionary<string, List<string>>();
                        foreach (var CategoricalAttributesName in CategoricalAttributesNames) {
                            listsOfCategoricalAttributes.Add(CategoricalAttributesName, new List<string>());
                        }
                        for (int indexX = indexGroupX; indexX < indexGroupX + rx; indexX++)
                        {
                            for (int indexY = indexGroupY; indexY < indexGroupY + ry; indexY++) 
                            {
                                for (int indexZ = indexGroupZ; indexZ < indexGroupZ + rz; indexZ++)
                                {
                                    Block evaluatedBlock = GetBlock(indexX, indexY, indexZ);
                                    if (evaluatedBlock == null)
                                    {
                                        evaluatedBlock = new Block(0, indexX, indexY, indexZ);
                                        evaluatedBlock.Weight = 0;
                                        foreach (var continuousAttributesName in ContinuousAttributesNames)
                                        {
                                            evaluatedBlock.ContinuousAttributes.Add(continuousAttributesName,0);
                                        }
                                        foreach (var massProportionalAttributesName in MassProportionalAttributesNames) 
                                        {
                                            evaluatedBlock.MassProportionalAttributes.Add(massProportionalAttributesName, 0);
                                        }
                                    }
                                    else
                                    {
                                        Notify($"Progress: {progress} of {num_blocks}" );
                                        progress++;
                                    }
                                    nuevoBloque.Weight += evaluatedBlock.Weight;
                                    foreach (var continuousAttribute in evaluatedBlock.ContinuousAttributes)
                                    {
                                        nuevoBloque.ContinuousAttributes[continuousAttribute.Key] += continuousAttribute.Value;
                                    }
                                    foreach (var massProportionalAttribute in evaluatedBlock.MassProportionalAttributes)
                                    {
                                        nuevoBloque.MassProportionalAttributes[massProportionalAttribute.Key] +=
                                            massProportionalAttribute.Value * evaluatedBlock.Weight;
                                    }
                                    foreach (var categoricalAttribute in evaluatedBlock.CategoricalAttributes)
                                    {
                                        listsOfCategoricalAttributes[categoricalAttribute.Key].Add(categoricalAttribute.Value);
                                    }
                                }
                            }
                        }
                        List<string> keysNuevoBloque = new List<string>(nuevoBloque.MassProportionalAttributes.Keys);
                        foreach (string keyNuevoBloqueMassProportionalAttribute in keysNuevoBloque)
                        {
                            if (nuevoBloque.Weight != 0)
                            {
                                nuevoBloque.MassProportionalAttributes[keyNuevoBloqueMassProportionalAttribute] /=
                                    nuevoBloque.Weight;
                            }
                        }

                        foreach (var listOfCategoricalAttribute in listsOfCategoricalAttributes)
                        {
                            if (listOfCategoricalAttribute.Value.Count != 0)
                            {
                                nuevoBloque.CategoricalAttributes[listOfCategoricalAttribute.Key] =
                                    listOfCategoricalAttribute.Value.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                                        .Select(grp => grp.Key).First();
                            }
                        }
                        newBlocks.Add(nuevoBloque);
                        newIndexZ++;
                    }

                    newIndexY++;
                }

                newIndexX++;
            }

            Blocks = newBlocks;
        }
    }
}