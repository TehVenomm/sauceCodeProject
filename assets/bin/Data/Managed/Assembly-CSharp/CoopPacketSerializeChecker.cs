// Decompiled with JetBrains decompiler
// Type: CoopPacketSerializeChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CoopPacketSerializeChecker
{
  public static void Test()
  {
    CoopPacketSerializeChecker.Test0();
    CoopPacketSerializeChecker.Test1();
    CoopPacketSerializeChecker.Test2();
  }

  public static void Test0()
  {
    CoopPacketSerializer packetSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
    CoopPacket packet1 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_Register()
    };
    packet1.header = new CoopPacketHeader(packet1.model.c, 0, 0, false, 0);
    PacketStream stream1 = (PacketStream) null;
    try
    {
      stream1 = packetSerializer.Serialize(packet1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Register\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_Register>(stream1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Register\n" + (object) ex);
    }
    CoopPacket packet2 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RegisterACK()
    };
    packet2.header = new CoopPacketHeader(packet2.model.c, 0, 0, false, 0);
    PacketStream stream2 = (PacketStream) null;
    try
    {
      stream2 = packetSerializer.Serialize(packet2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RegisterACK\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RegisterACK>(stream2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RegisterACK\n" + (object) ex);
    }
    CoopPacket packet3 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ACK()
    };
    packet3.header = new CoopPacketHeader(packet3.model.c, 0, 0, false, 0);
    PacketStream stream3 = (PacketStream) null;
    try
    {
      stream3 = packetSerializer.Serialize(packet3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ACK\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ACK>(stream3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ACK\n" + (object) ex);
    }
    CoopPacket packet4 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_Disconnect()
    };
    packet4.header = new CoopPacketHeader(packet4.model.c, 0, 0, false, 0);
    PacketStream stream4 = (PacketStream) null;
    try
    {
      stream4 = packetSerializer.Serialize(packet4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Disconnect\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_Disconnect>(stream4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Disconnect\n" + (object) ex);
    }
    CoopPacket packet5 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_Alive()
    };
    packet5.header = new CoopPacketHeader(packet5.model.c, 0, 0, false, 0);
    PacketStream stream5 = (PacketStream) null;
    try
    {
      stream5 = packetSerializer.Serialize(packet5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_Alive\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_Alive>(stream5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_Alive\n" + (object) ex);
    }
    CoopPacket packet6 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomEntryClose()
    };
    packet6.header = new CoopPacketHeader(packet6.model.c, 0, 0, false, 0);
    PacketStream stream6 = (PacketStream) null;
    try
    {
      stream6 = packetSerializer.Serialize(packet6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomEntryClose\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomEntryClose>(stream6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomEntryClose\n" + (object) ex);
    }
    CoopPacket packet7 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomJoined()
    };
    packet7.header = new CoopPacketHeader(packet7.model.c, 0, 0, false, 0);
    PacketStream stream7 = (PacketStream) null;
    try
    {
      stream7 = packetSerializer.Serialize(packet7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomJoined\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomJoined>(stream7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomJoined\n" + (object) ex);
    }
    CoopPacket packet8 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomLeaved()
    };
    packet8.header = new CoopPacketHeader(packet8.model.c, 0, 0, false, 0);
    PacketStream stream8 = (PacketStream) null;
    try
    {
      stream8 = packetSerializer.Serialize(packet8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomLeaved\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomLeaved>(stream8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomLeaved\n" + (object) ex);
    }
    CoopPacket packet9 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomStageChange()
    };
    packet9.header = new CoopPacketHeader(packet9.model.c, 0, 0, false, 0);
    PacketStream stream9 = (PacketStream) null;
    try
    {
      stream9 = packetSerializer.Serialize(packet9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageChange\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomStageChange>(stream9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageChange\n" + (object) ex);
    }
    CoopPacket packet10 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomStageChanged()
    };
    packet10.header = new CoopPacketHeader(packet10.model.c, 0, 0, false, 0);
    PacketStream stream10 = (PacketStream) null;
    try
    {
      stream10 = packetSerializer.Serialize(packet10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageChanged\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomStageChanged>(stream10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageChanged\n" + (object) ex);
    }
    CoopPacket packet11 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomStageRequest()
    };
    packet11.header = new CoopPacketHeader(packet11.model.c, 0, 0, false, 0);
    PacketStream stream11 = (PacketStream) null;
    try
    {
      stream11 = packetSerializer.Serialize(packet11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageRequest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomStageRequest>(stream11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageRequest\n" + (object) ex);
    }
    CoopPacket packet12 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomStageRequested()
    };
    packet12.header = new CoopPacketHeader(packet12.model.c, 0, 0, false, 0);
    PacketStream stream12 = (PacketStream) null;
    try
    {
      stream12 = packetSerializer.Serialize(packet12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageRequested\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomStageRequested>(stream12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageRequested\n" + (object) ex);
    }
    CoopPacket packet13 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomStageHostChanged()
    };
    packet13.header = new CoopPacketHeader(packet13.model.c, 0, 0, false, 0);
    PacketStream stream13 = (PacketStream) null;
    try
    {
      stream13 = packetSerializer.Serialize(packet13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomStageHostChanged\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomStageHostChanged>(stream13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomStageHostChanged\n" + (object) ex);
    }
    CoopPacket packet14 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_BattleStart()
    };
    packet14.header = new CoopPacketHeader(packet14.model.c, 0, 0, false, 0);
    PacketStream stream14 = (PacketStream) null;
    try
    {
      stream14 = packetSerializer.Serialize(packet14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_BattleStart\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_BattleStart>(stream14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_BattleStart\n" + (object) ex);
    }
    CoopPacket packet15 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyPop()
    };
    packet15.header = new CoopPacketHeader(packet15.model.c, 0, 0, false, 0);
    PacketStream stream15 = (PacketStream) null;
    try
    {
      stream15 = packetSerializer.Serialize(packet15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyPop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyPop>(stream15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyPop\n" + (object) ex);
    }
    CoopPacket packet16 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyAttack()
    };
    packet16.header = new CoopPacketHeader(packet16.model.c, 0, 0, false, 0);
    PacketStream stream16 = (PacketStream) null;
    try
    {
      stream16 = packetSerializer.Serialize(packet16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyAttack\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyAttack>(stream16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyAttack\n" + (object) ex);
    }
    CoopPacket packet17 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyOut()
    };
    packet17.header = new CoopPacketHeader(packet17.model.c, 0, 0, false, 0);
    PacketStream stream17 = (PacketStream) null;
    try
    {
      stream17 = packetSerializer.Serialize(packet17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyOut\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyOut>(stream17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyOut\n" + (object) ex);
    }
    CoopPacket packet18 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyDefeat()
    };
    packet18.header = new CoopPacketHeader(packet18.model.c, 0, 0, false, 0);
    PacketStream stream18 = (PacketStream) null;
    try
    {
      stream18 = packetSerializer.Serialize(packet18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyDefeat\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyDefeat>(stream18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyDefeat\n" + (object) ex);
    }
    CoopPacket packet19 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RewardGet()
    };
    packet19.header = new CoopPacketHeader(packet19.model.c, 0, 0, false, 0);
    PacketStream stream19 = (PacketStream) null;
    try
    {
      stream19 = packetSerializer.Serialize(packet19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RewardGet\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RewardGet>(stream19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RewardGet\n" + (object) ex);
    }
    CoopPacket packet20 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RewardPickup()
    };
    packet20.header = new CoopPacketHeader(packet20.model.c, 0, 0, false, 0);
    PacketStream stream20 = (PacketStream) null;
    try
    {
      stream20 = packetSerializer.Serialize(packet20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RewardPickup\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RewardPickup>(stream20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RewardPickup\n" + (object) ex);
    }
    CoopPacket packet21 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyExtermination()
    };
    packet21.header = new CoopPacketHeader(packet21.model.c, 0, 0, false, 0);
    PacketStream stream21 = (PacketStream) null;
    try
    {
      stream21 = packetSerializer.Serialize(packet21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyExtermination\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyExtermination>(stream21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyExtermination\n" + (object) ex);
    }
    CoopPacket packet22 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_UpdateBoost()
    };
    packet22.header = new CoopPacketHeader(packet22.model.c, 0, 0, false, 0);
    PacketStream stream22 = (PacketStream) null;
    try
    {
      stream22 = packetSerializer.Serialize(packet22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_UpdateBoost\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_UpdateBoost>(stream22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_UpdateBoost\n" + (object) ex);
    }
    CoopPacket packet23 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_UpdateBoostComplete()
    };
    packet23.header = new CoopPacketHeader(packet23.model.c, 0, 0, false, 0);
    PacketStream stream23 = (PacketStream) null;
    try
    {
      stream23 = packetSerializer.Serialize(packet23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_UpdateBoostComplete\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_UpdateBoostComplete>(stream23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_UpdateBoostComplete\n" + (object) ex);
    }
    CoopPacket packet24 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomTimeCheck()
    };
    packet24.header = new CoopPacketHeader(packet24.model.c, 0, 0, false, 0);
    PacketStream stream24 = (PacketStream) null;
    try
    {
      stream24 = packetSerializer.Serialize(packet24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomTimeCheck\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomTimeCheck>(stream24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomTimeCheck\n" + (object) ex);
    }
    CoopPacket packet25 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomTimeUpdate()
    };
    packet25.header = new CoopPacketHeader(packet25.model.c, 0, 0, false, 0);
    PacketStream stream25 = (PacketStream) null;
    try
    {
      stream25 = packetSerializer.Serialize(packet25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomTimeUpdate\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomTimeUpdate>(stream25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomTimeUpdate\n" + (object) ex);
    }
    CoopPacket packet26 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyBossPop()
    };
    packet26.header = new CoopPacketHeader(packet26.model.c, 0, 0, false, 0);
    PacketStream stream26 = (PacketStream) null;
    try
    {
      stream26 = packetSerializer.Serialize(packet26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyBossPop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyBossPop>(stream26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyBossPop\n" + (object) ex);
    }
    CoopPacket packet27 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_WaveMatchInfo()
    };
    packet27.header = new CoopPacketHeader(packet27.model.c, 0, 0, false, 0);
    PacketStream stream27 = (PacketStream) null;
    try
    {
      stream27 = packetSerializer.Serialize(packet27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_WaveMatchInfo\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_WaveMatchInfo>(stream27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_WaveMatchInfo\n" + (object) ex);
    }
    CoopPacket packet28 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_WaveMatchDrop()
    };
    packet28.header = new CoopPacketHeader(packet28.model.c, 0, 0, false, 0);
    PacketStream stream28 = (PacketStream) null;
    try
    {
      stream28 = packetSerializer.Serialize(packet28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_WaveMatchDrop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_WaveMatchDrop>(stream28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_WaveMatchDrop\n" + (object) ex);
    }
    CoopPacket packet29 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyForcePop()
    };
    packet29.header = new CoopPacketHeader(packet29.model.c, 0, 0, false, 0);
    PacketStream stream29 = (PacketStream) null;
    try
    {
      stream29 = packetSerializer.Serialize(packet29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyForcePop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyForcePop>(stream29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyForcePop\n" + (object) ex);
    }
    CoopPacket packet30 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EventHappenQuest()
    };
    packet30.header = new CoopPacketHeader(packet30.model.c, 0, 0, false, 0);
    PacketStream stream30 = (PacketStream) null;
    try
    {
      stream30 = packetSerializer.Serialize(packet30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EventHappenQuest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EventHappenQuest>(stream30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EventHappenQuest\n" + (object) ex);
    }
    CoopPacket packet31 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EventHappenQuestStatus()
    };
    packet31.header = new CoopPacketHeader(packet31.model.c, 0, 0, false, 0);
    PacketStream stream31 = (PacketStream) null;
    try
    {
      stream31 = packetSerializer.Serialize(packet31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EventHappenQuestStatus\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EventHappenQuestStatus>(stream31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EventHappenQuestStatus\n" + (object) ex);
    }
    CoopPacket packet32 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageChatMessage()
    };
    packet32.header = new CoopPacketHeader(packet32.model.c, 0, 0, false, 0);
    PacketStream stream32 = (PacketStream) null;
    try
    {
      stream32 = packetSerializer.Serialize(packet32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatMessage\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageChatMessage>(stream32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatMessage\n" + (object) ex);
    }
    CoopPacket packet33 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Party_Model_Register()
    };
    packet33.header = new CoopPacketHeader(packet33.model.c, 0, 0, false, 0);
    PacketStream stream33 = (PacketStream) null;
    try
    {
      stream33 = packetSerializer.Serialize(packet33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_Register\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Party_Model_Register>(stream33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_Register\n" + (object) ex);
    }
    CoopPacket packet34 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Party_Model_RegisterACK()
    };
    packet34.header = new CoopPacketHeader(packet34.model.c, 0, 0, false, 0);
    PacketStream stream34 = (PacketStream) null;
    try
    {
      stream34 = packetSerializer.Serialize(packet34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RegisterACK\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Party_Model_RegisterACK>(stream34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RegisterACK\n" + (object) ex);
    }
    CoopPacket packet35 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Party_Model_RoomJoined()
    };
    packet35.header = new CoopPacketHeader(packet35.model.c, 0, 0, false, 0);
    PacketStream stream35 = (PacketStream) null;
    try
    {
      stream35 = packetSerializer.Serialize(packet35);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomJoined\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Party_Model_RoomJoined>(stream35);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomJoined\n" + (object) ex);
    }
    CoopPacket packet36 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Party_Model_RoomLeaved()
    };
    packet36.header = new CoopPacketHeader(packet36.model.c, 0, 0, false, 0);
    PacketStream stream36 = (PacketStream) null;
    try
    {
      stream36 = packetSerializer.Serialize(packet36);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Party_Model_RoomLeaved\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Party_Model_RoomLeaved>(stream36);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Party_Model_RoomLeaved\n" + (object) ex);
    }
    CoopPacket packet37 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_Register()
    };
    packet37.header = new CoopPacketHeader(packet37.model.c, 0, 0, false, 0);
    PacketStream stream37 = (PacketStream) null;
    try
    {
      stream37 = packetSerializer.Serialize(packet37);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_Register\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_Register>(stream37);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_Register\n" + (object) ex);
    }
    CoopPacket packet38 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RegisterACK()
    };
    packet38.header = new CoopPacketHeader(packet38.model.c, 0, 0, false, 0);
    PacketStream stream38 = (PacketStream) null;
    try
    {
      stream38 = packetSerializer.Serialize(packet38);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RegisterACK\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RegisterACK>(stream38);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RegisterACK\n" + (object) ex);
    }
    CoopPacket packet39 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomEntryClose()
    };
    packet39.header = new CoopPacketHeader(packet39.model.c, 0, 0, false, 0);
    PacketStream stream39 = (PacketStream) null;
    try
    {
      stream39 = packetSerializer.Serialize(packet39);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomEntryClose\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomEntryClose>(stream39);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomEntryClose\n" + (object) ex);
    }
    CoopPacket packet40 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomJoined()
    };
    packet40.header = new CoopPacketHeader(packet40.model.c, 0, 0, false, 0);
    PacketStream stream40 = (PacketStream) null;
    try
    {
      stream40 = packetSerializer.Serialize(packet40);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomJoined\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomJoined>(stream40);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomJoined\n" + (object) ex);
    }
    CoopPacket packet41 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomLeaved()
    };
    packet41.header = new CoopPacketHeader(packet41.model.c, 0, 0, false, 0);
    PacketStream stream41 = (PacketStream) null;
    try
    {
      stream41 = packetSerializer.Serialize(packet41);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomLeaved\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomLeaved>(stream41);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomLeaved\n" + (object) ex);
    }
    CoopPacket packet42 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomHostChanged()
    };
    packet42.header = new CoopPacketHeader(packet42.model.c, 0, 0, false, 0);
    PacketStream stream42 = (PacketStream) null;
    try
    {
      stream42 = packetSerializer.Serialize(packet42);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomHostChanged\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomHostChanged>(stream42);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomHostChanged\n" + (object) ex);
    }
    CoopPacket packet43 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomKick()
    };
    packet43.header = new CoopPacketHeader(packet43.model.c, 0, 0, false, 0);
    PacketStream stream43 = (PacketStream) null;
    try
    {
      stream43 = packetSerializer.Serialize(packet43);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomKick\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomKick>(stream43);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomKick\n" + (object) ex);
    }
    CoopPacket packet44 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomMove()
    };
    packet44.header = new CoopPacketHeader(packet44.model.c, 0, 0, false, 0);
    PacketStream stream44 = (PacketStream) null;
    try
    {
      stream44 = packetSerializer.Serialize(packet44);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomMove\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomMove>(stream44);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomMove\n" + (object) ex);
    }
    CoopPacket packet45 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomPosition()
    };
    packet45.header = new CoopPacketHeader(packet45.model.c, 0, 0, false, 0);
    PacketStream stream45 = (PacketStream) null;
    try
    {
      stream45 = packetSerializer.Serialize(packet45);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomPosition\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomPosition>(stream45);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomPosition\n" + (object) ex);
    }
    CoopPacket packet46 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_RoomAction()
    };
    packet46.header = new CoopPacketHeader(packet46.model.c, 0, 0, false, 0);
    PacketStream stream46 = (PacketStream) null;
    try
    {
      stream46 = packetSerializer.Serialize(packet46);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_RoomAction\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_RoomAction>(stream46);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_RoomAction\n" + (object) ex);
    }
    CoopPacket packet47 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_AFK_Kick()
    };
    packet47.header = new CoopPacketHeader(packet47.model.c, 0, 0, false, 0);
    PacketStream stream47 = (PacketStream) null;
    try
    {
      stream47 = packetSerializer.Serialize(packet47);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_AFK_Kick\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_AFK_Kick>(stream47);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_AFK_Kick\n" + (object) ex);
    }
    CoopPacket packet48 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_MemberLounge()
    };
    packet48.header = new CoopPacketHeader(packet48.model.c, 0, 0, false, 0);
    PacketStream stream48 = (PacketStream) null;
    try
    {
      stream48 = packetSerializer.Serialize(packet48);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberLounge\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_MemberLounge>(stream48);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberLounge\n" + (object) ex);
    }
    CoopPacket packet49 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_MemberField()
    };
    packet49.header = new CoopPacketHeader(packet49.model.c, 0, 0, false, 0);
    PacketStream stream49 = (PacketStream) null;
    try
    {
      stream49 = packetSerializer.Serialize(packet49);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberField\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_MemberField>(stream49);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberField\n" + (object) ex);
    }
    CoopPacket packet50 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_MemberQuest()
    };
    packet50.header = new CoopPacketHeader(packet50.model.c, 0, 0, false, 0);
    PacketStream stream50 = (PacketStream) null;
    try
    {
      stream50 = packetSerializer.Serialize(packet50);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberQuest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_MemberQuest>(stream50);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberQuest\n" + (object) ex);
    }
  }

  public static void Test1()
  {
    CoopPacketSerializer packetSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
    CoopPacket packet1 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_MemberQuest()
    };
    packet1.header = new CoopPacketHeader(packet1.model.c, 0, 0, false, 0);
    PacketStream stream1 = (PacketStream) null;
    try
    {
      stream1 = packetSerializer.Serialize(packet1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberQuest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_MemberQuest>(stream1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberQuest\n" + (object) ex);
    }
    CoopPacket packet2 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Lounge_Model_MemberArena()
    };
    packet2.header = new CoopPacketHeader(packet2.model.c, 0, 0, false, 0);
    PacketStream stream2 = (PacketStream) null;
    try
    {
      stream2 = packetSerializer.Serialize(packet2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Lounge_Model_MemberArena\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Lounge_Model_MemberArena>(stream2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Lounge_Model_MemberArena\n" + (object) ex);
    }
    CoopPacket packet3 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientStatus()
    };
    packet3.header = new CoopPacketHeader(packet3.model.c, 0, 0, false, 0);
    PacketStream stream3 = (PacketStream) null;
    try
    {
      stream3 = packetSerializer.Serialize(packet3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientStatus\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientStatus>(stream3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientStatus\n" + (object) ex);
    }
    CoopPacket packet4 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientBecameHost()
    };
    packet4.header = new CoopPacketHeader(packet4.model.c, 0, 0, false, 0);
    PacketStream stream4 = (PacketStream) null;
    try
    {
      stream4 = packetSerializer.Serialize(packet4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBecameHost\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientBecameHost>(stream4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBecameHost\n" + (object) ex);
    }
    CoopPacket packet5 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientLoadingProgress()
    };
    packet5.header = new CoopPacketHeader(packet5.model.c, 0, 0, false, 0);
    PacketStream stream5 = (PacketStream) null;
    try
    {
      stream5 = packetSerializer.Serialize(packet5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientLoadingProgress\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientLoadingProgress>(stream5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientLoadingProgress\n" + (object) ex);
    }
    CoopPacket packet6 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientChangeEquip()
    };
    packet6.header = new CoopPacketHeader(packet6.model.c, 0, 0, false, 0);
    PacketStream stream6 = (PacketStream) null;
    try
    {
      stream6 = packetSerializer.Serialize(packet6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientChangeEquip\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientChangeEquip>(stream6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientChangeEquip\n" + (object) ex);
    }
    CoopPacket packet7 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientBattleRetire()
    };
    packet7.header = new CoopPacketHeader(packet7.model.c, 0, 0, false, 0);
    PacketStream stream7 = (PacketStream) null;
    try
    {
      stream7 = packetSerializer.Serialize(packet7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientBattleRetire\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientBattleRetire>(stream7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientBattleRetire\n" + (object) ex);
    }
    CoopPacket packet8 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ClientSeriesProgress()
    };
    packet8.header = new CoopPacketHeader(packet8.model.c, 0, 0, false, 0);
    PacketStream stream8 = (PacketStream) null;
    try
    {
      stream8 = packetSerializer.Serialize(packet8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ClientSeriesProgress\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ClientSeriesProgress>(stream8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ClientSeriesProgress\n" + (object) ex);
    }
    CoopPacket packet9 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomUpdatePortalPoint()
    };
    packet9.header = new CoopPacketHeader(packet9.model.c, 0, 0, false, 0);
    PacketStream stream9 = (PacketStream) null;
    try
    {
      stream9 = packetSerializer.Serialize(packet9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomUpdatePortalPoint\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomUpdatePortalPoint>(stream9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomUpdatePortalPoint\n" + (object) ex);
    }
    CoopPacket packet10 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomSyncExploreBoss()
    };
    packet10.header = new CoopPacketHeader(packet10.model.c, 0, 0, false, 0);
    PacketStream stream10 = (PacketStream) null;
    try
    {
      stream10 = packetSerializer.Serialize(packet10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBoss\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomSyncExploreBoss>(stream10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBoss\n" + (object) ex);
    }
    CoopPacket packet11 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomSyncExploreBossMap()
    };
    packet11.header = new CoopPacketHeader(packet11.model.c, 0, 0, false, 0);
    PacketStream stream11 = (PacketStream) null;
    try
    {
      stream11 = packetSerializer.Serialize(packet11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncExploreBossMap\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomSyncExploreBossMap>(stream11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncExploreBossMap\n" + (object) ex);
    }
    CoopPacket packet12 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomExploreBossDead()
    };
    packet12.header = new CoopPacketHeader(packet12.model.c, 0, 0, false, 0);
    PacketStream stream12 = (PacketStream) null;
    try
    {
      stream12 = packetSerializer.Serialize(packet12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDead\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomExploreBossDead>(stream12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDead\n" + (object) ex);
    }
    CoopPacket packet13 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomNotifyEncounterBoss()
    };
    packet13.header = new CoopPacketHeader(packet13.model.c, 0, 0, false, 0);
    PacketStream stream13 = (PacketStream) null;
    try
    {
      stream13 = packetSerializer.Serialize(packet13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomNotifyEncounterBoss>(stream13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyEncounterBoss\n" + (object) ex);
    }
    CoopPacket packet14 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomSyncPlayerStatus()
    };
    packet14.header = new CoopPacketHeader(packet14.model.c, 0, 0, false, 0);
    PacketStream stream14 = (PacketStream) null;
    try
    {
      stream14 = packetSerializer.Serialize(packet14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncPlayerStatus\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomSyncPlayerStatus>(stream14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncPlayerStatus\n" + (object) ex);
    }
    CoopPacket packet15 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomChatStamp()
    };
    packet15.header = new CoopPacketHeader(packet15.model.c, 0, 0, false, 0);
    PacketStream stream15 = (PacketStream) null;
    try
    {
      stream15 = packetSerializer.Serialize(packet15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomChatStamp\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomChatStamp>(stream15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomChatStamp\n" + (object) ex);
    }
    CoopPacket packet16 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomExploreBossDamage()
    };
    packet16.header = new CoopPacketHeader(packet16.model.c, 0, 0, false, 0);
    PacketStream stream16 = (PacketStream) null;
    try
    {
      stream16 = packetSerializer.Serialize(packet16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreBossDamage\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomExploreBossDamage>(stream16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreBossDamage\n" + (object) ex);
    }
    CoopPacket packet17 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomExploreAlive()
    };
    packet17.header = new CoopPacketHeader(packet17.model.c, 0, 0, false, 0);
    PacketStream stream17 = (PacketStream) null;
    try
    {
      stream17 = packetSerializer.Serialize(packet17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAlive\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomExploreAlive>(stream17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAlive\n" + (object) ex);
    }
    CoopPacket packet18 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomExploreAliveRequest()
    };
    packet18.header = new CoopPacketHeader(packet18.model.c, 0, 0, false, 0);
    PacketStream stream18 = (PacketStream) null;
    try
    {
      stream18 = packetSerializer.Serialize(packet18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomExploreAliveRequest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomExploreAliveRequest>(stream18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomExploreAliveRequest\n" + (object) ex);
    }
    CoopPacket packet19 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomSyncAllPortalPoint()
    };
    packet19.header = new CoopPacketHeader(packet19.model.c, 0, 0, false, 0);
    PacketStream stream19 = (PacketStream) null;
    try
    {
      stream19 = packetSerializer.Serialize(packet19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomSyncAllPortalPoint>(stream19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomSyncAllPortalPoint\n" + (object) ex);
    }
    CoopPacket packet20 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomMoveField()
    };
    packet20.header = new CoopPacketHeader(packet20.model.c, 0, 0, false, 0);
    PacketStream stream20 = (PacketStream) null;
    try
    {
      stream20 = packetSerializer.Serialize(packet20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomMoveField\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomMoveField>(stream20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomMoveField\n" + (object) ex);
    }
    CoopPacket packet21 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RushRequest()
    };
    packet21.header = new CoopPacketHeader(packet21.model.c, 0, 0, false, 0);
    PacketStream stream21 = (PacketStream) null;
    try
    {
      stream21 = packetSerializer.Serialize(packet21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RushRequest>(stream21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequest\n" + (object) ex);
    }
    CoopPacket packet22 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RushRequested()
    };
    packet22.header = new CoopPacketHeader(packet22.model.c, 0, 0, false, 0);
    PacketStream stream22 = (PacketStream) null;
    try
    {
      stream22 = packetSerializer.Serialize(packet22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RushRequested\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RushRequested>(stream22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RushRequested\n" + (object) ex);
    }
    CoopPacket packet23 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_RoomNotifyTraceBoss()
    };
    packet23.header = new CoopPacketHeader(packet23.model.c, 0, 0, false, 0);
    PacketStream stream23 = (PacketStream) null;
    try
    {
      stream23 = packetSerializer.Serialize(packet23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_RoomNotifyTraceBoss\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_RoomNotifyTraceBoss>(stream23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_RoomNotifyTraceBoss\n" + (object) ex);
    }
    CoopPacket packet24 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageRequest()
    };
    packet24.header = new CoopPacketHeader(packet24.model.c, 0, 0, false, 0);
    PacketStream stream24 = (PacketStream) null;
    try
    {
      stream24 = packetSerializer.Serialize(packet24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageRequest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageRequest>(stream24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageRequest\n" + (object) ex);
    }
    CoopPacket packet25 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StagePlayerPop()
    };
    packet25.header = new CoopPacketHeader(packet25.model.c, 0, 0, false, 0);
    PacketStream stream25 = (PacketStream) null;
    try
    {
      stream25 = packetSerializer.Serialize(packet25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StagePlayerPop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StagePlayerPop>(stream25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StagePlayerPop\n" + (object) ex);
    }
    CoopPacket packet26 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageInfo()
    };
    packet26.header = new CoopPacketHeader(packet26.model.c, 0, 0, false, 0);
    PacketStream stream26 = (PacketStream) null;
    try
    {
      stream26 = packetSerializer.Serialize(packet26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageInfo\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageInfo>(stream26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageInfo\n" + (object) ex);
    }
    CoopPacket packet27 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageResponseEnd()
    };
    packet27.header = new CoopPacketHeader(packet27.model.c, 0, 0, false, 0);
    PacketStream stream27 = (PacketStream) null;
    try
    {
      stream27 = packetSerializer.Serialize(packet27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageResponseEnd\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageResponseEnd>(stream27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageResponseEnd\n" + (object) ex);
    }
    CoopPacket packet28 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageQuestClose()
    };
    packet28.header = new CoopPacketHeader(packet28.model.c, 0, 0, false, 0);
    PacketStream stream28 = (PacketStream) null;
    try
    {
      stream28 = packetSerializer.Serialize(packet28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageQuestClose\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageQuestClose>(stream28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageQuestClose\n" + (object) ex);
    }
    CoopPacket packet29 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageTimeup()
    };
    packet29.header = new CoopPacketHeader(packet29.model.c, 0, 0, false, 0);
    PacketStream stream29 = (PacketStream) null;
    try
    {
      stream29 = packetSerializer.Serialize(packet29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageTimeup\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageTimeup>(stream29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageTimeup\n" + (object) ex);
    }
    CoopPacket packet30 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageChat()
    };
    packet30.header = new CoopPacketHeader(packet30.model.c, 0, 0, false, 0);
    PacketStream stream30 = (PacketStream) null;
    try
    {
      stream30 = packetSerializer.Serialize(packet30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChat\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageChat>(stream30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChat\n" + (object) ex);
    }
    CoopPacket packet31 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageChatStamp()
    };
    packet31.header = new CoopPacketHeader(packet31.model.c, 0, 0, false, 0);
    PacketStream stream31 = (PacketStream) null;
    try
    {
      stream31 = packetSerializer.Serialize(packet31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageChatStamp\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageChatStamp>(stream31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageChatStamp\n" + (object) ex);
    }
    CoopPacket packet32 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageSyncTimeRequest()
    };
    packet32.header = new CoopPacketHeader(packet32.model.c, 0, 0, false, 0);
    PacketStream stream32 = (PacketStream) null;
    try
    {
      stream32 = packetSerializer.Serialize(packet32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTimeRequest\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageSyncTimeRequest>(stream32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTimeRequest\n" + (object) ex);
    }
    CoopPacket packet33 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_StageSyncTime()
    };
    packet33.header = new CoopPacketHeader(packet33.model.c, 0, 0, false, 0);
    PacketStream stream33 = (PacketStream) null;
    try
    {
      stream33 = packetSerializer.Serialize(packet33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_StageSyncTime\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_StageSyncTime>(stream33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_StageSyncTime\n" + (object) ex);
    }
    CoopPacket packet34 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ObjectDestroy()
    };
    packet34.header = new CoopPacketHeader(packet34.model.c, 0, 0, false, 0);
    PacketStream stream34 = (PacketStream) null;
    try
    {
      stream34 = packetSerializer.Serialize(packet34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectDestroy\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ObjectDestroy>(stream34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectDestroy\n" + (object) ex);
    }
    CoopPacket packet35 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ObjectAttackedHitOwner()
    };
    packet35.header = new CoopPacketHeader(packet35.model.c, 0, 0, false, 0);
    PacketStream stream35 = (PacketStream) null;
    try
    {
      stream35 = packetSerializer.Serialize(packet35);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitOwner\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ObjectAttackedHitOwner>(stream35);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitOwner\n" + (object) ex);
    }
    CoopPacket packet36 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ObjectAttackedHitFix()
    };
    packet36.header = new CoopPacketHeader(packet36.model.c, 0, 0, false, 0);
    PacketStream stream36 = (PacketStream) null;
    try
    {
      stream36 = packetSerializer.Serialize(packet36);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectAttackedHitFix\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ObjectAttackedHitFix>(stream36);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectAttackedHitFix\n" + (object) ex);
    }
    CoopPacket packet37 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_ObjectKeepWaitingPacket()
    };
    packet37.header = new CoopPacketHeader(packet37.model.c, 0, 0, false, 0);
    PacketStream stream37 = (PacketStream) null;
    try
    {
      stream37 = packetSerializer.Serialize(packet37);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_ObjectKeepWaitingPacket>(stream37);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_ObjectKeepWaitingPacket\n" + (object) ex);
    }
    CoopPacket packet38 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterActionTarget()
    };
    packet38.header = new CoopPacketHeader(packet38.model.c, 0, 0, false, 0);
    PacketStream stream38 = (PacketStream) null;
    try
    {
      stream38 = packetSerializer.Serialize(packet38);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterActionTarget\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterActionTarget>(stream38);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterActionTarget\n" + (object) ex);
    }
    CoopPacket packet39 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterUpdateActionPosition()
    };
    packet39.header = new CoopPacketHeader(packet39.model.c, 0, 0, false, 0);
    PacketStream stream39 = (PacketStream) null;
    try
    {
      stream39 = packetSerializer.Serialize(packet39);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateActionPosition\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterUpdateActionPosition>(stream39);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateActionPosition\n" + (object) ex);
    }
    CoopPacket packet40 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterUpdateDirection()
    };
    packet40.header = new CoopPacketHeader(packet40.model.c, 0, 0, false, 0);
    PacketStream stream40 = (PacketStream) null;
    try
    {
      stream40 = packetSerializer.Serialize(packet40);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterUpdateDirection\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterUpdateDirection>(stream40);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterUpdateDirection\n" + (object) ex);
    }
    CoopPacket packet41 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterPeriodicSyncActionPosition()
    };
    packet41.header = new CoopPacketHeader(packet41.model.c, 0, 0, false, 0);
    PacketStream stream41 = (PacketStream) null;
    try
    {
      stream41 = packetSerializer.Serialize(packet41);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterPeriodicSyncActionPosition>(stream41);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterPeriodicSyncActionPosition\n" + (object) ex);
    }
    CoopPacket packet42 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterIdle()
    };
    packet42.header = new CoopPacketHeader(packet42.model.c, 0, 0, false, 0);
    PacketStream stream42 = (PacketStream) null;
    try
    {
      stream42 = packetSerializer.Serialize(packet42);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterIdle\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterIdle>(stream42);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterIdle\n" + (object) ex);
    }
    CoopPacket packet43 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterMoveVelocity()
    };
    packet43.header = new CoopPacketHeader(packet43.model.c, 0, 0, false, 0);
    PacketStream stream43 = (PacketStream) null;
    try
    {
      stream43 = packetSerializer.Serialize(packet43);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocity\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterMoveVelocity>(stream43);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocity\n" + (object) ex);
    }
    CoopPacket packet44 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterMoveVelocityEnd()
    };
    packet44.header = new CoopPacketHeader(packet44.model.c, 0, 0, false, 0);
    PacketStream stream44 = (PacketStream) null;
    try
    {
      stream44 = packetSerializer.Serialize(packet44);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterMoveVelocityEnd>(stream44);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveVelocityEnd\n" + (object) ex);
    }
    CoopPacket packet45 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterMoveToPosition()
    };
    packet45.header = new CoopPacketHeader(packet45.model.c, 0, 0, false, 0);
    PacketStream stream45 = (PacketStream) null;
    try
    {
      stream45 = packetSerializer.Serialize(packet45);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveToPosition\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterMoveToPosition>(stream45);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveToPosition\n" + (object) ex);
    }
    CoopPacket packet46 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterMoveHoming()
    };
    packet46.header = new CoopPacketHeader(packet46.model.c, 0, 0, false, 0);
    PacketStream stream46 = (PacketStream) null;
    try
    {
      stream46 = packetSerializer.Serialize(packet46);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterMoveHoming\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterMoveHoming>(stream46);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterMoveHoming\n" + (object) ex);
    }
    CoopPacket packet47 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterRotate()
    };
    packet47.header = new CoopPacketHeader(packet47.model.c, 0, 0, false, 0);
    PacketStream stream47 = (PacketStream) null;
    try
    {
      stream47 = packetSerializer.Serialize(packet47);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotate\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterRotate>(stream47);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotate\n" + (object) ex);
    }
    CoopPacket packet48 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterRotateMotion()
    };
    packet48.header = new CoopPacketHeader(packet48.model.c, 0, 0, false, 0);
    PacketStream stream48 = (PacketStream) null;
    try
    {
      stream48 = packetSerializer.Serialize(packet48);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterRotateMotion\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterRotateMotion>(stream48);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterRotateMotion\n" + (object) ex);
    }
    CoopPacket packet49 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterAttack()
    };
    packet49.header = new CoopPacketHeader(packet49.model.c, 0, 0, false, 0);
    PacketStream stream49 = (PacketStream) null;
    try
    {
      stream49 = packetSerializer.Serialize(packet49);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterAttack\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterAttack>(stream49);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterAttack\n" + (object) ex);
    }
    CoopPacket packet50 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterBuffSync()
    };
    packet50.header = new CoopPacketHeader(packet50.model.c, 0, 0, false, 0);
    PacketStream stream50 = (PacketStream) null;
    try
    {
      stream50 = packetSerializer.Serialize(packet50);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffSync\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterBuffSync>(stream50);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffSync\n" + (object) ex);
    }
    CoopPacket packet51 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterBuffReceive()
    };
    packet51.header = new CoopPacketHeader(packet51.model.c, 0, 0, false, 0);
    PacketStream stream51 = (PacketStream) null;
    try
    {
      stream51 = packetSerializer.Serialize(packet51);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffReceive\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterBuffReceive>(stream51);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffReceive\n" + (object) ex);
    }
  }

  public static void Test2()
  {
    CoopPacketSerializer packetSerializer = CoopWebSocketSingleton<KtbWebSocket>.CreatePacketSerializer();
    CoopPacket packet1 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterBuffReceive()
    };
    packet1.header = new CoopPacketHeader(packet1.model.c, 0, 0, false, 0);
    PacketStream stream1 = (PacketStream) null;
    try
    {
      stream1 = packetSerializer.Serialize(packet1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffReceive\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterBuffReceive>(stream1);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffReceive\n" + (object) ex);
    }
    CoopPacket packet2 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterBuffRoutine()
    };
    packet2.header = new CoopPacketHeader(packet2.model.c, 0, 0, false, 0);
    PacketStream stream2 = (PacketStream) null;
    try
    {
      stream2 = packetSerializer.Serialize(packet2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterBuffRoutine\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterBuffRoutine>(stream2);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterBuffRoutine\n" + (object) ex);
    }
    CoopPacket packet3 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterReaction()
    };
    packet3.header = new CoopPacketHeader(packet3.model.c, 0, 0, false, 0);
    PacketStream stream3 = (PacketStream) null;
    try
    {
      stream3 = packetSerializer.Serialize(packet3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReaction\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterReaction>(stream3);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReaction\n" + (object) ex);
    }
    CoopPacket packet4 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_CharacterReactionDelay()
    };
    packet4.header = new CoopPacketHeader(packet4.model.c, 0, 0, false, 0);
    PacketStream stream4 = (PacketStream) null;
    try
    {
      stream4 = packetSerializer.Serialize(packet4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_CharacterReactionDelay\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_CharacterReactionDelay>(stream4);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_CharacterReactionDelay\n" + (object) ex);
    }
    CoopPacket packet5 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerLoadComplete()
    };
    packet5.header = new CoopPacketHeader(packet5.model.c, 0, 0, false, 0);
    PacketStream stream5 = (PacketStream) null;
    try
    {
      stream5 = packetSerializer.Serialize(packet5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerLoadComplete\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerLoadComplete>(stream5);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerLoadComplete\n" + (object) ex);
    }
    CoopPacket packet6 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerInitialize()
    };
    packet6.header = new CoopPacketHeader(packet6.model.c, 0, 0, false, 0);
    PacketStream stream6 = (PacketStream) null;
    try
    {
      stream6 = packetSerializer.Serialize(packet6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerInitialize\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerInitialize>(stream6);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerInitialize\n" + (object) ex);
    }
    CoopPacket packet7 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerAttackCombo()
    };
    packet7.header = new CoopPacketHeader(packet7.model.c, 0, 0, false, 0);
    PacketStream stream7 = (PacketStream) null;
    try
    {
      stream7 = packetSerializer.Serialize(packet7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAttackCombo\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerAttackCombo>(stream7);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAttackCombo\n" + (object) ex);
    }
    CoopPacket packet8 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerChargeRelease()
    };
    packet8.header = new CoopPacketHeader(packet8.model.c, 0, 0, false, 0);
    PacketStream stream8 = (PacketStream) null;
    try
    {
      stream8 = packetSerializer.Serialize(packet8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChargeRelease\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerChargeRelease>(stream8);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChargeRelease\n" + (object) ex);
    }
    CoopPacket packet9 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerAvoid()
    };
    packet9.header = new CoopPacketHeader(packet9.model.c, 0, 0, false, 0);
    PacketStream stream9 = (PacketStream) null;
    try
    {
      stream9 = packetSerializer.Serialize(packet9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerAvoid\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerAvoid>(stream9);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerAvoid\n" + (object) ex);
    }
    CoopPacket packet10 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerBlowClear()
    };
    packet10.header = new CoopPacketHeader(packet10.model.c, 0, 0, false, 0);
    PacketStream stream10 = (PacketStream) null;
    try
    {
      stream10 = packetSerializer.Serialize(packet10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerBlowClear\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerBlowClear>(stream10);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerBlowClear\n" + (object) ex);
    }
    CoopPacket packet11 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerStunnedEnd()
    };
    packet11.header = new CoopPacketHeader(packet11.model.c, 0, 0, false, 0);
    PacketStream stream11 = (PacketStream) null;
    try
    {
      stream11 = packetSerializer.Serialize(packet11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStunnedEnd\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerStunnedEnd>(stream11);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStunnedEnd\n" + (object) ex);
    }
    CoopPacket packet12 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerDeadCount()
    };
    packet12.header = new CoopPacketHeader(packet12.model.c, 0, 0, false, 0);
    PacketStream stream12 = (PacketStream) null;
    try
    {
      stream12 = packetSerializer.Serialize(packet12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadCount\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerDeadCount>(stream12);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadCount\n" + (object) ex);
    }
    CoopPacket packet13 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerDeadStandup()
    };
    packet13.header = new CoopPacketHeader(packet13.model.c, 0, 0, false, 0);
    PacketStream stream13 = (PacketStream) null;
    try
    {
      stream13 = packetSerializer.Serialize(packet13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerDeadStandup\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerDeadStandup>(stream13);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerDeadStandup\n" + (object) ex);
    }
    CoopPacket packet14 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerStopCounter()
    };
    packet14.header = new CoopPacketHeader(packet14.model.c, 0, 0, false, 0);
    PacketStream stream14 = (PacketStream) null;
    try
    {
      stream14 = packetSerializer.Serialize(packet14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerStopCounter\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerStopCounter>(stream14);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerStopCounter\n" + (object) ex);
    }
    CoopPacket packet15 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerGather()
    };
    packet15.header = new CoopPacketHeader(packet15.model.c, 0, 0, false, 0);
    PacketStream stream15 = (PacketStream) null;
    try
    {
      stream15 = packetSerializer.Serialize(packet15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGather\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerGather>(stream15);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGather\n" + (object) ex);
    }
    CoopPacket packet16 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerSkillAction()
    };
    packet16.header = new CoopPacketHeader(packet16.model.c, 0, 0, false, 0);
    PacketStream stream16 = (PacketStream) null;
    try
    {
      stream16 = packetSerializer.Serialize(packet16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSkillAction\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerSkillAction>(stream16);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSkillAction\n" + (object) ex);
    }
    CoopPacket packet17 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerGetHeal()
    };
    packet17.header = new CoopPacketHeader(packet17.model.c, 0, 0, false, 0);
    PacketStream stream17 = (PacketStream) null;
    try
    {
      stream17 = packetSerializer.Serialize(packet17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetHeal\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerGetHeal>(stream17);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetHeal\n" + (object) ex);
    }
    CoopPacket packet18 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerSpecialAction()
    };
    packet18.header = new CoopPacketHeader(packet18.model.c, 0, 0, false, 0);
    PacketStream stream18 = (PacketStream) null;
    try
    {
      stream18 = packetSerializer.Serialize(packet18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSpecialAction\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerSpecialAction>(stream18);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSpecialAction\n" + (object) ex);
    }
    CoopPacket packet19 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerShotArrow()
    };
    packet19.header = new CoopPacketHeader(packet19.model.c, 0, 0, false, 0);
    PacketStream stream19 = (PacketStream) null;
    try
    {
      stream19 = packetSerializer.Serialize(packet19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerShotArrow\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerShotArrow>(stream19);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerShotArrow\n" + (object) ex);
    }
    CoopPacket packet20 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerUpdateSkillInfo()
    };
    packet20.header = new CoopPacketHeader(packet20.model.c, 0, 0, false, 0);
    PacketStream stream20 = (PacketStream) null;
    try
    {
      stream20 = packetSerializer.Serialize(packet20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerUpdateSkillInfo>(stream20);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerUpdateSkillInfo\n" + (object) ex);
    }
    CoopPacket packet21 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerPrayerStart()
    };
    packet21.header = new CoopPacketHeader(packet21.model.c, 0, 0, false, 0);
    PacketStream stream21 = (PacketStream) null;
    try
    {
      stream21 = packetSerializer.Serialize(packet21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerStart\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerPrayerStart>(stream21);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerStart\n" + (object) ex);
    }
    CoopPacket packet22 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerPrayerEnd()
    };
    packet22.header = new CoopPacketHeader(packet22.model.c, 0, 0, false, 0);
    PacketStream stream22 = (PacketStream) null;
    try
    {
      stream22 = packetSerializer.Serialize(packet22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerPrayerEnd\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerPrayerEnd>(stream22);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerPrayerEnd\n" + (object) ex);
    }
    CoopPacket packet23 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerChangeWeapon()
    };
    packet23.header = new CoopPacketHeader(packet23.model.c, 0, 0, false, 0);
    PacketStream stream23 = (PacketStream) null;
    try
    {
      stream23 = packetSerializer.Serialize(packet23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerChangeWeapon\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerChangeWeapon>(stream23);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerChangeWeapon\n" + (object) ex);
    }
    CoopPacket packet24 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerApplyChangeWeapon()
    };
    packet24.header = new CoopPacketHeader(packet24.model.c, 0, 0, false, 0);
    PacketStream stream24 = (PacketStream) null;
    try
    {
      stream24 = packetSerializer.Serialize(packet24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerApplyChangeWeapon>(stream24);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerApplyChangeWeapon\n" + (object) ex);
    }
    CoopPacket packet25 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerSetStatus()
    };
    packet25.header = new CoopPacketHeader(packet25.model.c, 0, 0, false, 0);
    PacketStream stream25 = (PacketStream) null;
    try
    {
      stream25 = packetSerializer.Serialize(packet25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerSetStatus\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerSetStatus>(stream25);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerSetStatus\n" + (object) ex);
    }
    CoopPacket packet26 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_PlayerGetRareDrop()
    };
    packet26.header = new CoopPacketHeader(packet26.model.c, 0, 0, false, 0);
    PacketStream stream26 = (PacketStream) null;
    try
    {
      stream26 = packetSerializer.Serialize(packet26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_PlayerGetRareDrop\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_PlayerGetRareDrop>(stream26);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_PlayerGetRareDrop\n" + (object) ex);
    }
    CoopPacket packet27 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyLoadComplete()
    };
    packet27.header = new CoopPacketHeader(packet27.model.c, 0, 0, false, 0);
    PacketStream stream27 = (PacketStream) null;
    try
    {
      stream27 = packetSerializer.Serialize(packet27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyLoadComplete\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyLoadComplete>(stream27);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyLoadComplete\n" + (object) ex);
    }
    CoopPacket packet28 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyInitialize()
    };
    packet28.header = new CoopPacketHeader(packet28.model.c, 0, 0, false, 0);
    PacketStream stream28 = (PacketStream) null;
    try
    {
      stream28 = packetSerializer.Serialize(packet28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyInitialize\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyInitialize>(stream28);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyInitialize\n" + (object) ex);
    }
    CoopPacket packet29 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyStep()
    };
    packet29.header = new CoopPacketHeader(packet29.model.c, 0, 0, false, 0);
    PacketStream stream29 = (PacketStream) null;
    try
    {
      stream29 = packetSerializer.Serialize(packet29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyStep\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyStep>(stream29);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyStep\n" + (object) ex);
    }
    CoopPacket packet30 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyReviveRegion()
    };
    packet30.header = new CoopPacketHeader(packet30.model.c, 0, 0, false, 0);
    PacketStream stream30 = (PacketStream) null;
    try
    {
      stream30 = packetSerializer.Serialize(packet30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyReviveRegion\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyReviveRegion>(stream30);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyReviveRegion\n" + (object) ex);
    }
    CoopPacket packet31 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyWarp()
    };
    packet31.header = new CoopPacketHeader(packet31.model.c, 0, 0, false, 0);
    PacketStream stream31 = (PacketStream) null;
    try
    {
      stream31 = packetSerializer.Serialize(packet31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyWarp\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyWarp>(stream31);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyWarp\n" + (object) ex);
    }
    CoopPacket packet32 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyTargetShotEvent()
    };
    packet32.header = new CoopPacketHeader(packet32.model.c, 0, 0, false, 0);
    PacketStream stream32 = (PacketStream) null;
    try
    {
      stream32 = packetSerializer.Serialize(packet32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyTargetShotEvent\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyTargetShotEvent>(stream32);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyTargetShotEvent\n" + (object) ex);
    }
    CoopPacket packet33 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyRandomShotEvent()
    };
    packet33.header = new CoopPacketHeader(packet33.model.c, 0, 0, false, 0);
    PacketStream stream33 = (PacketStream) null;
    try
    {
      stream33 = packetSerializer.Serialize(packet33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyRandomShotEvent\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyRandomShotEvent>(stream33);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyRandomShotEvent\n" + (object) ex);
    }
    CoopPacket packet34 = new CoopPacket()
    {
      model = (Coop_Model_Base) new Coop_Model_EnemyUpdateBleedDamage()
    };
    packet34.header = new CoopPacketHeader(packet34.model.c, 0, 0, false, 0);
    PacketStream stream34 = (PacketStream) null;
    try
    {
      stream34 = packetSerializer.Serialize(packet34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Serializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + (object) ex);
    }
    try
    {
      packetSerializer.Deserialize<Coop_Model_EnemyUpdateBleedDamage>(stream34);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.WEBSOCK, "CoopPacketSerializeChecker: Deserializer Error : Coop_Model_EnemyUpdateBleedDamage\n" + (object) ex);
    }
  }
}
