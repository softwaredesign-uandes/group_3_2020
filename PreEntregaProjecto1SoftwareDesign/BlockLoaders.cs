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
                    int weight = 0;
                    List<int> contAttributes = new List<int>();
                    List<int> massPropAttributes = new List<int>();
                    List<int> catAttributes = new List<int>();

                    for (int i = 0; i < all_attributes.Count; i++)
                    {
                        string[] att = all_attributes[i].Split(":");
                        if (att.Length>1)
                        {
                            if (att[1].Equals("cont")) contAttributes.Add(int.Parse(cubeData[i]));
                            if (att[1].Equals("mass")) massPropAttributes.Add(int.Parse(cubeData[i]));
                            if (att[1].Equals("cat")) catAttributes.Add(int.Parse(cubeData[i]));
                            if (att[1].Equals("weight")) weight = int.Parse(cubeData[i]);
                        }
                        else
                        {
                            if (att.Equals("id")) id = int.Parse(cubeData[i]);
                            if (att.Equals("x")) x = int.Parse(cubeData[i]);
                            if (att.Equals("y")) y = int.Parse(cubeData[i]);
                            if (att.Equals("z")) z = int.Parse(cubeData[i]);
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
