using UnityEngine;

public class CoopMyClient : CoopClient
{
	public CoopClientPacketSender packetSender
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		packetSender = base.gameObject.AddComponent<CoopClientPacketSender>();
		base.Awake();
	}

	public override string ToString()
	{
		return "CoopMyClient[" + base.slotIndex + "](" + base.status + "/" + base.isPartyOwner + "/" + base.isStageHost + ").userId=" + base.userId;
	}

	public override void Init(int client_id)
	{
		base.Init(client_id);
		base.joinType = MonoBehaviourSingleton<InGameManager>.I.currentJoinType;
	}

	public override string GetPlayerName()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			return MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
		}
		return base.GetPlayerName();
	}

	protected override void SetStatus(CLIENT_STATUS st)
	{
		base.SetStatus(st);
		packetSender.SendClientStatus(0);
	}

	public override void SetLoadingPer(int per)
	{
		base.SetLoadingPer(per);
		packetSender.SendClientLoadingProgress();
	}

	public override void OnRoomLeaved()
	{
		Logd("OnRoomLeaved. {0}/{1}", FieldManager.IsValidInGameNoQuest(), MonoBehaviourSingleton<InGameProgress>.IsValid());
		base.isLeave = true;
		if (InGameManager.IsReentry())
		{
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				if (_CheckWaveMatchRetire())
				{
					MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
				}
				else
				{
					MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
				}
			}
		}
		else if (!base.isBattleRetire && IsStageStart() && !MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
		{
			string text = StringTable.Get(STRING_CATEGORY.IN_GAME, 100u);
			UIInGamePopupDialog.PushOpen(text, false, 1.8f);
			MonoBehaviourSingleton<GoWrapManager>.I.trackBattleDisconnect();
		}
	}

	private bool _CheckWaveMatchRetire()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return true;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.playerList.IsNullOrEmpty())
		{
			return true;
		}
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; i < count; i++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as Player;
			if (!((Object)player == (Object)null) && !((Object)player == (Object)MonoBehaviourSingleton<StageObjectManager>.I.self) && !player.isNpc)
			{
				return false;
			}
		}
		return true;
	}

	public void WelcomeClient(int clientId)
	{
		Logd("WelcomeClient. clientId={0}", clientId);
		packetSender.SendClientStatus(clientId);
	}

	public void ChangeEquip()
	{
		packetSender.SendClientChangeEquip();
	}

	public void BattleRetire()
	{
		Logd("BattleRetire.");
		base.isBattleRetire = true;
		packetSender.SendClientBattleRetire();
	}

	public void StageStart()
	{
		SetStatus(CLIENT_STATUS.STAGE_START);
	}

	public void LoadingStart()
	{
		SetStatus(CLIENT_STATUS.LOADING_START);
	}

	public void LoadingFinish()
	{
		SetStatus(CLIENT_STATUS.LOADING_FINISH);
	}

	public void StageRequest()
	{
		SetStatus(CLIENT_STATUS.STAGE_REQUEST);
	}

	public void StartBattle()
	{
		SetStatus(CLIENT_STATUS.BATTLE_START);
	}

	public void EndBattle()
	{
		SetStatus(CLIENT_STATUS.BATTLE_END);
	}

	public void SeriesProgress(int endPhase)
	{
		base.isSeriesProgressEnd = true;
		packetSender.SendClientSeriesProgress(endPhase);
	}
}
