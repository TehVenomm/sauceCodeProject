using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMessageUIController : UIBehaviour
{
	private enum UI
	{
		SPR_BG,
		WGT_ANCHOR_TOP,
		WGT_ANCHOR_BOTTOM,
		GRD_STAMP_LIST
	}

	private const string PREFAB_NAME_CHAT_ITEM = "ChatItem";

	private const string PREFAB_NAME_CHAT_STAMP_ITEM = "ChatStampListItem";

	private const int STAMP_COL_DEFAULT_COUNT = 5;

	private const int STAMP_COL_LANDSCAPE_COUNT = 4;

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_CENTER = 0.5f;

	private const float ANCHOR_RIGHT = 1f;

	private const float ANCHOR_BOT = 0f;

	private const float ANCHOR_TOP = 1f;

	private static readonly Vector4 WIDGET_ANCHOR_BG_IMG_DEFAULT_SETTINGS = new Vector4(0f, 0f, 72f, -72f);

	private static readonly Vector4 WIDGET_ANCHOR_BG_IMG_SPLIT_LANDSCAPE_SETTINGS = new Vector4(0f, 0f, 15f, -15f);

	private static readonly Vector4 WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS = new Vector4(0f, 0f, -540f, -120f);

	private static readonly Vector4 WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS = new Vector4(0f, -390f, 40f, -80f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS = new Vector4(0f, 0f, 82f, -540f);

	private static readonly Vector4 WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(465f, 0f, 40f, -70f);

	private static readonly Vector4 UI_BTN_INPUT_CLOSE_DEFAULT_POS = new Vector4(10f, 46f, -2f, 59f);

	private static readonly Vector4 UI_BTN_INPUT_CLOSE_LANDSCAPE_POS = new Vector4(0f, 36f, 2f, 63f);

	private const int CHAT_ITEM_OFFSET = 22;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const float SCROLL_BAR_OFFSET = 48f;

	private const float LOADPREV_SCROLLY_THRESHOLD = 30f;

	private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0f);

	[SerializeField]
	private UILabel m_titleUpper;

	[SerializeField]
	private UILabel m_titleLower;

	[SerializeField]
	private GameObject m_defaultLabelObject;

	[SerializeField]
	private GameObject m_stampListRoot;

	[SerializeField]
	private GameObject m_postFrameObject;

	[SerializeField]
	private GameObject m_talkRootObject;

	[SerializeField]
	private UIPanel m_slideLimit;

	[SerializeField]
	private UIScrollView m_ScrollView;

	[SerializeField]
	private UIWidget m_DummyDragScroll;

	[SerializeField]
	private BoxCollider m_DragScrollCollider;

	[SerializeField]
	private ChatInputFrame m_InputFrame;

	[SerializeField]
	private UIInput m_Input;

	private Transform m_scrollViewTrans;

	private Transform m_dragScrollTrans;

	private List<FriendMessageData> postMessageList = new List<FriendMessageData>();

	private Queue<FriendMessage.PostMessageData> messageQueue = new Queue<FriendMessage.PostMessageData>();

	private int m_nowPage;

	private int m_loadedPage = -1;

	private FriendMessage.MessageItemListData itemListData;

	private GameObject chatItemPrefab;

	private GameObject chatStampListPrefab;

	private List<int> m_StampIdListCanPost;

	private bool isInitialized;

	private bool updateStampList;

	private MainChat m_manager;

	private UIWidget m_widgetBackGroundImg;

	private UIWidget m_widgetTop;

	private UIWidget m_widgetBot;

	private UIGrid m_gridStamp;

	private readonly float IntervalSendGetNoRead = 5f;

	private float interval;

	private Transform ScrollViewTrans => m_scrollViewTrans ?? (m_scrollViewTrans = m_ScrollView.transform);

	private Transform DragScrollTrans => m_dragScrollTrans ?? (m_dragScrollTrans = m_DragScrollCollider.transform);

	public FriendMessageUserListModel.MessageUserInfo talkUser
	{
		get;
		private set;
	}

	private float CurrentTotalHeight
	{
		get
		{
			if (itemListData == null)
			{
				return 0f;
			}
			return itemListData.currentTotalHeight;
		}
	}

	private float BasePosY
	{
		get
		{
			if (itemListData == null)
			{
				return 0f;
			}
			return itemListData.basePosY;
		}
	}

	private UIWidget WidgetBackGroundImg => m_widgetBackGroundImg ?? (m_widgetBackGroundImg = GetCtrl(UI.SPR_BG).GetComponent<UIWidget>());

	private UIWidget WidgetTop => m_widgetTop ?? (m_widgetTop = GetCtrl(UI.WGT_ANCHOR_TOP).GetComponent<UIWidget>());

	private UIWidget WidgetBot => m_widgetBot ?? (m_widgetBot = GetCtrl(UI.WGT_ANCHOR_BOTTOM).GetComponent<UIWidget>());

	private UIGrid GridStamp => m_gridStamp ?? (m_gridStamp = GetCtrl(UI.GRD_STAMP_LIST).GetComponent<UIGrid>());

	public void Initialize(MainChat _manager)
	{
		m_manager = _manager;
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		InitUI();
		CreateCtrlsArray(typeof(UI));
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotateAsInit(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatItem");
		LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
		talkUser = MonoBehaviourSingleton<FriendManager>.I.talkUser;
		if (talkUser == null)
		{
			OnClickCloseButton();
			yield break;
		}
		string name = talkUser.name;
		SetLebelText(m_titleUpper, name);
		SetLebelText(m_titleLower, name);
		m_nowPage = 0;
		m_loadedPage = -1;
		itemListData = new FriendMessage.MessageItemListData(m_talkRootObject);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		chatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		chatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		SetSliderLimit();
		m_DummyDragScroll.width = 410;
		ResetStampIdList();
		Reset();
		postMessageList.Clear();
		UIScrollView scrollView = m_ScrollView;
		scrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Combine(scrollView.onDragFinished, new UIScrollView.OnDragNotification(OnDragFinished));
		ChatInputFrame inputFrame = m_InputFrame;
		inputFrame.onChange = (Action)Delegate.Combine(inputFrame.onChange, (Action)delegate
		{
			OnInput();
		});
		ChatInputFrame inputFrame2 = m_InputFrame;
		inputFrame2.onSubmit = (Action)Delegate.Combine(inputFrame2.onSubmit, (Action)delegate
		{
			OnTouchPost();
		});
		if (talkUser.userId == 0)
		{
			m_stampListRoot.SetActive(value: false);
			m_postFrameObject.SetActive(value: false);
		}
		else
		{
			updateStampList = true;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, new Action(InitStampList));
		}
		isInitialized = true;
	}

	private void InitStampList()
	{
		if (m_StampIdListCanPost == null)
		{
			ResetStampIdList();
		}
		if (updateStampList)
		{
			int count = m_StampIdListCanPost.Count;
			SetGrid(UI.GRD_STAMP_LIST, null, count, reset: true, CreateStampItem, InitStampItem);
			updateStampList = false;
		}
	}

	public void ResetStampIdList()
	{
		if (m_StampIdListCanPost == null)
		{
			m_StampIdListCanPost = new List<int>();
		}
		m_StampIdListCanPost.Clear();
		if (Singleton<StampTable>.IsValid())
		{
			Singleton<StampTable>.I.table.ForEach(delegate(StampTable.Data stamp_data)
			{
				int id = (int)stamp_data.id;
				if (CanIPostTheStamp(id))
				{
					m_StampIdListCanPost.Add(id);
				}
			});
		}
	}

	public bool IsValidStampId(int stampId)
	{
		if (stampId < 1)
		{
			return false;
		}
		if (Singleton<StampTable>.I.GetData((uint)stampId) == null)
		{
			return false;
		}
		return true;
	}

	public bool CanIPostTheStamp(int id)
	{
		if (!IsValidStampId(id))
		{
			return false;
		}
		if (Singleton<StampTable>.I.GetData((uint)id).type == STAMP_TYPE.COMMON)
		{
			return true;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(id))
		{
			return false;
		}
		return true;
	}

	private Transform CreateStampItem(int index, Transform parent)
	{
		if (chatStampListPrefab == null)
		{
			return null;
		}
		Transform transform = ResourceUtility.Realizes(chatStampListPrefab, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		return transform;
	}

	private void InitStampItem(int index, Transform iTransform, bool isRecycle)
	{
		if (m_StampIdListCanPost != null)
		{
			int stampId = m_StampIdListCanPost[index];
			ChatStampListItem item = iTransform.GetComponent<ChatStampListItem>();
			item.Init(stampId);
			ChatStampListItem chatStampListItem = item;
			chatStampListItem.onButton = (Action)Delegate.Combine(chatStampListItem.onButton, (Action)delegate
			{
				SendStamp(item.StampId);
			});
		}
	}

	private void AddNextChatItem(Action<ChatItem> initializer, bool topPosition, bool forceScroll)
	{
		if (chatItemPrefab == null)
		{
			return;
		}
		FriendMessage.MessageItemListData messageItemListData = itemListData;
		ChatItem chatItem = null;
		chatItem = ResourceUtility.Realizes(chatItemPrefab, messageItemListData.rootObject.transform, 5).GetComponent<ChatItem>();
		if (topPosition)
		{
			initializer(chatItem);
			float num = chatItem.height + 22f;
			messageItemListData.basePosY += num;
			chatItem.transform.localPosition = new Vector3(-15f, messageItemListData.basePosY, 0f);
			messageItemListData.currentTotalHeight += num;
		}
		else
		{
			if (messageItemListData.itemList.Count > 0)
			{
				messageItemListData.currentTotalHeight += 22f;
			}
			float num2 = messageItemListData.currentTotalHeight - messageItemListData.basePosY;
			chatItem.transform.localPosition = new Vector3(-15f, 0f - num2, 0f);
			initializer(chatItem);
			messageItemListData.currentTotalHeight += chatItem.height;
		}
		UpdateDummyDragScroll();
		if (!topPosition && forceScroll)
		{
			float num3 = messageItemListData.currentTotalHeight + m_ScrollView.panel.baseClipRegion.y - m_ScrollView.panel.baseClipRegion.w * 0.5f + (ScrollViewTrans.localPosition.y + m_ScrollView.panel.clipOffset.y) + m_ScrollView.panel.clipSoftness.y;
			ForceScroll(num3 - messageItemListData.basePosY, useSpring: true);
		}
		if (topPosition)
		{
			messageItemListData.itemList.Insert(0, chatItem);
		}
		else
		{
			messageItemListData.itemList.Add(chatItem);
		}
	}

	private void SetSliderLimit()
	{
		if (!(m_slideLimit == null))
		{
			m_slideLimit.topAnchor.absolute = (int)HOME_SLIDER_OPEN_POS.y + 9;
			m_slideLimit.bottomAnchor.absolute = (int)HOME_SLIDER_CLOSE_POS.y - 49;
		}
	}

	private void SetLebelText(UILabel _ui, string _text)
	{
		if (!(_ui == null))
		{
			_ui.text = _text;
		}
	}

	private void Update()
	{
		if (!isInitialized)
		{
			return;
		}
		float num = m_ScrollView.panel.baseClipRegion.w - m_ScrollView.panel.baseClipRegion.y + DragScrollTrans.localPosition.y - (m_ScrollView.panel.finalClipRegion.w + m_ScrollView.panel.clipOffset.y);
		m_DragScrollCollider.center = new Vector2(m_ScrollView.panel.baseClipRegion.x, 0f - num);
		interval += Time.deltaTime;
		if (IntervalSendGetNoRead <= interval)
		{
			interval = 0f;
			SendGetNoReadMessage();
		}
		List<FriendMessageData> messageDetailList = MonoBehaviourSingleton<FriendManager>.I.messageDetailList;
		if (messageDetailList.Count > postMessageList.Count)
		{
			HashSet<FriendMessageData> hashSet = new HashSet<FriendMessageData>(messageDetailList);
			hashSet.ExceptWith(postMessageList);
			FriendMessageData[] array = new FriendMessageData[hashSet.Count];
			hashSet.CopyTo(array);
			long num2 = 0L;
			if (0 < postMessageList.Count)
			{
				num2 = postMessageList[0].lid;
			}
			for (int num3 = array.Length - 1; num3 >= 0; num3--)
			{
				if (num2 > array[0].lid)
				{
					messageQueue.Enqueue(new FriendMessage.PostMessageData(array[num3], _topPosition: true, _forceScroll: false));
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (num2 <= array[0].lid)
				{
					bool forceScroll = i == array.Length - 1;
					messageQueue.Enqueue(new FriendMessage.PostMessageData(array[i], _topPosition: false, forceScroll));
				}
			}
			postMessageList.AddRange(array);
			postMessageList.Sort((FriendMessageData l, FriendMessageData r) => l.lid.CompareTo(r.lid));
		}
		if (0 < messageQueue.Count)
		{
			FriendMessage.PostMessageData postMessageData = messageQueue.Dequeue();
			PostUI(postMessageData.message, postMessageData.topPosition, postMessageData.forceScroll);
			if (0 > m_loadedPage && messageQueue.Count == 0)
			{
				m_loadedPage = 0;
			}
		}
	}

	private void Reset()
	{
		m_Input.value = "";
		m_InputFrame.Reset();
		UpdateAnchors();
		UpdateWindowSize();
	}

	public void UpdateWindowSize()
	{
		ForceScroll(CurrentTotalHeight + m_ScrollView.panel.baseClipRegion.y - m_ScrollView.panel.baseClipRegion.w * 0.5f + (ScrollViewTrans.localPosition.y + m_ScrollView.panel.clipOffset.y) + m_ScrollView.panel.clipSoftness.y, useSpring: false);
		UpdateDummyDragScroll();
	}

	private void UpdateDummyDragScroll()
	{
		if (m_ScrollView.panel.height > CurrentTotalHeight)
		{
			m_DummyDragScroll.height = (int)(m_ScrollView.panel.height - 20f);
		}
		else
		{
			m_DummyDragScroll.height = (int)(CurrentTotalHeight - 20f);
		}
		DragScrollTrans.localPosition = new Vector3(m_ScrollView.panel.clipOffset.x, BasePosY - CurrentTotalHeight, 0f);
		m_DragScrollCollider.size = new Vector3(m_ScrollView.panel.finalClipRegion.z, m_ScrollView.panel.finalClipRegion.w - m_ScrollView.panel.clipSoftness.y * 2f, 0f);
	}

	private void ForceScroll(float newHeight, bool useSpring)
	{
		m_ScrollView.DisableSpring();
		if (useSpring)
		{
			SpringPanel.Begin(m_ScrollView.gameObject, Vector3.up * newHeight, 20f);
			return;
		}
		Vector2 clipOffset = m_ScrollView.panel.clipOffset;
		float num = ScrollViewTrans.localPosition.y + clipOffset.y;
		ScrollViewTrans.localPosition = Vector3.up * newHeight;
		clipOffset.y = 0f - newHeight + num;
		m_ScrollView.panel.clipOffset = clipOffset;
	}

	private void OnDragFinished()
	{
		if (MonoBehaviourSingleton<FriendManager>.I.messagePageMax - 1 > m_nowPage && m_nowPage == m_loadedPage && CurrentTotalHeight >= m_ScrollView.panel.height)
		{
			Bounds bounds = m_ScrollView.bounds;
			Vector3 vector = m_ScrollView.panel.CalculateConstrainOffset(bounds.min, bounds.max);
			if (30f <= vector.y)
			{
				m_nowPage++;
				SendGetMessageDetail();
			}
		}
	}

	public void OnTouchPost()
	{
		string value = m_Input.value;
		if (value.Length != 0)
		{
			SendText(value);
			m_Input.value = "";
			m_InputFrame.FrameResize();
		}
	}

	public void OnInput()
	{
		m_InputFrame.FrameResize();
		bool flag = string.IsNullOrEmpty(m_Input.value);
		if (m_defaultLabelObject.activeSelf != flag)
		{
			m_defaultLabelObject.SetActive(flag);
		}
	}

	private void OnScreenRotate(bool _isPortrait)
	{
		SetCommonScreenSettings(_isPortrait);
		UpdateAnchors();
		updateStampList = true;
		InitStampList();
	}

	private void OnScreenRotateAsInit(bool _isPortrait)
	{
		SetCommonScreenSettings(_isPortrait);
		UpdateAnchors();
	}

	private void SetCommonScreenSettings(bool _isPortrait)
	{
		Vector4 vector = _isPortrait ? WIDGET_ANCHOR_BG_IMG_DEFAULT_SETTINGS : WIDGET_ANCHOR_BG_IMG_SPLIT_LANDSCAPE_SETTINGS;
		WidgetBackGroundImg.leftAnchor.Set(0f, vector.x);
		WidgetBackGroundImg.rightAnchor.Set(1f, vector.y);
		WidgetBackGroundImg.bottomAnchor.Set(_isPortrait ? 0f : 0f, vector.z);
		WidgetBackGroundImg.topAnchor.Set(1f, vector.w);
		vector = (_isPortrait ? WIDGET_ANCHOR_TOP_DEFAULT_SETTINGS : WIDGET_ANCHOR_TOP_SPLIT_LANDSCAPE_SETTINGS);
		WidgetTop.leftAnchor.Set(0f, vector.x);
		WidgetTop.rightAnchor.Set(1f, vector.y);
		WidgetTop.bottomAnchor.Set(_isPortrait ? 1f : 0f, vector.z);
		WidgetTop.topAnchor.Set(1f, vector.w);
		vector = (_isPortrait ? WIDGET_ANCHOR_BOT_DEFAULT_SETTINGS : WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS);
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyChatAnchor)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			if (!SpecialDeviceManager.IsPortrait)
			{
				vector = specialDeviceInfo.FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS;
			}
		}
		WidgetBot.leftAnchor.Set(0f, vector.x);
		WidgetBot.rightAnchor.Set(1f, vector.y);
		WidgetBot.bottomAnchor.Set(0f, vector.z);
		WidgetBot.topAnchor.Set(1f, vector.w);
		GridStamp.maxPerLine = (_isPortrait ? 5 : 4);
		GridStamp.enabled = true;
	}

	private void SendGetMessageDetail()
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(talkUser.userId, m_nowPage, isCalledByOther: true, delegate(bool is_success)
		{
			if (is_success)
			{
				m_ScrollView.DisableSpring();
				RefreshUI();
				m_loadedPage = m_nowPage;
			}
		});
	}

	private void SendGetNoReadMessage()
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetNoreadMessage(isCalledByOther: true, delegate
		{
		});
	}

	private void SendStamp(int _stampId)
	{
		int num = _stampId;
		string message = "[STAMP]" + num.ToString();
		MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(talkUser.userId, message, isCalledByOther: true, delegate
		{
		});
	}

	private void SendText(string _text)
	{
		string text = _text;
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace("\n", "");
		}
		MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(talkUser.userId, text, isCalledByOther: true, delegate
		{
		});
	}

	private void PostUI(FriendMessageData data, bool topPosition, bool forceScroll)
	{
		if (data.message.StartsWith("[STAMP]"))
		{
			string message = data.message;
			message = message.Replace("[STAMP]", "");
			PostUIStamp(data.fromUserId, int.Parse(message), topPosition, forceScroll);
		}
		else
		{
			PostUIMessage(data.fromUserId, data.message, topPosition, forceScroll);
		}
	}

	private void PostUIMessage(int userId, string message, bool topPosition, bool forceScroll)
	{
		if (!string.IsNullOrEmpty(message))
		{
			message = message.Replace("\n", "");
		}
		AddNextChatItem(delegate(ChatItem chatItem)
		{
			chatItem.Init(userId, talkUser.name, message);
		}, topPosition, forceScroll);
	}

	private void PostUIStamp(int userId, int stampId, bool topPosition, bool forceScroll)
	{
		if (IsValidStampId(stampId) && Singleton<StampTable>.I.GetData((uint)stampId) != null)
		{
			AddNextChatItem(delegate(ChatItem chatItem)
			{
				chatItem.Init(userId, talkUser.name, stampId);
			}, topPosition, forceScroll);
		}
	}

	public void OnClickCloseButton()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		m_manager.PopState();
	}
}
