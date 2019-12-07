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
		return (T)DeserializeObject(new JSONInStream(message), typeof(T));
	}

	public static T Deserialize<T>(string message, Type type) where T : new()
	{
		return (T)DeserializeObject(new JSONInStream(message), type);
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
		FormerlySerializedAsAttribute formerlySerializedAsAttribute = fi.GetCustomAttributes(typeof(FormerlySerializedAsAttribute), inherit: false).FirstOrDefault() as FormerlySerializedAsAttribute;
		if (formerlySerializedAsAttribute == null)
		{
			return fi.Name;
		}
		return formerlySerializedAsAttribute.oldName;
	}

	private static IEnumerable<FieldInfo> GetTargetFields(Type type)
	{
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (!fieldInfo.IsPublic)
			{
				string a = fieldInfo.FieldType.ToString();
				if (a != "XorInt" && a != "XorUInt" && a != "XorFloat")
				{
					continue;
				}
			}
			yield return fieldInfo;
		}
	}

	private static void SerializeObject(JSONOutStream stream, Type type, object message)
	{
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
			foreach (FieldInfo targetField in GetTargetFields(type))
			{
				switch (targetField.FieldType.ToString())
				{
				case "System.String":
					stream.Content(GetName(targetField), (string)targetField.GetValue(message));
					break;
				case "System.Single":
					stream.Content(GetName(targetField), (float)targetField.GetValue(message));
					break;
				case "System.Double":
					stream.Content(GetName(targetField), (double)targetField.GetValue(message));
					break;
				case "System.Int32":
					stream.Content(GetName(targetField), (int)targetField.GetValue(message));
					break;
				case "System.Boolean":
					stream.Content(GetName(targetField), (bool)targetField.GetValue(message));
					break;
				case "UnityEngine.Vector3":
					stream.Content(GetName(targetField), (Vector3)targetField.GetValue(message));
					break;
				case "UnityEngine.Quaternion":
					stream.Content(GetName(targetField), (Quaternion)targetField.GetValue(message));
					break;
				case "UnityEngine.Color":
					stream.Content(GetName(targetField), (Color)targetField.GetValue(message));
					break;
				case "UnityEngine.Rect":
					stream.Content(GetName(targetField), (Rect)targetField.GetValue(message));
					break;
				case "UnityEngine.Vector2":
					stream.Content(GetName(targetField), (Vector2)targetField.GetValue(message));
					break;
				case "XorInt":
					stream.Content(GetName(targetField), targetField.GetValue(message) as XorInt);
					break;
				case "XorUInt":
					stream.Content(GetName(targetField), targetField.GetValue(message) as XorUInt);
					break;
				case "XorFloat":
					stream.Content(GetName(targetField), targetField.GetValue(message) as XorFloat);
					break;
				default:
					if (targetField.FieldType.IsEnum)
					{
						stream.Content(GetName(targetField), targetField.GetValue(message).ToString());
					}
					else if (targetField.FieldType.IsGenericType)
					{
						Type type2 = targetField.FieldType.GetGenericArguments()[0];
						Type type3 = typeof(List<>).MakeGenericType(type2);
						PropertyInfo property = type3.GetProperty("Count");
						PropertyInfo property2 = type3.GetProperty("Item");
						int num = (int)property.GetValue(targetField.GetValue(message), new object[0]);
						stream.List(GetName(targetField));
						for (int i = 0; i < num; i++)
						{
							object value = property2.GetValue(targetField.GetValue(message), new object[1]
							{
								i
							});
							SerializeListElement(stream, type2, value, i);
						}
						stream.End();
					}
					else if (targetField.FieldType.IsArray)
					{
						object[] array = ToObjectArray((IEnumerable)targetField.GetValue(message));
						Type type4 = Type.GetTypeArray(array)[0];
						stream.List(GetName(targetField));
						for (int j = 0; j < array.Length; j++)
						{
							object message2 = array[j];
							SerializeListElement(stream, type4, message2, j);
						}
						stream.End();
					}
					else
					{
						stream.Start(GetName(targetField));
						SerializeObject(stream, targetField.FieldType, targetField.GetValue(message));
						stream.End();
					}
					break;
				}
			}
		}
	}

	private static void SerializeListElement(JSONOutStream stream, Type type, object message, int i)
	{
		if (type.IsEnum)
		{
			stream.Content(i, (int)message);
			return;
		}
		switch (type.ToString())
		{
		case "System.String":
			stream.Content(i, (string)message);
			return;
		case "System.Single":
			stream.Content(i, (float)message);
			return;
		case "System.Double":
			stream.Content(i, (double)message);
			return;
		case "System.Int32":
			stream.Content(i, (int)message);
			return;
		case "System.Boolean":
			stream.Content(i, (bool)message);
			return;
		case "UnityEngine.Vector3":
			stream.Content(i, (Vector3)message);
			return;
		case "UnityEngine.Quaternion":
			stream.Content(i, (Quaternion)message);
			return;
		case "UnityEngine.Color":
			stream.Content(i, (Color)message);
			return;
		case "UnityEngine.Rect":
			stream.Content(i, (Rect)message);
			return;
		case "UnityEngine.Vector2":
			stream.Content(i, (Vector2)message);
			return;
		case "XorInt":
			stream.Content(i, new XorInt((int)message));
			return;
		case "XorUInt":
			stream.Content(i, new XorUInt((uint)message));
			return;
		case "XorFloat":
			stream.Content(i, new XorFloat((float)message));
			return;
		}
		stream.Start(i);
		SerializeObject(stream, type, message);
		stream.End();
	}

	private static object DeserializeObject(JSONInStream stream, Type type)
	{
		MethodInfo method = type.GetMethod("FromJSON");
		if (method != null)
		{
			return method.Invoke(null, new object[1]
			{
				stream
			});
		}
		object obj = Activator.CreateInstance(type);
		foreach (FieldInfo targetField in GetTargetFields(type))
		{
			switch (targetField.FieldType.ToString())
			{
			case "System.String":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out string value7);
					targetField.SetValue(obj, value7);
				}
				break;
			case "System.Single":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out float value12);
					targetField.SetValue(obj, value12);
				}
				break;
			case "System.Double":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out double value16);
					targetField.SetValue(obj, value16);
				}
				break;
			case "System.Int32":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out int value9);
					targetField.SetValue(obj, value9);
				}
				break;
			case "System.Boolean":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out bool value5);
					targetField.SetValue(obj, value5);
				}
				break;
			case "UnityEngine.Vector3":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out Vector3 value14);
					targetField.SetValue(obj, value14);
				}
				break;
			case "UnityEngine.Quaternion":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out Quaternion value10);
					targetField.SetValue(obj, value10);
				}
				break;
			case "UnityEngine.Color":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out Color value8);
					targetField.SetValue(obj, value8);
				}
				break;
			case "UnityEngine.Rect":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out Rect value6);
					targetField.SetValue(obj, value6);
				}
				break;
			case "UnityEngine.Vector2":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out Vector2 value4);
					targetField.SetValue(obj, value4);
				}
				break;
			case "XorInt":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out XorInt value15);
					targetField.SetValue(obj, value15);
				}
				break;
			case "XorUInt":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out XorUInt value13);
					targetField.SetValue(obj, value13);
				}
				break;
			case "XorFloat":
				if (stream.Has(GetName(targetField)))
				{
					stream.Content(GetName(targetField), out XorFloat value11);
					targetField.SetValue(obj, value11);
				}
				break;
			default:
				if (stream.Has(GetName(targetField)))
				{
					if (targetField.FieldType.IsEnum)
					{
						stream.Content(GetName(targetField), out string value);
						targetField.SetValue(obj, Enum.Parse(targetField.FieldType, value));
					}
					else if (targetField.FieldType.IsGenericType)
					{
						Type containedType2 = targetField.FieldType.GetGenericArguments()[0];
						Type type2 = typeof(List<>).MakeGenericType(containedType2);
						MethodInfo addMethod2 = type2.GetMethod("Add");
						object list2 = Activator.CreateInstance(type2);
						stream.List(GetName(targetField), delegate(int i, JSONInStream stream2)
						{
							object obj3 = DeserializeListElement(stream2, containedType2);
							addMethod2.Invoke(list2, new object[1]
							{
								obj3
							});
						});
						targetField.SetValue(obj, list2);
					}
					else if (targetField.FieldType.IsArray)
					{
						Type containedType = targetField.FieldType.GetElementType();
						Type type3 = typeof(List<>).MakeGenericType(containedType);
						MethodInfo addMethod = type3.GetMethod("Add");
						MethodInfo method2 = type3.GetMethod("ToArray");
						object list = Activator.CreateInstance(type3);
						stream.List(GetName(targetField), delegate(int i, JSONInStream stream2)
						{
							object obj2 = DeserializeListElement(stream2, containedType);
							addMethod.Invoke(list, new object[1]
							{
								obj2
							});
						});
						object value2 = method2.Invoke(list, new object[0]);
						targetField.SetValue(obj, value2);
					}
					else
					{
						stream.Start(GetName(targetField));
						object value3 = DeserializeObject(stream, targetField.FieldType);
						stream.End();
						targetField.SetValue(obj, value3);
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
		if (type.IsEnum)
		{
			stream.Content(0, out int value);
			return Enum.Parse(type, value.ToString());
		}
		switch (type.ToString())
		{
		case "System.String":
		{
			stream.Content(0, out string value14);
			return value14;
		}
		case "System.Single":
		{
			stream.Content(0, out float value13);
			return value13;
		}
		case "System.Double":
		{
			stream.Content(0, out double value12);
			return value12;
		}
		case "System.Int32":
		{
			stream.Content(0, out int value11);
			return value11;
		}
		case "System.Boolean":
		{
			stream.Content(0, out bool value10);
			return value10;
		}
		case "UnityEngine.Vector3":
		{
			stream.Content(0, out Vector3 value9);
			return value9;
		}
		case "UnityEngine.Quaternion":
		{
			stream.Content(0, out Quaternion value8);
			return value8;
		}
		case "UnityEngine.Color":
		{
			stream.Content(0, out Color value7);
			return value7;
		}
		case "UnityEngine.Rect":
		{
			stream.Content(0, out Rect value6);
			return value6;
		}
		case "UnityEngine.Vector2":
		{
			stream.Content(0, out Vector2 value5);
			return value5;
		}
		case "XorInt":
		{
			stream.Content(0, out XorInt value4);
			return value4;
		}
		case "XorUInt":
		{
			stream.Content(0, out XorUInt value3);
			return value3;
		}
		case "XorFloat":
		{
			stream.Content(0, out XorFloat value2);
			return value2;
		}
		default:
		{
			object result = DeserializeObject(stream, type);
			stream.End();
			return result;
		}
		}
	}
}
