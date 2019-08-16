using Network;
using UnityEngine;

public class ClanAcceptListItem : UIBehaviour
{
	private enum UI
	{
		LBL_NAME,
		LBL_LEVEL,
		LBL_COMMENT,
		LBL_HP,
		LBL_ATK,
		LBL_DEF,
		BTN_ACCEPT,
		BTN_REJECT
	}

	public void Setup(Transform t, int index, FriendCharaInfo info)
	{
		SetEvent(t, "DETAIL", index);
		SetEvent(t, UI.BTN_ACCEPT, "ACCEPT", index);
		SetEvent(t, UI.BTN_REJECT, "REJECT", index);
		SetLabelText(t, UI.LBL_NAME, info.name);
		SetLabelText(t, UI.LBL_LEVEL, string.Format("Lv.{0,3:D}", info.level));
		SetLabelText(t, UI.LBL_COMMENT, info.comment);
		SetLabelText(t, UI.LBL_HP, info.hp.ToString());
		SetLabelText(t, UI.LBL_ATK, info.atk.ToString());
		SetLabelText(t, UI.LBL_DEF, info.def.ToString());
	}
}
