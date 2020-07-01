using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PreEntregaProjecto1SoftwareDesign;
using SDPreSubmissionNS;
using WAPI.Controllers;
using static WAPI.Controllers.BlockModelController;

namespace WAPI.Models
{
    public class BlockModelContext : DbContext
    {
        static public void SaveNewModel(FileUploadAPI objFile, string attributesString)
        {
            if (objFile.file.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\TempModelFiles\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\TempModelFiles\\");
                }
                
                string filePath = _environment.WebRootPath + "\\TempModelFiles\\" + objFile.file.FileName;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    objFile.file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                FileInfo file = new FileInfo(filePath);

                List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
                List<string> continuous_att = new List<string>();
                List<string> mass_proportional_att = new List<string>();
                List<string> categorical_att = new List<string>();
                foreach (string attribute in attributesSplit)
                {
                    string[] att = attribute.Split(":");
                    if (att.Length <= 1) continue;
                    if (att[1].Equals("cont")) continuous_att.Add(att[0]);
                    else if (att[1].Equals("prop")) mass_proportional_att.Add(att[0]);
                    else if (att[1].Equals("cat")) categorical_att.Add(att[0]);
                }

                BlockModel blockModel = new BlockModel(file.Name, continuous_att, mass_proportional_att, categorical_att);

                new Trace("block_model_loaded", blockModel.Name, true);

                List<Block> blocks = BlockLoaders.GatherBlocks(filePath, attributesSplit, blockModel);
                blockModel.SetBlocks(blocks);
                string serializedFilePath = _environment.WebRootPath + "\\Models\\" + objFile.file.FileName + ".grupo3";
                BlockSerializer.SerializeBlockModel(serializedFilePath, blockModel);
            }
        }

        static public void SaveNewPrecFile(FileUploadAPI objFile)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\PrecFiles\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\PrecFiles\\");
            }

            string filePath = _environment.WebRootPath + "\\PrecFiles\\" + objFile.file.FileName;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                objFile.file.CopyTo(fileStream);
                fileStream.Flush();
            }
            

        }

        static public List<string> testGetFileNames(IWebHostEnvironment environment)
        {
            string path = environment.WebRootPath + "\\Upload\\"; 
            DirectoryInfo di = new DirectoryInfo(path);
            List<string> files = new List<string>();
            foreach (FileInfo file in di.GetFiles())
            {
                files.Add(file.Name);
            }
            return files;
        }

        static public List<string> LoadAllBlockModelNames(IWebHostEnvironment environment)
        {
            string path = environment.WebRootPath + "\\Models\\";
            List<string> blockModelsNames = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.grupo3").ToList();
            if (fileInfos.Count > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    blockModelsNames.Add(fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length));
                }
            }
            return blockModelsNames;
        }

        public static List<string> RecivePrecFiles()
        {
            string path = _environment.WebRootPath + "\\PrecFiles\\";
            List<string> precFilesNames = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.prec").ToList();
            if (fileInfos.Count > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    precFilesNames.Add(Path.GetFileNameWithoutExtension(fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length)));
                }
            }
            return precFilesNames;
        }

        public static BlockModel GetBlockModel(string name)
        {
            string path = _environment.WebRootPath + "\\Models\\";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> fileInfos = directoryInfo.GetFiles(name + "*.grupo3").ToList();
            if (fileInfos.Count > 0)
            {
                FileInfo fileInfo = fileInfos[0];
                BlockModel blockModel = BlockSerializer.DeserializeBlockModel(fileInfo.FullName);
                return blockModel;
            }
            else
            {
                return new BlockModel("noBlockModel");
            }

        }

        static public List<BlockModel> LoadAllModels(IWebHostEnvironment environment)
        {
            string path = environment.WebRootPath + "\\Models\\";
            List<BlockModel> blockModels = new List<BlockModel>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.grupo3").ToList();
            if (fileInfos.Count > 0)
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
            return blockModels;
        }

        public static Dictionary<int, List<int>> GeneratePrecDict(string fileName)
        {

            string precFilesPath = _environment.WebRootPath + "\\PrecFiles\\";
            string precName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fileName));

            DirectoryInfo directoryInfo = new DirectoryInfo(precFilesPath);
            List<FileInfo> fileInfos = directoryInfo.GetFiles(precName + "*").ToList();
            if (fileInfos.Count > 0)
            {
                FileInfo file = fileInfos[0];
                string pathProcFile = file.FullName;
                Dictionary<int, List<int>> dic = PrecManager.GenerateDictionary(pathProcFile);
                return dic;
            }
            else
            {
                return new Dictionary<int, List<int>>();
            }
            
        }

        public static List<int> ExtractCubes(int index, Dictionary<int, List<int>> precDictionary)
        {
            List<int> answer = PrecManager.ExtractBlock(index, precDictionary);
            return answer;
        }

        static public void UpdateModel(BlockModel blockModel)
        {
            string serializedFilePath = _environment.WebRootPath + "\\Models\\" + blockModel.Name + ".grupo3";
            if (File.Exists(serializedFilePath))
            {
                File.Delete(serializedFilePath);
                BlockSerializer.SerializeBlockModel(serializedFilePath, blockModel);
            }
            else
            {
                BlockSerializer.SerializeBlockModel(serializedFilePath, blockModel);
            }
        }

        static public void Reblock(string name, int x, int y, int z)
        {
            string serializedFilePath = _environment.WebRootPath + "\\Models\\" + name + ".grupo3";
            if (File.Exists(serializedFilePath))
            {
                BlockModel blockModel = BlockSerializer.DeserializeBlockModel(serializedFilePath);
                blockModel.Reblock(x, y, z);
                UpdateModel(blockModel);
            }
        }

        internal static bool DeleteFile(IWebHostEnvironment environment, string name)
        {
            string serializedFilePath = _environment.WebRootPath + "\\Models\\" + name + ".grupo3";
            if (File.Exists(serializedFilePath))
            {
                File.Delete(serializedFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }




        public BlockModelContext(DbContextOptions<BlockModelContext> options) : base(options)
        {
        }



        public DbSet<BlockModel> BlockModels { get; set; }

        
    }
}

