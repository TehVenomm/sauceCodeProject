// Decompiled with JetBrains decompiler
// Type: ItemIconTypeExtension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class ItemIconTypeExtension
{
  public static bool IsEquip(this ITEM_ICON_TYPE type)
  {
    switch (type)
    {
      case ITEM_ICON_TYPE.ONE_HAND_SWORD:
      case ITEM_ICON_TYPE.TWO_HAND_SWORD:
      case ITEM_ICON_TYPE.SPEAR:
      case ITEM_ICON_TYPE.PAIR_SWORDS:
      case ITEM_ICON_TYPE.ARROW:
      case ITEM_ICON_TYPE.ARMOR:
      case ITEM_ICON_TYPE.HELM:
      case ITEM_ICON_TYPE.ARM:
      case ITEM_ICON_TYPE.LEG:
        return true;
      default:
        return false;
    }
  }
}
