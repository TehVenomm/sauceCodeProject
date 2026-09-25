// Decompiled with JetBrains decompiler
// Type: JSONSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
public static class JSONSerializer
{
  public static T Deserialize<T>(string message) where T : new()
  {
    return (T) JSONSerializer.DeserializeObject(new JSONInStream(message), typeof (T));
  }

  public static T Deserialize<T>(string message, System.Type type) where T : new()
  {
    return (T) JSONSerializer.DeserializeObject(new JSONInStream(message), type);
  }

  public static string Serialize<T>(T message)
  {
    JSONOutStream stream = new JSONOutStream();
    JSONSerializer.SerializeObject(stream, typeof (T), (object) message);
    return stream.Serialize();
  }

  public static string Serialize(object message, System.Type type)
  {
    JSONOutStream stream = new JSONOutStream();
    JSONSerializer.SerializeObject(stream, type, message);
    return stream.Serialize();
  }

  private static string GetName(FieldInfo fi)
  {
    return !(((IEnumerable<object>) fi.GetCustomAttributes(typeof (FormerlySerializedAsAttribute), false)).FirstOrDefault<object>() is FormerlySerializedAsAttribute serializedAsAttribute) ? fi.Name : serializedAsAttribute.oldName;
  }

  private static IEnumerable<FieldInfo> GetTargetFields(System.Type type)
  {
    FieldInfo[] fieldInfoArray = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    for (int index = 0; index < fieldInfoArray.Length; ++index)
    {
      FieldInfo targetField = fieldInfoArray[index];
      if (!targetField.IsPublic)
      {
        string str = targetField.FieldType.ToString();
        if (str != "XorInt" && str != "XorUInt" && str != "XorFloat")
          continue;
      }
      yield return targetField;
    }
    fieldInfoArray = (FieldInfo[]) null;
  }

  private static void SerializeObject(JSONOutStream stream, System.Type type, object message)
  {
    MethodInfo method = type.GetMethod("ToJSON");
    if (method != (MethodInfo) null)
    {
      method.Invoke(message, new object[1]
      {
        (object) stream
      });
    }
    else
    {
      foreach (FieldInfo targetField in JSONSerializer.GetTargetFields(type))
      {
        switch (targetField.FieldType.ToString())
        {
          case "System.Boolean":
            stream.Content(JSONSerializer.GetName(targetField), (bool) targetField.GetValue(message));
            continue;
          case "System.Double":
            stream.Content(JSONSerializer.GetName(targetField), (double) targetField.GetValue(message));
            continue;
          case "System.Int32":
            stream.Content(JSONSerializer.GetName(targetField), (int) targetField.GetValue(message));
            continue;
          case "System.Single":
            stream.Content(JSONSerializer.GetName(targetField), (XorFloat) (float) targetField.GetValue(message));
            continue;
          case "System.String":
            stream.Content(JSONSerializer.GetName(targetField), (string) targetField.GetValue(message));
            continue;
          case "UnityEngine.Color":
            stream.Content(JSONSerializer.GetName(targetField), (Color) targetField.GetValue(message));
            continue;
          case "UnityEngine.Quaternion":
            stream.Content(JSONSerializer.GetName(targetField), (Quaternion) targetField.GetValue(message));
            continue;
          case "UnityEngine.Rect":
            stream.Content(JSONSerializer.GetName(targetField), (Rect) targetField.GetValue(message));
            continue;
          case "UnityEngine.Vector2":
            stream.Content(JSONSerializer.GetName(targetField), (Vector2) targetField.GetValue(message));
            continue;
          case "UnityEngine.Vector3":
            stream.Content(JSONSerializer.GetName(targetField), (Vector3) targetField.GetValue(message));
            continue;
          case "XorFloat":
            stream.Content(JSONSerializer.GetName(targetField), targetField.GetValue(message) as XorFloat);
            continue;
          case "XorInt":
            stream.Content(JSONSerializer.GetName(targetField), targetField.GetValue(message) as XorInt);
            continue;
          case "XorUInt":
            stream.Content(JSONSerializer.GetName(targetField), targetField.GetValue(message) as XorUInt);
            continue;
          default:
            if (targetField.FieldType.IsEnum)
            {
              stream.Content(JSONSerializer.GetName(targetField), targetField.GetValue(message).ToString());
              continue;
            }
            if (targetField.FieldType.IsGenericType)
            {
              System.Type genericArgument = targetField.FieldType.GetGenericArguments()[0];
              System.Type type1 = typeof (List<>).MakeGenericType(genericArgument);
              PropertyInfo property1 = type1.GetProperty("Count");
              PropertyInfo property2 = type1.GetProperty("Item");
              int num = (int) property1.GetValue(targetField.GetValue(message), new object[0]);
              stream.List(JSONSerializer.GetName(targetField));
              for (int i = 0; i < num; ++i)
              {
                object message1 = property2.GetValue(targetField.GetValue(message), new object[1]
                {
                  (object) i
                });
                JSONSerializer.SerializeListElement(stream, genericArgument, message1, i);
              }
              stream.End();
              continue;
            }
            if (targetField.FieldType.IsArray)
            {
              object[] objectArray = JSONSerializer.ToObjectArray((IEnumerable) targetField.GetValue(message));
              System.Type type2 = System.Type.GetTypeArray(objectArray)[0];
              stream.List(JSONSerializer.GetName(targetField));
              for (int i = 0; i < objectArray.Length; ++i)
              {
                object message2 = objectArray[i];
                JSONSerializer.SerializeListElement(stream, type2, message2, i);
              }
              stream.End();
              continue;
            }
            stream.Start(JSONSerializer.GetName(targetField));
            JSONSerializer.SerializeObject(stream, targetField.FieldType, targetField.GetValue(message));
            stream.End();
            continue;
        }
      }
    }
  }

  private static void SerializeListElement(JSONOutStream stream, System.Type type, object message, int i)
  {
    if (type.IsEnum)
    {
      stream.Content(i, (int) message);
    }
    else
    {
      switch (type.ToString())
      {
        case "System.Boolean":
          stream.Content(i, (bool) message);
          break;
        case "System.Double":
          stream.Content(i, (double) message);
          break;
        case "System.Int32":
          stream.Content(i, (int) message);
          break;
        case "System.Single":
          stream.Content(i, (XorFloat) (float) message);
          break;
        case "System.String":
          stream.Content(i, (string) message);
          break;
        case "UnityEngine.Color":
          stream.Content(i, (Color) message);
          break;
        case "UnityEngine.Quaternion":
          stream.Content(i, (Quaternion) message);
          break;
        case "UnityEngine.Rect":
          stream.Content(i, (Rect) message);
          break;
        case "UnityEngine.Vector2":
          stream.Content(i, (Vector2) message);
          break;
        case "UnityEngine.Vector3":
          stream.Content(i, (Vector3) message);
          break;
        case "XorFloat":
          stream.Content(i, new XorFloat((float) message));
          break;
        case "XorInt":
          stream.Content(i, new XorInt((int) message));
          break;
        case "XorUInt":
          stream.Content(i, new XorUInt((uint) message));
          break;
        default:
          stream.Start(i);
          JSONSerializer.SerializeObject(stream, type, message);
          stream.End();
          break;
      }
    }
  }

  private static object DeserializeObject(JSONInStream stream, System.Type type)
  {
    MethodInfo method1 = type.GetMethod("FromJSON");
    if (method1 != (MethodInfo) null)
      return method1.Invoke((object) null, new object[1]
      {
        (object) stream
      });
    object instance = Activator.CreateInstance(type);
    foreach (FieldInfo targetField in JSONSerializer.GetTargetFields(type))
    {
      switch (targetField.FieldType.ToString())
      {
        case "System.Boolean":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            bool flag;
            stream.Content(JSONSerializer.GetName(targetField), out flag);
            targetField.SetValue(instance, (object) flag);
            continue;
          }
          continue;
        case "System.Double":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            double num;
            stream.Content(JSONSerializer.GetName(targetField), out num);
            targetField.SetValue(instance, (object) num);
            continue;
          }
          continue;
        case "System.Int32":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            int num;
            stream.Content(JSONSerializer.GetName(targetField), out num);
            targetField.SetValue(instance, (object) num);
            continue;
          }
          continue;
        case "System.Single":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            float num;
            stream.Content(JSONSerializer.GetName(targetField), out num);
            targetField.SetValue(instance, (object) num);
            continue;
          }
          continue;
        case "System.String":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            string str;
            stream.Content(JSONSerializer.GetName(targetField), out str);
            targetField.SetValue(instance, (object) str);
            continue;
          }
          continue;
        case "UnityEngine.Color":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            Color color;
            stream.Content(JSONSerializer.GetName(targetField), out color);
            targetField.SetValue(instance, (object) color);
            continue;
          }
          continue;
        case "UnityEngine.Quaternion":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            Quaternion quaternion;
            stream.Content(JSONSerializer.GetName(targetField), out quaternion);
            targetField.SetValue(instance, (object) quaternion);
            continue;
          }
          continue;
        case "UnityEngine.Rect":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            Rect rect;
            stream.Content(JSONSerializer.GetName(targetField), out rect);
            targetField.SetValue(instance, (object) rect);
            continue;
          }
          continue;
        case "UnityEngine.Vector2":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            Vector2 vector2;
            stream.Content(JSONSerializer.GetName(targetField), out vector2);
            targetField.SetValue(instance, (object) vector2);
            continue;
          }
          continue;
        case "UnityEngine.Vector3":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            Vector3 vector3;
            stream.Content(JSONSerializer.GetName(targetField), out vector3);
            targetField.SetValue(instance, (object) vector3);
            continue;
          }
          continue;
        case "XorFloat":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            XorFloat xorFloat;
            stream.Content(JSONSerializer.GetName(targetField), out xorFloat);
            targetField.SetValue(instance, (object) xorFloat);
            continue;
          }
          continue;
        case "XorInt":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            XorInt xorInt;
            stream.Content(JSONSerializer.GetName(targetField), out xorInt);
            targetField.SetValue(instance, (object) xorInt);
            continue;
          }
          continue;
        case "XorUInt":
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            XorUInt xorUint;
            stream.Content(JSONSerializer.GetName(targetField), out xorUint);
            targetField.SetValue(instance, (object) xorUint);
            continue;
          }
          continue;
        default:
          if (stream.Has(JSONSerializer.GetName(targetField)))
          {
            if (targetField.FieldType.IsEnum)
            {
              string str;
              stream.Content(JSONSerializer.GetName(targetField), out str);
              targetField.SetValue(instance, Enum.Parse(targetField.FieldType, str));
              continue;
            }
            if (targetField.FieldType.IsGenericType)
            {
              System.Type containedType = targetField.FieldType.GetGenericArguments()[0];
              System.Type type1 = typeof (List<>).MakeGenericType(containedType);
              MethodInfo addMethod = type1.GetMethod("Add");
              object list = Activator.CreateInstance(type1);
              stream.List(JSONSerializer.GetName(targetField), (Action<int, JSONInStream>) ((i, stream2) =>
              {
                object obj = JSONSerializer.DeserializeListElement(stream2, containedType);
                addMethod.Invoke(list, new object[1]{ obj });
              }));
              targetField.SetValue(instance, list);
              continue;
            }
            if (targetField.FieldType.IsArray)
            {
              System.Type containedType = targetField.FieldType.GetElementType();
              System.Type type2 = typeof (List<>).MakeGenericType(containedType);
              MethodInfo addMethod = type2.GetMethod("Add");
              MethodInfo method2 = type2.GetMethod("ToArray");
              object list = Activator.CreateInstance(type2);
              stream.List(JSONSerializer.GetName(targetField), (Action<int, JSONInStream>) ((i, stream2) =>
              {
                object obj = JSONSerializer.DeserializeListElement(stream2, containedType);
                addMethod.Invoke(list, new object[1]{ obj });
              }));
              object obj1 = list;
              object[] parameters = new object[0];
              object obj2 = method2.Invoke(obj1, parameters);
              targetField.SetValue(instance, obj2);
              continue;
            }
            stream.Start(JSONSerializer.GetName(targetField));
            object obj3 = JSONSerializer.DeserializeObject(stream, targetField.FieldType);
            stream.End();
            targetField.SetValue(instance, obj3);
            continue;
          }
          continue;
      }
    }
    return instance;
  }

  private static object[] ToObjectArray(IEnumerable enumerableObject)
  {
    List<object> objectList = new List<object>();
    foreach (object obj in enumerableObject)
      objectList.Add(obj);
    return objectList.ToArray();
  }

  private static object DeserializeListElement(JSONInStream stream, System.Type type)
  {
    if (type.IsEnum)
    {
      int num;
      stream.Content(0, out num);
      return Enum.Parse(type, num.ToString());
    }
    switch (type.ToString())
    {
      case "System.Boolean":
        bool flag;
        stream.Content(0, out flag);
        return (object) flag;
      case "System.Double":
        double num1;
        stream.Content(0, out num1);
        return (object) num1;
      case "System.Int32":
        int num2;
        stream.Content(0, out num2);
        return (object) num2;
      case "System.Single":
        float num3;
        stream.Content(0, out num3);
        return (object) num3;
      case "System.String":
        string str;
        stream.Content(0, out str);
        return (object) str;
      case "UnityEngine.Color":
        Color color;
        stream.Content(0, out color);
        return (object) color;
      case "UnityEngine.Quaternion":
        Quaternion quaternion;
        stream.Content(0, out quaternion);
        return (object) quaternion;
      case "UnityEngine.Rect":
        Rect rect;
        stream.Content(0, out rect);
        return (object) rect;
      case "UnityEngine.Vector2":
        Vector2 vector2;
        stream.Content(0, out vector2);
        return (object) vector2;
      case "UnityEngine.Vector3":
        Vector3 vector3;
        stream.Content(0, out vector3);
        return (object) vector3;
      case "XorFloat":
        XorFloat xorFloat;
        stream.Content(0, out xorFloat);
        return (object) xorFloat;
      case "XorInt":
        XorInt xorInt;
        stream.Content(0, out xorInt);
        return (object) xorInt;
      case "XorUInt":
        XorUInt xorUint;
        stream.Content(0, out xorUint);
        return (object) xorUint;
      default:
        object obj = JSONSerializer.DeserializeObject(stream, type);
        stream.End();
        return obj;
    }
  }
}
