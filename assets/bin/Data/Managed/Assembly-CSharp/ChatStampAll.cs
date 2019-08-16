using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatStampAll : MonoBehaviour
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
		this.get_gameObject().SetActive(true);
		tweenCtrl.Play();
		this.StartCoroutine(DoOpen());
	}

	private IEnumerator DoOpen()
	{
		if (!initailized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
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
		tweenCtrl.Play(forward: false, delegate
		{
			this.get_gameObject().SetActive(false);
		});
	}

	private void CreateStampList()
	{
		for (int i = createIcons.Count; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + currentUnlockStamps.Count; i++)
		{
			Transform val = CreateStampItem(grid.get_transform());
			val.set_name(i.ToString());
			createIcons.Add(val);
		}
		grid.Reposition();
		scroll.ResetPosition();
	}

	private void InitStampList()
	{
		for (int i = 0; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + currentUnlockStamps.Count; i++)
		{
			Transform iTransform = createIcons[i];
			if (MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count > i)
			{
				int index = i;
				InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index], iTransform, delegate
				{
					OnClickIcon(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index]);
				});
			}
			else
			{
				int index2 = i - MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count;
				InitStampItem((int)currentUnlockStamps[index2].id, iTransform, delegate
				{
					OnClickIcon((int)currentUnlockStamps[index2].id);
				});
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
		SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
		Close();
	}
}
