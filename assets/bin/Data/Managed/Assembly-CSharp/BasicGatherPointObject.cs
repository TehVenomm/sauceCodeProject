// Decompiled with JetBrains decompiler
// Type: BasicGatherPointObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BasicGatherPointObject : GatherPointObject
{
  public override void Gather()
  {
    if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      return;
    this.isGathered = true;
    this.UpdateView();
    MonoBehaviourSingleton<FieldManager>.I.SendFieldGather((int) this.pointData.pointID, (Action<bool, FieldGatherRewardList>) ((b, list) =>
    {
      if (!MonoBehaviourSingleton<UIDropAnnounce>.IsValid())
        return;
      int index1 = 0;
      for (int count = list.fieldGather.accessoryItem.Count; index1 < count; ++index1)
      {
        QuestCompleteReward.AccessoryItem accessoryItem = list.fieldGather.accessoryItem[index1];
        bool is_rare = false;
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateAccessoryItemInfo((uint) accessoryItem.accessoryId, accessoryItem.num, out is_rare));
        SoundManager.PlayOneShotUISE(40000154);
      }
      int index2 = 0;
      for (int count = list.fieldGather.skillItem.Count; index2 < count; ++index2)
      {
        QuestCompleteReward.SkillItem skillItem = list.fieldGather.skillItem[index2];
        bool is_rare = false;
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateSkillItemInfo((uint) skillItem.skillItemId, skillItem.num, out is_rare));
        SoundManager.PlayOneShotUISE(40000154);
      }
      int index3 = 0;
      for (int count = list.fieldGather.equipItem.Count; index3 < count; ++index3)
      {
        QuestCompleteReward.EquipItem equipItem = list.fieldGather.equipItem[index3];
        bool is_rare = false;
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateEquipItemInfo((uint) equipItem.equipItemId, equipItem.num, out is_rare));
        SoundManager.PlayOneShotUISE(40000154);
      }
      int index4 = 0;
      for (int count = list.fieldGather.item.Count; index4 < count; ++index4)
      {
        QuestCompleteReward.Item obj = list.fieldGather.item[index4];
        bool is_rare = false;
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateItemInfo((uint) obj.itemId, obj.num, out is_rare));
        SoundManager.PlayOneShotUISE(is_rare ? 40000154 : 40000153);
      }
    }));
  }

  public override void CheckGather()
  {
    this.isGathered = true;
    List<int> fieldPointIdList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPointIdList;
    if (fieldPointIdList != null)
    {
      int index = 0;
      for (int count = fieldPointIdList.Count; index < count; ++index)
      {
        if ((long) this.pointData.pointID == (long) fieldPointIdList[index])
        {
          this.isGathered = false;
          break;
        }
      }
    }
    base.CheckGather();
  }

  public override void UpdateView()
  {
    base.UpdateView();
    if (!Object.op_Inequality((Object) this.gimmick, (Object) null))
      return;
    this.gimmick.OnNotify((object) this.isGathered);
  }
}
