// Decompiled with JetBrains decompiler
// Type: PointShopFilterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class PointShopFilterBase : GameSection
{
  private PointShopFilterBase.Filter filter;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if (!(eventData[0] is PointShopFilterBase.Filter filter))
    {
      filter = new PointShopFilterBase.Filter()
      {
        typeFilter = new PointShopFilterBase.Filter.CheckFilterBase[12]
        {
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.TicketCheckFilter(PointShopFilterBase.UI.BTN_TICKET),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.MoneyCheckFilter(PointShopFilterBase.UI.BTN_MONEY),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.UseItemCheckFilter(PointShopFilterBase.UI.BTN_USE_ITEM),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.LithographCheckFilter(PointShopFilterBase.UI.BTN_LITHOGRAPH),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.MetalCheckFilter(PointShopFilterBase.UI.BTN_METAL),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.MaterialCheckFilter(PointShopFilterBase.UI.BTN_MATERIAL),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.QuestCheckFilter(PointShopFilterBase.UI.BTN_QUEST),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.SkillCheckFilter(PointShopFilterBase.UI.BTN_SKILL),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.EquipCheckFilter(PointShopFilterBase.UI.BTN_EQUIP),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.StampCheckFilter(PointShopFilterBase.UI.BTN_STAMP),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.AvatarCheckFilter(PointShopFilterBase.UI.BTN_AVATAR),
          (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.DegreeCheckFilter(PointShopFilterBase.UI.BTN_DEGREE)
        }
      };
      filter.typeBit = filter.GetAllTrueBit(PointShopFilterBase.Filter.CATEGORY.TYPE);
      filter.rarityFilter = new PointShopFilterBase.Filter.CheckFilterBase[6]
      {
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_D, RARITY_TYPE.D),
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_C, RARITY_TYPE.C),
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_B, RARITY_TYPE.B),
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_A, RARITY_TYPE.A),
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_S, RARITY_TYPE.S),
        (PointShopFilterBase.Filter.CheckFilterBase) new PointShopFilterBase.Filter.RarityCheckFilter(PointShopFilterBase.UI.BTN_RARITY_SS, RARITY_TYPE.SS)
      };
      filter.rarityBit = filter.GetAllTrueBit(PointShopFilterBase.Filter.CATEGORY.RARITY);
    }
    this.filter = filter;
    List<PointShopItem> list = eventData[1] as List<PointShopItem>;
    foreach (PointShopFilterBase.Filter.CheckFilterBase checkFilterBase in this.filter.typeFilter)
    {
      this.SetEvent((Enum) checkFilterBase.checkboxUI, "TYPE", (int) checkFilterBase.type);
      if (list != null)
      {
        bool is_visible = checkFilterBase.IsInclude(list);
        this.SetActive((Enum) checkFilterBase.checkboxUI, is_visible);
        this.SetActive(this.GetCtrl((Enum) checkFilterBase.checkboxUI).parent, (Enum) PointShopFilterBase.UI.SPR_GRAY, !is_visible);
      }
    }
    foreach (PointShopFilterBase.Filter.CheckFilterBase checkFilterBase in this.filter.rarityFilter)
      this.SetEvent((Enum) checkFilterBase.checkboxUI, "RARITY", (int) checkFilterBase.rarity);
    this.SetEvent((Enum) PointShopFilterBase.UI.BTN_ALL_DESELECT, "ALL", 0);
    this.SetEvent((Enum) PointShopFilterBase.UI.BTN_ALL_SELECT, "ALL", 1);
    this.RefreshUI();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.filter == null)
      return;
    foreach (PointShopFilterBase.Filter.CheckFilterBase checkFilterBase in this.filter.rarityFilter)
      this.SetToggle((Enum) checkFilterBase.checkboxUI, this.filter.IsCheck(PointShopFilterBase.Filter.CATEGORY.RARITY, (int) checkFilterBase.rarity));
    foreach (PointShopFilterBase.Filter.CheckFilterBase checkFilterBase in this.filter.typeFilter)
      this.SetToggle((Enum) checkFilterBase.checkboxUI, this.filter.IsCheck(PointShopFilterBase.Filter.CATEGORY.TYPE, (int) checkFilterBase.type));
  }

  private void OnQuery_TYPE()
  {
    this.filter.FlipBit(PointShopFilterBase.Filter.CATEGORY.TYPE, (int) GameSection.GetEventData());
  }

  private void OnQuery_RARITY()
  {
    this.filter.FlipBit(PointShopFilterBase.Filter.CATEGORY.RARITY, (int) GameSection.GetEventData());
  }

  private void OnQuery_ALL()
  {
    this.filter.ResetBit((int) GameSection.GetEventData() != 0);
    this.RefreshUI();
  }

  private void OnQuery_FILTERING()
  {
    GameSection.SetEventData((object) this.filter);
    GameSection.BackSection();
  }

  public enum UI
  {
    BTN_ALL_SELECT,
    BTN_ALL_DESELECT,
    GRD_TYPE_ROOT,
    GRD_RARITY_ROOT,
    BTN_TICKET,
    BTN_MONEY,
    BTN_USE_ITEM,
    BTN_LITHOGRAPH,
    BTN_METAL,
    BTN_MATERIAL,
    BTN_QUEST,
    BTN_SKILL,
    BTN_EQUIP,
    BTN_STAMP,
    BTN_AVATAR,
    BTN_DEGREE,
    BTN_RARITY_D,
    BTN_RARITY_C,
    BTN_RARITY_B,
    BTN_RARITY_A,
    BTN_RARITY_S,
    BTN_RARITY_SS,
    SPR_GRAY,
  }

  public class Filter
  {
    public int typeBit;
    public PointShopFilterBase.Filter.CheckFilterBase[] typeFilter;
    public int rarityBit;
    public PointShopFilterBase.Filter.CheckFilterBase[] rarityFilter;

    public int GetAllTrueBit(PointShopFilterBase.Filter.CATEGORY category)
    {
      int allTrueBit = 0;
      switch (category)
      {
        case PointShopFilterBase.Filter.CATEGORY.TYPE:
          for (int index = 0; index < this.typeFilter.Length; ++index)
            allTrueBit |= 1 << (int) (this.typeFilter[index].type & (PointShopFilterBase.Filter.CheckFilterBase.TYPE) 31 /*0x1F*/);
          break;
        case PointShopFilterBase.Filter.CATEGORY.RARITY:
          for (int index = 0; index < this.rarityFilter.Length; ++index)
            allTrueBit |= 1 << (int) (this.rarityFilter[index].rarity & (RARITY_TYPE) 31 /*0x1F*/);
          break;
      }
      return allTrueBit;
    }

    public void ResetBit(bool is_check)
    {
      if (is_check)
      {
        this.typeBit = this.GetAllTrueBit(PointShopFilterBase.Filter.CATEGORY.TYPE);
        this.rarityBit = this.GetAllTrueBit(PointShopFilterBase.Filter.CATEGORY.RARITY);
      }
      else
      {
        this.typeBit = 0;
        this.rarityBit = 0;
      }
    }

    public void FlipBit(PointShopFilterBase.Filter.CATEGORY category, int index)
    {
      if (category != PointShopFilterBase.Filter.CATEGORY.TYPE)
      {
        if (category != PointShopFilterBase.Filter.CATEGORY.RARITY)
          return;
        this.rarityBit ^= 1 << index;
      }
      else
        this.typeBit ^= 1 << index;
    }

    public bool IsCheck(PointShopFilterBase.Filter.CATEGORY category, int index)
    {
      if (index >= 0)
      {
        if (category == PointShopFilterBase.Filter.CATEGORY.TYPE)
          return (this.typeBit & 1 << index) != 0;
        if (category == PointShopFilterBase.Filter.CATEGORY.RARITY)
          return (this.rarityBit & 1 << index) != 0;
      }
      return true;
    }

    public void DoFiltering(ref List<PointShopItem> list)
    {
      for (int index = 0; index < this.typeFilter.Length; ++index)
      {
        if (!this.IsCheck(PointShopFilterBase.Filter.CATEGORY.TYPE, index))
          this.typeFilter[index].DoFiltering(ref list);
      }
      for (int index = 0; index < this.rarityFilter.Length; ++index)
      {
        if (!this.IsCheck(PointShopFilterBase.Filter.CATEGORY.RARITY, index))
          this.rarityFilter[index].DoFiltering(ref list);
      }
    }

    public enum CATEGORY
    {
      TYPE,
      RARITY,
    }

    public class CheckFilterBase
    {
      public PointShopFilterBase.Filter.CheckFilterBase.TYPE type;
      public RARITY_TYPE rarity;
      public PointShopFilterBase.UI checkboxUI;

      public CheckFilterBase(PointShopFilterBase.UI ui) => this.checkboxUI = ui;

      public void DoFiltering(ref List<PointShopItem> list)
      {
        list = list.Where<PointShopItem>((Func<PointShopItem, bool>) (x => !this.IsCondition(x))).ToList<PointShopItem>();
      }

      protected virtual bool IsCondition(PointShopItem item) => false;

      public bool IsInclude(List<PointShopItem> list)
      {
        return list.Any<PointShopItem>((Func<PointShopItem, bool>) (x => this.IsCondition(x)));
      }

      public enum TYPE
      {
        TICKET,
        MONEY,
        USE_ITEM,
        LITHOGRAPH,
        METAL,
        MATERIAL,
        QUEST,
        SKILL,
        EQUIP,
        STAMP,
        AVATAR,
        DEGREE,
        ABILITY_ITEM,
      }
    }

    public class TicketCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public TicketCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.TICKET;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        return item.type == 3 && Singleton<ItemTable>.I.GetItemData((uint) item.itemId).type == ITEM_TYPE.TICKET;
      }
    }

    public class MoneyCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public MoneyCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.MONEY;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 2;
    }

    public class UseItemCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public UseItemCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.USE_ITEM;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        return item.type == 3 && Singleton<ItemTable>.I.GetItemData((uint) item.itemId).type == ITEM_TYPE.USE_ITEM;
      }
    }

    public class LithographCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public LithographCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.LITHOGRAPH;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        return item.type == 3 && Singleton<ItemTable>.I.GetItemData((uint) item.itemId).type == ITEM_TYPE.LITHOGRAPH;
      }
    }

    public class MetalCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public MetalCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.METAL;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        return item.type == 3 && Singleton<ItemTable>.I.GetItemData((uint) item.itemId).type == ITEM_TYPE.MATERIAL_METAL;
      }
    }

    public class MaterialCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public MaterialCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.MATERIAL;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        if (item.type != 3)
          return false;
        switch (Singleton<ItemTable>.I.GetItemData((uint) item.itemId).type)
        {
          case ITEM_TYPE.MATERIAL_METAL:
          case ITEM_TYPE.LITHOGRAPH:
          case ITEM_TYPE.USE_ITEM:
          case ITEM_TYPE.TICKET:
          case ITEM_TYPE.FORTUNE_TICKET:
            return false;
          default:
            return true;
        }
      }
    }

    public class QuestCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public QuestCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.QUEST;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 6;
    }

    public class SkillCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public SkillCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.SKILL;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 5;
    }

    public class EquipCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public EquipCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.EQUIP;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 4;
    }

    public class StampCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public StampCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.STAMP;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 8;
    }

    public class AvatarCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public AvatarCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.AVATAR;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 7;
    }

    public class DegreeCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public DegreeCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.DEGREE;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 9;
    }

    public class AbilityItemCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public AbilityItemCheckFilter(PointShopFilterBase.UI ui)
        : base(ui)
      {
        this.type = PointShopFilterBase.Filter.CheckFilterBase.TYPE.ABILITY_ITEM;
      }

      protected override bool IsCondition(PointShopItem item) => item.type == 10;
    }

    public class RarityCheckFilter : PointShopFilterBase.Filter.CheckFilterBase
    {
      public RarityCheckFilter(PointShopFilterBase.UI ui, RARITY_TYPE r)
        : base(ui)
      {
        this.rarity = r;
      }

      protected override bool IsCondition(PointShopItem item)
      {
        switch ((REWARD_TYPE) item.type)
        {
          case REWARD_TYPE.ITEM:
          case REWARD_TYPE.ABILITY_ITEM:
            ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) item.itemId);
            return itemData.type != ITEM_TYPE.USE_ITEM && itemData.rarity == this.rarity;
          case REWARD_TYPE.EQUIP_ITEM:
            return Singleton<EquipItemTable>.I.GetEquipItemData((uint) item.itemId).rarity == this.rarity;
          case REWARD_TYPE.SKILL_ITEM:
            return Singleton<SkillItemTable>.I.GetSkillItemData((uint) item.itemId).rarity == this.rarity;
          case REWARD_TYPE.QUEST_ITEM:
            return Singleton<QuestTable>.I.GetQuestData((uint) item.itemId).rarity == this.rarity;
          case REWARD_TYPE.ACCESSORY:
            return Singleton<AccessoryTable>.I.GetData((uint) item.itemId).rarity == this.rarity;
          default:
            return false;
        }
      }
    }
  }
}
