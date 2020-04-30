using System.Collections.Generic;
using System.IO;

namespace PreEntregaProjecto1SoftwareDesign
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
                            if (att[1].Equals("cont")) contAttributes.Add(double.Parse(cubeData[i]));
                            if (att[1].Equals("prop")) massPropAttributes.Add(double.Parse(cubeData[i]));
                            if (att[1].Equals("cat")) catAttributes.Add(cubeData[i]);
                            if (att[1].Equals("weight")) weight = double.Parse(cubeData[i]);
                        }
                        else
                        {
                            if (att[0].Equals("id")) id = int.Parse(cubeData[i]);
                            if (att[0].Equals("x")) x = int.Parse(cubeData[i]);
                            if (att[0].Equals("y")) y = int.Parse(cubeData[i]);
                            if (att[0].Equals("z")) z = int.Parse(cubeData[i]);
                        }
                    }

                    var block = new Block(id, x, y, z, weight,
                        contAttributes,massPropAttributes,catAttributes, blockModel);
                    blocks.Add(block);
                }
            }
            return blocks;
        }
    }
}
