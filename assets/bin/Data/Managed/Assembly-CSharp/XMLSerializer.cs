// Decompiled with JetBrains decompiler
// Type: XMLSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
public static class XMLSerializer
{
  public static T Deserialize<T>(string message) where T : new()
  {
    return (T) XMLSerializer.DeserializeObject(new XMLInStream(message), typeof (T));
  }

  public static string Serialize<T>(T message)
  {
    XMLOutStream stream = new XMLOutStream();
    stream.Start("object");
    XMLSerializer.SerializeObject(stream, typeof (T), (object) message);
    stream.End();
    return stream.Serialize();
  }

  private static void SerializeObject(XMLOutStream stream, System.Type type, object message)
  {
    foreach (FieldInfo field in type.GetFields())
    {
      switch (field.FieldType.ToString())
      {
        case "System.Boolean":
          stream.Content(field.Name, (bool) field.GetValue(message));
          break;
        case "System.Int32":
          stream.Content(field.Name, (int) field.GetValue(message));
          break;
        case "System.Single":
          stream.Content(field.Name, (float) field.GetValue(message));
          break;
        case "System.String":
          stream.Content(field.Name, (string) field.GetValue(message));
          break;
        case "UnityEngine.Color":
          stream.Content(field.Name, (Color) field.GetValue(message));
          break;
        case "UnityEngine.Quaternion":
          stream.Content(field.Name, (Quaternion) field.GetValue(message));
          break;
        case "UnityEngine.Rect":
          stream.Content(field.Name, (Rect) field.GetValue(message));
          break;
        case "UnityEngine.Vector2":
          stream.Content(field.Name, (Vector2) field.GetValue(message));
          break;
        case "UnityEngine.Vector3":
          stream.Content(field.Name, (Vector3) field.GetValue(message));
          break;
        default:
          if (field.FieldType.IsEnum)
          {
            stream.Content(field.Name, field.GetValue(message).ToString());
            break;
          }
          if (field.FieldType.IsGenericType)
          {
            System.Type genericArgument = field.FieldType.GetGenericArguments()[0];
            System.Type type1 = typeof (List<>).MakeGenericType(genericArgument);
            PropertyInfo property1 = type1.GetProperty("Count");
            PropertyInfo property2 = type1.GetProperty("Item");
            int num = (int) property1.GetValue(field.GetValue(message), new object[0]);
            stream.Start(field.Name);
            for (int i = 0; i < num; ++i)
            {
              object message1 = property2.GetValue(field.GetValue(message), new object[1]
              {
                (object) i
              });
              XMLSerializer.SerializeListElement(stream, genericArgument, message1, i);
            }
            stream.End();
            break;
          }
          if (field.FieldType.IsArray)
          {
            object[] objectArray = XMLSerializer.ToObjectArray((IEnumerable) field.GetValue(message));
            System.Type type2 = System.Type.GetTypeArray(objectArray)[0];
            stream.Start(field.Name);
            for (int i = 0; i < objectArray.Length; ++i)
            {
              object message2 = objectArray[i];
              XMLSerializer.SerializeListElement(stream, type2, message2, i);
            }
            stream.End();
            break;
          }
          stream.Start(field.Name);
          XMLSerializer.SerializeObject(stream, field.FieldType, field.GetValue(message));
          stream.End();
          break;
      }
    }
  }

  private static object[] ToObjectArray(IEnumerable enumerableObject)
  {
    List<object> objectList = new List<object>();
    foreach (object obj in enumerableObject)
      objectList.Add(obj);
    return objectList.ToArray();
  }

  private static void SerializeListElement(XMLOutStream stream, System.Type type, object message, int i)
  {
    switch (type.ToString())
    {
      case "System.Boolean":
        stream.Content("item", (bool) message);
        break;
      case "System.Int32":
        stream.Content("item", (int) message);
        break;
      case "System.Single":
        stream.Content("item", (float) message);
        break;
      case "System.String":
        stream.Content("item", (string) message);
        break;
      case "UnityEngine.Color":
        stream.Content("item", (Color) message);
        break;
      case "UnityEngine.Quaternion":
        stream.Content("item", (Quaternion) message);
        break;
      case "UnityEngine.Rect":
        stream.Content("item", (Rect) message);
        break;
      case "UnityEngine.Vector2":
        stream.Content("item", (Vector2) message);
        break;
      case "UnityEngine.Vector3":
        stream.Content("item", (Vector3) message);
        break;
      default:
        stream.Start("item");
        XMLSerializer.SerializeObject(stream, type, message);
        stream.End();
        break;
    }
  }

  private static object DeserializeObject(XMLInStream stream, System.Type type)
  {
    object instance = Activator.CreateInstance(type);
    foreach (FieldInfo field in type.GetFields())
    {
      switch (field.FieldType.ToString())
      {
        case "System.Boolean":
          if (stream.Has(field.Name))
          {
            bool flag;
            stream.Content(field.Name, out flag);
            field.SetValue(instance, (object) flag);
            break;
          }
          break;
        case "System.Int32":
          if (stream.Has(field.Name))
          {
            int num;
            stream.Content(field.Name, out num);
            field.SetValue(instance, (object) num);
            break;
          }
          break;
        case "System.Single":
          if (stream.Has(field.Name))
          {
            float num;
            stream.Content(field.Name, out num);
            field.SetValue(instance, (object) num);
            break;
          }
          break;
        case "System.String":
          if (stream.Has(field.Name))
          {
            string str;
            stream.Content(field.Name, out str);
            field.SetValue(instance, (object) str);
            break;
          }
          break;
        case "UnityEngine.Color":
          if (stream.Has(field.Name))
          {
            Color color;
            stream.Content(field.Name, out color);
            field.SetValue(instance, (object) color);
            break;
          }
          break;
        case "UnityEngine.Quaternion":
          if (stream.Has(field.Name))
          {
            Quaternion quaternion;
            stream.Content(field.Name, out quaternion);
            field.SetValue(instance, (object) quaternion);
            break;
          }
          break;
        case "UnityEngine.Rect":
          if (stream.Has(field.Name))
          {
            Rect rect;
            stream.Content(field.Name, out rect);
            field.SetValue(instance, (object) rect);
            break;
          }
          break;
        case "UnityEngine.Vector2":
          if (stream.Has(field.Name))
          {
            Vector2 vector2;
            stream.Content(field.Name, out vector2);
            field.SetValue(instance, (object) vector2);
            break;
          }
          break;
        case "UnityEngine.Vector3":
          if (stream.Has(field.Name))
          {
            Vector3 vector3;
            stream.Content(field.Name, out vector3);
            field.SetValue(instance, (object) vector3);
            break;
          }
          break;
        default:
          if (stream.Has(field.Name))
          {
            if (field.FieldType.IsEnum)
            {
              string str;
              stream.Content(field.Name, out str);
              field.SetValue(instance, Enum.Parse(field.FieldType, str));
              break;
            }
            if (field.FieldType.IsGenericType)
            {
              System.Type containedType = field.FieldType.GetGenericArguments()[0];
              System.Type type1 = typeof (List<>).MakeGenericType(containedType);
              MethodInfo addMethod = type1.GetMethod("Add");
              object list = Activator.CreateInstance(type1);
              stream.Start(field.Name).List("item", (Action<XMLInStream>) (stream2 =>
              {
                object obj = XMLSerializer.DeserializeListElement(stream2, containedType);
                addMethod.Invoke(list, new object[1]{ obj });
              })).End();
              field.SetValue(instance, list);
              break;
            }
            if (field.FieldType.IsArray)
            {
              System.Type containedType = field.FieldType.GetElementType();
              System.Type type2 = typeof (List<>).MakeGenericType(containedType);
              MethodInfo addMethod = type2.GetMethod("Add");
              MethodInfo method = type2.GetMethod("ToArray");
              object list = Activator.CreateInstance(type2);
              stream.Start(field.Name).List("item", (Action<XMLInStream>) (stream2 =>
              {
                object obj = XMLSerializer.DeserializeListElement(stream2, containedType);
                addMethod.Invoke(list, new object[1]{ obj });
              })).End();
              object obj1 = list;
              object[] parameters = new object[0];
              object obj2 = method.Invoke(obj1, parameters);
              field.SetValue(instance, obj2);
              break;
            }
            stream.Start(field.Name);
            object obj3 = XMLSerializer.DeserializeObject(stream, field.FieldType);
            stream.End();
            field.SetValue(instance, obj3);
            break;
          }
          break;
      }
    }
    return instance;
  }

  private static object DeserializeListElement(XMLInStream stream, System.Type type)
  {
    switch (type.ToString())
    {
      case "System.Boolean":
        bool flag;
        stream.Content(out flag);
        return (object) flag;
      case "System.Int32":
        int num1;
        stream.Content(out num1);
        return (object) num1;
      case "System.Single":
        float num2;
        stream.Content(out num2);
        return (object) num2;
      case "System.String":
        string str;
        stream.Content(out str);
        return (object) str;
      case "UnityEngine.Color":
        Color color;
        stream.Content(out color);
        return (object) color;
      case "UnityEngine.Quaternion":
        Quaternion quaternion;
        stream.Content(out quaternion);
        return (object) quaternion;
      case "UnityEngine.Rect":
        Rect rect;
        stream.Content(out rect);
        return (object) rect;
      case "UnityEngine.Vector2":
        Vector2 vector2;
        stream.Content(out vector2);
        return (object) vector2;
      case "UnityEngine.Vector3":
        Vector3 vector3;
        stream.Content(out vector3);
        return (object) vector3;
      default:
        return XMLSerializer.DeserializeObject(stream, type);
    }
  }
}
