using System;

public class CoopClientPacketSender
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
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

	public unsafe void SendClientStatus(int to_client_id = 0)
	{
		Coop_Model_ClientStatus model = new Coop_Model_ClientStatus();
		model.id = 1003;
		model.status = (int)coopClient.status;
		model.joinType = (int)coopClient.joinType;
		_003CSendClientStatus_003Ec__AnonStorey4EC _003CSendClientStatus_003Ec__AnonStorey4EC;
		Send(model, true, to_client_id, null, new Func<Coop_Model_Base, bool>((object)_003CSendClientStatus_003Ec__AnonStorey4EC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
