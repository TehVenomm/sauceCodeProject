// Decompiled with JetBrains decompiler
// Type: ItemIconMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconMaterial : ItemIcon
{
  public UILabel lblHave;
  public UILabel lblNeed;
  public UISprite baseBG;

  public static ItemIcon CreateMaterialIcon(
    ITEM_ICON_TYPE icon_type,
    ItemTable.ItemData item_table,
    Transform parent = null,
    int have_num = -1,
    int need_num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false)
  {
    ItemIconMaterial icon = ItemIcon.CreateIcon<ItemIconMaterial>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconMaterialPrefab, icon_type, item_table.iconID, new RARITY_TYPE?(item_table.rarity), parent, event_name: event_name, event_data: event_data, is_new: is_new, enemy_icon_id: item_table.enemyIconID, enemy_icon_id2: item_table.enemyIconID2);
    icon.SetMaterialNum(have_num, need_num);
    icon.SetVisibleBG(true);
    return (ItemIcon) icon;
  }

  public void SetMaterialNum(int have_num, int need_num)
  {
    UIBehaviour.SetMaterialNumText(((Component) this.lblHave).transform, ((Component) this.lblNeed).transform, have_num, need_num);
  }

  public void SetVisibleBG(bool is_visible) => ((Behaviour) this.baseBG).enabled = is_visible;
}
