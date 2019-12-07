using Network;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class UserListBase<T> : GameSection where T : CharaInfo
{
	protected int nowPage;

	protected int pageNumMax;

	protected List<T> recvList;

	protected bool isInitializeSend = true;

	protected bool isInitializeSendReopen;

	public override void Initialize()
	{
		if (isInitializeSend)
		{
			StartCoroutine(DoInitialize());
		}
		else
		{
			base.Initialize();
		}
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv = false;
		SendGetList(nowPage, delegate
		{
			is_recv = true;
		});
		while (!is_recv)
		{
			yield return null;
		}
		InitializeBase();
	}

	protected void InitializeBase()
	{
		base.Initialize();
	}

	public override void InitializeReopen()
	{
		if (isInitializeSendReopen)
		{
			StartCoroutine(DoInitializeReopen());
		}
		else
		{
			base.InitializeReopen();
		}
	}

	private IEnumerator DoInitializeReopen()
	{
		bool is_recv = false;
		SendGetList(nowPage, delegate
		{
			PostSendGetListByReopen(nowPage);
			is_recv = true;
		});
		while (!is_recv)
		{
			yield return null;
		}
		isInitializeSendReopen = false;
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
		int page = (nowPage > 0) ? (nowPage - 1) : (pageNumMax - 1);
		SendGetList(page, delegate(bool b)
		{
			wait = false;
			is_success = b;
		});
		while (wait)
		{
			yield return null;
		}
		call_back(is_success);
	}

	protected IEnumerator GetNextPage(Action<bool> call_back)
	{
		bool wait = true;
		bool is_success = true;
		int page = (nowPage < pageNumMax - 1) ? (nowPage + 1) : 0;
		SendGetList(page, delegate(bool b)
		{
			wait = false;
			is_success = b;
		});
		while (wait)
		{
			yield return null;
		}
		call_back(is_success);
	}

	protected virtual void OnQuery_PAGE_PREV()
	{
		GameSection.StayEvent();
		StartCoroutine(GetPrevPage(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	protected virtual void OnQuery_PAGE_NEXT()
	{
		GameSection.StayEvent();
		StartCoroutine(GetNextPage(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}
}
