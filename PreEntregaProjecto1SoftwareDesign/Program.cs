using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SDPreSubmissionNS
{
    public class Program
    {
        static private string path;
        static private string newPath;
        static private string option;
        static private List<Block> blocks;

        static void Main(string[] args)
        {
            bool menu = true;

            while (menu)
            {
                StartMenu();
                if (option.Equals("1"))
                {
                    blocks = GatherBlocks(path);
                    SerializeBlocks();
                }
                else if (option.Equals("2"))
                {
                    if (DeserializeBlocks())
                    {
                        PrintBlocks();
                    }
                }
                else if (option.Equals("3"))
                {
                    menu = false;
                }
            }
            Console.WriteLine("Bye bye");
            Console.ReadLine();
        }

        static public List<Block> GatherBlocks(string path)
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

        static public void SerializeBlocks()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@newPath,FileMode.Create,FileAccess.Write);
            formatter.Serialize(stream, blocks);
            stream.Close();
            Console.WriteLine("Done saving Block Model.");
        }

        //retorna True su funciona y False si falla
        static public bool DeserializeBlocks()
        {
            IFormatter formatter = new BinaryFormatter();
            if (File.Exists(@newPath))
            {
                Stream stream = new FileStream(@newPath, FileMode.Open, FileAccess.Read);
                blocks = (List<Block>)formatter.Deserialize(stream);
                stream.Close();
                return true;
            }
            else
            {
                Console.WriteLine($"File not found in {@newPath}");
                return false;
            }
            
        }

        static public void PrintBlocks()
        {
            Console.WriteLine("Blocks information:");
            foreach (Block block in blocks)
            {
                Console.Write($"ID: {block.id}, x:{block.x}, y:{block.y}, z:{block.z}, tonn:{block.tonn}, " +
                    $"au:{block.au}, cu:{block.cu}, proc_profit:{block.porc_profit} \n");
            }
        }

        static public void StartMenu()
        {
            Console.WriteLine("Welcome, please select an option: ");
            Console.WriteLine("1. Save Block Model.");
            Console.WriteLine("2. Load Block Model.");
            Console.WriteLine("3. Exit");
            option = Console.ReadLine();
            if (option.Equals("1"))
            {
                Console.Write("Please enter the path of your Block Model file:");
                path = Console.ReadLine();
                Console.Write("Please enter the name of the file you want to save: ");
                newPath = Console.ReadLine();
            }
            else if (option.Equals("2"))
            {
                Console.Write("Please enter the name of the file you want to load:");
                newPath = Console.ReadLine();
            }
        }
    }
}
