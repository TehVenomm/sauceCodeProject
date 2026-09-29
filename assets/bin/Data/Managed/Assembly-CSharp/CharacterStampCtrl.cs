// Decompiled with JetBrains decompiler
// Type: CharacterStampCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using UnityEngine;

#nullable disable
public class CharacterStampCtrl : MonoBehaviour
{
  private bool isPlayer;
  private bool isSelf;
  public StageObject.StampInfo[] stampInfos;
  public bool enableAutoStampEffect = true;
  public float stampDistance = 5f;
  public int effectLayer = -1;

  public Transform _transform { get; private set; }

  public Character owner { get; private set; }

  public bool isDirection { get; protected set; }

  public StampNode[] stampNodes { get; set; }

  public void Init(StageObject.StampInfo[] stamp_nodes, Character _owner, bool is_direction = false)
  {
    this.stampInfos = stamp_nodes;
    this.isDirection = is_direction;
    this._transform = ((Component) this).transform;
    this.owner = _owner;
    this.isPlayer = _owner is Player;
    this.isSelf = _owner is Self;
    this.enableAutoStampEffect = true;
    this.stampNodes = ((Component) this).gameObject.GetComponentsInChildren<StampNode>();
  }

  private void Update()
  {
    if (!this.isDirection && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0 || !this.isDirection && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1 && FieldManager.IsValidInGameNoQuest() && this.isPlayer && !this.isSelf)
      return;
    bool flag = false;
    if (this.isDirection || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 2)
      flag = true;
    if (this.stampNodes == null || this.stampNodes.Length == 0 || this.stampInfos == null || this.stampInfos.Length == 0 || !flag && !this.CheckDistance())
      return;
    float y = this._transform.position.y;
    int index = 0;
    for (int length = this.stampNodes.Length; index < length; ++index)
    {
      StampNode stampNode = this.stampNodes[index];
      if (stampNode.UpdateStamp(y) && this.enableAutoStampEffect)
        this.PlayStampEffect(!Object.op_Inequality((Object) this.owner, (Object) null) ? this.stampInfos[0] : (this.owner.actionID != Character.ACTION_ID.ATTACK || this.stampInfos.Length < 2 ? this.stampInfos[0] : this.stampInfos[1]), stampNode);
    }
  }

  public bool OnAnimEvent(AnimEventData.EventData data)
  {
    switch (data.id)
    {
      case AnimEventFormat.ID.STAMP:
        if (!this.CheckDistance())
          return true;
        int intArg = data.intArgs[0];
        if (this.stampInfos == null || this.stampNodes == null)
          return true;
        int index1 = 0;
        for (int length1 = this.stampNodes.Length; index1 < length1; ++index1)
        {
          StampNode stampNode = this.stampNodes[index1];
          int index2 = 0;
          for (int length2 = stampNode.triggers.Length; index2 < length2; ++index2)
          {
            StampNode.StampTrigger trigger = stampNode.triggers[index2];
            if (trigger.eventID == intArg)
            {
              this.PlayStampEffect(this.stampInfos[trigger.StampInfoID], stampNode);
              break;
            }
          }
        }
        return true;
      case AnimEventFormat.ID.AUTO_STAMP_ON:
        this.enableAutoStampEffect = true;
        return true;
      case AnimEventFormat.ID.AUTO_STAMP_OFF:
        this.enableAutoStampEffect = false;
        return true;
      default:
        return false;
    }
  }

  protected void PlayStampEffect(StageObject.StampInfo stamp_info, StampNode stamp_node)
  {
    Vector3 pos = StageManager.FitHeight(Vector3.op_Addition(stamp_node._transform.position, Quaternion.op_Multiply(stamp_node._transform.rotation, stamp_node.scaledeOffset)));
    string effectName = stamp_info.effectName;
    if (!string.IsNullOrEmpty(effectName))
      EffectManager.OneShot(effectName, pos, this._transform.rotation, Vector3.op_Multiply(this._transform.localScale, stamp_info.effectScale), this.isSelf, (Action<Transform>) (effect =>
      {
        SceneSettingsManager.ApplyEffect(((Component) effect).gameObject.GetComponent<rymFX>(), true);
        if (this.effectLayer == -1)
          return;
        Utility.SetLayerWithChildren(effect, this.effectLayer);
      }));
    if ((double) stamp_info.shakeCameraPercent > 0.0 && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(pos, stamp_info.shakeCameraPercent, stamp_info.shakeCycleTime);
    if (stamp_info.seID == 0)
      return;
    SoundManager.PlayOneShotSE(stamp_info.seID, pos);
  }

  private bool CheckDistance()
  {
    return !MonoBehaviourSingleton<InGameCameraManager>.IsValid() || (double) Vector3.Distance(MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.position, this._transform.position) < (double) this.stampDistance;
  }
}
