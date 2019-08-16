using System;
using System.Text;
using UnityEngine;

public class QuestRoomInviteDialog : GameSection
{
	protected enum UI
	{
		LBL_SNS,
		OBJ_LINE_ROOT,
		OBJ_TWITTER_ROOT,
		LBL_SERVICE,
		LBL_INVITE
	}

	protected string inviteMessage = string.Empty;

	protected string helpLinkURL = string.Empty;

	public override void Initialize()
	{
		string text = MonoBehaviourSingleton<PartyManager>.I.GetInviteMessage();
		helpLinkURL = MonoBehaviourSingleton<PartyManager>.I.GetInviteHelpURL();
		inviteMessage = text.Replace("<BR>", "\n").Replace("<br>", "\n");
		InitializeBase();
	}

	protected void InitializeBase()
	{
		Debug.Log((object)("<color=blue>InitializeBase  </color>" + MonoBehaviourSingleton<AccountManager>.I.usageLimitMode + " ; " + string.Format(base.sectionData.GetText("STR_SERVICE"))));
		if (MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
		{
			SetActive((Enum)UI.LBL_INVITE, is_visible: false);
			SetActive((Enum)UI.LBL_SNS, is_visible: false);
			SetActive((Enum)UI.OBJ_LINE_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_TWITTER_ROOT, is_visible: false);
			SetActive((Enum)UI.LBL_SERVICE, is_visible: true);
			SetLabelText((Enum)UI.LBL_SERVICE, string.Format(base.sectionData.GetText("STR_SERVICE")));
		}
		else
		{
			SetActive((Enum)UI.LBL_SNS, is_visible: false);
			SetActive((Enum)UI.LBL_SERVICE, is_visible: false);
			SetActive((Enum)UI.LBL_INVITE, is_visible: true);
		}
		base.Initialize();
	}

	protected void OnQuery_LINE()
	{
		Native.OpenURL("https://line.naver.jp/R/msg/text/?" + WWW.EscapeURL(inviteMessage, Encoding.UTF8));
	}

	protected void OnQuery_TWITTER()
	{
		Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(inviteMessage + " #DragonProject"));
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(helpLinkURL);
	}
}
