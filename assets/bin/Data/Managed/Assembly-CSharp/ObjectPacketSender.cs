using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPacketSender
{
	public class ActionHistoryData
	{
		public float startTime = -1f;

		public Vector3 startPos = Vector3.get_zero();

		public float startDir;
	}

	protected List<Coop_Model_ObjectBase> actionHistoryList = new List<Coop_Model_ObjectBase>();

	protected ActionHistoryData actionHistoryData;

	public StageObject owner
	{
		get;
		protected set;
	}

	public bool enableSend
	{
		get;
		set;
	}

	public float needWaitSyncTime
	{
		get;
		protected set;
	}

	public ObjectPacketSender()
		: this()
	{
		needWaitSyncTime = 0f;
		enableSend = true;
	}

	public static ObjectPacketSender SetupComponent(StageObject set_object)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (set_object is Enemy)
		{
			return set_object.get_gameObject().AddComponent<EnemyPacketSender>();
		}
		if (set_object is Player)
		{
			return set_object.get_gameObject().AddComponent<PlayerPacketSender>();
		}
		if (set_object is Character)
		{
			return set_object.get_gameObject().AddComponent<CharacterPacketSender>();
		}
		return set_object.get_gameObject().AddComponent<ObjectPacketSender>();
	}

	protected virtual void Awake()
	{
		owner = this.GetComponent<StageObject>();
	}

	protected int SendTo<T>(int to_client_id, T model, bool promise = false, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (!enableSend)
		{
			Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (PACKET_TYPE)model.c);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendToInBattle(to_client_id, model, promise, onReceiveAck, onPreResend);
	}

	protected int SendBroadcast<T>(T model, bool promise = false, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (!enableSend)
		{
			Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (PACKET_TYPE)model.c);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcastInBattle(model, promise, onReceiveAck, onPreResend);
	}

	protected int SendToExtra<T>(int to_client_id, T model, bool promise = false, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (!enableSend)
		{
			Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (PACKET_TYPE)model.c);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(to_client_id, model, promise, onReceiveAck, onPreResend);
	}

	public virtual bool IsEnableWaitSync()
	{
		return false;
	}

	public virtual float GetWaitTime(float base_time)
	{
		if (!IsEnableWaitSync())
		{
			return base_time;
		}
		float num = needWaitSyncTime;
		if (num > owner.objectParameter.maxWaitSyncTime)
		{
			num = owner.objectParameter.maxWaitSyncTime;
		}
		if (num < base_time)
		{
			num = base_time;
		}
		return num;
	}

	protected virtual void ClearActionHistory()
	{
		actionHistoryList.Clear();
		actionHistoryData = null;
	}

	protected virtual void StackActionHistory(Coop_Model_ObjectBase stack_model, bool is_act_model)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (is_act_model)
		{
			ClearActionHistory();
		}
		else if (actionHistoryList.Count <= 0)
		{
			return;
		}
		if (actionHistoryList.Count <= 0)
		{
			actionHistoryData = new ActionHistoryData();
			actionHistoryData.startTime = Time.get_time();
			actionHistoryData.startPos = owner._position;
			ActionHistoryData obj = actionHistoryData;
			Quaternion rotation = owner._rotation;
			Vector3 eulerAngles = rotation.get_eulerAngles();
			obj.startDir = eulerAngles.y;
		}
		actionHistoryList.Add(stack_model);
	}

	protected virtual void SendActionHistory(int to_client_id = 0)
	{
		int i = 0;
		for (int count = actionHistoryList.Count; i < count; i++)
		{
			if (to_client_id == 0)
			{
				SendBroadcast(actionHistoryList[i], false, null, null);
			}
			else
			{
				SendToExtra(to_client_id, actionHistoryList[i], false, null, null);
			}
		}
		SaveNeedWaitSyncTime();
	}

	public void SaveNeedWaitSyncTime()
	{
		float num = 0f;
		if (actionHistoryData != null && actionHistoryData.startTime >= 0f)
		{
			num = Time.get_time() - actionHistoryData.startTime;
		}
		if (num > needWaitSyncTime)
		{
			needWaitSyncTime = num;
		}
	}

	public void PassNeedWaitSyncTime(float time)
	{
		if (!(time <= 0f) && needWaitSyncTime > 0f)
		{
			needWaitSyncTime -= time;
			if (needWaitSyncTime < 0f)
			{
				needWaitSyncTime = 0f;
			}
		}
	}

	public virtual void OnUpdate()
	{
	}

	public virtual void OnDestroyObject()
	{
		if (enableSend && owner.IsOriginal())
		{
			Coop_Model_ObjectDestroy coop_Model_ObjectDestroy = new Coop_Model_ObjectDestroy();
			coop_Model_ObjectDestroy.id = owner.id;
			SendBroadcast(coop_Model_ObjectDestroy, true, null, null);
		}
	}

	public virtual void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		if (enableSend && !owner.IsCoopNone())
		{
			Coop_Model_ObjectAttackedHitOwner coop_Model_ObjectAttackedHitOwner = new Coop_Model_ObjectAttackedHitOwner();
			coop_Model_ObjectAttackedHitOwner.id = owner.id;
			coop_Model_ObjectAttackedHitOwner.SetAttackedHitStatus(status);
			SendTo(owner.coopClientId, coop_Model_ObjectAttackedHitOwner, false, null, null);
		}
	}

	public unsafe virtual void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		if (enableSend && owner.IsOriginal())
		{
			bool flag = false;
			if (status.afterHP <= 0)
			{
				flag = true;
			}
			if (status.breakRegion)
			{
				flag = true;
			}
			Coop_Model_ObjectAttackedHitFix coop_Model_ObjectAttackedHitFix = new Coop_Model_ObjectAttackedHitFix();
			coop_Model_ObjectAttackedHitFix.id = owner.id;
			coop_Model_ObjectAttackedHitFix.SetAttackedHitStatus(status);
			if (flag)
			{
				SendBroadcast(coop_Model_ObjectAttackedHitFix, true, null, new Func<Coop_Model_Base, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else
			{
				SendBroadcast(coop_Model_ObjectAttackedHitFix, false, null, null);
			}
		}
	}

	public virtual void OnKeepWaitingPacket(StageObject.WAITING_PACKET waiting_packet_type)
	{
		if (enableSend && owner.IsOriginal())
		{
			Coop_Model_ObjectKeepWaitingPacket coop_Model_ObjectKeepWaitingPacket = new Coop_Model_ObjectKeepWaitingPacket();
			coop_Model_ObjectKeepWaitingPacket.id = owner.id;
			coop_Model_ObjectKeepWaitingPacket.type = (int)waiting_packet_type;
			SendBroadcast(coop_Model_ObjectKeepWaitingPacket, false, null, null);
		}
	}

	public void OnBulletObservableSet(int observedID)
	{
		if (enableSend && owner.IsOriginal())
		{
			Coop_Model_ObjectBulletObservableSet coop_Model_ObjectBulletObservableSet = new Coop_Model_ObjectBulletObservableSet();
			coop_Model_ObjectBulletObservableSet.id = owner.id;
			coop_Model_ObjectBulletObservableSet.observedID = observedID;
			SendBroadcast(coop_Model_ObjectBulletObservableSet, false, null, null);
		}
	}

	public void OnBulletObservableBroken(int observedID, bool isSendOnlyOriginal)
	{
		if (enableSend)
		{
			if (isSendOnlyOriginal)
			{
				if (!owner.IsOriginal())
				{
					return;
				}
			}
			else if (owner.IsCoopNone())
			{
				return;
			}
			Coop_Model_ObjectBulletObservableBroken coop_Model_ObjectBulletObservableBroken = new Coop_Model_ObjectBulletObservableBroken();
			coop_Model_ObjectBulletObservableBroken.id = owner.id;
			coop_Model_ObjectBulletObservableBroken.observedID = observedID;
			SendBroadcast(coop_Model_ObjectBulletObservableBroken, false, null, null);
		}
	}

	public void OnShotGimmickGenerator(Vector3 pos)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (enableSend && owner.IsOriginal())
		{
			Coop_Model_ObjectShotGimmickGenerator coop_Model_ObjectShotGimmickGenerator = new Coop_Model_ObjectShotGimmickGenerator();
			coop_Model_ObjectShotGimmickGenerator.id = owner.id;
			coop_Model_ObjectShotGimmickGenerator.pos = pos;
			SendBroadcast(coop_Model_ObjectShotGimmickGenerator, false, null, null);
		}
	}

	public void OnSetCoopMode(StageObject.COOP_MODE_TYPE coopModeType)
	{
		if (enableSend && owner.IsOriginal())
		{
			Coop_Model_ObjectCoopInfo coop_Model_ObjectCoopInfo = new Coop_Model_ObjectCoopInfo();
			coop_Model_ObjectCoopInfo.id = owner.id;
			coop_Model_ObjectCoopInfo.CoopModeType = coopModeType;
			SendBroadcast(coop_Model_ObjectCoopInfo, false, null, null);
		}
	}
}
