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
                if (blocks == null)
                {
                    Console.WriteLine("database not loaded");
                }
                else
                {
                    Console.WriteLine($"database loaded {path}");
                }
                Console.WriteLine("Welcome, please select an option: ");
                Console.WriteLine("1. Save Block Model.");
                Console.WriteLine("2. Load Block Model.");
                Console.WriteLine("3. Load information from a Specific Block id");
                Console.WriteLine("4. Help");
                Console.WriteLine("5. Exit");
                option = Console.ReadLine();
                if (option.Equals("1"))
                {
                    Console.Write("Please enter the path of your Block Model file:");
                    path = Console.ReadLine();
                    Console.Write("Please enter the name of the file you want to save: ");
                    newPath = Console.ReadLine();
                    blocks = BlockLoaders.GatherBlocks(path);
                    SerializeBlocks();
                }
                else if (option.Equals("2"))
                {
                    Console.Write("Please enter the name of the file you want to load:");
                    newPath = Console.ReadLine();
                    if (DeserializeBlocks())
                    {
                        PrintBlocks();
                    }
                }
                else if (option.Equals("3"))
                {
                    if (blocks != null)
                    {
                        Console.WriteLine("Enter Block id:");
                        option = Console.ReadLine();
                        PrintBlockById(option, blocks);
                    }
                    else
                    {
                        Console.WriteLine("Please load a database first");
                    }
                    
                }
                else if (option.Equals("4"))
                {
                    Console.WriteLine("First save a model from a .Blocks file, then load it though the program to be able to access it!");
                }
                else if (option.Equals("5"))
                {
                    menu = false;
                }
            }
            Console.WriteLine("Bye bye");
            Console.ReadLine();
        }

        static public void PrintBlockById(string stringId, List<Block> blocks)
        {
            int intid = -1;
            if (int.TryParse(stringId, out intid))
            {
                Block block = blocks.Find(i => i.id == intid);
                if (block != null)
                {
                    Console.Write($"ID: {block.id}, x:{block.x}, y:{block.y}, z:{block.z}, tonn:{block.tonn}, " +
                    $"au:{block.au}, cu:{block.cu}, proc_profit:{block.porc_profit} \n");
                }
                else
                {
                    Console.WriteLine("Block id not found");
                }
            }
            else
            {
                Console.WriteLine("No int detected, going back to menu");
            }
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

    }
}
