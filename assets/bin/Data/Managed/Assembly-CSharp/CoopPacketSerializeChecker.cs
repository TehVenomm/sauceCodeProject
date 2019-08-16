using System;

public class CoopPacketSerializeChecker
{
	public static void Test()
	{
		Test0();
		Test1();
		Test2();
	}

	public static void Test0()
	{
		CoopPacketSerializer coopPacketSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
		CoopPacket coopPacket = new CoopPacket();
		Coop_Model_Register coop_Model_Register = (Coop_Model_Register)(coopPacket.model = new Coop_Model_Register());
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, promise: false, 0);
		PacketStream stream = null;
		try
		{
			stream = coopPacketSerializer.Serialize(coopPacket);
		}
		catch (Exception arg)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Register\n" + arg);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_Register>(stream);
		}
		catch (Exception arg2)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Register\n" + arg2);
		}
		CoopPacket coopPacket2 = new CoopPacket();
		Coop_Model_RegisterACK coop_Model_RegisterACK = (Coop_Model_RegisterACK)(coopPacket2.model = new Coop_Model_RegisterACK());
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, promise: false, 0);
		PacketStream stream2 = null;
		try
		{
			stream2 = coopPacketSerializer.Serialize(coopPacket2);
		}
		catch (Exception arg3)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RegisterACK\n" + arg3);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RegisterACK>(stream2);
		}
		catch (Exception arg4)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RegisterACK\n" + arg4);
		}
		CoopPacket coopPacket3 = new CoopPacket();
		Coop_Model_ACK coop_Model_ACK = (Coop_Model_ACK)(coopPacket3.model = new Coop_Model_ACK());
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, promise: false, 0);
		PacketStream stream3 = null;
		try
		{
			stream3 = coopPacketSerializer.Serialize(coopPacket3);
		}
		catch (Exception arg5)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ACK\n" + arg5);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ACK>(stream3);
		}
		catch (Exception arg6)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ACK\n" + arg6);
		}
		CoopPacket coopPacket4 = new CoopPacket();
		Coop_Model_Disconnect coop_Model_Disconnect = (Coop_Model_Disconnect)(coopPacket4.model = new Coop_Model_Disconnect());
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, promise: false, 0);
		PacketStream stream4 = null;
		try
		{
			stream4 = coopPacketSerializer.Serialize(coopPacket4);
		}
		catch (Exception arg7)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Disconnect\n" + arg7);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_Disconnect>(stream4);
		}
		catch (Exception arg8)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Disconnect\n" + arg8);
		}
		CoopPacket coopPacket5 = new CoopPacket();
		Coop_Model_Alive coop_Model_Alive = (Coop_Model_Alive)(coopPacket5.model = new Coop_Model_Alive());
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, promise: false, 0);
		PacketStream stream5 = null;
		try
		{
			stream5 = coopPacketSerializer.Serialize(coopPacket5);
		}
		catch (Exception arg9)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Alive\n" + arg9);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_Alive>(stream5);
		}
		catch (Exception arg10)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Alive\n" + arg10);
		}
		CoopPacket coopPacket6 = new CoopPacket();
		Coop_Model_RoomEntryClose coop_Model_RoomEntryClose = (Coop_Model_RoomEntryClose)(coopPacket6.model = new Coop_Model_RoomEntryClose());
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, promise: false, 0);
		PacketStream stream6 = null;
		try
		{
			stream6 = coopPacketSerializer.Serialize(coopPacket6);
		}
		catch (Exception arg11)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomEntryClose\n" + arg11);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomEntryClose>(stream6);
		}
		catch (Exception arg12)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomEntryClose\n" + arg12);
		}
		CoopPacket coopPacket7 = new CoopPacket();
		Coop_Model_RoomJoined coop_Model_RoomJoined = (Coop_Model_RoomJoined)(coopPacket7.model = new Coop_Model_RoomJoined());
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, promise: false, 0);
		PacketStream stream7 = null;
		try
		{
			stream7 = coopPacketSerializer.Serialize(coopPacket7);
		}
		catch (Exception arg13)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomJoined\n" + arg13);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomJoined>(stream7);
		}
		catch (Exception arg14)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomJoined\n" + arg14);
		}
		CoopPacket coopPacket8 = new CoopPacket();
		Coop_Model_RoomLeaved coop_Model_RoomLeaved = (Coop_Model_RoomLeaved)(coopPacket8.model = new Coop_Model_RoomLeaved());
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, promise: false, 0);
		PacketStream stream8 = null;
		try
		{
			stream8 = coopPacketSerializer.Serialize(coopPacket8);
		}
		catch (Exception arg15)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomLeaved\n" + arg15);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomLeaved>(stream8);
		}
		catch (Exception arg16)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomLeaved\n" + arg16);
		}
		CoopPacket coopPacket9 = new CoopPacket();
		Coop_Model_RoomStageChange coop_Model_RoomStageChange = (Coop_Model_RoomStageChange)(coopPacket9.model = new Coop_Model_RoomStageChange());
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, promise: false, 0);
		PacketStream stream9 = null;
		try
		{
			stream9 = coopPacketSerializer.Serialize(coopPacket9);
		}
		catch (Exception arg17)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageChange\n" + arg17);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomStageChange>(stream9);
		}
		catch (Exception arg18)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageChange\n" + arg18);
		}
		CoopPacket coopPacket10 = new CoopPacket();
		Coop_Model_RoomStageChanged coop_Model_RoomStageChanged = (Coop_Model_RoomStageChanged)(coopPacket10.model = new Coop_Model_RoomStageChanged());
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, promise: false, 0);
		PacketStream stream10 = null;
		try
		{
			stream10 = coopPacketSerializer.Serialize(coopPacket10);
		}
		catch (Exception arg19)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageChanged\n" + arg19);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomStageChanged>(stream10);
		}
		catch (Exception arg20)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageChanged\n" + arg20);
		}
		CoopPacket coopPacket11 = new CoopPacket();
		Coop_Model_RoomStageRequest coop_Model_RoomStageRequest = (Coop_Model_RoomStageRequest)(coopPacket11.model = new Coop_Model_RoomStageRequest());
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, promise: false, 0);
		PacketStream stream11 = null;
		try
		{
			stream11 = coopPacketSerializer.Serialize(coopPacket11);
		}
		catch (Exception arg21)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageRequest\n" + arg21);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomStageRequest>(stream11);
		}
		catch (Exception arg22)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageRequest\n" + arg22);
		}
		CoopPacket coopPacket12 = new CoopPacket();
		Coop_Model_RoomStageRequested coop_Model_RoomStageRequested = (Coop_Model_RoomStageRequested)(coopPacket12.model = new Coop_Model_RoomStageRequested());
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, promise: false, 0);
		PacketStream stream12 = null;
		try
		{
			stream12 = coopPacketSerializer.Serialize(coopPacket12);
		}
		catch (Exception arg23)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageRequested\n" + arg23);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomStageRequested>(stream12);
		}
		catch (Exception arg24)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageRequested\n" + arg24);
		}
		CoopPacket coopPacket13 = new CoopPacket();
		Coop_Model_RoomStageHostChanged coop_Model_RoomStageHostChanged = (Coop_Model_RoomStageHostChanged)(coopPacket13.model = new Coop_Model_RoomStageHostChanged());
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, promise: false, 0);
		PacketStream stream13 = null;
		try
		{
			stream13 = coopPacketSerializer.Serialize(coopPacket13);
		}
		catch (Exception arg25)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageHostChanged\n" + arg25);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomStageHostChanged>(stream13);
		}
		catch (Exception arg26)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageHostChanged\n" + arg26);
		}
		CoopPacket coopPacket14 = new CoopPacket();
		Coop_Model_BattleStart coop_Model_BattleStart = (Coop_Model_BattleStart)(coopPacket14.model = new Coop_Model_BattleStart());
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, promise: false, 0);
		PacketStream stream14 = null;
		try
		{
			stream14 = coopPacketSerializer.Serialize(coopPacket14);
		}
		catch (Exception arg27)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_BattleStart\n" + arg27);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_BattleStart>(stream14);
		}
		catch (Exception arg28)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_BattleStart\n" + arg28);
		}
		CoopPacket coopPacket15 = new CoopPacket();
		Coop_Model_EnemyPop coop_Model_EnemyPop = (Coop_Model_EnemyPop)(coopPacket15.model = new Coop_Model_EnemyPop());
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, promise: false, 0);
		PacketStream stream15 = null;
		try
		{
			stream15 = coopPacketSerializer.Serialize(coopPacket15);
		}
		catch (Exception arg29)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyPop\n" + arg29);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyPop>(stream15);
		}
		catch (Exception arg30)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyPop\n" + arg30);
		}
		CoopPacket coopPacket16 = new CoopPacket();
		Coop_Model_EnemyAttack coop_Model_EnemyAttack = (Coop_Model_EnemyAttack)(coopPacket16.model = new Coop_Model_EnemyAttack());
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, promise: false, 0);
		PacketStream stream16 = null;
		try
		{
			stream16 = coopPacketSerializer.Serialize(coopPacket16);
		}
		catch (Exception arg31)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyAttack\n" + arg31);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyAttack>(stream16);
		}
		catch (Exception arg32)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyAttack\n" + arg32);
		}
		CoopPacket coopPacket17 = new CoopPacket();
		Coop_Model_EnemyOut coop_Model_EnemyOut = (Coop_Model_EnemyOut)(coopPacket17.model = new Coop_Model_EnemyOut());
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, promise: false, 0);
		PacketStream stream17 = null;
		try
		{
			stream17 = coopPacketSerializer.Serialize(coopPacket17);
		}
		catch (Exception arg33)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyOut\n" + arg33);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyOut>(stream17);
		}
		catch (Exception arg34)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyOut\n" + arg34);
		}
		CoopPacket coopPacket18 = new CoopPacket();
		Coop_Model_EnemyDefeat coop_Model_EnemyDefeat = (Coop_Model_EnemyDefeat)(coopPacket18.model = new Coop_Model_EnemyDefeat());
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, promise: false, 0);
		PacketStream stream18 = null;
		try
		{
			stream18 = coopPacketSerializer.Serialize(coopPacket18);
		}
		catch (Exception arg35)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyDefeat\n" + arg35);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyDefeat>(stream18);
		}
		catch (Exception arg36)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyDefeat\n" + arg36);
		}
		CoopPacket coopPacket19 = new CoopPacket();
		Coop_Model_RewardGet coop_Model_RewardGet = (Coop_Model_RewardGet)(coopPacket19.model = new Coop_Model_RewardGet());
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, promise: false, 0);
		PacketStream stream19 = null;
		try
		{
			stream19 = coopPacketSerializer.Serialize(coopPacket19);
		}
		catch (Exception arg37)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RewardGet\n" + arg37);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RewardGet>(stream19);
		}
		catch (Exception arg38)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RewardGet\n" + arg38);
		}
		CoopPacket coopPacket20 = new CoopPacket();
		Coop_Model_RewardPickup coop_Model_RewardPickup = (Coop_Model_RewardPickup)(coopPacket20.model = new Coop_Model_RewardPickup());
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, promise: false, 0);
		PacketStream stream20 = null;
		try
		{
			stream20 = coopPacketSerializer.Serialize(coopPacket20);
		}
		catch (Exception arg39)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RewardPickup\n" + arg39);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RewardPickup>(stream20);
		}
		catch (Exception arg40)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RewardPickup\n" + arg40);
		}
		CoopPacket coopPacket21 = new CoopPacket();
		Coop_Model_EnemyExtermination coop_Model_EnemyExtermination = (Coop_Model_EnemyExtermination)(coopPacket21.model = new Coop_Model_EnemyExtermination());
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, promise: false, 0);
		PacketStream stream21 = null;
		try
		{
			stream21 = coopPacketSerializer.Serialize(coopPacket21);
		}
		catch (Exception arg41)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyExtermination\n" + arg41);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyExtermination>(stream21);
		}
		catch (Exception arg42)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyExtermination\n" + arg42);
		}
		CoopPacket coopPacket22 = new CoopPacket();
		Coop_Model_UpdateBoost coop_Model_UpdateBoost = (Coop_Model_UpdateBoost)(coopPacket22.model = new Coop_Model_UpdateBoost());
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, promise: false, 0);
		PacketStream stream22 = null;
		try
		{
			stream22 = coopPacketSerializer.Serialize(coopPacket22);
		}
		catch (Exception arg43)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_UpdateBoost\n" + arg43);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_UpdateBoost>(stream22);
		}
		catch (Exception arg44)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_UpdateBoost\n" + arg44);
		}
		CoopPacket coopPacket23 = new CoopPacket();
		Coop_Model_UpdateBoostComplete coop_Model_UpdateBoostComplete = (Coop_Model_UpdateBoostComplete)(coopPacket23.model = new Coop_Model_UpdateBoostComplete());
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, promise: false, 0);
		PacketStream stream23 = null;
		try
		{
			stream23 = coopPacketSerializer.Serialize(coopPacket23);
		}
		catch (Exception arg45)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_UpdateBoostComplete\n" + arg45);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_UpdateBoostComplete>(stream23);
		}
		catch (Exception arg46)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_UpdateBoostComplete\n" + arg46);
		}
		CoopPacket coopPacket24 = new CoopPacket();
		Coop_Model_RoomTimeCheck coop_Model_RoomTimeCheck = (Coop_Model_RoomTimeCheck)(coopPacket24.model = new Coop_Model_RoomTimeCheck());
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, promise: false, 0);
		PacketStream stream24 = null;
		try
		{
			stream24 = coopPacketSerializer.Serialize(coopPacket24);
		}
		catch (Exception arg47)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomTimeCheck\n" + arg47);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomTimeCheck>(stream24);
		}
		catch (Exception arg48)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomTimeCheck\n" + arg48);
		}
		CoopPacket coopPacket25 = new CoopPacket();
		Coop_Model_RoomTimeUpdate coop_Model_RoomTimeUpdate = (Coop_Model_RoomTimeUpdate)(coopPacket25.model = new Coop_Model_RoomTimeUpdate());
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, promise: false, 0);
		PacketStream stream25 = null;
		try
		{
			stream25 = coopPacketSerializer.Serialize(coopPacket25);
		}
		catch (Exception arg49)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomTimeUpdate\n" + arg49);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomTimeUpdate>(stream25);
		}
		catch (Exception arg50)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomTimeUpdate\n" + arg50);
		}
		CoopPacket coopPacket26 = new CoopPacket();
		Coop_Model_EnemyBossPop coop_Model_EnemyBossPop = (Coop_Model_EnemyBossPop)(coopPacket26.model = new Coop_Model_EnemyBossPop());
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, promise: false, 0);
		PacketStream stream26 = null;
		try
		{
			stream26 = coopPacketSerializer.Serialize(coopPacket26);
		}
		catch (Exception arg51)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyBossPop\n" + arg51);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyBossPop>(stream26);
		}
		catch (Exception arg52)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyBossPop\n" + arg52);
		}
		CoopPacket coopPacket27 = new CoopPacket();
		Coop_Model_WaveMatchInfo coop_Model_WaveMatchInfo = (Coop_Model_WaveMatchInfo)(coopPacket27.model = new Coop_Model_WaveMatchInfo());
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, promise: false, 0);
		PacketStream stream27 = null;
		try
		{
			stream27 = coopPacketSerializer.Serialize(coopPacket27);
		}
		catch (Exception arg53)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_WaveMatchInfo\n" + arg53);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_WaveMatchInfo>(stream27);
		}
		catch (Exception arg54)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_WaveMatchInfo\n" + arg54);
		}
		CoopPacket coopPacket28 = new CoopPacket();
		Coop_Model_WaveMatchDrop coop_Model_WaveMatchDrop = (Coop_Model_WaveMatchDrop)(coopPacket28.model = new Coop_Model_WaveMatchDrop());
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, promise: false, 0);
		PacketStream stream28 = null;
		try
		{
			stream28 = coopPacketSerializer.Serialize(coopPacket28);
		}
		catch (Exception arg55)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_WaveMatchDrop\n" + arg55);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_WaveMatchDrop>(stream28);
		}
		catch (Exception arg56)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_WaveMatchDrop\n" + arg56);
		}
		CoopPacket coopPacket29 = new CoopPacket();
		Coop_Model_EnemyForcePop coop_Model_EnemyForcePop = (Coop_Model_EnemyForcePop)(coopPacket29.model = new Coop_Model_EnemyForcePop());
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, promise: false, 0);
		PacketStream stream29 = null;
		try
		{
			stream29 = coopPacketSerializer.Serialize(coopPacket29);
		}
		catch (Exception arg57)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyForcePop\n" + arg57);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyForcePop>(stream29);
		}
		catch (Exception arg58)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyForcePop\n" + arg58);
		}
		CoopPacket coopPacket30 = new CoopPacket();
		Coop_Model_EventHappenQuest coop_Model_EventHappenQuest = (Coop_Model_EventHappenQuest)(coopPacket30.model = new Coop_Model_EventHappenQuest());
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, promise: false, 0);
		PacketStream stream30 = null;
		try
		{
			stream30 = coopPacketSerializer.Serialize(coopPacket30);
		}
		catch (Exception arg59)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EventHappenQuest\n" + arg59);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EventHappenQuest>(stream30);
		}
		catch (Exception arg60)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EventHappenQuest\n" + arg60);
		}
		CoopPacket coopPacket31 = new CoopPacket();
		Coop_Model_EventHappenQuestStatus coop_Model_EventHappenQuestStatus = (Coop_Model_EventHappenQuestStatus)(coopPacket31.model = new Coop_Model_EventHappenQuestStatus());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, promise: false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EventHappenQuestStatus\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EventHappenQuestStatus>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EventHappenQuestStatus\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Coop_Model_StageChatMessage coop_Model_StageChatMessage = (Coop_Model_StageChatMessage)(coopPacket32.model = new Coop_Model_StageChatMessage());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, promise: false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatMessage\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChatMessage>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatMessage\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Party_Model_Register party_Model_Register = (Party_Model_Register)(coopPacket33.model = new Party_Model_Register());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, promise: false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_Register\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_Register>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_Register\n" + arg66);
		}
		CoopPacket coopPacket34 = new CoopPacket();
		Party_Model_RegisterACK party_Model_RegisterACK = (Party_Model_RegisterACK)(coopPacket34.model = new Party_Model_RegisterACK());
		coopPacket34.header = new CoopPacketHeader(coopPacket34.model.c, 0, 0, promise: false, 0);
		PacketStream stream34 = null;
		try
		{
			stream34 = coopPacketSerializer.Serialize(coopPacket34);
		}
		catch (Exception arg67)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RegisterACK\n" + arg67);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RegisterACK>(stream34);
		}
		catch (Exception arg68)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RegisterACK\n" + arg68);
		}
		CoopPacket coopPacket35 = new CoopPacket();
		Party_Model_RoomJoined party_Model_RoomJoined = (Party_Model_RoomJoined)(coopPacket35.model = new Party_Model_RoomJoined());
		coopPacket35.header = new CoopPacketHeader(coopPacket35.model.c, 0, 0, promise: false, 0);
		PacketStream stream35 = null;
		try
		{
			stream35 = coopPacketSerializer.Serialize(coopPacket35);
		}
		catch (Exception arg69)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomJoined\n" + arg69);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RoomJoined>(stream35);
		}
		catch (Exception arg70)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomJoined\n" + arg70);
		}
		CoopPacket coopPacket36 = new CoopPacket();
		Party_Model_RoomLeaved party_Model_RoomLeaved = (Party_Model_RoomLeaved)(coopPacket36.model = new Party_Model_RoomLeaved());
		coopPacket36.header = new CoopPacketHeader(coopPacket36.model.c, 0, 0, promise: false, 0);
		PacketStream stream36 = null;
		try
		{
			stream36 = coopPacketSerializer.Serialize(coopPacket36);
		}
		catch (Exception arg71)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomLeaved\n" + arg71);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RoomLeaved>(stream36);
		}
		catch (Exception arg72)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomLeaved\n" + arg72);
		}
		CoopPacket coopPacket37 = new CoopPacket();
		Lounge_Model_Register lounge_Model_Register = (Lounge_Model_Register)(coopPacket37.model = new Lounge_Model_Register());
		coopPacket37.header = new CoopPacketHeader(coopPacket37.model.c, 0, 0, promise: false, 0);
		PacketStream stream37 = null;
		try
		{
			stream37 = coopPacketSerializer.Serialize(coopPacket37);
		}
		catch (Exception arg73)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_Register\n" + arg73);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_Register>(stream37);
		}
		catch (Exception arg74)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_Register\n" + arg74);
		}
		CoopPacket coopPacket38 = new CoopPacket();
		Lounge_Model_RegisterACK lounge_Model_RegisterACK = (Lounge_Model_RegisterACK)(coopPacket38.model = new Lounge_Model_RegisterACK());
		coopPacket38.header = new CoopPacketHeader(coopPacket38.model.c, 0, 0, promise: false, 0);
		PacketStream stream38 = null;
		try
		{
			stream38 = coopPacketSerializer.Serialize(coopPacket38);
		}
		catch (Exception arg75)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RegisterACK\n" + arg75);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RegisterACK>(stream38);
		}
		catch (Exception arg76)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RegisterACK\n" + arg76);
		}
		CoopPacket coopPacket39 = new CoopPacket();
		Lounge_Model_RoomEntryClose lounge_Model_RoomEntryClose = (Lounge_Model_RoomEntryClose)(coopPacket39.model = new Lounge_Model_RoomEntryClose());
		coopPacket39.header = new CoopPacketHeader(coopPacket39.model.c, 0, 0, promise: false, 0);
		PacketStream stream39 = null;
		try
		{
			stream39 = coopPacketSerializer.Serialize(coopPacket39);
		}
		catch (Exception arg77)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomEntryClose\n" + arg77);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomEntryClose>(stream39);
		}
		catch (Exception arg78)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomEntryClose\n" + arg78);
		}
		CoopPacket coopPacket40 = new CoopPacket();
		Lounge_Model_RoomJoined lounge_Model_RoomJoined = (Lounge_Model_RoomJoined)(coopPacket40.model = new Lounge_Model_RoomJoined());
		coopPacket40.header = new CoopPacketHeader(coopPacket40.model.c, 0, 0, promise: false, 0);
		PacketStream stream40 = null;
		try
		{
			stream40 = coopPacketSerializer.Serialize(coopPacket40);
		}
		catch (Exception arg79)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomJoined\n" + arg79);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomJoined>(stream40);
		}
		catch (Exception arg80)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomJoined\n" + arg80);
		}
		CoopPacket coopPacket41 = new CoopPacket();
		Lounge_Model_RoomLeaved lounge_Model_RoomLeaved = (Lounge_Model_RoomLeaved)(coopPacket41.model = new Lounge_Model_RoomLeaved());
		coopPacket41.header = new CoopPacketHeader(coopPacket41.model.c, 0, 0, promise: false, 0);
		PacketStream stream41 = null;
		try
		{
			stream41 = coopPacketSerializer.Serialize(coopPacket41);
		}
		catch (Exception arg81)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomLeaved\n" + arg81);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomLeaved>(stream41);
		}
		catch (Exception arg82)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomLeaved\n" + arg82);
		}
		CoopPacket coopPacket42 = new CoopPacket();
		Lounge_Model_RoomHostChanged lounge_Model_RoomHostChanged = (Lounge_Model_RoomHostChanged)(coopPacket42.model = new Lounge_Model_RoomHostChanged());
		coopPacket42.header = new CoopPacketHeader(coopPacket42.model.c, 0, 0, promise: false, 0);
		PacketStream stream42 = null;
		try
		{
			stream42 = coopPacketSerializer.Serialize(coopPacket42);
		}
		catch (Exception arg83)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomHostChanged\n" + arg83);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomHostChanged>(stream42);
		}
		catch (Exception arg84)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomHostChanged\n" + arg84);
		}
		CoopPacket coopPacket43 = new CoopPacket();
		Lounge_Model_RoomKick lounge_Model_RoomKick = (Lounge_Model_RoomKick)(coopPacket43.model = new Lounge_Model_RoomKick());
		coopPacket43.header = new CoopPacketHeader(coopPacket43.model.c, 0, 0, promise: false, 0);
		PacketStream stream43 = null;
		try
		{
			stream43 = coopPacketSerializer.Serialize(coopPacket43);
		}
		catch (Exception arg85)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomKick\n" + arg85);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomKick>(stream43);
		}
		catch (Exception arg86)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomKick\n" + arg86);
		}
		CoopPacket coopPacket44 = new CoopPacket();
		Lounge_Model_RoomMove lounge_Model_RoomMove = (Lounge_Model_RoomMove)(coopPacket44.model = new Lounge_Model_RoomMove());
		coopPacket44.header = new CoopPacketHeader(coopPacket44.model.c, 0, 0, promise: false, 0);
		PacketStream stream44 = null;
		try
		{
			stream44 = coopPacketSerializer.Serialize(coopPacket44);
		}
		catch (Exception arg87)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomMove\n" + arg87);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomMove>(stream44);
		}
		catch (Exception arg88)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomMove\n" + arg88);
		}
		CoopPacket coopPacket45 = new CoopPacket();
		Lounge_Model_RoomPosition lounge_Model_RoomPosition = (Lounge_Model_RoomPosition)(coopPacket45.model = new Lounge_Model_RoomPosition());
		coopPacket45.header = new CoopPacketHeader(coopPacket45.model.c, 0, 0, promise: false, 0);
		PacketStream stream45 = null;
		try
		{
			stream45 = coopPacketSerializer.Serialize(coopPacket45);
		}
		catch (Exception arg89)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomPosition\n" + arg89);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomPosition>(stream45);
		}
		catch (Exception arg90)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomPosition\n" + arg90);
		}
		CoopPacket coopPacket46 = new CoopPacket();
		Lounge_Model_RoomAction lounge_Model_RoomAction = (Lounge_Model_RoomAction)(coopPacket46.model = new Lounge_Model_RoomAction());
		coopPacket46.header = new CoopPacketHeader(coopPacket46.model.c, 0, 0, promise: false, 0);
		PacketStream stream46 = null;
		try
		{
			stream46 = coopPacketSerializer.Serialize(coopPacket46);
		}
		catch (Exception arg91)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomAction\n" + arg91);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomAction>(stream46);
		}
		catch (Exception arg92)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomAction\n" + arg92);
		}
		CoopPacket coopPacket47 = new CoopPacket();
		Lounge_Model_AFK_Kick lounge_Model_AFK_Kick = (Lounge_Model_AFK_Kick)(coopPacket47.model = new Lounge_Model_AFK_Kick());
		coopPacket47.header = new CoopPacketHeader(coopPacket47.model.c, 0, 0, promise: false, 0);
		PacketStream stream47 = null;
		try
		{
			stream47 = coopPacketSerializer.Serialize(coopPacket47);
		}
		catch (Exception arg93)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_AFK_Kick\n" + arg93);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_AFK_Kick>(stream47);
		}
		catch (Exception arg94)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_AFK_Kick\n" + arg94);
		}
		CoopPacket coopPacket48 = new CoopPacket();
		Lounge_Model_MemberLounge lounge_Model_MemberLounge = (Lounge_Model_MemberLounge)(coopPacket48.model = new Lounge_Model_MemberLounge());
		coopPacket48.header = new CoopPacketHeader(coopPacket48.model.c, 0, 0, promise: false, 0);
		PacketStream stream48 = null;
		try
		{
			stream48 = coopPacketSerializer.Serialize(coopPacket48);
		}
		catch (Exception arg95)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberLounge\n" + arg95);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberLounge>(stream48);
		}
		catch (Exception arg96)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberLounge\n" + arg96);
		}
		CoopPacket coopPacket49 = new CoopPacket();
		Lounge_Model_MemberField lounge_Model_MemberField = (Lounge_Model_MemberField)(coopPacket49.model = new Lounge_Model_MemberField());
		coopPacket49.header = new CoopPacketHeader(coopPacket49.model.c, 0, 0, promise: false, 0);
		PacketStream stream49 = null;
		try
		{
			stream49 = coopPacketSerializer.Serialize(coopPacket49);
		}
		catch (Exception arg97)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberField\n" + arg97);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberField>(stream49);
		}
		catch (Exception arg98)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberField\n" + arg98);
		}
		CoopPacket coopPacket50 = new CoopPacket();
		Lounge_Model_MemberQuest lounge_Model_MemberQuest = (Lounge_Model_MemberQuest)(coopPacket50.model = new Lounge_Model_MemberQuest());
		coopPacket50.header = new CoopPacketHeader(coopPacket50.model.c, 0, 0, promise: false, 0);
		PacketStream stream50 = null;
		try
		{
			stream50 = coopPacketSerializer.Serialize(coopPacket50);
		}
		catch (Exception arg99)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberQuest\n" + arg99);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberQuest>(stream50);
		}
		catch (Exception arg100)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberQuest\n" + arg100);
		}
	}

	public static void Test1()
	{
		CoopPacketSerializer coopPacketSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
		CoopPacket coopPacket = new CoopPacket();
		Lounge_Model_MemberQuest lounge_Model_MemberQuest = (Lounge_Model_MemberQuest)(coopPacket.model = new Lounge_Model_MemberQuest());
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, promise: false, 0);
		PacketStream stream = null;
		try
		{
			stream = coopPacketSerializer.Serialize(coopPacket);
		}
		catch (Exception arg)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberQuest\n" + arg);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberQuest>(stream);
		}
		catch (Exception arg2)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberQuest\n" + arg2);
		}
		CoopPacket coopPacket2 = new CoopPacket();
		Lounge_Model_MemberArena lounge_Model_MemberArena = (Lounge_Model_MemberArena)(coopPacket2.model = new Lounge_Model_MemberArena());
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, promise: false, 0);
		PacketStream stream2 = null;
		try
		{
			stream2 = coopPacketSerializer.Serialize(coopPacket2);
		}
		catch (Exception arg3)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberArena\n" + arg3);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberArena>(stream2);
		}
		catch (Exception arg4)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberArena\n" + arg4);
		}
		CoopPacket coopPacket3 = new CoopPacket();
		Coop_Model_ClientStatus coop_Model_ClientStatus = (Coop_Model_ClientStatus)(coopPacket3.model = new Coop_Model_ClientStatus());
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, promise: false, 0);
		PacketStream stream3 = null;
		try
		{
			stream3 = coopPacketSerializer.Serialize(coopPacket3);
		}
		catch (Exception arg5)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientStatus\n" + arg5);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientStatus>(stream3);
		}
		catch (Exception arg6)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientStatus\n" + arg6);
		}
		CoopPacket coopPacket4 = new CoopPacket();
		Coop_Model_ClientBecameHost coop_Model_ClientBecameHost = (Coop_Model_ClientBecameHost)(coopPacket4.model = new Coop_Model_ClientBecameHost());
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, promise: false, 0);
		PacketStream stream4 = null;
		try
		{
			stream4 = coopPacketSerializer.Serialize(coopPacket4);
		}
		catch (Exception arg7)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBecameHost\n" + arg7);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientBecameHost>(stream4);
		}
		catch (Exception arg8)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBecameHost\n" + arg8);
		}
		CoopPacket coopPacket5 = new CoopPacket();
		Coop_Model_ClientLoadingProgress coop_Model_ClientLoadingProgress = (Coop_Model_ClientLoadingProgress)(coopPacket5.model = new Coop_Model_ClientLoadingProgress());
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, promise: false, 0);
		PacketStream stream5 = null;
		try
		{
			stream5 = coopPacketSerializer.Serialize(coopPacket5);
		}
		catch (Exception arg9)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientLoadingProgress\n" + arg9);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientLoadingProgress>(stream5);
		}
		catch (Exception arg10)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientLoadingProgress\n" + arg10);
		}
		CoopPacket coopPacket6 = new CoopPacket();
		Coop_Model_ClientChangeEquip coop_Model_ClientChangeEquip = (Coop_Model_ClientChangeEquip)(coopPacket6.model = new Coop_Model_ClientChangeEquip());
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, promise: false, 0);
		PacketStream stream6 = null;
		try
		{
			stream6 = coopPacketSerializer.Serialize(coopPacket6);
		}
		catch (Exception arg11)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientChangeEquip\n" + arg11);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientChangeEquip>(stream6);
		}
		catch (Exception arg12)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientChangeEquip\n" + arg12);
		}
		CoopPacket coopPacket7 = new CoopPacket();
		Coop_Model_ClientBattleRetire coop_Model_ClientBattleRetire = (Coop_Model_ClientBattleRetire)(coopPacket7.model = new Coop_Model_ClientBattleRetire());
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, promise: false, 0);
		PacketStream stream7 = null;
		try
		{
			stream7 = coopPacketSerializer.Serialize(coopPacket7);
		}
		catch (Exception arg13)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBattleRetire\n" + arg13);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientBattleRetire>(stream7);
		}
		catch (Exception arg14)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBattleRetire\n" + arg14);
		}
		CoopPacket coopPacket8 = new CoopPacket();
		Coop_Model_ClientSeriesProgress coop_Model_ClientSeriesProgress = (Coop_Model_ClientSeriesProgress)(coopPacket8.model = new Coop_Model_ClientSeriesProgress());
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, promise: false, 0);
		PacketStream stream8 = null;
		try
		{
			stream8 = coopPacketSerializer.Serialize(coopPacket8);
		}
		catch (Exception arg15)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientSeriesProgress\n" + arg15);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientSeriesProgress>(stream8);
		}
		catch (Exception arg16)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientSeriesProgress\n" + arg16);
		}
		CoopPacket coopPacket9 = new CoopPacket();
		Coop_Model_RoomUpdatePortalPoint coop_Model_RoomUpdatePortalPoint = (Coop_Model_RoomUpdatePortalPoint)(coopPacket9.model = new Coop_Model_RoomUpdatePortalPoint());
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, promise: false, 0);
		PacketStream stream9 = null;
		try
		{
			stream9 = coopPacketSerializer.Serialize(coopPacket9);
		}
		catch (Exception arg17)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomUpdatePortalPoint\n" + arg17);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomUpdatePortalPoint>(stream9);
		}
		catch (Exception arg18)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomUpdatePortalPoint\n" + arg18);
		}
		CoopPacket coopPacket10 = new CoopPacket();
		Coop_Model_RoomSyncExploreBoss coop_Model_RoomSyncExploreBoss = (Coop_Model_RoomSyncExploreBoss)(coopPacket10.model = new Coop_Model_RoomSyncExploreBoss());
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, promise: false, 0);
		PacketStream stream10 = null;
		try
		{
			stream10 = coopPacketSerializer.Serialize(coopPacket10);
		}
		catch (Exception arg19)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBoss\n" + arg19);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncExploreBoss>(stream10);
		}
		catch (Exception arg20)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBoss\n" + arg20);
		}
		CoopPacket coopPacket11 = new CoopPacket();
		Coop_Model_RoomSyncExploreBossMap coop_Model_RoomSyncExploreBossMap = (Coop_Model_RoomSyncExploreBossMap)(coopPacket11.model = new Coop_Model_RoomSyncExploreBossMap());
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, promise: false, 0);
		PacketStream stream11 = null;
		try
		{
			stream11 = coopPacketSerializer.Serialize(coopPacket11);
		}
		catch (Exception arg21)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBossMap\n" + arg21);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncExploreBossMap>(stream11);
		}
		catch (Exception arg22)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBossMap\n" + arg22);
		}
		CoopPacket coopPacket12 = new CoopPacket();
		Coop_Model_RoomExploreBossDead coop_Model_RoomExploreBossDead = (Coop_Model_RoomExploreBossDead)(coopPacket12.model = new Coop_Model_RoomExploreBossDead());
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, promise: false, 0);
		PacketStream stream12 = null;
		try
		{
			stream12 = coopPacketSerializer.Serialize(coopPacket12);
		}
		catch (Exception arg23)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDead\n" + arg23);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreBossDead>(stream12);
		}
		catch (Exception arg24)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDead\n" + arg24);
		}
		CoopPacket coopPacket13 = new CoopPacket();
		Coop_Model_RoomNotifyEncounterBoss coop_Model_RoomNotifyEncounterBoss = (Coop_Model_RoomNotifyEncounterBoss)(coopPacket13.model = new Coop_Model_RoomNotifyEncounterBoss());
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, promise: false, 0);
		PacketStream stream13 = null;
		try
		{
			stream13 = coopPacketSerializer.Serialize(coopPacket13);
		}
		catch (Exception arg25)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + arg25);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomNotifyEncounterBoss>(stream13);
		}
		catch (Exception arg26)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + arg26);
		}
		CoopPacket coopPacket14 = new CoopPacket();
		Coop_Model_RoomSyncPlayerStatus coop_Model_RoomSyncPlayerStatus = (Coop_Model_RoomSyncPlayerStatus)(coopPacket14.model = new Coop_Model_RoomSyncPlayerStatus());
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, promise: false, 0);
		PacketStream stream14 = null;
		try
		{
			stream14 = coopPacketSerializer.Serialize(coopPacket14);
		}
		catch (Exception arg27)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncPlayerStatus\n" + arg27);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncPlayerStatus>(stream14);
		}
		catch (Exception arg28)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncPlayerStatus\n" + arg28);
		}
		CoopPacket coopPacket15 = new CoopPacket();
		Coop_Model_RoomChatStamp coop_Model_RoomChatStamp = (Coop_Model_RoomChatStamp)(coopPacket15.model = new Coop_Model_RoomChatStamp());
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, promise: false, 0);
		PacketStream stream15 = null;
		try
		{
			stream15 = coopPacketSerializer.Serialize(coopPacket15);
		}
		catch (Exception arg29)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomChatStamp\n" + arg29);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomChatStamp>(stream15);
		}
		catch (Exception arg30)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomChatStamp\n" + arg30);
		}
		CoopPacket coopPacket16 = new CoopPacket();
		Coop_Model_RoomExploreBossDamage coop_Model_RoomExploreBossDamage = (Coop_Model_RoomExploreBossDamage)(coopPacket16.model = new Coop_Model_RoomExploreBossDamage());
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, promise: false, 0);
		PacketStream stream16 = null;
		try
		{
			stream16 = coopPacketSerializer.Serialize(coopPacket16);
		}
		catch (Exception arg31)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDamage\n" + arg31);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreBossDamage>(stream16);
		}
		catch (Exception arg32)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDamage\n" + arg32);
		}
		CoopPacket coopPacket17 = new CoopPacket();
		Coop_Model_RoomExploreAlive coop_Model_RoomExploreAlive = (Coop_Model_RoomExploreAlive)(coopPacket17.model = new Coop_Model_RoomExploreAlive());
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, promise: false, 0);
		PacketStream stream17 = null;
		try
		{
			stream17 = coopPacketSerializer.Serialize(coopPacket17);
		}
		catch (Exception arg33)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAlive\n" + arg33);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreAlive>(stream17);
		}
		catch (Exception arg34)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAlive\n" + arg34);
		}
		CoopPacket coopPacket18 = new CoopPacket();
		Coop_Model_RoomExploreAliveRequest coop_Model_RoomExploreAliveRequest = (Coop_Model_RoomExploreAliveRequest)(coopPacket18.model = new Coop_Model_RoomExploreAliveRequest());
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, promise: false, 0);
		PacketStream stream18 = null;
		try
		{
			stream18 = coopPacketSerializer.Serialize(coopPacket18);
		}
		catch (Exception arg35)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAliveRequest\n" + arg35);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreAliveRequest>(stream18);
		}
		catch (Exception arg36)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAliveRequest\n" + arg36);
		}
		CoopPacket coopPacket19 = new CoopPacket();
		Coop_Model_RoomSyncAllPortalPoint coop_Model_RoomSyncAllPortalPoint = (Coop_Model_RoomSyncAllPortalPoint)(coopPacket19.model = new Coop_Model_RoomSyncAllPortalPoint());
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, promise: false, 0);
		PacketStream stream19 = null;
		try
		{
			stream19 = coopPacketSerializer.Serialize(coopPacket19);
		}
		catch (Exception arg37)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + arg37);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncAllPortalPoint>(stream19);
		}
		catch (Exception arg38)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + arg38);
		}
		CoopPacket coopPacket20 = new CoopPacket();
		Coop_Model_RoomMoveField coop_Model_RoomMoveField = (Coop_Model_RoomMoveField)(coopPacket20.model = new Coop_Model_RoomMoveField());
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, promise: false, 0);
		PacketStream stream20 = null;
		try
		{
			stream20 = coopPacketSerializer.Serialize(coopPacket20);
		}
		catch (Exception arg39)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomMoveField\n" + arg39);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomMoveField>(stream20);
		}
		catch (Exception arg40)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomMoveField\n" + arg40);
		}
		CoopPacket coopPacket21 = new CoopPacket();
		Coop_Model_RushRequest coop_Model_RushRequest = (Coop_Model_RushRequest)(coopPacket21.model = new Coop_Model_RushRequest());
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, promise: false, 0);
		PacketStream stream21 = null;
		try
		{
			stream21 = coopPacketSerializer.Serialize(coopPacket21);
		}
		catch (Exception arg41)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequest\n" + arg41);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RushRequest>(stream21);
		}
		catch (Exception arg42)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequest\n" + arg42);
		}
		CoopPacket coopPacket22 = new CoopPacket();
		Coop_Model_RushRequested coop_Model_RushRequested = (Coop_Model_RushRequested)(coopPacket22.model = new Coop_Model_RushRequested());
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, promise: false, 0);
		PacketStream stream22 = null;
		try
		{
			stream22 = coopPacketSerializer.Serialize(coopPacket22);
		}
		catch (Exception arg43)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequested\n" + arg43);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RushRequested>(stream22);
		}
		catch (Exception arg44)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequested\n" + arg44);
		}
		CoopPacket coopPacket23 = new CoopPacket();
		Coop_Model_RoomNotifyTraceBoss coop_Model_RoomNotifyTraceBoss = (Coop_Model_RoomNotifyTraceBoss)(coopPacket23.model = new Coop_Model_RoomNotifyTraceBoss());
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, promise: false, 0);
		PacketStream stream23 = null;
		try
		{
			stream23 = coopPacketSerializer.Serialize(coopPacket23);
		}
		catch (Exception arg45)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyTraceBoss\n" + arg45);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomNotifyTraceBoss>(stream23);
		}
		catch (Exception arg46)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyTraceBoss\n" + arg46);
		}
		CoopPacket coopPacket24 = new CoopPacket();
		Coop_Model_StageRequest coop_Model_StageRequest = (Coop_Model_StageRequest)(coopPacket24.model = new Coop_Model_StageRequest());
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, promise: false, 0);
		PacketStream stream24 = null;
		try
		{
			stream24 = coopPacketSerializer.Serialize(coopPacket24);
		}
		catch (Exception arg47)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageRequest\n" + arg47);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageRequest>(stream24);
		}
		catch (Exception arg48)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageRequest\n" + arg48);
		}
		CoopPacket coopPacket25 = new CoopPacket();
		Coop_Model_StagePlayerPop coop_Model_StagePlayerPop = (Coop_Model_StagePlayerPop)(coopPacket25.model = new Coop_Model_StagePlayerPop());
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, promise: false, 0);
		PacketStream stream25 = null;
		try
		{
			stream25 = coopPacketSerializer.Serialize(coopPacket25);
		}
		catch (Exception arg49)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StagePlayerPop\n" + arg49);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StagePlayerPop>(stream25);
		}
		catch (Exception arg50)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StagePlayerPop\n" + arg50);
		}
		CoopPacket coopPacket26 = new CoopPacket();
		Coop_Model_StageInfo coop_Model_StageInfo = (Coop_Model_StageInfo)(coopPacket26.model = new Coop_Model_StageInfo());
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, promise: false, 0);
		PacketStream stream26 = null;
		try
		{
			stream26 = coopPacketSerializer.Serialize(coopPacket26);
		}
		catch (Exception arg51)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageInfo\n" + arg51);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageInfo>(stream26);
		}
		catch (Exception arg52)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageInfo\n" + arg52);
		}
		CoopPacket coopPacket27 = new CoopPacket();
		Coop_Model_StageResponseEnd coop_Model_StageResponseEnd = (Coop_Model_StageResponseEnd)(coopPacket27.model = new Coop_Model_StageResponseEnd());
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, promise: false, 0);
		PacketStream stream27 = null;
		try
		{
			stream27 = coopPacketSerializer.Serialize(coopPacket27);
		}
		catch (Exception arg53)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageResponseEnd\n" + arg53);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageResponseEnd>(stream27);
		}
		catch (Exception arg54)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageResponseEnd\n" + arg54);
		}
		CoopPacket coopPacket28 = new CoopPacket();
		Coop_Model_StageQuestClose coop_Model_StageQuestClose = (Coop_Model_StageQuestClose)(coopPacket28.model = new Coop_Model_StageQuestClose());
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, promise: false, 0);
		PacketStream stream28 = null;
		try
		{
			stream28 = coopPacketSerializer.Serialize(coopPacket28);
		}
		catch (Exception arg55)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageQuestClose\n" + arg55);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageQuestClose>(stream28);
		}
		catch (Exception arg56)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageQuestClose\n" + arg56);
		}
		CoopPacket coopPacket29 = new CoopPacket();
		Coop_Model_StageTimeup coop_Model_StageTimeup = (Coop_Model_StageTimeup)(coopPacket29.model = new Coop_Model_StageTimeup());
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, promise: false, 0);
		PacketStream stream29 = null;
		try
		{
			stream29 = coopPacketSerializer.Serialize(coopPacket29);
		}
		catch (Exception arg57)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageTimeup\n" + arg57);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageTimeup>(stream29);
		}
		catch (Exception arg58)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageTimeup\n" + arg58);
		}
		CoopPacket coopPacket30 = new CoopPacket();
		Coop_Model_StageChat coop_Model_StageChat = (Coop_Model_StageChat)(coopPacket30.model = new Coop_Model_StageChat());
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, promise: false, 0);
		PacketStream stream30 = null;
		try
		{
			stream30 = coopPacketSerializer.Serialize(coopPacket30);
		}
		catch (Exception arg59)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChat\n" + arg59);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChat>(stream30);
		}
		catch (Exception arg60)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChat\n" + arg60);
		}
		CoopPacket coopPacket31 = new CoopPacket();
		Coop_Model_StageChatStamp coop_Model_StageChatStamp = (Coop_Model_StageChatStamp)(coopPacket31.model = new Coop_Model_StageChatStamp());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, promise: false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatStamp\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChatStamp>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatStamp\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Coop_Model_StageSyncTimeRequest coop_Model_StageSyncTimeRequest = (Coop_Model_StageSyncTimeRequest)(coopPacket32.model = new Coop_Model_StageSyncTimeRequest());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, promise: false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTimeRequest\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageSyncTimeRequest>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTimeRequest\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Coop_Model_StageSyncTime coop_Model_StageSyncTime = (Coop_Model_StageSyncTime)(coopPacket33.model = new Coop_Model_StageSyncTime());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, promise: false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTime\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageSyncTime>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTime\n" + arg66);
		}
		CoopPacket coopPacket34 = new CoopPacket();
		Coop_Model_ObjectDestroy coop_Model_ObjectDestroy = (Coop_Model_ObjectDestroy)(coopPacket34.model = new Coop_Model_ObjectDestroy());
		coopPacket34.header = new CoopPacketHeader(coopPacket34.model.c, 0, 0, promise: false, 0);
		PacketStream stream34 = null;
		try
		{
			stream34 = coopPacketSerializer.Serialize(coopPacket34);
		}
		catch (Exception arg67)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectDestroy\n" + arg67);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectDestroy>(stream34);
		}
		catch (Exception arg68)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectDestroy\n" + arg68);
		}
		CoopPacket coopPacket35 = new CoopPacket();
		Coop_Model_ObjectAttackedHitOwner coop_Model_ObjectAttackedHitOwner = (Coop_Model_ObjectAttackedHitOwner)(coopPacket35.model = new Coop_Model_ObjectAttackedHitOwner());
		coopPacket35.header = new CoopPacketHeader(coopPacket35.model.c, 0, 0, promise: false, 0);
		PacketStream stream35 = null;
		try
		{
			stream35 = coopPacketSerializer.Serialize(coopPacket35);
		}
		catch (Exception arg69)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitOwner\n" + arg69);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectAttackedHitOwner>(stream35);
		}
		catch (Exception arg70)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitOwner\n" + arg70);
		}
		CoopPacket coopPacket36 = new CoopPacket();
		Coop_Model_ObjectAttackedHitFix coop_Model_ObjectAttackedHitFix = (Coop_Model_ObjectAttackedHitFix)(coopPacket36.model = new Coop_Model_ObjectAttackedHitFix());
		coopPacket36.header = new CoopPacketHeader(coopPacket36.model.c, 0, 0, promise: false, 0);
		PacketStream stream36 = null;
		try
		{
			stream36 = coopPacketSerializer.Serialize(coopPacket36);
		}
		catch (Exception arg71)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitFix\n" + arg71);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectAttackedHitFix>(stream36);
		}
		catch (Exception arg72)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitFix\n" + arg72);
		}
		CoopPacket coopPacket37 = new CoopPacket();
		Coop_Model_ObjectKeepWaitingPacket coop_Model_ObjectKeepWaitingPacket = (Coop_Model_ObjectKeepWaitingPacket)(coopPacket37.model = new Coop_Model_ObjectKeepWaitingPacket());
		coopPacket37.header = new CoopPacketHeader(coopPacket37.model.c, 0, 0, promise: false, 0);
		PacketStream stream37 = null;
		try
		{
			stream37 = coopPacketSerializer.Serialize(coopPacket37);
		}
		catch (Exception arg73)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + arg73);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectKeepWaitingPacket>(stream37);
		}
		catch (Exception arg74)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + arg74);
		}
		CoopPacket coopPacket38 = new CoopPacket();
		Coop_Model_CharacterActionTarget coop_Model_CharacterActionTarget = (Coop_Model_CharacterActionTarget)(coopPacket38.model = new Coop_Model_CharacterActionTarget());
		coopPacket38.header = new CoopPacketHeader(coopPacket38.model.c, 0, 0, promise: false, 0);
		PacketStream stream38 = null;
		try
		{
			stream38 = coopPacketSerializer.Serialize(coopPacket38);
		}
		catch (Exception arg75)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterActionTarget\n" + arg75);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterActionTarget>(stream38);
		}
		catch (Exception arg76)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterActionTarget\n" + arg76);
		}
		CoopPacket coopPacket39 = new CoopPacket();
		Coop_Model_CharacterUpdateActionPosition coop_Model_CharacterUpdateActionPosition = (Coop_Model_CharacterUpdateActionPosition)(coopPacket39.model = new Coop_Model_CharacterUpdateActionPosition());
		coopPacket39.header = new CoopPacketHeader(coopPacket39.model.c, 0, 0, promise: false, 0);
		PacketStream stream39 = null;
		try
		{
			stream39 = coopPacketSerializer.Serialize(coopPacket39);
		}
		catch (Exception arg77)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateActionPosition\n" + arg77);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterUpdateActionPosition>(stream39);
		}
		catch (Exception arg78)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateActionPosition\n" + arg78);
		}
		CoopPacket coopPacket40 = new CoopPacket();
		Coop_Model_CharacterUpdateDirection coop_Model_CharacterUpdateDirection = (Coop_Model_CharacterUpdateDirection)(coopPacket40.model = new Coop_Model_CharacterUpdateDirection());
		coopPacket40.header = new CoopPacketHeader(coopPacket40.model.c, 0, 0, promise: false, 0);
		PacketStream stream40 = null;
		try
		{
			stream40 = coopPacketSerializer.Serialize(coopPacket40);
		}
		catch (Exception arg79)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateDirection\n" + arg79);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterUpdateDirection>(stream40);
		}
		catch (Exception arg80)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateDirection\n" + arg80);
		}
		CoopPacket coopPacket41 = new CoopPacket();
		Coop_Model_CharacterPeriodicSyncActionPosition coop_Model_CharacterPeriodicSyncActionPosition = (Coop_Model_CharacterPeriodicSyncActionPosition)(coopPacket41.model = new Coop_Model_CharacterPeriodicSyncActionPosition());
		coopPacket41.header = new CoopPacketHeader(coopPacket41.model.c, 0, 0, promise: false, 0);
		PacketStream stream41 = null;
		try
		{
			stream41 = coopPacketSerializer.Serialize(coopPacket41);
		}
		catch (Exception arg81)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + arg81);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterPeriodicSyncActionPosition>(stream41);
		}
		catch (Exception arg82)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + arg82);
		}
		CoopPacket coopPacket42 = new CoopPacket();
		Coop_Model_CharacterIdle coop_Model_CharacterIdle = (Coop_Model_CharacterIdle)(coopPacket42.model = new Coop_Model_CharacterIdle());
		coopPacket42.header = new CoopPacketHeader(coopPacket42.model.c, 0, 0, promise: false, 0);
		PacketStream stream42 = null;
		try
		{
			stream42 = coopPacketSerializer.Serialize(coopPacket42);
		}
		catch (Exception arg83)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterIdle\n" + arg83);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterIdle>(stream42);
		}
		catch (Exception arg84)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterIdle\n" + arg84);
		}
		CoopPacket coopPacket43 = new CoopPacket();
		Coop_Model_CharacterMoveVelocity coop_Model_CharacterMoveVelocity = (Coop_Model_CharacterMoveVelocity)(coopPacket43.model = new Coop_Model_CharacterMoveVelocity());
		coopPacket43.header = new CoopPacketHeader(coopPacket43.model.c, 0, 0, promise: false, 0);
		PacketStream stream43 = null;
		try
		{
			stream43 = coopPacketSerializer.Serialize(coopPacket43);
		}
		catch (Exception arg85)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocity\n" + arg85);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveVelocity>(stream43);
		}
		catch (Exception arg86)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocity\n" + arg86);
		}
		CoopPacket coopPacket44 = new CoopPacket();
		Coop_Model_CharacterMoveVelocityEnd coop_Model_CharacterMoveVelocityEnd = (Coop_Model_CharacterMoveVelocityEnd)(coopPacket44.model = new Coop_Model_CharacterMoveVelocityEnd());
		coopPacket44.header = new CoopPacketHeader(coopPacket44.model.c, 0, 0, promise: false, 0);
		PacketStream stream44 = null;
		try
		{
			stream44 = coopPacketSerializer.Serialize(coopPacket44);
		}
		catch (Exception arg87)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + arg87);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveVelocityEnd>(stream44);
		}
		catch (Exception arg88)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + arg88);
		}
		CoopPacket coopPacket45 = new CoopPacket();
		Coop_Model_CharacterMoveToPosition coop_Model_CharacterMoveToPosition = (Coop_Model_CharacterMoveToPosition)(coopPacket45.model = new Coop_Model_CharacterMoveToPosition());
		coopPacket45.header = new CoopPacketHeader(coopPacket45.model.c, 0, 0, promise: false, 0);
		PacketStream stream45 = null;
		try
		{
			stream45 = coopPacketSerializer.Serialize(coopPacket45);
		}
		catch (Exception arg89)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveToPosition\n" + arg89);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveToPosition>(stream45);
		}
		catch (Exception arg90)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveToPosition\n" + arg90);
		}
		CoopPacket coopPacket46 = new CoopPacket();
		Coop_Model_CharacterMoveHoming coop_Model_CharacterMoveHoming = (Coop_Model_CharacterMoveHoming)(coopPacket46.model = new Coop_Model_CharacterMoveHoming());
		coopPacket46.header = new CoopPacketHeader(coopPacket46.model.c, 0, 0, promise: false, 0);
		PacketStream stream46 = null;
		try
		{
			stream46 = coopPacketSerializer.Serialize(coopPacket46);
		}
		catch (Exception arg91)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveHoming\n" + arg91);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveHoming>(stream46);
		}
		catch (Exception arg92)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveHoming\n" + arg92);
		}
		CoopPacket coopPacket47 = new CoopPacket();
		Coop_Model_CharacterRotate coop_Model_CharacterRotate = (Coop_Model_CharacterRotate)(coopPacket47.model = new Coop_Model_CharacterRotate());
		coopPacket47.header = new CoopPacketHeader(coopPacket47.model.c, 0, 0, promise: false, 0);
		PacketStream stream47 = null;
		try
		{
			stream47 = coopPacketSerializer.Serialize(coopPacket47);
		}
		catch (Exception arg93)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotate\n" + arg93);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterRotate>(stream47);
		}
		catch (Exception arg94)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotate\n" + arg94);
		}
		CoopPacket coopPacket48 = new CoopPacket();
		Coop_Model_CharacterRotateMotion coop_Model_CharacterRotateMotion = (Coop_Model_CharacterRotateMotion)(coopPacket48.model = new Coop_Model_CharacterRotateMotion());
		coopPacket48.header = new CoopPacketHeader(coopPacket48.model.c, 0, 0, promise: false, 0);
		PacketStream stream48 = null;
		try
		{
			stream48 = coopPacketSerializer.Serialize(coopPacket48);
		}
		catch (Exception arg95)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotateMotion\n" + arg95);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterRotateMotion>(stream48);
		}
		catch (Exception arg96)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotateMotion\n" + arg96);
		}
		CoopPacket coopPacket49 = new CoopPacket();
		Coop_Model_CharacterAttack coop_Model_CharacterAttack = (Coop_Model_CharacterAttack)(coopPacket49.model = new Coop_Model_CharacterAttack());
		coopPacket49.header = new CoopPacketHeader(coopPacket49.model.c, 0, 0, promise: false, 0);
		PacketStream stream49 = null;
		try
		{
			stream49 = coopPacketSerializer.Serialize(coopPacket49);
		}
		catch (Exception arg97)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterAttack\n" + arg97);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterAttack>(stream49);
		}
		catch (Exception arg98)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterAttack\n" + arg98);
		}
		CoopPacket coopPacket50 = new CoopPacket();
		Coop_Model_CharacterBuffSync coop_Model_CharacterBuffSync = (Coop_Model_CharacterBuffSync)(coopPacket50.model = new Coop_Model_CharacterBuffSync());
		coopPacket50.header = new CoopPacketHeader(coopPacket50.model.c, 0, 0, promise: false, 0);
		PacketStream stream50 = null;
		try
		{
			stream50 = coopPacketSerializer.Serialize(coopPacket50);
		}
		catch (Exception arg99)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffSync\n" + arg99);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffSync>(stream50);
		}
		catch (Exception arg100)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffSync\n" + arg100);
		}
		CoopPacket coopPacket51 = new CoopPacket();
		Coop_Model_CharacterBuffReceive coop_Model_CharacterBuffReceive = (Coop_Model_CharacterBuffReceive)(coopPacket51.model = new Coop_Model_CharacterBuffReceive());
		coopPacket51.header = new CoopPacketHeader(coopPacket51.model.c, 0, 0, promise: false, 0);
		PacketStream stream51 = null;
		try
		{
			stream51 = coopPacketSerializer.Serialize(coopPacket51);
		}
		catch (Exception arg101)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffReceive\n" + arg101);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffReceive>(stream51);
		}
		catch (Exception arg102)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffReceive\n" + arg102);
		}
	}

	public static void Test2()
	{
		CoopPacketSerializer coopPacketSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
		CoopPacket coopPacket = new CoopPacket();
		Coop_Model_CharacterBuffReceive coop_Model_CharacterBuffReceive = (Coop_Model_CharacterBuffReceive)(coopPacket.model = new Coop_Model_CharacterBuffReceive());
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, promise: false, 0);
		PacketStream stream = null;
		try
		{
			stream = coopPacketSerializer.Serialize(coopPacket);
		}
		catch (Exception arg)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffReceive\n" + arg);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffReceive>(stream);
		}
		catch (Exception arg2)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffReceive\n" + arg2);
		}
		CoopPacket coopPacket2 = new CoopPacket();
		Coop_Model_CharacterBuffRoutine coop_Model_CharacterBuffRoutine = (Coop_Model_CharacterBuffRoutine)(coopPacket2.model = new Coop_Model_CharacterBuffRoutine());
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, promise: false, 0);
		PacketStream stream2 = null;
		try
		{
			stream2 = coopPacketSerializer.Serialize(coopPacket2);
		}
		catch (Exception arg3)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffRoutine\n" + arg3);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffRoutine>(stream2);
		}
		catch (Exception arg4)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffRoutine\n" + arg4);
		}
		CoopPacket coopPacket3 = new CoopPacket();
		Coop_Model_CharacterReaction coop_Model_CharacterReaction = (Coop_Model_CharacterReaction)(coopPacket3.model = new Coop_Model_CharacterReaction());
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, promise: false, 0);
		PacketStream stream3 = null;
		try
		{
			stream3 = coopPacketSerializer.Serialize(coopPacket3);
		}
		catch (Exception arg5)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReaction\n" + arg5);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterReaction>(stream3);
		}
		catch (Exception arg6)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReaction\n" + arg6);
		}
		CoopPacket coopPacket4 = new CoopPacket();
		Coop_Model_CharacterReactionDelay coop_Model_CharacterReactionDelay = (Coop_Model_CharacterReactionDelay)(coopPacket4.model = new Coop_Model_CharacterReactionDelay());
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, promise: false, 0);
		PacketStream stream4 = null;
		try
		{
			stream4 = coopPacketSerializer.Serialize(coopPacket4);
		}
		catch (Exception arg7)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReactionDelay\n" + arg7);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterReactionDelay>(stream4);
		}
		catch (Exception arg8)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReactionDelay\n" + arg8);
		}
		CoopPacket coopPacket5 = new CoopPacket();
		Coop_Model_PlayerLoadComplete coop_Model_PlayerLoadComplete = (Coop_Model_PlayerLoadComplete)(coopPacket5.model = new Coop_Model_PlayerLoadComplete());
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, promise: false, 0);
		PacketStream stream5 = null;
		try
		{
			stream5 = coopPacketSerializer.Serialize(coopPacket5);
		}
		catch (Exception arg9)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerLoadComplete\n" + arg9);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerLoadComplete>(stream5);
		}
		catch (Exception arg10)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerLoadComplete\n" + arg10);
		}
		CoopPacket coopPacket6 = new CoopPacket();
		Coop_Model_PlayerInitialize coop_Model_PlayerInitialize = (Coop_Model_PlayerInitialize)(coopPacket6.model = new Coop_Model_PlayerInitialize());
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, promise: false, 0);
		PacketStream stream6 = null;
		try
		{
			stream6 = coopPacketSerializer.Serialize(coopPacket6);
		}
		catch (Exception arg11)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerInitialize\n" + arg11);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerInitialize>(stream6);
		}
		catch (Exception arg12)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerInitialize\n" + arg12);
		}
		CoopPacket coopPacket7 = new CoopPacket();
		Coop_Model_PlayerAttackCombo coop_Model_PlayerAttackCombo = (Coop_Model_PlayerAttackCombo)(coopPacket7.model = new Coop_Model_PlayerAttackCombo());
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, promise: false, 0);
		PacketStream stream7 = null;
		try
		{
			stream7 = coopPacketSerializer.Serialize(coopPacket7);
		}
		catch (Exception arg13)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAttackCombo\n" + arg13);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerAttackCombo>(stream7);
		}
		catch (Exception arg14)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAttackCombo\n" + arg14);
		}
		CoopPacket coopPacket8 = new CoopPacket();
		Coop_Model_PlayerChargeRelease coop_Model_PlayerChargeRelease = (Coop_Model_PlayerChargeRelease)(coopPacket8.model = new Coop_Model_PlayerChargeRelease());
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, promise: false, 0);
		PacketStream stream8 = null;
		try
		{
			stream8 = coopPacketSerializer.Serialize(coopPacket8);
		}
		catch (Exception arg15)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChargeRelease\n" + arg15);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerChargeRelease>(stream8);
		}
		catch (Exception arg16)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChargeRelease\n" + arg16);
		}
		CoopPacket coopPacket9 = new CoopPacket();
		Coop_Model_PlayerAvoid coop_Model_PlayerAvoid = (Coop_Model_PlayerAvoid)(coopPacket9.model = new Coop_Model_PlayerAvoid());
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, promise: false, 0);
		PacketStream stream9 = null;
		try
		{
			stream9 = coopPacketSerializer.Serialize(coopPacket9);
		}
		catch (Exception arg17)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAvoid\n" + arg17);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerAvoid>(stream9);
		}
		catch (Exception arg18)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAvoid\n" + arg18);
		}
		CoopPacket coopPacket10 = new CoopPacket();
		Coop_Model_PlayerBlowClear coop_Model_PlayerBlowClear = (Coop_Model_PlayerBlowClear)(coopPacket10.model = new Coop_Model_PlayerBlowClear());
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, promise: false, 0);
		PacketStream stream10 = null;
		try
		{
			stream10 = coopPacketSerializer.Serialize(coopPacket10);
		}
		catch (Exception arg19)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerBlowClear\n" + arg19);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerBlowClear>(stream10);
		}
		catch (Exception arg20)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerBlowClear\n" + arg20);
		}
		CoopPacket coopPacket11 = new CoopPacket();
		Coop_Model_PlayerStunnedEnd coop_Model_PlayerStunnedEnd = (Coop_Model_PlayerStunnedEnd)(coopPacket11.model = new Coop_Model_PlayerStunnedEnd());
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, promise: false, 0);
		PacketStream stream11 = null;
		try
		{
			stream11 = coopPacketSerializer.Serialize(coopPacket11);
		}
		catch (Exception arg21)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStunnedEnd\n" + arg21);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerStunnedEnd>(stream11);
		}
		catch (Exception arg22)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStunnedEnd\n" + arg22);
		}
		CoopPacket coopPacket12 = new CoopPacket();
		Coop_Model_PlayerDeadCount coop_Model_PlayerDeadCount = (Coop_Model_PlayerDeadCount)(coopPacket12.model = new Coop_Model_PlayerDeadCount());
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, promise: false, 0);
		PacketStream stream12 = null;
		try
		{
			stream12 = coopPacketSerializer.Serialize(coopPacket12);
		}
		catch (Exception arg23)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadCount\n" + arg23);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerDeadCount>(stream12);
		}
		catch (Exception arg24)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadCount\n" + arg24);
		}
		CoopPacket coopPacket13 = new CoopPacket();
		Coop_Model_PlayerDeadStandup coop_Model_PlayerDeadStandup = (Coop_Model_PlayerDeadStandup)(coopPacket13.model = new Coop_Model_PlayerDeadStandup());
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, promise: false, 0);
		PacketStream stream13 = null;
		try
		{
			stream13 = coopPacketSerializer.Serialize(coopPacket13);
		}
		catch (Exception arg25)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadStandup\n" + arg25);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerDeadStandup>(stream13);
		}
		catch (Exception arg26)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadStandup\n" + arg26);
		}
		CoopPacket coopPacket14 = new CoopPacket();
		Coop_Model_PlayerStopCounter coop_Model_PlayerStopCounter = (Coop_Model_PlayerStopCounter)(coopPacket14.model = new Coop_Model_PlayerStopCounter());
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, promise: false, 0);
		PacketStream stream14 = null;
		try
		{
			stream14 = coopPacketSerializer.Serialize(coopPacket14);
		}
		catch (Exception arg27)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStopCounter\n" + arg27);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerStopCounter>(stream14);
		}
		catch (Exception arg28)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStopCounter\n" + arg28);
		}
		CoopPacket coopPacket15 = new CoopPacket();
		Coop_Model_PlayerGather coop_Model_PlayerGather = (Coop_Model_PlayerGather)(coopPacket15.model = new Coop_Model_PlayerGather());
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, promise: false, 0);
		PacketStream stream15 = null;
		try
		{
			stream15 = coopPacketSerializer.Serialize(coopPacket15);
		}
		catch (Exception arg29)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGather\n" + arg29);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGather>(stream15);
		}
		catch (Exception arg30)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGather\n" + arg30);
		}
		CoopPacket coopPacket16 = new CoopPacket();
		Coop_Model_PlayerSkillAction coop_Model_PlayerSkillAction = (Coop_Model_PlayerSkillAction)(coopPacket16.model = new Coop_Model_PlayerSkillAction());
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, promise: false, 0);
		PacketStream stream16 = null;
		try
		{
			stream16 = coopPacketSerializer.Serialize(coopPacket16);
		}
		catch (Exception arg31)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSkillAction\n" + arg31);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSkillAction>(stream16);
		}
		catch (Exception arg32)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSkillAction\n" + arg32);
		}
		CoopPacket coopPacket17 = new CoopPacket();
		Coop_Model_PlayerGetHeal coop_Model_PlayerGetHeal = (Coop_Model_PlayerGetHeal)(coopPacket17.model = new Coop_Model_PlayerGetHeal());
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, promise: false, 0);
		PacketStream stream17 = null;
		try
		{
			stream17 = coopPacketSerializer.Serialize(coopPacket17);
		}
		catch (Exception arg33)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetHeal\n" + arg33);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGetHeal>(stream17);
		}
		catch (Exception arg34)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetHeal\n" + arg34);
		}
		CoopPacket coopPacket18 = new CoopPacket();
		Coop_Model_PlayerSpecialAction coop_Model_PlayerSpecialAction = (Coop_Model_PlayerSpecialAction)(coopPacket18.model = new Coop_Model_PlayerSpecialAction());
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, promise: false, 0);
		PacketStream stream18 = null;
		try
		{
			stream18 = coopPacketSerializer.Serialize(coopPacket18);
		}
		catch (Exception arg35)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSpecialAction\n" + arg35);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSpecialAction>(stream18);
		}
		catch (Exception arg36)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSpecialAction\n" + arg36);
		}
		CoopPacket coopPacket19 = new CoopPacket();
		Coop_Model_PlayerShotArrow coop_Model_PlayerShotArrow = (Coop_Model_PlayerShotArrow)(coopPacket19.model = new Coop_Model_PlayerShotArrow());
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, promise: false, 0);
		PacketStream stream19 = null;
		try
		{
			stream19 = coopPacketSerializer.Serialize(coopPacket19);
		}
		catch (Exception arg37)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerShotArrow\n" + arg37);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerShotArrow>(stream19);
		}
		catch (Exception arg38)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerShotArrow\n" + arg38);
		}
		CoopPacket coopPacket20 = new CoopPacket();
		Coop_Model_PlayerUpdateSkillInfo coop_Model_PlayerUpdateSkillInfo = (Coop_Model_PlayerUpdateSkillInfo)(coopPacket20.model = new Coop_Model_PlayerUpdateSkillInfo());
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, promise: false, 0);
		PacketStream stream20 = null;
		try
		{
			stream20 = coopPacketSerializer.Serialize(coopPacket20);
		}
		catch (Exception arg39)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + arg39);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerUpdateSkillInfo>(stream20);
		}
		catch (Exception arg40)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + arg40);
		}
		CoopPacket coopPacket21 = new CoopPacket();
		Coop_Model_PlayerPrayerStart coop_Model_PlayerPrayerStart = (Coop_Model_PlayerPrayerStart)(coopPacket21.model = new Coop_Model_PlayerPrayerStart());
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, promise: false, 0);
		PacketStream stream21 = null;
		try
		{
			stream21 = coopPacketSerializer.Serialize(coopPacket21);
		}
		catch (Exception arg41)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerStart\n" + arg41);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerPrayerStart>(stream21);
		}
		catch (Exception arg42)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerStart\n" + arg42);
		}
		CoopPacket coopPacket22 = new CoopPacket();
		Coop_Model_PlayerPrayerEnd coop_Model_PlayerPrayerEnd = (Coop_Model_PlayerPrayerEnd)(coopPacket22.model = new Coop_Model_PlayerPrayerEnd());
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, promise: false, 0);
		PacketStream stream22 = null;
		try
		{
			stream22 = coopPacketSerializer.Serialize(coopPacket22);
		}
		catch (Exception arg43)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerEnd\n" + arg43);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerPrayerEnd>(stream22);
		}
		catch (Exception arg44)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerEnd\n" + arg44);
		}
		CoopPacket coopPacket23 = new CoopPacket();
		Coop_Model_PlayerChangeWeapon coop_Model_PlayerChangeWeapon = (Coop_Model_PlayerChangeWeapon)(coopPacket23.model = new Coop_Model_PlayerChangeWeapon());
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, promise: false, 0);
		PacketStream stream23 = null;
		try
		{
			stream23 = coopPacketSerializer.Serialize(coopPacket23);
		}
		catch (Exception arg45)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChangeWeapon\n" + arg45);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerChangeWeapon>(stream23);
		}
		catch (Exception arg46)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChangeWeapon\n" + arg46);
		}
		CoopPacket coopPacket24 = new CoopPacket();
		Coop_Model_PlayerApplyChangeWeapon coop_Model_PlayerApplyChangeWeapon = (Coop_Model_PlayerApplyChangeWeapon)(coopPacket24.model = new Coop_Model_PlayerApplyChangeWeapon());
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, promise: false, 0);
		PacketStream stream24 = null;
		try
		{
			stream24 = coopPacketSerializer.Serialize(coopPacket24);
		}
		catch (Exception arg47)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + arg47);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerApplyChangeWeapon>(stream24);
		}
		catch (Exception arg48)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + arg48);
		}
		CoopPacket coopPacket25 = new CoopPacket();
		Coop_Model_PlayerSetStatus coop_Model_PlayerSetStatus = (Coop_Model_PlayerSetStatus)(coopPacket25.model = new Coop_Model_PlayerSetStatus());
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, promise: false, 0);
		PacketStream stream25 = null;
		try
		{
			stream25 = coopPacketSerializer.Serialize(coopPacket25);
		}
		catch (Exception arg49)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSetStatus\n" + arg49);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSetStatus>(stream25);
		}
		catch (Exception arg50)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSetStatus\n" + arg50);
		}
		CoopPacket coopPacket26 = new CoopPacket();
		Coop_Model_PlayerGetRareDrop coop_Model_PlayerGetRareDrop = (Coop_Model_PlayerGetRareDrop)(coopPacket26.model = new Coop_Model_PlayerGetRareDrop());
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, promise: false, 0);
		PacketStream stream26 = null;
		try
		{
			stream26 = coopPacketSerializer.Serialize(coopPacket26);
		}
		catch (Exception arg51)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetRareDrop\n" + arg51);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGetRareDrop>(stream26);
		}
		catch (Exception arg52)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetRareDrop\n" + arg52);
		}
		CoopPacket coopPacket27 = new CoopPacket();
		Coop_Model_EnemyLoadComplete coop_Model_EnemyLoadComplete = (Coop_Model_EnemyLoadComplete)(coopPacket27.model = new Coop_Model_EnemyLoadComplete());
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, promise: false, 0);
		PacketStream stream27 = null;
		try
		{
			stream27 = coopPacketSerializer.Serialize(coopPacket27);
		}
		catch (Exception arg53)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyLoadComplete\n" + arg53);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyLoadComplete>(stream27);
		}
		catch (Exception arg54)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyLoadComplete\n" + arg54);
		}
		CoopPacket coopPacket28 = new CoopPacket();
		Coop_Model_EnemyInitialize coop_Model_EnemyInitialize = (Coop_Model_EnemyInitialize)(coopPacket28.model = new Coop_Model_EnemyInitialize());
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, promise: false, 0);
		PacketStream stream28 = null;
		try
		{
			stream28 = coopPacketSerializer.Serialize(coopPacket28);
		}
		catch (Exception arg55)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyInitialize\n" + arg55);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyInitialize>(stream28);
		}
		catch (Exception arg56)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyInitialize\n" + arg56);
		}
		CoopPacket coopPacket29 = new CoopPacket();
		Coop_Model_EnemyStep coop_Model_EnemyStep = (Coop_Model_EnemyStep)(coopPacket29.model = new Coop_Model_EnemyStep());
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, promise: false, 0);
		PacketStream stream29 = null;
		try
		{
			stream29 = coopPacketSerializer.Serialize(coopPacket29);
		}
		catch (Exception arg57)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyStep\n" + arg57);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyStep>(stream29);
		}
		catch (Exception arg58)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyStep\n" + arg58);
		}
		CoopPacket coopPacket30 = new CoopPacket();
		Coop_Model_EnemyReviveRegion coop_Model_EnemyReviveRegion = (Coop_Model_EnemyReviveRegion)(coopPacket30.model = new Coop_Model_EnemyReviveRegion());
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, promise: false, 0);
		PacketStream stream30 = null;
		try
		{
			stream30 = coopPacketSerializer.Serialize(coopPacket30);
		}
		catch (Exception arg59)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyReviveRegion\n" + arg59);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyReviveRegion>(stream30);
		}
		catch (Exception arg60)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyReviveRegion\n" + arg60);
		}
		CoopPacket coopPacket31 = new CoopPacket();
		Coop_Model_EnemyWarp coop_Model_EnemyWarp = (Coop_Model_EnemyWarp)(coopPacket31.model = new Coop_Model_EnemyWarp());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, promise: false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyWarp\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyWarp>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyWarp\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Coop_Model_EnemyTargetShotEvent coop_Model_EnemyTargetShotEvent = (Coop_Model_EnemyTargetShotEvent)(coopPacket32.model = new Coop_Model_EnemyTargetShotEvent());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, promise: false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyTargetShotEvent\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyTargetShotEvent>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyTargetShotEvent\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Coop_Model_EnemyRandomShotEvent coop_Model_EnemyRandomShotEvent = (Coop_Model_EnemyRandomShotEvent)(coopPacket33.model = new Coop_Model_EnemyRandomShotEvent());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, promise: false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyRandomShotEvent\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyRandomShotEvent>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyRandomShotEvent\n" + arg66);
		}
		CoopPacket coopPacket34 = new CoopPacket();
		Coop_Model_EnemyUpdateBleedDamage coop_Model_EnemyUpdateBleedDamage = (Coop_Model_EnemyUpdateBleedDamage)(coopPacket34.model = new Coop_Model_EnemyUpdateBleedDamage());
		coopPacket34.header = new CoopPacketHeader(coopPacket34.model.c, 0, 0, promise: false, 0);
		PacketStream stream34 = null;
		try
		{
			stream34 = coopPacketSerializer.Serialize(coopPacket34);
		}
		catch (Exception arg67)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + arg67);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyUpdateBleedDamage>(stream34);
		}
		catch (Exception arg68)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + arg68);
		}
	}
}
