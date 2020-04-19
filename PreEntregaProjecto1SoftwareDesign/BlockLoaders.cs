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
            throw new NotImplementedException();
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
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] buffer = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    string[] lines = temp.GetString(buffer).Split('\n');
                    if (lines.Length != 48) //test esto le indica al programa que ya no encontró más lineas en el archivo
                    {
                        foreach (string line in lines)
                        {
                            //esto es para devolver el lector en caso que el buffer se quede sin espacio a la mitad de una linea
                            if (line.Trim(' ').Split(' ').Length != 8 || line.Trim(' ').EndsWith('-'))
                            {
                                fileStream.Position = fileStream.Position - line.Length;
                            }
                            //este else agrega los bloques del archivo a una lista
                            else
                            {
                                string[] cubeData = line.Trim(' ').Split(' ');
                                #region preparando variables

                                int id = int.Parse(cubeData[0]);
                                int x = int.Parse(cubeData[1]);
                                int y = int.Parse(cubeData[2]);
                                int z = int.Parse(cubeData[3]);
                                double tonn = double.Parse(cubeData[4]);
                                double au = double.Parse(cubeData[5]);
                                double cu = double.Parse(cubeData[6]);
                                double porc_profit = double.Parse(cubeData[7]);

                                #endregion
                                Block block = new Block(id, x, y, z, tonn, au, cu, porc_profit);
                                blocks.Add(block);
                            }

                        }
                    }
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
