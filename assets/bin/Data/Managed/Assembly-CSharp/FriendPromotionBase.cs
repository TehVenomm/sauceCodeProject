// Decompiled with JetBrains decompiler
// Type: FriendPromotionBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

#nullable disable
public class FriendPromotionBase : GameSection
{
  private string linkMessage;
  private PromotionInfo promotionInfo;

  public override void Initialize()
  {
    FriendFollowLinkResult followLinkResult = MonoBehaviourSingleton<FriendManager>.I.followLinkResult;
    if (followLinkResult == null)
    {
      base.Initialize();
    }
    else
    {
      this.promotionInfo = followLinkResult.promotionInfo;
      string text = this.sectionData.GetText("TITLE");
      this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_TITLE, text);
      this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_TITLE_SHADOW, text);
      this.SetSuccessNum(this.promotionInfo);
      this.SetReceivedNum(this.promotionInfo);
      this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_DETAIL, this.sectionData.GetText("DETAIL"));
      bool isPromotionEvent = this.promotionInfo.isPromotionEvent;
      this.SetActive((Enum) FriendPromotionBase.UI.OBJ_SNS_AREA, isPromotionEvent);
      this.SetActive((Enum) FriendPromotionBase.UI.LBL_CANT_INVITE, !isPromotionEvent);
      this.linkMessage = string.Format(followLinkResult.message, (object) followLinkResult.link);
      this.linkMessage = this.linkMessage.Replace("<BR>", "\n");
      this.StartCoroutine(this.LoadTopBanner(this.promotionInfo.promotionBannerId));
      base.Initialize();
    }
  }

  private void SetSuccessNum(PromotionInfo promotionInfo)
  {
    string text1 = this.sectionData.GetText("SUCCESS_TITLE");
    string text2 = this.sectionData.GetText("PEOPLE");
    this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_SUCCESS_NAME, text1);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(promotionInfo.promotionCnt.ToString());
    stringBuilder.Append("/");
    stringBuilder.Append(promotionInfo.promotionMaxCnt.ToString());
    stringBuilder.Append(text2);
    this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_SUCCESS_NUM, stringBuilder.ToString());
  }

  private void SetReceivedNum(PromotionInfo promotionInfo)
  {
    string text1 = this.sectionData.GetText("RECEIVED_TITLE");
    string text2 = this.sectionData.GetText("COUNT");
    this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_RECEIVED_NAME, text1);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(promotionInfo.promotionReceivedCnt.ToString());
    stringBuilder.Append("/");
    stringBuilder.Append(promotionInfo.promotionCnt.ToString());
    stringBuilder.Append(text2);
    this.SetLabelText((Enum) FriendPromotionBase.UI.LBL_RECEIVED_NUM, stringBuilder.ToString());
  }

  private IEnumerator LoadTopBanner(int bannerId)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    string promotionBannerImage = ResourceName.GetFriendPromotionBannerImage(bannerId);
    LoadObject lo_image = loadingQueue.Load(RESOURCE_CATEGORY.COMMON, promotionBannerImage);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (!Object.op_Equality(lo_image.loadedObject, (Object) null))
    {
      Texture loadedObject = lo_image.loadedObject as Texture;
      ((Component) this.GetCtrl((Enum) FriendPromotionBase.UI.SPR_BANNER)).GetComponent<UITexture>().mainTexture = loadedObject;
    }
  }

  private void OnQuery_LINE()
  {
    Native.OpenURL("https://line.naver.jp/R/msg/text/?" + WWW.EscapeURL(this.linkMessage, Encoding.UTF8));
  }

  private void OnQuery_TWITTER()
  {
    Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL($"{this.linkMessage} {this.sectionData.GetText("HASH_TAG")}"));
  }

  private void OnQuery_DETAIL() => GameSection.SetEventData((object) this.promotionInfo.newsUrl);

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
    LBL_CANT_INVITE,
  }
}
