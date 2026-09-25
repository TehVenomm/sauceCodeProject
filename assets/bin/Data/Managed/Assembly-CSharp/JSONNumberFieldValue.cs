// Decompiled with JetBrains decompiler
// Type: JSONNumberFieldValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class JSONNumberFieldValue : IJSONFieldValue
{
  public double value;

  public JSONNumberFieldValue(double val) => this.value = val;

  public string Serialize() => this.value.ToString();
}
