// Decompiled with JetBrains decompiler
// Type: SystemEffectSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class SystemEffectSetting : ScriptableObject
{
  public int[] startGroupIds;
  public SystemEffectSetting.Data[] effectDataList;

  public SystemEffectSetting.Data AddNewData()
  {
    List<SystemEffectSetting.Data> dataList = new List<SystemEffectSetting.Data>();
    if (this.effectDataList != null && this.effectDataList.Length != 0)
      dataList.AddRange((IEnumerable<SystemEffectSetting.Data>) this.effectDataList);
    SystemEffectSetting.Data data = new SystemEffectSetting.Data();
    data.effectName = string.Empty;
    data.linkNodeName = string.Empty;
    data.offsetPos = Vector3.zero;
    data.offsetRot = Vector3.zero;
    data.groupID = 0;
    data.handle = 0;
    data.scale = 1f;
    dataList.Add(data);
    this.effectDataList = dataList.ToArray();
    return data;
  }

  public void DeleteData(SystemEffectSetting.Data targetData)
  {
    List<SystemEffectSetting.Data> dataList = new List<SystemEffectSetting.Data>();
    if (this.effectDataList != null && this.effectDataList.Length != 0)
      dataList.AddRange((IEnumerable<SystemEffectSetting.Data>) this.effectDataList);
    if (dataList.Contains(targetData))
      dataList.Remove(targetData);
    if (dataList.Count > 0)
      this.effectDataList = dataList.ToArray();
    else
      this.effectDataList = (SystemEffectSetting.Data[]) null;
  }

  [Serializable]
  public class Data
  {
    public string effectName;
    public string linkNodeName;
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public int groupID;
    public int handle;
    public float scale = 1f;

    public void Copy(SystemEffectSetting.Data srcInfo)
    {
      this.effectName = srcInfo.effectName;
      this.linkNodeName = srcInfo.linkNodeName;
      this.offsetPos = srcInfo.offsetPos;
      this.offsetRot = srcInfo.offsetRot;
      this.groupID = srcInfo.groupID;
      this.handle = srcInfo.handle;
      this.scale = srcInfo.scale;
    }

    public string UniqueName => this.effectName + this.linkNodeName + (object) this.groupID;
  }
}
