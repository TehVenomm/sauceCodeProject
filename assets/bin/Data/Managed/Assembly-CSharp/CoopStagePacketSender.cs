using System;
using UnityEngine;

public class CoopStagePacketSender : MonoBehaviour
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	public CoopStagePacketSender()
		: this()
	{
	}

	protected virtual void Awake()
	{
		coopStage = this.get_gameObject().GetComponent<CoopStage>();
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
		Send(coop_Model_StageRequest, promise: true, to_client_id);
	}

	public void SendStagePlayerPop(Player player, int to_client_id = 0)
	{
		if (player == null)
		{
			return;
		}
		if (player.IsCoopNone())
		{
			player.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
		}
		Coop_Model_StagePlayerPop coop_Model_StagePlayerPop = new Coop_Model_StagePlayerPop();
		coop_Model_StagePlayerPop.id = 1002;
		coop_Model_StagePlayerPop.sid = player.id;
		coop_Model_StagePlayerPop.isSelf = (player is Self);
		NonPlayer nonPlayer = player as NonPlayer;
		if (player.createInfo != null)
		{
			if (nonPlayer == null)
			{
				coop_Model_StagePlayerPop.charaInfo = player.createInfo.charaInfo;
			}
			coop_Model_StagePlayerPop.extentionInfo = player.createInfo.extentionInfo;
		}
		coop_Model_StagePlayerPop.transferInfo = player.CreateTransferInfo();
		Send(coop_Model_StagePlayerPop, promise: true, to_client_id);
	}

	public void SendStageInfo(int to_client_id = 0)
	{
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
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
					enable = o.get_gameObject().get_activeSelf()
				};
				model.gimmicks.Add(item);
			});
			MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmicksObjs(InGameProgress.eFieldGimmick.CarriableGimmick).ForEach(delegate(IFieldGimmickObject gimmick)
			{
				FieldCarriableGimmickObject fieldCarriableGimmickObject = gimmick as FieldCarriableGimmickObject;
				if (!(fieldCarriableGimmickObject == null))
				{
					model.carriableGimmickInfos.Add(fieldCarriableGimmickObject.GetCarriableGimmickInfo());
				}
			});
			MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmicksObjs(InGameProgress.eFieldGimmick.SupplyGimmick).ForEach(delegate(IFieldGimmickObject gimmick)
			{
				FieldSupplyGimmickObject fieldSupplyGimmickObject = gimmick as FieldSupplyGimmickObject;
				if (!(fieldSupplyGimmickObject == null))
				{
					model.supplyGimmickInfos.Add(fieldSupplyGimmickObject.GetSupplyGimmickInfo());
				}
			});
			model.enemyPos = Vector3.get_zero();
			if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
			{
				model.enemyPos = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.get_position();
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
		if (QuestManager.IsValidInGameWaveMatch())
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
					if (!(fieldWaveTargetObject == null))
					{
						Coop_Model_StageInfo.WaveTargetInfo waveTargetInfo = new Coop_Model_StageInfo.WaveTargetInfo();
						waveTargetInfo.id = fieldWaveTargetObject.id;
						waveTargetInfo.hp = fieldWaveTargetObject.nowHp;
						model.waveTargets.Add(waveTargetInfo);
						fieldWaveTargetObject.SetOwner(isOwner: true);
					}
				}
			}
		}
		if (QuestManager.IsValidInGameWaveStrategy())
		{
			coopStage.SetFirstWaveMatchInfoForStageInfo(ref model);
		}
		Send(model, promise: true, to_client_id, null, delegate(Coop_Model_Base send_model)
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
		if (!(_stgObj == null))
		{
			Coop_Model_StageObjectInfo coop_Model_StageObjectInfo = new Coop_Model_StageObjectInfo();
			coop_Model_StageObjectInfo.StageObjectID = _stgObj.id;
			coop_Model_StageObjectInfo.CoopModeType = _type;
			Send(coop_Model_StageObjectInfo, promise: true, _to_client_id);
		}
	}

	public void SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR error_id = CoopStage.STAGE_REQUEST_ERROR.NONE, int to_client_id = 0)
	{
		Coop_Model_StageResponseEnd coop_Model_StageResponseEnd = new Coop_Model_StageResponseEnd();
		coop_Model_StageResponseEnd.id = 1002;
		coop_Model_StageResponseEnd.error_id = (int)error_id;
		Send(coop_Model_StageResponseEnd, promise: true, to_client_id);
	}

	public void SendStageQuestClose(bool is_succeed)
	{
		Coop_Model_StageQuestClose coop_Model_StageQuestClose = new Coop_Model_StageQuestClose();
		coop_Model_StageQuestClose.id = 1002;
		coop_Model_StageQuestClose.is_succeed = is_succeed;
		Send(coop_Model_StageQuestClose);
	}

	public void SendStageTimeup()
	{
		Coop_Model_StageTimeup coop_Model_StageTimeup = new Coop_Model_StageTimeup();
		coop_Model_StageTimeup.id = 1002;
		Send(coop_Model_StageTimeup);
	}

	public void SendStageSyncTimeRequest()
	{
		Coop_Model_StageSyncTimeRequest coop_Model_StageSyncTimeRequest = new Coop_Model_StageSyncTimeRequest();
		coop_Model_StageSyncTimeRequest.id = 1002;
		Send(coop_Model_StageSyncTimeRequest, promise: false);
	}

	public void SendStageSyncTime(float elapsedTime, int toClientId)
	{
		Coop_Model_StageSyncTime coop_Model_StageSyncTime = new Coop_Model_StageSyncTime();
		coop_Model_StageSyncTime.id = 1002;
		coop_Model_StageSyncTime.elapsedTime = elapsedTime;
		Send(coop_Model_StageSyncTime, promise: false, toClientId);
	}

	public void SendStageChat(int chara_id, int chat_id)
	{
		Coop_Model_StageChat coop_Model_StageChat = new Coop_Model_StageChat();
		coop_Model_StageChat.id = 1002;
		coop_Model_StageChat.chara_id = chara_id;
		coop_Model_StageChat.chat_id = chat_id;
		Send(coop_Model_StageChat, promise: false);
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
		Send(coop_Model_StageChatMessage, promise: false);
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
		Send(coop_Model_StageChatStamp, promise: false);
	}

	public void SendRequestPop(int to_client_id, bool is_player, bool is_self, bool promise = false)
	{
		Coop_Model_StageRequestPop coop_Model_StageRequestPop = new Coop_Model_StageRequestPop();
		coop_Model_StageRequestPop.id = 1002;
		coop_Model_StageRequestPop.isPlayer = is_player;
		coop_Model_StageRequestPop.isSelf = is_self;
		Send(coop_Model_StageRequestPop, promise, to_client_id);
	}

	public void SendSyncPlayerRecord(InGameRecorder.PlayerRecordSyncHost record, int to_client_id, bool promise, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		Coop_Model_StageSyncPlayerRecord coop_Model_StageSyncPlayerRecord = new Coop_Model_StageSyncPlayerRecord();
		coop_Model_StageSyncPlayerRecord.id = 1002;
		coop_Model_StageSyncPlayerRecord.rec = record;
		Coop_Model_StageSyncPlayerRecord model = coop_Model_StageSyncPlayerRecord;
		bool promise2 = promise;
		Send(model, promise2, to_client_id, null, onPreResend);
	}

	public void SendEnemyBossEscape(int sid, bool promise)
	{
		Coop_Model_EnemyBossEscape coop_Model_EnemyBossEscape = new Coop_Model_EnemyBossEscape();
		coop_Model_EnemyBossEscape.sid = sid;
		coop_Model_EnemyBossEscape.id = 1002;
		Send(coop_Model_EnemyBossEscape, promise);
	}

	public void SendEnemyBossAliveRequest()
	{
		Coop_Model_EnemyBossAliveRequest coop_Model_EnemyBossAliveRequest = new Coop_Model_EnemyBossAliveRequest();
		coop_Model_EnemyBossAliveRequest.id = 1002;
		Send(coop_Model_EnemyBossAliveRequest, promise: false);
	}

	public void SendEnemyBossAliveRequested(int toClientId)
	{
		Coop_Model_EnemyBossAliveRequested coop_Model_EnemyBossAliveRequested = new Coop_Model_EnemyBossAliveRequested();
		coop_Model_EnemyBossAliveRequested.id = 1002;
		Send(coop_Model_EnemyBossAliveRequested, promise: false, toClientId);
	}

	public void OnActiveSupply(int pointId)
	{
		Coop_Model_ActiveSupply coop_Model_ActiveSupply = new Coop_Model_ActiveSupply();
		coop_Model_ActiveSupply.id = 1002;
		coop_Model_ActiveSupply.pointId = pointId;
		Send(coop_Model_ActiveSupply);
	}
}
