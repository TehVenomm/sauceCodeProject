using Network;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildInfoDetailDialog : GameSection
{
	private enum UI
	{
		LBL_HUNTER,
		SCR_LIST,
		GRD_LIST,
		LBL_NAME
	}

	private List<FriendCharaInfo> members;

	private GuildStatisticInfo _info;

	private int _clanId;

	private int _clanMasterUserId;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		_info = (array[0] as GuildStatisticInfo);
		_clanId = (int)array[1];
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool is_finish = false;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(_clanId, delegate(bool success, GuildMemberListModel ret)
		{
			members = new List<FriendCharaInfo>(ret.result.list);
			members.Remove(members.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
			_clanMasterUserId = ret.result.clanMasterId;
			is_finish = true;
		});
		while (!is_finish)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_HUNTER, string.Format(base.sectionData.GetText("TEXT_HUNTER"), _info.currentMem, _info.memCap));
		SetGrid(UI.GRD_LIST, "GuildInfoDetailListItem", members.Count, reset: true, delegate(int i, Transform t, bool b)
		{
			FriendCharaInfo friendCharaInfo = members[i];
			string str = (friendCharaInfo.userId == _clanMasterUserId) ? string.Format("({0})", base.sectionData.GetText("TEXT_MASTER")) : "";
			SetLabelText(t, UI.LBL_NAME, friendCharaInfo.name + str);
			SetEvent(t, "DETAIL", friendCharaInfo);
		});
	}
}
