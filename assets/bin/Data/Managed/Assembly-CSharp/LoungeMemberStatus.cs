// Decompiled with JetBrains decompiler
// Type: LoungeMemberStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class LoungeMemberStatus
{
  public int userId { get; private set; }

  public string partyId { get; private set; }

  public int questId { get; private set; }

  public string fieldId { get; private set; }

  public int fieldMapId { get; private set; }

  public DateTime lastExecTime { get; private set; }

  public bool isHost { get; private set; }

  public int arenaId { get; private set; }

  public LoungeMemberStatus(int user_id)
  {
    this.userId = user_id;
    this.UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
  }

  public LoungeMemberStatus(PartyModel.SlotInfo info)
    : this(info.userInfo.userId)
  {
  }

  public LoungeMemberStatus(Party_Model_RegisterACK.UserInfo ack)
  {
    this.userId = ack.userId;
    this.partyId = ack.partyId;
    this.questId = ack.questId;
    this.fieldId = ack.fieldId;
    this.fieldMapId = ack.fieldMapId;
    this.UpdateLastExecTime(ack.lastExecTime);
  }

  public void ToQuest(string party, int quest, bool host)
  {
    this.partyId = party;
    this.questId = quest;
    this.isHost = host;
  }

  public void ToField(string field, int fieldMap, string party, int quest, bool host)
  {
    this.fieldId = field;
    this.fieldMapId = fieldMap;
    this.ToQuest(party, quest, host);
  }

  public void ToLounge()
  {
    this.partyId = (string) null;
    this.questId = 0;
    this.fieldId = (string) null;
    this.fieldMapId = 0;
    this.isHost = false;
    this.arenaId = 0;
    this.UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
  }

  public void ToArena(int arenaId)
  {
    this.partyId = (string) null;
    this.questId = 0;
    this.fieldId = (string) null;
    this.fieldMapId = 0;
    this.isHost = false;
    this.arenaId = arenaId;
  }

  public LoungeMemberStatus.MEMBER_STATUS GetStatus()
  {
    if (this.questId > 0)
      return this.fieldMapId > 0 ? LoungeMemberStatus.MEMBER_STATUS.QUEST : LoungeMemberStatus.MEMBER_STATUS.QUEST_READY;
    if (this.arenaId > 0)
      return LoungeMemberStatus.MEMBER_STATUS.ARENA;
    return this.fieldMapId > 0 ? LoungeMemberStatus.MEMBER_STATUS.FIELD : LoungeMemberStatus.MEMBER_STATUS.LOUNGE;
  }

  public void SetCopy(LoungeMemberStatus copyData)
  {
    this.userId = copyData.userId;
    this.partyId = copyData.partyId;
    this.questId = copyData.questId;
    this.fieldId = copyData.fieldId;
    this.fieldMapId = copyData.fieldMapId;
    this.isHost = copyData.isHost;
    this.UpdateLastExecTime(copyData.lastExecTime);
  }

  public void UpdateLastExecTime(DateTime time) => this.lastExecTime = time;

  public enum MEMBER_STATUS
  {
    LOUNGE,
    QUEST_READY,
    QUEST,
    FIELD,
    ARENA,
  }
}
