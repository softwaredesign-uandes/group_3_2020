using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDPreSubmissionNS;

namespace WAPI.Models
{
    public class BlockModelContext : DbContext
    {
        
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

        static public void SaveModel(string path, string attributesString) {
            if (File.Exists(path)) {
                FileInfo file = new FileInfo(path);
                if (!File.Exists("Models\\" + file.Name + ".grupo3")) {
                    List<string> attributesSplit = new List<string>(attributesString.Trim(' ').Split(' '));
                    List<string> continuous_att = new List<string>();
                    List<string> mass_proportional_att = new List<string>();
                    List<string> categorical_att = new List<string>();
                    foreach (string attribute in attributesSplit) {
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

        public BlockModelContext(DbContextOptions<BlockModelContext> options)
            : base(options)
        {
        }

        public DbSet<BlockModel> BlockModels { get; set; }
    }
}
