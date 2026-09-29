// Decompiled with JetBrains decompiler
// Type: ChatState_ClanTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatState_ClanTab : ChatState
{
  private float updateTimer;

  public override void Enter(MainChat _manager)
  {
    base.Enter(_manager);
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
    {
      this.m_manager.UseNoClanBlock = false;
      this.updateTimer = (float) MonoBehaviourSingleton<ClanMatchingManager>.I.chatUpdateInterval;
      if (MonoBehaviourSingleton<ClanMatchingManager>.I.CachedMessageClanId != MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId)
        this.reloadChat();
    }
    else
    {
      this.updateTimer = 0.0f;
      this.m_manager.UseNoClanBlock = true;
      this.m_manager.CurrentData.Reset();
    }
    this.m_manager.IsDraging = false;
    this.m_manager.UpdateSendBlock();
    this.EndInitialize();
  }

  public override void Update(float _deltaTime)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || !MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      return;
    if (this.m_manager.UseNoClanBlock)
    {
      this.m_manager.UseNoClanBlock = false;
      this.m_manager.UpdateSendBlock();
    }
    this.updateTimer -= _deltaTime;
    if ((double) this.updateTimer > 0.0)
      return;
    if (this.m_manager.CurrentData.itemList.Count == 0)
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
    else if (this.m_manager.CurrentData.newestIndex >= 0)
    {
      ChatItem chatItem = this.m_manager.CurrentData.itemList[this.m_manager.CurrentData.newestIndex];
      if (((Component) chatItem).gameObject.activeSelf && chatItem.chatItemId == MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetLatestCacheId())
        MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItem.chatItemId);
    }
    int num = MonoBehaviourSingleton<ClanMatchingManager>.I.chatUpdateInterval;
    if (num < 1)
      num = 1;
    this.updateTimer = (float) num;
  }

  public override void Exit()
  {
    this.m_manager.UseNoClanBlock = false;
    this.m_manager.IsDraging = false;
    this.m_manager.UpdateSendBlock();
    base.Exit();
  }

  public override void OnDragAtTop(string chatItemId, float dragpower)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || !MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      return;
    if (this.m_manager.CurrentData.itemList.Count == 0)
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItemId);
    else
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetOldMessage(this.getDispatchNum(dragpower), chatItemId);
  }

  public override void OnDragAtBottom(string chatItemId, float dragpower)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || !MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      return;
    if (this.m_manager.CurrentData.itemList.Count == 0)
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100, chatItemId);
    else
      MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(this.getDispatchNum(dragpower), chatItemId);
  }

  public override void OnTapHeaderTab(MainChat.CHAT_TYPE chatType)
  {
    if (!this.IsInitialized || chatType != MainChat.CHAT_TYPE.CLAN)
      return;
    this.reloadChat();
  }

  private int getDispatchNum(float dragpower)
  {
    int dispatchNum = 1;
    if ((double) dragpower > 0.30000001192092896)
      dispatchNum += 4;
    if ((double) dragpower > 1.2000000476837158)
      dispatchNum += 5;
    return dispatchNum;
  }

  public override System.Type GetNextState()
  {
    return Object.op_Equality((Object) this.m_manager, (Object) null) || !this.IsInitialized ? base.GetNextState() : this.m_manager.GetTopState();
  }

  public override void OnShowMessageOnDisplay(string chatItemId)
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid() || !MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      return;
    MonoBehaviourSingleton<ClanMatchingManager>.I.OnReadMessage(chatItemId);
  }

  private void reloadChat()
  {
    this.m_manager.ResetCacheData(MainChat.CHAT_TYPE.CLAN);
    MonoBehaviourSingleton<ClanMatchingManager>.I.ChatResetCache();
    MonoBehaviourSingleton<ClanMatchingManager>.I.ChatGetNewMessage(100);
  }
}
