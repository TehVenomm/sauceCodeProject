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
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		FieldInfo[] fields = type.GetFields();
		FieldInfo[] array = fields;
		foreach (FieldInfo fieldInfo in array)
		{
			switch (fieldInfo.FieldType.ToString())
			{
			case "System.String":
				stream.Content(fieldInfo.Name, (string)fieldInfo.GetValue(message));
				continue;
			case "System.Single":
				stream.Content(fieldInfo.Name, (float)fieldInfo.GetValue(message));
				continue;
			case "System.Int32":
				stream.Content(fieldInfo.Name, (int)fieldInfo.GetValue(message));
				continue;
			case "System.Boolean":
				stream.Content(fieldInfo.Name, (bool)fieldInfo.GetValue(message));
				continue;
			case "UnityEngine.Vector3":
				stream.Content(fieldInfo.Name, (Vector3)fieldInfo.GetValue(message));
				continue;
			case "UnityEngine.Quaternion":
				stream.Content(fieldInfo.Name, (Quaternion)fieldInfo.GetValue(message));
				continue;
			case "UnityEngine.Color":
				stream.Content(fieldInfo.Name, (Color)fieldInfo.GetValue(message));
				continue;
			case "UnityEngine.Rect":
				stream.Content(fieldInfo.Name, (Rect)fieldInfo.GetValue(message));
				continue;
			case "UnityEngine.Vector2":
				stream.Content(fieldInfo.Name, (Vector2)fieldInfo.GetValue(message));
				continue;
			}
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
		}
	}

	private static object[] ToObjectArray(IEnumerable enumerableObject)
	{
		List<object> list = new List<object>();
		IEnumerator enumerator = enumerableObject.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				list.Add(current);
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
		return list.ToArray();
	}

	private static void SerializeListElement(XMLOutStream stream, Type type, object message, int i)
	{
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		switch (type.ToString())
		{
		case "System.String":
			stream.Content("item", (string)message);
			return;
		case "System.Single":
			stream.Content("item", (float)message);
			return;
		case "System.Int32":
			stream.Content("item", (int)message);
			return;
		case "System.Boolean":
			stream.Content("item", (bool)message);
			return;
		case "UnityEngine.Vector3":
			stream.Content("item", (Vector3)message);
			return;
		case "UnityEngine.Quaternion":
			stream.Content("item", (Quaternion)message);
			return;
		case "UnityEngine.Color":
			stream.Content("item", (Color)message);
			return;
		case "UnityEngine.Rect":
			stream.Content("item", (Rect)message);
			return;
		case "UnityEngine.Vector2":
			stream.Content("item", (Vector2)message);
			return;
		}
		stream.Start("item");
		SerializeObject(stream, type, message);
		stream.End();
	}

	private static object DeserializeObject(XMLInStream stream, Type type)
	{
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
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
					stream.Content(fieldInfo.Name, out string value9);
					fieldInfo.SetValue(obj, value9);
				}
				continue;
			case "System.Single":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out float value4);
					fieldInfo.SetValue(obj, value4);
				}
				continue;
			case "System.Int32":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out int value6);
					fieldInfo.SetValue(obj, value6);
				}
				continue;
			case "System.Boolean":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out bool value2);
					fieldInfo.SetValue(obj, value2);
				}
				continue;
			case "UnityEngine.Vector3":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Vector3 value7);
					fieldInfo.SetValue(obj, value7);
				}
				continue;
			case "UnityEngine.Quaternion":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Quaternion value5);
					fieldInfo.SetValue(obj, value5);
				}
				continue;
			case "UnityEngine.Color":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Color value3);
					fieldInfo.SetValue(obj, value3);
				}
				continue;
			case "UnityEngine.Rect":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Rect value);
					fieldInfo.SetValue(obj, value);
				}
				continue;
			case "UnityEngine.Vector2":
				if (stream.Has(fieldInfo.Name))
				{
					stream.Content(fieldInfo.Name, out Vector2 value8);
					fieldInfo.SetValue(obj, value8);
				}
				continue;
			}
			if (stream.Has(fieldInfo.Name))
			{
				if (fieldInfo.FieldType.IsEnum)
				{
					stream.Content(fieldInfo.Name, out string value10);
					fieldInfo.SetValue(obj, Enum.Parse(fieldInfo.FieldType, value10));
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
					object value11 = method.Invoke(list2, new object[0]);
					fieldInfo.SetValue(obj, value11);
				}
				else
				{
					stream.Start(fieldInfo.Name);
					object value12 = DeserializeObject(stream, fieldInfo.FieldType);
					stream.End();
					fieldInfo.SetValue(obj, value12);
				}
			}
		}
		return obj;
	}

	private static object DeserializeListElement(XMLInStream stream, Type type)
	{
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
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
