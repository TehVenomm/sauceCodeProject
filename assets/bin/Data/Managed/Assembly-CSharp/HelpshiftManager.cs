// Decompiled with JetBrains decompiler
// Type: HelpshiftManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Helpshift;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HelpshiftManager : MonoBehaviourSingleton<HelpshiftManager>
{
  private HelpshiftWrapper helpshift;
  private HelpshiftSdk _support;
  protected System.Action OnUpdateCountCallBack;
  private bool isInit;
  private bool isOpenConversation;
  private Coroutine updateCount;

  public int countMess { get; private set; }

  public void OnStart(string account_id, string email, string player_name)
  {
    Debug.Log((object) "INIT HELPSHIFT");
    this.StartCoroutine(this.StartInit(account_id, email, player_name));
  }

  public void Show(
    string account_id,
    string player_name,
    string dpro_id,
    bool is_Vip,
    string server,
    string email = "")
  {
    if (!this.isInit)
      this.Init(account_id, email, player_name);
    this.helpshift.ShowCustomContactUs(account_id, player_name, dpro_id, server, is_Vip);
    this.countMess = 0;
  }

  private IEnumerator StartInit(string account_id, string email, string player_name)
  {
    yield return (object) new WaitForSeconds(0.5f);
    this.Init(account_id, email, player_name);
  }

  private void Init(string account_id, string email, string player_name)
  {
    this.helpshift = new HelpshiftWrapper(((Object) ((Component) this).transform).name, new HelpshiftUser.Builder(account_id, "").setName(player_name).build());
    this._support = this.helpshift.Support;
    this._support.registerDelegates();
    this.helpshift.Start();
    this.isInit = true;
    Debug.Log((object) ("Helpshift Init " + (this.helpshift != null ? "succeed" : "fail")));
  }

  public void RegisterDelegate(System.Action callback)
  {
    this.OnUpdateCountCallBack = callback;
    if (this._support == null)
      return;
    this._support.requestUnreadMessagesCount(true);
  }

  public void UnRegisterDelegate(System.Action callback)
  {
  }

  private IEnumerator RequestMessage()
  {
    while (this.isOpenConversation)
    {
      yield return (object) new WaitForSeconds(30f);
      this._support.requestUnreadMessagesCount(true);
    }
  }

  private void OnDestroy()
  {
  }

  public void Logout()
  {
    if (this.helpshift == null)
      return;
    this.helpshift.Logout();
  }

  public void updateMetaData(string nothing)
  {
    this._support.updateMetaData(new Dictionary<string, object>()
    {
      {
        "user-level",
        (object) "21"
      },
      {
        "hs-tags",
        (object) new string[1]{ "Tag-1" }
      }
    });
  }

  public void helpshiftSessionBegan(string message)
  {
    if (!this.isOpenConversation)
      return;
    this.StopCoroutine(this.updateCount);
  }

  public void helpshiftSessionEnded(string message)
  {
    this._support.requestUnreadMessagesCount(true);
    this.updateCount = this.StartCoroutine(this.RequestMessage());
  }

  public void alertToRateAppAction(string result)
  {
  }

  public void didReceiveNotificationCount(string count)
  {
  }

  public void didReceiveInAppNotificationCount(string count)
  {
  }

  public void conversationEnded()
  {
    this.isOpenConversation = false;
    this._support.requestUnreadMessagesCount(true);
  }

  public void didReceiveUnreadMessagesCount(string count)
  {
    this.helpshift.SetUnReadMessCount(count);
    this.countMess = count.ToInt32OrDefault();
    if (this.countMess < 0)
      this.countMess = 0;
    System.Action updateCountCallBack = this.OnUpdateCountCallBack;
    if (updateCountCallBack == null)
      return;
    updateCountCallBack();
  }

  public void didCheckIfConversationActive(string active)
  {
    if (this.isOpenConversation)
      return;
    this.isOpenConversation = true;
    this.updateCount = this.StartCoroutine(this.RequestMessage());
  }

  public void displayAttachmentFile(string path)
  {
    path.StartsWith("content://", StringComparison.OrdinalIgnoreCase);
  }

  public void newConversationStarted(string message)
  {
    if (this.isOpenConversation)
      return;
    this.isOpenConversation = true;
    this.updateCount = this.StartCoroutine(this.RequestMessage());
  }

  public void userRepliedToConversation(string newMessage)
  {
  }

  public void userCompletedCustomerSatisfactionSurvey(string json)
  {
  }

  public void authenticationFailed(string serializedJSONUserData)
  {
    HelpshiftJSONUtility.getHelpshiftUser(serializedJSONUserData);
    int authFailureReason = (int) HelpshiftJSONUtility.getAuthFailureReason(serializedJSONUserData);
  }
}
