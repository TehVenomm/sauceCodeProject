// Decompiled with JetBrains decompiler
// Type: BestHTTP.Extensions.KeyValuePair
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.Extensions;

public sealed class KeyValuePair
{
  public string Key { get; set; }

  public string Value { get; set; }

  public KeyValuePair(string key) => this.Key = key;

  public override string ToString()
  {
    return !string.IsNullOrEmpty(this.Value) ? this.Key + (object) '=' + this.Value : this.Key;
  }
}
