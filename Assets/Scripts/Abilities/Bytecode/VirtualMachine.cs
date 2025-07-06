using System.Collections.Generic;
using System.IO;


namespace Prototype.BytecodeTool
{
    public class AbilityVirtualMachine
    {
        int maxStackSize = 128;

        public void Interpret(char[] buffer, Ability ab)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                
            }
        }
    }

    public static class InstructionWriter
    {
        public static void Write(string path, string filename, byte[] instructions)
        {
            if (File.Exists(path)) {
                File.WriteAllBytes($"{path}/filename.bin", instructions);
            }
        }

        public static byte[] Read(string path, string filename)
        {
            if (!File.Exists(path))
            {
                byte[] emptyReturn = new byte[0];
                return emptyReturn;
            }
            return File.ReadAllBytes(path);
        }
    }

    public class BytecodeBuilder
    {
        public static List<byte> OpList = new List<byte>();
        public static void Add(Instruction instruction, params byte[] operands)
        {
            OpList.Add((byte)instruction);
            OpList.AddRange(operands);
        }

        public static byte[] ToBytecode() => OpList.ToArray();
    }
}