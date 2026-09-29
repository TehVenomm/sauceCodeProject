// Decompiled with JetBrains decompiler
// Type: AnimEventData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AnimEventData : ScriptableObject
{
  public const string ANIMATOR_DEF_LAYER_NAME = "Base Layer.";
  public const float FIRST_EXECUTE_TIME = -3.40282347E+38f;
  public const float LAST_EXECUTE_TIME = 3.402823E+38f;
  public AnimEventData.AnimData[] animations = new AnimEventData.AnimData[0];
  [NonSerialized]
  private int[] hashs;
  public static int[] idHashs;
  public static int[] ids;
  public AnimEventData.ResidentEffectData[] residentEffectDataList;

  public void Initialize()
  {
    int length1 = this.animations.Length;
    int[] hashs = this.hashs;
    if (hashs != null && hashs.Length >= length1)
      return;
    int[] numArray;
    this.hashs = numArray = new int[length1];
    for (int index1 = 0; index1 < length1; ++index1)
    {
      numArray[index1] = Animator.StringToHash(this.animations[index1].GetAnimName());
      AnimEventData.AnimData animation = this.animations[index1];
      AnimEventData.EventData[] events = this.animations[index1].events;
      if (!animation.initIDs)
      {
        int index2 = 0;
        for (int length2 = events.Length; index2 < length2; ++index2)
          events[index2].id = AnimEventData.StringToID(events[index2].name);
        animation.initIDs = true;
      }
    }
  }

  public AnimEventData.EventData[] GetEventDatas(int hash)
  {
    int length1 = this.animations.Length;
    if (length1 == 0)
      return (AnimEventData.EventData[]) null;
    int[] numArray = this.hashs;
    if (numArray == null || numArray.Length < length1)
    {
      this.hashs = numArray = new int[length1];
      for (int index = 0; index < length1; ++index)
        numArray[index] = Animator.StringToHash(this.animations[index].GetAnimName());
    }
    for (int index1 = 0; index1 < length1; ++index1)
    {
      if (numArray[index1] == hash)
      {
        AnimEventData.AnimData animation = this.animations[index1];
        AnimEventData.EventData[] events = this.animations[index1].events;
        if (!animation.initIDs)
        {
          int index2 = 0;
          for (int length2 = events.Length; index2 < length2; ++index2)
            events[index2].id = AnimEventData.StringToID(events[index2].name);
          animation.initIDs = true;
        }
        return events;
      }
    }
    return (AnimEventData.EventData[]) null;
  }

  static AnimEventData()
  {
    string[] names = Enum.GetNames(typeof (AnimEventFormat.ID));
    int length = names != null ? names.Length : 0;
    AnimEventData.idHashs = new int[length];
    for (int index = 0; index < length; ++index)
      AnimEventData.idHashs[index] = Animator.StringToHash(names[index]);
    AnimEventData.ids = (int[]) Enum.GetValues(typeof (AnimEventFormat.ID));
  }

  public static AnimEventFormat.ID StringToID(string name)
  {
    int hash = Animator.StringToHash(name);
    int index = 0;
    for (int length = AnimEventData.idHashs.Length; index < length; ++index)
    {
      if (AnimEventData.idHashs[index] == hash)
        return (AnimEventFormat.ID) AnimEventData.ids[index];
    }
    return ~AnimEventFormat.ID.SHOT_ARROW;
  }

  public AnimEventData.ResidentEffectData AddResidentEffectData()
  {
    List<AnimEventData.ResidentEffectData> residentEffectDataList = new List<AnimEventData.ResidentEffectData>();
    if (this.residentEffectDataList != null && this.residentEffectDataList.Length != 0)
      residentEffectDataList.AddRange((IEnumerable<AnimEventData.ResidentEffectData>) this.residentEffectDataList);
    AnimEventData.ResidentEffectData residentEffectData = new AnimEventData.ResidentEffectData();
    residentEffectData.effectName = string.Empty;
    residentEffectData.linkNodeName = string.Empty;
    residentEffectData.offsetPos = Vector3.zero;
    residentEffectData.offsetRot = Vector3.zero;
    residentEffectData.groupID = 0;
    residentEffectData.handle = 0;
    residentEffectData.scale = 1f;
    residentEffectDataList.Add(residentEffectData);
    this.residentEffectDataList = residentEffectDataList.ToArray();
    return residentEffectData;
  }

  public void DeleteResidentEffectData(AnimEventData.ResidentEffectData targetData)
  {
    List<AnimEventData.ResidentEffectData> residentEffectDataList = new List<AnimEventData.ResidentEffectData>();
    if (this.residentEffectDataList != null && this.residentEffectDataList.Length != 0)
      residentEffectDataList.AddRange((IEnumerable<AnimEventData.ResidentEffectData>) this.residentEffectDataList);
    if (residentEffectDataList.Contains(targetData))
      residentEffectDataList.Remove(targetData);
    if (residentEffectDataList.Count > 0)
      this.residentEffectDataList = residentEffectDataList.ToArray();
    else
      this.residentEffectDataList = (AnimEventData.ResidentEffectData[]) null;
  }

  [Serializable]
  public class EventData
  {
    public string name;
    public float time;
    public int[] intArgs;
    public float[] floatArgs;
    public string[] stringArgs;
    [NonSerialized]
    public AnimEventFormat.ID id;
    [NonSerialized]
    public Player.ATTACK_MODE attackMode;

    public int GetInt(int index, int defVal = 0) => !this.HasInt(0) ? defVal : this.intArgs[index];

    public float GetFloat(int index, float defVal = 0.0f)
    {
      return !this.HasFloat(0) ? defVal : this.floatArgs[index];
    }

    public string GetString(int index, string defVal = "")
    {
      return !this.HasString(0) ? defVal : this.stringArgs[index];
    }

    public bool HasInt(int index)
    {
      return this.intArgs != null && index >= 0 && index < this.intArgs.Length;
    }

    public bool HasFloat(int index)
    {
      return this.floatArgs != null && index >= 0 && index < this.floatArgs.Length;
    }

    public bool HasString(int index)
    {
      return this.stringArgs != null && index >= 0 && index < this.stringArgs.Length;
    }

    public void Copy(AnimEventData.EventData from_data, bool with_time)
    {
      if (with_time)
        this.time = from_data.time;
      this.id = from_data.id;
      this.name = from_data.name;
      this.intArgs = from_data.intArgs != null ? (int[]) from_data.intArgs.Clone() : (int[]) null;
      this.floatArgs = from_data.floatArgs != null ? (float[]) from_data.floatArgs.Clone() : (float[]) null;
      this.stringArgs = from_data.stringArgs != null ? (string[]) from_data.stringArgs.Clone() : (string[]) null;
    }
  }

  [Serializable]
  public class AnimData
  {
    public string name;
    public string layerName = "Base Layer.";
    public AnimEventData.EventData[] events;
    [NonSerialized]
    public bool initIDs;

    public string GetAnimName() => this.layerName + this.name;

    public string GetUniqueName() => this.layerName.Substring("Base Layer.".Length) + this.name;
  }

  [Serializable]
  public class ResidentEffectData
  {
    public string effectName;
    public string linkNodeName;
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public int groupID;
    public int handle;
    public float scale = 1f;

    public void Copy(AnimEventData.ResidentEffectData srcInfo)
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
