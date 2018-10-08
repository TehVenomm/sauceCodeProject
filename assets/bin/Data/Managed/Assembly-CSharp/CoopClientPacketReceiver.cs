public class CoopClientPacketReceiver : PacketReceiver
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

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.CLIENT_STATUS:
		{
			Coop_Model_ClientStatus model5 = packet.GetModel<Coop_Model_ClientStatus>();
			result = coopClient.OnRecvClientStatus(model5, packet);
			break;
		}
		case PACKET_TYPE.CLIENT_LOADING_PROGRESS:
		{
			Coop_Model_ClientLoadingProgress model4 = packet.GetModel<Coop_Model_ClientLoadingProgress>();
			result = coopClient.OnRecvClientLoadingProgress(model4);
			break;
		}
		case PACKET_TYPE.CLIENT_CHANGE_EQUIP:
		{
			Coop_Model_ClientChangeEquip model3 = packet.GetModel<Coop_Model_ClientChangeEquip>();
			result = coopClient.OnRecvClientChangeEquip(model3);
			break;
		}
		case PACKET_TYPE.CLIENT_BATTLE_RETIRE:
		{
			Coop_Model_ClientBattleRetire model2 = packet.GetModel<Coop_Model_ClientBattleRetire>();
			result = coopClient.OnRecvClientBattleRetire(model2);
			break;
		}
		case PACKET_TYPE.CLIENT_SERIES_PROGRESS:
		{
			Coop_Model_ClientSeriesProgress model = packet.GetModel<Coop_Model_ClientSeriesProgress>();
			result = coopClient.OnRecvClientSeriesProgress(model);
			break;
		}
		}
		return result;
	}
}
