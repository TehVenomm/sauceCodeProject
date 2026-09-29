// Decompiled with JetBrains decompiler
// Type: QuestRequestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestRequestItem : UIBehaviour
{
  private readonly string[] SPR_FRAME_TYPE = new string[5]
  {
    "RequestPlate_Base",
    "RequestPlate_Event",
    "RequestPlate_Story",
    "RequestPlate_Hard",
    "RequestPlate_SubEvent"
  };

  public virtual void Setup(Transform t, DeliveryTable.DeliveryData info)
  {
    this.SetIcon(t, info);
    this.SetDeliveryName(t, info);
    bool is_visible1 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) info.id);
    this.SetActive(t, (Enum) QuestRequestItem.UI.OBJ_REQUEST_OK, is_visible1);
    this.SetActive(t, (Enum) QuestRequestItem.UI.OBJ_REQUEST_COMPLETED, false);
    int have;
    int need;
    string item_name;
    string limit_time;
    MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int) info.id, out have, out need, out item_name, out limit_time);
    this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_HAVE, have.ToString());
    this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_NEED, need.ToString());
    this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_NEED_ITEM_NAME, item_name);
    this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_LIMIT, limit_time);
    this.SetFrame(t, info);
    if (info.GetUIType() == DeliveryTable.UIType.STORY)
    {
      this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_TEXT_STORY, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.NORMAL);
      this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_TEXT_STORY_HARD, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.HARD);
    }
    if (info.GetUIType() == DeliveryTable.UIType.NONE)
      this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_TEXT_SUB_HARD, info.GetRegionDifficultyType() == REGION_DIFFICULTY_TYPE.HARD);
    DeliveryTable.UIType uiType = info.GetUITextType();
    if (uiType == DeliveryTable.UIType.NONE)
      uiType = info.GetUIType();
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_EVENT_TEXT, uiType == DeliveryTable.UIType.EVENT);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_DAILY_TEXT, uiType == DeliveryTable.UIType.DAILY);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_WEEKLY_TEXT, uiType == DeliveryTable.UIType.WEEKLY);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_DROP_DIFFICULTY_RARE, info.GetDeliveryDropRarity() == DELIVERY_DROP_DIFFICULTY.RARE);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_DROP_DIFFICULTY_SUPER_RARE, info.GetDeliveryDropRarity() == DELIVERY_DROP_DIFFICULTY.SUPER_RARE);
    DeliveryDropRareTextColor componentInChildren = ((Component) t).GetComponentInChildren<DeliveryDropRareTextColor>();
    if (Object.op_Inequality((Object) componentInChildren, (Object) null) && Object.op_Inequality((Object) this.GetComponent<UILabel>(t, (Enum) QuestRequestItem.UI.LBL_NEED_ITEM_NAME), (Object) null))
      this.SetColor(t, (Enum) QuestRequestItem.UI.LBL_NEED_ITEM_NAME, componentInChildren.GetRarityColor(info.GetDeliveryDropRarity()));
    this.SetSprite(t, (Enum) QuestRequestItem.UI.SPR_FRAME, this.SPR_FRAME_TYPE[info.DeliveryTypeIndex()]);
    List<DeliveryRewardTable.DeliveryRewardData.Reward> source = new List<DeliveryRewardTable.DeliveryRewardData.Reward>();
    DeliveryRewardTable.DeliveryRewardData[] deliveryRewardTableData = Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(info.id);
    if (deliveryRewardTableData != null)
    {
      foreach (DeliveryRewardTable.DeliveryRewardData deliveryRewardData in deliveryRewardTableData)
      {
        if (deliveryRewardData.reward.type != REWARD_TYPE.RANKING_POINT)
          source.Add(deliveryRewardData.reward);
      }
    }
    List<PointShopGetPointTable.Data> fromDeiliveryId = Singleton<PointShopGetPointTable>.I.GetFromDeiliveryId(info.id);
    if (fromDeiliveryId.Any<PointShopGetPointTable.Data>())
    {
      foreach (PointShopGetPointTable.Data data in fromDeiliveryId)
        source.Add(new DeliveryRewardTable.DeliveryRewardData.Reward()
        {
          item_id = data.pointShopId,
          num = data.basePoint,
          type = REWARD_TYPE.POINT_SHOP_POINT
        });
    }
    if (source.Any<DeliveryRewardTable.DeliveryRewardData.Reward>())
    {
      if (source.Count >= 2)
        source = source.OrderBy<DeliveryRewardTable.DeliveryRewardData.Reward, int>((Func<DeliveryRewardTable.DeliveryRewardData.Reward, int>) (x => this.GetRewardPriority(x))).ToList<DeliveryRewardTable.DeliveryRewardData.Reward>();
      QuestRequestItem.UI[] uiArray = new QuestRequestItem.UI[2]
      {
        QuestRequestItem.UI.OBJ_ICON_ROOT_1,
        QuestRequestItem.UI.OBJ_ICON_ROOT_2
      };
      for (int index = 0; index < 2; ++index)
      {
        bool is_visible2 = source.Count >= index + 1;
        this.SetActive(t, (Enum) uiArray[index], is_visible2);
        if (is_visible2)
        {
          DeliveryRewardTable.DeliveryRewardData.Reward reward = source[index];
          ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, this.FindCtrl(t, (Enum) uiArray[index]), questIconSizeType: ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST);
          if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
            rewardItemIcon.SetEnableCollider(false);
        }
      }
    }
    QuestTable.QuestTableData questData = info.GetQuestData();
    if (questData != null)
    {
      bool is_visible3 = questData.level > (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
      foreach (UIWidget componentsInChild in ((Component) this.FindCtrl(t, (Enum) QuestRequestItem.UI.SPR_FRAME)).GetComponentsInChildren<UIWidget>())
      {
        if (is_visible3 && !((Object) componentsInChild).name.Contains("Mask"))
          componentsInChild.color = Color.gray;
      }
      this.SetActive(t, (Enum) QuestRequestItem.UI.OBJ_LEVEL_LIMIT, is_visible3);
      this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_LEVEL_LIMIT, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 3U), (object) questData.level));
      this.SetButtonEnabled(t, !is_visible3);
    }
    else
      this.SetActive(t, (Enum) QuestRequestItem.UI.OBJ_LEVEL_LIMIT, false);
    UIGrid component = this.GetComponent<UIGrid>(t, (Enum) QuestRequestItem.UI.GRD_ICON_ROOT);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reposition();
  }

  protected virtual void SetFrame(Transform t, DeliveryTable.DeliveryData info)
  {
    DeliveryTable.UIType uiType = info.GetUIType();
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_NORMAL, uiType == DeliveryTable.UIType.NONE);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_EVENT, uiType == DeliveryTable.UIType.EVENT || uiType == DeliveryTable.UIType.DAILY || uiType == DeliveryTable.UIType.WEEKLY);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_STORY, uiType == DeliveryTable.UIType.STORY);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_HARD, uiType == DeliveryTable.UIType.HARD);
    this.SetActive(t, (Enum) QuestRequestItem.UI.SPR_TYPE_SUB_EVENT, uiType == DeliveryTable.UIType.SUB_EVENT);
  }

  protected virtual void SetIcon(Transform t, DeliveryTable.DeliveryData info)
  {
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData((int) info.npcID);
    if (info.id == 99140101U)
      this.SetNPCIcon(t, (Enum) QuestRequestItem.UI.TEX_NPC, 999);
    else
      this.SetNPCIcon(t, (Enum) QuestRequestItem.UI.TEX_NPC, npcData.npcModelID);
  }

  protected virtual void SetDeliveryName(Transform t, DeliveryTable.DeliveryData info)
  {
    this.SetLabelText(t, (Enum) QuestRequestItem.UI.LBL_DELIVERY_COMMENT, info.name);
  }

  private int GetRewardPriority(
    DeliveryRewardTable.DeliveryRewardData.Reward reward)
  {
    switch (reward.type)
    {
      case REWARD_TYPE.CRYSTAL:
        return 0;
      case REWARD_TYPE.ITEM:
        return Singleton<ItemTable>.I.GetItemData(reward.item_id).type == ITEM_TYPE.TICKET ? 1 : 4;
      case REWARD_TYPE.EQUIP_ITEM:
        return 2;
      case REWARD_TYPE.POINT_SHOP_POINT:
        return 3;
      default:
        return 4;
    }
  }

  private enum UI
  {
    TEX_NPC,
    LBL_DELIVERY_COMMENT,
    OBJ_REQUEST_OK,
    OBJ_REQUEST_COMPLETED,
    LBL_HAVE,
    LBL_NEED,
    LBL_NEED_ITEM_NAME,
    LBL_LIMIT,
    SPR_TYPE_NORMAL,
    SPR_TYPE_EVENT,
    SPR_TYPE_STORY,
    SPR_TYPE_HARD,
    SPR_TYPE_SUB_EVENT,
    SPR_TYPE_EVENT_TEXT,
    SPR_TYPE_DAILY_TEXT,
    SPR_TYPE_WEEKLY_TEXT,
    SPR_DROP_DIFFICULTY_RARE,
    SPR_DROP_DIFFICULTY_SUPER_RARE,
    SPR_FRAME,
    OBJ_ICON_ROOT_1,
    OBJ_ICON_ROOT_2,
    GRD_ICON_ROOT,
    OBJ_LEVEL_LIMIT,
    LBL_LEVEL_LIMIT,
    SPR_TYPE_TEXT_STORY,
    SPR_TYPE_TEXT_STORY_HARD,
    SPR_TYPE_TEXT_SUB_HARD,
  }
}
