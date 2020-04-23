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
                        blocks = GatherBlocksZuck(path);
                        break;
                    case "kd.blocks":
                        blocks = GatherBlocksKD(path);
                        break;
                    case "zuck_medium.blocks":
                        blocks = GatherBlocksZuck(path);
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
                        blocks = GatherBlocksZuck(path);
                        break;
                    case "mclaughlin_limit.blocks":
                        blocks = GatherBlocksMcLaughlin(path);
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

                    //id x y z type grade tonns min_caf value_extracc value_proc apriori_process
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
        private static List<Block> GatherBlocksZuck(string path)
        {
            List<Block> blocks = new List<Block>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {

                    string[] cubeData = line.Trim(' ').Split(' ');

                    //id x y z cost value rock_tonnes ore_tonnes
                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double cost = double.Parse(cubeData[4]);
                    double value = double.Parse(cubeData[5]);
                    double rock_tonnes = double.Parse(cubeData[6]);
                    double ore_tonnes = double.Parse(cubeData[7]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        cost = cost,
                        value = value,
                        rock_tonnes = rock_tonnes,
                        ore_tonnes = ore_tonnes,
                        tonn = rock_tonnes + ore_tonnes,

                    };
                    blocks.Add(block);
                }
            }
            return blocks;
        }
        private static List<Block> GatherBlocksKD(string path)
        {
            List<Block> blocks = new List<Block>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');

                    //<id> <x> <y> <z> <tonn> <blockvalue> <destination> <CU %> <process_profit>
                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double ton = double.Parse(cubeData[5]);
                    double blockvalue = double.Parse(cubeData[4]);
                    double destination = double.Parse(cubeData[6]);
                    double cu = double.Parse(cubeData[7]);
                    double processProfit = double.Parse(cubeData[8]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        blockvalue = blockvalue,
                        tonn = ton,
                        destination = destination,
                        porc_profit = processProfit,
                    };
                    blocks.Add(block);
                }
            }
            return blocks;
        }
        private static List<Block> GatherBlocksP4HD(string path)
        {
            List<Block> blocks = new List<Block>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');

                    //<id> <x> <y> <z> <tonn> <blockvalue> <destination> <Au (oz/ton)> <Ag (oz/ton)> <Cu %>
                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double tonn = double.Parse(cubeData[4]);
                    double blockvalue = double.Parse(cubeData[5]);
                    double destination = double.Parse(cubeData[6]);
                    double Au = double.Parse(cubeData[7]);
                    double Ag = double.Parse(cubeData[8]);
                    double Cu = double.Parse(cubeData[8]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        tonn = tonn,
                        blockvalue = blockvalue,
                        destination = destination,
                        au = Au,
                        ag = Ag,
                        cu = Cu,
                    };
                    blocks.Add(block);
                }
            }
            return blocks;
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

                    //Block block = new Block(id, x, y, z, tonn, au, cu, porc_profit);
                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        tonn = tonn,
                        au = au,
                        cu = cu,
                        porc_profit = porc_profit,
                    };
                    blocks.Add(block);

                    blocks.Add(block);

                }
            }
            Console.WriteLine("Done gathering blocks.");
            return blocks;
        }
        private static List<Block> GatherBlocksW23(string path)
        {
            List<Block> blocks = new List<Block>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');
                    //id x y z dest phase AuRec AuFA tons co3 orgc sulf Mcost Pcost Tcost Tvalue Bvalue rc_Stockpile rc_RockChar

                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double dest = double.Parse(cubeData[4]);
                    double phase = double.Parse(cubeData[5]);
                    double AuRec = double.Parse(cubeData[6]);
                    double AuFA = double.Parse(cubeData[7]);
                    double tons = double.Parse(cubeData[8]);
                    double co3 = double.Parse(cubeData[9]);
                    double orgc = double.Parse(cubeData[10]);
                    double sulf = double.Parse(cubeData[11]);
                    double Mcost = double.Parse(cubeData[12]);
                    double Pcost = double.Parse(cubeData[13]);
                    double Tcost = double.Parse(cubeData[14]);
                    double Tvalue = double.Parse(cubeData[15]);
                    double Bvalue = double.Parse(cubeData[16]);
                    double rc_Stockpile = double.Parse(cubeData[17]);
                    double rc_RockChar = double.Parse(cubeData[18]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        destination = dest,
                        phase = phase,
                        AuRec = AuRec,
                        AuFA = AuFA,
                        tonn = tons,
                        co3 = co3,
                        orgc = orgc,
                        sulf = sulf,
                        Mcost = Mcost,
                        Pcost = Pcost,
                        Tcost = Tcost,
                        Tvalue = Tvalue,
                        Bvalue = Bvalue,
                        rc_Stockpile = rc_Stockpile,
                        rc_RockChar = rc_RockChar,
                    };

                    blocks.Add(block);
                }
            }
            return blocks;
        }
        private static List<Block> GatherBlocksMcLaughlin(string path)
        {
            List<Block> blocks = new List<Block>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] cubeData = line.Trim(' ').Split(' ');
                    //id x y z blockvalue ton destination Au(oz/ton)
                    int id = int.Parse(cubeData[0]);
                    int x = int.Parse(cubeData[1]);
                    int y = int.Parse(cubeData[2]);
                    int z = int.Parse(cubeData[3]);
                    double blockvalue = double.Parse(cubeData[4]);
                    double ton = double.Parse(cubeData[5]);
                    double destination = double.Parse(cubeData[6]);
                    double Au = double.Parse(cubeData[7]);

                    Block block = new Block
                    {
                        id = id,
                        x = x,
                        y = y,
                        z = z,
                        blockvalue = blockvalue,
                        tonn = ton,
                        destination = destination,
                        au = Au,
                    };

                }
            }
            return blocks;
        }

    }
}
