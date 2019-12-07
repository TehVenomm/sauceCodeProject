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

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		tweenCtrl.Play();
		StartCoroutine(DoOpen());
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
			base.gameObject.SetActive(value: false);
		});
	}

	private void CreateStampList()
	{
		for (int i = createIcons.Count; i < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + currentUnlockStamps.Count; i++)
		{
			Transform transform = CreateStampItem(grid.transform);
			transform.name = i.ToString();
			createIcons.Add(transform);
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
		Transform transform = ResourceUtility.Realizes(mChatStampPrefab, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		return transform;
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
