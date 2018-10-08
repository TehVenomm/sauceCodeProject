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

	private void SetStartConnecting()
	{
		m_isConnecting = true;
	}

	private void SetEndConnecting()
	{
		m_isConnecting = false;
	}

	public IEnumerator LoadInternalResources(MonoBehaviour _coroutineExecutor)
	{
		if (!((UnityEngine.Object)m_userItemPrefab != (UnityEngine.Object)null))
		{
			LoadingQueue load_queue = new LoadingQueue(_coroutineExecutor);
			LoadObject loadObject_ListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "FollowListBaseItem", false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			m_userItemPrefab = (loadObject_ListItem.loadedObject as GameObject);
		}
	}

	public IEnumerator SendRequestMessagingPersonList(MonoBehaviour _coroutineExecutor)
	{
		if (MonoBehaviourSingleton<FriendManager>.IsValid())
		{
			if ((UnityEngine.Object)m_userItemPrefab == (UnityEngine.Object)null)
			{
				yield return (object)LoadInternalResources(_coroutineExecutor);
			}
			SetStartConnecting();
			bool isCalledByOther = true;
			MonoBehaviourSingleton<FriendManager>.I.SendGetUserListMessagedOnce(isCalledByOther, delegate(bool is_success, FriendMessagedMutualFollowerListModel.Param recv_data)
			{
				if (is_success)
				{
					((_003CSendRequestMessagingPersonList_003Ec__Iterator1F)/*Error near IL_008a: stateMachine*/)._003C_003Ef__this.m_apiResponce = recv_data.messageFollowList;
					((_003CSendRequestMessagingPersonList_003Ec__Iterator1F)/*Error near IL_008a: stateMachine*/)._003C_003Ef__this.GenerateMessageUserList(recv_data.messageFollowList);
				}
				((_003CSendRequestMessagingPersonList_003Ec__Iterator1F)/*Error near IL_008a: stateMachine*/)._003C_003Ef__this.SetEndConnecting();
			});
			while (IsConnecting)
			{
				yield return (object)null;
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
		int len = (recv_data != null) ? recv_data.Count : 0;
		m_currentItemList = GetItemObjects(len, m_itemListParent);
		int loadCompleteCount = 0;
		for (int i = 0; i < len; i++)
		{
			HomeMutualFollowerListItem homeMutualFollowerListItem = m_currentItemList[i];
			homeMutualFollowerListItem.transform.localPosition = Vector3.down * ((float)i * 130f);
			homeMutualFollowerListItem.transform.localScale = Vector3.one * 0.98f;
			HomeMutualFollowerListItem.InitParam initParam = new HomeMutualFollowerListItem.InitParam();
			initParam.CharacterInfo = recv_data[i];
			initParam.Index = i;
			initParam.IsFollower = recv_data[i].follower;
			initParam.IsFollowing = recv_data[i].following;
			initParam.NoReadMsgNum = recv_data[i].noReadNum;
			initParam.IsPermittedMessage = recv_data[i].isPermitted;
			initParam.IsUseRenderTextureCharaModel = (!FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial());
			initParam.OnClickItem = OnClickItem;
			initParam.OnCompleteLoading = delegate
			{
				loadCompleteCount++;
				int num = Mathf.Min(len, m_visibleItemCount);
				if (loadCompleteCount >= num)
				{
					SetRootAlpha(1f);
				}
			};
			homeMutualFollowerListItem.Initialize(initParam);
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
		if ((UnityEngine.Object)_parentObj == (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)m_userItemPrefab == (UnityEngine.Object)null)
		{
			return list;
		}
		int num = _requestCount - list.Count;
		for (int j = 0; j < num; j++)
		{
			Transform transform = ResourceUtility.Realizes(m_userItemPrefab, _parentObj, 5);
			if (!((UnityEngine.Object)transform == (UnityEngine.Object)null))
			{
				HomeMutualFollowerListItem component = transform.GetComponent<HomeMutualFollowerListItem>();
				if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
				{
					list.Add(component);
				}
			}
		}
		return list;
	}

	protected void OnClickItem(int _itemIndex)
	{
		if (m_apiResponce != null && _itemIndex >= 0 && m_apiResponce.Count > _itemIndex)
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = m_apiResponce[_itemIndex];
			if (messageUserInfo != null)
			{
				if (!messageUserInfo.isPermitted)
				{
					string errorMessage = StringTable.GetErrorMessage(13022u);
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage, null, null, null, null), delegate
					{
					}, true, 13022);
				}
				else if (MonoBehaviourSingleton<FriendManager>.IsValid())
				{
					MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, true, delegate
					{
						if (m_OnClickItem != null)
						{
							m_OnClickItem();
						}
					});
				}
			}
		}
	}

	private void SetRootAlpha(float _value)
	{
		if (!((UnityEngine.Object)m_rootWidget == (UnityEngine.Object)null))
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
