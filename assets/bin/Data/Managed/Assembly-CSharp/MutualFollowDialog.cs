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
		OBJ_FIRST_MET_LOUNGE,
		OBJ_AREA,
		OBJ_AREA2,
		OBJ_TWITTER_ROOT,
		OBJ_LINE_ROOT,
		BTN_DETAIL,
		LBL_SERVICE_MESSAGE,
		LBL_INVITE
	}

	private string linkMessage;

	private FriendFollowLinkResult followLinkResult;

	private const string MUTUAL_FOLLOW_BANNER_NAME = "IMG_00000001";

	private LoadingQueue loadQueue;

	public override void Initialize()
	{
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
		if (!MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
		{
			SetActive((Enum)UI.OBJ_AREA, is_visible: true);
			SetActive((Enum)UI.OBJ_AREA2, is_visible: false);
			SetActive((Enum)UI.LBL_SERVICE_MESSAGE, is_visible: false);
			SetActive((Enum)UI.LBL_INVITE, is_visible: true);
			this.StartCoroutine(LoadTopBanner());
		}
		else
		{
			SetActive((Enum)UI.LBL_INVITE, is_visible: false);
			SetActive((Enum)UI.OBJ_AREA, is_visible: false);
			SetActive((Enum)UI.OBJ_AREA2, is_visible: false);
			SetActive((Enum)UI.OBJ_LINE_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_TWITTER_ROOT, is_visible: false);
			SetActive((Enum)UI.BTN_DETAIL, is_visible: false);
			SetActive((Enum)UI.LBL_SERVICE_MESSAGE, is_visible: true);
			SetLabelText((Enum)UI.LBL_SERVICE_MESSAGE, base.sectionData.GetText("SERVICE_LIMITED"));
		}
		base.Initialize();
	}

	private IEnumerator LoadTopBanner()
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001");
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
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
					GameSection.ResumeEvent(success);
				});
			}
		}
		else
		{
			GameSection.ChangeEvent("FACEBOOK_CONNECT");
		}
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(followLinkResult.linkUrl);
	}
}
