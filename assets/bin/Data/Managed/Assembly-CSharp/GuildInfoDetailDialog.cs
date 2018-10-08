using Network;
using System;
using System.Collections;
using System.Collections.Generic;
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

	private unsafe IEnumerator DoInitialize()
	{
		bool is_finish = false;
		MonoBehaviourSingleton<GuildManager>.I.SendMemberList(_clanId, new Action<bool, GuildMemberListModel>((object)/*Error near IL_0038: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!is_finish)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_HUNTER, string.Format(base.sectionData.GetText("TEXT_HUNTER"), _info.currentMem, _info.memCap));
		SetGrid(UI.GRD_LIST, "GuildInfoDetailListItem", members.Count, true, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
