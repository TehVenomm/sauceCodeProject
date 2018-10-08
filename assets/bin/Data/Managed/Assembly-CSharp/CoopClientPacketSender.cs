using System;
using UnityEngine;

public class CoopClientPacketSender : MonoBehaviour
{
	private CoopClient coopClient
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopClient = base.gameObject.GetComponent<CoopClient>();
	}

	protected virtual void Start()
	{
	}

	private int Send<T>(T model, bool promise = true, int to_client_id = 0, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (to_client_id == 0)
		{
			return MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcast(model, promise, onReceiveAck, onPreResend);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo(to_client_id, model, promise, onReceiveAck, onPreResend);
	}

	public void SendClientStatus(int to_client_id = 0)
	{
		Coop_Model_ClientStatus model = new Coop_Model_ClientStatus();
		model.id = 1003;
		model.status = (int)coopClient.status;
		model.joinType = (int)coopClient.joinType;
		Send(model, true, to_client_id, null, delegate
		{
			if (model.status != (int)coopClient.status)
			{
				return false;
			}
			return true;
		});
	}

	public void SendClientBecameHost(int to_client_id = 0)
	{
		Coop_Model_ClientBecameHost coop_Model_ClientBecameHost = new Coop_Model_ClientBecameHost();
		coop_Model_ClientBecameHost.id = 1003;
		Send(coop_Model_ClientBecameHost, true, to_client_id, null, null);
	}

	public void SendClientLoadingProgress()
	{
		Coop_Model_ClientLoadingProgress coop_Model_ClientLoadingProgress = new Coop_Model_ClientLoadingProgress();
		coop_Model_ClientLoadingProgress.id = 1003;
		coop_Model_ClientLoadingProgress.per = coopClient.loadingPer;
		Send(coop_Model_ClientLoadingProgress, false, 0, null, null);
	}

	public void SendClientChangeEquip()
	{
		Coop_Model_ClientChangeEquip coop_Model_ClientChangeEquip = new Coop_Model_ClientChangeEquip();
		coop_Model_ClientChangeEquip.id = 1003;
		coop_Model_ClientChangeEquip.userInfo = coopClient.userInfo;
		Send(coop_Model_ClientChangeEquip, true, 0, null, null);
	}

	public void SendClientBattleRetire()
	{
		Coop_Model_ClientBattleRetire coop_Model_ClientBattleRetire = new Coop_Model_ClientBattleRetire();
		coop_Model_ClientBattleRetire.id = 1003;
		Send(coop_Model_ClientBattleRetire, true, 0, null, null);
	}

	public void SendClientSeriesProgress(int endPhase)
	{
		Coop_Model_ClientSeriesProgress coop_Model_ClientSeriesProgress = new Coop_Model_ClientSeriesProgress();
		coop_Model_ClientSeriesProgress.id = 1003;
		coop_Model_ClientSeriesProgress.ep = endPhase;
		Send(coop_Model_ClientSeriesProgress, true, 0, null, null);
	}
}
