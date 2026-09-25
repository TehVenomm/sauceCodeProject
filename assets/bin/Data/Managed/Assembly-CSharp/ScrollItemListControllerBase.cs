// Decompiled with JetBrains decompiler
// Type: ScrollItemListControllerBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public abstract class ScrollItemListControllerBase
{
  private const int INIT_PAGE_NUMBER = 0;
  private const int DEFAULT_MAX_PAGE_NUMBER = 1;
  private IUpdatexecutor m_coroutineExecutor;
  protected int m_currentPageNum;
  protected int m_maxPageNum;
  private bool m_isRequestNextPageInfo;
  private int m_itemLoadCompleteCount;
  private Action<int> m_onCompleteAllItemLoading;

  public int CurrentPageNum => this.m_currentPageNum;

  public int MaxPageNum => this.m_maxPageNum;

  protected void StartRequest() => this.m_isRequestNextPageInfo = true;

  protected void EndRequest() => this.m_isRequestNextPageInfo = false;

  public bool IsRequestNextPageInfo => this.m_isRequestNextPageInfo;

  protected int ItemLoadCompleteCount => this.m_itemLoadCompleteCount;

  protected void IncrementLoadCompleteCount() => ++this.m_itemLoadCompleteCount;

  protected void ResetLoadCompleteCount() => this.m_itemLoadCompleteCount = 0;

  protected Action<int> OnCompleteAllItemLoading => this.m_onCompleteAllItemLoading;

  public ScrollItemListControllerBase()
  {
  }

  public ScrollItemListControllerBase(
    ScrollItemListControllerBase.InitializeParameter _initParam)
  {
    this.ResetLoadCompleteCount();
    this.m_onCompleteAllItemLoading = _initParam.OnCompleteAllItemLoading;
    this.m_coroutineExecutor = _initParam.CoroutineExecutor;
    this.m_currentPageNum = 1;
    this.m_maxPageNum = _initParam.MaxPageNumber > 0 ? _initParam.MaxPageNumber : 1;
  }

  public bool SetInitPageInfo() => this.InvokeRequestNextPageInfo(0);

  public bool MoveOnNextPage()
  {
    return this.InvokeRequestNextPageInfo((this.CurrentPageNum + 1) % this.MaxPageNum);
  }

  public bool MoveOnPrevPage()
  {
    return this.InvokeRequestNextPageInfo((this.CurrentPageNum - 1 + this.MaxPageNum) % this.MaxPageNum);
  }

  private bool InvokeRequestNextPageInfo(int _nextPageNum)
  {
    if (this.MaxPageNum <= 0 || this.m_coroutineExecutor == null || this.IsRequestNextPageInfo)
      return false;
    this.StartRequest();
    this.ResetLoadCompleteCount();
    this.m_coroutineExecutor.InvokeCoroutine(this.RequestNextPageInfo(_nextPageNum, new Action<bool, int>(this.OnCallbackRequestPageInfo)));
    return true;
  }

  protected abstract IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback);

  protected virtual void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
  {
    if (_isSucceeded)
      this.m_currentPageNum = this.MaxPageNum < 1 ? 0 : _nextPageNum % this.MaxPageNum;
    this.EndRequest();
  }

  public abstract string GetItemPrefabName();

  public abstract void SetListItem(int i, Transform t, bool is_recycle);

  protected bool ExecUpdateAllUI(System.Action _action)
  {
    if (_action == null || this.m_coroutineExecutor == null)
      return false;
    this.m_coroutineExecutor.UpdateAllUI(_action);
    return true;
  }

  public virtual int GetItemListDataCount() => 0;

  public virtual string GetChatTitle() => string.Empty;

  public class InitializeParameter
  {
    public Action<int> OnCompleteAllItemLoading;
    public IUpdatexecutor CoroutineExecutor;
    public int MaxPageNumber;
  }
}
