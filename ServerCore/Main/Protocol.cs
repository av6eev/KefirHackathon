using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ServerCore.Main.Utilities;

namespace ServerCore.Main
{
    public class Protocol
    {
        public BinaryWriter Writer;
        public BinaryReader Reader;
        public MemoryStream Stream;
        
        public Protocol(byte[] buffer = null)
        {
            if (buffer != null)
            {
                Stream = new MemoryStream(buffer);
                Reader = new BinaryReader(Stream);
                Reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                Stream = new MemoryStream();
                Writer = new BinaryWriter(Stream);
            }
        }

        public void Add<T>(T value)
        {
            // Console.WriteLine(typeof(T).Name);
            
            switch (value)
            {
                case byte byteValue:
                    Writer.Write(byteValue);
                    break;
                case float floatValue:
                    Writer.Write(floatValue);
                    break;
                case int intValue:
                    Writer.Write(intValue);
                    break;
                case long longValue:
                    Writer.Write(longValue);
                    break;
                case double doubleValue:
                    Writer.Write(doubleValue);
                    break;
                case decimal decimalValue:
                    Writer.Write(decimalValue);
                    break;
                case bool boolValue:
                    Writer.Write(boolValue);
                    break;
                case uint uintValue:
                    Writer.Write(uintValue);
                    break;
                case short shortValue:
                    Writer.Write(shortValue);
                    break;
                case ushort uShortValue:
                    Writer.Write(uShortValue);
                    break;
                case Vector3 vectorValue:
                    Writer.Write(vectorValue.X);
                    Writer.Write(vectorValue.Y);
                    Writer.Write(vectorValue.Z);
                    break;
                case string str:
                    var length = (ushort)Encoding.UTF8.GetBytes(str).Length;
                    // Console.WriteLine(length);
                    Writer.Write(length);
                    Writer.Write(Encoding.UTF8.GetBytes(str));
                    break;
            }
        }
    
        public void Get<T>(out T value)
        {
            switch (typeof(T).Name)
            {
                case nameof(Byte):
                    value = (T)(object)Reader.ReadByte();
                    break;
                case nameof(Single):
                    value = (T)(object)Reader.ReadSingle();
                    break;
                case nameof(Int32):
                    value = (T)(object)Reader.ReadInt32();
                    break;
                case nameof(Int64):
                    value = (T)(object)Reader.ReadInt64();
                    break;
                case nameof(Double):
                    value = (T)(object)Reader.ReadDouble();
                    break;
                case nameof(Decimal):
                    value = (T)(object)Reader.ReadDecimal();
                    break;
                case nameof(Boolean):
                    value = (T)(object)Reader.ReadBoolean();
                    break;
                case nameof(UInt32):
                    value = (T)(object)Reader.ReadUInt32();
                    break;
                case nameof(Int16):
                    value = (T)(object)Reader.ReadInt16();
                    break;
                case nameof(UInt16):
                    value = (T)(object)Reader.ReadUInt16();
                    break;
                case nameof(Vector3):
                    var vector = new Vector3(Reader.ReadInt32(), Reader.ReadInt32(), Reader.ReadInt32());
                    value = (T)(object)vector;
                    break;
                case nameof(String):
                    var readInt32 = Reader.ReadUInt16();
                    var str = Encoding.UTF8.GetString(Reader.ReadBytes(readInt32));
                    value = (T)(object)str;
                    break;
                default:
                    value = default;
                    break;
            }
        }
    }
}