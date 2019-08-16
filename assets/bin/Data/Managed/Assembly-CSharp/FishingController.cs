using Network;
using System.Collections.Generic;
using UnityEngine;

public class FishingController
{
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
		CoopFailed
	}

	public enum eHitType
	{
		None,
		Item,
		GatherItem,
		Enemy
	}

	private readonly uint kItemHitStringId = 50u;

	private readonly uint kAccessoryHitStringId = 51u;

	private readonly uint kDontHitStringId = 100u;

	private eState state;

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

	private string hitStr = string.Empty;

	private string enemyHitStr = string.Empty;

	private bool isFightCompleteSend;

	private eHitType hitType;

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
		owner = player;
		isSelf = (player is Self);
		state = eState.None;
		param = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
		coopFishingGaugeMax = param.coopFishingGaugeMax;
		coopFishingGaugeDecreaseTimer = param.coopFishingGaugeMarginSecToStartDecrease;
		coopFishingGaugeNegativeTimer = param.coopFishingGaugeMarginToStartChangeRed;
	}

	public void Finalize()
	{
		_DestroyCoopFieldGimmick();
		if (coopOwnerPlayerId == 0)
		{
			_MakeOtherCoopFailed();
		}
		owner = null;
		state = eState.None;
		param = null;
	}

	public bool CanFishing()
	{
		return state == eState.None;
	}

	public bool IsFishing()
	{
		return state != eState.None;
	}

	public bool IsFighting()
	{
		return state == eState.Fight;
	}

	public bool IsCooperating()
	{
		return state == eState.Coop;
	}

	public int GetStateForInitialize()
	{
		return (int)state;
	}

	public float GetMaxWaitPacketSec()
	{
		if (param == null)
		{
			return 20f;
		}
		float num = param.waitSec[1];
		num += (float)(param.maxOmenNum - 1) * param.omenInterval;
		num += param.hookSec;
		return num + (param.sendMinSec + param.sendCrownTypeSec[2] + param.sendSec[3] + param.sendRareSec);
	}

	public float GetCoopFishingGaugeRate()
	{
		return Mathf.Clamp01(coopFishingGaugeCurrent / coopFishingGaugeMax);
	}

	public bool IsCoopFishingGaugeFull()
	{
		return coopFishingGaugeMax <= coopFishingGaugeCurrent;
	}

	public bool IsCoopFishingGaugeEmpty()
	{
		return coopFishingGaugeCurrent <= 0f;
	}

	public void AddCoopFishingGauge(bool isPacket = false)
	{
		coopFishingGaugeCurrent += ((!isPacket) ? param.coopFishingGaugeIncreasePerTapBySelf : param.coopFishingGaugeIncreasePerTapByOther);
		coopFishingGaugeDecreaseTimer = param.coopFishingGaugeMarginSecToStartDecrease;
		coopFishingGaugeNegativeTimer = param.coopFishingGaugeMarginToStartChangeRed;
		isGaugePositive = true;
		if (owner.playerSender != null)
		{
			owner.playerSender.OnCoopFishingGaugeSync(coopOwnerUserId, coopFishingGaugeCurrent);
		}
	}

	public bool IsGaugePositive()
	{
		return isGaugePositive;
	}

	private void _ShowWeapon(bool isShow)
	{
		owner.SetEnableNodeRenderer("R_Wep", isShow);
		owner.SetEnableNodeRenderer("L_Wep", isShow);
	}

	public void Start(int id, Transform ggTrans, int modelIndex)
	{
		gatherGimickTrans = ggTrans;
		gatherGimickModelIndex = modelIndex;
		lotId = id;
		hitStr = string.Empty;
		hitType = eHitType.None;
		waitTime = 0f;
		omenNum = 0;
		omenInterval = 0f;
		hookTime = 0f;
		sendTime = 0f;
		timing = -1;
		isSend = false;
		coopFishingGaugeCurrent = param.coopFishingGaugeInitial;
		owner._rigidbody.set_isKinematic(true);
		_ShowWeapon(isShow: false);
		_SetExclamation();
		ChangeState(eState.Wait);
	}

	public void End()
	{
		if (state != 0)
		{
			owner._rigidbody.set_isKinematic(false);
			_ShowWeapon(isShow: true);
			if (effectHookTrans != null)
			{
				EffectManager.ReleaseEffect(ref effectHookTrans);
			}
			effectHookTrans = null;
			_DispExclamation(isDisp: false);
			_DestroyCoopFieldGimmick();
			owner.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_GATHER_GIMMICK);
			ClearIds();
			state = eState.None;
		}
	}

	public void Get()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (state == eState.Hit && hitType != eHitType.Enemy)
		{
			if (hitType == eHitType.Item || hitType == eHitType.GatherItem)
			{
				EffectManager.OneShot("ef_btl_fishing_03", owner.FindNode("L_Hand").get_position(), Quaternion.get_identity());
			}
			if (isSelf)
			{
				SoundManager.PlayOneShotSE(param.hitSeIds[(int)hitType], owner, owner.FindNode(string.Empty));
				UIInGamePopupDialog.PushOpen(hitStr, is_important: false);
			}
		}
	}

	public void CoopStart()
	{
		hitStr = string.Empty;
		hitType = eHitType.None;
		waitTime = 0f;
		omenNum = 0;
		omenInterval = 0f;
		hookTime = 0f;
		sendTime = 0f;
		timing = -1;
		isSend = false;
		isFightCompleteSend = false;
		coopFishingGaugeCurrent = param.coopFishingGaugeInitial;
		owner._rigidbody.set_isKinematic(true);
		_ShowWeapon(isShow: false);
		ChangeState(eState.Coop);
	}

	public void CoopEnd()
	{
		if (IsFishing())
		{
			owner._rigidbody.set_isKinematic(false);
			_ShowWeapon(isShow: true);
			if (effectHookTrans != null)
			{
				EffectManager.ReleaseEffect(ref effectHookTrans);
			}
			effectHookTrans = null;
			_DestroyCoopFieldGimmick();
			ClearIds();
			state = eState.None;
		}
	}

	public void OnReaction()
	{
		if (coopOwnerPlayerId > 0)
		{
			CoopEnd();
			return;
		}
		SoundManager.StopLoopSE(GetSeId(2), owner);
		_MakeOtherCoopFailed();
	}

	public int GetSeId(int type)
	{
		switch (type)
		{
		case 0:
			return param.se0Id[gatherGimickModelIndex];
		case 1:
			return param.se1Id[gatherGimickModelIndex];
		case 2:
			return param.se2Id[gatherGimickModelIndex];
		case 3:
			return param.se3Id[gatherGimickModelIndex];
		default:
			return 0;
		}
	}

	public void ChangeState(eState s)
	{
		switch (s)
		{
		case eState.Wait:
			_ChangeWait();
			break;
		case eState.Omen:
			_ChangeOmen();
			break;
		case eState.Hook:
			_ChangeHook();
			break;
		case eState.Send:
			_ChangeSend();
			break;
		case eState.Hit:
			_ChangeHit();
			break;
		case eState.Quit:
			_ChangeQuit();
			break;
		case eState.Fight:
			_ChangeFight();
			break;
		case eState.FightSuccess:
			_ChangeFightSuccess();
			break;
		case eState.FightFailed:
			_ChangeFightFailed();
			break;
		case eState.Coop:
			_ChangeCoop();
			break;
		case eState.CoopSuccess:
			_ChangeCoopSuccess();
			break;
		case eState.CoopFailed:
			_ChangeCoopFailed();
			break;
		}
		state = s;
		if (isSelf && owner != null && owner.playerSender != null)
		{
			owner.playerSender.OnGatherGimmickState((int)state);
		}
	}

	public void Update()
	{
		if (isSelf)
		{
			switch (state)
			{
			case eState.Hit:
			case eState.Quit:
				break;
			case eState.Wait:
				_UpdateWait();
				break;
			case eState.Omen:
				_UpdateOmen();
				break;
			case eState.Hook:
				_UpdateHook();
				break;
			case eState.Send:
				_UpdateSend();
				break;
			case eState.Fight:
				_UpdateFight();
				break;
			case eState.FightSuccess:
				_UpdateFightSuccess();
				break;
			case eState.FightFailed:
				_UpdateFightFailed();
				break;
			case eState.Coop:
				_UpdateCoop();
				break;
			case eState.CoopSuccess:
				_UpdateCoopSuccess();
				break;
			case eState.CoopFailed:
				_UpdateCoopFailed();
				break;
			}
		}
	}

	public void Tap()
	{
		switch (state)
		{
		case eState.Send:
		case eState.Hit:
		case eState.Quit:
		case eState.FightSuccess:
		case eState.FightFailed:
			break;
		case eState.Wait:
		case eState.Omen:
			_TapWait();
			break;
		case eState.Hook:
			_TapHook();
			break;
		case eState.Fight:
			_TapFight();
			break;
		case eState.Coop:
			_TapCoop();
			break;
		}
	}

	private void _ChangeWait()
	{
		waitTime = Random.Range(param.waitSec[0], param.waitSec[1]);
	}

	private void _UpdateWait()
	{
		waitTime -= Time.get_deltaTime();
		if (waitTime <= 0f)
		{
			ChangeState(eState.Omen);
		}
	}

	private void _TapWait()
	{
		ChangeState(eState.Quit);
	}

	private void _ChangeOmen()
	{
		omenNum = Random.Range(0, param.maxOmenNum);
		omenInterval = 0f;
	}

	private void _UpdateOmen()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		omenInterval -= Time.get_deltaTime();
		if (omenInterval <= 0f)
		{
			SoundManager.PlayOneShotSE(GetSeId(1), owner, owner.FindNode(string.Empty));
			EffectManager.OneShot(param.omenEffect[gatherGimickModelIndex], gatherGimickTrans.get_position(), Quaternion.get_identity());
			if (--omenNum < 0)
			{
				ChangeState(eState.Hook);
			}
			else
			{
				omenInterval = param.omenInterval;
			}
		}
	}

	private void _ChangeHook()
	{
		hookTime = param.hookSec;
		_DispExclamation(isDisp: true);
	}

	private void _UpdateHook()
	{
		hookTime -= Time.get_deltaTime();
		if (hookTime <= 0f)
		{
			ChangeState(eState.Send);
		}
	}

	private void _TapHook()
	{
		timing = (int)((1f - hookTime / param.hookSec) * 100f);
		ChangeState(eState.Send);
	}

	private void _ChangeSend()
	{
		_DispExclamation(isDisp: false);
		owner.SetNextTrigger();
		sendTime = param.sendMinSec;
		hitType = eHitType.None;
		int isPop = MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle() ? 1 : 0;
		isSend = false;
		if (isSelf)
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldGatherGimmick(lotId, timing, isPop, delegate(bool is_success, FieldFishModel.Param retParam)
			{
				isSend = true;
				if (retParam.hitBoss > 0)
				{
					ChangeState(eState.Fight);
				}
				else
				{
					FieldGatherRewardList reward = retParam.reward;
					if (reward != null && reward.fieldGather != null)
					{
						if (!reward.fieldGather.gatherItem.IsNullOrEmpty())
						{
							int i = 0;
							for (int count = reward.fieldGather.gatherItem.Count; i < count; i++)
							{
								QuestCompleteReward.GatherItem gatherItem = reward.fieldGather.gatherItem[i];
								if (gatherItem != null)
								{
									GatherItemTable.GatherItemData data = Singleton<GatherItemTable>.I.GetData((uint)gatherItem.gatherItemId);
									if (data != null)
									{
										sendTime += param.sendSec[2];
										sendTime += param.sendCrownTypeSec[gatherItem.maxCrownType];
										if (data.isRare == 1)
										{
											sendTime += param.sendRareSec;
										}
										string arg = GatherItemRecord.ShapeSize(gatherItem.score);
										hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, (uint)gatherItem.status), data.name, arg);
										if (gatherItem.psig != null)
										{
											enemyHitStr = hitStr;
											hitType = eHitType.Enemy;
											popInfo = gatherItem.psig;
										}
										else
										{
											hitType = eHitType.GatherItem;
										}
										return;
									}
								}
							}
						}
						int j = 0;
						for (int count2 = reward.fieldGather.item.Count; j < count2; j++)
						{
							QuestCompleteReward.Item item = reward.fieldGather.item[j];
							if (item != null)
							{
								ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)item.itemId);
								if (itemData != null)
								{
									hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, kItemHitStringId), itemData.name, MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint)item.itemId));
									hitType = eHitType.Item;
									sendTime += param.sendSec[1];
									return;
								}
							}
						}
						int k = 0;
						for (int count3 = reward.fieldGather.accessoryItem.Count; k < count3; k++)
						{
							QuestCompleteReward.AccessoryItem accessoryItem = reward.fieldGather.accessoryItem[k];
							if (accessoryItem != null)
							{
								AccessoryTable.AccessoryData data2 = Singleton<AccessoryTable>.I.GetData((uint)accessoryItem.accessoryId);
								if (data2 != null)
								{
									hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, kAccessoryHitStringId), data2.name);
									hitType = eHitType.Item;
									sendTime += param.sendSec[1];
									return;
								}
							}
						}
					}
					hitStr = StringTable.Get(STRING_CATEGORY.FISHING, kDontHitStringId);
					hitType = eHitType.None;
					sendTime += param.sendSec[0];
				}
			});
			if (effectHookTrans != null)
			{
				EffectManager.ReleaseEffect(ref effectHookTrans);
			}
			effectHookTrans = EffectManager.GetEffect(param.hookEffect[gatherGimickModelIndex], gatherGimickTrans);
		}
	}

	private void _UpdateSend()
	{
		sendTime -= Time.get_deltaTime();
		if (sendTime <= 0f && isSend)
		{
			ChangeState(eState.Hit);
		}
	}

	private void _ChangeHit()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (effectHookTrans != null)
		{
			EffectManager.ReleaseEffect(ref effectHookTrans);
		}
		effectHookTrans = null;
		if (hitType == eHitType.Enemy)
		{
			if (popInfo != null && gatherGimickTrans != null)
			{
				MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyForcePop(popInfo, gatherGimickTrans.get_position());
			}
			AppMain.Delay(param.delayEnemyFishing, delegate
			{
				UIInGamePopupDialog.PushOpen(enemyHitStr, is_important: false);
			});
		}
		owner.SetNextTrigger();
	}

	private void _ChangeQuit()
	{
		if (effectHookTrans != null)
		{
			EffectManager.ReleaseEffect(ref effectHookTrans);
		}
		effectHookTrans = null;
		owner.SetNextTrigger(1);
	}

	private void _ChangeFight()
	{
		_CreateCoopFishingGimmick();
		SetCoopOwnerUserId(owner.createInfo.charaInfo.userId);
		isRoutineStamp = false;
		if (isSelf && param.coopFishingStampId > -1 && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(param.coopFishingStampId);
			if (param.coopFishingStampRoutineSec > 0f)
			{
				isRoutineStamp = true;
				stampRoutineSec = 0f;
			}
		}
	}

	private void _UpdateFight()
	{
		if (coopFishingGaugeDecreaseTimer < 0f)
		{
			coopFishingGaugeCurrent -= param.coopFishingGaugeDecreasePerSec * Time.get_deltaTime();
		}
		if (coopFishingGaugeNegativeTimer < 0f)
		{
			isGaugePositive = false;
		}
		coopFishingGaugeDecreaseTimer -= Time.get_deltaTime();
		coopFishingGaugeNegativeTimer -= Time.get_deltaTime();
		if (isRoutineStamp)
		{
			stampRoutineSec += Time.get_deltaTime();
			if (stampRoutineSec >= param.coopFishingStampRoutineSec)
			{
				stampRoutineSec -= param.coopFishingStampRoutineSec;
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(param.coopFishingStampId);
			}
		}
		if (IsCoopFishingGaugeFull())
		{
			ChangeState(eState.FightSuccess);
		}
		if (IsCoopFishingGaugeEmpty())
		{
			ChangeState(eState.FightFailed);
		}
	}

	private void _TapFight()
	{
		AddCoopFishingGauge();
	}

	private void _ChangeFightSuccess()
	{
		isFightCompleteSend = false;
		_DeactivateCoopFieldGimmick();
		_MakeOtherCoopSuccess();
		if (isSelf)
		{
			_SendFightSuccess();
		}
	}

	private void _UpdateFightSuccess()
	{
		sendTime -= Time.get_deltaTime();
		if (sendTime <= 0f && isFightCompleteSend)
		{
			ChangeState(eState.Hit);
		}
	}

	private void _ChangeFightFailed()
	{
		isFightCompleteSend = false;
		_DeactivateCoopFieldGimmick();
		_MakeOtherCoopFailed();
		if (isSelf)
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldFishBossComplete(coopOwnerUserId, 0, delegate
			{
				isFightCompleteSend = true;
				hitStr = StringTable.Get(STRING_CATEGORY.FISHING, kDontHitStringId);
				hitType = eHitType.None;
				sendTime += param.sendSec[0];
			});
		}
	}

	private void _UpdateFightFailed()
	{
		sendTime -= Time.get_deltaTime();
		if (sendTime <= 0f && isFightCompleteSend)
		{
			ChangeState(eState.Hit);
		}
	}

	private void _ChangeCoop()
	{
		isRoutineStamp = false;
		if (isSelf && param.coopFishingGuestStampId > -1 && MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(param.coopFishingGuestStampId);
			if (param.coopFishingGuestStampRoutineSec > 0f)
			{
				isRoutineStamp = true;
				stampRoutineSec = 0f;
			}
		}
		if (owner.playerSender != null)
		{
			owner.playerSender.OnCoopFishingGaugeIncrease(coopOwnerClientId);
		}
	}

	private void _UpdateCoop()
	{
		if (coopFishingGaugeDecreaseTimer < 0f)
		{
			coopFishingGaugeCurrent -= param.coopFishingGaugeDecreasePerSec * Time.get_deltaTime();
		}
		if (coopFishingGaugeNegativeTimer < 0f)
		{
			isGaugePositive = false;
		}
		coopFishingGaugeDecreaseTimer -= Time.get_deltaTime();
		coopFishingGaugeNegativeTimer -= Time.get_deltaTime();
		if (isRoutineStamp)
		{
			stampRoutineSec += Time.get_deltaTime();
			if (stampRoutineSec >= param.coopFishingGuestStampRoutineSec)
			{
				stampRoutineSec -= param.coopFishingGuestStampRoutineSec;
				MonoBehaviourSingleton<ChatManager>.I.roomChat.SendStamp(param.coopFishingGuestStampId);
			}
		}
	}

	private void _TapCoop()
	{
		if (owner.playerSender != null)
		{
			owner.playerSender.OnCoopFishingGaugeIncrease(coopOwnerClientId);
		}
	}

	private void _ChangeCoopSuccess()
	{
		isFightCompleteSend = false;
		if (isSelf)
		{
			_SendFightSuccess();
		}
	}

	private void _UpdateCoopSuccess()
	{
		sendTime -= Time.get_deltaTime();
		if (sendTime <= 0f && isFightCompleteSend)
		{
			ChangeState(eState.Hit);
		}
	}

	private void _ChangeCoopFailed()
	{
		_ChangeFightFailed();
	}

	private void _UpdateCoopFailed()
	{
		sendTime -= Time.get_deltaTime();
		if (sendTime <= 0f && isFightCompleteSend)
		{
			ChangeState(eState.Quit);
			UIInGamePopupDialog.PushOpen(hitStr, is_important: false);
		}
	}

	private void _SetExclamation()
	{
		if (isSelf && !(owner == null) && !(owner.uiPlayerStatusGizmo == null) && owner.uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			owner.uiPlayerStatusGizmo.SetEmotionDuration(param.hookSec);
		}
	}

	private void _DispExclamation(bool isDisp)
	{
		if (isSelf && !(owner == null) && !(owner.uiPlayerStatusGizmo == null) && owner.uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			owner.uiPlayerStatusGizmo.OnDispEmotion(isDisp);
			if (isDisp)
			{
				SoundManager.PlayOneShotSE(param.hookSeId, owner, owner.FindNode(string.Empty));
			}
		}
	}

	private void _SendFightSuccess()
	{
		MonoBehaviourSingleton<FieldManager>.I.SendFieldFishBossComplete(coopOwnerUserId, 1, delegate(bool is_success, FieldGatherRewardList list)
		{
			isFightCompleteSend = true;
			if (list != null && list.fieldGather != null && !list.fieldGather.gatherItem.IsNullOrEmpty())
			{
				int num = 0;
				int count = list.fieldGather.gatherItem.Count;
				QuestCompleteReward.GatherItem gatherItem;
				GatherItemTable.GatherItemData data;
				while (true)
				{
					if (num >= count)
					{
						return;
					}
					gatherItem = list.fieldGather.gatherItem[num];
					if (gatherItem != null)
					{
						data = Singleton<GatherItemTable>.I.GetData((uint)gatherItem.gatherItemId);
						if (data != null)
						{
							break;
						}
					}
					num++;
				}
				sendTime += param.sendSec[2];
				sendTime += param.sendCrownTypeSec[gatherItem.maxCrownType];
				if (data.isRare == 1)
				{
					sendTime += param.sendRareSec;
				}
				string arg = GatherItemRecord.ShapeSize(gatherItem.score);
				hitStr = string.Format(StringTable.Get(STRING_CATEGORY.FISHING, (uint)gatherItem.status), data.name, arg);
				if (gatherItem.psig != null)
				{
					enemyHitStr = hitStr;
					hitType = eHitType.Enemy;
					popInfo = gatherItem.psig;
				}
			}
		});
	}

	private void _CreateCoopFishingGimmick()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (!(fieldGimmickCoopFishingObject != null))
		{
			FieldMapTable.FieldGimmickPointTableData fieldGimmickPointTableData = new FieldMapTable.FieldGimmickPointTableData();
			fieldGimmickPointTableData.gimmickType = FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.COOP_FISHING;
			fieldGimmickPointTableData.pointID = (uint)owner.id;
			FieldMapTable.FieldGimmickPointTableData fieldGimmickPointTableData2 = fieldGimmickPointTableData;
			Vector3 position = owner._position;
			fieldGimmickPointTableData2.pointX = position.x;
			FieldMapTable.FieldGimmickPointTableData fieldGimmickPointTableData3 = fieldGimmickPointTableData;
			Vector3 position2 = owner._position;
			fieldGimmickPointTableData3.pointZ = position2.z;
			fieldGimmickPointTableData.value2 = owner.createInfo.charaInfo.userId.ToString();
			FieldGimmickCoopFishing fieldGimmickCoopFishing = Utility.CreateGameObjectAndComponent<FieldGimmickCoopFishing>(MonoBehaviourSingleton<StageObjectManager>.I._transform, 19);
			fieldGimmickCoopFishing.Initialize(fieldGimmickPointTableData);
			fieldGimmickCoopFishing.SetOwner(owner);
			fieldGimmickCoopFishing.get_transform().set_position(owner._position);
			fieldGimmickCoopFishingObject = fieldGimmickCoopFishing;
			MonoBehaviourSingleton<InGameProgress>.I.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, fieldGimmickCoopFishing);
		}
	}

	private void _DeactivateCoopFieldGimmick()
	{
		if (!(fieldGimmickCoopFishingObject == null))
		{
			fieldGimmickCoopFishingObject.Deactivate();
		}
	}

	private void _DestroyCoopFieldGimmick()
	{
		if (!(fieldGimmickCoopFishingObject == null))
		{
			MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, fieldGimmickCoopFishingObject);
			fieldGimmickCoopFishingObject.RequestDestroy();
			fieldGimmickCoopFishingObject = null;
		}
	}

	private void _MakeOtherCoopSuccess()
	{
		List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			Player player = playerList[i] as Player;
			if (!(player == null) && !(player == owner))
			{
				player.fishingCtrl.MakeCoopSuccessByOwner(owner.createInfo.charaInfo.userId);
			}
		}
	}

	public void MakeCoopSuccessByOwner(int coopOwnerUserId)
	{
		if (owner.IsOriginal() && IsFishing() && this.coopOwnerUserId == coopOwnerUserId)
		{
			ChangeState(eState.CoopSuccess);
		}
	}

	private void _MakeOtherCoopFailed()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			Player player = playerList[i] as Player;
			if (!(player == null) && !(player == owner))
			{
				player.fishingCtrl.MakeCoopFailedByOwner(owner.createInfo.charaInfo.userId);
			}
		}
	}

	public void MakeCoopFailedByOwner(int coopOwnerUserId)
	{
		if (!owner.IsOriginal() && !owner.IsCoopNone())
		{
			if (owner.IsPuppet() && IsCooperating())
			{
				ChangeState(eState.Quit);
			}
		}
		else if (IsFishing() && IsCooperating() && this.coopOwnerUserId == coopOwnerUserId)
		{
			ChangeState(eState.CoopFailed);
		}
	}

	public void SetCoopOwnerPlayerId(int id)
	{
		coopOwnerPlayerId = id;
	}

	public void SetCoopOwnerClientId(int id)
	{
		coopOwnerClientId = id;
	}

	public void SetCoopOwnerUserId(int id)
	{
		coopOwnerUserId = id;
	}

	private void ClearIds()
	{
		coopOwnerPlayerId = 0;
		coopOwnerClientId = 0;
		coopOwnerUserId = 0;
	}

	public void OnReceiveCoopFishingGaugeIncrease()
	{
		if (coopOwnerPlayerId > 0 && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(coopOwnerPlayerId) as Player;
			if (!(player == null) && player.fishingCtrl != null)
			{
				player.fishingCtrl.AddCoopFishingGauge(isPacket: true);
			}
		}
	}

	public void OnCurrentGaugeSync(int coopOwnerUserId, float value)
	{
		List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
		int i = 0;
		for (int count = playerList.Count; i < count; i++)
		{
			Player player = playerList[i] as Player;
			if (!(player == null))
			{
				player.fishingCtrl.SetCurrentGaugeSync(coopOwnerUserId, value);
			}
		}
	}

	public void SetCurrentGaugeSync(int coopOwnerUserId, float value)
	{
		if (owner.IsOriginal() && IsFishing() && this.coopOwnerUserId == coopOwnerUserId)
		{
			coopFishingGaugeCurrent = value;
			coopFishingGaugeDecreaseTimer = param.coopFishingGaugeMarginSecToStartDecrease;
			coopFishingGaugeNegativeTimer = param.coopFishingGaugeMarginToStartChangeRed;
			isGaugePositive = true;
		}
	}
}
