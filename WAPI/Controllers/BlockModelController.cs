using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDPreSubmissionNS;
using WAPI.Models;
using Newtonsoft.Json;
using System.Net;

namespace WAPI.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/block_models")]
    [ApiController]
    public class BlockModelController : ControllerBase
    {
        public string Get()
        {
            List<BlockModel> blockModels = BlockModelContext.LoadAllModels();
            List < Dictionary<string, string> > dics = new List<Dictionary<string, string>>();

            if (blockModels.Count != 0)
            {
                foreach (BlockModel blockModel in blockModels)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("name", blockModel.Name);
                    dics.Add(dic);
                }
            }

            string jsonA = JsonConvert.SerializeObject(dics);
            return jsonA;
        }

        [HttpGet("{name}/blocks")]
        public string Get(string name)
        {

            List<BlockModel> blockModels = BlockModelContext.LoadAllModels();
            BlockModel blockModel = blockModels.Find(r => r.Name.Equals(name));
            List<Dictionary<string, dynamic>> dics = new List<Dictionary<string, dynamic>>();
            foreach (Block block in blockModel.Blocks)
            {
                Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
                dic.Add("x", block.X);
                dic.Add("y", block.Y);
                dic.Add("z", block.Z);
                dic.Add("id", block.Id);
                dic.Add("weight", block.Weight);
                foreach (var keyValuePair in block.CategoricalAttributes)
                {
                    dic.Add(keyValuePair.Key, keyValuePair.Value);
                }
                foreach (var keyValuePair in block.ContinuousAttributes)
                {
                    dic.Add(keyValuePair.Key, keyValuePair.Value);
                }
                foreach (var keyValuePair in block.MassProportionalAttributes)
                {
                    dic.Add(keyValuePair.Key, keyValuePair.Value);
                }
                dics.Add(dic);
            }
            string json = JsonConvert.SerializeObject(dics);
            return json;
        }
    }
}