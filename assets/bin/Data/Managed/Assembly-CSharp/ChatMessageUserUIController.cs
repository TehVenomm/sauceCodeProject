using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageUserUIController
{
	public class InitParam
	{
		public UIWidget RootWidget;

		public Transform ItemListParent;

		public int ItemVisibleCount;

		public Action OnClickItem;
	}

	private static readonly string MESSAGE_USER_ITEM_PREFAB_PATH = "InternalUI/UI_Friend/FollowListBaseItem";

	private GameObject m_userItemPrefab;

	private bool m_isConnecting;

	private UIWidget m_rootWidget;

	private Transform m_itemListParent;

	private int m_visibleItemCount;

	private List<FriendMessageUserListModel.MessageUserInfo> m_apiResponce;

	private List<HomeMutualFollowerListItem> m_currentItemList = new List<HomeMutualFollowerListItem>();

	private Queue<HomeMutualFollowerListItem> m_itemPool = new Queue<HomeMutualFollowerListItem>();

	private Action m_OnClickItem;

	public bool IsConnecting => m_isConnecting;

	private void SetStartConnecting()
	{
		m_isConnecting = true;
	}

	private void SetEndConnecting()
	{
		m_isConnecting = false;
	}

	public ChatMessageUserUIController()
	{
	}

	public ChatMessageUserUIController(InitParam _param)
	{
		if (_param != null)
		{
			m_rootWidget = _param.RootWidget;
			m_itemListParent = _param.ItemListParent;
			m_visibleItemCount = _param.ItemVisibleCount;
			m_OnClickItem = _param.OnClickItem;
		}
	}

	public IEnumerator LoadInternalResources(MonoBehaviour _coroutineExecutor)
	{
		if (!(m_userItemPrefab != null))
		{
			LoadingQueue loadingQueue = new LoadingQueue(_coroutineExecutor);
			LoadObject loadObject_ListItem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "FollowListBaseItem");
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			m_userItemPrefab = (loadObject_ListItem.loadedObject as GameObject);
		}
	}

	public IEnumerator SendRequestMessagingPersonList(MonoBehaviour _coroutineExecutor)
	{
		if (MonoBehaviourSingleton<FriendManager>.IsValid())
		{
			if (m_userItemPrefab == null)
			{
				yield return LoadInternalResources(_coroutineExecutor);
			}
			SetStartConnecting();
			bool isCalledByOther = true;
			MonoBehaviourSingleton<FriendManager>.I.SendGetUserListMessagedOnce(isCalledByOther, delegate(bool is_success, FriendMessagedMutualFollowerListModel.Param recv_data)
			{
				if (is_success)
				{
					m_apiResponce = recv_data.messageFollowList;
					GenerateMessageUserList(recv_data.messageFollowList);
				}
				SetEndConnecting();
			});
			while (IsConnecting)
			{
				yield return null;
			}
		}
	}

	public void HideAll()
	{
	}

	public void ShowAll()
	{
	}

	protected void GenerateMessageUserList(List<FriendMessageUserListModel.MessageUserInfo> recv_data)
	{
		SetRootAlpha(0f);
		RecycleItem();
		int len = recv_data?.Count ?? 0;
		m_currentItemList = GetItemObjects(len, m_itemListParent);
		int loadCompleteCount = 0;
		for (int i = 0; i < len; i++)
		{
			HomeMutualFollowerListItem homeMutualFollowerListItem = m_currentItemList[i];
			homeMutualFollowerListItem.transform.localPosition = Vector3.down * ((float)i * 130f);
			homeMutualFollowerListItem.transform.localScale = Vector3.one * 0.98f;
			homeMutualFollowerListItem.Initialize(new HomeMutualFollowerListItem.InitParam
			{
				CharacterInfo = recv_data[i],
				Index = i,
				IsFollower = recv_data[i].follower,
				IsFollowing = recv_data[i].following,
				clanId = ((recv_data[i].userClanData != null) ? recv_data[i].userClanData.cId : ""),
				NoReadMsgNum = recv_data[i].noReadNum,
				IsPermittedMessage = recv_data[i].isPermitted,
				IsUseRenderTextureCharaModel = (!FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial()),
				OnClickItem = OnClickItem,
				OnCompleteLoading = delegate
				{
					loadCompleteCount++;
					int num = Mathf.Min(len, m_visibleItemCount);
					if (loadCompleteCount >= num)
					{
						SetRootAlpha(1f);
					}
				}
			});
		}
	}

	private void RecycleItem()
	{
		int i = 0;
		for (int count = m_currentItemList.Count; i < count; i++)
		{
			m_currentItemList[i].HideAll();
			m_itemPool.Enqueue(m_currentItemList[i]);
		}
		m_currentItemList.Clear();
	}

	private List<HomeMutualFollowerListItem> GetItemObjects(int _requestCount, Transform _parentObj)
	{
		List<HomeMutualFollowerListItem> list = new List<HomeMutualFollowerListItem>();
		if (_parentObj == null)
		{
			return list;
		}
		if (_requestCount < m_itemPool.Count)
		{
			for (int i = 0; i < _requestCount; i++)
			{
				list.Add(m_itemPool.Dequeue());
			}
			return list;
		}
		while (m_itemPool.Count > 0)
		{
			list.Add(m_itemPool.Dequeue());
		}
		if (m_userItemPrefab == null)
		{
			return list;
		}
		int num = _requestCount - list.Count;
		for (int j = 0; j < num; j++)
		{
			Transform transform = ResourceUtility.Realizes(m_userItemPrefab, _parentObj, 5);
			if (!(transform == null))
			{
				HomeMutualFollowerListItem component = transform.GetComponent<HomeMutualFollowerListItem>();
				if (!(component == null))
				{
					list.Add(component);
				}
			}
		}
		return list;
	}

	protected void OnClickItem(int _itemIndex)
	{
		if (m_apiResponce == null || _itemIndex < 0 || m_apiResponce.Count <= _itemIndex)
		{
			return;
		}
		FriendMessageUserListModel.MessageUserInfo messageUserInfo = m_apiResponce[_itemIndex];
		if (messageUserInfo != null)
		{
			if (!messageUserInfo.isPermitted)
			{
				string errorMessage = StringTable.GetErrorMessage(13022u);
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), delegate
				{
				}, error: true, 13022);
			}
			else if (MonoBehaviourSingleton<FriendManager>.IsValid())
			{
				MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, isCalledByOther: true, delegate
				{
					if (m_OnClickItem != null)
					{
						m_OnClickItem();
					}
				});
			}
		}
	}

	private void SetRootAlpha(float _value)
	{
		if (!(m_rootWidget == null))
		{
			float alpha = Mathf.Clamp01(_value);
			m_rootWidget.alpha = alpha;
		}
	}

	public void ClearList()
	{
		int i = 0;
		for (int count = m_currentItemList.Count; i < count; i++)
		{
			m_currentItemList[i].CleanRenderTexture();
			UnityEngine.Object.Destroy(m_currentItemList[i].gameObject);
			m_currentItemList[i] = null;
		}
		m_currentItemList.Clear();
	}
}
