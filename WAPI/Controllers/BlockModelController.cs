﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using SDPreSubmissionNS;
using WAPI.Models;
using Newtonsoft.Json;
using System.Net;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System.IO;
using Azure.Core;
using System.Web.Helpers;
using Microsoft.AspNetCore.Cors;
using System.Web.Cors;
using PreEntregaProjecto1SoftwareDesign;

namespace WAPI.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/block_models")]
    [ApiController]
    public class BlockModelController : ControllerBase
    {

        private readonly IFeatureManager _featureManager;
        public static IWebHostEnvironment _environment;
        
        public BlockModelController(IFeatureManager featureManager, IWebHostEnvironment environment)
        {
            _featureManager = featureManager;
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public IFormFile file { get; set; }
        }
        public string Get()
        {
            bool flagRestfulResponse = _featureManager.IsEnabledAsync("restful_response").Result;
            List<string> blockModelsNames = BlockModelContext.LoadAllBlockModelNames(_environment);
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

        [HttpGet("test")]
        public string GetTest()
        {
            List<string> fileNames = BlockModelContext.testGetFileNames(_environment);
            string json = "";
            json = JsonConvert.SerializeObject(fileNames);
            return json;
        }

        [HttpGet("prec")]
        public List<string> GetPrecs()
        {
            List<string> names = BlockModelContext.RecivePrecFiles();

            return names;
        }

        [HttpGet("webrootpath")]
        public string TestDos()
        {
            return _environment.WebRootPath;
        }

        [HttpGet("{name}/blocks")]
        public string Get(string name)
        {

            try
            {
                bool flagRestfulResponse = _featureManager.IsEnabledAsync("restful_response").Result;
                List<BlockModel> blockModels = BlockModelContext.LoadAllModels(_environment);
                BlockModel blockModel = blockModels.Find(r => r.Name.Equals(name));
                new Trace("blocks_requested", name, true);
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
            catch (Exception ex)
            {
                return "error";
            }
            
        }

        [HttpGet("{name}/blocks/{index}")]
        public string GetCube(string name, string index)
        {
            
            BlockModel blockModel = BlockModelContext.GetBlockModel(name);
            int blockid;
            if (blockModel.Name != "noBlockModel" && int.TryParse(index, out blockid))
            {
                Block block = blockModel.Blocks.FirstOrDefault(b => b.Id == blockid);
                BlockForIndexEndpoint bloque = new BlockForIndexEndpoint(block.Id, block.X, block.Y, block.Z, block.Weight, block.MassProportionalAttributes);
                Dictionary<string, BlockForIndexEndpoint> dic = new Dictionary<string, BlockForIndexEndpoint>();

                new Trace("block_info_requested", block.X.ToString() + "," + block.Y.ToString() + "," + block.Z.ToString() , true);
                
                dic.Add("block", bloque);
                string json = JsonConvert.SerializeObject(dic);
                return json;
            }
            else
            {
                return "Error Loading Block Model or Index";
            }
        }

        [HttpPost("{name}/blocks/{index}/extract")]
        public string Get(string name, string index)
        {
            int id;
            if (int.TryParse(index, out id))
            {
                Dictionary<int, List<int>> precDictionary = BlockModelContext.GeneratePrecDict(name);
                if (precDictionary.Count > 0)
                {
                    List<int> blocksToExtract = BlockModelContext.ExtractCubes(id, precDictionary);

                    Dictionary<string, List<Dictionary<string, int>>> dicForJson = new Dictionary<string, List<Dictionary<string, int>>>();
                    List<Dictionary<string, int>> valuesDicForJson = new List<Dictionary<string, int>>();
                    foreach (int i in blocksToExtract)
                    {
                        Dictionary<string, int> value = new Dictionary<string, int>();
                        value.Add("index", i);
                        valuesDicForJson.Add(value);
                    }
                    dicForJson.Add("blocks", valuesDicForJson);

                    BlockModel blockModel = BlockModelContext.GetBlockModel(name);
                    if (blockModel.Name != "noBlockModel")
                    {
                        Block block = blockModel.Blocks.FirstOrDefault(b => b.Id == id);
                        new Trace("block_extracted", block.X.ToString() + "," + block.Y.ToString() + "," + block.Z.ToString(), true);
                    }

                    string elJsonDeRetorno = JsonConvert.SerializeObject(dicForJson);
                    return elJsonDeRetorno;
                }
                else
                {
                    return "precFile not found";
                }
                
            }
            else
            {
                return "wrong id";
            }
            
        }

        //Ejemplo de funcion para subir archivos
        [HttpPost("upload")]
        public string Post([FromForm] FileUploadAPI objFile)
        {
            if (objFile.file.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.file.FileName))
                {
                    objFile.file.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\Updload\\" + objFile.file.FileName;
                }
            }
            else
            {
                return "Failed";
            }
        }

        [HttpPost("new")]
        public string Post([FromForm] FileUploadAPI objFile,[FromForm] string attributesString)
        {
            try
            {
                BlockModelContext.SaveNewModel(objFile, attributesString);
                return "Model Saved";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPost("{name}/reblock")]
        public string Post(string name, string rx, string ry, string rz)
        {
            int x = int.Parse(rx);
            int y = int.Parse(ry);
            int z = int.Parse(rz);
            BlockModelContext.Reblock(name, x, y, z);
            return "reblocked";
        }

        [HttpPost("newprec")]
        public string PostPrec([FromForm] FileUploadAPI objFile)
        {
            try
            {
                BlockModelContext.SaveNewPrecFile(objFile);

                new Trace("block_model_precedences_loaded", Path.GetFileNameWithoutExtension(objFile.file.FileName) , true);

                return "prec file uploaded";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


        

        [HttpDelete("{name}/delete")]
        public string Delete(string name)
        {
            bool borro = BlockModelContext.DeleteFile(_environment, name);
            if (borro)
            {
                return "deleted";
            }
            else
            {
                return "nothing was deleted";
            }
        }
    }
}