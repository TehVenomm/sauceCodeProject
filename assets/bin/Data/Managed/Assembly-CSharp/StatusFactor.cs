// Decompiled with JetBrains decompiler
// Type: StatusFactor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class StatusFactor
{
  public SimpleStatus baseStatus = new SimpleStatus();
  public float hpRate;
  public float[] atkRate = new float[7];
  public float[] defRate = new float[7];
  public float[] tolRate = new float[6];
  public int constHp;
  public int[] constAtks = new int[7];
  public int[] constDefs = new int[7];
  public int[] constTols = new int[6];

  public void Reset()
  {
    this.baseStatus.Reset();
    this.hpRate = 1f;
    this.constHp = 0;
    for (int index = 0; index < 7; ++index)
    {
      this.atkRate[index] = 1f;
      this.defRate[index] = 1f;
      this.constAtks[index] = 0;
      this.constDefs[index] = 0;
    }
    for (int index = 0; index < 6; ++index)
    {
      this.tolRate[index] = 1f;
      this.constTols[index] = 0;
    }
  }

  public void CheckMinusRate()
  {
    if ((double) this.hpRate < 0.0)
      this.hpRate = 0.0f;
    for (int index = 0; index < 7; ++index)
    {
      if ((double) this.atkRate[index] < 0.0)
        this.atkRate[index] = 0.0f;
      if ((double) this.defRate[index] < 0.0)
        this.defRate[index] = 0.0f;
    }
    for (int index = 0; index < 6; ++index)
    {
      if ((double) this.tolRate[index] < 0.0)
        this.tolRate[index] = 0.0f;
    }
  }
}
