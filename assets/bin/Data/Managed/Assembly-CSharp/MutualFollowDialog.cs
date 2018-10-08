using Network;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class MutualFollowDialog : GameSection
{
	private enum UI
	{
		SPR_MUTUAL_FOLLOW_BANNER,
		LBL_FOLLOWER_NUM,
		LBL_REMAIN_NUM,
		LBL_MESSAGE,
		LBL_LOUNGE_REMAIN_NUM,
		OBJ_FIRST_MET_LOUNGE
	}

	private const string MUTUAL_FOLLOW_BANNER_NAME = "IMG_00000001";

	private string linkMessage;

	private FriendFollowLinkResult followLinkResult;

	private LoadingQueue loadQueue;

	public override void Initialize()
	{
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		followLinkResult = MonoBehaviourSingleton<FriendManager>.I.followLinkResult;
		SetLabelText((Enum)UI.LBL_FOLLOWER_NUM, followLinkResult.followCnt.ToString() + "/" + followLinkResult.followMaxCnt.ToString());
		string text = base.sectionData.GetText("REMAIN");
		string text2 = base.sectionData.GetText("PEOPLE");
		SetLabelText((Enum)UI.LBL_REMAIN_NUM, text + " " + followLinkResult.remainedCampaignNum.ToString() + " " + text2);
		string empty = string.Empty;
		SetLabelText(text: (followLinkResult.remainedLoungeFirstMetNum >= 0) ? (text + " " + followLinkResult.remainedLoungeFirstMetNum.ToString() + " " + text2) : base.sectionData.GetText("NON_CAMPAIN"), label_enum: UI.LBL_LOUNGE_REMAIN_NUM);
		string message = followLinkResult.message;
		linkMessage = string.Format(message, followLinkResult.link);
		linkMessage = linkMessage.Replace("<BR>", "\n");
		this.StartCoroutine(LoadTopBanner());
		base.Initialize();
	}

	private IEnumerator LoadTopBanner()
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001", false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (!(lo_image.loadedObject == null))
		{
			Texture bannerImg = lo_image.loadedObject as Texture;
			Transform banner = GetCtrl(UI.SPR_MUTUAL_FOLLOW_BANNER);
			UITexture uiTexture = banner.GetComponent<UITexture>();
			uiTexture.mainTexture = bannerImg;
		}
	}

	private void OnQuery_LINE()
	{
		Native.OpenURL("https://line.naver.jp/R/msg/text/?" + WWW.EscapeURL(linkMessage, Encoding.UTF8));
	}

	private void OnQuery_TWITTER()
	{
		Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(linkMessage));
	}

	private void OnQuery_FACEBOOK()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook)
		{
			if (!MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission(delegate(bool success, string r)
				{
					GameSection.ResumeEvent(success, null);
				});
			}
		}
		else
		{
			GameSection.ChangeEvent("FACEBOOK_CONNECT", null);
		}
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(followLinkResult.linkUrl);
	}
}
