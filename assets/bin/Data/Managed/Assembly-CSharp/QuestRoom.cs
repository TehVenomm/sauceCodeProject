using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRoom : GameSection
{
	private enum UI
	{
		LBL_QUEST_NAME,
		LBL_ROOM_ID,
		SPR_WEAK,
		LBL_NON_WEAK,
		BTN_INVITE,
		GRD_PLAYER_INFO,
		LBL_READY,
		BTN_READY,
		BTN_READY_BACK,
		TGL_READY,
		BTN_OWNER_READY,
		BTN_BACK,
		LBL_NAME,
		LBL_LV,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_USER_READY,
		SPR_USER_READY_WAIT,
		SPR_USER_EQUIP_CHANGE,
		SPR_USER_EMPTY,
		SPR_USER_BATTLE,
		BTN_EMO_0,
		BTN_EMO_1,
		BTN_EMO_2,
		SPR_WEAPON_1,
		SPR_WEAPON_2,
		SPR_WEAPON_3,
		BTN_NAME_BG,
		BTN_FRAME,
		LBL_USER_LIMIT_NUM,
		BTN_CHAT,
		OBJ_CHAT,
		OBJ_STAMP_TWEEN,
		OBJ_TEXT_TWEEN,
		TEX_CHAT_STAMP,
		LBL_CHAT_TEXT,
		SPR_CHAT_TEXT_BG,
		BTN_CHANGE_PUBLIC,
		BTN_BACK_2,
		BTN_CHANGE_PUBLIC_OFF,
		OBJ_QUEST_INFO,
		OBJ_EXPLORE_INFO,
		LBL_EXPLORE_NAME,
		LBL_LIMIT_TIME,
		SPR_WEAK_EXPLORE,
		LBL_NON_WEAK_EXPLORE,
		OBJ_ENEMY,
		SPR_ENM_ELEMENT,
		OBJ_RUSH_INFO,
		LBL_RUSH_NAME,
		LBL_RUSH_LIMIT_TIME,
		LBL_RUSH_FLOORNUMBER,
		TEX_RUSH_ICON,
		SPR_TYPE_DIFFICULTY,
		OBJ_WAVE_MATCH_INFO,
		LBL_WM_TITLE,
		LBL_WM_LIMIT_TIME,
		LBL_WM_TYPE,
		LBL_WM_EVENT_TYPE,
		BTN_REPEAT_ON,
		BTN_REPEAT_OFF
	}

	private class RoomUserModelInfo
	{
		public enum ROOM_USER_STATE
		{
			NONE,
			EMPTY,
			STAY,
			READY,
			EQUIP_CHANGE,
			BATTLE
		}

		public QuestRoomUserInfo roomInfo
		{
			get;
			private set;
		}

		public int userID
		{
			get;
			private set;
		}

		public ROOM_USER_STATE roomState
		{
			get;
			private set;
		}

		public int emotionStateIndex
		{
			get;
			private set;
		}

		public RoomUserModelInfo()
		{
			emotionStateIndex = 0;
			Reset();
		}

		public void SetInfo(QuestRoomUserInfo room_info, ROOM_USER_STATE state, int id)
		{
			roomInfo = room_info;
			roomState = state;
			userID = id;
		}

		public void Reset()
		{
			roomInfo = null;
			roomState = ROOM_USER_STATE.EMPTY;
			userID = 0;
		}

		public bool SetStateIndex(int index, int max)
		{
			if (index < 0 || index > max)
			{
				return false;
			}
			emotionStateIndex = index;
			return true;
		}

		public bool IsSameRoomState(ROOM_USER_STATE state, int user_id)
		{
			return state == roomState && userID == user_id;
		}
	}

	private class ChatBalloon
	{
		private int position;

		private UIPanel balloonPanel;

		private UITweener chatTextTween;

		private UITweener chatStampTween;

		private UILabel chatTextLabel;

		private UISprite chatTextBG;

		private UITexture chatStampTexture;

		private MonoBehaviour behaviour;

		private bool isText;

		private bool isStamp;

		private EventDelegate onFinishText;

		private EventDelegate onFinishStamp;

		private IEnumerator loading;

		public int userId
		{
			get;
			private set;
		}

		public ChatBalloon(MonoBehaviour behaviour)
		{
			this.behaviour = behaviour;
			userId = -1;
			CreateDelegate();
		}

		private void CreateDelegate()
		{
			onFinishText = new EventDelegate(ResetText);
			onFinishStamp = new EventDelegate(ResetStamp);
		}

		public void Init(UIPanel balloonPanel, UITweener chat_text_tween, UITweener chat_stamp_tween, UILabel chat_text_label, UISprite chat_text_bg, UITexture chat_stamp_texture)
		{
			this.balloonPanel = balloonPanel;
			if (chatTextTween != null)
			{
				ResetText();
				chatTextTween.RemoveOnFinished(onFinishText);
			}
			if (chatStampTween != null)
			{
				ResetStamp();
				chatStampTween.RemoveOnFinished(onFinishStamp);
			}
			chatTextTween = chat_text_tween;
			chatStampTween = chat_stamp_tween;
			chatTextLabel = chat_text_label;
			chatTextBG = chat_text_bg;
			chatStampTexture = chat_stamp_texture;
			chatTextTween.AddOnFinished(onFinishText);
			chatStampTween.AddOnFinished(onFinishStamp);
			Reset();
		}

		public void SetUser(int user_id)
		{
			userId = user_id;
			Reset();
		}

		public void SetPosition(int position)
		{
			if (this.position != position)
			{
				this.position = position;
				Reset();
			}
		}

		public void UpdateDepth(int adddepth)
		{
			balloonPanel.depth = balloonPanel.parentPanel.depth + adddepth;
		}

		public void Reset()
		{
			ResetText();
			ResetStamp();
		}

		private void ResetIfNeeded()
		{
			if (isText)
			{
				ResetText();
			}
			if (isStamp)
			{
				ResetStamp();
			}
		}

		public void ShowStamp(int stampId)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			ResetIfNeeded();
			isStamp = true;
			loading = LoadStamp(stampId);
			behaviour.StartCoroutine(loading);
		}

		private IEnumerator LoadStamp(int stampId)
		{
			LoadingQueue load_queue = new LoadingQueue(behaviour);
			LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
			yield return (object)load_queue.Wait();
			Texture2D tex = lo_stamp.loadedObject as Texture2D;
			chatStampTexture.mainTexture = tex;
			PlayTween(chatStampTween);
		}

		public void ShowText(string message)
		{
			ResetIfNeeded();
			isText = true;
			chatTextLabel.text = message;
			FitTextBG();
			MoveTextBG();
			PlayTween(chatTextTween);
		}

		private void FitTextBG()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			UISprite uISprite = chatTextBG;
			Vector2 printedSize = chatTextLabel.printedSize;
			uISprite.width = Mathf.CeilToInt(printedSize.x) + 34;
		}

		private void MoveTextBG()
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			float num = -150f;
			float num2 = -68f;
			float num3 = 128f;
			float num4 = 390f;
			Vector3 localPosition = chatTextBG.get_transform().get_localPosition();
			if ((float)chatTextBG.width < num3)
			{
				localPosition.x = 0f - (float)chatTextBG.width * 0.5f;
				chatTextBG.get_transform().set_localPosition(localPosition);
			}
			else
			{
				if (position == 0)
				{
					localPosition.x = -68f;
				}
				else if (position == 1)
				{
					float num5 = Mathf.Max((float)chatTextBG.width - num3, 0f) / (num4 - num3);
					localPosition.x = (num - num2) * num5 + num2;
				}
				else if (position == 2)
				{
					float num6 = Mathf.Max((float)chatTextBG.width - num3, 0f) / (num4 - num3);
					localPosition.x = (num - num2) * num6 + num2;
					localPosition.x = 0f - ((float)chatTextBG.width + localPosition.x);
				}
				else if (position == 3)
				{
					localPosition.x = (float)(-(chatTextBG.width - 68));
				}
				chatTextBG.get_transform().set_localPosition(localPosition);
			}
		}

		private void PlayTween(UITweener tween)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			tween.get_gameObject().SetActive(true);
			tween.ResetToBeginning();
			tween.PlayForward();
		}

		private void ResetStamp()
		{
			isStamp = false;
			if (loading != null)
			{
				behaviour.StopCoroutine(loading);
				loading = null;
			}
			Reset(chatStampTween);
		}

		private void ResetText()
		{
			isText = false;
			Reset(chatTextTween);
		}

		private void Reset(UITweener balloonRoot)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			balloonRoot.ResetToBeginning();
			balloonRoot.get_gameObject().SetActive(false);
		}
	}

	protected class ResourceInfo
	{
		public RESOURCE_CATEGORY category;

		public string packageName;

		public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
		{
			this.category = category;
			this.packageName = packageName;
		}
	}

	private const int ROOM_MEMBER_MAX = 4;

	private object[] eventData;

	private QuestTable.QuestTableData questData;

	private bool openAfterUpdate;

	private QuestRoomObserver observer;

	private PARTY_STATUS partyStatus;

	private bool isExplore;

	private bool isRush;

	private bool isWaveMatch;

	private bool isWaveMatchEvent;

	private bool isRandomSearch;

	private bool canShowRepeatQuest;

	private IEnumerator preDownloadCoroutine;

	private bool goToInGame;

	private UI[] weaponIcon = new UI[3]
	{
		UI.SPR_WEAPON_1,
		UI.SPR_WEAPON_2,
		UI.SPR_WEAPON_3
	};

	private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[5]
	{
		"Sword",
		"Brade",
		"Lance",
		"Edge",
		"Arrow"
	};

	private RoomUserModelInfo[] roomUserModelInfo;

	private ChatBalloon[] balloons;

	private int[] chatBalloonDepth;

	private int eSetNo;

	private int selfUserId;

	private bool isTutorialRoom;

	private CharaInfo[] npcData = new CharaInfo[3];

	private bool[] loadNPC;

	private PARTY_PLAYER_STATUS[] partyPlayerStatus = new PARTY_PLAYER_STATUS[4];

	private bool section_back_event;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "FieldMapTable";
		}
	}

	public unsafe override void Initialize()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		selfUserId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		eventData = (GameSection.GetEventData() as object[]);
		bool flag = eventData != null && eventData[0] is bool;
		bool flag2 = flag && (bool)eventData[0];
		if (MonoBehaviourSingleton<PartyManager>.I.randomMatchingInfo != null)
		{
			isRandomSearch = MonoBehaviourSingleton<PartyManager>.I.randomMatchingInfo.usedSearchRandomMatching;
		}
		QuestRoomObserver questRoomObserver = this.get_gameObject().AddComponent<QuestRoomObserver>();
		bool from_search_section = flag;
		bool is_entry_pass = flag2;
		Action<string> dispatch_callback = delegate(string dispatch_event_name)
		{
			DispatchEvent(dispatch_event_name, null);
		};
		Action<string> change_event_callback = delegate(string change_event_name)
		{
			GameSection.ChangeEvent(change_event_name, null);
		};
		if (_003C_003Ef__am_0024cache1A == null)
		{
			_003C_003Ef__am_0024cache1A = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		observer = questRoomObserver.Initialize(from_search_section, is_entry_pass, dispatch_callback, change_event_callback, _003C_003Ef__am_0024cache1A, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		}, true);
		if (PartyManager.IsValidInParty())
		{
			MonoBehaviourSingleton<StatusManager>.I.SetupEventEquipSet(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
			questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
			PartyModel.QuestInfo quest = MonoBehaviourSingleton<PartyManager>.I.partyData.quest;
			isExplore = (quest.explore != null);
			isRush = (quest.rush != null);
			if (questData != null)
			{
				isWaveMatch = (questData.questType == QUEST_TYPE.WAVE);
				isWaveMatchEvent = (questData.questType == QUEST_TYPE.EVENT_WAVE);
				canShowRepeatQuest = (questData.questType == QUEST_TYPE.ORDER);
			}
			preDownloadCoroutine = StartPredownload();
			this.StartCoroutine(preDownloadCoroutine);
		}
		else
		{
			MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
		}
		SetActive((Enum)UI.OBJ_QUEST_INFO, !isExplore && !isRush && !isWaveMatch && !isWaveMatchEvent);
		SetActive((Enum)UI.OBJ_EXPLORE_INFO, isExplore);
		SetActive((Enum)UI.OBJ_RUSH_INFO, isRush);
		SetActive((Enum)UI.OBJ_WAVE_MATCH_INFO, isWaveMatch || isWaveMatchEvent);
		roomUserModelInfo = new RoomUserModelInfo[4];
		for (int i = 0; i < 4; i++)
		{
			roomUserModelInfo[i] = new RoomUserModelInfo();
		}
		balloons = new ChatBalloon[4];
		for (int j = 0; j < 4; j++)
		{
			balloons[j] = new ChatBalloon(this);
		}
		chatBalloonDepth = new int[4];
		eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		partyStatus = PARTY_STATUS.NONE;
		InitializeChatUI();
		if (MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
		{
			isTutorialRoom = true;
			int[] array = new int[3]
			{
				990,
				991,
				992
			};
			loadNPC = new bool[4];
			for (int k = 0; k < 3; k++)
			{
				NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(array[k]);
				npcData[k] = new CharaInfo();
				nPCData.CopyCharaInfo(npcData[k]);
			}
			canShowRepeatQuest = false;
		}
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.enableRepeatQuest)
		{
			canShowRepeatQuest = false;
		}
		if (!MonoBehaviourSingleton<UserInfoManager>.I.repeatPartyEnable)
		{
			canShowRepeatQuest = false;
		}
		if (canShowRepeatQuest && selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			this.StartCoroutine(SetDeaultRepeat());
		}
		else
		{
			base.Initialize();
		}
	}

	private IEnumerator SetDeaultRepeat()
	{
		MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest = GameSaveData.instance.defaultRepeatPartyOn;
		bool wait = true;
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest, delegate
		{
			((_003CSetDeaultRepeat_003Ec__Iterator120)/*Error near IL_004b: stateMachine*/)._003C_003Ef__this.SetActive((Enum)UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			((_003CSetDeaultRepeat_003Ec__Iterator120)/*Error near IL_004b: stateMachine*/)._003C_003Ef__this.SetActive((Enum)UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			((_003CSetDeaultRepeat_003Ec__Iterator120)/*Error near IL_004b: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public override void Exit()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (goToInGame)
		{
			base.Exit();
		}
		else
		{
			MonoBehaviourSingleton<StatusManager>.I.ClearEventEquipSet();
			this.StartCoroutine(DoExit());
		}
	}

	private IEnumerator DoExit()
	{
		if (PartyManager.IsValidInParty())
		{
			bool wait = true;
			MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate
			{
				((_003CDoExit_003Ec__Iterator121)/*Error near IL_0037: stateMachine*/)._003Cwait_003E__0 = false;
			});
			while (wait)
			{
				yield return (object)null;
			}
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
		}
		base.Exit();
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
			{
				MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText -= OnReceiveChatText;
				MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp -= OnReceiveChatStamp;
			}
			if (goToInGame)
			{
				MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
			}
			else
			{
				MonoBehaviourSingleton<ChatManager>.I.DestroyRoomChat();
			}
		}
		if (preDownloadCoroutine != null)
		{
			this.StopCoroutine(preDownloadCoroutine);
		}
		base.OnDestroy();
	}

	public unsafe override void UpdateUI()
	{
		SetActive((Enum)UI.BTN_BACK_2, false);
		if (PartyManager.IsValidInParty() && questData != null)
		{
			if (partyStatus == PARTY_STATUS.NONE)
			{
				partyStatus = MonoBehaviourSingleton<PartyManager>.I.GetStatus();
			}
			if (partyStatus != PARTY_STATUS.WAITING || MonoBehaviourSingleton<PartyManager>.I.GetStatus() < PARTY_STATUS.PLAYING)
			{
				SetLabelText((Enum)UI.LBL_QUEST_NAME, questData.questText);
				int num = 0;
				QuestTable.QuestTableData questTableData = null;
				if (isExplore)
				{
					int mainQuestId = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.mainQuestId;
					questTableData = Singleton<QuestTable>.I.GetQuestData((uint)mainQuestId);
					num = questTableData.GetMainEnemyID();
				}
				else
				{
					num = questData.GetMainEnemyID();
				}
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)num);
				int num2 = 6;
				if (enemyData != null)
				{
					num2 = (int)enemyData.weakElement;
				}
				SetElementSprite((Enum)UI.SPR_WEAK, num2);
				SetActive((Enum)UI.LBL_NON_WEAK, num2 == 6);
				if (isExplore)
				{
					SetLabelText((Enum)UI.LBL_EXPLORE_NAME, questTableData.questText);
					SetElementSprite((Enum)UI.SPR_WEAK_EXPLORE, num2);
					SetActive((Enum)UI.LBL_NON_WEAK_EXPLORE, num2 == 6);
					ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, GetCtrl(UI.OBJ_ENEMY), ELEMENT_TYPE.MAX, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
					itemIcon.SetDepth(7);
					SetElementSprite((Enum)UI.SPR_ENM_ELEMENT, (int)enemyData.element);
					int num3 = (int)questTableData.limitTime;
					SetLabelText((Enum)UI.LBL_LIMIT_TIME, $"{num3 / 60:D2}:{num3 % 60:D2}");
				}
				if (isRush)
				{
					Transform ctrl = GetCtrl(UI.TEX_RUSH_ICON);
					if (ctrl != null)
					{
						UITexture component = ctrl.GetComponent<UITexture>();
						if (component != null)
						{
							ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int)questData.rushIconId));
						}
					}
					int num4 = (int)questData.limitTime;
					SetLabelText((Enum)UI.LBL_RUSH_LIMIT_TIME, $"{num4 / 60:D2}:{num4 % 60:D2}");
					SetLabelText((Enum)UI.LBL_RUSH_NAME, questData.questText);
					SetActive((Enum)UI.LBL_RUSH_FLOORNUMBER, false);
				}
				if (isWaveMatch || isWaveMatchEvent)
				{
					int num5 = (int)questData.limitTime;
					SetLabelText((Enum)UI.LBL_WM_TITLE, questData.questText);
					SetLabelText((Enum)UI.LBL_WM_LIMIT_TIME, $"{num5 / 60:D2}:{num5 % 60:D2}");
					SetActive((Enum)UI.LBL_WM_TYPE, isWaveMatch);
					SetActive((Enum)UI.LBL_WM_EVENT_TYPE, isWaveMatchEvent);
				}
				if (isExplore || isRush || isWaveMatch || isWaveMatchEvent)
				{
					DeliveryTable.DeliveryData deliveryTableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questData.questID);
					SetActive((Enum)UI.SPR_TYPE_DIFFICULTY, (deliveryTableDataFromQuestId != null && deliveryTableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
				}
				SetLabelText((Enum)UI.LBL_ROOM_ID, MonoBehaviourSingleton<PartyManager>.I.GetPartyNumber());
				SetGrid(UI.GRD_PLAYER_INFO, string.Empty, 4, false, new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(selfUserId);
				bool is_active2 = slotInfoByUserId.status == 21;
				bool flag = selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
				bool flag2 = MonoBehaviourSingleton<PartyManager>.I.GetStatus() >= PARTY_STATUS.PLAYING;
				if (flag)
				{
					SetActive((Enum)UI.BTN_READY, false);
					SetActive((Enum)UI.BTN_READY_BACK, false);
					SetActive((Enum)UI.BTN_OWNER_READY, true);
					SetActive((Enum)UI.BTN_INVITE, true);
					bool flag3 = MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.partySetting != null && MonoBehaviourSingleton<PartyManager>.I.partySetting.isLock;
					SetActive((Enum)UI.BTN_CHANGE_PUBLIC, flag3);
					SetActive((Enum)UI.BTN_CHANGE_PUBLIC_OFF, !flag3);
				}
				else
				{
					SetActive((Enum)UI.BTN_READY, !flag2);
					SetActive((Enum)UI.BTN_READY_BACK, !flag2);
					SetActive((Enum)UI.BTN_OWNER_READY, flag2);
					if (!flag2)
					{
						SetToggleButton((Enum)UI.TGL_READY, is_active2, (Action<bool>)delegate(bool is_active)
						{
							if (is_active)
							{
								DispatchEvent("READY", null);
							}
							else
							{
								DispatchEvent("READY_BACK", null);
							}
						});
					}
					SetActive((Enum)UI.BTN_INVITE, false);
					SetActive((Enum)UI.BTN_CHANGE_PUBLIC, false);
					SetActive((Enum)UI.BTN_CHANGE_PUBLIC_OFF, false);
				}
				if (questData.questType == QUEST_TYPE.GATE)
				{
					string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0u);
					string text = string.Empty;
					if (enemyData != null)
					{
						text = string.Format(format, questData.GetMainEnemyLv(), enemyData.name);
					}
					SetLabelText((Enum)UI.LBL_QUEST_NAME, text);
				}
				openAfterUpdate = false;
				if (canShowRepeatQuest)
				{
					SetActive((Enum)UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
					SetActive((Enum)UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
				}
				else
				{
					SetActive((Enum)UI.BTN_REPEAT_OFF, false);
					SetActive((Enum)UI.BTN_REPEAT_ON, false);
				}
			}
		}
	}

	private void UpdateRoomUserInfo(Transform trans, int index)
	{
		QuestRoomUserInfo component = trans.GetComponent<QuestRoomUserInfo>();
		if (!(component == null))
		{
			RoomUserModelInfo.ROOM_USER_STATE rOOM_USER_STATE = RoomUserModelInfo.ROOM_USER_STATE.EMPTY;
			PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(index);
			CharaInfo org = slotInfoByIndex?.userInfo;
			if (slotInfoByIndex == null || org == null)
			{
				SetLabelText(trans, UI.LBL_NAME, string.Empty);
				SetLabelText(trans, UI.LBL_LV, string.Empty);
				SetLabelText(trans, UI.LBL_ATK, string.Empty);
				SetLabelText(trans, UI.LBL_DEF, string.Empty);
				SetLabelText(trans, UI.LBL_HP, string.Empty);
				SetActive(trans, UI.SPR_WEAPON_1, false);
				SetActive(trans, UI.SPR_WEAPON_2, false);
				SetActive(trans, UI.SPR_WEAPON_3, false);
				rOOM_USER_STATE = RoomUserModelInfo.ROOM_USER_STATE.EMPTY;
				if (isTutorialRoom)
				{
					if (npcData[index - 1] != null && !loadNPC[index])
					{
						loadNPC[index] = true;
						component.DeleteModel();
						component.LoadModel(index, npcData[index - 1]);
					}
					SetLabelText(trans, UI.LBL_NAME, "NPC " + index);
					rOOM_USER_STATE = RoomUserModelInfo.ROOM_USER_STATE.NONE;
				}
				else
				{
					component.LoadModel(index, null);
				}
			}
			else
			{
				if (isTutorialRoom)
				{
					loadNPC[index] = false;
				}
				if (org.userId == selfUserId && eSetNo != MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo)
				{
					eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
					StageObjectManager.CreatePlayerInfo createPlayerInfo = MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo();
					if (createPlayerInfo != null)
					{
						org = createPlayerInfo.charaInfo;
					}
				}
				if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
				{
					AssignedEquipmentTable.MergeAssignedEquip(ref org, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
				}
				int ownerUserId = MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId();
				bool flag = org.userId == ownerUserId;
				bool flag2 = slotInfoByIndex.status == 21;
				bool flag3 = slotInfoByIndex.status == 30;
				bool flag4 = MonoBehaviourSingleton<PartyManager>.I.IsEquipChangeByIndex(index);
				rOOM_USER_STATE = (flag3 ? RoomUserModelInfo.ROOM_USER_STATE.BATTLE : ((!flag) ? (flag4 ? RoomUserModelInfo.ROOM_USER_STATE.EQUIP_CHANGE : ((!flag2) ? RoomUserModelInfo.ROOM_USER_STATE.STAY : RoomUserModelInfo.ROOM_USER_STATE.READY)) : RoomUserModelInfo.ROOM_USER_STATE.NONE));
				SetActive(trans, UI.SPR_WEAPON_1, false);
				SetActive(trans, UI.SPR_WEAPON_2, false);
				SetActive(trans, UI.SPR_WEAPON_3, false);
				int weapon_index = 0;
				org.equipSet.ForEach(delegate(CharaInfo.EquipItem data)
				{
					if (data != null)
					{
						EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)data.eId);
						if (equipItemData != null && equipItemData.IsWeapon())
						{
							SetActive(trans, weaponIcon[weapon_index], true);
							int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemData.type);
							SetSprite(trans, weaponIcon[weapon_index], ITEM_TYPE_ICON_SPRITE_NAME[equipmentTypeIndex]);
							weapon_index++;
						}
					}
				});
				EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(index);
				bool flag5 = roomUserModelInfo[index].userID != org.userId;
				bool flag6 = !component.IsAllSameEquip(org);
				if (flag5 || flag6 || openAfterUpdate)
				{
					if (flag6)
					{
						otherEquipSetCalculator.SetEquipSet(org.equipSet, false);
					}
					component.DeleteModel();
					component.LoadModel(index, org);
				}
				SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, org.hp, org.atk, org.def);
				SetLabelText(trans, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
				SetLabelText(trans, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
				SetLabelText(trans, UI.LBL_HP, finalStatus.hp.ToString());
				CharaInfo.ClanInfo clanInfo = org.clanInfo;
				if (clanInfo == null)
				{
					clanInfo = new CharaInfo.ClanInfo();
					clanInfo.clanId = -1;
					clanInfo.tag = string.Empty;
				}
				bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
				SetSupportEncoding(trans, UI.LBL_NAME, true);
				SetLabelText(trans, UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, org.name, org.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
				SetLabelText(trans, UI.LBL_LV, org.level.ToString());
			}
			int num = org?.userId ?? 0;
			if (!roomUserModelInfo[index].IsSameRoomState(rOOM_USER_STATE, num) || openAfterUpdate)
			{
				if (rOOM_USER_STATE == RoomUserModelInfo.ROOM_USER_STATE.EMPTY)
				{
					if (index < questData.userNumLimit)
					{
						ActiveAndTween(trans, UI.SPR_USER_EMPTY, true);
						SetActive(trans, UI.LBL_USER_LIMIT_NUM, false);
					}
					else
					{
						ActiveAndTween(trans, UI.SPR_USER_EMPTY, false);
						SetActive(trans, UI.LBL_USER_LIMIT_NUM, true);
					}
				}
				else
				{
					ActiveAndTween(trans, UI.SPR_USER_EMPTY, false);
					SetActive(trans, UI.LBL_USER_LIMIT_NUM, false);
				}
				ActiveAndTween(trans, UI.SPR_USER_BATTLE, rOOM_USER_STATE == RoomUserModelInfo.ROOM_USER_STATE.BATTLE);
				ActiveAndTween(trans, UI.SPR_USER_READY, rOOM_USER_STATE == RoomUserModelInfo.ROOM_USER_STATE.READY);
				ActiveAndTween(trans, UI.SPR_USER_READY_WAIT, rOOM_USER_STATE == RoomUserModelInfo.ROOM_USER_STATE.STAY);
				ActiveAndTween(trans, UI.SPR_USER_EQUIP_CHANGE, rOOM_USER_STATE == RoomUserModelInfo.ROOM_USER_STATE.EQUIP_CHANGE);
			}
			roomUserModelInfo[index].Reset();
			roomUserModelInfo[index].SetInfo(component, rOOM_USER_STATE, num);
		}
	}

	private void SetupChatBalloon(Transform t, int index)
	{
		ChatBalloon chatBalloon = balloons[index];
		UIPanel component = FindCtrl(t, UI.OBJ_CHAT).GetComponent<UIPanel>();
		UITweener component2 = FindCtrl(t, UI.OBJ_TEXT_TWEEN).GetComponent<UITweener>();
		UITweener component3 = FindCtrl(t, UI.OBJ_STAMP_TWEEN).GetComponent<UITweener>();
		UILabel component4 = FindCtrl(t, UI.LBL_CHAT_TEXT).GetComponent<UILabel>();
		UISprite component5 = FindCtrl(t, UI.SPR_CHAT_TEXT_BG).GetComponent<UISprite>();
		UITexture component6 = FindCtrl(t, UI.TEX_CHAT_STAMP).GetComponent<UITexture>();
		chatBalloon.Init(component, component2, component3, component4, component5, component6);
	}

	private void UpdateChatBalloon(int index)
	{
		ChatBalloon chatBalloon = balloons[index];
		chatBalloon.SetPosition(index);
		PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(index);
		int num = -1;
		if (slotInfoByIndex != null && slotInfoByIndex.userInfo != null)
		{
			num = slotInfoByIndex.userInfo.userId;
		}
		if (num != chatBalloon.userId)
		{
			chatBalloon.SetUser(num);
		}
	}

	private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
	{
		SetActive(root, _enum, is_active);
		if (is_active)
		{
			ResetTween(root, _enum, 0);
			PlayTween(root, _enum, true, null, false, 0);
		}
	}

	protected void OnQuery_QuestRoomChangePublicDialog_YES()
	{
		GameSection.StayEvent();
		PartyManager.PartySetting partySetting = MonoBehaviourSingleton<PartyManager>.I.partySetting;
		partySetting.isLock = false;
		partySetting.level = partySetting.reserveLimitLevel;
		MonoBehaviourSingleton<PartyManager>.I.SendEdit(partySetting, delegate(bool isSuccess)
		{
			SetActive((Enum)UI.BTN_CHANGE_PUBLIC, false);
			SetActive((Enum)UI.BTN_CHANGE_PUBLIC_OFF, true);
			GameSection.ResumeEvent(isSuccess, null);
		});
	}

	protected void OnQuery_CHANGE_EQUIP()
	{
		GameSection.StayEvent();
		int i = 0;
		for (int num = 4; i < num; i++)
		{
			roomUserModelInfo[i].Reset();
		}
		MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(true, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
			GameSection.SetEventData(new object[2]
			{
				observer.fromSearchSection,
				observer.isEntryPass
			});
		});
	}

	protected void OnQuery_READY()
	{
		if (selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			uint questId = 0u;
			if (questData != null)
			{
				questId = questData.questID;
			}
			if (MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(questId))
			{
				TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_START, delegate(bool b)
				{
					if (b)
					{
						DispatchEvent("QUEST_ROOM_IN_GAME", questData);
					}
				});
			}
			else
			{
				DispatchEvent("QUEST_ROOM_IN_GAME", questData);
			}
		}
		else
		{
			DispatchEvent("READY_GO", null);
		}
	}

	protected void OnQuery_READY_GO()
	{
		switch (MonoBehaviourSingleton<PartyManager>.I.GetStatus())
		{
		case PARTY_STATUS.WAITING:
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendReady(true, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
			break;
		case PARTY_STATUS.PLAYING:
		case PARTY_STATUS.PLAYING_MATCH_CLOSE:
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty(delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
				DispatchEvent("QUEST_ROOM_IN_GAME", questData);
			}, false);
			break;
		default:
			GameSection.StopEvent();
			break;
		}
	}

	protected void OnQuery_READY_BACK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendReady(false, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected void OnQuery_SECTION_BACK()
	{
		if (selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			DispatchEvent("BACK_HOST", null);
		}
		else if (isExplore && !observer.fromSearchSection)
		{
			DispatchEvent("BACK_CLIENT_EXPLORE", null);
		}
		else if (isRush && !observer.fromSearchSection)
		{
			DispatchEvent("BACK_CLIENT_RUSH", null);
		}
		else
		{
			DispatchEvent("SECTION_BACK_DO", null);
		}
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
		}
	}

	protected void OnQuery_SECTION_BACK_DO()
	{
		section_back_event = true;
		if (isRandomSearch)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "QuestAcceptSearchMatchingFailed")
			{
				GameSection.ChangeEvent("BACK_SEARCH_MATCHING_FAILED", null);
			}
			else
			{
				GameSection.ChangeEvent("BACK_SEARCH_MATCHING", null);
			}
		}
		else if (observer.fromSearchSection)
		{
			GameSection.ChangeEvent((!observer.isEntryPass) ? "BACK_ROOM_SEARCH" : "BACK_ROOM_PASS_ENTRY", null);
		}
		else if (isExplore)
		{
			GameSection.ChangeEvent("BACK_CLIENT_EXPLORE", null);
		}
		else if (isRush)
		{
			GameSection.ChangeEvent("BACK_CLIENT_RUSH", null);
		}
		else
		{
			GameSection.SetEventData(eventData);
		}
		bool flag = false;
		if (!flag && !section_back_event)
		{
			flag = true;
		}
		if (!flag && !PartyManager.IsValidInParty())
		{
			flag = true;
		}
		if (!flag)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate(bool b)
			{
				GameSection.ResumeEvent(b, null);
				MonoBehaviourSingleton<CoopManager>.I.Clear();
				QuestRoomObserver.OffObserve();
				MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInLounge();
			});
		}
	}

	private void OnQuery_QuestAcceptRoomBackHost_YES()
	{
		if (isExplore)
		{
			RequestEvent("BACK_HOST_EXPLORE", null);
		}
		else if (isRush)
		{
			RequestEvent("BACK_HOST_RUSH", null);
		}
		else
		{
			RequestEvent("SECTION_BACK_DO_HOST", null);
		}
	}

	private void OnQuery_SECTION_BACK_DO_HOST()
	{
		QuestRoomObserver.OffObserve();
	}

	private void OnQuery_BACK_HOST_EXPLORE()
	{
		QuestRoomObserver.OffObserve();
	}

	protected override void OnQuery_QUEST_ROOM_IN_GAME()
	{
		goToInGame = true;
		base.OnQuery_QUEST_ROOM_IN_GAME();
	}

	protected override void OnOpen()
	{
		MonoBehaviourSingleton<UIManager>.I.mainChat.Open(UITransition.TYPE.OPEN);
		openAfterUpdate = true;
		if (loadNPC != null)
		{
			int i = 0;
			for (int num = loadNPC.Length; i < num; i++)
			{
				loadNPC[i] = false;
			}
		}
	}

	protected override void OnCloseStart()
	{
		MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
	}

	public override void OnNotify(NOTIFY_FLAG notify_flags)
	{
		if ((notify_flags & NOTIFY_FLAG.RECEIVE_COOP_ROOM_START) != (NOTIFY_FLAG)0L)
		{
			goToInGame = true;
			DispatchEvent("QUEST_ROOM_IN_GAME", questData);
			MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty(delegate
			{
			}, false);
		}
		if ((notify_flags & NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE) != (NOTIFY_FLAG)0L && canShowRepeatQuest)
		{
			SetActive((Enum)UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
			SetActive((Enum)UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
		}
		base.OnNotify(notify_flags);
	}

	protected void OnQuery_MEMBER_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		PartyModel.SlotInfo slotInfoByIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(num);
		if (slotInfoByIndex == null || slotInfoByIndex.userInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
			playerRecord.id = num + 1;
			playerRecord.isNPC = false;
			playerRecord.isSelf = (slotInfoByIndex.userInfo.userId == selfUserId);
			playerRecord.animID = 90;
			playerRecord.charaInfo = slotInfoByIndex.userInfo;
			if (MonoBehaviourSingleton<StatusManager>.I.HasEventEquipSet())
			{
				AssignedEquipmentTable.MergeAssignedEquip(ref playerRecord.charaInfo, MonoBehaviourSingleton<StatusManager>.I.EventEquipSet);
			}
			playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo(playerRecord.charaInfo, true, true, true, true);
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num;
			GameSection.SetEventData(new object[3]
			{
				playerRecord,
				observer.fromSearchSection,
				observer.isEntryPass
			});
		}
	}

	private void Update()
	{
		if (base.isOpen && !section_back_event && !(observer == null) && observer.IsValidParty() && observer.IsConnect())
		{
			bool flag = false;
			for (int i = 0; i < 4; i++)
			{
				if (!flag)
				{
					PARTY_PLAYER_STATUS pARTY_PLAYER_STATUS = PARTY_PLAYER_STATUS.NONE;
					if (MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(i) != null)
					{
						pARTY_PLAYER_STATUS = (PARTY_PLAYER_STATUS)MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByIndex(i).status;
					}
					if (partyPlayerStatus[i] != pARTY_PLAYER_STATUS)
					{
						flag = true;
					}
					partyPlayerStatus[i] = pARTY_PLAYER_STATUS;
				}
			}
			if (flag)
			{
				RefreshUI();
			}
		}
	}

	protected void OnQuery_QuestRoomInvalid_OK()
	{
		observer.SetupBackSectionEvent();
	}

	private void InitializeChatUI()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<ChatManager>.I.CreateRoomChatWithParty();
		MonoBehaviourSingleton<ChatManager>.I.roomChat.JoinRoom(0);
		if (MonoBehaviourSingleton<ChatManager>.I.roomChat != null)
		{
			MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveText += OnReceiveChatText;
			MonoBehaviourSingleton<ChatManager>.I.roomChat.onReceiveStamp += OnReceiveChatStamp;
		}
		UIButton component = GetCtrl(UI.BTN_CHAT).GetComponent<UIButton>();
		if (!(component == null))
		{
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				component.get_gameObject().SetActive(false);
			}
			else if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
			{
				MonoBehaviourSingleton<UIManager>.I.mainChat.SetRoomChatNameType(false);
				MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver(this);
				component.onClick.Clear();
				component.onClick.Add(new EventDelegate(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly_NotOneShot));
			}
			else
			{
				component.get_gameObject().SetActive(false);
			}
		}
	}

	public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != 0)
			{
				SetBadge((Enum)UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNumWithoutRoom(), 1, 5, -10, false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(true);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
			if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) != 0)
			{
				GetCtrl(UI.BTN_CHAT).get_gameObject().SetActive(false);
			}
		}
	}

	private void UpdateChatBalloonDepth()
	{
		for (int i = 0; i < balloons.Length; i++)
		{
			balloons[i].UpdateDepth(chatBalloonDepth[i]);
		}
	}

	private void ChatBalloonMoveForward(int index)
	{
		for (int i = 0; i < chatBalloonDepth.Length; i++)
		{
			chatBalloonDepth[i]--;
		}
		chatBalloonDepth[index] = 4;
		UpdateChatBalloonDepth();
	}

	private void OnReceiveChatText(int userId, string userName, string message)
	{
		int slotIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotIndex(userId);
		if (slotIndex >= 0)
		{
			ChatBalloonMoveForward(slotIndex);
			balloons[slotIndex].ShowText(message);
		}
	}

	private void OnReceiveChatStamp(int userId, string userName, int stampId)
	{
		int slotIndex = MonoBehaviourSingleton<PartyManager>.I.GetSlotIndex(userId);
		if (slotIndex >= 0)
		{
			ChatBalloonMoveForward(slotIndex);
			balloons[slotIndex].ShowStamp(stampId);
		}
	}

	protected IEnumerator StartPredownload()
	{
		List<ResourceInfo> list = new List<ResourceInfo>();
		uint mapId = questData.mapId;
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
		if (fieldMapData != null)
		{
			string stageName = fieldMapData.stageName;
			if (string.IsNullOrEmpty(stageName))
			{
				stageName = "ST011D_01";
			}
			StageTable.StageData stageData = Singleton<StageTable>.I.GetData(stageName);
			if (stageData != null)
			{
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, stageData.scene));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, stageData.sky));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffect));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.cameraLinkEffectY0));
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.rootEffect));
				for (int i2 = 0; i2 < 8; i2++)
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, stageData.useEffects[i2]));
				}
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.enemyID[0]);
				int bodyId = enemyData.modelId;
				string bodyName = ResourceName.GetEnemyBody(bodyId);
				string mateName = ResourceName.GetEnemyMaterial(bodyId);
				string animName = ResourceName.GetEnemyAnim(enemyData.animId);
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, bodyName));
				if (!string.IsNullOrEmpty(mateName))
				{
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, bodyName));
				}
				list.Add(new ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, animName));
				LoadingQueue load_queue = new LoadingQueue(this);
				foreach (ResourceInfo item in list)
				{
					if (!string.IsNullOrEmpty(item.packageName))
					{
						ResourceManager.downloadOnly = true;
						load_queue.Load(item.category, item.packageName, null, false);
						ResourceManager.downloadOnly = false;
						yield return (object)load_queue.Wait();
					}
				}
				if (MonoBehaviourSingleton<ResourceManager>.I.cache.PreloadedInGameResouces.Count < 64)
				{
					GlobalSettingsManager.LinkResources globalResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
					InGameSettingsManager.UseResources useResources2 = globalResources.inGameCommonResources;
					for (int n = 0; n < useResources2.effects.Length; n++)
					{
						list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, useResources2.effects[n]));
					}
					for (int m = 0; m < useResources2.uiEffects.Length; m++)
					{
						list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_UI, useResources2.uiEffects[m]));
					}
					useResources2 = globalResources.inGameQuestResources;
					for (int l = 0; l < useResources2.effects.Length; l++)
					{
						list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, useResources2.effects[l]));
					}
					for (int k = 0; k < useResources2.uiEffects.Length; k++)
					{
						list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_UI, useResources2.uiEffects[k]));
					}
					for (int j = 0; j < globalResources.stunnedEffectList.Length; j++)
					{
						list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.stunnedEffectList[j]));
					}
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.battleStartEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.changeWeaponEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.spActionStartEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.arrowBleedOtherEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.shadowSealingEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_frozen_01"));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.enemyParalyzeHitEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.enemyPoisonHitEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.enemyFreezeHitEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, globalResources.enemyOtherSimpleHitEffectName));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_04"));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_shock_01"));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01"));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01"));
					list.Add(new ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01"));
					for (int i = 0; i < list.Count; i++)
					{
						if (!string.IsNullOrEmpty(list[i].packageName) && MonoBehaviourSingleton<ResourceManager>.I.cache.PreloadedInGameResouces.Count < 64)
						{
							RESOURCE_CATEGORY category = list[i].category;
							if (category == RESOURCE_CATEGORY.EFFECT_ACTION || category == RESOURCE_CATEGORY.EFFECT_UI)
							{
								load_queue.CacheEffect(list[i].category, list[i].packageName);
								yield return (object)load_queue.Wait();
								MonoBehaviourSingleton<ResourceManager>.I.cache.AddPreloadResources(list[i].packageName);
							}
						}
					}
				}
			}
		}
	}

	protected void OnQuery_REPEAT_BATTLE()
	{
		if (selfUserId == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendRepeat(!MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest, delegate(bool is_success)
			{
				SetActive((Enum)UI.BTN_REPEAT_OFF, !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
				SetActive((Enum)UI.BTN_REPEAT_ON, MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
				GameSaveData.instance.defaultRepeatPartyOn = MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest;
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}
}
