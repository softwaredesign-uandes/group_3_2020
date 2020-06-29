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
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace WAPI.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/block_models")]
    [ApiController]
    public class BlockModelController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;
        public BlockModelController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }
        public string Get()
        {
            bool flagRestfulResponse = _featureManager.IsEnabledAsync("restful_response").Result;
            List<string> blockModelsNames = BlockModelContext.LoadAllBlockModelNames();
            List<Dictionary<string, string>> dics = new List<Dictionary<string, string>>();

            if (blockModelsNames.Count != 0)
            {
                foreach (string blockModelName in blockModelsNames)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("name", blockModelName);
                    dics.Add(dic);
                }
            }
            string jsonA = "";
            if (flagRestfulResponse)
            {
                Dictionary<string, List<Dictionary<string, string>>> prefix = new Dictionary<string, List<Dictionary<string, string>>>();
                prefix.Add("block_models", dics);
                jsonA = JsonConvert.SerializeObject(prefix);
            }
            else
            {
                jsonA = JsonConvert.SerializeObject(dics);
            }
            return jsonA;
        }

        [HttpGet("{name}/blocks")]
        public string Get(string name)
        {

            bool flagRestfulResponse = _featureManager.IsEnabledAsync("restful_response").Result;
            List <BlockModel> blockModels = BlockModelContext.LoadAllModels();
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
            string json = "";
            if (flagRestfulResponse)
            {
                Dictionary<string, List<Dictionary<string, dynamic>>> prefix = new Dictionary<string, List<Dictionary<string, dynamic>>>();
                prefix.Add("blocks", dics);
                Dictionary<string, Dictionary<string, List<Dictionary<string, dynamic>>>> preprefix = new Dictionary<string, Dictionary<string, List<Dictionary<string, dynamic>>>>();
                preprefix.Add("block_model", prefix);
                json = JsonConvert.SerializeObject(preprefix);
            }
            else
            {
                json = JsonConvert.SerializeObject(dics);
            }
            
            return json;
        }
        [HttpPost("new")]
        public void Post(string path, string attributesString) {
            BlockModelContext.SaveModel(path, attributesString);
        }
        [HttpPost("{name}/reblock")]
        public void Post(string name, int rx, int ry, int rz) {
            List<string> blockModelsNames = new List<string>();
        }

    }
}