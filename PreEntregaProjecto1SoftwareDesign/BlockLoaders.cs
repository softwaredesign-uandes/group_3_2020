using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SDPreSubmissionNS;

namespace SDPreSubmissionNS
{
    internal class BlockLoaders
    {

        public static List<Block> GatherBlocks(string path, List<string> all_attributes, BlockModel blockModel)
        {
            var blocks = new List<Block>();
            if (!File.Exists(path)) return blocks;
            using (var streamReader = new StreamReader(path)) {
                string line;
                while ((line = streamReader.ReadLine()) != null) {
                    var cubeData = line.Trim(' ').Split(' ');
                    int id = 0;
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    double weight = 0;
                    List<double> contAttributes = new List<double>();
                    List<double> massPropAttributes = new List<double>();
                    List<string> catAttributes = new List<string>();

                    for (int i = 0; i < all_attributes.Count; i++)
                    {
                        string[] att = all_attributes[i].Split(":");
                        if (att.Length>1)
                        {
                            if (att[1].Equals("cont")) contAttributes.Add(double.Parse(cubeData[i].Replace('.', ',')));
                            if (att[1].Equals("prop")) massPropAttributes.Add(double.Parse(cubeData[i].Replace('.', ',')));
                            if (att[1].Equals("cat")) catAttributes.Add(cubeData[i]);
                            if (att[1].Equals("weight")) weight = double.Parse(cubeData[i].Replace('.', ','));
                        }
                        else
                        {
                            if (att[0].Equals("id")) id = int.Parse(cubeData[i]);
                            if (att[0].Equals("x")) x = int.Parse(cubeData[i]);
                            if (att[0].Equals("y")) y = int.Parse(cubeData[i]);
                            if (att[0].Equals("z")) z = int.Parse(cubeData[i]);
                        }
                    }
                    Dictionary<string, double> continuousAttributes = new Dictionary<string, double>();
                    Dictionary<string, double> massProportionalAttributes = new Dictionary<string, double>();
                    Dictionary<string, string> categoricalAttributes = new Dictionary<string, string>();
                    for (var i = 0; i < contAttributes.Count; i++) {
                        continuousAttributes.Add(blockModel.ContinuousAttributesNames[i], contAttributes[i]);
                    }
                    for (var i = 0; i < massPropAttributes.Count; i++) {
                        massProportionalAttributes.Add(blockModel.MassProportionalAttributesNames[i], massPropAttributes[i]);
                    }
                    for (var i = 0; i < catAttributes.Count; i++)
                    {
                        categoricalAttributes.Add(blockModel.CategoricalAttributesNames[i], catAttributes[i]);
                    }

                    BlockType blockType = BlockFactory.getBlockType(weight, continuousAttributes,
                        massProportionalAttributes, categoricalAttributes);
                    var block = new Block(id, x, y, z, blockType);
                    blocks.Add(block);
                }
            }
            return blocks;
        }
    }
}
