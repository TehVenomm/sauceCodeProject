// Decompiled with JetBrains decompiler
// Type: FieldReadStoryObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldReadStoryObject : FieldGimmickObject
{
  private Transform targetMarker;
  private Transform _transform;
  private uint deliveryId;
  private List<int> subStoryIds = new List<int>();
  private uint endDeliveryId;
  private float markerOffsetY;
  private float sqlRadius = 4f;
  private float npcScale = 1f;
  private int npcId = -1;

  public static bool IsValid(string value2)
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || value2.IsNullOrWhiteSpace())
      return false;
    uint result1 = 0;
    int num = 0;
    uint result2 = 0;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "did":
            uint.TryParse(strArray[1], out result1);
            continue;
          case "sid":
            int result3 = 0;
            if (int.TryParse(strArray[1], out result3))
            {
              num = result3;
              continue;
            }
            continue;
          case "edid":
            uint.TryParse(strArray[1], out result2);
            continue;
          default:
            continue;
        }
      }
    }
    if (result1 == 0U && num == 0 || result2 != 0U && MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2))
      return false;
    if (result1 != 0U)
    {
      if (!MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result1))
        return false;
      if (num == 0)
        return !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(result1) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) result1);
    }
    return true;
  }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    if (this.npcId == -1 || !Singleton<NPCTable>.IsValid())
      return;
    Singleton<NPCTable>.I.GetNPCData(this.npcId)?.LoadModel(((Component) this).gameObject, true, true, (Action<Animator>) (animator =>
    {
      Transform transform = ((Component) animator).gameObject.transform;
      transform.localScale = Vector3.op_Multiply(transform.localScale, this.npcScale);
    }), false);
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "did":
            uint.TryParse(strArray[1], out this.deliveryId);
            continue;
          case "edid":
            uint.TryParse(strArray[1], out this.endDeliveryId);
            continue;
          case "npcId":
            int.TryParse(strArray[1], out this.npcId);
            continue;
          case "npcScale":
            float.TryParse(strArray[1], out this.npcScale);
            continue;
          case "r":
            if (!float.TryParse(strArray[1], out this.sqlRadius))
            {
              this.sqlRadius = 4f;
              continue;
            }
            continue;
          case "sid":
            int result;
            if (int.TryParse(strArray[1], out result))
            {
              this.subStoryIds.Add(result);
              continue;
            }
            continue;
          case "ty":
            float.TryParse(strArray[1], out this.markerOffsetY);
            continue;
          default:
            continue;
        }
      }
    }
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 39))
    {
      string targetEffectName = ResourceName.GetReadStoryTargetEffectName();
      if (Object.op_Equality((Object) this.targetMarker, (Object) null) && !string.IsNullOrEmpty(targetEffectName))
        this.targetMarker = EffectManager.GetEffect(targetEffectName, this._transform);
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      Vector3 pos = Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this._transform.position);
      pos.y += this.markerOffsetY;
      this.targetMarker.Set(pos, rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetMarker).gameObject);
    }
  }

  public void StartReadStory()
  {
    if (!this.IsValidReadStory() || !Singleton<DeliveryTable>.IsValid())
      return;
    int storyId = 0;
    bool isSend = true;
    if (this.deliveryId != 0U && !MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(this.deliveryId) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) this.deliveryId))
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(this.deliveryId);
      if (deliveryTableData != null && deliveryTableData.readScriptId != 0U)
      {
        storyId = (int) deliveryTableData.readScriptId;
        isSend = true;
      }
    }
    if (storyId == 0 && this.subStoryIds.Count > 0)
    {
      storyId = this.subStoryIds[Random.Range(0, this.subStoryIds.Count)];
      isSend = false;
    }
    if (storyId == 0)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory(storyId, isSend);
  }

  public override string GetObjectName() => "ReadStory";

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  private bool IsValidReadStory()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    return !Object.op_Equality((Object) self, (Object) null) && !self.isDead;
  }

  public override float GetTargetSqrRadius() => this.sqlRadius;
}
