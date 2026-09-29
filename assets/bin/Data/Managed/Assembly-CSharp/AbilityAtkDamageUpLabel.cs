// Decompiled with JetBrains decompiler
// Type: AbilityAtkDamageUpLabel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkDamageUpLabel : AbilityAtkBase
{
  public string label;

  public override void init(Player _player, string target, int val)
  {
    base.init(_player, target, val);
    this.label = target;
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    if (status.attackInfo.damageUpLabels != null)
    {
      int index = 0;
      for (int length = status.attackInfo.damageUpLabels.Length; index < length; ++index)
      {
        if (this.label == status.attackInfo.damageUpLabels[index])
          return base.GetDamageRate(chara, status);
      }
    }
    return (AtkAttribute) null;
  }
}
