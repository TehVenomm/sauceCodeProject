using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatStampAll
{
	public UIGrid grid;

	public UIScrollView scroll;

	public UIButton closeButton;

	public UITweenCtrl tweenCtrl;

	private GameObject mChatStampPrefab;

	private List<StampTable.Data> currentUnlockStamps;

	private List<Transform> createIcons;

	private bool initailized;

	public ChatStampAll()
		: this()
	{
	}

	public void Open()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		tweenCtrl.Play(true, null);
		this.StartCoroutine(DoOpen());
	}

	private IEnumerator DoOpen()
	{
		if (!initailized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
			if (loadingQueue.IsLoading())
			{
				yield return (object)loadingQueue.Wait();
			}
			mChatStampPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
			closeButton.onClick.Add(new EventDelegate(OnClose));
			createIcons = new List<Transform>();
			initailized = true;
		}
		currentUnlockStamps = Singleton<StampTable>.I.GetUnlockStamps(MonoBehaviourSingleton<UserInfoManager>.I);
		CreateStampList();
		InitStampList();
		UIUtility.SetGridItemsDraggableWidget(scroll, grid, createIcons.Count);
	}

	public void Close()
	{
		tweenCtrl.Play(false, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			this.get_gameObject().SetActive(false);
		});
	}

	private void CreateStampList()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		for (int i = createIcons.Count; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + currentUnlockStamps.Count; i++)
		{
			Transform val = CreateStampItem(grid.get_transform());
			val.set_name(i.ToString());
			createIcons.Add(val);
		}
		grid.Reposition();
		scroll.ResetPosition();
	}

	private unsafe void InitStampList()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		for (int i = 0; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + currentUnlockStamps.Count; i++)
		{
			Transform iTransform = createIcons[i];
			if (MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count > i)
			{
				int index2 = i;
				_003CInitStampList_003Ec__AnonStorey2CC _003CInitStampList_003Ec__AnonStorey2CC;
				InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index2], iTransform, new Action((object)_003CInitStampList_003Ec__AnonStorey2CC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else
			{
				int index = i - MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count;
				_003CInitStampList_003Ec__AnonStorey2CD _003CInitStampList_003Ec__AnonStorey2CD;
				InitStampItem((int)currentUnlockStamps[index].id, iTransform, new Action((object)_003CInitStampList_003Ec__AnonStorey2CD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}

	private Transform CreateStampItem(Transform parent)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(mChatStampPrefab, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		return val;
	}

	private void InitStampItem(int stampId, Transform iTransform, Action onClick)
	{
		ChatStampListItem component = iTransform.GetComponent<ChatStampListItem>();
		component.Init(stampId);
		component.onButton = onClick;
	}

	private void OnClickIcon(int stampId)
	{
		MonoBehaviourSingleton<UIManager>.I.mainChat.SendStampAsMine(stampId);
		Close();
	}

	private void OnClose()
	{
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
		Close();
	}
}
