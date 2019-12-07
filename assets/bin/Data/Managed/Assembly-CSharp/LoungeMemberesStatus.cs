using System.Collections.Generic;
using System.Linq;

public class LoungeMemberesStatus
{
	private List<LoungeMemberStatus> memberes = new List<LoungeMemberStatus>();

	public LoungeMemberStatus this[int userId] => GetMemberData(userId);

	public LoungeMemberesStatus(LoungeModel.Lounge lounge)
	{
		memberes = lounge.slotInfos.Select((PartyModel.SlotInfo x) => new LoungeMemberStatus(x)).ToList();
	}

	public LoungeMemberesStatus(List<Party_Model_RegisterACK.UserInfo> data)
	{
		Set(data);
	}

	public LoungeMemberStatus GetMemberData(int userId)
	{
		LoungeMemberStatus loungeMemberStatus = memberes.FirstOrDefault((LoungeMemberStatus x) => x.userId == userId);
		if (loungeMemberStatus == null)
		{
			loungeMemberStatus = new LoungeMemberStatus(userId);
			memberes.Add(loungeMemberStatus);
		}
		return loungeMemberStatus;
	}

	public void Add(LoungeMemberStatus member)
	{
		LoungeMemberStatus memberData = GetMemberData(member.userId);
		if (memberData == null)
		{
			memberes.Add(memberData);
		}
		else
		{
			memberData.SetCopy(member);
		}
	}

	public void Remove(int userId)
	{
		LoungeMemberStatus memberData = GetMemberData(userId);
		if (memberData != null)
		{
			memberes.Remove(memberData);
		}
	}

	public void Set(List<Party_Model_RegisterACK.UserInfo> data)
	{
		if (data != null)
		{
			memberes = data.Select((Party_Model_RegisterACK.UserInfo x) => new LoungeMemberStatus(x)).ToList();
		}
	}

	public List<LoungeMemberStatus> GetAll()
	{
		return memberes;
	}

	public void SyncLoungeMember(LoungeModel.Lounge lounge)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		list = (from x in lounge.slotInfos
			where x.userInfo != null && !memberes.Any((LoungeMemberStatus m) => m.userId == x.userInfo.userId)
			select x.userInfo.userId).ToList();
		list2 = (from m in memberes
			where !lounge.slotInfos.Any((PartyModel.SlotInfo x) => x.userInfo != null && x.userInfo.userId == m.userId)
			select m into x
			select x.userId).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			memberes.Add(new LoungeMemberStatus(list[i]));
		}
		for (int j = 0; j < list2.Count; j++)
		{
			Remove(list2[j]);
		}
	}

	public void SyncPartyMember(PartyModel.Party party)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		list = (from x in party.slotInfos
			where x.userInfo != null && !memberes.Any((LoungeMemberStatus m) => m.userId == x.userInfo.userId)
			select x.userInfo.userId).ToList();
		list2 = (from m in memberes
			where !party.slotInfos.Any((PartyModel.SlotInfo x) => x.userInfo != null && x.userInfo.userId == m.userId)
			select m into x
			select x.userId).ToList();
		if (!list.IsNullOrEmpty())
		{
			for (int i = 0; i < list.Count; i++)
			{
				memberes.Add(new LoungeMemberStatus(list[i]));
			}
		}
		if (!list2.IsNullOrEmpty())
		{
			for (int j = 0; j < list2.Count; j++)
			{
				Remove(list2[j]);
			}
		}
	}
}
