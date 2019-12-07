using System;
using System.Collections.Generic;

public class CoopOfflineManager : MonoBehaviourSingleton<CoopOfflineManager>
{
	public class EnemyPopParam
	{
		public FieldMapTable.EnemyPopTableData data;

		public int count;
	}

	private CoopLocalServerSocket svSocket;

	private CoopNetworkPacketReceiver packetReceiver;

	private uint mapId;

	private List<EnemyPopParam> enemyPopParams;

	private int nowEnemyId = 500000;

	public bool isActivate
	{
		get;
		private set;
	}

	public static bool IsValidActivate()
	{
		if (MonoBehaviourSingleton<CoopOfflineManager>.IsValid())
		{
			return MonoBehaviourSingleton<CoopOfflineManager>.I.isActivate;
		}
		return false;
	}

	protected override void Awake()
	{
		base.Awake();
		isActivate = false;
		svSocket = new CoopLocalServerSocket();
		packetReceiver = base.gameObject.AddComponent<CoopNetworkPacketReceiver>();
	}

	private void Update()
	{
		if (isActivate)
		{
			svSocket.Update();
			packetReceiver.OnUpdate();
		}
	}

	private void Logd(string str, params object[] objs)
	{
		_ = Log.enabled;
	}

	public void Clear()
	{
		isActivate = false;
		mapId = 0u;
		enemyPopParams = null;
		packetReceiver.EraseAllPackets();
		Logd("Clear");
	}

	public void Deactivate()
	{
		isActivate = false;
		packetReceiver.EraseAllPackets();
		Logd("Deactivate.");
	}

	public void Activate()
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			Logd("Activate failed with online.");
		}
		else if (!isActivate)
		{
			isActivate = true;
			packetReceiver.EraseAllPackets();
			Logd("Activate.");
			if (mapId != 0)
			{
				svSocket.InitStage(mapId, enemyPopParams, nowEnemyId);
			}
			if (MonoBehaviourSingleton<CoopManager>.I.coopStage.isActivateStart)
			{
				MonoBehaviourSingleton<CoopNetworkManager>.I.RoomStageRequest();
			}
		}
	}

	public void OnStageActivate()
	{
		mapId = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
		InitEnemyPopParam(mapId);
		Logd("OnStageActivate.");
		if (isActivate)
		{
			svSocket.InitStage(mapId, enemyPopParams, nowEnemyId);
		}
	}

	public void OnStageChangeInterval()
	{
		mapId = 0u;
		enemyPopParams = null;
		svSocket.Clear();
		packetReceiver.EraseAllPackets();
		Logd("OnStageChangeInterval.");
	}

	public void OnQuestSeriesInterval()
	{
		mapId = 0u;
		enemyPopParams = null;
		svSocket.Clear();
		packetReceiver.EraseAllPackets();
		Logd("OnQuestSeriesInterval.");
	}

	public int Send<T>(T model, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null) where T : Coop_Model_Base
	{
		if (!isActivate)
		{
			return -1;
		}
		Logd("Recv. {0}", model);
		Coop_Model_ACK arg = svSocket.Recv(model);
		onReceiveAck?.Invoke(arg);
		return 0;
	}

	public void Recv(CoopPacket packet)
	{
		if (isActivate)
		{
			Logd("Send. {0}", packet);
			packetReceiver.Set(packet);
			packetReceiver.OnUpdate();
		}
	}

	private void InitEnemyPopParam(uint map_id)
	{
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(map_id);
		if (enemyPopList != null && enemyPopList.Count > 0)
		{
			nowEnemyId = 500000;
			enemyPopParams = new List<EnemyPopParam>();
			int i = 0;
			for (int count = enemyPopList.Count; i < count; i++)
			{
				EnemyPopParam enemyPopParam = new EnemyPopParam();
				enemyPopParam.data = enemyPopList[i];
				enemyPopParams.Insert(i, enemyPopParam);
			}
		}
	}

	public EnemyPopParam GetEnemyPopParam(int idx)
	{
		if (enemyPopParams == null)
		{
			return null;
		}
		if (idx >= enemyPopParams.Count)
		{
			return null;
		}
		return enemyPopParams[idx];
	}

	public void OnEnemyPop(int idx, int sid)
	{
		EnemyPopParam enemyPopParam = GetEnemyPopParam(idx);
		if (enemyPopParam != null)
		{
			enemyPopParam.count++;
			Logd("OnEnemyPop. idx={0},sid={1},count={2}", idx, sid, enemyPopParam.count);
			if (sid > nowEnemyId)
			{
				nowEnemyId = sid;
			}
		}
	}

	public void EnemyPopForSeriesArena(int index)
	{
		svSocket.SendEnemyPopForSeriesArena(index);
	}
}
