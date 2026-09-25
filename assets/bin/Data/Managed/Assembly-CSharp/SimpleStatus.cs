// Decompiled with JetBrains decompiler
// Type: SimpleStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SimpleStatus
{
  public int hp;
  public int[] attacks = new int[7];
  public int[] defences = new int[7];
  public int[] tolerances = new int[6];

  public SimpleStatus() => this.Reset();

  public void Reset()
  {
    this.hp = 0;
    for (int index = 0; index < 7; ++index)
    {
      this.attacks[index] = 0;
      this.defences[index] = 0;
    }
    for (int index = 0; index < 6; ++index)
      this.tolerances[index] = 0;
  }

  public int GetAttacksSum()
  {
    int attacksSum = 0;
    for (int index = 0; index < 7; ++index)
      attacksSum += this.attacks[index];
    return attacksSum;
  }

  public int GetDefencesSum()
  {
    int defencesSum = 0 + this.defences[0];
    for (int index = 0; index < 6; ++index)
      defencesSum += this.tolerances[index];
    return defencesSum;
  }
}
