// Decompiled with JetBrains decompiler
// Type: UIDropAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIDropAnnounce : MonoBehaviourSingleton<UIDropAnnounce>
{
  [SerializeField]
  protected GameObject announceItem;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected float announceItemSize;
  [SerializeField]
  protected int announceMax = 1;
  [SerializeField]
  protected Color[] announceColor = new Color[8]
  {
    new Color(1f, 1f, 1f),
    new Color(1f, 0.5f, 0.0f),
    new Color(1f, 1f, 0.0f),
    new Color(1f, 0.0f, 0.0f),
    new Color(0.0f, 1f, 0.0f),
    new Color(0.0f, 0.0f, 1f),
    new Color(0.8f, 1f, 0.0f),
    new Color(0.5f, 0.9f, 0.5f)
  };
  private List<UIDropAnnounceItem> announceItems = new List<UIDropAnnounceItem>();
  private List<UIDropAnnounceItem> announceDispItems = new List<UIDropAnnounceItem>();
  private List<UIDropAnnounce.DropAnnounceInfo> announceQueue = new List<UIDropAnnounce.DropAnnounceInfo>();

  protected override void OnDisable()
  {
    base.OnDisable();
    this.announceQueue.Clear();
    int index = 0;
    for (int count = this.announceDispItems.Count; index < count; ++index)
    {
      if (Object.op_Inequality((Object) this.panelChange, (Object) null))
        this.panelChange.Lock();
      ((Component) this.announceDispItems[index]).gameObject.SetActive(false);
    }
    this.announceDispItems.Clear();
  }

  public void Announce(UIDropAnnounce.DropAnnounceInfo info)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    if (this.announceDispItems.Count == this.announceMax)
    {
      this.announceQueue.Add(info);
    }
    else
    {
      UIDropAnnounceItem dropAnnounceItem = (UIDropAnnounceItem) null;
      int index = 0;
      for (int count = this.announceItems.Count; index < count; ++index)
      {
        if (!((Component) this.announceItems[index]).gameObject.activeSelf)
        {
          dropAnnounceItem = this.announceItems[index];
          break;
        }
      }
      if (Object.op_Equality((Object) dropAnnounceItem, (Object) null))
      {
        GameObject gameObject = ResourceUtility.Instantiate<GameObject>(this.announceItem);
        gameObject.transform.parent = ((Component) this).gameObject.transform;
        gameObject.transform.localScale = Vector3.one;
        dropAnnounceItem = gameObject.GetComponent<UIDropAnnounceItem>();
        this.announceItems.Add(dropAnnounceItem);
      }
      if (Object.op_Inequality((Object) this.panelChange, (Object) null))
        this.panelChange.UnLock();
      dropAnnounceItem.StartAnnounce(info.text, this.announceColor[(int) info.color], this.announceDispItems.Count > 0, new Action<UIDropAnnounceItem>(this.OnEnd));
      Vector3 zero = Vector3.zero;
      zero.y = -this.announceItemSize * (float) this.announceDispItems.Count;
      ((Component) dropAnnounceItem).transform.localPosition = zero;
      this.announceDispItems.Add(dropAnnounceItem);
    }
  }

  protected void OnEnd(UIDropAnnounceItem item)
  {
    this.announceDispItems.Remove(item);
    int index = 0;
    for (int count = this.announceDispItems.Count; index < count; ++index)
      this.announceDispItems[index].MovePos(index != 0, new Vector3(0.0f, -this.announceItemSize * (float) index, 0.0f), 0.1f);
    if (Object.op_Inequality((Object) this.panelChange, (Object) null))
      this.panelChange.Lock();
    if (this.announceQueue.Count <= 0)
      return;
    this.Announce(this.announceQueue[0]);
    this.announceQueue.RemoveAt(0);
  }

  public enum COLOR
  {
    NORMAL,
    RARE,
    DELIVERY,
    MAGI_AT,
    MAGI_SU,
    MAGI_HE,
    MAGI_PA,
    LOUNGE,
    SP_N,
    SP_HN,
    SP_R,
    HALLOWEEN,
    ESP_N,
    ESP_HN,
    ESP_R,
    SEASONAL,
    MAX,
  }

  public class DropAnnounceInfo
  {
    public string text;
    public UIDropAnnounce.COLOR color;

    public static UIDropAnnounce.DropAnnounceInfo CreateAccessoryItemInfo(
      uint id,
      int num,
      out bool is_rare)
    {
      is_rare = false;
      if (Singleton<AccessoryTable>.IsValid())
        return (UIDropAnnounce.DropAnnounceInfo) null;
      AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(id);
      if (data == null)
        return (UIDropAnnounce.DropAnnounceInfo) null;
      UIDropAnnounce.DropAnnounceInfo accessoryItemInfo = new UIDropAnnounce.DropAnnounceInfo();
      accessoryItemInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2004U, (object) data.name, (object) num);
      if (GameDefine.IsRare(data.rarity))
      {
        accessoryItemInfo.color = UIDropAnnounce.COLOR.RARE;
        is_rare = true;
      }
      else
        accessoryItemInfo.color = UIDropAnnounce.COLOR.NORMAL;
      return accessoryItemInfo;
    }

    public static UIDropAnnounce.DropAnnounceInfo CreateSkillItemInfo(
      uint id,
      int num,
      out bool is_rare)
    {
      is_rare = false;
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(id);
      if (skillItemData == null)
        return (UIDropAnnounce.DropAnnounceInfo) null;
      UIDropAnnounce.DropAnnounceInfo skillItemInfo = new UIDropAnnounce.DropAnnounceInfo();
      skillItemInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2002U, (object) skillItemData.name, (object) num);
      switch (skillItemData.type)
      {
        case SKILL_SLOT_TYPE.ATTACK:
          skillItemInfo.color = UIDropAnnounce.COLOR.MAGI_AT;
          break;
        case SKILL_SLOT_TYPE.SUPPORT:
          skillItemInfo.color = UIDropAnnounce.COLOR.MAGI_SU;
          break;
        case SKILL_SLOT_TYPE.HEAL:
          skillItemInfo.color = UIDropAnnounce.COLOR.MAGI_HE;
          break;
        default:
          skillItemInfo.color = UIDropAnnounce.COLOR.MAGI_PA;
          break;
      }
      is_rare = true;
      return skillItemInfo;
    }

    public static UIDropAnnounce.DropAnnounceInfo CreateEquipItemInfo(
      uint id,
      int num,
      out bool is_rare)
    {
      is_rare = false;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
      if (equipItemData == null)
        return (UIDropAnnounce.DropAnnounceInfo) null;
      UIDropAnnounce.DropAnnounceInfo equipItemInfo = new UIDropAnnounce.DropAnnounceInfo();
      equipItemInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2003U, (object) equipItemData.name, (object) num);
      if (!GameDefine.IsRare(equipItemData.rarity))
      {
        equipItemInfo.color = UIDropAnnounce.COLOR.NORMAL;
      }
      else
      {
        equipItemInfo.color = UIDropAnnounce.COLOR.RARE;
        is_rare = true;
      }
      return equipItemInfo;
    }

    public static UIDropAnnounce.DropAnnounceInfo CreateItemInfo(
      uint id,
      int num,
      out bool is_rare)
    {
      is_rare = false;
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(id);
      if (itemData == null)
        return (UIDropAnnounce.DropAnnounceInfo) null;
      UIDropAnnounce.DropAnnounceInfo itemInfo = new UIDropAnnounce.DropAnnounceInfo();
      int num1 = Mathf.Min(MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(id), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.ITEM_NUM_MAX);
      itemInfo.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2000U, (object) itemData.name, (object) num, (object) num1);
      if (!GameDefine.IsRare(itemData.rarity))
      {
        itemInfo.color = UIDropAnnounce.COLOR.NORMAL;
      }
      else
      {
        itemInfo.color = UIDropAnnounce.COLOR.RARE;
        is_rare = true;
      }
      return itemInfo;
    }
  }
}
