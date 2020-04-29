﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SDPreSubmissionNS;

namespace SDPreSubmissionNS
{
    class BlockLoaders
    {

        static public List<Block> GatherBlocks(string path, BlockModel blockModel, bool has_mass)
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
                        int weight = 0;
                        if (has_mass) weight = int.Parse(cubeData[4]);
                        List<string> otherAttributes = new List<string>();
                        for (int i = 4; i < cubeData.Length; i++) {
                            otherAttributes.Add(cubeData[i]);
                        }

                        Block block = new Block(id, x, y, z, weight, otherAttributes, blockModel);
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
