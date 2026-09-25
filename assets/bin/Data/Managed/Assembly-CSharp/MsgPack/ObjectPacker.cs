// Decompiled with JetBrains decompiler
// Type: MsgPack.ObjectPacker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

#nullable disable
namespace MsgPack;

public class ObjectPacker
{
  private byte[] _buf = new byte[64 /*0x40*/];
  private static Dictionary<System.Type, ObjectPacker.PackDelegate> PackerMapping = new Dictionary<System.Type, ObjectPacker.PackDelegate>();
  private static Dictionary<System.Type, ObjectPacker.UnpackDelegate> UnpackerMapping = new Dictionary<System.Type, ObjectPacker.UnpackDelegate>();

  static ObjectPacker()
  {
    ObjectPacker.PackerMapping.Add(typeof (string), new ObjectPacker.PackDelegate(ObjectPacker.StringPacker));
    ObjectPacker.UnpackerMapping.Add(typeof (string), new ObjectPacker.UnpackDelegate(ObjectPacker.StringUnpacker));
    ObjectPacker.PackerMapping.Add(typeof (DateTime), new ObjectPacker.PackDelegate(ObjectPacker.DateTimePacker));
    ObjectPacker.UnpackerMapping.Add(typeof (DateTime), new ObjectPacker.UnpackDelegate(ObjectPacker.DateTimeUnpacker));
    ObjectPacker.PackerMapping.Add(typeof (XorInt), new ObjectPacker.PackDelegate(ObjectPacker.XorIntPacker));
    ObjectPacker.UnpackerMapping.Add(typeof (XorInt), new ObjectPacker.UnpackDelegate(ObjectPacker.XorIntUnpacker));
    ObjectPacker.PackerMapping.Add(typeof (XorUInt), new ObjectPacker.PackDelegate(ObjectPacker.XorUIntPacker));
    ObjectPacker.UnpackerMapping.Add(typeof (XorUInt), new ObjectPacker.UnpackDelegate(ObjectPacker.XorUIntUnpacker));
    ObjectPacker.PackerMapping.Add(typeof (XorFloat), new ObjectPacker.PackDelegate(ObjectPacker.XorFloatPacker));
    ObjectPacker.UnpackerMapping.Add(typeof (XorFloat), new ObjectPacker.UnpackDelegate(ObjectPacker.XorFloatUnpacker));
  }

  public byte[] Pack(object o)
  {
    using (MemoryStream strm = new MemoryStream())
    {
      this.Pack((Stream) strm, o);
      return strm.ToArray();
    }
  }

  public void Pack(Stream strm, object o)
  {
    if (o != null && o.GetType().IsPrimitive)
      throw new NotSupportedException();
    this.Pack(new MsgPackWriter(strm), o);
  }

  private void Pack(MsgPackWriter writer, object o, System.Type typeHint = null)
  {
    if (o == null)
    {
      if (typeHint == typeof (XorInt))
        this.Pack(writer, (object) 0);
      else if (typeHint == typeof (XorUInt))
        this.Pack(writer, (object) 0U);
      else if (typeHint == typeof (XorFloat))
        this.Pack(writer, (object) 0.0f);
      else
        writer.WriteNil();
    }
    else
    {
      System.Type type = o.GetType();
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
          writer.Write((short) o);
        else if (type.Equals(typeof (ushort)))
        {
          writer.Write((ushort) o);
        }
        else
        {
          if (!type.Equals(typeof (char)))
            throw new NotSupportedException();
          writer.Write((ushort) (char) o);
        }
      }
      else
      {
        ObjectPacker.PackDelegate packDelegate;
        if (ObjectPacker.PackerMapping.TryGetValue(type, out packDelegate))
          packDelegate(this, writer, o);
        else if (type.IsArray)
        {
          Array array = (Array) o;
          writer.WriteArrayHeader(array.Length);
          for (int index = 0; index < array.Length; ++index)
            this.Pack(writer, array.GetValue(index));
        }
        else if (type.IsEnum)
        {
          writer.Write((int) o);
        }
        else
        {
          ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(type);
          writer.WriteMapHeader(reflectionCacheEntry.FieldMap.Count);
          foreach (KeyValuePair<string, FieldInfo> field in (IEnumerable<KeyValuePair<string, FieldInfo>>) reflectionCacheEntry.FieldMap)
          {
            writer.Write(field.Key, this._buf);
            object o1 = field.Value.GetValue(o);
            if (field.Value.FieldType.IsInterface && o1 != null)
            {
              writer.WriteArrayHeader(2);
              writer.Write(o1.GetType().FullName);
            }
            this.Pack(writer, o1, field.Value.FieldType);
          }
        }
      }
    }
  }

  public T Unpack<T>(byte[] buf) => this.Unpack<T>(buf, 0, buf.Length);

  public T Unpack<T>(byte[] buf, int offset, int size)
  {
    using (MemoryStream strm = new MemoryStream(buf, offset, size))
      return this.Unpack<T>((Stream) strm);
  }

  public T Unpack<T>(Stream strm)
  {
    if (typeof (T).IsPrimitive)
      throw new NotSupportedException();
    return (T) this.Unpack(new MsgPackReader(strm), typeof (T));
  }

  public object Unpack(System.Type type, byte[] buf) => this.Unpack(type, buf, 0, buf.Length);

  public object Unpack(System.Type type, byte[] buf, int offset, int size)
  {
    using (MemoryStream strm = new MemoryStream(buf, offset, size))
      return this.Unpack(type, (Stream) strm);
  }

  public object Unpack(System.Type type, Stream strm)
  {
    return !type.IsPrimitive ? this.Unpack(new MsgPackReader(strm), type) : throw new NotSupportedException();
  }

  private object Unpack(MsgPackReader reader, System.Type t)
  {
    if (t.IsPrimitive)
    {
      if (!reader.Read())
        throw new FormatException();
      if (t.Equals(typeof (int)) && reader.IsSigned())
        return (object) reader.ValueSigned;
      if (t.Equals(typeof (int)) && reader.IsUnsigned())
        return (object) (int) reader.ValueUnsigned;
      if (t.Equals(typeof (uint)) && reader.IsUnsigned())
        return (object) reader.ValueUnsigned;
      if (t.Equals(typeof (float)))
      {
        if (reader.Type == TypePrefixes.Float)
          return (object) reader.ValueFloat;
        if (reader.Type == TypePrefixes.Double)
          return (object) (float) reader.ValueDouble;
        if (reader.IsUnsigned())
          return (object) (float) reader.ValueUnsigned;
        if (reader.IsSigned())
          return (object) (float) reader.ValueSigned;
      }
      else
      {
        if (t.Equals(typeof (double)) && reader.Type == TypePrefixes.Double)
          return (object) reader.ValueDouble;
        if (t.Equals(typeof (long)))
        {
          if (reader.IsSigned64())
            return (object) reader.ValueSigned64;
          if (reader.IsSigned())
            return (object) (long) reader.ValueSigned;
          if (reader.IsUnsigned64())
            return (object) (long) reader.ValueUnsigned64;
          if (reader.IsUnsigned())
            return (object) (long) reader.ValueUnsigned;
        }
        else if (t.Equals(typeof (ulong)))
        {
          if (reader.IsUnsigned64())
            return (object) reader.ValueUnsigned64;
          if (reader.IsUnsigned())
            return (object) (ulong) reader.ValueUnsigned;
        }
        else
        {
          if (t.Equals(typeof (bool)) && reader.IsBoolean())
            return (object) (reader.Type == TypePrefixes.True);
          if (t.Equals(typeof (byte)) && reader.IsUnsigned())
            return (object) (byte) reader.ValueUnsigned;
          if (t.Equals(typeof (sbyte)) && reader.IsSigned())
            return (object) (sbyte) reader.ValueSigned;
          if (t.Equals(typeof (short)) && reader.IsSigned())
            return (object) (short) reader.ValueSigned;
          if (t.Equals(typeof (ushort)) && reader.IsUnsigned())
            return (object) (ushort) reader.ValueUnsigned;
          return t.Equals(typeof (char)) && reader.IsUnsigned() ? (object) (char) reader.ValueUnsigned : throw new NotSupportedException();
        }
      }
    }
    ObjectPacker.UnpackDelegate unpackDelegate;
    if (ObjectPacker.UnpackerMapping.TryGetValue(t, out unpackDelegate))
      return unpackDelegate(this, reader);
    if (t.IsArray)
    {
      if (!reader.Read() || !reader.IsArray() && reader.Type != TypePrefixes.Nil)
        throw new FormatException();
      if (reader.Type == TypePrefixes.Nil)
        return (object) null;
      System.Type elementType = t.GetElementType();
      Array instance = Array.CreateInstance(elementType, (int) reader.Length);
      for (int index = 0; index < instance.Length; ++index)
        instance.SetValue(this.Unpack(reader, elementType), index);
      return (object) instance;
    }
    if (t.IsEnum)
    {
      if (!reader.Read())
        throw new FormatException();
      if (reader.IsSigned())
        return Enum.ToObject(t, reader.ValueSigned);
      if (reader.IsSigned64())
        return Enum.ToObject(t, reader.ValueSigned64);
      if (reader.IsUnsigned())
        return Enum.ToObject(t, reader.ValueUnsigned);
      if (reader.IsUnsigned64())
        return Enum.ToObject(t, reader.ValueUnsigned64);
      if (!reader.IsRaw())
        throw new FormatException();
      this.CheckBufferSize((int) reader.Length);
      reader.ReadValueRaw(this._buf, 0, (int) reader.Length);
      string str = Encoding.UTF8.GetString(this._buf, 0, (int) reader.Length);
      return Enum.Parse(t, str);
    }
    if (!reader.Read())
      throw new FormatException();
    if (reader.Type == TypePrefixes.Nil)
      return (object) null;
    if (t.IsInterface)
    {
      if (reader.Type != TypePrefixes.FixArray && reader.Length != 2U)
        throw new FormatException();
      if (!reader.Read() || !reader.IsRaw())
        throw new FormatException();
      this.CheckBufferSize((int) reader.Length);
      reader.ReadValueRaw(this._buf, 0, (int) reader.Length);
      t = System.Type.GetType(Encoding.UTF8.GetString(this._buf, 0, (int) reader.Length));
      if (!reader.Read() || reader.Type == TypePrefixes.Nil)
        throw new FormatException();
    }
    if (!reader.IsMap())
      throw new FormatException();
    object obj = !typeof (ScriptableObject).IsAssignableFrom(t) ? FormatterServices.GetUninitializedObject(t) : (object) ScriptableObject.CreateInstance(t);
    ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(t);
    int length = (int) reader.Length;
    for (int index = 0; index < length; ++index)
    {
      if (!reader.Read() || !reader.IsRaw())
        throw new FormatException();
      this.CheckBufferSize((int) reader.Length);
      reader.ReadValueRaw(this._buf, 0, (int) reader.Length);
      string key = Encoding.UTF8.GetString(this._buf, 0, (int) reader.Length);
      FieldInfo fieldInfo;
      if (!reflectionCacheEntry.FieldMap.TryGetValue(key, out fieldInfo))
        new BoxingPacker().Unpack(reader);
      else
        fieldInfo.SetValue(obj, this.Unpack(reader, fieldInfo.FieldType));
    }
    if (obj is IDeserializationCallback deserializationCallback)
      deserializationCallback.OnDeserialization((object) this);
    return obj;
  }

  private void CheckBufferSize(int size)
  {
    if (this._buf.Length >= size)
      return;
    Array.Resize<byte>(ref this._buf, size);
  }

  private static void StringPacker(ObjectPacker packer, MsgPackWriter writer, object o)
  {
    writer.Write(Encoding.UTF8.GetBytes((string) o));
  }

  private static object StringUnpacker(ObjectPacker packer, MsgPackReader reader)
  {
    if (!reader.Read())
      throw new FormatException();
    if (reader.Type == TypePrefixes.Nil)
      return (object) null;
    if (!reader.IsRaw())
      throw new FormatException();
    packer.CheckBufferSize((int) reader.Length);
    reader.ReadValueRaw(packer._buf, 0, (int) reader.Length);
    return (object) Encoding.UTF8.GetString(packer._buf, 0, (int) reader.Length);
  }

  private static void DateTimePacker(ObjectPacker packer, MsgPackWriter writer, object o)
  {
    DateTime localTime = new DateTime(1970, 1, 1).ToLocalTime();
    writer.Write((long) ((DateTime) o - localTime).TotalSeconds);
  }

  private static object DateTimeUnpacker(ObjectPacker packer, MsgPackReader reader)
  {
    if (!reader.Read())
      throw new FormatException();
    if (reader.Type == TypePrefixes.Nil)
      return (object) null;
    return reader.IsUnsigned() ? (object) new DateTime(1970, 1, 1).ToLocalTime().AddSeconds((double) reader.ValueUnsigned) : throw new FormatException();
  }

  private static void XorIntPacker(ObjectPacker packer, MsgPackWriter writer, object o)
  {
    packer.Pack(writer, (object) (int) (XorInt) o, typeof (int));
  }

  private static object XorIntUnpacker(ObjectPacker packer, MsgPackReader reader)
  {
    return (object) new XorInt((int) packer.Unpack(reader, typeof (int)));
  }

  private static void XorUIntPacker(ObjectPacker packer, MsgPackWriter writer, object o)
  {
    packer.Pack(writer, (object) (uint) (XorUInt) o, typeof (uint));
  }

  private static object XorUIntUnpacker(ObjectPacker packer, MsgPackReader reader)
  {
    return (object) new XorUInt((uint) packer.Unpack(reader, typeof (uint)));
  }

  private static void XorFloatPacker(ObjectPacker packer, MsgPackWriter writer, object o)
  {
    packer.Pack(writer, (object) (float) (XorFloat) o, typeof (float));
  }

  private static object XorFloatUnpacker(ObjectPacker packer, MsgPackReader reader)
  {
    return (object) new XorFloat((float) packer.Unpack(reader, typeof (float)));
  }

  private delegate void PackDelegate(ObjectPacker packer, MsgPackWriter writer, object o);

  private delegate object UnpackDelegate(ObjectPacker packer, MsgPackReader reader);
}
