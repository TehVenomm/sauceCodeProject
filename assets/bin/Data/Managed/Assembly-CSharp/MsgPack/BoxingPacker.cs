// Decompiled with JetBrains decompiler
// Type: MsgPack.BoxingPacker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace MsgPack;

public class BoxingPacker
{
  private static Type KeyValuePairDefinitionType = typeof (KeyValuePair<object, object>).GetGenericTypeDefinition();

  public void Pack(Stream strm, object o) => this.Pack(new MsgPackWriter(strm), o);

  public byte[] Pack(object o)
  {
    using (MemoryStream strm = new MemoryStream())
    {
      this.Pack((Stream) strm, o);
      return strm.ToArray();
    }
  }

  private void Pack(MsgPackWriter writer, object o)
  {
    if (o == null)
    {
      writer.WriteNil();
    }
    else
    {
      Type type = o.GetType();
      if (type.IsPrimitive)
      {
        if (type.Equals(typeof (int)))
          writer.Write((int) o);
        else if (type.Equals(typeof (uint)))
          writer.Write((uint) o);
        else if (type.Equals(typeof (float)))
          writer.Write((float) o);
        else if (type.Equals(typeof (double)))
          writer.Write((double) o);
        else if (type.Equals(typeof (long)))
          writer.Write((long) o);
        else if (type.Equals(typeof (ulong)))
          writer.Write((ulong) o);
        else if (type.Equals(typeof (bool)))
          writer.Write((bool) o);
        else if (type.Equals(typeof (byte)))
          writer.Write((byte) o);
        else if (type.Equals(typeof (sbyte)))
          writer.Write((sbyte) o);
        else if (type.Equals(typeof (short)))
        {
          writer.Write((short) o);
        }
        else
        {
          if (!type.Equals(typeof (ushort)))
            throw new NotSupportedException();
          writer.Write((ushort) o);
        }
      }
      else if (o is IDictionary dictionary)
      {
        writer.WriteMapHeader(dictionary.Count);
        foreach (DictionaryEntry dictionaryEntry in dictionary)
        {
          this.Pack(writer, dictionaryEntry.Key);
          this.Pack(writer, dictionaryEntry.Value);
        }
      }
      else
      {
        if (!type.IsArray)
          return;
        Array array = (Array) o;
        Type elementType = type.GetElementType();
        if (elementType.IsGenericType && elementType.GetGenericTypeDefinition().Equals(BoxingPacker.KeyValuePairDefinitionType))
        {
          PropertyInfo property1 = elementType.GetProperty("Key");
          PropertyInfo property2 = elementType.GetProperty("Value");
          writer.WriteMapHeader(array.Length);
          for (int index = 0; index < array.Length; ++index)
          {
            object obj = array.GetValue(index);
            this.Pack(writer, property1.GetValue(obj, (object[]) null));
            this.Pack(writer, property2.GetValue(obj, (object[]) null));
          }
        }
        else
        {
          writer.WriteArrayHeader(array.Length);
          for (int index = 0; index < array.Length; ++index)
            this.Pack(writer, array.GetValue(index));
        }
      }
    }
  }

  public object Unpack(Stream strm) => this.Unpack(new MsgPackReader(strm));

  public object Unpack(byte[] buf, int offset, int size)
  {
    using (MemoryStream strm = new MemoryStream(buf, offset, size))
      return this.Unpack((Stream) strm);
  }

  public object Unpack(byte[] buf) => this.Unpack(buf, 0, buf.Length);

  public object Unpack(MsgPackReader reader)
  {
    TypePrefixes typePrefixes = reader.Read() ? reader.Type : throw new FormatException();
    if ((uint) typePrefixes <= 128U /*0x80*/)
    {
      if (typePrefixes != TypePrefixes.PositiveFixNum)
      {
        if (typePrefixes == TypePrefixes.FixMap)
          goto label_25;
        goto label_37;
      }
    }
    else
    {
      switch (typePrefixes)
      {
        case TypePrefixes.FixArray:
        case TypePrefixes.Array16:
        case TypePrefixes.Array32:
          object[] objArray = new object[(int) reader.Length];
          for (int index = 0; index < objArray.Length; ++index)
            objArray[index] = this.Unpack(reader);
          return (object) objArray;
        case TypePrefixes.FixRaw:
        case TypePrefixes.Raw8:
        case TypePrefixes.Raw16:
        case TypePrefixes.Raw32:
          return (object) reader.ReadRawString();
        case TypePrefixes.Nil:
          return (object) null;
        case TypePrefixes.False:
          return (object) false;
        case TypePrefixes.True:
          return (object) true;
        case TypePrefixes.Bin8:
        case TypePrefixes.Bin16:
        case TypePrefixes.Bin32:
          byte[] buf = new byte[(int) reader.Length];
          reader.ReadValueRaw(buf, 0, buf.Length);
          return (object) buf;
        case TypePrefixes.Ext8:
        case TypePrefixes.Ext16:
        case TypePrefixes.Ext32:
          sbyte type = reader.ReadExtType();
          switch (type)
          {
            case 81:
              if (reader.Length == 16U /*0x10*/)
              {
                double num1 = (double) reader.ReadSingle();
                float num2 = reader.ReadSingle();
                float num3 = reader.ReadSingle();
                float num4 = reader.ReadSingle();
                double num5 = (double) num2;
                double num6 = (double) num3;
                double num7 = (double) num4;
                return (object) new Quaternion((float) num1, (float) num5, (float) num6, (float) num7);
              }
              break;
            case 86:
              if (reader.Length == 12U)
              {
                double num8 = (double) reader.ReadSingle();
                float num9 = reader.ReadSingle();
                float num10 = reader.ReadSingle();
                double num11 = (double) num9;
                double num12 = (double) num10;
                return (object) new Vector3((float) num8, (float) num11, (float) num12);
              }
              break;
            case 87:
              if (reader.Length == 8U)
                return (object) new Vector2(reader.ReadSingle(), reader.ReadSingle());
              break;
          }
          byte[] numArray = new byte[(int) reader.Length];
          reader.ReadValueRaw(numArray, 0, (int) reader.Length);
          return (object) new Ext(type, numArray);
        case TypePrefixes.Float:
          return (object) reader.ValueFloat;
        case TypePrefixes.Double:
          return (object) reader.ValueDouble;
        case TypePrefixes.UInt8:
          return (object) (byte) reader.ValueUnsigned;
        case TypePrefixes.UInt16:
          return (object) (ushort) reader.ValueUnsigned;
        case TypePrefixes.UInt32:
          return (object) reader.ValueUnsigned;
        case TypePrefixes.UInt64:
          return (object) reader.ValueUnsigned64;
        case TypePrefixes.Int8:
          return (object) (sbyte) reader.ValueSigned;
        case TypePrefixes.Int16:
          return (object) (short) reader.ValueSigned;
        case TypePrefixes.Int32:
        case TypePrefixes.NegativeFixNum:
          break;
        case TypePrefixes.Int64:
          return (object) reader.ValueSigned64;
        case TypePrefixes.Map16:
        case TypePrefixes.Map32:
          goto label_25;
        default:
          goto label_37;
      }
    }
    return (object) reader.ValueSigned;
label_25:
    IDictionary<object, object> dictionary = (IDictionary<object, object>) new Dictionary<object, object>((int) reader.Length);
    int length = (int) reader.Length;
    for (int index = 0; index < length; ++index)
    {
      object key = this.Unpack(reader);
      object obj = this.Unpack(reader);
      dictionary.Add(key, obj);
    }
    return (object) dictionary;
label_37:
    throw new FormatException();
  }
}
