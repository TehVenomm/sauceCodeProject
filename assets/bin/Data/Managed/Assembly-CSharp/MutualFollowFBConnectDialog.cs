// Decompiled with JetBrains decompiler
// Type: MutualFollowFBConnectDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class MutualFollowFBConnectDialog : GameSection
{
  private const string MUTUAL_FOLLOW_BANNER_NAME = "IMG_00000001";

  public override void Initialize()
  {
    this.StartCoroutine(this.LoadTopBanner());
    base.Initialize();
  }

  private IEnumerator LoadTopBanner()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = loadingQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (Object.op_Equality(lo_image.loadedObject, (Object) null))
      yield return (object) null;
    Texture loadedObject = lo_image.loadedObject as Texture;
    ((Component) this.GetCtrl((Enum) MutualFollowFBConnectDialog.UI.SPR_MUTUAL_FOLLOW_BANNER)).GetComponent<UITexture>().mainTexture = loadedObject;
  }

  private void OnQuery_CONNECT()
  {
    GameSection.StayEvent();
    if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
      this._SendRegistLinkFacebook();
    else
      MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission((Action<bool, string>) ((success, s) =>
      {
        if (success)
          this._SendRegistLinkFacebook();
        else
          GameSection.ResumeEvent(success);
      }));
  }

  private void _SendRegistLinkFacebook()
  {
    MonoBehaviourSingleton<AccountManager>.I.SendRegistLinkFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, (Action<bool, RegistLinkFacebookModel>) ((success, ret) =>
    {
      if (success)
      {
        GameSection.ResumeEvent(success);
      }
      else
      {
        if (ret.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
        {
          GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", (object) ret.existInfo);
          success = true;
        }
        GameSection.ResumeEvent(success);
      }
    }));
  }

  private enum UI
  {
    SPR_MUTUAL_FOLLOW_BANNER,
    BTN_CONNECT,
  }
}
