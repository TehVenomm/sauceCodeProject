using System;
using System.Collections;
using UnityEngine;

public abstract class ScrollItemListControllerBase
{
	public class InitializeParameter
	{
		public Action<int> OnCompleteAllItemLoading;

		public IUpdatexecutor CoroutineExecutor;

		public int MaxPageNumber;
	}

	private const int INIT_PAGE_NUMBER = 0;

	private const int DEFAULT_MAX_PAGE_NUMBER = 1;

	private IUpdatexecutor m_coroutineExecutor;

	protected int m_currentPageNum;

	protected int m_maxPageNum;

	private bool m_isRequestNextPageInfo;

	private int m_itemLoadCompleteCount;

	private Action<int> m_onCompleteAllItemLoading;

	public int CurrentPageNum => m_currentPageNum;

	public int MaxPageNum => m_maxPageNum;

	public bool IsRequestNextPageInfo => m_isRequestNextPageInfo;

	protected int ItemLoadCompleteCount => m_itemLoadCompleteCount;

	protected Action<int> OnCompleteAllItemLoading => m_onCompleteAllItemLoading;

	public ScrollItemListControllerBase()
	{
	}

	public ScrollItemListControllerBase(InitializeParameter _initParam)
	{
		ResetLoadCompleteCount();
		m_onCompleteAllItemLoading = _initParam.OnCompleteAllItemLoading;
		m_coroutineExecutor = _initParam.CoroutineExecutor;
		m_currentPageNum = 1;
		m_maxPageNum = ((_initParam.MaxPageNumber <= 0) ? 1 : _initParam.MaxPageNumber);
	}

	protected void StartRequest()
	{
		m_isRequestNextPageInfo = true;
	}

	protected void EndRequest()
	{
		m_isRequestNextPageInfo = false;
	}

	protected void IncrementLoadCompleteCount()
	{
		m_itemLoadCompleteCount++;
	}

	protected void ResetLoadCompleteCount()
	{
		m_itemLoadCompleteCount = 0;
	}

	public bool SetInitPageInfo()
	{
		return InvokeRequestNextPageInfo(0);
	}

	public bool MoveOnNextPage()
	{
		int nextPageNum = (CurrentPageNum + 1) % MaxPageNum;
		return InvokeRequestNextPageInfo(nextPageNum);
	}

	public bool MoveOnPrevPage()
	{
		int nextPageNum = (CurrentPageNum - 1 + MaxPageNum) % MaxPageNum;
		return InvokeRequestNextPageInfo(nextPageNum);
	}

	private unsafe bool InvokeRequestNextPageInfo(int _nextPageNum)
	{
		if (MaxPageNum <= 0 || m_coroutineExecutor == null)
		{
			return false;
		}
		if (IsRequestNextPageInfo)
		{
			return false;
		}
		StartRequest();
		ResetLoadCompleteCount();
		m_coroutineExecutor.InvokeCoroutine(RequestNextPageInfo(_nextPageNum, new Action<bool, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/)));
		return true;
	}

	protected abstract IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback);

	protected virtual void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
	{
		if (_isSucceeded)
		{
			m_currentPageNum = ((MaxPageNum >= 1) ? (_nextPageNum % MaxPageNum) : 0);
		}
		EndRequest();
	}

	public abstract string GetItemPrefabName();

	public abstract void SetListItem(int i, Transform t, bool is_recycle);

	protected bool ExecUpdateAllUI(Action _action)
	{
		if (_action == null || m_coroutineExecutor == null)
		{
			return false;
		}
		m_coroutineExecutor.UpdateAllUI(_action);
		return true;
	}

	public virtual int GetItemListDataCount()
	{
		return 0;
	}

	public virtual string GetChatTitle()
	{
		return string.Empty;
	}
}
