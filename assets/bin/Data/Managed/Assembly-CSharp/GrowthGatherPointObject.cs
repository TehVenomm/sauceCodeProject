// Decompiled with JetBrains decompiler
// Type: GrowthGatherPointObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GrowthGatherPointObject : GatherPointObject
{
  public GrowthGatherPointObject.OBJECT_MODE ObjectMode;
  protected float localElapsedTime;
  protected float localTimer;
  private GrowthGatherPointObject.GrowthInfo tmpGrowth = new GrowthGatherPointObject.GrowthInfo();

  public override void Initialize(FieldMapTable.GatherPointTableData point_data)
  {
    this.localElapsedTime = 0.0f;
    this.localTimer = 1f;
    this.ObjectMode = GrowthGatherPointObject.OBJECT_MODE.None;
    if (this.pointData != null)
    {
      this.tmpGrowth.current = 0;
      this.tmpGrowth.max = (int) point_data.maxNumRate;
    }
    base.Initialize(point_data);
  }

  public override void Gather()
  {
    if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      return;
    MonoBehaviourSingleton<FieldManager>.I.SendFieldGather((int) this.pointData.pointID, (Action<bool, FieldGatherRewardList>) ((b, list) =>
    {
      if (b)
        this.ObjectMode = GrowthGatherPointObject.OBJECT_MODE.Rest;
      this.localElapsedTime = 0.0f;
      this.calcGrowth();
      this.UpdateView();
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

  private void Update()
  {
    if (this.pointData == null || this.ObjectMode == GrowthGatherPointObject.OBJECT_MODE.None)
      return;
    this.localTimer += Time.deltaTime;
    if (this.ObjectMode == GrowthGatherPointObject.OBJECT_MODE.Growth)
      this.localElapsedTime += Time.deltaTime;
    if ((double) this.localTimer <= 1.0)
      return;
    if (this.calcGrowth())
      this.UpdateView();
    this.localTimer = 0.0f;
  }

  public override void CheckGather()
  {
    this.isGathered = true;
    this.ObjectMode = GrowthGatherPointObject.OBJECT_MODE.None;
    if (this.pointData != null)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<int> fieldPointIdList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPointIdList;
      if (fieldPointIdList != null)
      {
        int index = 0;
        for (int count = fieldPointIdList.Count; index < count; ++index)
        {
          if ((long) this.pointData.pointID == (long) fieldPointIdList[index])
          {
            flag1 = true;
            break;
          }
        }
      }
      List<GatherGrowthInfo> gatherGrowthList = MonoBehaviourSingleton<FieldManager>.I.currentFieldGatherGrowthList;
      if (gatherGrowthList != null)
      {
        for (int index = 0; index < gatherGrowthList.Count; ++index)
        {
          if ((long) this.pointData.pointID == (long) gatherGrowthList[index].pId)
          {
            flag2 = true;
            this.localElapsedTime = (float) gatherGrowthList[index].elapsedTime;
            break;
          }
        }
      }
      if (flag1 & flag2)
        this.ObjectMode = GrowthGatherPointObject.OBJECT_MODE.Growth;
      else if (!flag1 & flag2)
        this.ObjectMode = GrowthGatherPointObject.OBJECT_MODE.Rest;
    }
    this.calcGrowth();
    base.CheckGather();
  }

  public override void UpdateView()
  {
    if (Object.op_Inequality((Object) this.gatherEffect, (Object) null))
      ((Component) this.gatherEffect).gameObject.SetActive(!this.isGathered);
    if (!Object.op_Inequality((Object) this.modelView, (Object) null) || string.IsNullOrEmpty(this.viewData.modelHideNodeName))
      return;
    Transform transform = Utility.Find(this.modelView, this.viewData.modelHideNodeName);
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    if (this.ObjectMode != GrowthGatherPointObject.OBJECT_MODE.None)
      ((Component) transform).gameObject.SetActive(true);
    else
      ((Component) transform).gameObject.SetActive(false);
  }

  private bool calcGrowth()
  {
    bool flag = false;
    this.tmpGrowth.current = 0;
    if (this.ObjectMode == GrowthGatherPointObject.OBJECT_MODE.Growth)
    {
      if (this.pointData.growthInterval > 0U)
        this.tmpGrowth.current = Mathf.Min((int) this.pointData.maxNumRate, Mathf.FloorToInt(this.localElapsedTime / (float) this.pointData.growthInterval * (float) this.pointData.addNumRate));
      if (this.tmpGrowth.current < 0)
        this.tmpGrowth.current = 0;
    }
    if (this.tmpGrowth.current > 0)
    {
      if (this.isGathered)
        flag = true;
      this.isGathered = false;
    }
    else
    {
      if (!this.isGathered)
        flag = true;
      this.isGathered = true;
    }
    this.tmpGrowth.max = (int) this.pointData.maxNumRate;
    if (Object.op_Inequality((Object) this.gimmick, (Object) null))
      this.gimmick.OnNotify((object) this.tmpGrowth);
    return flag;
  }

  public class GrowthInfo
  {
    public int current;
    public int max;
  }

  public enum OBJECT_MODE
  {
    None,
    Rest,
    Growth,
  }
}
