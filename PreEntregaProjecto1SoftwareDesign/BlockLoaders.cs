using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SDPreSubmissionNS;

namespace SDPreSubmissionNS
{
    class BlockLoaders
    {
        static public List<Block> GatherBlocks(string path)
        {

            if (File.Exists(path))
            {
                List<Block> blocks = new List<Block>();
                //Codigo
                FileInfo fileInfo = new FileInfo(path);
                switch (fileInfo.Name.ToLower())
                {
                    case "newman1.blocks":
                        blocks = GatherBlocksNewMan(path);
                        break;
                    case "zuck_small.blocks":
                        blocks = GatherBlocksZuckSmall(path);
                        break;
                    case "kd.blocks":
                        blocks = GatherBlocksKD(path);
                        break;
                    case "zuck_medium.blocks":
                        blocks = GatherBlocksZuckMedium(path);
                        break;
                    case "p4hd.blocks":
                        blocks = GatherBlocksP4HD(path);
                        break;
                    case "marvin.blocks":
                        blocks = GatherBlocksMarvin(path);
                        break;
                    case "w23.blocks":
                        blocks = GatherBlocksW23(path);
                        break;
                    case "zuck_large.blocks":
                        blocks = GatherBlocksZuckLarge(path);
                        break;
                    case "mclaughlin_limit.blocks":
                        blocks = GatherBlocksMcLaughlinLimit(path);
                        break;
                    case "mclaughlin.blocks":
                        blocks = GatherBlocksMcLaughlin(path);
                        break;
                }
                return blocks;
            }
            else
            {
                return new List<Block>();
            }
        }
        private static List<Block> GatherBlocksNewMan(string path)
        {
            List<Block> blocks = new List<Block>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');

                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    string type = cubeData[4];
                    double grade = double.Parse(cubeData[5]);
                    double tonns = double.Parse(cubeData[6]);
                    double min_caf = double.Parse(cubeData[7]);
                    double value_extracc = double.Parse(cubeData[8]);
                    double value_proc = double.Parse(cubeData[9]);
                    double apriori_process = double.Parse(cubeData[10]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        type = type,
                        grade = grade,
                        tonn = tonns,
                        min_caf = min_caf,
                        value_extracc = value_extracc,
                        value_proc = value_proc,
                        apriori_process = apriori_process
                    };
                    blocks.Add(block);
                }
            }
            Console.WriteLine("Done gathering blocks.");
            return blocks;
        }
        private static List<Block> GatherBlocksZuckSmall(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksKD(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksZuckMedium(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksP4HD(string path)
        {
            throw new NotImplementedException();
        }
        static public List<Block> GatherBlocksMarvin(string path)
        {
            List<Block> blocks = new List<Block>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');

                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double tonn = double.Parse(cubeData[4]);
                    double au = double.Parse(cubeData[5]);
                    double cu = double.Parse(cubeData[6]);
                    double porc_profit = double.Parse(cubeData[7]);

                    Block block = new Block(id, x, y, z, tonn, au, cu, porc_profit);
                    blocks.Add(block);

                    blocks.Add(block);

                }
            }
            Console.WriteLine("Done gathering blocks.");
            return blocks;
        }
        private static List<Block> GatherBlocksW23(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksZuckLarge(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksMcLaughlinLimit(string path)
        {
            throw new NotImplementedException();
        }
        private static List<Block> GatherBlocksMcLaughlin(string path)
        {
            throw new NotImplementedException();
        }

    }
}
