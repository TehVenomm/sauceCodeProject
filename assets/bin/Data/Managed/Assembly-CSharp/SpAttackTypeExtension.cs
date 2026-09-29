// Decompiled with JetBrains decompiler
// Type: SpAttackTypeExtension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class SpAttackTypeExtension
{
  private const int ITEM_ICON_BG_ID_DEFAULT = 90000100;
  private const int ITEM_ICON_BG_ID_HEAT = 90000107;
  private const int ITEM_ICON_BG_ID_SOUL = 90000108;
  private const int ITEM_ICON_BG_ID_BURST = 90000109;
  private const int ITEM_ICON_BG_ID_ORACLE = 90000110;

  public static string GetName(this SP_ATTACK_TYPE type)
  {
    return StringTable.Get(STRING_CATEGORY.SP_ATTACK_TYPE, (uint) type);
  }

  public static string GetBigFrameSpriteName(this SP_ATTACK_TYPE type)
  {
    switch (type)
    {
      case SP_ATTACK_TYPE.HEAT:
        return "SpAttackType_Frame_heat";
      case SP_ATTACK_TYPE.SOUL:
        return "SpAttackType_Frame_soul";
      case SP_ATTACK_TYPE.BURST:
        return "SpAttackType_Frame_burst";
      case SP_ATTACK_TYPE.ORACLE:
        return "SpAttackType_Frame_oracle";
      default:
        return "SpAttackType_Frame_none";
    }
  }

  public static string GetSmallFrameSpriteName(this SP_ATTACK_TYPE type)
  {
    switch (type)
    {
      case SP_ATTACK_TYPE.HEAT:
        return "SpAttackType_smallFrame_heat";
      case SP_ATTACK_TYPE.SOUL:
        return "SpAttackType_smallFrame_soul";
      case SP_ATTACK_TYPE.BURST:
        return "SpAttackType_smallFrame_burst";
      case SP_ATTACK_TYPE.ORACLE:
        return "SpAttackType_smallFrame_oracle";
      default:
        return "SpAttackType_smallFrame_none";
    }
  }

  public static string GetSpTypeTextSpriteName(this SP_ATTACK_TYPE type)
  {
    switch (type)
    {
      case SP_ATTACK_TYPE.HEAT:
        return "EquipRemodelingTxt_04";
      case SP_ATTACK_TYPE.SOUL:
        return "EquipRemodelingTxt_05";
      case SP_ATTACK_TYPE.BURST:
        return "EquipRemodelingTxt_06";
      case SP_ATTACK_TYPE.ORACLE:
        return "EquipRemodelingTxt_07";
      default:
        return "EquipRemodelingTxt_03";
    }
  }

  public static int GetItemIconBGId(this SP_ATTACK_TYPE type)
  {
    switch (type)
    {
      case SP_ATTACK_TYPE.HEAT:
        return 90000107;
      case SP_ATTACK_TYPE.SOUL:
        return 90000108;
      case SP_ATTACK_TYPE.BURST:
        return 90000109;
      case SP_ATTACK_TYPE.ORACLE:
        return 90000110;
      default:
        return 90000100;
    }
  }
}
