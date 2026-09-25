// Decompiled with JetBrains decompiler
// Type: StatusBoostAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusBoostAnimator : MonoBehaviour
{
  private List<BoostStatus> enableBoostList = new List<BoostStatus>();
  private List<BoostStatus> deleteBoostList = new List<BoostStatus>();
  private USE_ITEM_EFFECT_TYPE boostDispType;
  private int time;
  private int updataCnt;
  private int[] animTable;
  private int animIndex;
  private Action<BoostStatus> updateCallback;
  private Action<BoostStatus> changeCallback;
  private const int ANIM_TIME = 3;
  private static readonly Color[] TEXT_RATE_COLOR = new Color[5]
  {
    Color.white,
    Color32.op_Implicit(new Color32((byte) 0, byte.MaxValue, (byte) 244, byte.MaxValue)),
    Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue)),
    Color32.op_Implicit(new Color32((byte) 251, (byte) 210, (byte) 0, byte.MaxValue)),
    Color32.op_Implicit(new Color32((byte) 225, (byte) 23, (byte) 23, byte.MaxValue))
  };

  public void SetupUI(Action<BoostStatus> update_callback, Action<BoostStatus> change_callback)
  {
    this.enableBoostList.Clear();
    BoostStatus boostStatus1 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EXP_UP);
    if (boostStatus1 != null)
      this.enableBoostList.Add(boostStatus1);
    BoostStatus boostStatus2 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.MONEY_UP);
    if (boostStatus2 != null)
      this.enableBoostList.Add(boostStatus2);
    BoostStatus boostStatus3 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.DROP_UP);
    if (boostStatus3 != null)
      this.enableBoostList.Add(boostStatus3);
    BoostStatus boostStatus4 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP);
    if (boostStatus4 != null)
    {
      if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && !MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
        this.enableBoostList.Add(boostStatus4);
      else if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && MonoBehaviourSingleton<UIPlayerStatus>.I.PermitHGPBoostUpdate)
        this.enableBoostList.Add(boostStatus4);
      else if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && MonoBehaviourSingleton<UIEnduranceStatus>.I.PermitHGPBoostUpdate)
        this.enableBoostList.Add(boostStatus4);
    }
    BoostStatus boostStatus5 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP);
    if (boostStatus5 != null)
      this.enableBoostList.Add(boostStatus5);
    BoostStatus boostStatus6 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP);
    if (boostStatus6 != null)
      this.enableBoostList.Add(boostStatus6);
    int boostDispType1 = (int) this.boostDispType;
    this.CreateAnimTable();
    this.updateCallback = update_callback;
    this.changeCallback = change_callback;
    int boostDispType2 = (int) this.boostDispType;
    if (boostDispType1 == boostDispType2 && this.boostDispType != USE_ITEM_EFFECT_TYPE.NONE)
      return;
    this.changeCallback(this.GetShowBoostStatus());
  }

  private void CreateAnimTable()
  {
    int count = this.enableBoostList.Count;
    if (count == 0)
    {
      this.boostDispType = USE_ITEM_EFFECT_TYPE.NONE;
    }
    else
    {
      this.animTable = new int[count];
      int index = 0;
      this.enableBoostList.ForEach((Action<BoostStatus>) (boost =>
      {
        this.animTable[index] = boost.type;
        ++index;
      }));
      if (this.boostDispType == USE_ITEM_EFFECT_TYPE.NONE)
      {
        this.animIndex = 0;
      }
      else
      {
        int index1 = this.enableBoostList.FindIndex((Predicate<BoostStatus>) (data => (USE_ITEM_EFFECT_TYPE) data.type == this.boostDispType));
        this.animIndex = index1 == -1 ? 0 : index1;
      }
      this.boostDispType = (USE_ITEM_EFFECT_TYPE) this.animTable[this.animIndex];
    }
  }

  private void Update()
  {
    if (this.enableBoostList.Count <= 0)
      return;
    DateTime now = TimeManager.GetNow();
    bool is_recommend_change = false;
    if (this.time == now.Second)
      return;
    this.time = now.Second;
    int index1 = 0;
    for (int count = this.enableBoostList.Count; index1 < count; ++index1)
    {
      BoostStatus enableBoost = this.enableBoostList[index1];
      if (!enableBoost.IsRemain())
      {
        this.deleteBoostList.Add(enableBoost);
        if (this.boostDispType == (USE_ITEM_EFFECT_TYPE) enableBoost.type)
          is_recommend_change = true;
      }
    }
    bool flag = false;
    if (this.deleteBoostList.Count > 0)
    {
      flag = true;
      int index2 = 0;
      for (int count = this.deleteBoostList.Count; index2 < count; ++index2)
      {
        BoostStatus delete_boost = this.deleteBoostList[index2];
        this.enableBoostList.RemoveAll((Predicate<BoostStatus>) (data => data.type == delete_boost.type));
      }
      this.deleteBoostList.Clear();
    }
    if (flag)
      this.CreateAnimTable();
    if (this.enableBoostList.Count > 0)
    {
      ++this.updataCnt;
      if (is_recommend_change || this.updataCnt >= 3)
      {
        this.ShowNextBoost(is_recommend_change);
        this.updataCnt = 0;
      }
      else
        this.updateCallback(this.GetShowBoostStatus());
    }
    else
    {
      this.updataCnt = 0;
      this.EndShowBoost();
    }
  }

  private void ShowNextBoost(bool is_recommend_change)
  {
    ++this.animIndex;
    if (this.animIndex >= this.animTable.Length)
      this.animIndex = 0;
    int boostDispType1 = (int) this.boostDispType;
    this.boostDispType = (USE_ITEM_EFFECT_TYPE) this.animTable[this.animIndex];
    int boostDispType2 = (int) this.boostDispType;
    if (boostDispType1 != boostDispType2 | is_recommend_change)
      this.changeCallback(this.GetShowBoostStatus());
    else
      this.updateCallback(this.GetShowBoostStatus());
  }

  private void EndShowBoost()
  {
    this.boostDispType = USE_ITEM_EFFECT_TYPE.NONE;
    this.changeCallback(this.GetShowBoostStatus());
  }

  public USE_ITEM_EFFECT_TYPE GetShowBoostType() => this.boostDispType;

  public BoostStatus GetShowBoostStatus()
  {
    if (this.boostDispType == USE_ITEM_EFFECT_TYPE.NONE)
      return (BoostStatus) null;
    return this.enableBoostList.Count == 0 ? (BoostStatus) null : this.enableBoostList.Find((Predicate<BoostStatus>) (data => (USE_ITEM_EFFECT_TYPE) data.type == this.boostDispType));
  }

  public Color GetRateColor(int boost_rate)
  {
    int num = 100 + boost_rate;
    int index = num < 500 ? (num < 300 ? (num < 200 ? (num <= 100 ? 0 : 1) : 2) : 3) : 4;
    return StatusBoostAnimator.TEXT_RATE_COLOR[index];
  }
}
