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
            if (File.Exists("*.grupo3"))
            {
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
            }
            return blockModels;
        }

        public BlockModelContext(DbContextOptions<BlockModelContext> options)
            : base(options)
        {
        }

        public DbSet<BlockModel> BlockModels { get; set; }
    }
}
