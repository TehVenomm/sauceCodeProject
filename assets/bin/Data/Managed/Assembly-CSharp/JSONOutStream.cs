// Decompiled with JetBrains decompiler
// Type: JSONOutStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class JSONOutStream
{
  public JSONNode node = new JSONNode();

  public string Serialize() => this.node.Serialize();

  public JSONOutStream Content(string tag, string value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONStringFieldValue(value));
    return this;
  }

  public JSONOutStream Content(int idx, string value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONStringFieldValue(value));
    return this;
  }

  public JSONOutStream Content(string value)
  {
    this.node.AddField((IJSONFieldValue) new JSONStringFieldValue(value));
    return this;
  }

  public JSONOutStream Content(string tag, double value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONNumberFieldValue(value));
    return this;
  }

  public JSONOutStream Content(int idx, double value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONNumberFieldValue(value));
    return this;
  }

  public JSONOutStream Content(double value)
  {
    this.node.AddField((IJSONFieldValue) new JSONNumberFieldValue(value));
    return this;
  }

  public JSONOutStream Content(string tag, bool value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONBooleanFieldValue(value));
    return this;
  }

  public JSONOutStream Content(int idx, bool value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONBooleanFieldValue(value));
    return this;
  }

  public JSONOutStream Content(bool value)
  {
    this.node.AddField((IJSONFieldValue) new JSONBooleanFieldValue(value));
    return this;
  }

  public JSONOutStream Content(string tag, int value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONNumberFieldValue((double) value));
    return this;
  }

  public JSONOutStream Content(int idx, int value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONNumberFieldValue((double) value));
    return this;
  }

  public JSONOutStream Content(int value)
  {
    this.node.AddField((IJSONFieldValue) new JSONNumberFieldValue((double) value));
    return this;
  }

  public JSONOutStream Content(string tag, XorInt value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONNumberFieldValue((double) (int) value));
    return this;
  }

  public JSONOutStream Content(int idx, XorInt value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONNumberFieldValue((double) (int) value));
    return this;
  }

  public JSONOutStream Content(XorInt value)
  {
    this.node.AddField((IJSONFieldValue) new JSONNumberFieldValue((double) (int) value));
    return this;
  }

  public JSONOutStream Content(string tag, XorUInt value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONNumberFieldValue((double) (uint) value));
    return this;
  }

  public JSONOutStream Content(int idx, XorUInt value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONNumberFieldValue((double) (uint) value));
    return this;
  }

  public JSONOutStream Content(XorUInt value)
  {
    this.node.AddField((IJSONFieldValue) new JSONNumberFieldValue((double) (uint) value));
    return this;
  }

  public JSONOutStream Content(string tag, XorFloat value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONNumberFieldValue((double) (float) value));
    return this;
  }

  public JSONOutStream Content(int idx, XorFloat value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONNumberFieldValue((double) (float) value));
    return this;
  }

  public JSONOutStream Content(XorFloat value)
  {
    this.node.AddField((IJSONFieldValue) new JSONNumberFieldValue((double) (float) value));
    return this;
  }

  public JSONOutStream Content(string tag, Vector2 value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Vector2 value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y)
    }));
    return this;
  }

  public JSONOutStream Content(Vector2 value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y)
    }));
    return this;
  }

  public JSONOutStream Content(string tag, Vector3 value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Vector3 value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z)
    }));
    return this;
  }

  public JSONOutStream Content(Vector3 value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z)
    }));
    return this;
  }

  public JSONOutStream Content(string tag, Vector4 value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Vector4 value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(Vector4 value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(string tag, Quaternion value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Quaternion value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(Quaternion value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.z),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.w)
    }));
    return this;
  }

  public JSONOutStream Content(string tag, Color value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.r),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.g),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.b),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.a)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Color value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.r),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.g),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.b),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.a)
    }));
    return this;
  }

  public JSONOutStream Content(Color value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.r),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.g),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.b),
      (IJSONFieldValue) new JSONNumberFieldValue((double) value.a)
    }));
    return this;
  }

  public JSONOutStream Content(string tag, Rect value)
  {
    this.node.AddField(tag, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).width),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).height)
    }));
    return this;
  }

  public JSONOutStream Content(int idx, Rect value)
  {
    this.node.AddField(idx, (IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).width),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).height)
    }));
    return this;
  }

  public JSONOutStream Content(Rect value)
  {
    this.node.AddField((IJSONFieldValue) new JSONListFieldValue(new System.Collections.Generic.List<IJSONFieldValue>()
    {
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).x),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).y),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).width),
      (IJSONFieldValue) new JSONNumberFieldValue((double) ((Rect) ref value).height)
    }));
    return this;
  }

  public JSONOutStream List(string tag)
  {
    this.node = new JSONNode(this.node)
    {
      isList = true,
      listName = tag
    };
    return this;
  }

  public JSONOutStream List()
  {
    this.node = new JSONNode(this.node)
    {
      isList = true,
      listName = (string) null
    };
    return this;
  }

  public JSONOutStream Start(string tag)
  {
    JSONNode val = new JSONNode(this.node);
    this.node.AddField(tag, (IJSONFieldValue) new JSONObjectFieldValue(val));
    this.node = val;
    return this;
  }

  public JSONOutStream Start(int idx)
  {
    JSONNode val = new JSONNode(this.node);
    this.node.AddField(idx, (IJSONFieldValue) new JSONObjectFieldValue(val));
    this.node = val;
    return this;
  }

  public JSONOutStream Start()
  {
    JSONNode val = new JSONNode(this.node);
    this.node.AddField((IJSONFieldValue) new JSONObjectFieldValue(val));
    this.node = val;
    return this;
  }

  public JSONOutStream End()
  {
    if (this.node.parent != null)
    {
      if (this.node.isList && this.node.listName != null)
        this.node.parent.AddField(this.node.listName, (IJSONFieldValue) this.node.GetListFieldValue());
      else if (this.node.isList)
        this.node.parent.AddField((IJSONFieldValue) this.node.GetListFieldValue());
      this.node = this.node.parent;
    }
    return this;
  }
}
