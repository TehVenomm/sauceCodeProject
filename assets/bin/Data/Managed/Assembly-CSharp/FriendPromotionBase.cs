using Network;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class FriendPromotionBase : GameSection
{
	private enum UI
	{
		LBL_TITLE,
		LBL_TITLE_SHADOW,
		LBL_MESSAGE,
		LBL_DETAIL,
		LBL_SUCCESS_NUM,
		LBL_SUCCESS_NAME,
		LBL_RECEIVED_NUM,
		LBL_RECEIVED_NAME,
		OBJ_TWITTER_ROOT,
		OBJ_LINE_ROOT,
		OBJ_NOT_CAMPAIN_ROOT,
		SPR_BANNER,
		OBJ_SNS_AREA,
		LBL_CANT_INVITE
	}

	private string linkMessage;

	private PromotionInfo promotionInfo;

	public override void Initialize()
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		FriendFollowLinkResult followLinkResult = MonoBehaviourSingleton<FriendManager>.I.followLinkResult;
		if (followLinkResult == null)
		{
			base.Initialize();
		}
		else
		{
			promotionInfo = followLinkResult.promotionInfo;
			string text = base.sectionData.GetText("TITLE");
			SetLabelText((Enum)UI.LBL_TITLE, text);
			SetLabelText((Enum)UI.LBL_TITLE_SHADOW, text);
			SetSuccessNum(promotionInfo);
			SetReceivedNum(promotionInfo);
			string text2 = base.sectionData.GetText("DETAIL");
			SetLabelText((Enum)UI.LBL_DETAIL, text2);
			bool isPromotionEvent = promotionInfo.isPromotionEvent;
			SetActive((Enum)UI.OBJ_SNS_AREA, isPromotionEvent);
			SetActive((Enum)UI.LBL_CANT_INVITE, !isPromotionEvent);
			string message = followLinkResult.message;
			linkMessage = string.Format(message, followLinkResult.link);
			linkMessage = linkMessage.Replace("<BR>", "\n");
			this.StartCoroutine(LoadTopBanner(promotionInfo.promotionBannerId));
			base.Initialize();
		}
	}

	private void SetSuccessNum(PromotionInfo promotionInfo)
	{
		string text = base.sectionData.GetText("SUCCESS_TITLE");
		string text2 = base.sectionData.GetText("PEOPLE");
		SetLabelText((Enum)UI.LBL_SUCCESS_NAME, text);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(promotionInfo.promotionCnt.ToString());
		stringBuilder.Append("/");
		stringBuilder.Append(promotionInfo.promotionMaxCnt.ToString());
		stringBuilder.Append(text2);
		SetLabelText((Enum)UI.LBL_SUCCESS_NUM, stringBuilder.ToString());
	}

	private void SetReceivedNum(PromotionInfo promotionInfo)
	{
		string text = base.sectionData.GetText("RECEIVED_TITLE");
		string text2 = base.sectionData.GetText("COUNT");
		SetLabelText((Enum)UI.LBL_RECEIVED_NAME, text);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(promotionInfo.promotionReceivedCnt.ToString());
		stringBuilder.Append("/");
		stringBuilder.Append(promotionInfo.promotionCnt.ToString());
		stringBuilder.Append(text2);
		SetLabelText((Enum)UI.LBL_RECEIVED_NUM, stringBuilder.ToString());
	}

	private IEnumerator LoadTopBanner(int bannerId)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		string bannerName = ResourceName.GetFriendPromotionBannerImage(bannerId);
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.COMMON, bannerName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (!(lo_image.loadedObject == null))
		{
			Texture bannerImg = lo_image.loadedObject as Texture;
			Transform banner = GetCtrl(UI.SPR_BANNER);
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
		string text = base.sectionData.GetText("HASH_TAG");
		Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(linkMessage + " " + text));
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(promotionInfo.newsUrl);
	}
}
