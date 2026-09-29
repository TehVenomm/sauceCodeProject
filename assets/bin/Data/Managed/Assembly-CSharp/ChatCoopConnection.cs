// Decompiled with JetBrains decompiler
// Type: ChatCoopConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatCoopConnection : IChatConnection
{
  private bool established;

  public event ChatRoom.OnJoin onJoin;

  public event ChatRoom.OnJoinClan onJoinClan;

  public event ChatRoom.OnReceiveText onReceiveText;

  public event ChatRoom.OnReceiveStamp onReceiveStamp;

  public event ChatRoom.OnReceiveNotification onReceiveNotification;

  public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

  public event ChatRoom.OnDisconnect onDisconnect;

  public event ChatRoom.OnReceiveText onReceivePrivateText;

  public event ChatRoom.OnReceiveStamp onReceivePrivateStamp;

  public bool isEstablished => this.established;

  public void Connect() => this.established = true;

  public void Disconnect(System.Action onFinished = null)
  {
    this.established = false;
    if (onFinished == null)
      return;
    onFinished();
  }

  public void Join(int roomNo, string userName)
  {
    this.established = true;
    if (this.onJoin == null)
      return;
    this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
  }

  public void SendText(string message)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
    }
    else
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (Object.op_Equality((Object) self, (Object) null) || !((Component) self).gameObject.activeInHierarchy || !((Behaviour) self.uiPlayerStatusGizmo).isActiveAndEnabled)
      {
        MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
      }
      else
      {
        MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSay(message);
        if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsCoopNone() && QuestManager.IsValidInGameExplore())
          MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), message);
      }
    }
    this.OnReceiveMessage(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, message);
  }

  public void SendStamp(int stampId)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      if (QuestManager.IsValidInGameExplore())
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
      else
        MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), stampId);
    }
    else
    {
      Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      if (Object.op_Equality((Object) self, (Object) null) || !((Component) self).gameObject.activeInHierarchy || !((Behaviour) self.uiPlayerStatusGizmo).isActiveAndEnabled)
      {
        if (QuestManager.IsValidInGameExplore())
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
        else
          MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(MonoBehaviourSingleton<CoopManager>.I.GetSelfID(), stampId);
      }
      else
      {
        MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSayStamp(stampId);
        if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsCoopNone() && QuestManager.IsValidInGameExplore())
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stampId);
      }
    }
    this.OnReceiveStamp(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, stampId);
  }

  public void SendPrivateText(string target_id, string message)
  {
  }

  public void SendPrivateStamp(string target_id, int stampId)
  {
  }

  public void OnReceiveMessage(int userId, string userName, string message, string chatItemId = "")
  {
    if (!this.isEstablished || this.onReceiveText == null)
      return;
    this.onReceiveText(userId, userName, message, chatItemId);
  }

  public void OnReceiveStamp(int userId, string userName, int stampId, string chatItemId = "")
  {
    if (!this.isEstablished || this.onReceiveStamp == null)
      return;
    this.onReceiveStamp(userId, userName, stampId, chatItemId);
  }

  public void OnReceiveNotification(string message, string chatItemId = "")
  {
    if (!this.isEstablished || this.onReceiveNotification == null)
      return;
    this.onReceiveNotification(message, chatItemId);
  }
}
