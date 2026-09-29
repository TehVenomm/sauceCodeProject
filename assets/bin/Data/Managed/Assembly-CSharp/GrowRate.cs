// Decompiled with JetBrains decompiler
// Type: GrowRate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GrowRate
{
  public XorInt rate = (XorInt) 0;
  public XorInt add = (XorInt) 0;

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    GrowRate growRate = obj as GrowRate;
    return obj != null && this.rate.value == growRate.rate.value && this.add.value == growRate.add.value;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override string ToString() => $"rate:{(object) this.rate}, add:{(object) this.add}";
}
