using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class XMLSerializer
{
	public static T Deserialize<T>(string message) where T : new()
	{
		XMLInStream stream = new XMLInStream(message);
		return (T)DeserializeObject(stream, typeof(T));
	}

	public static string Serialize<T>(T message)
	{
		XMLOutStream xMLOutStream = new XMLOutStream();
		xMLOutStream.Start("object");
		SerializeObject(xMLOutStream, typeof(T), message);
		xMLOutStream.End();
		return xMLOutStream.Serialize();
	}

	private static void SerializeObject(XMLOutStream stream, Type type, object message)
	{
		FieldInfo[] fields = type.GetFields();
		FieldInfo[] array = fields;
		foreach (FieldInfo fieldInfo in array)
		{
			switch (fieldInfo.FieldType.ToString())
			{
			case "System.String":
				stream.Content(fieldInfo.Name, (string)fieldInfo.GetValue(message));
				break;
			case "System.Single":
				stream.Content(fieldInfo.Name, (float)fieldInfo.GetValue(message));
				break;
			case "System.Int32":
				stream.Content(fieldInfo.Name, (int)fieldInfo.GetValue(message));
				break;
			case "System.Boolean":
				stream.Content(fieldInfo.Name, (bool)fieldInfo.GetValue(message));
				break;
			case "UnityEngine.Vector3":
				stream.Content(fieldInfo.Name, (Vector3)fieldInfo.GetValue(message));
				break;
			case "UnityEngine.Quaternion":
				stream.Content(fieldInfo.Name, (Quaternion)fieldInfo.GetValue(message));
				break;
			case "UnityEngine.Color":
				stream.Content(fieldInfo.Name, (Color)fieldInfo.GetValue(message));
				break;
			case "UnityEngine.Rect":
				stream.Content(fieldInfo.Name, (Rect)fieldInfo.GetValue(message));
				break;
			case "UnityEngine.Vector2":
				stream.Content(fieldInfo.Name, (Vector2)fieldInfo.GetValue(message));
				break;
			default:
				if (fieldInfo.FieldType.IsEnum)
				{
					stream.Content(fieldInfo.Name, fieldInfo.GetValue(message).ToString());
				}
				else if (fieldInfo.FieldType.IsGenericType)
				{
					Type type2 = fieldInfo.FieldType.GetGenericArguments()[0];
					Type typeFromHandle = typeof(List<>);
					Type type3 = typeFromHandle.MakeGenericType(type2);
					PropertyInfo property = type3.GetProperty("Count");
					PropertyInfo property2 = type3.GetProperty("Item");
					int num = (int)property.GetValue(fieldInfo.GetValue(message), new object[0]);
					stream.Start(fieldInfo.Name);
					for (int j = 0; j < num; j++)
					{
						object value = property2.GetValue(fieldInfo.GetValue(message), new object[1]
						{
							j
						});
						SerializeListElement(stream, type2, value, j);
					}
					stream.End();
				}
				else if (fieldInfo.FieldType.IsArray)
				{
					object[] array2 = ToObjectArray((IEnumerable)fieldInfo.GetValue(message));
					Type type4 = Type.GetTypeArray(array2)[0];
					stream.Start(fieldInfo.Name);
					for (int k = 0; k < array2.Length; k++)
					{
						object message2 = array2[k];
						SerializeListElement(stream, type4, message2, k);
					}
					stream.End();
				}
				else
				{
					stream.Start(fieldInfo.Name);
					SerializeObject(stream, fieldInfo.FieldType, fieldInfo.GetValue(message));
					stream.End();
				}
				break;
			}
		}
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

	private static void SerializeListElement(XMLOutStream stream, Type type, object message, int i)
	{
		switch (type.ToString())
		{
		case "System.String":
			stream.Content("item", (string)message);
			break;
		case "System.Single":
			stream.Content("item", (float)message);
			break;
		case "System.Int32":
			stream.Content("item", (int)message);
			break;
		case "System.Boolean":
			stream.Content("item", (bool)message);
			break;
		case "UnityEngine.Vector3":
			stream.Content("item", (Vector3)message);
			break;
		case "UnityEngine.Quaternion":
			stream.Content("item", (Quaternion)message);
			break;
		case "UnityEngine.Color":
			stream.Content("item", (Color)message);
			break;
		case "UnityEngine.Rect":
			stream.Content("item", (Rect)message);
			break;
		case "UnityEngine.Vector2":
			stream.Content("item", (Vector2)message);
			break;
		default:
			stream.Start("item");
			SerializeObject(stream, type, message);
			stream.End();
			break;
		}
	}

	private static object DeserializeObject(XMLInStream stream, Type type)
	{
		object obj = Activator.CreateInstance(type);
		FieldInfo[] fields = type.GetFields();
		FieldInfo[] array = fields;
		Type containedType;
		MethodInfo addMethod;
		object list;
		Type containedType2;
		MethodInfo addMethod2;
		object list2;
		foreach (FieldInfo fieldInfo in array)
		{
			switch (fieldInfo.FieldType.ToString())
			{
			case "System.String":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out string value12);
					fieldInfo.SetValue(obj, value12);
				}
				break;
			case "System.Single":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out float value7);
					fieldInfo.SetValue(obj, value7);
				}
				break;
			case "System.Int32":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out int value9);
					fieldInfo.SetValue(obj, value9);
				}
				break;
			case "System.Boolean":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out bool value5);
					fieldInfo.SetValue(obj, value5);
				}
				break;
			case "UnityEngine.Vector3":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Vector3 value10);
					fieldInfo.SetValue(obj, value10);
				}
				break;
			case "UnityEngine.Quaternion":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Quaternion value8);
					fieldInfo.SetValue(obj, value8);
				}
				break;
			case "UnityEngine.Color":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Color value6);
					fieldInfo.SetValue(obj, value6);
				}
				break;
			case "UnityEngine.Rect":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Rect value4);
					fieldInfo.SetValue(obj, value4);
				}
				break;
			case "UnityEngine.Vector2":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Vector2 value11);
					fieldInfo.SetValue(obj, value11);
				}
				break;
			default:
				if (stream.Has(fieldInfo.Name))
				{
					if (fieldInfo.FieldType.IsEnum)
					{
						stream.Content(fieldInfo.Name, out string value);
						fieldInfo.SetValue(obj, Enum.Parse(fieldInfo.FieldType, value));
					}
					else if (fieldInfo.FieldType.IsGenericType)
					{
						containedType = fieldInfo.FieldType.GetGenericArguments()[0];
						Type typeFromHandle = typeof(List<>);
						Type type2 = typeFromHandle.MakeGenericType(containedType);
						addMethod = type2.GetMethod("Add");
						list = Activator.CreateInstance(type2);
						stream.Start(fieldInfo.Name).List("item", delegate(XMLInStream stream2)
						{
							object obj3 = DeserializeListElement(stream2, containedType);
							addMethod.Invoke(list, new object[1]
							{
								obj3
							});
						}).End();
						fieldInfo.SetValue(obj, list);
					}
					else if (fieldInfo.FieldType.IsArray)
					{
						containedType2 = fieldInfo.FieldType.GetElementType();
						Type typeFromHandle2 = typeof(List<>);
						Type type3 = typeFromHandle2.MakeGenericType(containedType2);
						addMethod2 = type3.GetMethod("Add");
						MethodInfo method = type3.GetMethod("ToArray");
						list2 = Activator.CreateInstance(type3);
						stream.Start(fieldInfo.Name).List("item", delegate(XMLInStream stream2)
						{
							object obj2 = DeserializeListElement(stream2, containedType2);
							addMethod2.Invoke(list2, new object[1]
							{
								obj2
							});
						}).End();
						object value2 = method.Invoke(list2, new object[0]);
						fieldInfo.SetValue(obj, value2);
					}
					else
					{
						stream.Start(fieldInfo.Name);
						object value3 = DeserializeObject(stream, fieldInfo.FieldType);
						stream.End();
						fieldInfo.SetValue(obj, value3);
					}
				}
				break;
			}
		}
		return obj;
	}

	private static object DeserializeListElement(XMLInStream stream, Type type)
	{
		switch (type.ToString())
		{
		case "System.String":
			stream.Content(out string value9);
			return value9;
		case "System.Single":
			stream.Content(out float value8);
			return value8;
		case "System.Int32":
			stream.Content(out int value7);
			return value7;
		case "System.Boolean":
			stream.Content(out bool value6);
			return value6;
		case "UnityEngine.Vector3":
			stream.Content(out Vector3 value5);
			return value5;
		case "UnityEngine.Quaternion":
			stream.Content(out Quaternion value4);
			return value4;
		case "UnityEngine.Color":
			stream.Content(out Color value3);
			return value3;
		case "UnityEngine.Rect":
			stream.Content(out Rect value2);
			return value2;
		case "UnityEngine.Vector2":
			stream.Content(out Vector2 value);
			return value;
		default:
			return DeserializeObject(stream, type);
		}
	}
}
