using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SDPreSubmissionNS
{
    public class Program:ISubscriber
    {
        static private string path;
        static private string option;
        static private List<Block> blocks;
        static private string newFileName;
        static private List<BlockModel> blockModels;
        private static Program mainProgram;

        static void Main(string[] args)
        {
            mainProgram = new Program();
            blockModels = new List<BlockModel>();
            CreateModelsFolder();
            mainProgram.LoadAllModels();
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
                        Console.WriteLine(blockModel.Name);
                    }
                }
                Console.WriteLine("Welcome, please select an option: ");
                Console.WriteLine("1. Save Block Model.");
                Console.WriteLine("2. Load a Saved Block Model.");
                Console.WriteLine("3. UnLoad a Block Model.");
                Console.WriteLine("4. Delete a Saved Block Model");
                Console.WriteLine("5. Reports");
                Console.WriteLine("6. Reblock Model.");
                Console.WriteLine("7. Help");
                Console.WriteLine("8. Exit");
                option = Console.ReadLine();
                if (option.Equals("1"))
                {
                    mainProgram.SaveModel();
                }
                else if (option.Equals("2"))
                {
                    mainProgram.LoadModel();
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
                    if (blockModels.Count == 0)
                    {
                        Console.WriteLine("Save a Block Model, or Load a previusy saved one before \n" +
                            "looking at the possible reports!");
                    }
                    else
                    {
                        ReportsMenu();
                    }
                }
                else if (option.Equals("6"))
                {
                    Reblock();
                }
                else if (option.Equals("7"))
                {
                    Console.WriteLine("First save a model from a .Blocks file, then load it though the program to be able to access it!");
                }
                else if (option.Equals("8"))
                {
                    menu = false;
                }
            }
            Console.WriteLine("Bye bye");
            Console.ReadLine();
        }

        private static void Reblock()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels) {
                Console.WriteLine(blockModel.Name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.Name == choosenBlockModel);
            if (bModel != null) {
                int x = 0;
                int y = 0;
                int z = 0;

                Console.WriteLine("Insert X coordinate");
                string strx = Console.ReadLine();
                while (!int.TryParse(strx, out x)) {
                    Console.WriteLine("invalid X coordinate, enter it again please");
                    strx = Console.ReadLine();
                }

                Console.WriteLine("Insert Y coordinate");
                string stry = Console.ReadLine();
                while (!int.TryParse(stry, out y)) {
                    Console.WriteLine("invalid Y coordinate, enter it again please");
                    stry = Console.ReadLine();
                }

                Console.WriteLine("Insert Z coordinate");
                string strz = Console.ReadLine();
                while (!int.TryParse(strz, out z)) {
                    Console.WriteLine("invalid Z coordinate, enter it again please");
                    strz = Console.ReadLine();
                }

                bModel.Reblock(x,y,z);
            }
            else {
                Console.WriteLine("No Loaded Block Model has that name");
            }
        }
        private static void ReportsMenu()
        {
            bool menu = true;

            while (menu)
            {
                Console.WriteLine("");
                Console.WriteLine("Choose one of the following reports:");
                Console.WriteLine("1. Number Of Blocks in Block Model");
                Console.WriteLine("2. Mass in Kilograms of block");
                Console.WriteLine("3. Grade in Percentage for each Mineral");
                Console.WriteLine("4. Display all other attributes of one block");
                Console.WriteLine("5. Go back to main menu");
                option = Console.ReadLine();
                if (option.Equals("1"))
                {
                    ReportGetNumberBlocks();
                }
                else if (option.Equals("2"))
                {
                    ReportMassInKilograms();
                }
                else if (option.Equals("3"))
                {
                    ReportGradePercentageOfMinerals();
                }
                else if (option.Equals("4"))
                {
                    ReportDisplayAttriburesOfOneBlock();
                }
                else if (option.Equals("5"))
                {
                    menu = false;
                }

            }
        }

        private static void ReportGetNumberBlocks()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels)
            {
                Console.WriteLine(blockModel.Name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.Name == choosenBlockModel);
            if (bModel != null)
            {
                Console.WriteLine(bModel.GetNumberOfBlocks().ToString());
            }
            else
            {
                Console.WriteLine("No Loaded Block Model has that name");
            }
        }

        private static void ReportDisplayAttriburesOfOneBlock()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels)
            {
                Console.WriteLine(blockModel.Name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.Name == choosenBlockModel);
            if (bModel != null)
            {
                int x = 0;
                int y = 0;
                int z = 0;

                Console.WriteLine("Insert X coordinate");
                string strx = Console.ReadLine();
                while (!int.TryParse(strx, out x))
                {
                    Console.WriteLine("invalid X coordinate, enter it again please");
                    strx = Console.ReadLine();
                }

                Console.WriteLine("Insert Y coordinate");
                string stry = Console.ReadLine();
                while (!int.TryParse(stry, out y))
                {
                    Console.WriteLine("invalid Y coordinate, enter it again please");
                    stry = Console.ReadLine();
                }

                Console.WriteLine("Insert Z coordinate");
                string strz = Console.ReadLine();
                while (!int.TryParse(strz, out z))
                {
                    Console.WriteLine("invalid Z coordinate, enter it again please");
                    strz = Console.ReadLine();
                }

                Console.WriteLine("Insert Attribute Name");
                string attribute = Console.ReadLine();
                Block block = bModel.GetBlock(x, y, z);

                List<string> possibleAttributes = new List<string>();
                possibleAttributes = bModel.GetPossibleAttributes();
                bool attrAprooved = false;
                foreach (string str in possibleAttributes)
                {
                    if (attribute.ToLower() == str.ToLower())
                    {
                        attrAprooved = true;
                    }
                }
                if (attrAprooved)
                {
                    Console.WriteLine("variables at 0 will not be printed");
                    string strToPrint = "";
                    strToPrint += "id:" + block.Id;
                    strToPrint += " x:" + block.X;
                    strToPrint += " y:" + block.Y;
                    strToPrint += " z:" + block.Z;
                    strToPrint += " weight:" + block.Weight;

                    foreach (KeyValuePair<string, string> entry in block.CategoricalAttributes)
                    {
                        strToPrint += " " + entry.Key + ":" + entry.Value.ToString();
                    }
                    foreach (KeyValuePair<string, double> entry in block.ContinuousAttributes)
                    {
                        strToPrint += " " + entry.Key + ":" + entry.Value.ToString();
                    }
                    foreach (KeyValuePair<string, double> entry in block.MassProportionalAttributes)
                    {
                        strToPrint += " " + entry.Key + ":" + entry.Value.ToString();
                    }

                    Console.WriteLine(strToPrint);
                }
                else
                {
                    Console.WriteLine("Variable no encontrada");
                }

            }
        }
        private static void ReportGradePercentageOfMinerals()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels)
            {
                Console.WriteLine(blockModel.Name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.Name == choosenBlockModel);
            if (bModel != null)
            {
                int x = 0;
                int y = 0;
                int z = 0;

                Console.WriteLine("Insert X coordinate");
                string strx = Console.ReadLine();
                while (!int.TryParse(strx, out x))
                {
                    Console.WriteLine("invalid X coordinate, enter it again please");
                    strx = Console.ReadLine();
                }

                Console.WriteLine("Insert Y coordinate");
                string stry = Console.ReadLine();
                while (!int.TryParse(stry, out y))
                {
                    Console.WriteLine("invalid Y coordinate, enter it again please");
                    stry = Console.ReadLine();
                }

                Console.WriteLine("Insert Z coordinate");
                string strz = Console.ReadLine();
                while (!int.TryParse(strz, out z))
                {
                    Console.WriteLine("invalid Z coordinate, enter it again please");
                    strz = Console.ReadLine();
                }

                Console.WriteLine("Insert Mineral Name");
                string attribute = Console.ReadLine();
                Block block = bModel.GetBlock(x, y, z);

                if (block.MassProportionalAttributes.ContainsKey(attribute))
                {
                    string strToPrint = "";
                    foreach (var attr in block.MassProportionalAttributes)
                    {
                        strToPrint += attr.Key + ":" + attr.Value + " ";
                    }
                    Console.WriteLine(strToPrint);
                }
                else
                {
                    Console.WriteLine("Mineral not found");
                }

            }
            else
            {
                Console.WriteLine("No Loaded Block Model has that name");
            }
        }
        private static void ReportMassInKilograms()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels)
            {
                Console.WriteLine(blockModel.Name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.Name == choosenBlockModel);
            if (bModel != null)
            {
                int x = 0;
                int y = 0;
                int z = 0;

                Console.WriteLine("Insert X coordinate");
                string strx = Console.ReadLine();
                while (!int.TryParse(strx, out x))
                {
                    Console.WriteLine("invalid X coordinate, enter it again please");
                    strx = Console.ReadLine();
                }

                Console.WriteLine("Insert Y coordinate");
                string stry = Console.ReadLine();
                while (!int.TryParse(stry, out y))
                {
                    Console.WriteLine("invalid Y coordinate, enter it again please");
                    stry = Console.ReadLine();
                }

                Console.WriteLine("Insert Z coordinate");
                string strz = Console.ReadLine();
                while (!int.TryParse(strz, out z))
                {
                    Console.WriteLine("invalid Z coordinate, enter it again please");
                    strz = Console.ReadLine();
                }

                Block block = bModel.GetBlock(x, y, z);
                if (block != null)
                {
                    Console.WriteLine(block.GetMassInKg().ToString());
                }
                else
                {
                    Console.WriteLine("There is no block in those coordinates");
                }
            }
            else
            {
                Console.WriteLine("No Loaded Block Model has that name");
            }
        }
        private void LoadAllModels()
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
                                blockModel.Subscribe(this);
                                blockModels.Add(blockModel);
                            }
                        }
                    }
                }
            }
        }
        private void SaveModel()
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
                    Console.WriteLine("Please enter the attribute names of a block in order." +
                                      "\nIf an attribute is the weight of the block, write it followed by ':weight'." +
                                      "\nIf an attribute is continuous, write it followed by ':cont'." +
                                      "\nIf an attribute is mass proportional, write it followed by ':prop'." +
                                      "\nIf an attribute is categorical, write it followed by ':cat'." +
                                      "\nExample: id x y z tonn:cont cu:prop au:prop other:cat");
                    string attributesString = Console.ReadLine();
                    List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
                    List<string> continuous_att = new List<string>();
                    List<string> mass_proportional_att = new List<string>();
                    List<string> categorical_att = new List<string>();
                    foreach (string attribute in attributesSplit)
                    { 
                        string[]att = attribute.Split(":");
                        if (att.Length <= 1) continue;
                        if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                        else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                        else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
                    }

                    BlockModel blockModel = new BlockModel(file.Name, continuous_att, mass_proportional_att, categorical_att);
                    List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, blockModel);
                    blockModel.SetBlocks(blocks);
                    BlockSerializer.SerializeBlockModel("Models\\" + file.Name + ".grupo3", blockModel);

                    blockModel.Subscribe(this);

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
        private void LoadModel()
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
            blockModel.Subscribe(this);
            if (blockModel != null)
            {
                if (blockModels.Find(i => i.Name == newPath) != null)
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
                Console.WriteLine(blockModel1.Name);
            }
            string newPath = Console.ReadLine();
            BlockModel blockModel = blockModels.Find(i => i.Name == newPath);
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
                Block block = blocks.Find(i => i.Id == intid);
                if (block != null)
                {
                    Console.Write($"ID: {block.Id}, x:{block.X}, y:{block.Y}, z:{block.Z} \n");
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

        public void Update(string data)
        {
            Console.WriteLine(data);
        }
    }
}
