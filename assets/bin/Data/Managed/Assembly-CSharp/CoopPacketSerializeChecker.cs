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
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, false, 0);
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
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, false, 0);
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
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, false, 0);
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
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, false, 0);
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
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, false, 0);
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
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, false, 0);
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
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, false, 0);
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
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, false, 0);
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
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, false, 0);
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
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, false, 0);
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
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, false, 0);
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
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, false, 0);
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
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, false, 0);
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
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, false, 0);
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
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, false, 0);
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
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, false, 0);
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
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, false, 0);
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
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, false, 0);
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
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, false, 0);
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
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, false, 0);
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
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, false, 0);
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
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, false, 0);
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
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, false, 0);
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
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, false, 0);
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
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, false, 0);
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
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, false, 0);
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
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, false, 0);
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
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, false, 0);
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
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, false, 0);
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
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, false, 0);
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
		Coop_Model_StageChatMessage coop_Model_StageChatMessage = (Coop_Model_StageChatMessage)(coopPacket31.model = new Coop_Model_StageChatMessage());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatMessage\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChatMessage>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatMessage\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Party_Model_Register party_Model_Register = (Party_Model_Register)(coopPacket32.model = new Party_Model_Register());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_Register\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_Register>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_Register\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Party_Model_RegisterACK party_Model_RegisterACK = (Party_Model_RegisterACK)(coopPacket33.model = new Party_Model_RegisterACK());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RegisterACK\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RegisterACK>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RegisterACK\n" + arg66);
		}
		CoopPacket coopPacket34 = new CoopPacket();
		Party_Model_RoomJoined party_Model_RoomJoined = (Party_Model_RoomJoined)(coopPacket34.model = new Party_Model_RoomJoined());
		coopPacket34.header = new CoopPacketHeader(coopPacket34.model.c, 0, 0, false, 0);
		PacketStream stream34 = null;
		try
		{
			stream34 = coopPacketSerializer.Serialize(coopPacket34);
		}
		catch (Exception arg67)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomJoined\n" + arg67);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RoomJoined>(stream34);
		}
		catch (Exception arg68)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomJoined\n" + arg68);
		}
		CoopPacket coopPacket35 = new CoopPacket();
		Party_Model_RoomLeaved party_Model_RoomLeaved = (Party_Model_RoomLeaved)(coopPacket35.model = new Party_Model_RoomLeaved());
		coopPacket35.header = new CoopPacketHeader(coopPacket35.model.c, 0, 0, false, 0);
		PacketStream stream35 = null;
		try
		{
			stream35 = coopPacketSerializer.Serialize(coopPacket35);
		}
		catch (Exception arg69)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomLeaved\n" + arg69);
		}
		try
		{
			coopPacketSerializer.Deserialize<Party_Model_RoomLeaved>(stream35);
		}
		catch (Exception arg70)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomLeaved\n" + arg70);
		}
		CoopPacket coopPacket36 = new CoopPacket();
		Lounge_Model_Register lounge_Model_Register = (Lounge_Model_Register)(coopPacket36.model = new Lounge_Model_Register());
		coopPacket36.header = new CoopPacketHeader(coopPacket36.model.c, 0, 0, false, 0);
		PacketStream stream36 = null;
		try
		{
			stream36 = coopPacketSerializer.Serialize(coopPacket36);
		}
		catch (Exception arg71)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_Register\n" + arg71);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_Register>(stream36);
		}
		catch (Exception arg72)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_Register\n" + arg72);
		}
		CoopPacket coopPacket37 = new CoopPacket();
		Lounge_Model_RegisterACK lounge_Model_RegisterACK = (Lounge_Model_RegisterACK)(coopPacket37.model = new Lounge_Model_RegisterACK());
		coopPacket37.header = new CoopPacketHeader(coopPacket37.model.c, 0, 0, false, 0);
		PacketStream stream37 = null;
		try
		{
			stream37 = coopPacketSerializer.Serialize(coopPacket37);
		}
		catch (Exception arg73)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RegisterACK\n" + arg73);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RegisterACK>(stream37);
		}
		catch (Exception arg74)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RegisterACK\n" + arg74);
		}
		CoopPacket coopPacket38 = new CoopPacket();
		Lounge_Model_RoomEntryClose lounge_Model_RoomEntryClose = (Lounge_Model_RoomEntryClose)(coopPacket38.model = new Lounge_Model_RoomEntryClose());
		coopPacket38.header = new CoopPacketHeader(coopPacket38.model.c, 0, 0, false, 0);
		PacketStream stream38 = null;
		try
		{
			stream38 = coopPacketSerializer.Serialize(coopPacket38);
		}
		catch (Exception arg75)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomEntryClose\n" + arg75);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomEntryClose>(stream38);
		}
		catch (Exception arg76)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomEntryClose\n" + arg76);
		}
		CoopPacket coopPacket39 = new CoopPacket();
		Lounge_Model_RoomJoined lounge_Model_RoomJoined = (Lounge_Model_RoomJoined)(coopPacket39.model = new Lounge_Model_RoomJoined());
		coopPacket39.header = new CoopPacketHeader(coopPacket39.model.c, 0, 0, false, 0);
		PacketStream stream39 = null;
		try
		{
			stream39 = coopPacketSerializer.Serialize(coopPacket39);
		}
		catch (Exception arg77)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomJoined\n" + arg77);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomJoined>(stream39);
		}
		catch (Exception arg78)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomJoined\n" + arg78);
		}
		CoopPacket coopPacket40 = new CoopPacket();
		Lounge_Model_RoomLeaved lounge_Model_RoomLeaved = (Lounge_Model_RoomLeaved)(coopPacket40.model = new Lounge_Model_RoomLeaved());
		coopPacket40.header = new CoopPacketHeader(coopPacket40.model.c, 0, 0, false, 0);
		PacketStream stream40 = null;
		try
		{
			stream40 = coopPacketSerializer.Serialize(coopPacket40);
		}
		catch (Exception arg79)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomLeaved\n" + arg79);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomLeaved>(stream40);
		}
		catch (Exception arg80)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomLeaved\n" + arg80);
		}
		CoopPacket coopPacket41 = new CoopPacket();
		Lounge_Model_RoomHostChanged lounge_Model_RoomHostChanged = (Lounge_Model_RoomHostChanged)(coopPacket41.model = new Lounge_Model_RoomHostChanged());
		coopPacket41.header = new CoopPacketHeader(coopPacket41.model.c, 0, 0, false, 0);
		PacketStream stream41 = null;
		try
		{
			stream41 = coopPacketSerializer.Serialize(coopPacket41);
		}
		catch (Exception arg81)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomHostChanged\n" + arg81);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomHostChanged>(stream41);
		}
		catch (Exception arg82)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomHostChanged\n" + arg82);
		}
		CoopPacket coopPacket42 = new CoopPacket();
		Lounge_Model_RoomKick lounge_Model_RoomKick = (Lounge_Model_RoomKick)(coopPacket42.model = new Lounge_Model_RoomKick());
		coopPacket42.header = new CoopPacketHeader(coopPacket42.model.c, 0, 0, false, 0);
		PacketStream stream42 = null;
		try
		{
			stream42 = coopPacketSerializer.Serialize(coopPacket42);
		}
		catch (Exception arg83)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomKick\n" + arg83);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomKick>(stream42);
		}
		catch (Exception arg84)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomKick\n" + arg84);
		}
		CoopPacket coopPacket43 = new CoopPacket();
		Lounge_Model_RoomMove lounge_Model_RoomMove = (Lounge_Model_RoomMove)(coopPacket43.model = new Lounge_Model_RoomMove());
		coopPacket43.header = new CoopPacketHeader(coopPacket43.model.c, 0, 0, false, 0);
		PacketStream stream43 = null;
		try
		{
			stream43 = coopPacketSerializer.Serialize(coopPacket43);
		}
		catch (Exception arg85)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomMove\n" + arg85);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomMove>(stream43);
		}
		catch (Exception arg86)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomMove\n" + arg86);
		}
		CoopPacket coopPacket44 = new CoopPacket();
		Lounge_Model_RoomPosition lounge_Model_RoomPosition = (Lounge_Model_RoomPosition)(coopPacket44.model = new Lounge_Model_RoomPosition());
		coopPacket44.header = new CoopPacketHeader(coopPacket44.model.c, 0, 0, false, 0);
		PacketStream stream44 = null;
		try
		{
			stream44 = coopPacketSerializer.Serialize(coopPacket44);
		}
		catch (Exception arg87)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomPosition\n" + arg87);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomPosition>(stream44);
		}
		catch (Exception arg88)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomPosition\n" + arg88);
		}
		CoopPacket coopPacket45 = new CoopPacket();
		Lounge_Model_RoomAction lounge_Model_RoomAction = (Lounge_Model_RoomAction)(coopPacket45.model = new Lounge_Model_RoomAction());
		coopPacket45.header = new CoopPacketHeader(coopPacket45.model.c, 0, 0, false, 0);
		PacketStream stream45 = null;
		try
		{
			stream45 = coopPacketSerializer.Serialize(coopPacket45);
		}
		catch (Exception arg89)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomAction\n" + arg89);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_RoomAction>(stream45);
		}
		catch (Exception arg90)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomAction\n" + arg90);
		}
		CoopPacket coopPacket46 = new CoopPacket();
		Lounge_Model_AFK_Kick lounge_Model_AFK_Kick = (Lounge_Model_AFK_Kick)(coopPacket46.model = new Lounge_Model_AFK_Kick());
		coopPacket46.header = new CoopPacketHeader(coopPacket46.model.c, 0, 0, false, 0);
		PacketStream stream46 = null;
		try
		{
			stream46 = coopPacketSerializer.Serialize(coopPacket46);
		}
		catch (Exception arg91)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_AFK_Kick\n" + arg91);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_AFK_Kick>(stream46);
		}
		catch (Exception arg92)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_AFK_Kick\n" + arg92);
		}
		CoopPacket coopPacket47 = new CoopPacket();
		Lounge_Model_MemberLounge lounge_Model_MemberLounge = (Lounge_Model_MemberLounge)(coopPacket47.model = new Lounge_Model_MemberLounge());
		coopPacket47.header = new CoopPacketHeader(coopPacket47.model.c, 0, 0, false, 0);
		PacketStream stream47 = null;
		try
		{
			stream47 = coopPacketSerializer.Serialize(coopPacket47);
		}
		catch (Exception arg93)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberLounge\n" + arg93);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberLounge>(stream47);
		}
		catch (Exception arg94)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberLounge\n" + arg94);
		}
		CoopPacket coopPacket48 = new CoopPacket();
		Lounge_Model_MemberField lounge_Model_MemberField = (Lounge_Model_MemberField)(coopPacket48.model = new Lounge_Model_MemberField());
		coopPacket48.header = new CoopPacketHeader(coopPacket48.model.c, 0, 0, false, 0);
		PacketStream stream48 = null;
		try
		{
			stream48 = coopPacketSerializer.Serialize(coopPacket48);
		}
		catch (Exception arg95)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberField\n" + arg95);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberField>(stream48);
		}
		catch (Exception arg96)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberField\n" + arg96);
		}
		CoopPacket coopPacket49 = new CoopPacket();
		Lounge_Model_MemberQuest lounge_Model_MemberQuest = (Lounge_Model_MemberQuest)(coopPacket49.model = new Lounge_Model_MemberQuest());
		coopPacket49.header = new CoopPacketHeader(coopPacket49.model.c, 0, 0, false, 0);
		PacketStream stream49 = null;
		try
		{
			stream49 = coopPacketSerializer.Serialize(coopPacket49);
		}
		catch (Exception arg97)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberQuest\n" + arg97);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberQuest>(stream49);
		}
		catch (Exception arg98)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberQuest\n" + arg98);
		}
		CoopPacket coopPacket50 = new CoopPacket();
		Lounge_Model_MemberArena lounge_Model_MemberArena = (Lounge_Model_MemberArena)(coopPacket50.model = new Lounge_Model_MemberArena());
		coopPacket50.header = new CoopPacketHeader(coopPacket50.model.c, 0, 0, false, 0);
		PacketStream stream50 = null;
		try
		{
			stream50 = coopPacketSerializer.Serialize(coopPacket50);
		}
		catch (Exception arg99)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberArena\n" + arg99);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberArena>(stream50);
		}
		catch (Exception arg100)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberArena\n" + arg100);
		}
	}

	public static void Test1()
	{
		CoopPacketSerializer coopPacketSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
		CoopPacket coopPacket = new CoopPacket();
		Lounge_Model_MemberArena lounge_Model_MemberArena = (Lounge_Model_MemberArena)(coopPacket.model = new Lounge_Model_MemberArena());
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, false, 0);
		PacketStream stream = null;
		try
		{
			stream = coopPacketSerializer.Serialize(coopPacket);
		}
		catch (Exception arg)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberArena\n" + arg);
		}
		try
		{
			coopPacketSerializer.Deserialize<Lounge_Model_MemberArena>(stream);
		}
		catch (Exception arg2)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberArena\n" + arg2);
		}
		CoopPacket coopPacket2 = new CoopPacket();
		Coop_Model_ClientStatus coop_Model_ClientStatus = (Coop_Model_ClientStatus)(coopPacket2.model = new Coop_Model_ClientStatus());
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, false, 0);
		PacketStream stream2 = null;
		try
		{
			stream2 = coopPacketSerializer.Serialize(coopPacket2);
		}
		catch (Exception arg3)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientStatus\n" + arg3);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientStatus>(stream2);
		}
		catch (Exception arg4)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientStatus\n" + arg4);
		}
		CoopPacket coopPacket3 = new CoopPacket();
		Coop_Model_ClientBecameHost coop_Model_ClientBecameHost = (Coop_Model_ClientBecameHost)(coopPacket3.model = new Coop_Model_ClientBecameHost());
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, false, 0);
		PacketStream stream3 = null;
		try
		{
			stream3 = coopPacketSerializer.Serialize(coopPacket3);
		}
		catch (Exception arg5)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBecameHost\n" + arg5);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientBecameHost>(stream3);
		}
		catch (Exception arg6)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBecameHost\n" + arg6);
		}
		CoopPacket coopPacket4 = new CoopPacket();
		Coop_Model_ClientLoadingProgress coop_Model_ClientLoadingProgress = (Coop_Model_ClientLoadingProgress)(coopPacket4.model = new Coop_Model_ClientLoadingProgress());
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, false, 0);
		PacketStream stream4 = null;
		try
		{
			stream4 = coopPacketSerializer.Serialize(coopPacket4);
		}
		catch (Exception arg7)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientLoadingProgress\n" + arg7);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientLoadingProgress>(stream4);
		}
		catch (Exception arg8)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientLoadingProgress\n" + arg8);
		}
		CoopPacket coopPacket5 = new CoopPacket();
		Coop_Model_ClientChangeEquip coop_Model_ClientChangeEquip = (Coop_Model_ClientChangeEquip)(coopPacket5.model = new Coop_Model_ClientChangeEquip());
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, false, 0);
		PacketStream stream5 = null;
		try
		{
			stream5 = coopPacketSerializer.Serialize(coopPacket5);
		}
		catch (Exception arg9)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientChangeEquip\n" + arg9);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientChangeEquip>(stream5);
		}
		catch (Exception arg10)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientChangeEquip\n" + arg10);
		}
		CoopPacket coopPacket6 = new CoopPacket();
		Coop_Model_ClientBattleRetire coop_Model_ClientBattleRetire = (Coop_Model_ClientBattleRetire)(coopPacket6.model = new Coop_Model_ClientBattleRetire());
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, false, 0);
		PacketStream stream6 = null;
		try
		{
			stream6 = coopPacketSerializer.Serialize(coopPacket6);
		}
		catch (Exception arg11)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBattleRetire\n" + arg11);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientBattleRetire>(stream6);
		}
		catch (Exception arg12)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBattleRetire\n" + arg12);
		}
		CoopPacket coopPacket7 = new CoopPacket();
		Coop_Model_ClientSeriesProgress coop_Model_ClientSeriesProgress = (Coop_Model_ClientSeriesProgress)(coopPacket7.model = new Coop_Model_ClientSeriesProgress());
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, false, 0);
		PacketStream stream7 = null;
		try
		{
			stream7 = coopPacketSerializer.Serialize(coopPacket7);
		}
		catch (Exception arg13)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientSeriesProgress\n" + arg13);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ClientSeriesProgress>(stream7);
		}
		catch (Exception arg14)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientSeriesProgress\n" + arg14);
		}
		CoopPacket coopPacket8 = new CoopPacket();
		Coop_Model_RoomUpdatePortalPoint coop_Model_RoomUpdatePortalPoint = (Coop_Model_RoomUpdatePortalPoint)(coopPacket8.model = new Coop_Model_RoomUpdatePortalPoint());
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, false, 0);
		PacketStream stream8 = null;
		try
		{
			stream8 = coopPacketSerializer.Serialize(coopPacket8);
		}
		catch (Exception arg15)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomUpdatePortalPoint\n" + arg15);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomUpdatePortalPoint>(stream8);
		}
		catch (Exception arg16)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomUpdatePortalPoint\n" + arg16);
		}
		CoopPacket coopPacket9 = new CoopPacket();
		Coop_Model_RoomSyncExploreBoss coop_Model_RoomSyncExploreBoss = (Coop_Model_RoomSyncExploreBoss)(coopPacket9.model = new Coop_Model_RoomSyncExploreBoss());
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, false, 0);
		PacketStream stream9 = null;
		try
		{
			stream9 = coopPacketSerializer.Serialize(coopPacket9);
		}
		catch (Exception arg17)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBoss\n" + arg17);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncExploreBoss>(stream9);
		}
		catch (Exception arg18)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBoss\n" + arg18);
		}
		CoopPacket coopPacket10 = new CoopPacket();
		Coop_Model_RoomSyncExploreBossMap coop_Model_RoomSyncExploreBossMap = (Coop_Model_RoomSyncExploreBossMap)(coopPacket10.model = new Coop_Model_RoomSyncExploreBossMap());
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, false, 0);
		PacketStream stream10 = null;
		try
		{
			stream10 = coopPacketSerializer.Serialize(coopPacket10);
		}
		catch (Exception arg19)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBossMap\n" + arg19);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncExploreBossMap>(stream10);
		}
		catch (Exception arg20)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBossMap\n" + arg20);
		}
		CoopPacket coopPacket11 = new CoopPacket();
		Coop_Model_RoomExploreBossDead coop_Model_RoomExploreBossDead = (Coop_Model_RoomExploreBossDead)(coopPacket11.model = new Coop_Model_RoomExploreBossDead());
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, false, 0);
		PacketStream stream11 = null;
		try
		{
			stream11 = coopPacketSerializer.Serialize(coopPacket11);
		}
		catch (Exception arg21)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDead\n" + arg21);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreBossDead>(stream11);
		}
		catch (Exception arg22)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDead\n" + arg22);
		}
		CoopPacket coopPacket12 = new CoopPacket();
		Coop_Model_RoomNotifyEncounterBoss coop_Model_RoomNotifyEncounterBoss = (Coop_Model_RoomNotifyEncounterBoss)(coopPacket12.model = new Coop_Model_RoomNotifyEncounterBoss());
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, false, 0);
		PacketStream stream12 = null;
		try
		{
			stream12 = coopPacketSerializer.Serialize(coopPacket12);
		}
		catch (Exception arg23)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + arg23);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomNotifyEncounterBoss>(stream12);
		}
		catch (Exception arg24)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + arg24);
		}
		CoopPacket coopPacket13 = new CoopPacket();
		Coop_Model_RoomSyncPlayerStatus coop_Model_RoomSyncPlayerStatus = (Coop_Model_RoomSyncPlayerStatus)(coopPacket13.model = new Coop_Model_RoomSyncPlayerStatus());
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, false, 0);
		PacketStream stream13 = null;
		try
		{
			stream13 = coopPacketSerializer.Serialize(coopPacket13);
		}
		catch (Exception arg25)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncPlayerStatus\n" + arg25);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncPlayerStatus>(stream13);
		}
		catch (Exception arg26)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncPlayerStatus\n" + arg26);
		}
		CoopPacket coopPacket14 = new CoopPacket();
		Coop_Model_RoomChatStamp coop_Model_RoomChatStamp = (Coop_Model_RoomChatStamp)(coopPacket14.model = new Coop_Model_RoomChatStamp());
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, false, 0);
		PacketStream stream14 = null;
		try
		{
			stream14 = coopPacketSerializer.Serialize(coopPacket14);
		}
		catch (Exception arg27)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomChatStamp\n" + arg27);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomChatStamp>(stream14);
		}
		catch (Exception arg28)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomChatStamp\n" + arg28);
		}
		CoopPacket coopPacket15 = new CoopPacket();
		Coop_Model_RoomExploreBossDamage coop_Model_RoomExploreBossDamage = (Coop_Model_RoomExploreBossDamage)(coopPacket15.model = new Coop_Model_RoomExploreBossDamage());
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, false, 0);
		PacketStream stream15 = null;
		try
		{
			stream15 = coopPacketSerializer.Serialize(coopPacket15);
		}
		catch (Exception arg29)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDamage\n" + arg29);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreBossDamage>(stream15);
		}
		catch (Exception arg30)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDamage\n" + arg30);
		}
		CoopPacket coopPacket16 = new CoopPacket();
		Coop_Model_RoomExploreAlive coop_Model_RoomExploreAlive = (Coop_Model_RoomExploreAlive)(coopPacket16.model = new Coop_Model_RoomExploreAlive());
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, false, 0);
		PacketStream stream16 = null;
		try
		{
			stream16 = coopPacketSerializer.Serialize(coopPacket16);
		}
		catch (Exception arg31)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAlive\n" + arg31);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreAlive>(stream16);
		}
		catch (Exception arg32)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAlive\n" + arg32);
		}
		CoopPacket coopPacket17 = new CoopPacket();
		Coop_Model_RoomExploreAliveRequest coop_Model_RoomExploreAliveRequest = (Coop_Model_RoomExploreAliveRequest)(coopPacket17.model = new Coop_Model_RoomExploreAliveRequest());
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, false, 0);
		PacketStream stream17 = null;
		try
		{
			stream17 = coopPacketSerializer.Serialize(coopPacket17);
		}
		catch (Exception arg33)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAliveRequest\n" + arg33);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomExploreAliveRequest>(stream17);
		}
		catch (Exception arg34)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAliveRequest\n" + arg34);
		}
		CoopPacket coopPacket18 = new CoopPacket();
		Coop_Model_RoomSyncAllPortalPoint coop_Model_RoomSyncAllPortalPoint = (Coop_Model_RoomSyncAllPortalPoint)(coopPacket18.model = new Coop_Model_RoomSyncAllPortalPoint());
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, false, 0);
		PacketStream stream18 = null;
		try
		{
			stream18 = coopPacketSerializer.Serialize(coopPacket18);
		}
		catch (Exception arg35)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + arg35);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomSyncAllPortalPoint>(stream18);
		}
		catch (Exception arg36)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + arg36);
		}
		CoopPacket coopPacket19 = new CoopPacket();
		Coop_Model_RoomMoveField coop_Model_RoomMoveField = (Coop_Model_RoomMoveField)(coopPacket19.model = new Coop_Model_RoomMoveField());
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, false, 0);
		PacketStream stream19 = null;
		try
		{
			stream19 = coopPacketSerializer.Serialize(coopPacket19);
		}
		catch (Exception arg37)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomMoveField\n" + arg37);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomMoveField>(stream19);
		}
		catch (Exception arg38)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomMoveField\n" + arg38);
		}
		CoopPacket coopPacket20 = new CoopPacket();
		Coop_Model_RushRequest coop_Model_RushRequest = (Coop_Model_RushRequest)(coopPacket20.model = new Coop_Model_RushRequest());
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, false, 0);
		PacketStream stream20 = null;
		try
		{
			stream20 = coopPacketSerializer.Serialize(coopPacket20);
		}
		catch (Exception arg39)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequest\n" + arg39);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RushRequest>(stream20);
		}
		catch (Exception arg40)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequest\n" + arg40);
		}
		CoopPacket coopPacket21 = new CoopPacket();
		Coop_Model_RushRequested coop_Model_RushRequested = (Coop_Model_RushRequested)(coopPacket21.model = new Coop_Model_RushRequested());
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, false, 0);
		PacketStream stream21 = null;
		try
		{
			stream21 = coopPacketSerializer.Serialize(coopPacket21);
		}
		catch (Exception arg41)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequested\n" + arg41);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RushRequested>(stream21);
		}
		catch (Exception arg42)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequested\n" + arg42);
		}
		CoopPacket coopPacket22 = new CoopPacket();
		Coop_Model_RoomNotifyTraceBoss coop_Model_RoomNotifyTraceBoss = (Coop_Model_RoomNotifyTraceBoss)(coopPacket22.model = new Coop_Model_RoomNotifyTraceBoss());
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, false, 0);
		PacketStream stream22 = null;
		try
		{
			stream22 = coopPacketSerializer.Serialize(coopPacket22);
		}
		catch (Exception arg43)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyTraceBoss\n" + arg43);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_RoomNotifyTraceBoss>(stream22);
		}
		catch (Exception arg44)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyTraceBoss\n" + arg44);
		}
		CoopPacket coopPacket23 = new CoopPacket();
		Coop_Model_StageRequest coop_Model_StageRequest = (Coop_Model_StageRequest)(coopPacket23.model = new Coop_Model_StageRequest());
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, false, 0);
		PacketStream stream23 = null;
		try
		{
			stream23 = coopPacketSerializer.Serialize(coopPacket23);
		}
		catch (Exception arg45)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageRequest\n" + arg45);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageRequest>(stream23);
		}
		catch (Exception arg46)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageRequest\n" + arg46);
		}
		CoopPacket coopPacket24 = new CoopPacket();
		Coop_Model_StagePlayerPop coop_Model_StagePlayerPop = (Coop_Model_StagePlayerPop)(coopPacket24.model = new Coop_Model_StagePlayerPop());
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, false, 0);
		PacketStream stream24 = null;
		try
		{
			stream24 = coopPacketSerializer.Serialize(coopPacket24);
		}
		catch (Exception arg47)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StagePlayerPop\n" + arg47);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StagePlayerPop>(stream24);
		}
		catch (Exception arg48)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StagePlayerPop\n" + arg48);
		}
		CoopPacket coopPacket25 = new CoopPacket();
		Coop_Model_StageInfo coop_Model_StageInfo = (Coop_Model_StageInfo)(coopPacket25.model = new Coop_Model_StageInfo());
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, false, 0);
		PacketStream stream25 = null;
		try
		{
			stream25 = coopPacketSerializer.Serialize(coopPacket25);
		}
		catch (Exception arg49)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageInfo\n" + arg49);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageInfo>(stream25);
		}
		catch (Exception arg50)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageInfo\n" + arg50);
		}
		CoopPacket coopPacket26 = new CoopPacket();
		Coop_Model_StageResponseEnd coop_Model_StageResponseEnd = (Coop_Model_StageResponseEnd)(coopPacket26.model = new Coop_Model_StageResponseEnd());
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, false, 0);
		PacketStream stream26 = null;
		try
		{
			stream26 = coopPacketSerializer.Serialize(coopPacket26);
		}
		catch (Exception arg51)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageResponseEnd\n" + arg51);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageResponseEnd>(stream26);
		}
		catch (Exception arg52)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageResponseEnd\n" + arg52);
		}
		CoopPacket coopPacket27 = new CoopPacket();
		Coop_Model_StageQuestClose coop_Model_StageQuestClose = (Coop_Model_StageQuestClose)(coopPacket27.model = new Coop_Model_StageQuestClose());
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, false, 0);
		PacketStream stream27 = null;
		try
		{
			stream27 = coopPacketSerializer.Serialize(coopPacket27);
		}
		catch (Exception arg53)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageQuestClose\n" + arg53);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageQuestClose>(stream27);
		}
		catch (Exception arg54)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageQuestClose\n" + arg54);
		}
		CoopPacket coopPacket28 = new CoopPacket();
		Coop_Model_StageTimeup coop_Model_StageTimeup = (Coop_Model_StageTimeup)(coopPacket28.model = new Coop_Model_StageTimeup());
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, false, 0);
		PacketStream stream28 = null;
		try
		{
			stream28 = coopPacketSerializer.Serialize(coopPacket28);
		}
		catch (Exception arg55)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageTimeup\n" + arg55);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageTimeup>(stream28);
		}
		catch (Exception arg56)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageTimeup\n" + arg56);
		}
		CoopPacket coopPacket29 = new CoopPacket();
		Coop_Model_StageChat coop_Model_StageChat = (Coop_Model_StageChat)(coopPacket29.model = new Coop_Model_StageChat());
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, false, 0);
		PacketStream stream29 = null;
		try
		{
			stream29 = coopPacketSerializer.Serialize(coopPacket29);
		}
		catch (Exception arg57)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChat\n" + arg57);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChat>(stream29);
		}
		catch (Exception arg58)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChat\n" + arg58);
		}
		CoopPacket coopPacket30 = new CoopPacket();
		Coop_Model_StageChatStamp coop_Model_StageChatStamp = (Coop_Model_StageChatStamp)(coopPacket30.model = new Coop_Model_StageChatStamp());
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, false, 0);
		PacketStream stream30 = null;
		try
		{
			stream30 = coopPacketSerializer.Serialize(coopPacket30);
		}
		catch (Exception arg59)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatStamp\n" + arg59);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageChatStamp>(stream30);
		}
		catch (Exception arg60)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatStamp\n" + arg60);
		}
		CoopPacket coopPacket31 = new CoopPacket();
		Coop_Model_StageSyncTimeRequest coop_Model_StageSyncTimeRequest = (Coop_Model_StageSyncTimeRequest)(coopPacket31.model = new Coop_Model_StageSyncTimeRequest());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTimeRequest\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageSyncTimeRequest>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTimeRequest\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Coop_Model_StageSyncTime coop_Model_StageSyncTime = (Coop_Model_StageSyncTime)(coopPacket32.model = new Coop_Model_StageSyncTime());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTime\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_StageSyncTime>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTime\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Coop_Model_ObjectDestroy coop_Model_ObjectDestroy = (Coop_Model_ObjectDestroy)(coopPacket33.model = new Coop_Model_ObjectDestroy());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectDestroy\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectDestroy>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectDestroy\n" + arg66);
		}
		CoopPacket coopPacket34 = new CoopPacket();
		Coop_Model_ObjectAttackedHitOwner coop_Model_ObjectAttackedHitOwner = (Coop_Model_ObjectAttackedHitOwner)(coopPacket34.model = new Coop_Model_ObjectAttackedHitOwner());
		coopPacket34.header = new CoopPacketHeader(coopPacket34.model.c, 0, 0, false, 0);
		PacketStream stream34 = null;
		try
		{
			stream34 = coopPacketSerializer.Serialize(coopPacket34);
		}
		catch (Exception arg67)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitOwner\n" + arg67);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectAttackedHitOwner>(stream34);
		}
		catch (Exception arg68)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitOwner\n" + arg68);
		}
		CoopPacket coopPacket35 = new CoopPacket();
		Coop_Model_ObjectAttackedHitFix coop_Model_ObjectAttackedHitFix = (Coop_Model_ObjectAttackedHitFix)(coopPacket35.model = new Coop_Model_ObjectAttackedHitFix());
		coopPacket35.header = new CoopPacketHeader(coopPacket35.model.c, 0, 0, false, 0);
		PacketStream stream35 = null;
		try
		{
			stream35 = coopPacketSerializer.Serialize(coopPacket35);
		}
		catch (Exception arg69)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitFix\n" + arg69);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectAttackedHitFix>(stream35);
		}
		catch (Exception arg70)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitFix\n" + arg70);
		}
		CoopPacket coopPacket36 = new CoopPacket();
		Coop_Model_ObjectKeepWaitingPacket coop_Model_ObjectKeepWaitingPacket = (Coop_Model_ObjectKeepWaitingPacket)(coopPacket36.model = new Coop_Model_ObjectKeepWaitingPacket());
		coopPacket36.header = new CoopPacketHeader(coopPacket36.model.c, 0, 0, false, 0);
		PacketStream stream36 = null;
		try
		{
			stream36 = coopPacketSerializer.Serialize(coopPacket36);
		}
		catch (Exception arg71)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + arg71);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_ObjectKeepWaitingPacket>(stream36);
		}
		catch (Exception arg72)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + arg72);
		}
		CoopPacket coopPacket37 = new CoopPacket();
		Coop_Model_CharacterActionTarget coop_Model_CharacterActionTarget = (Coop_Model_CharacterActionTarget)(coopPacket37.model = new Coop_Model_CharacterActionTarget());
		coopPacket37.header = new CoopPacketHeader(coopPacket37.model.c, 0, 0, false, 0);
		PacketStream stream37 = null;
		try
		{
			stream37 = coopPacketSerializer.Serialize(coopPacket37);
		}
		catch (Exception arg73)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterActionTarget\n" + arg73);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterActionTarget>(stream37);
		}
		catch (Exception arg74)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterActionTarget\n" + arg74);
		}
		CoopPacket coopPacket38 = new CoopPacket();
		Coop_Model_CharacterUpdateActionPosition coop_Model_CharacterUpdateActionPosition = (Coop_Model_CharacterUpdateActionPosition)(coopPacket38.model = new Coop_Model_CharacterUpdateActionPosition());
		coopPacket38.header = new CoopPacketHeader(coopPacket38.model.c, 0, 0, false, 0);
		PacketStream stream38 = null;
		try
		{
			stream38 = coopPacketSerializer.Serialize(coopPacket38);
		}
		catch (Exception arg75)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateActionPosition\n" + arg75);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterUpdateActionPosition>(stream38);
		}
		catch (Exception arg76)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateActionPosition\n" + arg76);
		}
		CoopPacket coopPacket39 = new CoopPacket();
		Coop_Model_CharacterUpdateDirection coop_Model_CharacterUpdateDirection = (Coop_Model_CharacterUpdateDirection)(coopPacket39.model = new Coop_Model_CharacterUpdateDirection());
		coopPacket39.header = new CoopPacketHeader(coopPacket39.model.c, 0, 0, false, 0);
		PacketStream stream39 = null;
		try
		{
			stream39 = coopPacketSerializer.Serialize(coopPacket39);
		}
		catch (Exception arg77)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateDirection\n" + arg77);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterUpdateDirection>(stream39);
		}
		catch (Exception arg78)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateDirection\n" + arg78);
		}
		CoopPacket coopPacket40 = new CoopPacket();
		Coop_Model_CharacterPeriodicSyncActionPosition coop_Model_CharacterPeriodicSyncActionPosition = (Coop_Model_CharacterPeriodicSyncActionPosition)(coopPacket40.model = new Coop_Model_CharacterPeriodicSyncActionPosition());
		coopPacket40.header = new CoopPacketHeader(coopPacket40.model.c, 0, 0, false, 0);
		PacketStream stream40 = null;
		try
		{
			stream40 = coopPacketSerializer.Serialize(coopPacket40);
		}
		catch (Exception arg79)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + arg79);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterPeriodicSyncActionPosition>(stream40);
		}
		catch (Exception arg80)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + arg80);
		}
		CoopPacket coopPacket41 = new CoopPacket();
		Coop_Model_CharacterIdle coop_Model_CharacterIdle = (Coop_Model_CharacterIdle)(coopPacket41.model = new Coop_Model_CharacterIdle());
		coopPacket41.header = new CoopPacketHeader(coopPacket41.model.c, 0, 0, false, 0);
		PacketStream stream41 = null;
		try
		{
			stream41 = coopPacketSerializer.Serialize(coopPacket41);
		}
		catch (Exception arg81)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterIdle\n" + arg81);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterIdle>(stream41);
		}
		catch (Exception arg82)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterIdle\n" + arg82);
		}
		CoopPacket coopPacket42 = new CoopPacket();
		Coop_Model_CharacterMoveVelocity coop_Model_CharacterMoveVelocity = (Coop_Model_CharacterMoveVelocity)(coopPacket42.model = new Coop_Model_CharacterMoveVelocity());
		coopPacket42.header = new CoopPacketHeader(coopPacket42.model.c, 0, 0, false, 0);
		PacketStream stream42 = null;
		try
		{
			stream42 = coopPacketSerializer.Serialize(coopPacket42);
		}
		catch (Exception arg83)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocity\n" + arg83);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveVelocity>(stream42);
		}
		catch (Exception arg84)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocity\n" + arg84);
		}
		CoopPacket coopPacket43 = new CoopPacket();
		Coop_Model_CharacterMoveVelocityEnd coop_Model_CharacterMoveVelocityEnd = (Coop_Model_CharacterMoveVelocityEnd)(coopPacket43.model = new Coop_Model_CharacterMoveVelocityEnd());
		coopPacket43.header = new CoopPacketHeader(coopPacket43.model.c, 0, 0, false, 0);
		PacketStream stream43 = null;
		try
		{
			stream43 = coopPacketSerializer.Serialize(coopPacket43);
		}
		catch (Exception arg85)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + arg85);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveVelocityEnd>(stream43);
		}
		catch (Exception arg86)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + arg86);
		}
		CoopPacket coopPacket44 = new CoopPacket();
		Coop_Model_CharacterMoveToPosition coop_Model_CharacterMoveToPosition = (Coop_Model_CharacterMoveToPosition)(coopPacket44.model = new Coop_Model_CharacterMoveToPosition());
		coopPacket44.header = new CoopPacketHeader(coopPacket44.model.c, 0, 0, false, 0);
		PacketStream stream44 = null;
		try
		{
			stream44 = coopPacketSerializer.Serialize(coopPacket44);
		}
		catch (Exception arg87)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveToPosition\n" + arg87);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveToPosition>(stream44);
		}
		catch (Exception arg88)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveToPosition\n" + arg88);
		}
		CoopPacket coopPacket45 = new CoopPacket();
		Coop_Model_CharacterMoveHoming coop_Model_CharacterMoveHoming = (Coop_Model_CharacterMoveHoming)(coopPacket45.model = new Coop_Model_CharacterMoveHoming());
		coopPacket45.header = new CoopPacketHeader(coopPacket45.model.c, 0, 0, false, 0);
		PacketStream stream45 = null;
		try
		{
			stream45 = coopPacketSerializer.Serialize(coopPacket45);
		}
		catch (Exception arg89)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveHoming\n" + arg89);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterMoveHoming>(stream45);
		}
		catch (Exception arg90)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveHoming\n" + arg90);
		}
		CoopPacket coopPacket46 = new CoopPacket();
		Coop_Model_CharacterRotate coop_Model_CharacterRotate = (Coop_Model_CharacterRotate)(coopPacket46.model = new Coop_Model_CharacterRotate());
		coopPacket46.header = new CoopPacketHeader(coopPacket46.model.c, 0, 0, false, 0);
		PacketStream stream46 = null;
		try
		{
			stream46 = coopPacketSerializer.Serialize(coopPacket46);
		}
		catch (Exception arg91)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotate\n" + arg91);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterRotate>(stream46);
		}
		catch (Exception arg92)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotate\n" + arg92);
		}
		CoopPacket coopPacket47 = new CoopPacket();
		Coop_Model_CharacterRotateMotion coop_Model_CharacterRotateMotion = (Coop_Model_CharacterRotateMotion)(coopPacket47.model = new Coop_Model_CharacterRotateMotion());
		coopPacket47.header = new CoopPacketHeader(coopPacket47.model.c, 0, 0, false, 0);
		PacketStream stream47 = null;
		try
		{
			stream47 = coopPacketSerializer.Serialize(coopPacket47);
		}
		catch (Exception arg93)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotateMotion\n" + arg93);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterRotateMotion>(stream47);
		}
		catch (Exception arg94)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotateMotion\n" + arg94);
		}
		CoopPacket coopPacket48 = new CoopPacket();
		Coop_Model_CharacterAttack coop_Model_CharacterAttack = (Coop_Model_CharacterAttack)(coopPacket48.model = new Coop_Model_CharacterAttack());
		coopPacket48.header = new CoopPacketHeader(coopPacket48.model.c, 0, 0, false, 0);
		PacketStream stream48 = null;
		try
		{
			stream48 = coopPacketSerializer.Serialize(coopPacket48);
		}
		catch (Exception arg95)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterAttack\n" + arg95);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterAttack>(stream48);
		}
		catch (Exception arg96)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterAttack\n" + arg96);
		}
		CoopPacket coopPacket49 = new CoopPacket();
		Coop_Model_CharacterBuffSync coop_Model_CharacterBuffSync = (Coop_Model_CharacterBuffSync)(coopPacket49.model = new Coop_Model_CharacterBuffSync());
		coopPacket49.header = new CoopPacketHeader(coopPacket49.model.c, 0, 0, false, 0);
		PacketStream stream49 = null;
		try
		{
			stream49 = coopPacketSerializer.Serialize(coopPacket49);
		}
		catch (Exception arg97)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffSync\n" + arg97);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffSync>(stream49);
		}
		catch (Exception arg98)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffSync\n" + arg98);
		}
		CoopPacket coopPacket50 = new CoopPacket();
		Coop_Model_CharacterBuffReceive coop_Model_CharacterBuffReceive = (Coop_Model_CharacterBuffReceive)(coopPacket50.model = new Coop_Model_CharacterBuffReceive());
		coopPacket50.header = new CoopPacketHeader(coopPacket50.model.c, 0, 0, false, 0);
		PacketStream stream50 = null;
		try
		{
			stream50 = coopPacketSerializer.Serialize(coopPacket50);
		}
		catch (Exception arg99)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffReceive\n" + arg99);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffReceive>(stream50);
		}
		catch (Exception arg100)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffReceive\n" + arg100);
		}
		CoopPacket coopPacket51 = new CoopPacket();
		Coop_Model_CharacterBuffRoutine coop_Model_CharacterBuffRoutine = (Coop_Model_CharacterBuffRoutine)(coopPacket51.model = new Coop_Model_CharacterBuffRoutine());
		coopPacket51.header = new CoopPacketHeader(coopPacket51.model.c, 0, 0, false, 0);
		PacketStream stream51 = null;
		try
		{
			stream51 = coopPacketSerializer.Serialize(coopPacket51);
		}
		catch (Exception arg101)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffRoutine\n" + arg101);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffRoutine>(stream51);
		}
		catch (Exception arg102)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffRoutine\n" + arg102);
		}
	}

	public static void Test2()
	{
		CoopPacketSerializer coopPacketSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
		CoopPacket coopPacket = new CoopPacket();
		Coop_Model_CharacterBuffRoutine coop_Model_CharacterBuffRoutine = (Coop_Model_CharacterBuffRoutine)(coopPacket.model = new Coop_Model_CharacterBuffRoutine());
		coopPacket.header = new CoopPacketHeader(coopPacket.model.c, 0, 0, false, 0);
		PacketStream stream = null;
		try
		{
			stream = coopPacketSerializer.Serialize(coopPacket);
		}
		catch (Exception arg)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffRoutine\n" + arg);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterBuffRoutine>(stream);
		}
		catch (Exception arg2)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffRoutine\n" + arg2);
		}
		CoopPacket coopPacket2 = new CoopPacket();
		Coop_Model_CharacterReaction coop_Model_CharacterReaction = (Coop_Model_CharacterReaction)(coopPacket2.model = new Coop_Model_CharacterReaction());
		coopPacket2.header = new CoopPacketHeader(coopPacket2.model.c, 0, 0, false, 0);
		PacketStream stream2 = null;
		try
		{
			stream2 = coopPacketSerializer.Serialize(coopPacket2);
		}
		catch (Exception arg3)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReaction\n" + arg3);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterReaction>(stream2);
		}
		catch (Exception arg4)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReaction\n" + arg4);
		}
		CoopPacket coopPacket3 = new CoopPacket();
		Coop_Model_CharacterReactionDelay coop_Model_CharacterReactionDelay = (Coop_Model_CharacterReactionDelay)(coopPacket3.model = new Coop_Model_CharacterReactionDelay());
		coopPacket3.header = new CoopPacketHeader(coopPacket3.model.c, 0, 0, false, 0);
		PacketStream stream3 = null;
		try
		{
			stream3 = coopPacketSerializer.Serialize(coopPacket3);
		}
		catch (Exception arg5)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReactionDelay\n" + arg5);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_CharacterReactionDelay>(stream3);
		}
		catch (Exception arg6)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReactionDelay\n" + arg6);
		}
		CoopPacket coopPacket4 = new CoopPacket();
		Coop_Model_PlayerLoadComplete coop_Model_PlayerLoadComplete = (Coop_Model_PlayerLoadComplete)(coopPacket4.model = new Coop_Model_PlayerLoadComplete());
		coopPacket4.header = new CoopPacketHeader(coopPacket4.model.c, 0, 0, false, 0);
		PacketStream stream4 = null;
		try
		{
			stream4 = coopPacketSerializer.Serialize(coopPacket4);
		}
		catch (Exception arg7)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerLoadComplete\n" + arg7);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerLoadComplete>(stream4);
		}
		catch (Exception arg8)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerLoadComplete\n" + arg8);
		}
		CoopPacket coopPacket5 = new CoopPacket();
		Coop_Model_PlayerInitialize coop_Model_PlayerInitialize = (Coop_Model_PlayerInitialize)(coopPacket5.model = new Coop_Model_PlayerInitialize());
		coopPacket5.header = new CoopPacketHeader(coopPacket5.model.c, 0, 0, false, 0);
		PacketStream stream5 = null;
		try
		{
			stream5 = coopPacketSerializer.Serialize(coopPacket5);
		}
		catch (Exception arg9)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerInitialize\n" + arg9);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerInitialize>(stream5);
		}
		catch (Exception arg10)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerInitialize\n" + arg10);
		}
		CoopPacket coopPacket6 = new CoopPacket();
		Coop_Model_PlayerAttackCombo coop_Model_PlayerAttackCombo = (Coop_Model_PlayerAttackCombo)(coopPacket6.model = new Coop_Model_PlayerAttackCombo());
		coopPacket6.header = new CoopPacketHeader(coopPacket6.model.c, 0, 0, false, 0);
		PacketStream stream6 = null;
		try
		{
			stream6 = coopPacketSerializer.Serialize(coopPacket6);
		}
		catch (Exception arg11)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAttackCombo\n" + arg11);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerAttackCombo>(stream6);
		}
		catch (Exception arg12)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAttackCombo\n" + arg12);
		}
		CoopPacket coopPacket7 = new CoopPacket();
		Coop_Model_PlayerChargeRelease coop_Model_PlayerChargeRelease = (Coop_Model_PlayerChargeRelease)(coopPacket7.model = new Coop_Model_PlayerChargeRelease());
		coopPacket7.header = new CoopPacketHeader(coopPacket7.model.c, 0, 0, false, 0);
		PacketStream stream7 = null;
		try
		{
			stream7 = coopPacketSerializer.Serialize(coopPacket7);
		}
		catch (Exception arg13)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChargeRelease\n" + arg13);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerChargeRelease>(stream7);
		}
		catch (Exception arg14)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChargeRelease\n" + arg14);
		}
		CoopPacket coopPacket8 = new CoopPacket();
		Coop_Model_PlayerAvoid coop_Model_PlayerAvoid = (Coop_Model_PlayerAvoid)(coopPacket8.model = new Coop_Model_PlayerAvoid());
		coopPacket8.header = new CoopPacketHeader(coopPacket8.model.c, 0, 0, false, 0);
		PacketStream stream8 = null;
		try
		{
			stream8 = coopPacketSerializer.Serialize(coopPacket8);
		}
		catch (Exception arg15)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAvoid\n" + arg15);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerAvoid>(stream8);
		}
		catch (Exception arg16)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAvoid\n" + arg16);
		}
		CoopPacket coopPacket9 = new CoopPacket();
		Coop_Model_PlayerBlowClear coop_Model_PlayerBlowClear = (Coop_Model_PlayerBlowClear)(coopPacket9.model = new Coop_Model_PlayerBlowClear());
		coopPacket9.header = new CoopPacketHeader(coopPacket9.model.c, 0, 0, false, 0);
		PacketStream stream9 = null;
		try
		{
			stream9 = coopPacketSerializer.Serialize(coopPacket9);
		}
		catch (Exception arg17)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerBlowClear\n" + arg17);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerBlowClear>(stream9);
		}
		catch (Exception arg18)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerBlowClear\n" + arg18);
		}
		CoopPacket coopPacket10 = new CoopPacket();
		Coop_Model_PlayerStunnedEnd coop_Model_PlayerStunnedEnd = (Coop_Model_PlayerStunnedEnd)(coopPacket10.model = new Coop_Model_PlayerStunnedEnd());
		coopPacket10.header = new CoopPacketHeader(coopPacket10.model.c, 0, 0, false, 0);
		PacketStream stream10 = null;
		try
		{
			stream10 = coopPacketSerializer.Serialize(coopPacket10);
		}
		catch (Exception arg19)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStunnedEnd\n" + arg19);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerStunnedEnd>(stream10);
		}
		catch (Exception arg20)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStunnedEnd\n" + arg20);
		}
		CoopPacket coopPacket11 = new CoopPacket();
		Coop_Model_PlayerDeadCount coop_Model_PlayerDeadCount = (Coop_Model_PlayerDeadCount)(coopPacket11.model = new Coop_Model_PlayerDeadCount());
		coopPacket11.header = new CoopPacketHeader(coopPacket11.model.c, 0, 0, false, 0);
		PacketStream stream11 = null;
		try
		{
			stream11 = coopPacketSerializer.Serialize(coopPacket11);
		}
		catch (Exception arg21)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadCount\n" + arg21);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerDeadCount>(stream11);
		}
		catch (Exception arg22)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadCount\n" + arg22);
		}
		CoopPacket coopPacket12 = new CoopPacket();
		Coop_Model_PlayerDeadStandup coop_Model_PlayerDeadStandup = (Coop_Model_PlayerDeadStandup)(coopPacket12.model = new Coop_Model_PlayerDeadStandup());
		coopPacket12.header = new CoopPacketHeader(coopPacket12.model.c, 0, 0, false, 0);
		PacketStream stream12 = null;
		try
		{
			stream12 = coopPacketSerializer.Serialize(coopPacket12);
		}
		catch (Exception arg23)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadStandup\n" + arg23);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerDeadStandup>(stream12);
		}
		catch (Exception arg24)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadStandup\n" + arg24);
		}
		CoopPacket coopPacket13 = new CoopPacket();
		Coop_Model_PlayerStopCounter coop_Model_PlayerStopCounter = (Coop_Model_PlayerStopCounter)(coopPacket13.model = new Coop_Model_PlayerStopCounter());
		coopPacket13.header = new CoopPacketHeader(coopPacket13.model.c, 0, 0, false, 0);
		PacketStream stream13 = null;
		try
		{
			stream13 = coopPacketSerializer.Serialize(coopPacket13);
		}
		catch (Exception arg25)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStopCounter\n" + arg25);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerStopCounter>(stream13);
		}
		catch (Exception arg26)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStopCounter\n" + arg26);
		}
		CoopPacket coopPacket14 = new CoopPacket();
		Coop_Model_PlayerGather coop_Model_PlayerGather = (Coop_Model_PlayerGather)(coopPacket14.model = new Coop_Model_PlayerGather());
		coopPacket14.header = new CoopPacketHeader(coopPacket14.model.c, 0, 0, false, 0);
		PacketStream stream14 = null;
		try
		{
			stream14 = coopPacketSerializer.Serialize(coopPacket14);
		}
		catch (Exception arg27)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGather\n" + arg27);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGather>(stream14);
		}
		catch (Exception arg28)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGather\n" + arg28);
		}
		CoopPacket coopPacket15 = new CoopPacket();
		Coop_Model_PlayerSkillAction coop_Model_PlayerSkillAction = (Coop_Model_PlayerSkillAction)(coopPacket15.model = new Coop_Model_PlayerSkillAction());
		coopPacket15.header = new CoopPacketHeader(coopPacket15.model.c, 0, 0, false, 0);
		PacketStream stream15 = null;
		try
		{
			stream15 = coopPacketSerializer.Serialize(coopPacket15);
		}
		catch (Exception arg29)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSkillAction\n" + arg29);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSkillAction>(stream15);
		}
		catch (Exception arg30)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSkillAction\n" + arg30);
		}
		CoopPacket coopPacket16 = new CoopPacket();
		Coop_Model_PlayerGetHeal coop_Model_PlayerGetHeal = (Coop_Model_PlayerGetHeal)(coopPacket16.model = new Coop_Model_PlayerGetHeal());
		coopPacket16.header = new CoopPacketHeader(coopPacket16.model.c, 0, 0, false, 0);
		PacketStream stream16 = null;
		try
		{
			stream16 = coopPacketSerializer.Serialize(coopPacket16);
		}
		catch (Exception arg31)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetHeal\n" + arg31);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGetHeal>(stream16);
		}
		catch (Exception arg32)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetHeal\n" + arg32);
		}
		CoopPacket coopPacket17 = new CoopPacket();
		Coop_Model_PlayerSpecialAction coop_Model_PlayerSpecialAction = (Coop_Model_PlayerSpecialAction)(coopPacket17.model = new Coop_Model_PlayerSpecialAction());
		coopPacket17.header = new CoopPacketHeader(coopPacket17.model.c, 0, 0, false, 0);
		PacketStream stream17 = null;
		try
		{
			stream17 = coopPacketSerializer.Serialize(coopPacket17);
		}
		catch (Exception arg33)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSpecialAction\n" + arg33);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSpecialAction>(stream17);
		}
		catch (Exception arg34)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSpecialAction\n" + arg34);
		}
		CoopPacket coopPacket18 = new CoopPacket();
		Coop_Model_PlayerShotArrow coop_Model_PlayerShotArrow = (Coop_Model_PlayerShotArrow)(coopPacket18.model = new Coop_Model_PlayerShotArrow());
		coopPacket18.header = new CoopPacketHeader(coopPacket18.model.c, 0, 0, false, 0);
		PacketStream stream18 = null;
		try
		{
			stream18 = coopPacketSerializer.Serialize(coopPacket18);
		}
		catch (Exception arg35)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerShotArrow\n" + arg35);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerShotArrow>(stream18);
		}
		catch (Exception arg36)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerShotArrow\n" + arg36);
		}
		CoopPacket coopPacket19 = new CoopPacket();
		Coop_Model_PlayerUpdateSkillInfo coop_Model_PlayerUpdateSkillInfo = (Coop_Model_PlayerUpdateSkillInfo)(coopPacket19.model = new Coop_Model_PlayerUpdateSkillInfo());
		coopPacket19.header = new CoopPacketHeader(coopPacket19.model.c, 0, 0, false, 0);
		PacketStream stream19 = null;
		try
		{
			stream19 = coopPacketSerializer.Serialize(coopPacket19);
		}
		catch (Exception arg37)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + arg37);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerUpdateSkillInfo>(stream19);
		}
		catch (Exception arg38)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + arg38);
		}
		CoopPacket coopPacket20 = new CoopPacket();
		Coop_Model_PlayerPrayerStart coop_Model_PlayerPrayerStart = (Coop_Model_PlayerPrayerStart)(coopPacket20.model = new Coop_Model_PlayerPrayerStart());
		coopPacket20.header = new CoopPacketHeader(coopPacket20.model.c, 0, 0, false, 0);
		PacketStream stream20 = null;
		try
		{
			stream20 = coopPacketSerializer.Serialize(coopPacket20);
		}
		catch (Exception arg39)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerStart\n" + arg39);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerPrayerStart>(stream20);
		}
		catch (Exception arg40)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerStart\n" + arg40);
		}
		CoopPacket coopPacket21 = new CoopPacket();
		Coop_Model_PlayerPrayerEnd coop_Model_PlayerPrayerEnd = (Coop_Model_PlayerPrayerEnd)(coopPacket21.model = new Coop_Model_PlayerPrayerEnd());
		coopPacket21.header = new CoopPacketHeader(coopPacket21.model.c, 0, 0, false, 0);
		PacketStream stream21 = null;
		try
		{
			stream21 = coopPacketSerializer.Serialize(coopPacket21);
		}
		catch (Exception arg41)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerEnd\n" + arg41);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerPrayerEnd>(stream21);
		}
		catch (Exception arg42)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerEnd\n" + arg42);
		}
		CoopPacket coopPacket22 = new CoopPacket();
		Coop_Model_PlayerChangeWeapon coop_Model_PlayerChangeWeapon = (Coop_Model_PlayerChangeWeapon)(coopPacket22.model = new Coop_Model_PlayerChangeWeapon());
		coopPacket22.header = new CoopPacketHeader(coopPacket22.model.c, 0, 0, false, 0);
		PacketStream stream22 = null;
		try
		{
			stream22 = coopPacketSerializer.Serialize(coopPacket22);
		}
		catch (Exception arg43)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChangeWeapon\n" + arg43);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerChangeWeapon>(stream22);
		}
		catch (Exception arg44)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChangeWeapon\n" + arg44);
		}
		CoopPacket coopPacket23 = new CoopPacket();
		Coop_Model_PlayerApplyChangeWeapon coop_Model_PlayerApplyChangeWeapon = (Coop_Model_PlayerApplyChangeWeapon)(coopPacket23.model = new Coop_Model_PlayerApplyChangeWeapon());
		coopPacket23.header = new CoopPacketHeader(coopPacket23.model.c, 0, 0, false, 0);
		PacketStream stream23 = null;
		try
		{
			stream23 = coopPacketSerializer.Serialize(coopPacket23);
		}
		catch (Exception arg45)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + arg45);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerApplyChangeWeapon>(stream23);
		}
		catch (Exception arg46)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + arg46);
		}
		CoopPacket coopPacket24 = new CoopPacket();
		Coop_Model_PlayerSetStatus coop_Model_PlayerSetStatus = (Coop_Model_PlayerSetStatus)(coopPacket24.model = new Coop_Model_PlayerSetStatus());
		coopPacket24.header = new CoopPacketHeader(coopPacket24.model.c, 0, 0, false, 0);
		PacketStream stream24 = null;
		try
		{
			stream24 = coopPacketSerializer.Serialize(coopPacket24);
		}
		catch (Exception arg47)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSetStatus\n" + arg47);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerSetStatus>(stream24);
		}
		catch (Exception arg48)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSetStatus\n" + arg48);
		}
		CoopPacket coopPacket25 = new CoopPacket();
		Coop_Model_PlayerGetRareDrop coop_Model_PlayerGetRareDrop = (Coop_Model_PlayerGetRareDrop)(coopPacket25.model = new Coop_Model_PlayerGetRareDrop());
		coopPacket25.header = new CoopPacketHeader(coopPacket25.model.c, 0, 0, false, 0);
		PacketStream stream25 = null;
		try
		{
			stream25 = coopPacketSerializer.Serialize(coopPacket25);
		}
		catch (Exception arg49)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetRareDrop\n" + arg49);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_PlayerGetRareDrop>(stream25);
		}
		catch (Exception arg50)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetRareDrop\n" + arg50);
		}
		CoopPacket coopPacket26 = new CoopPacket();
		Coop_Model_EnemyLoadComplete coop_Model_EnemyLoadComplete = (Coop_Model_EnemyLoadComplete)(coopPacket26.model = new Coop_Model_EnemyLoadComplete());
		coopPacket26.header = new CoopPacketHeader(coopPacket26.model.c, 0, 0, false, 0);
		PacketStream stream26 = null;
		try
		{
			stream26 = coopPacketSerializer.Serialize(coopPacket26);
		}
		catch (Exception arg51)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyLoadComplete\n" + arg51);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyLoadComplete>(stream26);
		}
		catch (Exception arg52)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyLoadComplete\n" + arg52);
		}
		CoopPacket coopPacket27 = new CoopPacket();
		Coop_Model_EnemyInitialize coop_Model_EnemyInitialize = (Coop_Model_EnemyInitialize)(coopPacket27.model = new Coop_Model_EnemyInitialize());
		coopPacket27.header = new CoopPacketHeader(coopPacket27.model.c, 0, 0, false, 0);
		PacketStream stream27 = null;
		try
		{
			stream27 = coopPacketSerializer.Serialize(coopPacket27);
		}
		catch (Exception arg53)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyInitialize\n" + arg53);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyInitialize>(stream27);
		}
		catch (Exception arg54)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyInitialize\n" + arg54);
		}
		CoopPacket coopPacket28 = new CoopPacket();
		Coop_Model_EnemyStep coop_Model_EnemyStep = (Coop_Model_EnemyStep)(coopPacket28.model = new Coop_Model_EnemyStep());
		coopPacket28.header = new CoopPacketHeader(coopPacket28.model.c, 0, 0, false, 0);
		PacketStream stream28 = null;
		try
		{
			stream28 = coopPacketSerializer.Serialize(coopPacket28);
		}
		catch (Exception arg55)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyStep\n" + arg55);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyStep>(stream28);
		}
		catch (Exception arg56)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyStep\n" + arg56);
		}
		CoopPacket coopPacket29 = new CoopPacket();
		Coop_Model_EnemyReviveRegion coop_Model_EnemyReviveRegion = (Coop_Model_EnemyReviveRegion)(coopPacket29.model = new Coop_Model_EnemyReviveRegion());
		coopPacket29.header = new CoopPacketHeader(coopPacket29.model.c, 0, 0, false, 0);
		PacketStream stream29 = null;
		try
		{
			stream29 = coopPacketSerializer.Serialize(coopPacket29);
		}
		catch (Exception arg57)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyReviveRegion\n" + arg57);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyReviveRegion>(stream29);
		}
		catch (Exception arg58)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyReviveRegion\n" + arg58);
		}
		CoopPacket coopPacket30 = new CoopPacket();
		Coop_Model_EnemyWarp coop_Model_EnemyWarp = (Coop_Model_EnemyWarp)(coopPacket30.model = new Coop_Model_EnemyWarp());
		coopPacket30.header = new CoopPacketHeader(coopPacket30.model.c, 0, 0, false, 0);
		PacketStream stream30 = null;
		try
		{
			stream30 = coopPacketSerializer.Serialize(coopPacket30);
		}
		catch (Exception arg59)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyWarp\n" + arg59);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyWarp>(stream30);
		}
		catch (Exception arg60)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyWarp\n" + arg60);
		}
		CoopPacket coopPacket31 = new CoopPacket();
		Coop_Model_EnemyTargetShotEvent coop_Model_EnemyTargetShotEvent = (Coop_Model_EnemyTargetShotEvent)(coopPacket31.model = new Coop_Model_EnemyTargetShotEvent());
		coopPacket31.header = new CoopPacketHeader(coopPacket31.model.c, 0, 0, false, 0);
		PacketStream stream31 = null;
		try
		{
			stream31 = coopPacketSerializer.Serialize(coopPacket31);
		}
		catch (Exception arg61)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyTargetShotEvent\n" + arg61);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyTargetShotEvent>(stream31);
		}
		catch (Exception arg62)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyTargetShotEvent\n" + arg62);
		}
		CoopPacket coopPacket32 = new CoopPacket();
		Coop_Model_EnemyRandomShotEvent coop_Model_EnemyRandomShotEvent = (Coop_Model_EnemyRandomShotEvent)(coopPacket32.model = new Coop_Model_EnemyRandomShotEvent());
		coopPacket32.header = new CoopPacketHeader(coopPacket32.model.c, 0, 0, false, 0);
		PacketStream stream32 = null;
		try
		{
			stream32 = coopPacketSerializer.Serialize(coopPacket32);
		}
		catch (Exception arg63)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyRandomShotEvent\n" + arg63);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyRandomShotEvent>(stream32);
		}
		catch (Exception arg64)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyRandomShotEvent\n" + arg64);
		}
		CoopPacket coopPacket33 = new CoopPacket();
		Coop_Model_EnemyUpdateBleedDamage coop_Model_EnemyUpdateBleedDamage = (Coop_Model_EnemyUpdateBleedDamage)(coopPacket33.model = new Coop_Model_EnemyUpdateBleedDamage());
		coopPacket33.header = new CoopPacketHeader(coopPacket33.model.c, 0, 0, false, 0);
		PacketStream stream33 = null;
		try
		{
			stream33 = coopPacketSerializer.Serialize(coopPacket33);
		}
		catch (Exception arg65)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + arg65);
		}
		try
		{
			coopPacketSerializer.Deserialize<Coop_Model_EnemyUpdateBleedDamage>(stream33);
		}
		catch (Exception arg66)
		{
			Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + arg66);
		}
	}
}
