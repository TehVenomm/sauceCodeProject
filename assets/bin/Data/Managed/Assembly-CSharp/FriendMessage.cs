using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMessage : GameSection
{
	private enum UI
	{
		SCR_CHAT,
		SPR_BG_CHAT,
		SPR_BG_IN_FRAME,
		Title_U,
		Title_D,
		OBJ_ROOM_ITEM_LIST_ROOT,
		WGT_DUMMY_DRAG_SCROLL,
		WGT_SLIDE_LIMIT,
		IPT_POST,
		OBJ_INPUT_FRAME,
		WGT_ANCHOR_BOTTOM,
		WGT_ANCHOR_TOP,
		WGT_CHAT_ROOT,
		OBJ_POST_FRAME,
		LBL_DEFAULT,
		SCR_STAMP_LIST,
		GRD_STAMP_LIST
	}

	public class MessageItemListData
	{
		private const float DEFAULT_OFFSET = -26f;

		public GameObject rootObject;

		public List<ChatItem> itemList;

		public float currentTotalHeight;

		public int oldestItemIndex;

		public float slideOffset;

		public float basePosY;

		public MessageItemListData(GameObject root)
		{
			rootObject = root;
			itemList = new List<ChatItem>();
			Init();
		}

		public void Init()
		{
			currentTotalHeight = 0f;
			oldestItemIndex = 0;
			basePosY = 0f;
			slideOffset = -26f;
		}

		public void Reset()
		{
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				UnityEngine.Object.DestroyImmediate(itemList[i].gameObject);
			}
			itemList.Clear();
			Init();
		}

		public void MoveAll(float y)
		{
			Vector3 localPosition = itemList[0].transform.localPosition;
			int i = 0;
			for (int count = itemList.Count; i < count; i++)
			{
				Transform transform = itemList[i].transform;
				Vector3 localPosition2 = transform.localPosition;
				localPosition.y = localPosition2.y + y;
				transform.localPosition = localPosition;
			}
		}
	}

	public class PostMessageData
	{
		public FriendMessageData message;

		public bool topPosition;

		public bool forceScroll;

		public PostMessageData(FriendMessageData _message, bool _topPosition, bool _forceScroll)
		{
			message = _message;
			topPosition = _topPosition;
			forceScroll = _forceScroll;
		}
	}

	private const int CHAT_ITEM_OFFSET = 22;

	private const float SOFTNESS_HEIGHT = 10f;

	private const float SPRING_STRENGTH = 20f;

	private const float CHAT_WIDTH = 410f;

	private const float SCROLL_BAR_OFFSET = 48f;

	private const float LOADPREV_SCROLLY_THRESHOLD = 30f;

	private string talkerName = string.Empty;

	private int nowPage;

	private int loadedPage;

	private GameObject chatItemPrefab;

	private GameObject chatStampListPrefab;

	private MessageItemListData itemListData;

	private List<FriendMessageData> postMessageList = new List<FriendMessageData>();

	private Queue<PostMessageData> messageQueue = new Queue<PostMessageData>();

	private List<int> m_StampIdListCanPost;

	private UIScrollView m_ScrollView;

	private Transform m_ScrollViewTrans;

	private UIWidget m_DummyDragScroll;

	private BoxCollider m_DragScrollCollider;

	private Transform m_DragScrollTrans;

	private UISprite m_BackgroundInFrame;

	private UIInput m_Input;

	private ChatInputFrame m_InputFrame;

	private readonly float IntervalSendGetNoRead = 5f;

	private float interval;

	private readonly Vector3 HOME_SLIDER_OPEN_POS = new Vector3(-180f, -474f, 0f);

	private readonly Vector3 HOME_SLIDER_CLOSE_POS = new Vector3(-180f, -109f, 0f);

	private bool updateStampList;

	private float CurrentTotalHeight
	{
		get
		{
			float result = 0f;
			if (itemListData != null)
			{
				result = itemListData.currentTotalHeight;
			}
			return result;
		}
	}

	private float BasePosY
	{
		get
		{
			float result = 0f;
			if (itemListData != null)
			{
				result = itemListData.basePosY;
			}
			return result;
		}
	}

	private UIScrollView ScrollView
	{
		get
		{
			if ((UnityEngine.Object)m_ScrollView == (UnityEngine.Object)null)
			{
				m_ScrollView = GetCtrl(UI.SCR_CHAT).GetComponent<UIScrollView>();
			}
			return m_ScrollView;
		}
	}

	private Transform ScrollViewTrans
	{
		get
		{
			if ((UnityEngine.Object)m_ScrollViewTrans == (UnityEngine.Object)null)
			{
				m_ScrollViewTrans = GetCtrl(UI.SCR_CHAT);
			}
			return m_ScrollViewTrans;
		}
	}

	private UIWidget DummyDragScroll
	{
		get
		{
			if ((UnityEngine.Object)m_DummyDragScroll == (UnityEngine.Object)null)
			{
				m_DummyDragScroll = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<UIWidget>();
			}
			return m_DummyDragScroll;
		}
	}

	private BoxCollider DragScrollCollider
	{
		get
		{
			if ((UnityEngine.Object)m_DragScrollCollider == (UnityEngine.Object)null)
			{
				m_DragScrollCollider = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL).GetComponent<BoxCollider>();
			}
			return m_DragScrollCollider;
		}
	}

	private Transform DragScrollTrans
	{
		get
		{
			if ((UnityEngine.Object)m_DragScrollTrans == (UnityEngine.Object)null)
			{
				m_DragScrollTrans = GetCtrl(UI.WGT_DUMMY_DRAG_SCROLL);
			}
			return m_DragScrollTrans;
		}
	}

	private UISprite BackgroundInFrame
	{
		get
		{
			if ((UnityEngine.Object)m_BackgroundInFrame == (UnityEngine.Object)null)
			{
				m_BackgroundInFrame = GetCtrl(UI.SPR_BG_IN_FRAME).GetComponent<UISprite>();
			}
			return m_BackgroundInFrame;
		}
	}

	private UIInput Input
	{
		get
		{
			if ((UnityEngine.Object)m_Input == (UnityEngine.Object)null)
			{
				m_Input = GetCtrl(UI.IPT_POST).GetComponent<UIInput>();
			}
			return m_Input;
		}
	}

	private ChatInputFrame InputFrame
	{
		get
		{
			if ((UnityEngine.Object)m_InputFrame == (UnityEngine.Object)null)
			{
				m_InputFrame = GetCtrl(UI.OBJ_INPUT_FRAME).GetComponent<ChatInputFrame>();
			}
			return m_InputFrame;
		}
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_chatitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatItem", false);
		LoadObject lo_chat_stamp_listitem = load_queue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem", false);
		talkerName = GetTalkerName(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId);
		SetLabelText(UI.Title_U, talkerName);
		SetLabelText(UI.Title_D, talkerName);
		nowPage = 0;
		loadedPage = -1;
		itemListData = new MessageItemListData(GetCtrl(UI.OBJ_ROOM_ITEM_LIST_ROOT).gameObject);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		chatItemPrefab = (lo_quest_chatitem.loadedObject as GameObject);
		chatStampListPrefab = (lo_chat_stamp_listitem.loadedObject as GameObject);
		SetSliderLimit();
		DummyDragScroll.width = 410;
		ResetStampIdList();
		Reset();
		postMessageList.Clear();
		UIScrollView scrollView = ScrollView;
		scrollView.onDragFinished = (UIScrollView.OnDragNotification)Delegate.Combine(scrollView.onDragFinished, new UIScrollView.OnDragNotification(OnDragFinished));
		ChatInputFrame inputFrame = InputFrame;
		inputFrame.onChange = (Action)Delegate.Combine(inputFrame.onChange, (Action)delegate
		{
			((_003CDoInitialize_003Ec__Iterator44)/*Error near IL_01ea: stateMachine*/)._003C_003Ef__this.OnInput();
		});
		ChatInputFrame inputFrame2 = InputFrame;
		inputFrame2.onSubmit = (Action)Delegate.Combine(inputFrame2.onSubmit, (Action)delegate
		{
			((_003CDoInitialize_003Ec__Iterator44)/*Error near IL_0216: stateMachine*/)._003C_003Ef__this.OnTouchPost();
		});
		if (MonoBehaviourSingleton<FriendManager>.I.talkUser.userId == 0)
		{
			GetCtrl(UI.SCR_STAMP_LIST).gameObject.SetActive(false);
			GetCtrl(UI.OBJ_POST_FRAME).gameObject.SetActive(false);
		}
		else
		{
			updateStampList = true;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, new Action(InitStampList));
		}
		base.Initialize();
	}

	private void Update()
	{
		if (base.isInitialized)
		{
			Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
			float w = baseClipRegion.w;
			Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
			float num = w - baseClipRegion2.y;
			Vector3 localPosition = DragScrollTrans.localPosition;
			float num2 = num + localPosition.y;
			Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
			float w2 = finalClipRegion.w;
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			float num3 = num2 - (w2 + clipOffset.y);
			BoxCollider dragScrollCollider = DragScrollCollider;
			Vector4 baseClipRegion3 = ScrollView.panel.baseClipRegion;
			dragScrollCollider.center = new Vector2(baseClipRegion3.x, 0f - num3);
			interval += Time.deltaTime;
			if (IntervalSendGetNoRead <= interval)
			{
				interval = 0f;
				DispatchEvent("SEND_GET_NOREAD_MESSAGE", null);
			}
			List<FriendMessageData> messageDetailList = MonoBehaviourSingleton<FriendManager>.I.messageDetailList;
			if (messageDetailList.Count > postMessageList.Count)
			{
				HashSet<FriendMessageData> hashSet = new HashSet<FriendMessageData>(messageDetailList);
				hashSet.ExceptWith(postMessageList);
				FriendMessageData[] array = new FriendMessageData[hashSet.Count];
				hashSet.CopyTo(array);
				long num4 = 0L;
				if (0 < postMessageList.Count)
				{
					num4 = postMessageList[0].lid;
				}
				for (int num5 = array.Length - 1; num5 >= 0; num5--)
				{
					if (num4 > array[0].lid)
					{
						messageQueue.Enqueue(new PostMessageData(array[num5], true, false));
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (num4 <= array[0].lid)
					{
						bool forceScroll = i == array.Length - 1;
						messageQueue.Enqueue(new PostMessageData(array[i], false, forceScroll));
					}
				}
				postMessageList.AddRange(array);
				postMessageList.Sort((FriendMessageData l, FriendMessageData r) => l.lid.CompareTo(r.lid));
			}
			if (0 < messageQueue.Count)
			{
				PostMessageData postMessageData = messageQueue.Dequeue();
				PostUI(postMessageData.message, postMessageData.topPosition, postMessageData.forceScroll);
				if (0 > loadedPage && messageQueue.Count == 0)
				{
					loadedPage = 0;
				}
			}
		}
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<FriendManager>.IsValid())
		{
			MonoBehaviourSingleton<FriendManager>.I.ResetUser();
		}
		base.Exit();
	}

	private string GetTalkerName(int user_id)
	{
		if (MonoBehaviourSingleton<FriendManager>.I.talkUser.userId == user_id)
		{
			return MonoBehaviourSingleton<FriendManager>.I.talkUser.name;
		}
		return string.Empty;
	}

	private void OnQuery_SEND_GET_MESSAGE_DETAIL()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, nowPage, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
			if (is_success)
			{
				ScrollView.DisableSpring();
				RefreshUI();
				loadedPage = nowPage;
			}
		});
	}

	private void OnQuery_SEND_GET_NOREAD_MESSAGE()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetNoreadMessage(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_SEND()
	{
		string text = GameSection.GetEventData() as string;
		if (string.IsNullOrEmpty(text))
		{
			text = string.Empty;
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, text, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_SEND_STAMP()
	{
		int num = 1;
		object eventData = GameSection.GetEventData();
		if (eventData != null)
		{
			num = (int)eventData;
		}
		string message = "[STAMP]" + num.ToString();
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendFriendMessage(MonoBehaviourSingleton<FriendManager>.I.talkUser.userId, message, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void PostUI(FriendMessageData data, bool topPosition, bool forceScroll)
	{
		if (data.message.StartsWith("[STAMP]"))
		{
			string message = data.message;
			message = message.Replace("[STAMP]", string.Empty);
			PostUIStamp(data.fromUserId, int.Parse(message), topPosition, forceScroll);
		}
		else
		{
			PostUIMessage(data.fromUserId, data.message, topPosition, forceScroll);
		}
	}

	private void PostUIMessage(int userId, string message, bool topPosition, bool forceScroll)
	{
		AddNextChatItem(delegate(ChatItem chatItem)
		{
			chatItem.Init(userId, talkerName, message);
		}, topPosition, forceScroll);
	}

	private void PostUIStamp(int userId, int stampId, bool topPosition, bool forceScroll)
	{
		if (IsValidStampId(stampId))
		{
			StampTable.Data data = Singleton<StampTable>.I.GetData((uint)stampId);
			if (data != null)
			{
				AddNextChatItem(delegate(ChatItem chatItem)
				{
					chatItem.Init(userId, talkerName, stampId);
				}, topPosition, forceScroll);
			}
		}
	}

	private void AddNextChatItem(Action<ChatItem> initializer, bool topPosition, bool forceScroll)
	{
		if (!((UnityEngine.Object)chatItemPrefab == (UnityEngine.Object)null))
		{
			MessageItemListData messageItemListData = itemListData;
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
				float currentTotalHeight = messageItemListData.currentTotalHeight;
				Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
				float num3 = currentTotalHeight + baseClipRegion.y;
				Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
				float num4 = num3 - baseClipRegion2.w * 0.5f;
				Vector3 localPosition = ScrollViewTrans.localPosition;
				float y = localPosition.y;
				Vector2 clipOffset = ScrollView.panel.clipOffset;
				float num5 = num4 + (y + clipOffset.y);
				Vector2 clipSoftness = ScrollView.panel.clipSoftness;
				float num6 = num5 + clipSoftness.y;
				ForceScroll(num6 - messageItemListData.basePosY, true);
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
	}

	private void UpdateDummyDragScroll()
	{
		if (ScrollView.panel.height > CurrentTotalHeight)
		{
			DummyDragScroll.height = (int)(ScrollView.panel.height - 20f);
		}
		else
		{
			DummyDragScroll.height = (int)(CurrentTotalHeight - 20f);
		}
		Transform dragScrollTrans = DragScrollTrans;
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		dragScrollTrans.localPosition = new Vector3(clipOffset.x, BasePosY - CurrentTotalHeight, 0f);
		BoxCollider dragScrollCollider = DragScrollCollider;
		Vector4 finalClipRegion = ScrollView.panel.finalClipRegion;
		float z = finalClipRegion.z;
		Vector4 finalClipRegion2 = ScrollView.panel.finalClipRegion;
		float w = finalClipRegion2.w;
		Vector2 clipSoftness = ScrollView.panel.clipSoftness;
		dragScrollCollider.size = new Vector3(z, w - clipSoftness.y * 2f, 0f);
	}

	private void ForceScroll(float newHeight, bool useSpring)
	{
		ScrollView.DisableSpring();
		if (useSpring)
		{
			SpringPanel.Begin(ScrollView.gameObject, Vector3.up * newHeight, 20f);
		}
		else
		{
			Vector2 clipOffset = ScrollView.panel.clipOffset;
			Vector3 localPosition = ScrollViewTrans.localPosition;
			float num = localPosition.y + clipOffset.y;
			ScrollViewTrans.localPosition = Vector3.up * newHeight;
			clipOffset.y = 0f - newHeight + num;
			ScrollView.panel.clipOffset = clipOffset;
		}
	}

	private void SetSliderLimit()
	{
		UIPanel component = GetCtrl(UI.WGT_SLIDE_LIMIT).GetComponent<UIPanel>();
		UIRect.AnchorPoint topAnchor = component.topAnchor;
		Vector3 hOME_SLIDER_OPEN_POS = HOME_SLIDER_OPEN_POS;
		topAnchor.absolute = (int)hOME_SLIDER_OPEN_POS.y + 9;
		UIRect.AnchorPoint bottomAnchor = component.bottomAnchor;
		Vector3 hOME_SLIDER_CLOSE_POS = HOME_SLIDER_CLOSE_POS;
		bottomAnchor.absolute = (int)hOME_SLIDER_CLOSE_POS.y - 49;
	}

	private void Reset()
	{
		Input.value = string.Empty;
		InputFrame.Reset();
		UpdateAnchors();
		UpdateWindowSize();
	}

	public void UpdateWindowSize()
	{
		float currentTotalHeight = CurrentTotalHeight;
		Vector4 baseClipRegion = ScrollView.panel.baseClipRegion;
		float num = currentTotalHeight + baseClipRegion.y;
		Vector4 baseClipRegion2 = ScrollView.panel.baseClipRegion;
		float num2 = num - baseClipRegion2.w * 0.5f;
		Vector3 localPosition = ScrollViewTrans.localPosition;
		float y = localPosition.y;
		Vector2 clipOffset = ScrollView.panel.clipOffset;
		float num3 = num2 + (y + clipOffset.y);
		Vector2 clipSoftness = ScrollView.panel.clipSoftness;
		ForceScroll(num3 + clipSoftness.y, false);
		UpdateDummyDragScroll();
	}

	private void OnDragFinished()
	{
		if (MonoBehaviourSingleton<FriendManager>.I.messagePageMax - 1 > nowPage && nowPage == loadedPage && CurrentTotalHeight >= ScrollView.panel.height)
		{
			Bounds bounds = ScrollView.bounds;
			Vector3 vector = ScrollView.panel.CalculateConstrainOffset(bounds.min, bounds.max);
			if (30f <= vector.y)
			{
				nowPage++;
				DispatchEvent("SEND_GET_MESSAGE_DETAIL", null);
			}
		}
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
			SetGrid(UI.GRD_STAMP_LIST, null, count, true, CreateStampItem, InitStampItem);
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
		StampTable.Data data = Singleton<StampTable>.I.GetData((uint)stampId);
		if (data == null)
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
				DispatchEvent("SEND_STAMP", item.StampId);
			});
		}
	}

	public void OnTouchPost()
	{
		string value = Input.value;
		if (value.Length != 0)
		{
			DispatchEvent("SEND", value);
			Input.value = string.Empty;
			InputFrame.FrameResize();
		}
	}

	public void OnInput()
	{
		InputFrame.FrameResize();
		string value = Input.value;
		SetActive(UI.LBL_DEFAULT, string.IsNullOrEmpty(value));
	}
}
