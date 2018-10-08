using System;
using System.Collections.Generic;
using System.Linq;

public class LoungeMemberesStatus
{
	private List<LoungeMemberStatus> memberes = new List<LoungeMemberStatus>();

	public LoungeMemberStatus this[int userId]
	{
		get
		{
			return GetMemberData(userId);
		}
	}

	public unsafe LoungeMemberesStatus(LoungeModel.Lounge lounge)
	{
		List<LoungeModel.SlotInfo> slotInfos = lounge.slotInfos;
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = new Func<LoungeModel.SlotInfo, LoungeMemberStatus>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		memberes = slotInfos.Select<LoungeModel.SlotInfo, LoungeMemberStatus>(_003C_003Ef__am_0024cache1).ToList();
	}

	public LoungeMemberesStatus(List<Party_Model_RegisterACK.UserInfo> data)
	{
		Set(data);
	}

	public unsafe LoungeMemberStatus GetMemberData(int userId)
	{
		_003CGetMemberData_003Ec__AnonStorey66A _003CGetMemberData_003Ec__AnonStorey66A;
		LoungeMemberStatus loungeMemberStatus = memberes.FirstOrDefault(new Func<LoungeMemberStatus, bool>((object)_003CGetMemberData_003Ec__AnonStorey66A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (object.ReferenceEquals(null, loungeMemberStatus))
		{
			loungeMemberStatus = new LoungeMemberStatus(userId);
			memberes.Add(loungeMemberStatus);
		}
		return loungeMemberStatus;
	}

	public void Add(LoungeMemberStatus member)
	{
		LoungeMemberStatus memberData = GetMemberData(member.userId);
		if (object.ReferenceEquals(null, memberData))
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
		if (!object.ReferenceEquals(null, memberData))
		{
			memberes.Remove(memberData);
		}
	}

	public unsafe void Set(List<Party_Model_RegisterACK.UserInfo> data)
	{
		if (!object.ReferenceEquals(null, data))
		{
			if (_003C_003Ef__am_0024cache2 == null)
			{
				_003C_003Ef__am_0024cache2 = new Func<Party_Model_RegisterACK.UserInfo, LoungeMemberStatus>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			memberes = data.Select<Party_Model_RegisterACK.UserInfo, LoungeMemberStatus>(_003C_003Ef__am_0024cache2).ToList();
		}
	}

	public List<LoungeMemberStatus> GetAll()
	{
		return memberes;
	}

	public unsafe void SyncLoungeMember(LoungeModel.Lounge lounge)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		_003CSyncLoungeMember_003Ec__AnonStorey66C _003CSyncLoungeMember_003Ec__AnonStorey66C;
		IEnumerable<LoungeModel.SlotInfo> source = lounge.slotInfos.Where(new Func<LoungeModel.SlotInfo, bool>((object)_003CSyncLoungeMember_003Ec__AnonStorey66C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (_003C_003Ef__am_0024cache3 == null)
		{
			_003C_003Ef__am_0024cache3 = new Func<LoungeModel.SlotInfo, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		list = source.Select<LoungeModel.SlotInfo, int>(_003C_003Ef__am_0024cache3).ToList();
		IEnumerable<LoungeMemberStatus> source2 = memberes.Where(new Func<LoungeMemberStatus, bool>((object)_003CSyncLoungeMember_003Ec__AnonStorey66C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (_003C_003Ef__am_0024cache4 == null)
		{
			_003C_003Ef__am_0024cache4 = new Func<LoungeMemberStatus, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		list2 = source2.Select<LoungeMemberStatus, int>(_003C_003Ef__am_0024cache4).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			memberes.Add(new LoungeMemberStatus(list[i]));
		}
		for (int j = 0; j < list2.Count; j++)
		{
			Remove(list2[j]);
		}
	}
}
