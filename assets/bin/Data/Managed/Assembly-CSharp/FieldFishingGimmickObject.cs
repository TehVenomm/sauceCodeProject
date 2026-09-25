// Decompiled with JetBrains decompiler
// Type: FieldFishingGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldFishingGimmickObject : FieldGatherGimmickObject
{
  public const string kFishingMarkerName = "ef_btl_target_fishing_01";
  public const string kFishingEffectGet = "ef_btl_fishing_03";
  public const string kFishingToolModelName = "Fishingrod";
  public const string kFishingToolNodeName = "R_Wep";
  private const int kDefaultModelIndex = 0;
  protected int _modelIndex;

  public int modelIndex => this._modelIndex;

  public static string[] GetEffectNames(int index)
  {
    List<string> stringList = new List<string>();
    stringList.Add("ef_btl_target_fishing_01");
    stringList.Add("ef_btl_fishing_03");
    InGameSettingsManager.FishingParam fishingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
    stringList.Add(fishingParam.omenEffect[index]);
    stringList.Add(fishingParam.hookEffect[index]);
    stringList.Add("ef_btl_target_fishing_02");
    return stringList.ToArray();
  }

  public static int GetModelIndex(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return 0;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "mi")
      {
        int result = 0;
        if (int.TryParse(strArray[1], out result))
          return result;
      }
    }
    return 0;
  }

  public static string ConvertModelIndexToName(int idx) => $"CMN_fishing{idx + 1:D2}";

  protected override void ParseParam(string value2)
  {
    this._modelIndex = 0;
    base.ParseParam(value2);
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "mi")
        int.TryParse(strArray[1], out this._modelIndex);
    }
  }

  public override string GetObjectName() => "Fishing";

  public override string GetMarkerName() => "ef_btl_target_fishing_01";

  public override GATHER_GIMMICK_TYPE GetGatherGimmickType() => GATHER_GIMMICK_TYPE.FISHING;

  public override Character.ACTION_ID GetTargetActionId() => (Character.ACTION_ID) 40;

  public override bool StartAction(Player player, bool isSend)
  {
    if (!base.StartAction(player, isSend))
      return false;
    Vector3 vector3 = Vector3.op_Subtraction(player._transform.position, this.m_transform.position);
    if ((double) ((Vector3) ref vector3).sqrMagnitude < (double) this.sqlRadius)
      player._transform.position = Vector3.op_Addition(this.m_transform.position, Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.radius), 0.9f));
    return true;
  }
}
