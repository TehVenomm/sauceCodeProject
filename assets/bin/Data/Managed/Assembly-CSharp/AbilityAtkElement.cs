// Decompiled with JetBrains decompiler
// Type: AbilityAtkElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AbilityAtkElement : AbilityAtkBase
{
  public override void init(Player _player, string target, int val)
  {
    switch ((ELEMENT_TYPE) Enum.Parse(typeof (ELEMENT_TYPE), target))
    {
      case ELEMENT_TYPE.FIRE:
        this.attr.fire = (float) val * 0.01f;
        break;
      case ELEMENT_TYPE.WATER:
        this.attr.water = (float) val * 0.01f;
        break;
      case ELEMENT_TYPE.THUNDER:
        this.attr.thunder = (float) val * 0.01f;
        break;
      case ELEMENT_TYPE.SOIL:
        this.attr.soil = (float) val * 0.01f;
        break;
      case ELEMENT_TYPE.LIGHT:
        this.attr.light = (float) val * 0.01f;
        break;
      case ELEMENT_TYPE.DARK:
        this.attr.dark = (float) val * 0.01f;
        break;
    }
  }
}
