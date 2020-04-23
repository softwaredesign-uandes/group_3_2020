using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace SDPreSubmissionNS
{
    public class Program
    {
        static private string path;
        static private string option;
        static private List<BlockModel> blockModels;

        static void Main(string[] args)
        {
            blockModels = new List<BlockModel>();
            CreateModelsFolder();
            LoadAllModels();
            MainMenu();
        }
        static void MainMenu()
        {
            bool menu = true;
            
            while (menu)
            {
                if (blockModels.Count == 0)
                {
                    Console.WriteLine("No Database Loaded");
                }
                else
                {
                    Console.WriteLine($"Block Models loaded:");
                    foreach (BlockModel blockModel in blockModels)
                    {
                        Console.WriteLine(blockModel.name);
                    }
                }
                Console.WriteLine("Welcome, please select an option: ");
                Console.WriteLine("1. Save Block Model.");
                Console.WriteLine("2. Load a Saved Block Model.");
                Console.WriteLine("3. UnLoad a Block Model.");
                Console.WriteLine("4. Delete a Saved Block Model");
                Console.WriteLine("5. Help");
                Console.WriteLine("6. Exit");
                option = Console.ReadLine();
                if (option.Equals("1"))
                {
                    SaveModel(); 
                }
                else if (option.Equals("2"))
                {
                    LoadModel();
                }
                else if (option.Equals("3"))
                {
                    UnLoadModel();
                }
                else if (option.Equals("4"))
                {
                    DeleteBlockModel();
                }
                else if (option.Equals("5"))
                {
                    Console.WriteLine("First save a model from a .Blocks file, then load it though the program to be able to access it!");
                }
                else if (option.Equals("6"))
                {
                    menu = false;
                }
            }
            Console.WriteLine("Bye bye");
            Console.ReadLine();
        }

        private static void LoadAllModels()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Models\\");
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.grupo3").ToList();
            if (fileInfos.Count > 0)
            {
                Console.WriteLine("Do you want to load all saved Block Models? Y or N ");
                string yesOrNo = "";
                while (!yesOrNo.Equals("yes") && !yesOrNo.Equals("no") && !yesOrNo.Equals("y") && !yesOrNo.Equals("n"))
                {
                    yesOrNo = Console.ReadLine().ToLower();
                    if (yesOrNo.Equals("y") || yesOrNo.Equals("yes"))
                    {
                        foreach (FileInfo fileInfo in fileInfos)
                        {
                            BlockModel blockModel = BlockSerializer.DeserializeBlockModel(fileInfo.FullName);
                            if (blockModel != null)
                            {
                                blockModels.Add(blockModel);
                            }
                        }
                    }
                }
            }
        }
        private static void SaveModel()
        {
            Console.WriteLine("Please enter the path of your Block Model file:");
            path = Console.ReadLine();
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                if (File.Exists("Models\\" + file.Name + ".grupo3"))
                {
                    Console.WriteLine("Block Model already saved");
                }
                else
                {
                    List<Block> blocks = BlockLoaders.GatherBlocks(path);
                    BlockModel blockModel = new BlockModel(blocks, file.Name);
                    BlockSerializer.SerializeBlockModel("Models\\" + file.Name + ".grupo3", blockModel);

                    string yesOrNo = "";
                    while (!yesOrNo.Equals("yes") && !yesOrNo.Equals("no") && !yesOrNo.Equals("y") && !yesOrNo.Equals("n"))
                    {
                        Console.WriteLine("Do you want to load the saved Block Model now? Y or N");
                        yesOrNo = Console.ReadLine().ToLower();
                        if (yesOrNo.Equals("y") || yesOrNo.Equals("yes"))
                        {
                            blockModels.Add(blockModel);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found");
            }
        }
        private static void LoadModel()
        {
            Console.WriteLine("Please enter the name of the file you want to load:");
            DirectoryInfo directoryInfo = new DirectoryInfo("Models\\");
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.grupo3").ToList();
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".grupo3")
                {
                    string name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    Console.WriteLine(name);
                }
            }
            string newPath = Console.ReadLine();
            BlockModel blockModel = BlockSerializer.DeserializeBlockModel("Models\\" + newPath + ".grupo3");
            if (blockModel != null)
            {
                if (blockModels.Find(i => i.name == newPath) != null)
                {
                    Console.WriteLine("Block model already Loaded");
                }
                else
                {
                    blockModels.Add(blockModel);
                }
                
            }
            else
            {
                Console.WriteLine("Error deserializing Block Model");
            }
        }
        private static void UnLoadModel()
        {
            Console.WriteLine("Select Block data to Unload: ");
            foreach (BlockModel blockModel1 in blockModels)
            {
                Console.WriteLine(blockModel1.name);
            }
            string newPath = Console.ReadLine();
            BlockModel blockModel = blockModels.Find(i => i.name == newPath);
            if (blockModel != null)
            {
                blockModels.Remove(blockModel);
            }
            else
            {
            }

        }
        private static void DeleteBlockModel()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Models\\");
            List<FileInfo> fileInfos = directoryInfo.GetFiles().ToList();
            Console.WriteLine("Select the Block Model you want to erase:");
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".grupo3")
                {
                    string name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    Console.WriteLine(name);
                }
            }
            string newPath = Console.ReadLine();
            if (File.Exists("Models\\" + newPath + ".grupo3"))
            {
                File.Delete("Models\\" + newPath + ".grupo3");
            }
            else
            {
                Console.WriteLine("File not found, process canceled");
            }
        }

        static void CreateModelsFolder()
        {
            if (!Directory.Exists("Models"))
            {
                Directory.CreateDirectory("Models");
            }
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

    }
}
