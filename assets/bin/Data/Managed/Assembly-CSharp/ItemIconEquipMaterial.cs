// Decompiled with JetBrains decompiler
// Type: ItemIconEquipMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconEquipMaterial : ItemIcon
{
  public UILabel strUnselect;
  public UILabel lblHave;
  public UISprite baseBG;
  public UILabel lblLv;
  public UISprite spriteValueType;
  public UILabel lblText;
  public ulong selectedUniqueID;

  public static ItemIconEquipMaterial CreateEquipMaterialIcon(
    ITEM_ICON_TYPE icon_type,
    EquipItemTable.EquipItemData equip_table,
    Transform parent = null,
    int have_num = -1,
    int need_num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    ItemIconEquipMaterial icon = ItemIcon.CreateIcon<ItemIconEquipMaterial>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconEquipMaterialPrefab, icon_type, equip_table.GetIconID(), new RARITY_TYPE?(equip_table.rarity), parent, event_name: event_name, event_data: event_data, is_new: is_new, getType: getType);
    icon.EquipTypeIconInit(equip_table);
    icon.SetMaterialNum(have_num);
    icon.SetVisibleBG(true);
    icon.SelectUniqueID(0UL);
    return icon;
  }

  public void SetMaterialNum(int have_num) => this.lblHave.text = have_num.ToString();

  public void SetVisibleBG(bool is_visible) => ((Behaviour) this.baseBG).enabled = is_visible;

  public void EquipTypeIconInit(EquipItemTable.EquipItemData equip_table = null)
  {
    if (equip_table == null)
    {
      ((Behaviour) this.spriteValueType).enabled = false;
    }
    else
    {
      ((Behaviour) this.spriteValueType).enabled = true;
      this.spriteValueType.spriteName = equip_table.IsWeapon() ? ItemIcon.SPR_TYPE_ATK : ItemIcon.SPR_TYPE_DEF;
    }
  }

  public void SelectUniqueID(ulong id)
  {
    this.selectedUniqueID = id;
    if (this.selectedUniqueID == 0UL)
    {
      ((Component) this.strUnselect).gameObject.SetActive(true);
      ((Component) ((Component) this.lblHave).transform.parent).gameObject.SetActive(true);
      ((Component) ((Component) this.lblLv).transform.parent).gameObject.SetActive(false);
      ((Component) this.spriteValueType).gameObject.SetActive(false);
      this.lblText.text = "0";
      this.lblLv.text = "0";
    }
    else
    {
      ((Component) this.strUnselect).gameObject.SetActive(false);
      ((Component) ((Component) this.lblHave).transform.parent).gameObject.SetActive(false);
      ((Component) ((Component) this.lblLv).transform.parent).gameObject.SetActive(true);
      ((Component) this.spriteValueType).gameObject.SetActive(true);
      EquipItemInfo equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(id);
      if (equipItemInfo == null)
        return;
      UILabel lblText = this.lblText;
      int num;
      string str1;
      if (!equipItemInfo.tableData.IsWeapon())
      {
        str1 = equipItemInfo.def.ToString();
      }
      else
      {
        num = equipItemInfo.atk;
        str1 = num.ToString();
      }
      lblText.text = str1;
      UILabel lblLv = this.lblLv;
      num = equipItemInfo.level;
      string str2 = num.ToString();
      lblLv.text = str2;
    }
  }
}
