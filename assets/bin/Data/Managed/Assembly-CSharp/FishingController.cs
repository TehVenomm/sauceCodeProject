// Decompiled with JetBrains decompiler
// Type: FishingController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FishingController
{
  private readonly uint kItemHitStringId = 50;
  private readonly uint kAccessoryHitStringId = 51;
  private readonly uint kDontHitStringId = 100;
  private FishingController.eState state;
  private Player owner;
  private bool isSelf;
  private InGameSettingsManager.FishingParam param;
  private int lotId;
  private float waitTime;
  private int omenNum;
  private float omenInterval;
  private float hookTime;
  private float sendTime;
  private int timing = -1;
  private bool isSend;
  private string hitStr = "";
  private string enemyHitStr = "";
  private bool isFightCompleteSend;
  private FishingController.eHitType hitType;
  private PopSignatureInfo popInfo;
  private int gatherGimickModelIndex = 1;
  private Transform gatherGimickTrans;
  private Transform effectHookTrans;
  private float coopFishingGaugeCurrent;
  private float coopFishingGaugeMax;
  private float coopFishingGaugeDecreaseTimer;
  private float coopFishingGaugeNegativeTimer;
  private bool isGaugePositive = true;
  private FieldGimmickCoopFishing fieldGimmickCoopFishingObject;
  private int coopOwnerUserId;
  private int coopOwnerPlayerId;
  private int coopOwnerClientId;
  private bool isRoutineStamp;
  private float stampRoutineSec;

  public void Initialize(Player player)
  {
    this.owner = player;
    this.isSelf = player is Self;
    this.state = FishingController.eState.None;
    this.param = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
    this.coopFishingGaugeMax = this.param.coopFishingGaugeMax;
    this.coopFishingGaugeDecreaseTimer = this.param.coopFishingGaugeMarginSecToStartDecrease;
    this.coopFishingGaugeNegativeTimer = this.param.coopFishingGaugeMarginToStartChangeRed;
  }

  public void TryFinalize()
  {
    this._DestroyCoopFieldGimmick();
    if (this.coopOwnerPlayerId == 0)
      this._MakeOtherCoopFailed();
    this.owner = (Player) null;
    this.state = FishingController.eState.None;
    this.param = (InGameSettingsManager.FishingParam) null;
  }

  public bool CanFishing() => this.state == FishingController.eState.None;

  public bool IsFishing() => this.state != 0;

  public bool IsFighting() => this.state == FishingController.eState.Fight;

  public bool IsCooperating() => this.state == FishingController.eState.Coop;

  public int GetStateForInitialize() => (int) this.state;

  public float GetMaxWaitPacketSec()
  {
    return this.param == null ? 20f : (float) ((double) this.param.waitSec[1] + (double) (this.param.maxOmenNum - 1) * (double) this.param.omenInterval + (double) this.param.hookSec + ((double) this.param.sendMinSec + (double) this.param.sendCrownTypeSec[2] + (double) this.param.sendSec[3] + (double) this.param.sendRareSec));
  }

  public float GetCoopFishingGaugeRate()
  {
    return Mathf.Clamp01(this.coopFishingGaugeCurrent / this.coopFishingGaugeMax);
  }

  public bool IsCoopFishingGaugeFull()
  {
    return (double) this.coopFishingGaugeMax <= (double) this.coopFishingGaugeCurrent;
  }

  public bool IsCoopFishingGaugeEmpty() => (double) this.coopFishingGaugeCurrent <= 0.0;

  public void AddCoopFishingGauge(bool isPacket = false)
  {
    this.coopFishingGaugeCurrent += isPacket ? this.param.coopFishingGaugeIncreasePerTapByOther : this.param.coopFishingGaugeIncreasePerTapBySelf;
    this.coopFishingGaugeDecreaseTimer = this.param.coopFishingGaugeMarginSecToStartDecrease;
    this.coopFishingGaugeNegativeTimer = this.param.coopFishingGaugeMarginToStartChangeRed;
    this.isGaugePositive = true;
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnCoopFishingGaugeSync(this.coopOwnerUserId, this.coopFishingGaugeCurrent);
  }

  public bool IsGaugePositive() => this.isGaugePositive;

  private void _ShowWeapon(bool isShow)
  {
    this.owner.SetEnableNodeRenderer("R_Wep", isShow);
    this.owner.SetEnableNodeRenderer("L_Wep", isShow);
  }

  public void Start(int id, Transform ggTrans, int modelIndex)
  {
    this.gatherGimickTrans = ggTrans;
    this.gatherGimickModelIndex = modelIndex;
    this.lotId = id;
    this.hitStr = "";
    this.hitType = FishingController.eHitType.None;
    this.waitTime = 0.0f;
    this.omenNum = 0;
    this.omenInterval = 0.0f;
    this.hookTime = 0.0f;
    this.sendTime = 0.0f;
    this.timing = -1;
    this.isSend = false;
    this.coopFishingGaugeCurrent = this.param.coopFishingGaugeInitial;
    this.owner._rigidbody.isKinematic = true;
    this._ShowWeapon(false);
    this._SetExclamation();
    this.ChangeState(FishingController.eState.Wait);
  }

  public void End()
  {
    if (this.state == FishingController.eState.None)
      return;
    this.owner._rigidbody.isKinematic = false;
    this._ShowWeapon(true);
    if (Object.op_Inequality((Object) this.effectHookTrans, (Object) null))
      EffectManager.ReleaseEffect(ref this.effectHookTrans);
    this.effectHookTrans = (Transform) null;
    this._DispExclamation(false);
    this._DestroyCoopFieldGimmick();
    this.owner.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_GATHER_GIMMICK);
    this.ClearIds();
    this.state = FishingController.eState.None;
  }

  public void Get()
  {
    if (this.state != FishingController.eState.Hit || this.hitType == FishingController.eHitType.Enemy)
      return;
    if (this.hitType == FishingController.eHitType.Item || this.hitType == FishingController.eHitType.GatherItem)
      EffectManager.OneShot("ef_btl_fishing_03", this.owner.FindNode("L_Hand").position, Quaternion.identity);
    if (!this.isSelf)
      return;
    SoundManager.PlayOneShotSE(this.param.hitSeIds[(int) this.hitType], (DisableNotifyMonoBehaviour) this.owner, this.owner.FindNode(""));
    UIInGamePopupDialog.PushOpen(this.hitStr, false);
  }

  public void CoopStart()
  {
    this.hitStr = "";
    this.hitType = FishingController.eHitType.None;
    this.waitTime = 0.0f;
    this.omenNum = 0;
    this.omenInterval = 0.0f;
    this.hookTime = 0.0f;
    this.sendTime = 0.0f;
    this.timing = -1;
    this.isSend = false;
    this.isFightCompleteSend = false;
    this.coopFishingGaugeCurrent = this.param.coopFishingGaugeInitial;
    this.owner._rigidbody.isKinematic = true;
    this._ShowWeapon(false);
    this.ChangeState(FishingController.eState.Coop);
  }

  public void CoopEnd()
  {
    if (!this.IsFishing())
      return;
    this.owner._rigidbody.isKinematic = false;
    this._ShowWeapon(true);
    if (Object.op_Inequality((Object) this.effectHookTrans, (Object) null))
      EffectManager.ReleaseEffect(ref this.effectHookTrans);
    this.effectHookTrans = (Transform) null;
    this._DestroyCoopFieldGimmick();
    this.ClearIds();
    this.state = FishingController.eState.None;
  }

  public void OnReaction()
  {
    if (this.coopOwnerPlayerId > 0)
    {
      this.CoopEnd();
    }
    else
    {
      SoundManager.StopLoopSE(this.GetSeId(2), (DisableNotifyMonoBehaviour) this.owner);
      this._MakeOtherCoopFailed();
    }
  }

  public int GetSeId(int type)
  {
    switch (type)
    {
      case 0:
        return this.param.se0Id[this.gatherGimickModelIndex];
      case 1:
        return this.param.se1Id[this.gatherGimickModelIndex];
      case 2:
        return this.param.se2Id[this.gatherGimickModelIndex];
      case 3:
        return this.param.se3Id[this.gatherGimickModelIndex];
      default:
        return 0;
    }
  }

  public void ChangeState(FishingController.eState s)
  {
    switch (s)
    {
      case FishingController.eState.Wait:
        this._ChangeWait();
        break;
      case FishingController.eState.Omen:
        this._ChangeOmen();
        break;
      case FishingController.eState.Hook:
        this._ChangeHook();
        break;
      case FishingController.eState.Send:
        this._ChangeSend();
        break;
      case FishingController.eState.Hit:
        this._ChangeHit();
        break;
      case FishingController.eState.Quit:
        this._ChangeQuit();
        break;
      case FishingController.eState.Fight:
        this._ChangeFight();
        break;
      case FishingController.eState.FightSuccess:
        this._ChangeFightSuccess();
        break;
      case FishingController.eState.FightFailed:
        this._ChangeFightFailed();
        break;
      case FishingController.eState.Coop:
        this._ChangeCoop();
        break;
      case FishingController.eState.CoopSuccess:
        this._ChangeCoopSuccess();
        break;
      case FishingController.eState.CoopFailed:
        this._ChangeCoopFailed();
        break;
    }
    this.state = s;
    if (!this.isSelf || !Object.op_Inequality((Object) this.owner, (Object) null) || !Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnGatherGimmickState((int) this.state);
  }

  public void Update()
  {
    if (!this.isSelf)
      return;
    switch (this.state)
    {
      case FishingController.eState.Wait:
        this._UpdateWait();
        break;
      case FishingController.eState.Omen:
        this._UpdateOmen();
        break;
      case FishingController.eState.Hook:
        this._UpdateHook();
        break;
      case FishingController.eState.Send:
        this._UpdateSend();
        break;
      case FishingController.eState.Fight:
        this._UpdateFight();
        break;
      case FishingController.eState.FightSuccess:
        this._UpdateFightSuccess();
        break;
      case FishingController.eState.FightFailed:
        this._UpdateFightFailed();
        break;
      case FishingController.eState.Coop:
        this._UpdateCoop();
        break;
      case FishingController.eState.CoopSuccess:
        this._UpdateCoopSuccess();
        break;
      case FishingController.eState.CoopFailed:
        this._UpdateCoopFailed();
        break;
    }
  }

  public void Tap()
  {
    switch (this.state)
    {
      case FishingController.eState.Wait:
      case FishingController.eState.Omen:
        this._TapWait();
        break;
      case FishingController.eState.Hook:
        this._TapHook();
        break;
      case FishingController.eState.Fight:
        this._TapFight();
        break;
      case FishingController.eState.Coop:
        this._TapCoop();
        break;
    }
  }

  private void _ChangeWait()
  {
    this.waitTime = Random.Range(this.param.waitSec[0], this.param.waitSec[1]);
  }

  private void _UpdateWait()
  {
    this.waitTime -= Time.deltaTime;
    if ((double) this.waitTime > 0.0)
      return;
    this.ChangeState(FishingController.eState.Omen);
  }

  private void _TapWait() => this.ChangeState(FishingController.eState.Quit);

  private void _ChangeOmen()
  {
    this.omenNum = Random.Range(0, this.param.maxOmenNum);
    this.omenInterval = 0.0f;
  }

  private void _UpdateOmen()
  {
    this.omenInterval -= Time.deltaTime;
    if ((double) this.omenInterval > 0.0)
      return;
    SoundManager.PlayOneShotSE(this.GetSeId(1), (DisableNotifyMonoBehaviour) this.owner, this.owner.FindNode(""));
    EffectManager.OneShot(this.param.omenEffect[this.gatherGimickModelIndex], this.gatherGimickTrans.position, Quaternion.identity);
    if (--this.omenNum < 0)
      this.ChangeState(FishingController.eState.Hook);
    else
      this.omenInterval = this.param.omenInterval;
  }

  private void _ChangeHook()
  {
    this.hookTime = this.param.hookSec;
    this._DispExclamation(true);
  }

  private void _UpdateHook()
  {
    this.hookTime -= Time.deltaTime;
    if ((double) this.hookTime > 0.0)
      return;
    this.ChangeState(FishingController.eState.Send);
  }

  private void _TapHook()
  {
    this.timing = (int) ((1.0 - (double) this.hookTime / (double) this.param.hookSec) * 100.0);
    this.ChangeState(FishingController.eState.Send);
  }

  private void _ChangeSend()
  {
    this._DispExclamation(false);
    this.owner.SetNextTrigger();
    this.sendTime = this.param.sendMinSec;
    this.hitType = FishingController.eHitType.None;
    int isPop = MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle() ? 1 : 0;
    this.isSend = false;
    if (!this.isSelf)
      return;
    MonoBehaviourSingleton<FieldManager>.I.SendFieldGatherGimmick(this.lotId, this.timing, isPop, (Action<bool, FieldFishModel.Param>) ((is_success, retParam) =>
    {
      this.isSend = true;
      if (retParam.hitBoss > 0)
      {
        this.ChangeState(FishingController.eState.Fight);
      }
      else
      {
        FieldGatherRewardList reward = retParam.reward;
        if (reward != null && reward.fieldGather != null)
        {
          if (!reward.fieldGather.gatherItem.IsNullOrEmpty<QuestCompleteReward.GatherItem>())
          {
            int index = 0;
            for (int count = reward.fieldGather.gatherItem.Count; index < count; ++index)
            {
              QuestCompleteReward.GatherItem gatherItem = reward.fieldGather.gatherItem[index];
              if (gatherItem != null)
              {
                GatherItemTable.GatherItemData data = Singleton<GatherItemTable>.I.GetData((uint) gatherItem.gatherItemId);
                if (data != null)
                {
                  this.sendTime += this.param.sendSec[2];
                  this.sendTime += this.param.sendCrownTypeSec[gatherItem.maxCrownType];
                  if (data.isRare == 1)
                    this.sendTime += this.param.sendRareSec;
                  string str = GatherItemRecord.ShapeSize(gatherItem.score);
                  this.hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, (uint) gatherItem.status), (object) data.name, (object) str);
                  if (gatherItem.psig != null)
                  {
                    this.enemyHitStr = this.hitStr;
                    this.hitType = FishingController.eHitType.Enemy;
                    this.popInfo = gatherItem.psig;
                    return;
                  }
                  this.hitType = FishingController.eHitType.GatherItem;
                  return;
                }
              }
            }
          }
          int index1 = 0;
          for (int count = reward.fieldGather.item.Count; index1 < count; ++index1)
          {
            QuestCompleteReward.Item obj = reward.fieldGather.item[index1];
            if (obj != null)
            {
              ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) obj.itemId);
              if (itemData != null)
              {
                this.hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, this.kItemHitStringId), (object) itemData.name, (object) MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint) obj.itemId));
                this.hitType = FishingController.eHitType.Item;
                this.sendTime += this.param.sendSec[1];
                return;
              }
            }
          }
          int index2 = 0;
          for (int count = reward.fieldGather.accessoryItem.Count; index2 < count; ++index2)
          {
            QuestCompleteReward.AccessoryItem accessoryItem = reward.fieldGather.accessoryItem[index2];
            if (accessoryItem != null)
            {
              AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint) accessoryItem.accessoryId);
              if (data != null)
              {
                this.hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, this.kAccessoryHitStringId), (object) data.name);
                this.hitType = FishingController.eHitType.Item;
                this.sendTime += this.param.sendSec[1];
                return;
              }
            }
          }
        }
        this.hitStr = StringTable.Get(STRING_CATEGORY.FISHING, this.kDontHitStringId);
        this.hitType = FishingController.eHitType.None;
        this.sendTime += this.param.sendSec[0];
      }
    }));
    if (Object.op_Inequality((Object) this.effectHookTrans, (Object) null))
      EffectManager.ReleaseEffect(ref this.effectHookTrans);
    this.effectHookTrans = EffectManager.GetEffect(this.param.hookEffect[this.gatherGimickModelIndex], this.gatherGimickTrans);
  }

  private void _UpdateSend()
  {
    this.sendTime -= Time.deltaTime;
    if ((double) this.sendTime > 0.0 || !this.isSend)
      return;
    this.ChangeState(FishingController.eState.Hit);
  }

  private void _ChangeHit()
  {
    if (Object.op_Inequality((Object) this.effectHookTrans, (Object) null))
      EffectManager.ReleaseEffect(ref this.effectHookTrans);
    this.effectHookTrans = (Transform) null;
    if (this.hitType == FishingController.eHitType.Enemy)
    {
      if (this.popInfo != null && Object.op_Inequality((Object) this.gatherGimickTrans, (Object) null))
        MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyForcePop(this.popInfo, this.gatherGimickTrans.position);
      AppMain.Delay(this.param.delayEnemyFishing, (System.Action) (() => UIInGamePopupDialog.PushOpen(this.enemyHitStr, false)));
    }
    this.owner.SetNextTrigger();
  }

  private void _ChangeQuit()
  {
    if (Object.op_Inequality((Object) this.effectHookTrans, (Object) null))
      EffectManager.ReleaseEffect(ref this.effectHookTrans);
    this.effectHookTrans = (Transform) null;
    this.owner.SetNextTrigger(1);
  }

  private void _ChangeFight()
  {
    this._CreateCoopFishingGimmick();
    this.SetCoopOwnerUserId(this.owner.createInfo.charaInfo.userId);
    this.isRoutineStamp = false;
    if (!this.isSelf || this.param.coopFishingStampId <= -1 || !MonoBehaviourSingleton<ChatManager>.IsValid())
      return;
    MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(this.param.coopFishingStampId);
    if ((double) this.param.coopFishingStampRoutineSec <= 0.0)
      return;
    this.isRoutineStamp = true;
    this.stampRoutineSec = 0.0f;
  }

  private void _UpdateFight()
  {
    if ((double) this.coopFishingGaugeDecreaseTimer < 0.0)
      this.coopFishingGaugeCurrent -= this.param.coopFishingGaugeDecreasePerSec * Time.deltaTime;
    if ((double) this.coopFishingGaugeNegativeTimer < 0.0)
      this.isGaugePositive = false;
    this.coopFishingGaugeDecreaseTimer -= Time.deltaTime;
    this.coopFishingGaugeNegativeTimer -= Time.deltaTime;
    if (this.isRoutineStamp)
    {
      this.stampRoutineSec += Time.deltaTime;
      if ((double) this.stampRoutineSec >= (double) this.param.coopFishingStampRoutineSec)
      {
        this.stampRoutineSec -= this.param.coopFishingStampRoutineSec;
        MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(this.param.coopFishingStampId);
      }
    }
    if (this.IsCoopFishingGaugeFull())
      this.ChangeState(FishingController.eState.FightSuccess);
    if (!this.IsCoopFishingGaugeEmpty())
      return;
    this.ChangeState(FishingController.eState.FightFailed);
  }

  private void _TapFight() => this.AddCoopFishingGauge();

  private void _ChangeFightSuccess()
  {
    this.isFightCompleteSend = false;
    this._DeactivateCoopFieldGimmick();
    this._MakeOtherCoopSuccess();
    if (!this.isSelf)
      return;
    this._SendFightSuccess();
  }

  private void _UpdateFightSuccess()
  {
    this.sendTime -= Time.deltaTime;
    if ((double) this.sendTime > 0.0 || !this.isFightCompleteSend)
      return;
    this.ChangeState(FishingController.eState.Hit);
  }

  private void _ChangeFightFailed()
  {
    this.isFightCompleteSend = false;
    this._DeactivateCoopFieldGimmick();
    this._MakeOtherCoopFailed();
    if (!this.isSelf)
      return;
    MonoBehaviourSingleton<FieldManager>.I.SendFieldFishBossComplete(this.coopOwnerUserId, 0, (Action<bool, FieldGatherRewardList>) ((is_success, list) =>
    {
      this.isFightCompleteSend = true;
      this.hitStr = StringTable.Get(STRING_CATEGORY.FISHING, this.kDontHitStringId);
      this.hitType = FishingController.eHitType.None;
      this.sendTime += this.param.sendSec[0];
    }));
  }

  private void _UpdateFightFailed()
  {
    this.sendTime -= Time.deltaTime;
    if ((double) this.sendTime > 0.0 || !this.isFightCompleteSend)
      return;
    this.ChangeState(FishingController.eState.Hit);
  }

  private void _ChangeCoop()
  {
    this.isRoutineStamp = false;
    if (this.isSelf && this.param.coopFishingGuestStampId > -1 && MonoBehaviourSingleton<ChatManager>.IsValid())
    {
      MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(this.param.coopFishingGuestStampId);
      if ((double) this.param.coopFishingGuestStampRoutineSec > 0.0)
      {
        this.isRoutineStamp = true;
        this.stampRoutineSec = 0.0f;
      }
    }
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnCoopFishingGaugeIncrease(this.coopOwnerClientId);
  }

  private void _UpdateCoop()
  {
    if ((double) this.coopFishingGaugeDecreaseTimer < 0.0)
      this.coopFishingGaugeCurrent -= this.param.coopFishingGaugeDecreasePerSec * Time.deltaTime;
    if ((double) this.coopFishingGaugeNegativeTimer < 0.0)
      this.isGaugePositive = false;
    this.coopFishingGaugeDecreaseTimer -= Time.deltaTime;
    this.coopFishingGaugeNegativeTimer -= Time.deltaTime;
    if (!this.isRoutineStamp)
      return;
    this.stampRoutineSec += Time.deltaTime;
    if ((double) this.stampRoutineSec < (double) this.param.coopFishingGuestStampRoutineSec)
      return;
    this.stampRoutineSec -= this.param.coopFishingGuestStampRoutineSec;
    MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(this.param.coopFishingGuestStampId);
  }

  private void _TapCoop()
  {
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnCoopFishingGaugeIncrease(this.coopOwnerClientId);
  }

  private void _ChangeCoopSuccess()
  {
    this.isFightCompleteSend = false;
    if (!this.isSelf)
      return;
    this._SendFightSuccess();
  }

  private void _UpdateCoopSuccess()
  {
    this.sendTime -= Time.deltaTime;
    if ((double) this.sendTime > 0.0 || !this.isFightCompleteSend)
      return;
    this.ChangeState(FishingController.eState.Hit);
  }

  private void _ChangeCoopFailed() => this._ChangeFightFailed();

  private void _UpdateCoopFailed()
  {
    this.sendTime -= Time.deltaTime;
    if ((double) this.sendTime > 0.0 || !this.isFightCompleteSend)
      return;
    this.ChangeState(FishingController.eState.Quit);
    UIInGamePopupDialog.PushOpen(this.hitStr, false);
  }

  private void _SetExclamation()
  {
    if (!this.isSelf || Object.op_Equality((Object) this.owner, (Object) null) || Object.op_Equality((Object) this.owner.uiPlayerStatusGizmo, (Object) null) || !((Component) this.owner.uiPlayerStatusGizmo).gameObject.activeInHierarchy)
      return;
    this.owner.uiPlayerStatusGizmo.SetEmotionDuration(this.param.hookSec);
  }

  private void _DispExclamation(bool isDisp)
  {
    if (!this.isSelf || Object.op_Equality((Object) this.owner, (Object) null) || Object.op_Equality((Object) this.owner.uiPlayerStatusGizmo, (Object) null) || !((Component) this.owner.uiPlayerStatusGizmo).gameObject.activeInHierarchy)
      return;
    this.owner.uiPlayerStatusGizmo.OnDispEmotion(isDisp);
    if (!isDisp)
      return;
    SoundManager.PlayOneShotSE(this.param.hookSeId, (DisableNotifyMonoBehaviour) this.owner, this.owner.FindNode(""));
  }

  private void _SendFightSuccess()
  {
    MonoBehaviourSingleton<FieldManager>.I.SendFieldFishBossComplete(this.coopOwnerUserId, 1, (Action<bool, FieldGatherRewardList>) ((is_success, list) =>
    {
      this.isFightCompleteSend = true;
      if (list == null || list.fieldGather == null || list.fieldGather.gatherItem.IsNullOrEmpty<QuestCompleteReward.GatherItem>())
        return;
      int index = 0;
      for (int count = list.fieldGather.gatherItem.Count; index < count; ++index)
      {
        QuestCompleteReward.GatherItem gatherItem = list.fieldGather.gatherItem[index];
        if (gatherItem != null)
        {
          GatherItemTable.GatherItemData data = Singleton<GatherItemTable>.I.GetData((uint) gatherItem.gatherItemId);
          if (data != null)
          {
            this.sendTime += this.param.sendSec[2];
            this.sendTime += this.param.sendCrownTypeSec[gatherItem.maxCrownType];
            if (data.isRare == 1)
              this.sendTime += this.param.sendRareSec;
            string str = GatherItemRecord.ShapeSize(gatherItem.score);
            this.hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, (uint) gatherItem.status), (object) data.name, (object) str);
            if (gatherItem.psig == null)
              break;
            this.enemyHitStr = this.hitStr;
            this.hitType = FishingController.eHitType.Enemy;
            this.popInfo = gatherItem.psig;
            break;
          }
        }
      }
    }));
  }

  private void _CreateCoopFishingGimmick()
  {
    if (Object.op_Inequality((Object) this.fieldGimmickCoopFishingObject, (Object) null))
      return;
    FieldMapTable.FieldGimmickPointTableData pointData = new FieldMapTable.FieldGimmickPointTableData();
    pointData.gimmickType = FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.COOP_FISHING;
    pointData.pointID = (uint) this.owner.id;
    pointData.pointX = this.owner._position.x;
    pointData.pointZ = this.owner._position.z;
    pointData.value2 = this.owner.createInfo.charaInfo.userId.ToString();
    FieldGimmickCoopFishing objectAndComponent = Utility.CreateGameObjectAndComponent<FieldGimmickCoopFishing>(MonoBehaviourSingleton<StageObjectManager>.I._transform, 19);
    objectAndComponent.Initialize(pointData);
    objectAndComponent.SetOwner(this.owner);
    ((Component) objectAndComponent).transform.position = this.owner._position;
    this.fieldGimmickCoopFishingObject = objectAndComponent;
    MonoBehaviourSingleton<InGameProgress>.I.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, (IFieldGimmickObject) objectAndComponent);
  }

  private void _DeactivateCoopFieldGimmick()
  {
    if (Object.op_Equality((Object) this.fieldGimmickCoopFishingObject, (Object) null))
      return;
    this.fieldGimmickCoopFishingObject.Deactivate();
  }

  private void _DestroyCoopFieldGimmick()
  {
    if (Object.op_Equality((Object) this.fieldGimmickCoopFishingObject, (Object) null))
      return;
    MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, (IFieldGimmickObject) this.fieldGimmickCoopFishingObject);
    this.fieldGimmickCoopFishingObject.RequestDestroy();
    this.fieldGimmickCoopFishingObject = (FieldGimmickCoopFishing) null;
  }

  private void _MakeOtherCoopSuccess()
  {
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    int index = 0;
    for (int count = playerList.Count; index < count; ++index)
    {
      Player player = playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && !Object.op_Equality((Object) player, (Object) this.owner))
        player.fishingCtrl.MakeCoopSuccessByOwner(this.owner.createInfo.charaInfo.userId);
    }
  }

  public void MakeCoopSuccessByOwner(int coopOwnerUserId)
  {
    if (!this.owner.IsOriginal() || !this.IsFishing() || this.coopOwnerUserId != coopOwnerUserId)
      return;
    this.ChangeState(FishingController.eState.CoopSuccess);
  }

  private void _MakeOtherCoopFailed()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    int index = 0;
    for (int count = playerList.Count; index < count; ++index)
    {
      Player player = playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && !Object.op_Equality((Object) player, (Object) this.owner))
        player.fishingCtrl.MakeCoopFailedByOwner(this.owner.createInfo.charaInfo.userId);
    }
  }

  public void MakeCoopFailedByOwner(int coopOwnerUserId)
  {
    if (!this.owner.IsOriginal() && !this.owner.IsCoopNone())
    {
      if (!this.owner.IsPuppet() || !this.IsCooperating())
        return;
      this.ChangeState(FishingController.eState.Quit);
    }
    else
    {
      if (!this.IsFishing() || !this.IsCooperating() || this.coopOwnerUserId != coopOwnerUserId)
        return;
      this.ChangeState(FishingController.eState.CoopFailed);
    }
  }

  public void SetCoopOwnerPlayerId(int id) => this.coopOwnerPlayerId = id;

  public void SetCoopOwnerClientId(int id) => this.coopOwnerClientId = id;

  public void SetCoopOwnerUserId(int id) => this.coopOwnerUserId = id;

  private void ClearIds()
  {
    this.coopOwnerPlayerId = 0;
    this.coopOwnerClientId = 0;
    this.coopOwnerUserId = 0;
  }

  public void OnReceiveCoopFishingGaugeIncrease()
  {
    if (this.coopOwnerPlayerId <= 0 || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.coopOwnerPlayerId) as Player;
    if (Object.op_Equality((Object) player, (Object) null) || player.fishingCtrl == null)
      return;
    player.fishingCtrl.AddCoopFishingGauge(true);
  }

  public void OnCurrentGaugeSync(int coopOwnerUserId, float value)
  {
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    int index = 0;
    for (int count = playerList.Count; index < count; ++index)
    {
      Player player = playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null))
        player.fishingCtrl.SetCurrentGaugeSync(coopOwnerUserId, value);
    }
  }

  public void SetCurrentGaugeSync(int coopOwnerUserId, float value)
  {
    if (!this.owner.IsOriginal() || !this.IsFishing() || this.coopOwnerUserId != coopOwnerUserId)
      return;
    this.coopFishingGaugeCurrent = value;
    this.coopFishingGaugeDecreaseTimer = this.param.coopFishingGaugeMarginSecToStartDecrease;
    this.coopFishingGaugeNegativeTimer = this.param.coopFishingGaugeMarginToStartChangeRed;
    this.isGaugePositive = true;
  }

  public enum eState
  {
    None,
    Wait,
    Omen,
    Hook,
    Send,
    Hit,
    Quit,
    Fight,
    FightSuccess,
    FightFailed,
    Coop,
    CoopSuccess,
    CoopFailed,
  }

  public enum eHitType
  {
    None,
    Item,
    GatherItem,
    Enemy,
  }
}
