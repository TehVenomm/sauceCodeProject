using System.Collections.Generic;

public class CoopLocalServerSocket
{
	private CoopLocalServerStage stage;

	public void InitStage(uint map_id, List<CoopOfflineManager.EnemyPopParam> enemy_pop_params, int now_enemy_id)
	{
		stage = new CoopLocalServerStage(this);
		stage.Init(map_id, enemy_pop_params, now_enemy_id);
	}

	public void Clear()
	{
		stage = null;
	}

	public void Update()
	{
		if (stage != null)
		{
			stage.Update();
		}
	}

	public Coop_Model_ACK Recv(Coop_Model_Base model)
	{
		Coop_Model_ACK result = null;
		switch (model.c)
		{
		case 1:
		{
			Coop_Model_Register model4 = model as Coop_Model_Register;
			result = RecvRegister(model4);
			break;
		}
		case 15:
		{
			Coop_Model_RoomStageRequest model3 = model as Coop_Model_RoomStageRequest;
			result = RecvRoomStageRequest(model3);
			break;
		}
		case 23:
		{
			Coop_Model_EnemyOut model2 = model as Coop_Model_EnemyOut;
			result = RecvEnemyOut(model2);
			break;
		}
		}
		return result;
	}

	private Coop_Model_ACK RecvRegister(Coop_Model_Register model)
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			return null;
		}
		return new Coop_Model_RegisterACK
		{
			ack = model.u,
			positive = true,
			sid = MonoBehaviourSingleton<CoopManager>.I.GetSelfID(),
			of = false,
			ids = 
			{
				MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id
			},
			stgids = 
			{
				1
			},
			stgidxs = 
			{
				0
			},
			stghosts = 
			{
				true
			}
		};
	}

	private Coop_Model_ACK RecvRoomStageRequest(Coop_Model_RoomStageRequest model)
	{
		if (stage == null)
		{
			return null;
		}
		stage.StartEnemyPop();
		if (QuestManager.IsValidInGameSeriesArena())
		{
			SendEnemyPopForSeriesArena(0);
		}
		SendRoomStageRequested();
		return null;
	}

	private Coop_Model_ACK RecvEnemyOut(Coop_Model_EnemyOut model)
	{
		if (stage == null)
		{
			return null;
		}
		stage.OutEnemy(model.sid);
		return null;
	}

	public void Send<T>(T model, bool promise = true) where T : Coop_Model_Base
	{
		model.id = 1000;
		CoopPacket packet = CoopPacket.Create(model, -1000, -2000, promise, 0);
		MonoBehaviourSingleton<CoopOfflineManager>.I.Recv(packet);
	}

	public void SendRoomStageRequested()
	{
		Coop_Model_RoomStageRequested coop_Model_RoomStageRequested = new Coop_Model_RoomStageRequested();
		coop_Model_RoomStageRequested.cid = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		Send(coop_Model_RoomStageRequested);
	}

	public void SendEnemyPop(CoopLocalServerEnemy enemy)
	{
		Coop_Model_EnemyPop coop_Model_EnemyPop = new Coop_Model_EnemyPop();
		coop_Model_EnemyPop.sid = enemy.sid;
		coop_Model_EnemyPop.ownerClientId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		coop_Model_EnemyPop.popIndex = enemy.popIndex;
		Send(coop_Model_EnemyPop);
	}

	public void SendEnemyPopForSeriesArena(int index)
	{
		Coop_Model_EnemyPop coop_Model_EnemyPop = new Coop_Model_EnemyPop();
		coop_Model_EnemyPop.sid = stage.GenerateEnemyUniqId();
		coop_Model_EnemyPop.ownerClientId = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		coop_Model_EnemyPop.popIndex = index;
		coop_Model_EnemyPop.seriesIdx = index;
		Send(coop_Model_EnemyPop);
	}

	public void SendEnemyExtermination()
	{
		Coop_Model_EnemyExtermination model = new Coop_Model_EnemyExtermination();
		Send(model);
	}
}
