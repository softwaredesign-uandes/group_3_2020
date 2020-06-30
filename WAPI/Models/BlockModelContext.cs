using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SDPreSubmissionNS;
using WAPI.Controllers;
using static WAPI.Controllers.BlockModelController;

namespace WAPI.Models
{
    public class BlockModelContext : DbContext
    {
        static public void SaveNewModelTest(FileUploadAPI objFile, string attributesString)
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
                List<Block> blocks = BlockLoaders.GatherBlocks(filePath, attributesSplit, blockModel);
                blockModel.SetBlocks(blocks);
                string serializedFilePath = _environment.WebRootPath + "\\Models\\" + objFile.file.FileName + ".grupo3";
                BlockSerializer.SerializeBlockModel(serializedFilePath, blockModel);
                

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


        /*
         * Original, se debiese poder borrar
        static public List<string> LoadAllBlockModelNames() {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models";
            List<string> blockModelsNames = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.grupo3").ToList();
            if (fileInfos.Count > 0) {
                foreach (FileInfo fileInfo in fileInfos) {
                    blockModelsNames.Add(fileInfo.Name.Remove(fileInfo.Name.Length-fileInfo.Extension.Length));
                }
            }
            return blockModelsNames;
        }
        */

        static public List<BlockModel> LoadAllModels()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models";
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


        static public void SaveNewModel(string path, string attributesString)
        {
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                if (!File.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                        @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + file.Name + ".grupo3"))
                {
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
                    List<Block> blocks = BlockLoaders.GatherBlocks(path, attributesSplit, blockModel);
                    blockModel.SetBlocks(blocks);
                    BlockSerializer.SerializeBlockModel(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                        @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + file.Name + ".grupo3", blockModel);

                }
            }
        }

        static public void UpdateModel(BlockModel blockModel)
        {
            if (File.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                         @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + blockModel.Name + ".grupo3"))
            {
                File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                         @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + blockModel.Name + ".grupo3");
                BlockSerializer.SerializeBlockModel(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                        @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + blockModel.Name + ".grupo3", blockModel);
            }
        }

        static public void Reblock(string name, int x, int y, int z)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" +
                    name + ".grupo3";
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                if (!File.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
                        @"\PreEntregaProjecto1SoftwareDesign\bin\Debug\netcoreapp3.1\Models\" + file.Name + ".grupo3"))
                {
                    BlockModel blockModel = BlockSerializer.DeserializeBlockModel(file.FullName);
                    blockModel.Reblock(x, y, z);
                    UpdateModel(blockModel);
                }
            }
        }


        public BlockModelContext(DbContextOptions<BlockModelContext> options) : base(options)
        {
        }



        public DbSet<BlockModel> BlockModels { get; set; }
    }
}
