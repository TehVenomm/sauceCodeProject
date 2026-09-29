// Decompiled with JetBrains decompiler
// Type: AbilityAtkBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkBase
{
  protected Player player;
  protected AtkAttribute attr = new AtkAttribute();

  public virtual void init(Player _player, string target, int val)
  {
    this.player = _player;
    this.attr.normal = (float) val * 0.01f;
    this.attr.fire = (float) val * 0.01f;
    this.attr.water = (float) val * 0.01f;
    this.attr.thunder = (float) val * 0.01f;
    this.attr.soil = (float) val * 0.01f;
    this.attr.light = (float) val * 0.01f;
    this.attr.dark = (float) val * 0.01f;
  }

  public virtual AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    return this.attr;
  }
}
