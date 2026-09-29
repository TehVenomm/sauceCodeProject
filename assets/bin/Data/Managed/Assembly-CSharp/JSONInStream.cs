// Decompiled with JetBrains decompiler
// Type: JSONInStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class JSONInStream
{
  public JSONNode node;

  public JSONInStream(string input)
  {
    this.node = new JSONParser().Parse(new FlashCompatibleTextReader(input));
  }

  public JSONInStream(JSONNode node) => this.node = node;

  public bool Has(string tag) => this.node.GetField(tag) != null;

  public JSONInStream Content(string tag, out string value)
  {
    try
    {
      JSONStringFieldValue field = (JSONStringFieldValue) this.node.GetField(tag);
      value = field.value;
    }
    catch (Exception ex1)
    {
      try
      {
        JSONNullFieldValue field = (JSONNullFieldValue) this.node.GetField(tag);
        value = (string) null;
      }
      catch (Exception ex2)
      {
        Debug.LogError((object) $"Error JSONInStream {tag} {ex1.ToString()} {ex2.ToString()}");
        value = (string) null;
        return (JSONInStream) null;
      }
    }
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref string value)
  {
    try
    {
      JSONStringFieldValue field = (JSONStringFieldValue) this.node.GetField(tag);
      if (field == null)
        return this;
      value = field.value;
    }
    catch (Exception ex1)
    {
      try
      {
        JSONNullFieldValue field = (JSONNullFieldValue) this.node.GetField(tag);
        value = (string) null;
      }
      catch (Exception ex2)
      {
        Debug.LogError((object) $"Error JSONInStream {tag} {ex1.ToString()} {ex2.ToString()}");
        return (JSONInStream) null;
      }
    }
    return this;
  }

  public JSONInStream Content(out string value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out string value)
  {
    try
    {
      JSONStringFieldValue field = (JSONStringFieldValue) this.node.GetField(idx);
      value = field.value;
    }
    catch (Exception ex1)
    {
      try
      {
        JSONNullFieldValue field = (JSONNullFieldValue) this.node.GetField(idx);
        value = (string) null;
      }
      catch (Exception ex2)
      {
        Debug.LogError((object) $"Error JSONInStream {(object) idx} {ex1.ToString()} {ex2.ToString()}");
        value = (string) null;
        return (JSONInStream) null;
      }
    }
    return this;
  }

  public JSONInStream Content(string tag, out float value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = (float) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out float value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out float value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = (float) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out double value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out double value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out double value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = numberFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref double value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (numberFieldValue != null)
      value = numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out int value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out int value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out int value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref int value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (numberFieldValue != null)
      value = (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out bool value)
  {
    JSONBooleanFieldValue booleanFieldValue = (JSONBooleanFieldValue) null;
    try
    {
      booleanFieldValue = (JSONBooleanFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = booleanFieldValue.value;
    return this;
  }

  public JSONInStream Content(out bool value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out bool value)
  {
    JSONBooleanFieldValue booleanFieldValue = (JSONBooleanFieldValue) null;
    try
    {
      booleanFieldValue = (JSONBooleanFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = booleanFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref bool value)
  {
    JSONBooleanFieldValue booleanFieldValue = (JSONBooleanFieldValue) null;
    try
    {
      booleanFieldValue = (JSONBooleanFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (booleanFieldValue != null)
      value = booleanFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out Vector2 value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector2.zero;
      return this;
    }
    float[] fs = new float[2];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector2(fs[0], fs[1]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Vector2 value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector2.zero;
      return this;
    }
    float[] fs = new float[2];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector2(fs[0], fs[1]);
    return this;
  }

  public JSONInStream Content(out Vector2 value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Vector2 value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector2.zero;
      return this;
    }
    float[] fs = new float[2];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector2(fs[0], fs[1]);
    return this;
  }

  public JSONInStream Content(string tag, out Vector3 value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector3.zero;
      return this;
    }
    float[] fs = new float[3];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector3(fs[0], fs[1], fs[2]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Vector3 value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector3.zero;
      return this;
    }
    float[] fs = new float[3];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector3(fs[0], fs[1], fs[2]);
    return this;
  }

  public JSONInStream Content(out Vector3 value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Vector3 value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector3.zero;
      return this;
    }
    float[] fs = new float[3];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector3(fs[0], fs[1], fs[2]);
    return this;
  }

  public JSONInStream Content(string tag, out Vector4 value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector4.zero;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Vector4 value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector4.zero;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(out Vector4 value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Vector4 value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = Vector4.zero;
      return this;
    }
    float[] fs = new float[4];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(string tag, out Quaternion value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = Quaternion.identity;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Quaternion value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = Quaternion.identity;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(out Quaternion value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Quaternion value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = Quaternion.identity;
      return this;
    }
    float[] fs = new float[4];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(string tag, out Color value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = Color.white;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Color(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Color value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = Color.white;
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Color(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(out Color value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Color value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = Color.white;
      return this;
    }
    float[] fs = new float[4];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Color(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(string tag, out Rect value)
  {
    if (this.node.GetField(tag).GetType() == typeof (JSONNullFieldValue))
    {
      value = new Rect();
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Rect(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref Rect value)
  {
    IJSONFieldValue field = this.node.GetField(tag);
    if (field == null)
      return this;
    if (field.GetType() == typeof (JSONNullFieldValue))
    {
      value = new Rect();
      return this;
    }
    float[] fs = new float[4];
    this.List(tag, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Rect(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(out Rect value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out Rect value)
  {
    if (this.node.GetField(idx).GetType() == typeof (JSONNullFieldValue))
    {
      value = new Rect();
      return this;
    }
    float[] fs = new float[4];
    this.List(idx, (Action<int, JSONInStream>) ((i, stream) =>
    {
      float num;
      stream.Content(out num);
      fs[i] = num;
    }));
    value = new Rect(fs[0], fs[1], fs[2], fs[3]);
    return this;
  }

  public JSONInStream Content(string tag, out XorInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = (XorInt) (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out XorInt value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out XorInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = (XorInt) (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref XorInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (numberFieldValue != null)
      value = (XorInt) (int) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out XorUInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = (XorUInt) (uint) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out XorUInt value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out XorUInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = (XorUInt) (uint) numberFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref XorUInt value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (numberFieldValue != null)
      value = (XorUInt) (uint) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(string tag, out XorFloat value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    value = (XorFloat) (float) numberFieldValue.value;
    return this;
  }

  public JSONInStream Content(out XorFloat value) => this.Content(0, out value);

  public JSONInStream Content(int idx, out XorFloat value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(idx);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) idx);
    }
    value = (XorFloat) (float) numberFieldValue.value;
    return this;
  }

  public JSONInStream ContentOptional(string tag, ref XorFloat value)
  {
    JSONNumberFieldValue numberFieldValue = (JSONNumberFieldValue) null;
    try
    {
      numberFieldValue = (JSONNumberFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    if (numberFieldValue != null)
      value = (XorFloat) (float) numberFieldValue.value;
    return this;
  }

  public JSONInStream List(string tag, Action<int, JSONInStream> callback)
  {
    JSONListFieldValue jsonListFieldValue = (JSONListFieldValue) null;
    try
    {
      jsonListFieldValue = (JSONListFieldValue) this.node.GetField(tag);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    int num = 0;
    foreach (IJSONFieldValue val in jsonListFieldValue.value)
    {
      JSONInStream jsonInStream = new JSONInStream(new JSONNode(val));
      try
      {
        if ((JSONObjectFieldValue) val != null)
          jsonInStream = jsonInStream.Start(0);
      }
      catch
      {
      }
      callback(num++, jsonInStream);
    }
    return this;
  }

  public JSONInStream List(int idx, Action<int, JSONInStream> callback)
  {
    JSONListFieldValue field = (JSONListFieldValue) this.node.GetField(idx);
    int num = 0;
    foreach (IJSONFieldValue val in field.value)
    {
      JSONInStream jsonInStream = new JSONInStream(new JSONNode(val));
      try
      {
        if ((JSONObjectFieldValue) val != null)
          jsonInStream = jsonInStream.Start(0);
      }
      catch
      {
      }
      callback(num++, jsonInStream);
    }
    return this;
  }

  public JSONInStream Start(string tag)
  {
    try
    {
      JSONObjectFieldValue field = (JSONObjectFieldValue) this.node.GetField(tag);
      field.value.parent = this.node;
      this.node = field.value;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      Debug.LogError((object) tag);
    }
    return this;
  }

  public JSONInStream Start(int idx)
  {
    JSONObjectFieldValue field = (JSONObjectFieldValue) this.node.GetField(idx);
    field.value.parent = this.node;
    this.node = field.value;
    return this;
  }

  public JSONInStream End()
  {
    if (this.node.parent != null)
      this.node = this.node.parent;
    return this;
  }

  public int Count => this.node.GetFieldCount();
}
