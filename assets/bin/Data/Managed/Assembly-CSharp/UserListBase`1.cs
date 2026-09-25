// Decompiled with JetBrains decompiler
// Type: UserListBase`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public abstract class UserListBase<T> : GameSection where T : CharaInfo
{
  protected int nowPage;
  protected int pageNumMax;
  protected List<T> recvList;
  protected bool isInitializeSend = true;
  protected bool isInitializeSendReopen;

  public override void Initialize()
  {
    if (this.isInitializeSend)
      this.StartCoroutine(this.DoInitialize());
    else
      base.Initialize();
  }

  private IEnumerator DoInitialize()
  {
    bool is_recv = false;
    this.SendGetList(this.nowPage, (Action<bool>) (b => is_recv = true));
    while (!is_recv)
      yield return (object) null;
    this.InitializeBase();
  }

  protected void InitializeBase() => base.Initialize();

  public override void InitializeReopen()
  {
    if (this.isInitializeSendReopen)
      this.StartCoroutine(this.DoInitializeReopen());
    else
      base.InitializeReopen();
  }

  private IEnumerator DoInitializeReopen()
  {
    bool is_recv = false;
    this.SendGetList(this.nowPage, (Action<bool>) (b =>
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E4__this.PostSendGetListByReopen(this.\u003C\u003E4__this.nowPage);
      is_recv = true;
    }));
    while (!is_recv)
      yield return (object) null;
    this.isInitializeSendReopen = false;
    base.InitializeReopen();
  }

  protected virtual void SendGetList(int page, Action<bool> callback)
  {
  }

  protected virtual void PostSendGetListByReopen(int page)
  {
  }

  protected IEnumerator GetPrevPage(Action<bool> call_back)
  {
    bool wait = true;
    bool is_success = true;
    this.SendGetList(this.nowPage > 0 ? this.nowPage - 1 : this.pageNumMax - 1, (Action<bool>) (b =>
    {
      wait = false;
      is_success = b;
    }));
    while (wait)
      yield return (object) null;
    call_back(is_success);
  }

  protected IEnumerator GetNextPage(Action<bool> call_back)
  {
    bool wait = true;
    bool is_success = true;
    this.SendGetList(this.nowPage < this.pageNumMax - 1 ? this.nowPage + 1 : 0, (Action<bool>) (b =>
    {
      wait = false;
      is_success = b;
    }));
    while (wait)
      yield return (object) null;
    call_back(is_success);
  }

  protected virtual void OnQuery_PAGE_PREV()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.GetPrevPage((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  protected virtual void OnQuery_PAGE_NEXT()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.GetNextPage((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }
}
