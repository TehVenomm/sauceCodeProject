// Decompiled with JetBrains decompiler
// Type: gogame.JSONObjectEnumer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;

#nullable disable
namespace gogame;

public class JSONObjectEnumer : IEnumerator
{
  public JSONObject _jobj;
  private int position = -1;

  public JSONObjectEnumer(JSONObject jsonObject) => this._jobj = jsonObject;

  public JSONObject Current
  {
    get
    {
      return this._jobj.IsArray ? this._jobj[this.position] : this._jobj[this._jobj.keys[this.position]];
    }
  }

  public bool MoveNext()
  {
    ++this.position;
    return this.position < this._jobj.Count;
  }

  public void Reset() => this.position = -1;

  object IEnumerator.Current => (object) this.Current;
}
