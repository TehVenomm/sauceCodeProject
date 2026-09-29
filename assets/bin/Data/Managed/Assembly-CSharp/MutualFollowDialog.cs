// Decompiled with JetBrains decompiler
// Type: MutualFollowDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

#nullable disable
public class MutualFollowDialog : GameSection
{
  private string linkMessage;
  private FriendFollowLinkResult followLinkResult;
  private const string MUTUAL_FOLLOW_BANNER_NAME = "IMG_00000001";
  private LoadingQueue loadQueue;

  public override void Initialize()
  {
    this.followLinkResult = MonoBehaviourSingleton<FriendManager>.I.followLinkResult;
    this.SetLabelText((Enum) MutualFollowDialog.UI.LBL_FOLLOWER_NUM, $"{this.followLinkResult.followCnt.ToString()}/{this.followLinkResult.followMaxCnt.ToString()}");
    string text1 = this.sectionData.GetText("REMAIN");
    string text2 = this.sectionData.GetText("PEOPLE");
    this.SetLabelText((Enum) MutualFollowDialog.UI.LBL_REMAIN_NUM, $"{text1} {this.followLinkResult.remainedCampaignNum.ToString()} {text2}");
    string text3;
    if (this.followLinkResult.remainedLoungeFirstMetNum < 0)
      text3 = this.sectionData.GetText("NON_CAMPAIN");
    else
      text3 = $"{text1} {this.followLinkResult.remainedLoungeFirstMetNum.ToString()} {text2}";
    this.SetLabelText((Enum) MutualFollowDialog.UI.LBL_LOUNGE_REMAIN_NUM, text3);
    this.linkMessage = string.Format(this.followLinkResult.message, (object) this.followLinkResult.link);
    this.linkMessage = this.linkMessage.Replace("<BR>", "\n");
    if (!MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
    {
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_AREA, true);
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_AREA2, false);
      this.SetActive((Enum) MutualFollowDialog.UI.LBL_SERVICE_MESSAGE, false);
      this.SetActive((Enum) MutualFollowDialog.UI.LBL_INVITE, true);
      this.StartCoroutine(this.LoadTopBanner());
    }
    else
    {
      this.SetActive((Enum) MutualFollowDialog.UI.LBL_INVITE, false);
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_AREA, false);
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_AREA2, false);
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_LINE_ROOT, false);
      this.SetActive((Enum) MutualFollowDialog.UI.OBJ_TWITTER_ROOT, false);
      this.SetActive((Enum) MutualFollowDialog.UI.BTN_DETAIL, false);
      this.SetActive((Enum) MutualFollowDialog.UI.LBL_SERVICE_MESSAGE, true);
      this.SetLabelText((Enum) MutualFollowDialog.UI.LBL_SERVICE_MESSAGE, this.sectionData.GetText("SERVICE_LIMITED"));
    }
    base.Initialize();
  }

  private IEnumerator LoadTopBanner()
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = this.loadQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001");
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (!Object.op_Equality(lo_image.loadedObject, (Object) null))
    {
      Texture loadedObject = lo_image.loadedObject as Texture;
      ((Component) this.GetCtrl((Enum) MutualFollowDialog.UI.SPR_MUTUAL_FOLLOW_BANNER)).GetComponent<UITexture>().mainTexture = loadedObject;
    }
  }

  private void OnQuery_LINE()
  {
    Native.OpenURL("https://line.naver.jp/R/msg/text/?" + WWW.EscapeURL(this.linkMessage, Encoding.UTF8));
  }

  private void OnQuery_TWITTER()
  {
    Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(this.linkMessage));
  }

  private void OnQuery_FACEBOOK()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook)
    {
      if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
        return;
      GameSection.StayEvent();
      MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission((Action<bool, string>) ((success, r) => GameSection.ResumeEvent(success)));
    }
    else
      GameSection.ChangeEvent("FACEBOOK_CONNECT");
  }

  private void OnQuery_DETAIL() => GameSection.SetEventData((object) this.followLinkResult.linkUrl);

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
    LBL_INVITE,
  }
}
