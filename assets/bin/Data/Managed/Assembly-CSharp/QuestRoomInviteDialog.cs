using System.Text;
using UnityEngine;

public class QuestRoomInviteDialog : GameSection
{
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
