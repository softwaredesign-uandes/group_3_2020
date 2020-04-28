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
                Console.WriteLine("5. Reports");
                Console.WriteLine("6. Help");
                Console.WriteLine("7. Exit");
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
                    Console.WriteLine("First save a model from a .Blocks file, then load it though the program to be able to access it!");
                }
                else if (option.Equals("7"))
                {
                    menu = false;
                }
            }
            Console.WriteLine("Bye bye");
            Console.ReadLine();
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
                Console.WriteLine(blockModel.name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.name == choosenBlockModel);
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
                Console.WriteLine(blockModel.name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.name == choosenBlockModel);
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
                possibleAttributes = bModel.GetPossibleAtrributes();
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
                    strToPrint += "id:" + block.id;
                    strToPrint += "x:" + block.x;
                    strToPrint += " y:" + block.y;
                    strToPrint += " z:" + block.z;
                    if (block.ag != 0) { strToPrint += " Ag:" + block.ag.ToString(); }
                    if (block.apriori_process != 0) { strToPrint += " apriori_process:" + block.apriori_process.ToString(); }
                    if (block.au != 0) { strToPrint += " Au:" + block.au.ToString(); }
                    if (block.AuFA != 0) { strToPrint += " AuFA:" + block.AuFA.ToString(); }
                    if (block.AuRec != 0) { strToPrint += " AuRec:" + block.AuRec.ToString(); }
                    if (block.blockvalue != 0) { strToPrint += " blockvalue:" + block.blockvalue.ToString(); }
                    if (block.Bvalue != 0) { strToPrint += " Bvalue:" + block.Bvalue.ToString(); }
                    if (block.co3 != 0) { strToPrint += " co3:" + block.co3.ToString(); }
                    if (block.cost != 0) { strToPrint += " cost:" + block.cost.ToString(); }
                    if (block.cu != 0) { strToPrint += " cu:" + block.cu.ToString(); }
                    if (block.destination != 0) { strToPrint += " destination:" + block.destination.ToString(); }
                    if (block.grade != 0) { strToPrint += " grade:" + block.grade.ToString(); }
                    if (block.Mcost != 0) { strToPrint += " Mcost:" + block.Mcost.ToString(); }
                    if (block.min_caf != 0) { strToPrint += " min_caf:" + block.min_caf.ToString(); }
                    if (block.ore_tonnes != 0) { strToPrint += " ore_tonnes:" + block.ore_tonnes.ToString(); }
                    if (block.orgc != 0) { strToPrint += " orgc:" + block.orgc.ToString(); }
                    if (block.Pcost != 0) { strToPrint += " Pcost:" + block.Pcost.ToString(); }
                    if (block.phase != 0) { strToPrint += " phase:" + block.phase.ToString(); }
                    if (block.porc_profit != 0) { strToPrint += " porc_profit:" + block.porc_profit.ToString(); }
                    if (block.rc_RockChar != "") { strToPrint += " rc_RockChar:" + block.rc_RockChar.ToString(); }
                    if (block.rc_Stockpile != 0) { strToPrint += " rc_Stockpile:" + block.rc_Stockpile.ToString(); }
                    if (block.rock_tonnes != 0) { strToPrint += " rock_tonnes:" + block.rock_tonnes.ToString(); }
                    if (block.sulf != 0) { strToPrint += " sulf:" + block.sulf.ToString(); }
                    if (block.Tcost != 0) { strToPrint += " Tcost:" + block.Tcost.ToString(); }
                    if (block.tonn != 0) { strToPrint += " tonn:" + block.tonn.ToString(); }
                    if (block.Tvalue != 0) { strToPrint += " Tvalue:" + block.Tvalue.ToString(); }
                    if (block.type == "") { strToPrint += " type:" + block.type.ToString(); }
                    if (block.value != 0) { strToPrint += " value:" + block.value.ToString(); }
                    if (block.value_extracc != 0) { strToPrint += " value_extracc:" + block.value_extracc.ToString(); }
                    if (block.value_proc != 0) { strToPrint += " value_proc:" + block.value_proc.ToString(); }

                    Console.WriteLine(strToPrint);
                }
                else
                {
                    Console.WriteLine("Variable no encontrada");
                }

            }
        }

        //TODO: Falta implementar
        private static void ReportGradePercentageOfMinerals()
        {
            throw new NotImplementedException();
        }

        private static void ReportMassInKilograms()
        {
            Console.WriteLine("Choose one of the loaded Block Models");
            Console.WriteLine($"Block Models loaded:");
            foreach (BlockModel blockModel in blockModels)
            {
                Console.WriteLine(blockModel.name);
            }
            string choosenBlockModel = Console.ReadLine();
            BlockModel bModel = blockModels.Find(i => i.name == choosenBlockModel);
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
                    Console.WriteLine("Please enter the attributes of a block:");
                    string attributesString = Console.ReadLine();
                    string[] attributesSplit = attributesString.Trim(' ').Split(' ');
                    attributesSplit = attributesSplit.Skip(4);

                    BlockModel blockModel = new BlockModel(blocks, file.Name, attributesSplit);
                    List<Block> blocks = BlockLoaders.GatherBlocks(path, blockModel);
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
