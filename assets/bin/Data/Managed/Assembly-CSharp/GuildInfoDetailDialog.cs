using Network;
using System;
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		_info = (array[0] as GuildStatisticInfo);
		_clanId = (int)array[1];
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool is_finish = false;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(_clanId, delegate(bool success, GuildMemberListModel ret)
		{
			((_003CDoInitialize_003Ec__Iterator4A)/*Error near IL_0038: stateMachine*/)._003C_003Ef__this.members = new List<FriendCharaInfo>(ret.result.list);
			((_003CDoInitialize_003Ec__Iterator4A)/*Error near IL_0038: stateMachine*/)._003C_003Ef__this.members.Remove(((_003CDoInitialize_003Ec__Iterator4A)/*Error near IL_0038: stateMachine*/)._003C_003Ef__this.members.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
			((_003CDoInitialize_003Ec__Iterator4A)/*Error near IL_0038: stateMachine*/)._003C_003Ef__this._clanMasterUserId = ret.result.clanMasterId;
			((_003CDoInitialize_003Ec__Iterator4A)/*Error near IL_0038: stateMachine*/)._003Cis_finish_003E__0 = true;
		});
		while (!is_finish)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_HUNTER, string.Format(base.sectionData.GetText("TEXT_HUNTER"), _info.currentMem, _info.memCap));
		SetGrid(UI.GRD_LIST, "GuildInfoDetailListItem", members.Count, true, delegate(int i, Transform t, bool b)
		{
			FriendCharaInfo friendCharaInfo = members[i];
			string str = (friendCharaInfo.userId != _clanMasterUserId) ? string.Empty : string.Format("({0})", base.sectionData.GetText("TEXT_MASTER"));
			SetLabelText(t, UI.LBL_NAME, friendCharaInfo.name + str);
			SetEvent(t, "DETAIL", friendCharaInfo);
		});
	}
}
