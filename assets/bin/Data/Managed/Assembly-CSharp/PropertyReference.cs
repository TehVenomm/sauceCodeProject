// Decompiled with JetBrains decompiler
// Type: PropertyReference
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

#nullable disable
[Serializable]
public class PropertyReference
{
  [SerializeField]
  private Component mTarget;
  [SerializeField]
  private string mName;
  private FieldInfo mField;
  private PropertyInfo mProperty;
  private static int s_Hash = "PropertyBinding".GetHashCode();

  public Component target
  {
    get => this.mTarget;
    set
    {
      this.mTarget = value;
      this.mProperty = (PropertyInfo) null;
      this.mField = (FieldInfo) null;
    }
  }

  public string name
  {
    get => this.mName;
    set
    {
      this.mName = value;
      this.mProperty = (PropertyInfo) null;
      this.mField = (FieldInfo) null;
    }
  }

  public bool isValid
  {
    get
    {
      return Object.op_Inequality((Object) this.mTarget, (Object) null) && !string.IsNullOrEmpty(this.mName);
    }
  }

  public bool isEnabled
  {
    get
    {
      if (Object.op_Equality((Object) this.mTarget, (Object) null))
        return false;
      MonoBehaviour mTarget = this.mTarget as MonoBehaviour;
      return Object.op_Equality((Object) mTarget, (Object) null) || ((Behaviour) mTarget).enabled;
    }
  }

  public PropertyReference()
  {
  }

  public PropertyReference(Component target, string fieldName)
  {
    this.mTarget = target;
    this.mName = fieldName;
  }

  public System.Type GetPropertyType()
  {
    if (this.mProperty == (PropertyInfo) null && this.mField == (FieldInfo) null && this.isValid)
      this.Cache();
    if (this.mProperty != (PropertyInfo) null)
      return this.mProperty.PropertyType;
    return this.mField != (FieldInfo) null ? this.mField.FieldType : typeof (void);
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return !this.isValid;
    if (!(obj is PropertyReference))
      return false;
    PropertyReference propertyReference = obj as PropertyReference;
    return Object.op_Equality((Object) this.mTarget, (Object) propertyReference.mTarget) && string.Equals(this.mName, propertyReference.mName);
  }

  public override int GetHashCode() => PropertyReference.s_Hash;

  public void Set(Component target, string methodName)
  {
    this.mTarget = target;
    this.mName = methodName;
  }

  public void Clear()
  {
    this.mTarget = (Component) null;
    this.mName = (string) null;
  }

  public void Reset()
  {
    this.mField = (FieldInfo) null;
    this.mProperty = (PropertyInfo) null;
  }

  public override string ToString() => PropertyReference.ToString(this.mTarget, this.name);

  public static string ToString(Component comp, string property)
  {
    if (!Object.op_Inequality((Object) comp, (Object) null))
      return (string) null;
    string str = comp.GetType().ToString();
    int num = str.LastIndexOf('.');
    if (num > 0)
      str = str.Substring(num + 1);
    return !string.IsNullOrEmpty(property) ? $"{str}.{property}" : str + ".[property]";
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public object Get()
  {
    if (this.mProperty == (PropertyInfo) null && this.mField == (FieldInfo) null && this.isValid)
      this.Cache();
    if (this.mProperty != (PropertyInfo) null)
    {
      if (this.mProperty.CanRead)
        return this.mProperty.GetValue((object) this.mTarget, (object[]) null);
    }
    else if (this.mField != (FieldInfo) null)
      return this.mField.GetValue((object) this.mTarget);
    return (object) null;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  public bool Set(object value)
  {
    if (this.mProperty == (PropertyInfo) null && this.mField == (FieldInfo) null && this.isValid)
      this.Cache();
    if (this.mProperty == (PropertyInfo) null && this.mField == (FieldInfo) null)
      return false;
    if (value == null)
    {
      try
      {
        if (this.mProperty != (PropertyInfo) null)
        {
          if (this.mProperty.CanWrite)
          {
            this.mProperty.SetValue((object) this.mTarget, (object) null, (object[]) null);
            return true;
          }
        }
        else
        {
          this.mField.SetValue((object) this.mTarget, (object) null);
          return true;
        }
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    if (!this.Convert(ref value))
    {
      if (Application.isPlaying)
        Debug.LogError((object) $"Unable to convert {(object) value.GetType()} to {(object) this.GetPropertyType()}");
    }
    else
    {
      if (this.mField != (FieldInfo) null)
      {
        this.mField.SetValue((object) this.mTarget, value);
        return true;
      }
      if (this.mProperty.CanWrite)
      {
        this.mProperty.SetValue((object) this.mTarget, value, (object[]) null);
        return true;
      }
    }
    return false;
  }

  [DebuggerHidden]
  [DebuggerStepThrough]
  private bool Cache()
  {
    if (Object.op_Inequality((Object) this.mTarget, (Object) null) && !string.IsNullOrEmpty(this.mName))
    {
      System.Type type = this.mTarget.GetType();
      this.mField = type.GetField(this.mName);
      this.mProperty = type.GetProperty(this.mName);
    }
    else
    {
      this.mField = (FieldInfo) null;
      this.mProperty = (PropertyInfo) null;
    }
    return this.mField != (FieldInfo) null || this.mProperty != (PropertyInfo) null;
  }

  private bool Convert(ref object value)
  {
    if (Object.op_Equality((Object) this.mTarget, (Object) null))
      return false;
    System.Type propertyType = this.GetPropertyType();
    System.Type from;
    if (value == null)
    {
      if (!propertyType.IsClass)
        return false;
      from = propertyType;
    }
    else
      from = value.GetType();
    return PropertyReference.Convert(ref value, from, propertyType);
  }

  public static bool Convert(System.Type from, System.Type to)
  {
    object obj = (object) null;
    return PropertyReference.Convert(ref obj, from, to);
  }

  public static bool Convert(object value, System.Type to)
  {
    if (value != null)
      return PropertyReference.Convert(ref value, value.GetType(), to);
    value = (object) null;
    return PropertyReference.Convert(ref value, to, to);
  }

  public static bool Convert(ref object value, System.Type from, System.Type to)
  {
    if (to.IsAssignableFrom(from))
      return true;
    if (to == typeof (string))
    {
      value = value != null ? (object) value.ToString() : (object) "null";
      return true;
    }
    if (value == null)
      return false;
    if (to == typeof (int))
    {
      if (from == typeof (string))
      {
        int result;
        if (int.TryParse((string) value, out result))
        {
          value = (object) result;
          return true;
        }
      }
      else if (from == typeof (float))
      {
        value = (object) Mathf.RoundToInt((float) value);
        return true;
      }
    }
    else
    {
      float result;
      if (to == typeof (float) && from == typeof (string) && float.TryParse((string) value, out result))
      {
        value = (object) result;
        return true;
      }
    }
    return false;
  }
}
