using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameSelfAnnounce
{
	[SerializeField]
	protected UITweenCtrl tweenCtrl;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected float lockInterval;

	[SerializeField]
	protected List<GameObject> visibleTargets;

	private bool isUnLock;

	private bool isLockReq;

	private float lockTimer;

	public UIInGameSelfAnnounce()
		: this()
	{
	}

	private void Awake()
	{
		SetVisible(false);
	}

	public void Play(Action callback = null)
	{
		if (!(tweenCtrl == null))
		{
			SetVisible(true);
			tweenCtrl.Reset();
			tweenCtrl.Play(true, delegate
			{
				isLockReq = true;
				lockTimer = lockInterval;
				if (callback != null)
				{
					callback.Invoke();
				}
				SetVisible(false);
			});
			if (!isUnLock)
			{
				panelChange.UnLock();
				isUnLock = true;
			}
			isLockReq = false;
		}
	}

	public void Skip()
	{
		isLockReq = true;
		lockTimer = lockInterval;
		tweenCtrl.Skip(true);
	}

	private void LateUpdate()
	{
		if (isLockReq)
		{
			lockTimer -= Time.get_deltaTime();
			if (!(lockTimer > 0f))
			{
				panelChange.Lock();
				isLockReq = false;
				isUnLock = false;
			}
		}
	}

	private void SetVisible(bool isVisible)
	{
		if (!object.ReferenceEquals(visibleTargets, null) && visibleTargets.Count != 0)
		{
			for (int i = 0; i < visibleTargets.Count; i++)
			{
				visibleTargets[i].SetActive(isVisible);
			}
		}
	}
}
