// Decompiled with JetBrains decompiler
// Type: QuestGetReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestGetReward : GameSection
{
  protected QuestCompleteReward reward;
  protected List<QuestCompleteReward.SellItem> sell;
  private bool isStoryComplete;
  private bool isDelivery;

  public override void Initialize()
  {
    switch (GameSection.GetEventData())
    {
      case DeliveryRewardList deliveryRewardList:
        this.reward = deliveryRewardList.delivery;
        this.sell = deliveryRewardList.sell;
        this.isDelivery = true;
        break;
      case StoryRewardList storyRewardList:
        this.reward = storyRewardList.story;
        this.isStoryComplete = true;
        break;
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.reward == null)
    {
      this.SetActive((Enum) QuestGetReward.UI.OBJ_FRAME, false);
    }
    else
    {
      this.SetFullScreenButton((Enum) QuestGetReward.UI.BTN_CENTER);
      this.SetActive((Enum) QuestGetReward.UI.OBJ_FRAME, true);
      this.SetLabelText((Enum) QuestGetReward.UI.LBL_GOLD, this.reward.money.ToString("N0"));
      this.SetLabelText((Enum) QuestGetReward.UI.LBL_CRYSTAL, this.reward.crystal.ToString("N0"));
      this.SetLabelText((Enum) QuestGetReward.UI.LBL_EXP, this.reward.exp.ToString("N0"));
      int num1 = this.reward.crystal > 0 ? 1 : 0;
      int item_num = this.reward.item.Count + this.reward.equipItem.Count + this.reward.skillItem.Count + this.reward.accessoryItem.Count + num1;
      int num2 = num1 + this.reward.item.Count;
      int num3 = num2 + this.reward.equipItem.Count;
      int num4 = num3 + this.reward.accessoryItem.Count;
      QuestGetReward.RewardData[] data = new QuestGetReward.RewardData[item_num];
      int index1 = 0;
      for (int index2 = item_num; index1 < index2; ++index1)
      {
        data[index1] = new QuestGetReward.RewardData();
        if (index1 < num1)
        {
          data[index1].reward_type = REWARD_TYPE.CRYSTAL;
          data[index1].icon_type = ITEM_ICON_TYPE.NONE;
          data[index1].icon_id = 1;
          data[index1].item_id = 0U;
          data[index1].rarity = new RARITY_TYPE?();
          data[index1].element = ELEMENT_TYPE.MAX;
          data[index1].magi_enable_equip_type = new EQUIPMENT_TYPE?();
          data[index1].num = this.reward.crystal;
          data[index1].enemy_icon_id = 0;
          data[index1].enemy_icon_id2 = 0;
          data[index1].getType = GET_TYPE.PAY;
        }
        else if (index1 < num2)
        {
          QuestCompleteReward.Item obj = this.reward.item[index1 - num1];
          ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) obj.itemId);
          data[index1].reward_type = REWARD_TYPE.ITEM;
          data[index1].icon_type = ItemIcon.GetItemIconType(itemData.type);
          data[index1].icon_id = itemData.iconID;
          data[index1].item_id = itemData.id;
          data[index1].rarity = new RARITY_TYPE?(itemData.rarity);
          data[index1].element = ELEMENT_TYPE.MAX;
          data[index1].magi_enable_equip_type = new EQUIPMENT_TYPE?();
          data[index1].num = obj.num;
          data[index1].enemy_icon_id = itemData.enemyIconID;
          data[index1].enemy_icon_id2 = itemData.enemyIconID2;
          data[index1].getType = GET_TYPE.PAY;
        }
        else if (index1 < num3)
        {
          QuestCompleteReward.EquipItem equipItem = this.reward.equipItem[index1 - num2];
          EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equipItem.equipItemId);
          data[index1].reward_type = REWARD_TYPE.EQUIP_ITEM;
          data[index1].icon_type = ItemIcon.GetItemIconType(equipItemData.type);
          data[index1].icon_id = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
          data[index1].item_id = equipItemData.id;
          data[index1].rarity = new RARITY_TYPE?(equipItemData.rarity);
          data[index1].element = equipItemData.GetTargetElementPriorityToTable();
          data[index1].magi_enable_equip_type = new EQUIPMENT_TYPE?();
          data[index1].num = equipItem.num == 1 ? -1 : equipItem.num;
          data[index1].enemy_icon_id = 0;
          data[index1].enemy_icon_id2 = 0;
          data[index1].getType = equipItemData.getType;
        }
        else if (index1 < num4)
        {
          QuestCompleteReward.SkillItem skillItem = this.reward.skillItem[index1 - num3];
          SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) skillItem.skillItemId);
          data[index1].reward_type = REWARD_TYPE.SKILL_ITEM;
          data[index1].icon_type = ItemIcon.GetItemIconType(skillItemData.type);
          data[index1].icon_id = skillItemData.iconID;
          data[index1].item_id = skillItemData.id;
          data[index1].rarity = new RARITY_TYPE?(skillItemData.rarity);
          data[index1].element = ELEMENT_TYPE.MAX;
          data[index1].magi_enable_equip_type = skillItemData.GetEnableEquipType();
          data[index1].num = skillItem.num == 1 ? -1 : skillItem.num;
          data[index1].enemy_icon_id = 0;
          data[index1].enemy_icon_id2 = 0;
          data[index1].getType = GET_TYPE.PAY;
        }
        else
        {
          QuestCompleteReward.AccessoryItem accessoryItem = this.reward.accessoryItem[index1 - num4];
          AccessoryTable.AccessoryData data1 = Singleton<AccessoryTable>.I.GetData((uint) accessoryItem.accessoryId);
          data[index1].reward_type = REWARD_TYPE.ACCESSORY;
          data[index1].icon_type = ITEM_ICON_TYPE.ACCESSORY;
          data[index1].icon_id = (int) data1.accessoryId;
          data[index1].item_id = data1.accessoryId;
          data[index1].rarity = new RARITY_TYPE?(data1.rarity);
          data[index1].element = ELEMENT_TYPE.MAX;
          data[index1].magi_enable_equip_type = new EQUIPMENT_TYPE?();
          data[index1].num = accessoryItem.num == 1 ? -1 : accessoryItem.num;
          data[index1].enemy_icon_id = 0;
          data[index1].enemy_icon_id2 = 0;
          data[index1].getType = data1.getType;
        }
      }
      this.SetGrid((Enum) QuestGetReward.UI.GRD_ICON, "", item_num, false, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetMaterialInfo(ItemIcon.Create(data[i].icon_type, data[i].icon_id, data[i].rarity, t, data[i].element, data[i].magi_enable_equip_type, data[i].num, "REWARD", i, enemy_icon_id: data[i].enemy_icon_id, enemy_icon_id2: data[i].enemy_icon_id2, getType: data[i].getType).transform, data[i].reward_type, data[i].item_id)));
    }
  }

  public void OnQuery_OK()
  {
    if (this.isDelivery)
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList((Action<bool>) (b =>
      {
        GameSection.ChangeStayEvent("FROM_DELIVERY");
        GameSection.ResumeEvent(b);
      }));
    }
    else if (this.isStoryComplete)
      GameSection.ChangeEvent("TO_SELECT");
    else
      GameSection.BackSection();
  }

  private enum UI
  {
    OBJ_FRAME,
    LBL_GOLD,
    LBL_CRYSTAL,
    LBL_EXP,
    GRD_ICON,
    BTN_CENTER,
  }

  private struct RewardData
  {
    public REWARD_TYPE reward_type;
    public ITEM_ICON_TYPE icon_type;
    public int icon_id;
    public uint item_id;
    public RARITY_TYPE? rarity;
    public ELEMENT_TYPE element;
    public EQUIPMENT_TYPE? magi_enable_equip_type;
    public int num;
    public int enemy_icon_id;
    public int enemy_icon_id2;
    public GET_TYPE getType;
  }
}
