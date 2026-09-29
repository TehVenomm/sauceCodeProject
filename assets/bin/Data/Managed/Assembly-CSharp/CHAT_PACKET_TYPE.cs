// Decompiled with JetBrains decompiler
// Type: CHAT_PACKET_TYPE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public enum CHAT_PACKET_TYPE
{
  ERROR_CONNECT_FAILED = -1, // 0xFFFFFFFF
  NONE = 0,
  JOIN_ROOM = 1,
  LEAVE_ROOM = 2,
  BROADCAST_ROOM = 3,
  SENDTO = 4,
  ROOM_INFO = 5,
  PARTY_INVITE = 100, // 0x00000064
  CLAN_JOIN_ROOM = 101, // 0x00000065
  RALLY_INVITE = 200, // 0x000000C8
  CLAN_LEAVE_ROOM = 201, // 0x000000C9
  DARK_MARKET_RESET = 202, // 0x000000CA
  DARK_MARKET_UPDATE = 203, // 0x000000CB
  JACKPOT_WIN_UPDATE = 204, // 0x000000CC
  TRADING_POST_SOLD = 205, // 0x000000CD
  CLAN_BROADCAST_ROOM = 301, // 0x0000012D
  CLAN_BROADCAST_STATUS = 302, // 0x0000012E
  CLAN_SENDTO = 401, // 0x00000191
  CLAN_ROOM_INFO = 501, // 0x000001F5
  HEART_BEAT = 502, // 0x000001F6
}
