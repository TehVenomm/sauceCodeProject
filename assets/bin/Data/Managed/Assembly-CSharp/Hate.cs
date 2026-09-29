// Decompiled with JetBrains decompiler
// Type: Hate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Hate
{
  public int[] val = new int[7];
  public int turnVal;
  public int cycleLockCount;
  public int totalLockCount;
  public int continuousLockCount;

  public int CalcTotalHate(HateParam param)
  {
    float num = 0.0f;
    for (int index = 0; index < 7; ++index)
      num += (float) this.val[index] * param.categoryParam[index].importance;
    return (int) num;
  }

  public override string ToString()
  {
    return $"{(object) this.val}/{(object) this.turnVal}[{(object) this.cycleLockCount}/{(object) this.totalLockCount}]";
  }

  public enum TYPE
  {
    Distance,
    LifeLowner,
    Damage,
    Heal,
    Skill,
    SpecialDamage,
    Guard,
    MAX_NUM,
  }
}
