// Decompiled with JetBrains decompiler
// Type: ContinusAttackParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ContinusAttackParam
{
  private Character m_owner;
  private List<ContinusAttackParam.ContinusAtkData> m_continusAtkDataList = new List<ContinusAttackParam.ContinusAtkData>();

  public ContinusAttackParam(Character chara) => this.m_owner = chara;

  public void Register(
    int eventIndex,
    float endTime,
    AnimEventCollider eventCollider,
    Transform effectTrans)
  {
    ContinusAttackParam.ContinusAtkData continusAtkData = this.SearchByIndex(eventIndex);
    if (continusAtkData != null)
    {
      continusAtkData.Release();
      this.m_continusAtkDataList.Remove(continusAtkData);
    }
    this.m_continusAtkDataList.Add(new ContinusAttackParam.ContinusAtkData()
    {
      eventIndex = eventIndex,
      endTime = endTime,
      eventCollider = eventCollider,
      effectTrans = effectTrans
    });
  }

  public void Update()
  {
    for (int index = this.m_continusAtkDataList.Count - 1; index >= 0; --index)
    {
      ContinusAttackParam.ContinusAtkData continusAtkData = this.m_continusAtkDataList[index];
      continusAtkData.endTime -= Time.deltaTime;
      if ((double) continusAtkData.endTime <= 0.0)
      {
        continusAtkData.Release();
        this.m_continusAtkDataList.Remove(continusAtkData);
      }
    }
  }

  public void RemoveAll()
  {
    if (this.m_continusAtkDataList == null)
      return;
    foreach (ContinusAttackParam.ContinusAtkData continusAtkData in this.m_continusAtkDataList)
      continusAtkData.Release();
    this.m_continusAtkDataList.Clear();
  }

  public ContinusAttackParam.ContinusAtkData SearchByIndex(int eventIndex)
  {
    int count = this.m_continusAtkDataList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.m_continusAtkDataList[index].eventIndex == eventIndex)
        return this.m_continusAtkDataList[index];
    }
    return (ContinusAttackParam.ContinusAtkData) null;
  }

  public ContinusAttackParam.SyncParam CreateSyncParam()
  {
    ContinusAttackParam.SyncParam syncParam = new ContinusAttackParam.SyncParam();
    foreach (ContinusAttackParam.ContinusAtkData continusAtkData in this.m_continusAtkDataList)
      syncParam.syncDataList.Add(new ContinusAttackParam.SyncData()
      {
        eventIndex = continusAtkData.eventIndex,
        endTime = continusAtkData.endTime
      });
    return syncParam;
  }

  public void ApplySyncParam(ContinusAttackParam.SyncParam syncParam)
  {
    if (syncParam == null)
    {
      this.RemoveAll();
    }
    else
    {
      List<ContinusAttackParam.SyncData> syncDataList = syncParam.syncDataList;
      if (syncDataList == null)
      {
        this.RemoveAll();
      }
      else
      {
        foreach (ContinusAttackParam.SyncData syncData in syncDataList)
        {
          ContinusAttackParam.ContinusAtkData continusAtkData = this.SearchByIndex(syncData.eventIndex);
          if (continusAtkData != null)
            continusAtkData.endTime = syncData.endTime;
          else
            this.m_owner.CreateContinusAttackBySyncData(syncData);
        }
      }
    }
  }

  [Serializable]
  public class ContinusAtkData
  {
    public int eventIndex;
    public float endTime;
    public AnimEventCollider eventCollider;
    public Transform effectTrans;

    public void Release()
    {
      if (Object.op_Inequality((Object) this.effectTrans, (Object) null))
      {
        EffectManager.ReleaseEffect(((Component) this.effectTrans).gameObject);
        this.effectTrans = (Transform) null;
      }
      if (this.eventCollider == null)
        return;
      this.eventCollider.Destroy();
      this.eventCollider = (AnimEventCollider) null;
    }
  }

  [Serializable]
  public class SyncParam
  {
    public List<ContinusAttackParam.SyncData> syncDataList = new List<ContinusAttackParam.SyncData>();
  }

  [Serializable]
  public class SyncData
  {
    public int eventIndex;
    public float endTime;
  }
}
