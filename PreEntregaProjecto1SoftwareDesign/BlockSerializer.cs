using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PreEntregaProjecto1SoftwareDesign
{
    class BlockSerializer
    {
        static public void SerializeBlockModel(string path, BlockModel blockModel)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, blockModel);
            stream.Close();
            Console.WriteLine("Done saving Block Model.");
        }

        //retorna True su funciona y False si falla
        static public BlockModel DeserializeBlockModel(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            if (File.Exists(path))
            {
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BlockModel blockModel = (BlockModel)formatter.Deserialize(stream);
                stream.Close();
                return blockModel;
            }
            else
            {
                Console.WriteLine($"File not found");
                return null;
            }

        }

    }
}
