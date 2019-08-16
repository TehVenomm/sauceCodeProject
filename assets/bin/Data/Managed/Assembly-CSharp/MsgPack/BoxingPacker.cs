using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MsgPack
{
	public class BoxingPacker
	{
		private static Type KeyValuePairDefinitionType;

		static BoxingPacker()
		{
			KeyValuePairDefinitionType = typeof(KeyValuePair<object, object>).GetGenericTypeDefinition();
		}

		public void Pack(Stream strm, object o)
		{
			MsgPackWriter writer = new MsgPackWriter(strm);
			Pack(writer, o);
		}

		public byte[] Pack(object o)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Pack(memoryStream, o);
				return memoryStream.ToArray();
			}
		}

		private void Pack(MsgPackWriter writer, object o)
		{
			if (o == null)
			{
				writer.WriteNil();
				return;
			}
			Type type = o.GetType();
			if (type.IsPrimitive)
			{
				if (type.Equals(typeof(int)))
				{
					writer.Write((int)o);
					return;
				}
				if (type.Equals(typeof(uint)))
				{
					writer.Write((uint)o);
					return;
				}
				if (type.Equals(typeof(float)))
				{
					writer.Write((float)o);
					return;
				}
				if (type.Equals(typeof(double)))
				{
					writer.Write((double)o);
					return;
				}
				if (type.Equals(typeof(long)))
				{
					writer.Write((long)o);
					return;
				}
				if (type.Equals(typeof(ulong)))
				{
					writer.Write((ulong)o);
					return;
				}
				if (type.Equals(typeof(bool)))
				{
					writer.Write((bool)o);
					return;
				}
				if (type.Equals(typeof(byte)))
				{
					writer.Write((byte)o);
					return;
				}
				if (type.Equals(typeof(sbyte)))
				{
					writer.Write((sbyte)o);
					return;
				}
				if (type.Equals(typeof(short)))
				{
					writer.Write((short)o);
					return;
				}
				if (type.Equals(typeof(ushort)))
				{
					writer.Write((ushort)o);
					return;
				}
				throw new NotSupportedException();
			}
			IDictionary dictionary = o as IDictionary;
			if (dictionary != null)
			{
				writer.WriteMapHeader(dictionary.Count);
				IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
						Pack(writer, dictionaryEntry.Key);
						Pack(writer, dictionaryEntry.Value);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				if (!type.IsArray)
				{
					return;
				}
				Array array = (Array)o;
				Type elementType = type.GetElementType();
				if (elementType.IsGenericType && elementType.GetGenericTypeDefinition().Equals(KeyValuePairDefinitionType))
				{
					PropertyInfo property = elementType.GetProperty("Key");
					PropertyInfo property2 = elementType.GetProperty("Value");
					writer.WriteMapHeader(array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						object value = array.GetValue(i);
						Pack(writer, property.GetValue(value, null));
						Pack(writer, property2.GetValue(value, null));
					}
				}
				else
				{
					writer.WriteArrayHeader(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						Pack(writer, array.GetValue(j));
					}
				}
			}
		}

		public object Unpack(Stream strm)
		{
			MsgPackReader reader = new MsgPackReader(strm);
			return Unpack(reader);
		}

		public object Unpack(byte[] buf, int offset, int size)
		{
			using (MemoryStream strm = new MemoryStream(buf, offset, size))
			{
				return Unpack(strm);
			}
		}

		public object Unpack(byte[] buf)
		{
			return Unpack(buf, 0, buf.Length);
		}

		public object Unpack(MsgPackReader reader)
		{
			//IL_0258: Unknown result type (might be due to invalid IL or missing references)
			//IL_0293: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			if (!reader.Read())
			{
				throw new FormatException();
			}
			switch (reader.Type)
			{
			case TypePrefixes.PositiveFixNum:
			case TypePrefixes.Int32:
			case TypePrefixes.NegativeFixNum:
				return reader.ValueSigned;
			case TypePrefixes.Int8:
				return (sbyte)reader.ValueSigned;
			case TypePrefixes.Int16:
				return (short)reader.ValueSigned;
			case TypePrefixes.Int64:
				return reader.ValueSigned64;
			case TypePrefixes.UInt8:
				return (byte)reader.ValueUnsigned;
			case TypePrefixes.UInt16:
				return (ushort)reader.ValueUnsigned;
			case TypePrefixes.UInt32:
				return reader.ValueUnsigned;
			case TypePrefixes.UInt64:
				return reader.ValueUnsigned64;
			case TypePrefixes.True:
				return true;
			case TypePrefixes.False:
				return false;
			case TypePrefixes.Float:
				return reader.ValueFloat;
			case TypePrefixes.Double:
				return reader.ValueDouble;
			case TypePrefixes.Nil:
				return null;
			case TypePrefixes.FixRaw:
			case TypePrefixes.Raw8:
			case TypePrefixes.Raw16:
			case TypePrefixes.Raw32:
				return reader.ReadRawString();
			case TypePrefixes.Bin8:
			case TypePrefixes.Bin16:
			case TypePrefixes.Bin32:
			{
				byte[] array3 = new byte[reader.Length];
				reader.ReadValueRaw(array3, 0, array3.Length);
				return array3;
			}
			case TypePrefixes.FixArray:
			case TypePrefixes.Array16:
			case TypePrefixes.Array32:
			{
				object[] array2 = new object[reader.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = Unpack(reader);
				}
				return array2;
			}
			case TypePrefixes.FixMap:
			case TypePrefixes.Map16:
			case TypePrefixes.Map32:
			{
				IDictionary<object, object> dictionary = new Dictionary<object, object>((int)reader.Length);
				int length = (int)reader.Length;
				for (int i = 0; i < length; i++)
				{
					object key = Unpack(reader);
					object value = Unpack(reader);
					dictionary.Add(key, value);
				}
				return dictionary;
			}
			case TypePrefixes.Ext8:
			case TypePrefixes.Ext16:
			case TypePrefixes.Ext32:
			{
				sbyte b = reader.ReadExtType();
				switch (b)
				{
				case 81:
					if (reader.Length == 16)
					{
						float num3 = reader.ReadSingle();
						float num4 = reader.ReadSingle();
						float num5 = reader.ReadSingle();
						float num6 = reader.ReadSingle();
						return (object)new Quaternion(num3, num4, num5, num6);
					}
					break;
				case 86:
					if (reader.Length == 12)
					{
						float num7 = reader.ReadSingle();
						float num8 = reader.ReadSingle();
						float num9 = reader.ReadSingle();
						return (object)new Vector3(num7, num8, num9);
					}
					break;
				case 87:
					if (reader.Length == 8)
					{
						float num = reader.ReadSingle();
						float num2 = reader.ReadSingle();
						return (object)new Vector2(num, num2);
					}
					break;
				}
				byte[] array = new byte[reader.Length];
				reader.ReadValueRaw(array, 0, (int)reader.Length);
				return new Ext(b, array);
			}
			default:
				throw new FormatException();
			}
		}
	}
}
