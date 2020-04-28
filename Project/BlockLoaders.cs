using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SDPreSubmissionNS;

namespace SDPreSubmissionNS
{
    class BlockLoaders
    {

        static public List<Block> GatherBlocks(string path, BlockModel blockModel)
        {

            if (File.Exists(path))
            {
                List<Block> blocks = new List<Block>();

                using (StreamReader streamReader = new StreamReader(path)) {
                    string line;
                    while ((line = streamReader.ReadLine()) != null) {
                        string[] cubeData = line.Trim(' ').Split(' ');
                        int id = int.Parse(cubeData[0]);
                        int x = int.Parse(cubeData[1]);
                        int y = int.Parse(cubeData[2]);
                        int z = int.Parse(cubeData[3]);
                        List<string> otherAttributes;
                        for (int i = 4; i < cubeData.Count; i++) {
                            otherAttributes.Add(string.Parse(cubeData[i]));
                        }

                        Block block = new Block(id, x, y, z, otherAttributes, blockModel);
                        blocks.Add(block);
                    }
                }
                return blocks;
            }
            else
            {
                return new List<Block>();
            }
        }
    }
}
