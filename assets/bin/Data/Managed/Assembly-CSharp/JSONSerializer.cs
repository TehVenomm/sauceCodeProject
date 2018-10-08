using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public static class JSONSerializer
{
	public static T Deserialize<T>(string message) where T : new()
	{
		JSONInStream stream = new JSONInStream(message);
		return (T)DeserializeObject(stream, typeof(T));
	}

	public static T Deserialize<T>(string message, Type type) where T : new()
	{
		JSONInStream stream = new JSONInStream(message);
		return (T)DeserializeObject(stream, type);
	}

	public static string Serialize<T>(T message)
	{
		JSONOutStream jSONOutStream = new JSONOutStream();
		SerializeObject(jSONOutStream, typeof(T), message);
		return jSONOutStream.Serialize();
	}

	public static string Serialize(object message, Type type)
	{
		JSONOutStream jSONOutStream = new JSONOutStream();
		SerializeObject(jSONOutStream, type, message);
		return jSONOutStream.Serialize();
	}

	private static string GetName(FieldInfo fi)
	{
		FormerlySerializedAsAttribute val = fi.GetCustomAttributes(typeof(FormerlySerializedAsAttribute), false).FirstOrDefault() as FormerlySerializedAsAttribute;
		if (val == null)
		{
			return fi.Name;
		}
		return val.get_oldName();
	}

	private static IEnumerable<FieldInfo> GetTargetFields(Type type)
	{
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo f in fields)
		{
			if (!f.IsPublic)
			{
				string typeName = f.FieldType.ToString();
				if (typeName != "XorInt" && typeName != "XorUInt" && typeName != "XorFloat")
				{
					continue;
				}
			}
			yield return f;
		}
	}

	private static void SerializeObject(JSONOutStream stream, Type type, object message)
	{
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		MethodInfo method = type.GetMethod("ToJSON");
		if (method != null)
		{
			method.Invoke(message, new object[1]
			{
				stream
			});
		}
		else
		{
			IEnumerable<FieldInfo> targetFields = GetTargetFields(type);
			foreach (FieldInfo item in targetFields)
			{
				switch (item.FieldType.ToString())
				{
				case "System.String":
					stream.Content(GetName(item), (string)item.GetValue(message));
					break;
				case "System.Single":
					stream.Content(GetName(item), (XorFloat)(float)item.GetValue(message));
					break;
				case "System.Double":
					stream.Content(GetName(item), (double)item.GetValue(message));
					break;
				case "System.Int32":
					stream.Content(GetName(item), (int)item.GetValue(message));
					break;
				case "System.Boolean":
					stream.Content(GetName(item), (bool)item.GetValue(message));
					break;
				case "UnityEngine.Vector3":
					stream.Content(GetName(item), (Vector3)item.GetValue(message));
					break;
				case "UnityEngine.Quaternion":
					stream.Content(GetName(item), (Quaternion)item.GetValue(message));
					break;
				case "UnityEngine.Color":
					stream.Content(GetName(item), (Color)item.GetValue(message));
					break;
				case "UnityEngine.Rect":
					stream.Content(GetName(item), (Rect)item.GetValue(message));
					break;
				case "UnityEngine.Vector2":
					stream.Content(GetName(item), (Vector2)item.GetValue(message));
					break;
				case "XorInt":
					stream.Content(GetName(item), item.GetValue(message) as XorInt);
					break;
				case "XorUInt":
					stream.Content(GetName(item), item.GetValue(message) as XorUInt);
					break;
				case "XorFloat":
					stream.Content(GetName(item), item.GetValue(message) as XorFloat);
					break;
				default:
					if (item.FieldType.IsEnum)
					{
						stream.Content(GetName(item), item.GetValue(message).ToString());
					}
					else if (item.FieldType.IsGenericType)
					{
						Type type2 = item.FieldType.GetGenericArguments()[0];
						Type typeFromHandle = typeof(List<>);
						Type type3 = typeFromHandle.MakeGenericType(type2);
						PropertyInfo property = type3.GetProperty("Count");
						PropertyInfo property2 = type3.GetProperty("Item");
						int num = (int)property.GetValue(item.GetValue(message), new object[0]);
						stream.List(GetName(item));
						for (int i = 0; i < num; i++)
						{
							object value = property2.GetValue(item.GetValue(message), new object[1]
							{
								i
							});
							SerializeListElement(stream, type2, value, i);
						}
						stream.End();
					}
					else if (item.FieldType.IsArray)
					{
						object[] array = ToObjectArray((IEnumerable)item.GetValue(message));
						Type type4 = Type.GetTypeArray(array)[0];
						stream.List(GetName(item));
						for (int j = 0; j < array.Length; j++)
						{
							object message2 = array[j];
							SerializeListElement(stream, type4, message2, j);
						}
						stream.End();
					}
					else
					{
						stream.Start(GetName(item));
						SerializeObject(stream, item.FieldType, item.GetValue(message));
						stream.End();
					}
					break;
				}
			}
		}
	}

	private static void SerializeListElement(JSONOutStream stream, Type type, object message, int i)
	{
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		if (!type.IsEnum)
		{
			switch (type.ToString())
			{
			case "System.String":
				stream.Content(i, (string)message);
				break;
			case "System.Single":
				stream.Content(i, (XorFloat)(float)message);
				break;
			case "System.Double":
				stream.Content(i, (double)message);
				break;
			case "System.Int32":
				stream.Content(i, (int)message);
				break;
			case "System.Boolean":
				stream.Content(i, (bool)message);
				break;
			case "UnityEngine.Vector3":
				stream.Content(i, (Vector3)message);
				break;
			case "UnityEngine.Quaternion":
				stream.Content(i, (Quaternion)message);
				break;
			case "UnityEngine.Color":
				stream.Content(i, (Color)message);
				break;
			case "UnityEngine.Rect":
				stream.Content(i, (Rect)message);
				break;
			case "UnityEngine.Vector2":
				stream.Content(i, (Vector2)message);
				break;
			case "XorInt":
				stream.Content(i, new XorInt((int)message));
				break;
			case "XorUInt":
				stream.Content(i, new XorUInt((uint)message));
				break;
			case "XorFloat":
				stream.Content(i, new XorFloat((float)message));
				break;
			default:
				stream.Start(i);
				SerializeObject(stream, type, message);
				stream.End();
				break;
			}
		}
		else
		{
			stream.Content(i, (int)message);
		}
	}

	private static object DeserializeObject(JSONInStream stream, Type type)
	{
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		MethodInfo method = type.GetMethod("FromJSON");
		if (method != null)
		{
			return method.Invoke(null, new object[1]
			{
				stream
			});
		}
		object obj = Activator.CreateInstance(type);
		IEnumerable<FieldInfo> targetFields = GetTargetFields(type);
		foreach (FieldInfo item in targetFields)
		{
			switch (item.FieldType.ToString())
			{
			case "System.String":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out string value7);
					item.SetValue(obj, value7);
				}
				break;
			case "System.Single":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out float value12);
					item.SetValue(obj, value12);
				}
				break;
			case "System.Double":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out double value16);
					item.SetValue(obj, value16);
				}
				break;
			case "System.Int32":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out int value9);
					item.SetValue(obj, value9);
				}
				break;
			case "System.Boolean":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out bool value5);
					item.SetValue(obj, value5);
				}
				break;
			case "UnityEngine.Vector3":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out Vector3 value14);
					item.SetValue(obj, value14);
				}
				break;
			case "UnityEngine.Quaternion":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out Quaternion value10);
					item.SetValue(obj, value10);
				}
				break;
			case "UnityEngine.Color":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out Color value8);
					item.SetValue(obj, value8);
				}
				break;
			case "UnityEngine.Rect":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out Rect value6);
					item.SetValue(obj, value6);
				}
				break;
			case "UnityEngine.Vector2":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out Vector2 value4);
					item.SetValue(obj, value4);
				}
				break;
			case "XorInt":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out XorInt value15);
					item.SetValue(obj, value15);
				}
				break;
			case "XorUInt":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out XorUInt value13);
					item.SetValue(obj, value13);
				}
				break;
			case "XorFloat":
				if (stream.Has(GetName(item)))
				{
					stream.Content(GetName(item), out XorFloat value11);
					item.SetValue(obj, value11);
				}
				break;
			default:
				if (stream.Has(GetName(item)))
				{
					if (item.FieldType.IsEnum)
					{
						stream.Content(GetName(item), out string value);
						item.SetValue(obj, Enum.Parse(item.FieldType, value));
					}
					else if (item.FieldType.IsGenericType)
					{
						Type containedType = item.FieldType.GetGenericArguments()[0];
						Type typeFromHandle = typeof(List<>);
						Type type2 = typeFromHandle.MakeGenericType(containedType);
						MethodInfo addMethod = type2.GetMethod("Add");
						object list = Activator.CreateInstance(type2);
						stream.List(GetName(item), delegate(int i, JSONInStream stream2)
						{
							object obj3 = DeserializeListElement(stream2, containedType);
							addMethod.Invoke(list, new object[1]
							{
								obj3
							});
						});
						item.SetValue(obj, list);
					}
					else if (item.FieldType.IsArray)
					{
						Type containedType2 = item.FieldType.GetElementType();
						Type typeFromHandle2 = typeof(List<>);
						Type type3 = typeFromHandle2.MakeGenericType(containedType2);
						MethodInfo addMethod2 = type3.GetMethod("Add");
						MethodInfo method2 = type3.GetMethod("ToArray");
						object list2 = Activator.CreateInstance(type3);
						stream.List(GetName(item), delegate(int i, JSONInStream stream2)
						{
							object obj2 = DeserializeListElement(stream2, containedType2);
							addMethod2.Invoke(list2, new object[1]
							{
								obj2
							});
						});
						object value2 = method2.Invoke(list2, new object[0]);
						item.SetValue(obj, value2);
					}
					else
					{
						stream.Start(GetName(item));
						object value3 = DeserializeObject(stream, item.FieldType);
						stream.End();
						item.SetValue(obj, value3);
					}
				}
				break;
			}
		}
		return obj;
	}

	private static object[] ToObjectArray(IEnumerable enumerableObject)
	{
		List<object> list = new List<object>();
		foreach (object item in enumerableObject)
		{
			list.Add(item);
		}
		return list.ToArray();
	}

	private static object DeserializeListElement(JSONInStream stream, Type type)
	{
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		if (!type.IsEnum)
		{
			switch (type.ToString())
			{
			case "System.String":
				stream.Content(0, out string value13);
				return value13;
			case "System.Single":
				stream.Content(0, out float value12);
				return value12;
			case "System.Double":
				stream.Content(0, out double value11);
				return value11;
			case "System.Int32":
				stream.Content(0, out int value10);
				return value10;
			case "System.Boolean":
				stream.Content(0, out bool value9);
				return value9;
			case "UnityEngine.Vector3":
				stream.Content(0, out Vector3 value8);
				return value8;
			case "UnityEngine.Quaternion":
				stream.Content(0, out Quaternion value7);
				return value7;
			case "UnityEngine.Color":
				stream.Content(0, out Color value6);
				return value6;
			case "UnityEngine.Rect":
				stream.Content(0, out Rect value5);
				return value5;
			case "UnityEngine.Vector2":
				stream.Content(0, out Vector2 value4);
				return value4;
			case "XorInt":
				stream.Content(0, out XorInt value3);
				return value3;
			case "XorUInt":
				stream.Content(0, out XorUInt value2);
				return value2;
			case "XorFloat":
				stream.Content(0, out XorFloat value);
				return value;
			default:
			{
				object result = DeserializeObject(stream, type);
				stream.End();
				return result;
			}
			}
		}
		stream.Content(0, out int value14);
		return Enum.Parse(type, value14.ToString());
	}
}
