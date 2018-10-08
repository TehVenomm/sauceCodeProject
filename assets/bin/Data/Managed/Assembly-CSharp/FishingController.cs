using Network;
using System;
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
		Quit
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

	private eHitType hitType;

	private PopSignatureInfo popInfo;

	private Transform gatherGimickTrans;

	private Transform effectHookTrans;

	public void Initialize(Player player)
	{
		owner = player;
		isSelf = (player is Self);
		state = eState.None;
		param = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
	}

	public new void Finalize()
	{
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

	private void _ShowWeapon(bool isShow)
	{
		owner.SetEnableNodeRenderer("R_Wep", isShow);
		owner.SetEnableNodeRenderer("L_Wep", isShow);
	}

	public void Start(int id, Transform ggTrans)
	{
		gatherGimickTrans = ggTrans;
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
		owner._rigidbody.set_isKinematic(true);
		_ShowWeapon(false);
		_SetExclamation();
		ChangeState(eState.Wait);
	}

	public void End()
	{
		if (state != 0)
		{
			owner._rigidbody.set_isKinematic(false);
			_ShowWeapon(true);
			if (effectHookTrans != null)
			{
				EffectManager.ReleaseEffect(ref effectHookTrans);
			}
			effectHookTrans = null;
			_DispExclamation(false);
			owner.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_GATHER_GIMMICK);
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
				EffectManager.OneShot("ef_btl_fishing_03", owner.FindNode("L_Hand").get_position(), Quaternion.get_identity(), false);
			}
			if (isSelf)
			{
				SoundManager.PlayOneShotSE(param.hitSeIds[(int)hitType], owner, owner.FindNode(string.Empty));
				UIInGamePopupDialog.PushOpen(hitStr, false, 1.8f);
			}
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
			}
		}
	}

	public void Tap()
	{
		switch (state)
		{
		case eState.Wait:
		case eState.Omen:
			_TapWait();
			break;
		case eState.Hook:
			_TapHook();
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
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		omenInterval -= Time.get_deltaTime();
		if (omenInterval <= 0f)
		{
			SoundManager.PlayOneShotSE(param.omenSeId, owner, owner.FindNode(string.Empty));
			EffectManager.OneShot("ef_btl_fishing_01", gatherGimickTrans.get_position(), Quaternion.get_identity(), false);
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
		_DispExclamation(true);
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

	private unsafe void _ChangeSend()
	{
		_DispExclamation(false);
		owner.SetNextTrigger(0);
		sendTime = param.sendMinSec;
		hitType = eHitType.None;
		int isPop = MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle() ? 1 : 0;
		isSend = false;
		if (isSelf)
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldGatherGimmick(lotId, timing, isPop, new Action<bool, FieldGatherRewardList>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			if (effectHookTrans != null)
			{
				EffectManager.ReleaseEffect(ref effectHookTrans);
			}
			effectHookTrans = EffectManager.GetEffect("ef_btl_fishing_02", gatherGimickTrans);
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

	private unsafe void _ChangeHit()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		if (effectHookTrans != null)
		{
			EffectManager.ReleaseEffect(ref effectHookTrans);
		}
		effectHookTrans = null;
		if (hitType == eHitType.Enemy)
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyForcePop(popInfo, gatherGimickTrans.get_position());
			AppMain.Delay(param.delayEnemyFishing, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		owner.SetNextTrigger(0);
	}

	private void _ChangeQuit()
	{
		owner.SetNextTrigger(1);
	}

	private void _SetExclamation()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (isSelf && !(owner == null) && !(owner.uiPlayerStatusGizmo == null) && owner.uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			owner.uiPlayerStatusGizmo.SetEmotionDuration(param.hookSec);
		}
	}

	private void _DispExclamation(bool isDisp)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (isSelf && !(owner == null) && !(owner.uiPlayerStatusGizmo == null) && owner.uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			owner.uiPlayerStatusGizmo.OnDispEmotion(isDisp);
			if (isDisp)
			{
				SoundManager.PlayOneShotSE(param.hookSeId, owner, owner.FindNode(string.Empty));
			}
		}
	}
}
