using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.XPath;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class BlockModel
    {
        public string Name;
        public List<string> ContinuousAttributesNames;
        public List<string> MassProportionalAttributesNames;
        public List<string> CategoricalAttributesNames;
        public List<IComponent> Blocks { get; set; }

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

        public void SetBlocks(List<IComponent> blocks)
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
        public IComponent GetBlock(int x, int y, int z)
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
            List<IComponent> newBlocks = new List<IComponent>();
            int[] maximos = GetMaxCoordinates();
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
                        CompositeBlock nuevoBloque = new CompositeBlock(newId,newIndexX,newIndexY,newIndexZ, this);
                        newId++;

                        for (int indexX = indexGroupX; indexX < indexGroupX + rx; indexX++)
                        {
                            for (int indexY = indexGroupY; indexY < indexGroupY + ry; indexY++) 
                            {
                                for (int indexZ = indexGroupZ; indexZ < indexGroupZ + rz; indexZ++)
                                {
                                    IComponent evaluatedBlock = GetBlock(indexX, indexY, indexZ);
                                    if (evaluatedBlock == null)
                                    {
                                        Block emptyBlock = new Block(0, indexX, indexY, indexZ);
                                        emptyBlock.Weight = 0;
                                        foreach (var continuousAttributesName in ContinuousAttributesNames)
                                        {
                                            emptyBlock.ContinuousAttributes.Add(continuousAttributesName,0);
                                        }
                                        foreach (var massProportionalAttributesName in MassProportionalAttributesNames) 
                                        {
                                            emptyBlock.MassProportionalAttributes.Add(massProportionalAttributesName, 0);
                                        }

                                        foreach (var categoricalAttributesName in CategoricalAttributesNames)
                                        {
                                            emptyBlock.CategoricalAttributes.Add(categoricalAttributesName,"");
                                        }
                                        nuevoBloque.AddComponent(emptyBlock);
                                    }
                                    else
                                    {
                                        nuevoBloque.AddComponent(evaluatedBlock);
                                    }
                                }
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