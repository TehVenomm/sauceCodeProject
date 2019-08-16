using System;
using UnityEngine;

public class CoopClientPacketSender : MonoBehaviour
{
	private CoopClient coopClient
	{
		get;
		set;
	}

	public CoopClientPacketSender()
		: this()
	{
	}

	protected virtual void Awake()
	{
		coopClient = this.get_gameObject().GetComponent<CoopClient>();
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
		Send(model, promise: true, to_client_id, null, delegate
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
		Send(coop_Model_ClientBecameHost, promise: true, to_client_id);
	}

	public void SendClientLoadingProgress()
	{
		Coop_Model_ClientLoadingProgress coop_Model_ClientLoadingProgress = new Coop_Model_ClientLoadingProgress();
		coop_Model_ClientLoadingProgress.id = 1003;
		coop_Model_ClientLoadingProgress.per = coopClient.loadingPer;
		Send(coop_Model_ClientLoadingProgress, promise: false);
	}

	public void SendClientChangeEquip()
	{
		Coop_Model_ClientChangeEquip coop_Model_ClientChangeEquip = new Coop_Model_ClientChangeEquip();
		coop_Model_ClientChangeEquip.id = 1003;
		coop_Model_ClientChangeEquip.userInfo = coopClient.userInfo;
		Send(coop_Model_ClientChangeEquip);
	}

	public void SendClientBattleRetire()
	{
		Coop_Model_ClientBattleRetire coop_Model_ClientBattleRetire = new Coop_Model_ClientBattleRetire();
		coop_Model_ClientBattleRetire.id = 1003;
		Send(coop_Model_ClientBattleRetire);
	}

	public void SendClientSeriesProgress(int endPhase)
	{
		Coop_Model_ClientSeriesProgress coop_Model_ClientSeriesProgress = new Coop_Model_ClientSeriesProgress();
		coop_Model_ClientSeriesProgress.id = 1003;
		coop_Model_ClientSeriesProgress.ep = endPhase;
		Send(coop_Model_ClientSeriesProgress);
	}
}
