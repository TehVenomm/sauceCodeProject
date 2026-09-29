// Decompiled with JetBrains decompiler
// Type: LoungeMemberesStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class LoungeMemberesStatus
{
  private List<LoungeMemberStatus> memberes = new List<LoungeMemberStatus>();

  public LoungeMemberesStatus(LoungeModel.Lounge lounge)
  {
    this.memberes = lounge.slotInfos.Select<PartyModel.SlotInfo, LoungeMemberStatus>((Func<PartyModel.SlotInfo, LoungeMemberStatus>) (x => new LoungeMemberStatus(x))).ToList<LoungeMemberStatus>();
  }

  public LoungeMemberesStatus(List<Party_Model_RegisterACK.UserInfo> data) => this.Set(data);

  public LoungeMemberStatus this[int userId] => this.GetMemberData(userId);

  public LoungeMemberStatus GetMemberData(int userId)
  {
    LoungeMemberStatus memberData = this.memberes.FirstOrDefault<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (x => x.userId == userId));
    if (memberData == null)
    {
      memberData = new LoungeMemberStatus(userId);
      this.memberes.Add(memberData);
    }
    return memberData;
  }

  public void Add(LoungeMemberStatus member)
  {
    LoungeMemberStatus memberData = this.GetMemberData(member.userId);
    if (memberData == null)
      this.memberes.Add(memberData);
    else
      memberData.SetCopy(member);
  }

  public void Remove(int userId)
  {
    LoungeMemberStatus memberData = this.GetMemberData(userId);
    if (memberData == null)
      return;
    this.memberes.Remove(memberData);
  }

  public void Set(List<Party_Model_RegisterACK.UserInfo> data)
  {
    if (data == null)
      return;
    this.memberes = data.Select<Party_Model_RegisterACK.UserInfo, LoungeMemberStatus>((Func<Party_Model_RegisterACK.UserInfo, LoungeMemberStatus>) (x => new LoungeMemberStatus(x))).ToList<LoungeMemberStatus>();
  }

  public List<LoungeMemberStatus> GetAll() => this.memberes;

  public void SyncLoungeMember(LoungeModel.Lounge lounge)
  {
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<int> list1 = lounge.slotInfos.Where<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (x => x.userInfo != null && !this.memberes.Any<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (m => m.userId == x.userInfo.userId)))).Select<PartyModel.SlotInfo, int>((Func<PartyModel.SlotInfo, int>) (x => x.userInfo.userId)).ToList<int>();
    List<int> list2 = this.memberes.Where<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (m => !lounge.slotInfos.Any<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (x => x.userInfo != null && x.userInfo.userId == m.userId)))).Select<LoungeMemberStatus, int>((Func<LoungeMemberStatus, int>) (x => x.userId)).ToList<int>();
    for (int index = 0; index < list1.Count; ++index)
      this.memberes.Add(new LoungeMemberStatus(list1[index]));
    for (int index = 0; index < list2.Count; ++index)
      this.Remove(list2[index]);
  }

  public void SyncPartyMember(PartyModel.Party party)
  {
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    List<int> list1 = party.slotInfos.Where<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (x => x.userInfo != null && !this.memberes.Any<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (m => m.userId == x.userInfo.userId)))).Select<PartyModel.SlotInfo, int>((Func<PartyModel.SlotInfo, int>) (x => x.userInfo.userId)).ToList<int>();
    List<int> list2 = this.memberes.Where<LoungeMemberStatus>((Func<LoungeMemberStatus, bool>) (m => !party.slotInfos.Any<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (x => x.userInfo != null && x.userInfo.userId == m.userId)))).Select<LoungeMemberStatus, int>((Func<LoungeMemberStatus, int>) (x => x.userId)).ToList<int>();
    if (!list1.IsNullOrEmpty<int>())
    {
      for (int index = 0; index < list1.Count; ++index)
        this.memberes.Add(new LoungeMemberStatus(list1[index]));
    }
    if (list2.IsNullOrEmpty<int>())
      return;
    for (int index = 0; index < list2.Count; ++index)
      this.Remove(list2[index]);
  }
}
