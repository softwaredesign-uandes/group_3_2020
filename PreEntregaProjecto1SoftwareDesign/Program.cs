﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PreEntregaProjecto1SoftwareDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\ignap\source\repos\PreEntregaProjecto1SoftwareDesign\PreEntregaProjecto1SoftwareDesign\marvin.blocks";
            List<Block> blocks = new List<Block>();
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] buffer = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    string[] lines = temp.GetString(buffer).Split('\n');
                    if (lines.Length != 48) //esto le indica al programa que ya no encontró más lineas en el archivo
                    {
                        foreach (string line in lines)
                        {
                            //esto es para devolver el lector en caso que el buffer se quede sin espacio a la mitad de una linea
                            if (line.Trim(' ').Split(' ').Length != 8 || line.Trim(' ').EndsWith('-'))
                            {
                                fileStream.Position = fileStream.Position - line.Length; 
                            }
                            //este else agrega los bloques del archivo a una lista
                            else
                            {
                                string[] cubeData = line.Trim(' ').Split(' ');
                                #region preparando variables

                                int id = int.Parse(cubeData[0]);
                                int x = int.Parse(cubeData[1]);
                                int y = int.Parse(cubeData[2]);
                                int z = int.Parse(cubeData[3]);
                                double tonn = double.Parse(cubeData[4]);
                                double au = double.Parse(cubeData[5]);
                                double cu = double.Parse(cubeData[6]);
                                double porc_profit = double.Parse(cubeData[7]);

                                #endregion
                                Block block = new Block(id, x, y, z, tonn, au, cu, porc_profit);
                                blocks.Add(block);
                                Console.WriteLine(block.id);
                            }

                        }
                    }
                }
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
