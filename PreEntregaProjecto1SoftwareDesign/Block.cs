using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SDPreSubmissionNS
{
    [Serializable]
    public class Block
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockType Type;

        public Block(int id, int x, int y, int z, BlockType type)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Type = type;
        }

        public double? GetMassInKg()
        {
            return Type.Weight*1000;
        }
    }
}
