using SDPreSubmissionNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SDPreSubmissionNS
{
    public class PrecManager
    {

        static public Dictionary<int,List<int>> GenerateDictionary(string filePath)
        {
            string line;
            Dictionary<int, List<int>> diccionarioPrec = new Dictionary<int, List<int>>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            
            while ((line = file.ReadLine()) != null)
            {
                List<int> values = new List<int>();
                string[] splittedLine = line.Split(' ');
                string key_s = splittedLine[0];
                int key_i;
                int.TryParse(key_s, out key_i);
                int c = 2;
                while (c < splittedLine.Length)
                {
                    string num_s = splittedLine[c];
                    int num_i;
                    int.TryParse(num_s, out num_i);
                    values.Add(num_i);
                    c++;
                }
                diccionarioPrec.Add(key_i, values);
            }
            file.Close();

            return diccionarioPrec;
        }
        static public List<int> ExtractBlock(int idBlock, Dictionary<int,List<int>> dic)
        {
            int x = 0;
            List<int> blocksToExtract = new List<int>();
            blocksToExtract.Add(idBlock);
            /*
            foreach (int i in dic[idBlock])
            {
                if (!blocksToExtract.Contains(i))
                {
                    blocksToExtract.Add(i);
                }
            }
            */

            while (x < blocksToExtract.Count)
            {
                foreach (int i in dic[blocksToExtract[x]])
                {
                    if (!blocksToExtract.Contains(i))
                    {
                        blocksToExtract.Add(i);
                    }
                }
                x++;
            }

            return blocksToExtract;
        }

    }
}
