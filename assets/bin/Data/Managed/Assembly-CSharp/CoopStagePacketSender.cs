using System;
using UnityEngine;

public class CoopStagePacketSender : MonoBehaviour
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopStage = base.gameObject.GetComponent<CoopStage>();
	}

	protected virtual void Start()
	{
	}

	private int Send<T>(T model, bool promise = true, int to_client_id = 0, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (to_client_id == 0)
		{
			return MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcastInStage(model, promise, onReceiveAck, onPreResend);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendToInStage(to_client_id, model, promise, onReceiveAck, onPreResend);
	}

	public void SendStageRequest(int to_client_id = 0)
	{
		Coop_Model_StageRequest coop_Model_StageRequest = new Coop_Model_StageRequest();
		coop_Model_StageRequest.id = 1002;
		coop_Model_StageRequest.series_index = 0;
		if (QuestManager.IsValidInGame())
		{
			coop_Model_StageRequest.series_index = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
		}
		Send(coop_Model_StageRequest, true, to_client_id, null, null);
	}

	public void SendStagePlayerPop(Player player, int to_client_id = 0)
	{
		if (!((UnityEngine.Object)player == (UnityEngine.Object)null))
		{
			if (player.IsCoopNone())
			{
				player.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
			}
			Coop_Model_StagePlayerPop coop_Model_StagePlayerPop = new Coop_Model_StagePlayerPop();
			coop_Model_StagePlayerPop.id = 1002;
			coop_Model_StagePlayerPop.sid = player.id;
			coop_Model_StagePlayerPop.isSelf = (player is Self);
			NonPlayer x = player as NonPlayer;
			if (player.createInfo != null)
			{
				if ((UnityEngine.Object)x == (UnityEngine.Object)null)
				{
					coop_Model_StagePlayerPop.charaInfo = player.createInfo.charaInfo;
				}
				coop_Model_StagePlayerPop.extentionInfo = player.createInfo.extentionInfo;
			}
			coop_Model_StagePlayerPop.transferInfo = player.CreateTransferInfo();
			Send(coop_Model_StagePlayerPop, true, to_client_id, null, null);
		}
	}

	public void SendStageInfo(int to_client_id = 0)
	{
		Coop_Model_StageInfo model = new Coop_Model_StageInfo();
		model.id = 1002;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			model.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
		{
			model.isInFieldEnemyBossBattle = true;
			model.isInFieldFishingEnemyBattle = MonoBehaviourSingleton<CoopManager>.I.coopStage.GetisInFieldFishingEnemyBattle();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.gimmickList.ForEach(delegate(StageObject o)
			{
				if (o.IsCoopNone())
				{
					o.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
				}
				Coop_Model_StageInfo.GimmickInfo item = new Coop_Model_StageInfo.GimmickInfo
				{
					id = o.id,
					enable = o.gameObject.activeSelf
				};
				model.gimmicks.Add(item);
			});
			model.enemyPos = Vector3.zero;
			if ((UnityEngine.Object)MonoBehaviourSingleton<StageObjectManager>.I.boss != (UnityEngine.Object)null)
			{
				model.enemyPos = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.position;
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.HasFieldEnemyBossLimitTime())
		{
			model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
		}
		if (QuestManager.IsValidInGameWaveMatch(false))
		{
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				int i = 0;
				for (int count = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; i < count; i++)
				{
					FieldWaveTargetObject fieldWaveTargetObject = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[i] as FieldWaveTargetObject;
					if (!((UnityEngine.Object)fieldWaveTargetObject == (UnityEngine.Object)null))
					{
						Coop_Model_StageInfo.WaveTargetInfo waveTargetInfo = new Coop_Model_StageInfo.WaveTargetInfo();
						waveTargetInfo.id = fieldWaveTargetObject.id;
						waveTargetInfo.hp = fieldWaveTargetObject.nowHp;
						model.waveTargets.Add(waveTargetInfo);
						fieldWaveTargetObject.SetOwner(true);
					}
				}
			}
		}
		Send(model, true, to_client_id, null, delegate(Coop_Model_Base send_model)
		{
			Coop_Model_StageInfo coop_Model_StageInfo = send_model as Coop_Model_StageInfo;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				coop_Model_StageInfo.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
			}
			return true;
		});
	}

	public void SendObjectInfo(StageObject _stgObj, StageObject.COOP_MODE_TYPE _type, int _to_client_id = 0)
	{
		if (!((UnityEngine.Object)_stgObj == (UnityEngine.Object)null))
		{
			Coop_Model_StageObjectInfo coop_Model_StageObjectInfo = new Coop_Model_StageObjectInfo();
			coop_Model_StageObjectInfo.StageObjectID = _stgObj.id;
			coop_Model_StageObjectInfo.CoopModeType = _type;
			Send(coop_Model_StageObjectInfo, true, _to_client_id, null, null);
		}
	}

	public void SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR error_id = CoopStage.STAGE_REQUEST_ERROR.NONE, int to_client_id = 0)
	{
		Coop_Model_StageResponseEnd coop_Model_StageResponseEnd = new Coop_Model_StageResponseEnd();
		coop_Model_StageResponseEnd.id = 1002;
		coop_Model_StageResponseEnd.error_id = (int)error_id;
		Send(coop_Model_StageResponseEnd, true, to_client_id, null, null);
	}

	public void SendStageQuestClose(bool is_succeed)
	{
		Coop_Model_StageQuestClose coop_Model_StageQuestClose = new Coop_Model_StageQuestClose();
		coop_Model_StageQuestClose.id = 1002;
		coop_Model_StageQuestClose.is_succeed = is_succeed;
		Send(coop_Model_StageQuestClose, true, 0, null, null);
	}

	public void SendStageTimeup()
	{
		Coop_Model_StageTimeup coop_Model_StageTimeup = new Coop_Model_StageTimeup();
		coop_Model_StageTimeup.id = 1002;
		Send(coop_Model_StageTimeup, true, 0, null, null);
	}

	public void SendStageSyncTimeRequest()
	{
		Coop_Model_StageSyncTimeRequest coop_Model_StageSyncTimeRequest = new Coop_Model_StageSyncTimeRequest();
		coop_Model_StageSyncTimeRequest.id = 1002;
		Send(coop_Model_StageSyncTimeRequest, false, 0, null, null);
	}

	public void SendStageSyncTime(float elapsedTime, int toClientId)
	{
		Coop_Model_StageSyncTime coop_Model_StageSyncTime = new Coop_Model_StageSyncTime();
		coop_Model_StageSyncTime.id = 1002;
		coop_Model_StageSyncTime.elapsedTime = elapsedTime;
		Send(coop_Model_StageSyncTime, false, toClientId, null, null);
	}

	public void SendStageChat(int chara_id, int chat_id)
	{
		Coop_Model_StageChat coop_Model_StageChat = new Coop_Model_StageChat();
		coop_Model_StageChat.id = 1002;
		coop_Model_StageChat.chara_id = chara_id;
		coop_Model_StageChat.chat_id = chat_id;
		Send(coop_Model_StageChat, false, 0, null, null);
	}

	public void SendChatMessage(int chara_id, string message)
	{
		Coop_Model_StageChatMessage coop_Model_StageChatMessage = new Coop_Model_StageChatMessage();
		coop_Model_StageChatMessage.id = 1002;
		coop_Model_StageChatMessage.chara_id = chara_id;
		coop_Model_StageChatMessage.text = message;
		coop_Model_StageChatMessage.user_id = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			coop_Model_StageChatMessage.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		}
		Send(coop_Model_StageChatMessage, false, 0, null, null);
	}

	public void SendChatStamp(int chara_id, int stamp_id)
	{
		Coop_Model_StageChatStamp coop_Model_StageChatStamp = new Coop_Model_StageChatStamp();
		coop_Model_StageChatStamp.id = 1002;
		coop_Model_StageChatStamp.chara_id = chara_id;
		coop_Model_StageChatStamp.stamp_id = stamp_id;
		coop_Model_StageChatStamp.user_id = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			coop_Model_StageChatStamp.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		}
		Send(coop_Model_StageChatStamp, false, 0, null, null);
	}

	public void SendRequestPop(int to_client_id, bool is_player, bool is_self, bool promise = false)
	{
		Coop_Model_StageRequestPop coop_Model_StageRequestPop = new Coop_Model_StageRequestPop();
		coop_Model_StageRequestPop.id = 1002;
		coop_Model_StageRequestPop.isPlayer = is_player;
		coop_Model_StageRequestPop.isSelf = is_self;
		Send(coop_Model_StageRequestPop, promise, to_client_id, null, null);
	}

	public void SendSyncPlayerRecord(InGameRecorder.PlayerRecordSyncHost record, int to_client_id, bool promise, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		Coop_Model_StageSyncPlayerRecord coop_Model_StageSyncPlayerRecord = new Coop_Model_StageSyncPlayerRecord();
		coop_Model_StageSyncPlayerRecord.id = 1002;
		coop_Model_StageSyncPlayerRecord.rec = record;
		Send(coop_Model_StageSyncPlayerRecord, promise, to_client_id, null, onPreResend);
	}

	public void SendEnemyBossEscape(int sid, bool promise)
	{
		Coop_Model_EnemyBossEscape coop_Model_EnemyBossEscape = new Coop_Model_EnemyBossEscape();
		coop_Model_EnemyBossEscape.sid = sid;
		coop_Model_EnemyBossEscape.id = 1002;
		Send(coop_Model_EnemyBossEscape, promise, 0, null, null);
	}

	public void SendEnemyBossAliveRequest()
	{
		Coop_Model_EnemyBossAliveRequest coop_Model_EnemyBossAliveRequest = new Coop_Model_EnemyBossAliveRequest();
		coop_Model_EnemyBossAliveRequest.id = 1002;
		Send(coop_Model_EnemyBossAliveRequest, false, 0, null, null);
	}

	public void SendEnemyBossAliveRequested(int toClientId)
	{
		Coop_Model_EnemyBossAliveRequested coop_Model_EnemyBossAliveRequested = new Coop_Model_EnemyBossAliveRequested();
		coop_Model_EnemyBossAliveRequested.id = 1002;
		Send(coop_Model_EnemyBossAliveRequested, false, toClientId, null, null);
	}
}
