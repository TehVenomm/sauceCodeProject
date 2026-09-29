// Decompiled with JetBrains decompiler
// Type: FieldChatGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldChatGimmickObject : FieldGimmickObject
{
  public const string kTargetEffectName = "ef_btl_target_readstory_01";
  private Transform targetMarker;
  private Transform _transform;
  private UIChatGimmickGizmo _gizmo;
  private List<string> messageList = new List<string>();
  private float markerOffsetY;
  private float chatOffsetY;
  private float sqlRadius = 9f;
  private float npcScale = 1f;
  private int npcId = -1;

  public static bool IsValid(string value2)
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || value2.IsNullOrWhiteSpace())
      return false;
    uint result1 = 0;
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
          case "sdid":
            uint.TryParse(strArray[1], out result1);
            continue;
          case "edid":
            uint.TryParse(strArray[1], out result2);
            continue;
          default:
            continue;
        }
      }
    }
    return (result2 == 0U || !MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2)) && (result1 == 0U || MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result1));
  }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      this._gizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateGimmick(this);
    if (!Singleton<NPCTable>.IsValid())
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
          case "cy":
            float.TryParse(strArray[1], out this.chatOffsetY);
            continue;
          case "m":
            if (!strArray[1].IsNullOrWhiteSpace())
            {
              this.messageList.Add(strArray[1]);
              continue;
            }
            continue;
          case "mid":
            uint result;
            if (uint.TryParse(strArray[1], out result))
            {
              string self = StringTable.Get(STRING_CATEGORY.GIMMICK, result);
              if (!self.IsNullOrWhiteSpace())
              {
                this.messageList.Add(self);
                continue;
              }
              continue;
            }
            continue;
          case "npcId":
            int.TryParse(strArray[1], out this.npcId);
            continue;
          case "npcScale":
            float.TryParse(strArray[1], out this.npcScale);
            continue;
          case "r":
            float.TryParse(strArray[1], out this.sqlRadius);
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
    if (isNear && this.IsValid())
    {
      if (Object.op_Equality((Object) this.targetMarker, (Object) null) && !string.IsNullOrEmpty("ef_btl_target_readstory_01"))
        this.targetMarker = EffectManager.GetEffect("ef_btl_target_readstory_01", this._transform);
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

  public bool StartChat()
  {
    if (!this.IsValid())
      return false;
    this._gizmo.SayChat(this.messageList[Random.Range(0, this.messageList.Count)]);
    return true;
  }

  public override string GetObjectName() => "ChatGimmick";

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  private bool IsValid()
  {
    if (this.messageList.IsNullOrEmpty<string>() || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || Object.op_Equality((Object) this._gizmo, (Object) null) || this._gizmo.isDisp())
      return false;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    return !Object.op_Equality((Object) self, (Object) null) && !self.isDead;
  }

  public override float GetTargetSqrRadius() => this.sqlRadius;

  public Vector3 GetPosition()
  {
    Vector3 position = this._transform.position;
    position.y += this.chatOffsetY;
    return position;
  }
}
