using Network;
using OnePF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShopTop : SkillInfoBase
{
	private enum UI
	{
		GRD_LIST,
		TBL_LIST,
		SCR_LIST,
		SCR_LIST_2,
		BTN_MAGI_GACHA,
		BTN_QUEST_GACHA,
		OBJ_BTN_ROOT,
		GRD_BTN,
		TBL_BTN_HOLIZONTAL,
		GRD_BTN_INNER,
		TXT_GACHA_ITEM_NUM,
		TEX_GACHA_EVENT_TITLE,
		SPR_RARITY_SSS,
		SPR_RARITY_SS,
		SPR_RARITY_S,
		SPR_RARITY_A,
		SPR_RARITY_B,
		LBL_NAME,
		LBL_DESCRIPTION,
		TEX_ENEMY_MODEL,
		LBL_ENEMY,
		LBL_ENEMY_LV,
		OBJ_REWARD_ICON_ROOT,
		OBJ_QUEST_INFO_ROOT,
		LBL_TIME,
		OBJ_SKILL_INFO_ROOT,
		LBL_SKILL_ITEM_NAME,
		LBL_TYPE_NAME,
		SPR_RARITY_BG,
		SPR_RARITY_ICON,
		SPR_EQUIP_TYPE_ICON,
		OBJ_SKILL_BANNER,
		OBJ_SKILL_MODEL_ROOT,
		TEX_SKILL_MODEL,
		TEX_SKILL_INNER_MODEL,
		TEX_SKILL_NPC_MODEL,
		TEX_SKILL_SUB_NPC_MODEL,
		BTN_GACHA,
		SPR_CRYSTAL,
		TEX_TICKET,
		LBL_CRYSTAL_PRICE,
		LBL_TICKET_PRICE,
		LBL_HAVE,
		SPR_MULTI,
		SPR_LINE,
		LBL_TITLE,
		SPR_GACHA_BUTTON_BG,
		LBL_GACHA_CAPTION,
		TEX_TICKET_HAVE,
		COUNTER_LBL,
		NUMBER_COUNTER_IMG,
		S_COUNTER,
		S_AVAILABLE,
		COUNTER_PROGRESSBAR_FOREGROUND,
		LBL_CRYSTAL_NUM,
		LBL_MORE_TICKET,
		OBJ_GUARANTEE_HEADER_ROOT,
		OBJ_GUARANTEE_FOOTER_ROOT,
		BTN_GUARANTEE_COUNT_DOWN,
		BTN_GUARANTEE_DETAIL,
		TEX_GUARANTEE_TIME,
		LBL_GACHA_DESCRIPTION,
		TEX_GACHA_BANNER,
		BTN_GACHA_BANNER,
		TEX_SKILL_BANNER,
		WGT_REWARD_EFFECT,
		SPR_NOTE_BG,
		LBL_NOTE,
		BTN_VIEW_PROBABIRITY,
		SPR_GACHA_EFFECT,
		SPR_GACHA_PLAY,
		SPR_GACHA_ICON,
		LBL_FRIEND_INVITATION_REMAIN,
		LBL_FRIEND_INVITATION_INVITED,
		LBL_FRIEND_INVITATION_TIME
	}

	public class PickUp
	{
		public int orderNo;

		protected uint gachaResultItemID;

		public uint GetGachaResultItemID()
		{
			return gachaResultItemID;
		}
	}

	public class PickUpQuest : PickUp
	{
		public uint materialID;

		public uint equipItemID;

		public uint questID
		{
			get
			{
				return gachaResultItemID;
			}
			set
			{
				gachaResultItemID = value;
			}
		}

		public PickUpQuest(int no, int quest, uint material, uint equip)
		{
			orderNo = no;
			questID = (uint)quest;
			materialID = material;
			equipItemID = equip;
		}
	}

	public class PickUpSkill : PickUp
	{
		public GachaList.GachaPickupAnim gachaAnim;

		public uint skillID
		{
			get
			{
				return gachaResultItemID;
			}
			set
			{
				gachaResultItemID = value;
			}
		}

		public PickUpSkill(int no, int skill, GachaList.GachaPickupAnim anim)
		{
			orderNo = no;
			skillID = (uint)skill;
			gachaAnim = anim;
		}
	}

	private class GachaModelInfo
	{
		public class GachaDataInfo
		{
			public int groupID;

			public int priority;

			public int showPickupIndex;

			public List<GachaList.Gacha> gachas;

			public List<GachaGuaranteeCampaignInfo> gachaGuaranteeCampaignInfos;

			public List<GachaFriendPromotionInfo> friendPromotionInfo;

			public List<PickUp> pickup;

			public string bannerImg;

			public string buttonImgId;

			public string url;

			public string note;

			public int counter = -1;

			public string expireAt;

			public NPCLoader npcLoader;

			public bool isFirstSkillListDirection = true;

			public uint rewardIconMaterialInfoID;

			public GachaGuaranteeCampaignInfo GetGachaGuaranteeCampaignInfo(List<GachaList.Gacha> gachas)
			{
				int num = gachas.Count();
				for (int i = 0; i < num; i++)
				{
					GachaList.Gacha gacha = gachas[i];
					GachaGuaranteeCampaignInfo gachaGuaranteeCampaignInfo = GetGachaGuaranteeCampaignInfo(gacha.gachaId);
					if (gachaGuaranteeCampaignInfo != null)
					{
						return gachaGuaranteeCampaignInfo;
					}
				}
				return null;
			}

			public unsafe GachaGuaranteeCampaignInfo GetGachaGuaranteeCampaignInfo(int gachaId)
			{
				if (gachaGuaranteeCampaignInfos == null)
				{
					return null;
				}
				_003CGetGachaGuaranteeCampaignInfo_003Ec__AnonStorey476 _003CGetGachaGuaranteeCampaignInfo_003Ec__AnonStorey;
				IEnumerable<GachaGuaranteeCampaignInfo> source = gachaGuaranteeCampaignInfos.Where(new Func<GachaGuaranteeCampaignInfo, bool>((object)_003CGetGachaGuaranteeCampaignInfo_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				if (_003C_003Ef__am_0024cache10 == null)
				{
					_003C_003Ef__am_0024cache10 = new Func<GachaGuaranteeCampaignInfo, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				return source.Where(_003C_003Ef__am_0024cache10).FirstOrDefault();
			}

			public GachaFriendPromotionInfo GetGachaFriendPromotionInfo(int gachaId)
			{
				if (friendPromotionInfo == null || friendPromotionInfo.Count <= 0)
				{
					return null;
				}
				int i = 0;
				for (int count = friendPromotionInfo.Count; i < count; i++)
				{
					GachaFriendPromotionInfo gachaFriendPromotionInfo = friendPromotionInfo[i];
					if (gachaFriendPromotionInfo.gachaId == gachaId)
					{
						return gachaFriendPromotionInfo;
					}
				}
				return null;
			}
		}

		public int sortPriority;

		public GACHA_TYPE type;

		public string url;

		public List<GachaDataInfo> gachaDataInfo;
	}

	private class GachaUIInfo
	{
		public int index;

		public Transform parent;

		public GachaModelInfo gachaModelInfo;
	}

	private class GuaranteeInfoUIStatus
	{
	}

	private const string STR_ENEMY_TEX_ITEM = "enemy_tex_";

	private const string STR_SKILL_TEX_ITEM = "skill_tex_";

	private const string SPR_NAME_CRYSTAL = "Juel5";

	private const string SPR_NAME_TICKET = "Ticket";

	private const float PICKUP_UPDATE_TIME = 5f;

	private string selectProductId = string.Empty;

	private bool isPurchase;

	private string pp;

	private bool _isFinishGetNativeProductlist;

	private StoreDataList _nativeStoreList;

	private List<GachaModelInfo> gachaModelInfo;

	private List<uint> pickUpMaterialIDs;

	private int pageIndex;

	private bool isSectionStarted;

	private List<GachaUIInfo> gachaUIInfoList = new List<GachaUIInfo>();

	private List<UITable> gachaBtn = new List<UITable>();

	private LoadingQueue loadQueue;

	private bool isFinished;

	private List<Coroutine> coroutineList = new List<Coroutine>();

	private bool isDoGacha;

	private int currentCrystalRequestCount;

	private float timer;

	public override bool useOnPressBackKey => true;

	public override void OnPressBackKey()
	{
		string event_name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		DispatchEvent(event_name, null);
	}

	public override void Initialize()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST || MonoBehaviourSingleton<GachaManager>.I.selectGachaType == (GACHA_TYPE)0)
		{
			pageIndex = 0;
		}
		else
		{
			pageIndex = 1;
		}
		pickUpMaterialIDs = new List<uint>();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha, "Tutorial");
			Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha.ToString()));
			MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha, "Tutorial", null, null);
		}
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		EnemyLoader.CacheUIElementEffects(load_queue);
		bool wait = true;
		MonoBehaviourSingleton<GachaManager>.I.SendGetGacha(delegate
		{
			((_003CDoInitialize_003Ec__Iterator156)/*Error near IL_0051: stateMachine*/)._003Cwait_003E__1 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		List<string> productIds = new List<string>();
		MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach(delegate(GachaList.GachaType data)
		{
			data.groups.ForEach(delegate(GachaList.GachaGroup group)
			{
				group.gachas.ForEach(delegate(GachaList.Gacha gacha)
				{
					if (!string.IsNullOrEmpty(gacha.productId))
					{
						((_003CDoInitialize_003Ec__Iterator156)/*Error near IL_009f: stateMachine*/)._003CproductIds_003E__2.Add(gacha.productId);
					}
				});
			});
		});
		ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
		i.onGetProductDatas = (Action<StoreDataList>)Delegate.Combine(i.onGetProductDatas, new Action<StoreDataList>(OnGetProductDatas));
		_isFinishGetNativeProductlist = false;
		Native.GetProductDatas(string.Join("----", productIds.ToArray()));
		while (!_isFinishGetNativeProductlist)
		{
			yield return (object)null;
		}
		gachaModelInfo = new List<GachaModelInfo>();
		MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach(delegate(GachaList.GachaType data)
		{
			_003CDoInitialize_003Ec__Iterator156 _003CDoInitialize_003Ec__Iterator = (_003CDoInitialize_003Ec__Iterator156)/*Error near IL_0148: stateMachine*/;
			GACHA_TYPE gacha_type = data.ViewType;
			GachaModelInfo add_data = ((_003CDoInitialize_003Ec__Iterator156)/*Error near IL_0148: stateMachine*/)._003C_003Ef__this.gachaModelInfo.Find((GachaModelInfo g) => g.type == gacha_type);
			if (add_data == null)
			{
				add_data = new GachaModelInfo();
				add_data.type = gacha_type;
				add_data.url = data.url;
				add_data.gachaDataInfo = new List<GachaModelInfo.GachaDataInfo>();
				((_003CDoInitialize_003Ec__Iterator156)/*Error near IL_0148: stateMachine*/)._003C_003Ef__this.gachaModelInfo.Add(add_data);
			}
			switch (gacha_type)
			{
			case GACHA_TYPE.QUEST:
				add_data.sortPriority = 1;
				data.groups.ForEach(delegate(GachaList.GachaGroup groups)
				{
					_003CDoInitialize_003Ec__Iterator156 _003CDoInitialize_003Ec__Iterator2 = _003CDoInitialize_003Ec__Iterator;
					GachaModelInfo.GachaDataInfo gacha_data_info = new GachaModelInfo.GachaDataInfo();
					gacha_data_info.showPickupIndex = 0;
					gacha_data_info.groupID = groups.group;
					gacha_data_info.bannerImg = groups.bannerImg;
					gacha_data_info.url = groups.url;
					gacha_data_info.gachas = groups.gachas;
					gacha_data_info.gachaGuaranteeCampaignInfos = groups.gachaGuaranteeCampaignInfo;
					gacha_data_info.friendPromotionInfo = groups.friendPromotionInfo;
					gacha_data_info.note = groups.note;
					gacha_data_info.priority = groups.priority;
					gacha_data_info.counter = groups.counter;
					Debug.Log((object)("Gacha Info Counter: " + gacha_data_info.counter));
					gacha_data_info.expireAt = groups.expireAt;
					if (gacha_data_info.gachas != null && gacha_data_info.gachas[0] != null)
					{
						gacha_data_info.buttonImgId = groups.gachas[0].buttonImg;
					}
					gacha_data_info.pickup = new List<PickUp>();
					groups.pickupLineups.ForEach(delegate(GachaList.GachaLineup pickup_data)
					{
						_003CDoInitialize_003Ec__Iterator156 _003CDoInitialize_003Ec__Iterator3 = _003CDoInitialize_003Ec__Iterator2;
						int reward_pri = -1;
						uint reward_id = 0u;
						pickup_data.sellItems.ForEach(delegate(QuestItem.SellItem reward_data)
						{
							if (reward_data.type == 3)
							{
								ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)reward_data.itemId);
								if (itemData != null && itemData.type != ITEM_TYPE.USE_ITEM && itemData.type != ITEM_TYPE.FAIRY && itemData.type != 0 && (reward_id == 0 || reward_pri == -1 || reward_pri > reward_data.pri))
								{
									reward_pri = reward_data.pri;
									reward_id = (uint)reward_data.itemId;
								}
							}
						});
						uint equip = 0u;
						CreateEquipItemTable.CreateEquipItemData[] creatableEquipItem = Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(reward_id);
						if (creatableEquipItem != null && creatableEquipItem.Length > 0)
						{
							equip = creatableEquipItem[0].equipItemID;
						}
						gacha_data_info.pickup.Add(new PickUpQuest(pickup_data.orderNo, pickup_data.itemId, reward_id, equip));
						_003CDoInitialize_003Ec__Iterator2._003C_003Ef__this.CacheQuestAudio((uint)pickup_data.itemId, _003CDoInitialize_003Ec__Iterator2._003Cload_queue_003E__0);
					});
					gacha_data_info.pickup.Sort((PickUp l, PickUp r) => l.orderNo - r.orderNo);
					add_data.gachaDataInfo.Add(gacha_data_info);
				});
				break;
			case GACHA_TYPE.SKILL:
				add_data.sortPriority = 2;
				data.groups.ForEach(delegate(GachaList.GachaGroup groups)
				{
					GachaModelInfo.GachaDataInfo gacha_data_info2 = new GachaModelInfo.GachaDataInfo();
					gacha_data_info2.showPickupIndex = 0;
					gacha_data_info2.groupID = groups.group;
					gacha_data_info2.bannerImg = groups.bannerImg;
					gacha_data_info2.url = groups.url;
					gacha_data_info2.gachas = groups.gachas;
					gacha_data_info2.gachaGuaranteeCampaignInfos = groups.gachaGuaranteeCampaignInfo;
					gacha_data_info2.note = groups.note;
					gacha_data_info2.priority = groups.priority;
					gacha_data_info2.counter = groups.counter;
					Debug.Log((object)("Gacha Info Counter: " + gacha_data_info2.counter));
					gacha_data_info2.expireAt = groups.expireAt;
					if (gacha_data_info2.gachas != null && gacha_data_info2.gachas[0] != null)
					{
						gacha_data_info2.buttonImgId = groups.gachas[0].buttonImg;
					}
					gacha_data_info2.pickup = new List<PickUp>();
					groups.pickupLineups.ForEach(delegate(GachaList.GachaLineup pickup_data)
					{
						gacha_data_info2.pickup.Add(new PickUpSkill(pickup_data.orderNo, pickup_data.itemId, pickup_data.anim));
					});
					add_data.gachaDataInfo.Add(gacha_data_info2);
				});
				break;
			default:
				add_data.sortPriority = 3;
				break;
			}
			add_data.gachaDataInfo.Sort((GachaModelInfo.GachaDataInfo l, GachaModelInfo.GachaDataInfo r) => r.priority - l.priority);
		});
		gachaModelInfo.Sort((GachaModelInfo l, GachaModelInfo r) => l.sortPriority - r.sortPriority);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		base.Initialize();
	}

	public override void StartSection()
	{
		GachaList.Gacha latestPopupGacha = GetLatestPopupGacha();
		GachaGuaranteeCampaignInfo latestPopupGualantee = GetLatestPopupGualantee();
		DateTime startDateTime;
		string campaignDetailImg;
		if (latestPopupGualantee == null)
		{
			startDateTime = latestPopupGacha.GetStartDateTime();
			campaignDetailImg = latestPopupGacha.campaignDetailImg;
		}
		else if (latestPopupGualantee.GetStartDateTime() > latestPopupGacha.GetStartDateTime())
		{
			startDateTime = latestPopupGualantee.GetStartDateTime();
			campaignDetailImg = latestPopupGualantee.campaignDetailImg;
		}
		else
		{
			startDateTime = latestPopupGacha.GetStartDateTime();
			campaignDetailImg = latestPopupGacha.campaignDetailImg;
		}
		bool flag = CheckShowPopUp(startDateTime, campaignDetailImg);
		if (!MonoBehaviourSingleton<GachaManager>.I.IsTutorial() && flag)
		{
			ShowPopUp(campaignDetailImg);
		}
		isSectionStarted = true;
		base.StartSection();
	}

	private bool CheckShowPopUp(DateTime startDate, string campaignDetailImg)
	{
		if (campaignDetailImg == string.Empty)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<GachaManager>.I.HasBeenShowAdvertisement())
		{
			MonoBehaviourSingleton<GachaManager>.I.SetTimeShowShopAdvertisement(startDate);
			return true;
		}
		DateTime timeShowShopAdvertisement = MonoBehaviourSingleton<GachaManager>.I.GetTimeShowShopAdvertisement();
		if (timeShowShopAdvertisement < startDate)
		{
			MonoBehaviourSingleton<GachaManager>.I.SetTimeShowShopAdvertisement(startDate);
			return true;
		}
		return false;
	}

	private void ShowPopUp(string popupImageName)
	{
		if (popupImageName != string.Empty)
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData("POP_UP", popupImageName)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	private List<GachaGuaranteeCampaignInfo> GetStepUpInfos()
	{
		List<GachaGuaranteeCampaignInfo> list = new List<GachaGuaranteeCampaignInfo>();
		for (int i = 0; i < gachaModelInfo.Count; i++)
		{
			List<GachaModelInfo.GachaDataInfo> gachaDataInfo = gachaModelInfo[i].gachaDataInfo;
			for (int j = 0; j < gachaDataInfo.Count; j++)
			{
				GachaModelInfo.GachaDataInfo gachaDataInfo2 = gachaDataInfo[j];
				List<GachaGuaranteeCampaignInfo> list2 = gachaDataInfo2.gachaGuaranteeCampaignInfos.FindAll((GachaGuaranteeCampaignInfo g) => g.IsStepUp());
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
		}
		return list;
	}

	private GachaList.Gacha GetLatestPopupGacha()
	{
		GachaList.Gacha gacha = null;
		for (int i = 0; i < gachaModelInfo.Count; i++)
		{
			for (int j = 0; j < gachaModelInfo[i].gachaDataInfo.Count; j++)
			{
				for (int k = 0; k < gachaModelInfo[i].gachaDataInfo[j].gachas.Count; k++)
				{
					GachaList.Gacha gacha2 = gachaModelInfo[i].gachaDataInfo[j].gachas[k];
					if (!(gacha2.campaignDetailImg == string.Empty))
					{
						DateTime startDateTime = gacha2.GetStartDateTime();
						if (gacha == null || !(gacha.GetStartDateTime() >= startDateTime))
						{
							gacha = gacha2;
						}
					}
				}
			}
		}
		return gacha;
	}

	private GachaGuaranteeCampaignInfo GetLatestPopupGualantee()
	{
		List<GachaGuaranteeCampaignInfo> stepUpInfos = GetStepUpInfos();
		if (stepUpInfos.Count == 0)
		{
			return null;
		}
		GachaGuaranteeCampaignInfo gachaGuaranteeCampaignInfo = null;
		int count = stepUpInfos.Count;
		for (int i = 0; i < count; i++)
		{
			GachaGuaranteeCampaignInfo gachaGuaranteeCampaignInfo2 = stepUpInfos[i];
			if (!(gachaGuaranteeCampaignInfo2.campaignDetailImg == string.Empty))
			{
				DateTime startDateTime = gachaGuaranteeCampaignInfo2.GetStartDateTime();
				if (gachaGuaranteeCampaignInfo == null || !(gachaGuaranteeCampaignInfo.GetStartDateTime() >= startDateTime))
				{
					gachaGuaranteeCampaignInfo = gachaGuaranteeCampaignInfo2;
				}
			}
		}
		return gachaGuaranteeCampaignInfo;
	}

	protected void CacheQuestAudio(uint quest_id, LoadingQueue lo_queue)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
		if (questData != null)
		{
			int mainEnemyID = questData.GetMainEnemyID();
			if (Singleton<EnemyTable>.IsValid())
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID);
				if (enemyData != null)
				{
					CacheEnemyAudio(enemyData, lo_queue);
				}
			}
		}
	}

	protected void CacheEnemyAudio(EnemyTable.EnemyData enemyData, LoadingQueue lo_queue)
	{
		if (lo_queue != null)
		{
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
			if (enemyDisplayInfo != null && enemyDisplayInfo.seIdGachaShort > 0)
			{
				lo_queue.CacheSE(enemyDisplayInfo.seIdhowl, null);
			}
		}
	}

	protected override void OnOpen()
	{
		MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
		MonoBehaviourSingleton<ShopReceiver>.I.onBuyGacha = null;
		ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
		i.onBuyGacha = (Action<string>)Delegate.Combine(i.onBuyGacha, new Action<string>(OnBuyItem));
		ShopReceiver i2 = MonoBehaviourSingleton<ShopReceiver>.I;
		i2.onBuyItem = (Action<string>)Delegate.Combine(i2.onBuyItem, new Action<string>(OnBuyItem));
		isPurchase = false;
		base.OnOpen();
	}

	protected override void OnClose()
	{
		MonoBehaviourSingleton<UIManager>.I.enableShadow = false;
		base.OnClose();
	}

	public override void Close(UITransition.TYPE type)
	{
		base.Close(type);
		DeleteModel();
	}

	private void DeleteModel()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		if (gachaModelInfo != null)
		{
			Transform ctrl = GetCtrl(UI.SCR_LIST_2);
			if (ctrl != null && ctrl.get_childCount() > 0)
			{
				int i = 0;
				for (int childCount = ctrl.get_childCount(); i < childCount; i++)
				{
					Transform val = ctrl.GetChild(i);
					if (val.get_name().Contains("enemy_tex_"))
					{
						string s = val.get_name().Remove(0, "enemy_tex_".Length);
						if (int.TryParse(s, out int result))
						{
							Transform root = GetCtrl(UI.SCR_LIST_2).FindChild("enemy_tex_" + result);
							DeleteRenderTexture(root, UI.TEX_ENEMY_MODEL);
							SetVisibleWidgetEffect(UI.SCR_LIST_2, root, UI.TEX_ENEMY_MODEL, null);
						}
					}
					else if (val.get_name().Contains("skill_tex_"))
					{
						string s2 = val.get_name().Remove(0, "skill_tex_".Length);
						if (int.TryParse(s2, out int result2))
						{
							Transform root2 = GetCtrl(UI.SCR_LIST_2).FindChild("skill_tex_" + result2);
							DeleteRenderTexture(root2, UI.TEX_SKILL_NPC_MODEL);
							DeleteRenderTexture(root2, UI.TEX_SKILL_SUB_NPC_MODEL);
							DeleteRenderTexture(root2, UI.TEX_SKILL_BANNER);
						}
					}
				}
			}
		}
	}

	private void SetGachaChangeButton(int pageIndex)
	{
		SetActive((Enum)UI.BTN_QUEST_GACHA, pageIndex != 0);
		SetActive((Enum)UI.BTN_MAGI_GACHA, pageIndex == 0);
	}

	private unsafe void SetGachaListUI()
	{
		SetGachaChangeButton(pageIndex);
		gachaUIInfoList.Clear();
		gachaBtn.Clear();
		int count = 0;
		if (gachaModelInfo != null && gachaModelInfo.Count > pageIndex && gachaModelInfo[pageIndex].gachaDataInfo != null)
		{
			count = gachaModelInfo[pageIndex].gachaDataInfo.Count;
			count *= 2;
		}
		int item_num = (count != 0) ? (count + 1) : 0;
		StopLoadCoroutine();
		_003CSetGachaListUI_003Ec__AnonStorey468 _003CSetGachaListUI_003Ec__AnonStorey;
		SetTable(UI.TBL_LIST, "GachaListItem", item_num, false, new Func<int, Transform, Transform>((object)_003CSetGachaListUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CSetGachaListUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		RepositionTables();
	}

	private void SetGachaBanner(Transform t, GachaModelInfo.GachaDataInfo gacha_info)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		bool flag = !string.IsNullOrEmpty(gacha_info.bannerImg);
		SetActive(t, flag);
		if (flag)
		{
			string bannerImg = gacha_info.bannerImg;
			Coroutine item = this.StartCoroutine(LoadGachaBanner(t, UI.TEX_GACHA_BANNER, bannerImg));
			coroutineList.Add(item);
			SetButtonEnabled(FindCtrl(t, UI.BTN_GACHA_BANNER), !string.IsNullOrEmpty(gacha_info.url));
			SetEvent(FindCtrl(t, UI.BTN_GACHA_BANNER), "GACHA_BANNER", gacha_info);
		}
	}

	private void SetGachaNote(Transform t, GachaModelInfo.GachaDataInfo gacha_info)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(gacha_info.note))
		{
			FindCtrl(t, UI.SPR_NOTE_BG).get_gameObject().SetActive(true);
			FindCtrl(t, UI.LBL_NOTE).get_gameObject().SetActive(true);
			if (gacha_info.note.Contains("\\n"))
			{
				gacha_info.note = gacha_info.note.Replace("\\n", "\n");
				int num = gacha_info.note.Length - gacha_info.note.Replace("\n", string.Empty).Length;
				if (num > 0)
				{
					Transform val = FindCtrl(t, UI.OBJ_BTN_ROOT);
					Vector3 localPosition = val.get_transform().get_localPosition();
					val.get_transform().set_localPosition(new Vector3(localPosition.x, -204f - (float)(num + 1) * 16f, localPosition.z));
					UIWidget component = t.get_gameObject().GetComponent<UIWidget>();
					component.height = 471 + (num + 1) * 16;
				}
			}
			SetLabelText(t, UI.LBL_NOTE, gacha_info.note);
		}
		else
		{
			FindCtrl(t, UI.SPR_NOTE_BG).get_gameObject().SetActive(false);
			FindCtrl(t, UI.LBL_NOTE).get_gameObject().SetActive(false);
			Transform val2 = FindCtrl(t, UI.OBJ_BTN_ROOT);
			Vector3 localPosition2 = val2.get_transform().get_localPosition();
			val2.get_transform().set_localPosition(new Vector3(localPosition2.x, -188f, localPosition2.z));
		}
	}

	private unsafe void SetGachaButtonsTable(Transform parent, GachaModelInfo.GachaDataInfo gacha_info)
	{
		List<int> subGroupIds = new List<int>();
		int count = gacha_info.gachas.Count;
		for (int i = 0; i < count; i++)
		{
			GachaList.Gacha gacha = gacha_info.gachas[i];
			if (!subGroupIds.Contains(gacha.subGroup))
			{
				subGroupIds.Add(gacha.subGroup);
			}
		}
		List<int> ticketTitleIndexList = new List<int>();
		List<GachaList.Gacha> list = null;
		List<int> promotionGachaIndexList = new List<int>();
		int num = 0;
		for (int j = 0; j < subGroupIds.Count; j++)
		{
			int subGroupId = subGroupIds[j];
			_003CSetGachaButtonsTable_003Ec__AnonStorey469 _003CSetGachaButtonsTable_003Ec__AnonStorey;
			List<GachaList.Gacha> list2 = gacha_info.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003CSetGachaButtonsTable_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
			GachaList.Gacha gacha2 = list2.First();
			int requiredItemId = gacha2.requiredItemId;
			int item = j + ticketTitleIndexList.Count;
			if (requiredItemId > 0 && (list == null || requiredItemId != num))
			{
				ticketTitleIndexList.Add(item);
			}
			else if (gacha_info.GetGachaFriendPromotionInfo(gacha2.gachaId) != null)
			{
				promotionGachaIndexList.Add(item);
			}
			num = requiredItemId;
			list = list2;
		}
		gachaBtn.Add(FindCtrl(parent, UI.GRD_BTN).GetComponent<UITable>());
		int tableListCount = subGroupIds.Count + ticketTitleIndexList.Count;
		bool isHaveGachaTicket = false;
		bool flag = true;
		int requireItemId = num;
		if (gachaModelInfo[pageIndex].type == GACHA_TYPE.QUEST && num > 0)
		{
			isHaveGachaTicket = true;
			flag = false;
			tableListCount++;
		}
		int ticketNumOfGroup = 0;
		_003CSetGachaButtonsTable_003Ec__AnonStorey46A _003CSetGachaButtonsTable_003Ec__AnonStorey46A;
		SetTable(parent, UI.GRD_BTN, "GachaButtonItemTwoLine", tableListCount, false, new Func<int, Transform, Transform>((object)_003CSetGachaButtonsTable_003Ec__AnonStorey46A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CSetGachaButtonsTable_003Ec__AnonStorey46A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void SetUpGachaButtonGrid(Transform t, List<GachaList.Gacha> subGroupGachas, GachaModelInfo.GachaDataInfo gachaInfo, GachaGuaranteeCampaignInfo gachaGuaranteeInfo)
	{
		int gachaEventTitleCount = 0;
		string gachaEventTitleName = subGroupGachas[0].eventTitleImg;
		if (subGroupGachas.Count() == 1 && !string.IsNullOrEmpty(gachaEventTitleName))
		{
			gachaEventTitleCount = 1;
			if (subGroupGachas[0].IsDirectPurchase())
			{
				gachaEventTitleName += "_AND";
			}
		}
		int item_num = subGroupGachas.Count() + gachaEventTitleCount;
		_003CSetUpGachaButtonGrid_003Ec__AnonStorey46E _003CSetUpGachaButtonGrid_003Ec__AnonStorey46E;
		SetGrid(t, UI.GRD_BTN_INNER, "GachaButtonRoot", item_num, false, new Func<int, Transform, Transform>((object)_003CSetUpGachaButtonGrid_003Ec__AnonStorey46E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CSetUpGachaButtonGrid_003Ec__AnonStorey46E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private string CreateButtonName(GachaList.Gacha gacha, GachaGuaranteeCampaignInfo guarantee)
	{
		string str = MonoBehaviourSingleton<GachaManager>.I.CreateButtonBaseName(gacha, guarantee, false);
		return str + "_VER2";
	}

	private bool IsTitleMini(GachaModelInfo.GachaDataInfo gachaInfo, List<GachaList.Gacha> subGroupGachas)
	{
		if (gachaInfo.GetGachaFriendPromotionInfo(subGroupGachas[0].gachaId) != null)
		{
			return true;
		}
		return false;
	}

	private int CreateGachaSubGroupIndex(int tableIndex, List<int> ticketTitleIndexList)
	{
		int num = tableIndex;
		foreach (int ticketTitleIndex in ticketTitleIndexList)
		{
			if (ticketTitleIndex >= tableIndex)
			{
				return num;
			}
			num--;
		}
		return num;
	}

	private unsafe void SetGachaDetailUI(Transform t, GachaModelInfo.GachaDataInfo gachaInfo, List<GachaList.Gacha> subGroupGachas, GachaGuaranteeCampaignInfo gachaGuaranteeInfo)
	{
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		if (_003C_003Ef__am_0024cache12 == null)
		{
			_003C_003Ef__am_0024cache12 = new Func<GachaList.Gacha, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		List<GachaList.Gacha> detailImgGachas = subGroupGachas.Where(_003C_003Ef__am_0024cache12).ToList();
		bool flag = gachaGuaranteeInfo != null;
		if (detailImgGachas.Count == 0 && !flag)
		{
			SetActive(t, UI.OBJ_GUARANTEE_HEADER_ROOT, false);
			SetActive(t, UI.OBJ_GUARANTEE_FOOTER_ROOT, false);
		}
		else
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			string event_data = string.Empty;
			List<GachaList.Gacha> source;
			_003CSetGachaDetailUI_003Ec__AnonStorey46F _003CSetGachaDetailUI_003Ec__AnonStorey46F;
			if (flag)
			{
				source = gachaInfo.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003CSetGachaDetailUI_003Ec__AnonStorey46F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
				empty = gachaGuaranteeInfo.GetTitleImageName();
				empty2 = gachaGuaranteeInfo.endAt;
				empty3 = gachaGuaranteeInfo.link;
			}
			else
			{
				source = detailImgGachas.Where(new Func<GachaList.Gacha, bool>((object)_003CSetGachaDetailUI_003Ec__AnonStorey46F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
				empty = detailImgGachas[0].GetTitleImageName();
				empty2 = detailImgGachas[0].endDate;
				empty3 = detailImgGachas[0].link;
				event_data = detailImgGachas[0].description;
			}
			bool flag2 = subGroupGachas.Contains(source.First());
			bool flag3 = subGroupGachas.Contains(source.Last());
			if (flag2)
			{
				SetActive(t, UI.OBJ_GUARANTEE_HEADER_ROOT, true);
				this.StartCoroutine(LoadGachaGuaranteeCounter(t, UI.BTN_GUARANTEE_COUNT_DOWN, gachaGuaranteeInfo.GetImageCount(), empty));
				if (!flag || !gachaGuaranteeInfo.IsItemConfirmed())
				{
					Transform val = FindCtrl(t, UI.BTN_GUARANTEE_COUNT_DOWN);
					val.GetComponent<UIButton>().set_enabled(true);
					if (empty3 == string.Empty)
					{
						SetEvent(val, "GUARANTEE_GACHA_DETAIL", event_data);
					}
					else
					{
						SetEvent(val, "GUARANTEE_GACHA_DETAIL_WEB", empty3);
					}
				}
				else
				{
					REWARD_TYPE type = (REWARD_TYPE)gachaGuaranteeInfo.type;
					if (type != REWARD_TYPE.SKILL_ITEM)
					{
						FindCtrl(t, UI.BTN_GUARANTEE_COUNT_DOWN).GetComponent<UIButton>().set_enabled(false);
					}
					else
					{
						FindCtrl(t, UI.BTN_GUARANTEE_COUNT_DOWN).GetComponent<UIButton>().set_enabled(true);
						SetEvent(FindCtrl(t, UI.BTN_GUARANTEE_COUNT_DOWN), "SKILL_DETAIL", new object[2]
						{
							ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
							Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaGuaranteeInfo.itemId)
						});
					}
				}
			}
			else
			{
				SetActive(t, UI.OBJ_GUARANTEE_HEADER_ROOT, false);
			}
			if (flag3)
			{
				SetActive(t, UI.OBJ_GUARANTEE_FOOTER_ROOT, true);
				string text = StringTable.Get(STRING_CATEGORY.SHOP, 15u);
				SetLabelText(t, UI.TEX_GUARANTEE_TIME, string.Format(empty2));
				if (empty3 == string.Empty)
				{
					SetEvent(FindCtrl(t, UI.BTN_GUARANTEE_DETAIL), "GUARANTEE_GACHA_DETAIL", event_data);
				}
				else
				{
					SetEvent(FindCtrl(t, UI.BTN_GUARANTEE_DETAIL), "GUARANTEE_GACHA_DETAIL_WEB", empty3);
				}
			}
			else
			{
				SetActive(t, UI.OBJ_GUARANTEE_FOOTER_ROOT, false);
			}
		}
	}

	private void SetUpFriendPromotionGachaTableItem(Transform t, GachaFriendPromotionInfo friendPromotionInfo, GachaList.Gacha firstGacha)
	{
		if (friendPromotionInfo != null)
		{
			string format = StringTable.Get(STRING_CATEGORY.SHOP, 15u);
			SetLabelText(t, UI.LBL_FRIEND_INVITATION_TIME, string.Format(format, firstGacha.endDate));
			string format2 = StringTable.Get(STRING_CATEGORY.SHOP, 16u);
			SetLabelText(t, UI.LBL_FRIEND_INVITATION_REMAIN, string.Format(format2, friendPromotionInfo.remainAllowedCount));
			string format3 = StringTable.Get(STRING_CATEGORY.SHOP, 17u);
			SetLabelText(t, UI.LBL_FRIEND_INVITATION_INVITED, string.Format(format3, friendPromotionInfo.invitedCount));
		}
	}

	private void UpdateGachaList()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		for (int i = 0; i < gachaUIInfoList.Count; i++)
		{
			GachaUIInfo gachaUIInfo = gachaUIInfoList[i];
			if (gachaUIInfo.gachaModelInfo.type == GACHA_TYPE.QUEST)
			{
				Coroutine item = this.StartCoroutine(UpdateQuestList(gachaUIInfo.index, gachaUIInfo.parent));
				coroutineList.Add(item);
			}
			else if (gachaUIInfo.gachaModelInfo.type == GACHA_TYPE.SKILL)
			{
				Coroutine item2 = this.StartCoroutine(UpdateSkillList(gachaUIInfo.index, gachaUIInfo.parent));
				coroutineList.Add(item2);
			}
		}
	}

	private void RepositionTables()
	{
		UpdateAnchors();
		for (int i = 0; i < gachaBtn.Count; i++)
		{
			gachaBtn[i].Reposition();
		}
		base.GetComponent<UITable>((Enum)UI.TBL_LIST).Reposition();
	}

	public override void UpdateUI()
	{
		if (!isSectionStarted)
		{
			SetGachaListUI();
		}
		UpdateGachaList();
	}

	private unsafe IEnumerator LoadEnemyModel(Transform t, Enum _enum, uint enemy_id, string foundation_name, ELEMENT_TYPE element_type, bool is_Howl)
	{
		yield return (object)null;
		bool is_Howl2 = is_Howl;
		if (_003CLoadEnemyModel_003Ec__Iterator157._003C_003Ef__am_0024cacheF == null)
		{
			_003CLoadEnemyModel_003Ec__Iterator157._003C_003Ef__am_0024cacheF = new Action<bool, EnemyLoader>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		SetRenderEnemyModel(t, _enum, enemy_id, foundation_name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, _003CLoadEnemyModel_003Ec__Iterator157._003C_003Ef__am_0024cacheF, UIModelRenderTexture.ENEMY_MOVE_TYPE.DEFULT, is_Howl2);
		if (element_type < ELEMENT_TYPE.MAX)
		{
			SetVisibleWidgetEffect(UI.SCR_LIST_2, t, UI.TEX_ENEMY_MODEL, EnemyLoader.GetElementEffectName(element_type));
		}
	}

	private IEnumerator LoadSkillModel(Transform t, Enum _enum, Enum _inner_enum, uint skill_id)
	{
		yield return (object)null;
		SetRenderSkillItemModel(t, _enum, skill_id, false, true);
		SetRenderSkillItemSymbolModel(t, _inner_enum, skill_id, false);
	}

	private IEnumerator LoadNPCModel(Transform t, Enum main_npc_enum, Enum sub_npc_enum, GachaModelInfo.GachaDataInfo gacha_info)
	{
		yield return (object)null;
		SetRenderNPCModel(t, main_npc_enum, 1, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCFOV, delegate(NPCLoader loader)
		{
			((_003CLoadNPCModel_003Ec__Iterator159)/*Error near IL_0074: stateMachine*/).gacha_info.npcLoader = loader;
			PlayerAnimCtrl.Get(loader.animator, PLCA.SKILL_GACHA_TOP, null, null, null);
		});
		SetRenderNPCModel(t, sub_npc_enum, 501, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillCatNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillCatNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCFOV, delegate(NPCLoader loader)
		{
			PlayerAnimCtrl.Get(loader.animator, PLCA.LIE, null, null, null);
		});
	}

	private IEnumerator LoadSkillBanner(Transform t, Enum root_enum, Enum tex_enum, SkillItemTable.SkillItemData table, GachaModelInfo.GachaDataInfo gacha_info, PickUpSkill pickup)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		int pattern_index;
		switch (pickup.gachaAnim.pattern)
		{
		default:
			pattern_index = 0;
			break;
		case "B":
			pattern_index = 1;
			break;
		case "C":
			pattern_index = 2;
			break;
		case "D":
			pattern_index = 3;
			break;
		case "E":
			pattern_index = 4;
			break;
		}
		int banner_id = (int)table.id;
		LoadObject lo_image = loadQueue.Load(true, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetSkillGachaBannerImage(banner_id), false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		while (gacha_info.npcLoader == null)
		{
			yield return (object)null;
		}
		Transform banner = FindCtrl(t, root_enum);
		if (banner != null)
		{
			Transform banner_anim_t = SetPrefab(banner, "GachaSkillBannerAnim", true);
			if (banner_anim_t != null)
			{
				GachaSkillBannerAnim banner_anim = banner_anim_t.GetComponent<GachaSkillBannerAnim>();
				if (banner_anim != null)
				{
					banner_anim.Init(pattern_index, table, lo_image.loadedObject as Texture, pickup.gachaAnim);
					if (gacha_info.pickup.Count == 1 || MonoBehaviourSingleton<GachaManager>.I.IsTutorialSkillGacha())
					{
						bool is_skip = gacha_info.isFirstSkillListDirection;
						banner_anim.Entry(is_skip, delegate
						{
							PLCA default_anim2 = (!((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_02b9: stateMachine*/)._003Cis_skip_003E__6) ? PLCA.SKILL_GACHA_TOP_SLIDE_END : PLCA.SKILL_GACHA_TOP;
							PlayerAnimCtrl.Get(((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_02b9: stateMachine*/).gacha_info.npcLoader.animator, default_anim2, null, null, null);
						});
						gacha_info.isFirstSkillListDirection = false;
						banner_anim.WaitAndNextPickup(delegate
						{
							PlayerAnimCtrl.Get(((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_02dc: stateMachine*/).gacha_info.npcLoader.animator, PLCA.SKILL_GACHA_TOP_SLIDE, null, null, null);
						});
					}
					else
					{
						bool is_skip2 = gacha_info.isFirstSkillListDirection;
						banner_anim.Entry(is_skip2, delegate
						{
							PLCA default_anim = (!((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_0312: stateMachine*/)._003Cis_skip_003E__7) ? PLCA.SKILL_GACHA_TOP_SLIDE_END : PLCA.SKILL_GACHA_TOP;
							PlayerAnimCtrl.Get(((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_0312: stateMachine*/).gacha_info.npcLoader.animator, default_anim, null, null, null);
						});
						gacha_info.isFirstSkillListDirection = false;
						banner_anim.WaitAndNextPickup(delegate
						{
							PlayerAnimCtrl.Get(((_003CLoadSkillBanner_003Ec__Iterator15A)/*Error near IL_0335: stateMachine*/).gacha_info.npcLoader.animator, PLCA.SKILL_GACHA_TOP_SLIDE, null, null, null);
						});
					}
				}
			}
		}
	}

	private IEnumerator LoadGachaBanner(Transform t, Enum _enum, string banner_img)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		LoadObject lo_image = loadQueue.Load(true, RESOURCE_CATEGORY.GACHA_BANNER, banner_img, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (lo_image.loadedObject != null)
		{
			SetTexture(t, _enum, lo_image.loadedObject as Texture);
		}
	}

	private IEnumerator LoadGachaGuaranteeCounter(Transform t, Enum _enum, int remainNum, string detailButtonImg = "")
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		string empty = string.Empty;
		if (detailButtonImg == string.Empty)
		{
			detailButtonImg = "GGC_000000000";
		}
		string imgName = detailButtonImg;
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, imgName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (lo_image.loadedObject != null)
		{
			SetTexture(t, _enum, lo_image.loadedObject as Texture);
		}
	}

	private IEnumerator LoadGachaEventTitle(Transform t, Enum _enum, string eventTitleImg)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.GACHA_EVENT_TITLE, eventTitleImg, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (lo_image.loadedObject != null)
		{
			SetTexture(t, _enum, lo_image.loadedObject as Texture);
		}
	}

	private IEnumerator LoadGachaButton(Transform t, string buttonImg, string strItemNum, int gachaId, int gachaIndex)
	{
		if (loadQueue == null)
		{
			loadQueue = new LoadingQueue(this);
		}
		while (t.get_childCount() != 0)
		{
			Transform child = t.GetChild(0);
			child.set_parent(null);
			child.get_gameObject().SetActive(false);
			Object.Destroy(child.get_gameObject());
		}
		LoadObject lo_button = loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonImg, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		GameObject button = Object.Instantiate(lo_button.loadedObject) as GameObject;
		button.get_transform().set_parent(t);
		button.get_transform().set_name(UI.BTN_GACHA.ToString());
		button.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
		button.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
		SetLabelText(t, UI.TXT_GACHA_ITEM_NUM, strItemNum);
		SetEvent(event_data: new int[2]
		{
			gachaId,
			gachaIndex
		}, t: FindCtrl(t, UI.BTN_GACHA), event_name: "GACHA");
		RepositionTables();
	}

	private IEnumerator UpdateSkillList(int index, Transform t)
	{
		GachaModelInfo gacha_model_info = gachaModelInfo[pageIndex];
		GachaModelInfo.GachaDataInfo gacha_info = gacha_model_info.gachaDataInfo[index];
		int show_pickup_index = gacha_info.showPickupIndex;
		bool first_update = false;
		Transform skill_tex_trans = GetCtrl(UI.SCR_LIST_2).FindChild("skill_tex_" + index);
		if (skill_tex_trans == null)
		{
			first_update = true;
			skill_tex_trans = Realizes("GachaSkillListItem2", GetCtrl(UI.SCR_LIST_2), true);
			skill_tex_trans.set_name("skill_tex_" + index);
		}
		skill_tex_trans.GetComponent<UIScrollOutSideObject>().SetTargetTransform(FindCtrl(t, UI.OBJ_SKILL_MODEL_ROOT));
		if (index > 0)
		{
			if (first_update)
			{
				SetActive(t, UI.OBJ_SKILL_INFO_ROOT, false);
			}
			yield return (object)new WaitForSeconds((float)index);
			SetActive(t, UI.OBJ_SKILL_INFO_ROOT, true);
		}
		base.GetComponent<UIPanel>((Enum)UI.SCR_LIST_2).depth = base.GetComponent<UIPanel>((Enum)UI.SCR_LIST).depth - 1;
		PickUpSkill pickup = gacha_info.pickup[show_pickup_index] as PickUpSkill;
		SkillItemTable.SkillItemData skill_table = Singleton<SkillItemTable>.I.GetSkillItemData(pickup.skillID);
		SetLabelText(t, UI.LBL_SKILL_ITEM_NAME, skill_table.name);
		SetLabelText(t, UI.LBL_TYPE_NAME, MonoBehaviourSingleton<StatusManager>.I.GetSkillItemGroupString(skill_table.type));
		SetSkillSlotTypeIcon(t, UI.SPR_EQUIP_TYPE_ICON, UI.SPR_RARITY_BG, UI.SPR_RARITY_ICON, skill_table);
		Coroutine coroutine_LoadSkillModel = this.StartCoroutine(LoadNPCModel(skill_tex_trans, UI.TEX_SKILL_NPC_MODEL, UI.TEX_SKILL_SUB_NPC_MODEL, gacha_info));
		Coroutine coroutine_LoadSkillBanner = this.StartCoroutine(LoadSkillBanner(skill_tex_trans, UI.OBJ_SKILL_BANNER, UI.TEX_SKILL_BANNER, skill_table, gacha_info, pickup));
		coroutineList.Add(coroutine_LoadSkillModel);
		coroutineList.Add(coroutine_LoadSkillBanner);
		if (string.IsNullOrEmpty(gacha_info.expireAt))
		{
			SetActive(t, UI.LBL_TIME, false);
		}
		else
		{
			SetActive(t, UI.LBL_TIME, true);
			FindCtrl(t, UI.LBL_TIME).GetComponent<UILabel>().text = GetTimeCountDown(gacha_info.expireAt);
		}
	}

	private IEnumerator UpdateQuestList(int index, Transform t)
	{
		GachaModelInfo gacha_model_info = gachaModelInfo[pageIndex];
		GachaModelInfo.GachaDataInfo gacha_info = gacha_model_info.gachaDataInfo[index];
		int show_pickup_index = gacha_info.showPickupIndex;
		base.GetComponent<UIPanel>((Enum)UI.SCR_LIST_2).depth = base.GetComponent<UIPanel>((Enum)UI.SCR_LIST).depth - 1;
		bool first_update = false;
		Transform enemy_tex_trans = GetCtrl(UI.SCR_LIST_2).FindChild("enemy_tex_" + index);
		if (enemy_tex_trans == null)
		{
			first_update = true;
			enemy_tex_trans = Realizes("GachaListItem2", GetCtrl(UI.SCR_LIST_2), true);
			enemy_tex_trans.set_name("enemy_tex_" + index);
		}
		enemy_tex_trans.GetComponent<UIScrollOutSideObject>().SetTargetTransform(t);
		if (index >= 0)
		{
			if (first_update)
			{
				SetActive(enemy_tex_trans, UI.OBJ_QUEST_INFO_ROOT, false);
			}
			else
			{
				ItemIcon icon = t.GetComponentInChildren<ItemIcon>();
				if (icon != null && gacha_info.rewardIconMaterialInfoID != 0)
				{
					SetMaterialInfo(icon.transform, REWARD_TYPE.ITEM, gacha_info.rewardIconMaterialInfoID, GetCtrl(UI.SCR_LIST));
				}
			}
			yield return (object)new WaitForSeconds((float)index);
			SetActive(enemy_tex_trans, UI.OBJ_QUEST_INFO_ROOT, true);
		}
		PickUpQuest pickup = gacha_info.pickup[show_pickup_index] as PickUpQuest;
		string enemy_name = string.Empty;
		string enemy_lv = string.Empty;
		uint enemy_id = 0u;
		string foundation_name = null;
		ELEMENT_TYPE enemy_element_type = ELEMENT_TYPE.MAX;
		RARITY_TYPE rarity = RARITY_TYPE.D;
		QuestTable.QuestTableData quest_table = Singleton<QuestTable>.I.GetQuestData(pickup.questID);
		if (quest_table != null)
		{
			EnemyTable.EnemyData enemy_table = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_table.GetMainEnemyID());
			if (enemy_table != null)
			{
				enemy_id = enemy_table.id;
				enemy_name = enemy_table.name;
				enemy_element_type = enemy_table.element;
				foundation_name = quest_table.GetFoundationName();
				enemy_lv = quest_table.GetMainEnemyLv().ToString();
				rarity = quest_table.rarity;
			}
		}
		ClearRenderModel(enemy_tex_trans, UI.TEX_ENEMY_MODEL);
		Coroutine coroutine_LoadEnemyModel = this.StartCoroutine(LoadEnemyModel(enemy_tex_trans, UI.TEX_ENEMY_MODEL, enemy_id, foundation_name, enemy_element_type, index == 0));
		coroutineList.Add(coroutine_LoadEnemyModel);
		SetLabelText(enemy_tex_trans, UI.LBL_ENEMY, enemy_name);
		SetLabelText(enemy_tex_trans, UI.LBL_ENEMY_LV, enemy_lv);
		SetActive(enemy_tex_trans, UI.SPR_RARITY_SSS, rarity == RARITY_TYPE.SSS);
		SetActive(enemy_tex_trans, UI.SPR_RARITY_SS, rarity == RARITY_TYPE.SS);
		SetActive(enemy_tex_trans, UI.SPR_RARITY_S, rarity == RARITY_TYPE.S);
		SetActive(enemy_tex_trans, UI.SPR_RARITY_A, rarity == RARITY_TYPE.A);
		SetActive(enemy_tex_trans, UI.SPR_RARITY_B, rarity == RARITY_TYPE.B);
		SetActive(t, UI.OBJ_REWARD_ICON_ROOT, true);
		if (pickup.materialID != 0)
		{
			int matIdIndex = pickUpMaterialIDs.Count;
			pickUpMaterialIDs.Add(pickup.materialID);
			ItemTable.ItemData item_table = Singleton<ItemTable>.I.GetItemData(pickup.materialID);
			ItemIcon.Create(ITEM_ICON_TYPE.ITEM, item_table.iconID, item_table.rarity, FindCtrl(t, UI.OBJ_REWARD_ICON_ROOT), ELEMENT_TYPE.MAX, null, -1, "GACHA_EQUIP_LIST", matIdIndex, false, -1, false, null, false, item_table.enemyIconID, item_table.enemyIconID2, true, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
			gacha_info.rewardIconMaterialInfoID = item_table.id;
		}
		SetActive(t, UI.OBJ_REWARD_ICON_ROOT, pickup.materialID != 0);
		UIVisibleWidgetShriken uvws = null;
		UIWidget wgt_reward_effect = null;
		Transform reward_effect = FindCtrl(t, UI.WGT_REWARD_EFFECT);
		if (reward_effect != null)
		{
			uvws = reward_effect.GetComponent<UIVisibleWidgetShriken>();
			wgt_reward_effect = reward_effect.GetComponent<UIWidget>();
		}
		if (uvws == null)
		{
			UIPanel panel = base.GetComponent<UIPanel>((Enum)UI.SCR_LIST_2);
			if (panel != null)
			{
				UIVisibleWidgetShriken.Set(panel, wgt_reward_effect);
			}
		}
		if (string.IsNullOrEmpty(gacha_info.expireAt))
		{
			SetActive(enemy_tex_trans, UI.LBL_TIME, false);
		}
		else
		{
			SetActive(enemy_tex_trans, UI.LBL_TIME, true);
			FindCtrl(enemy_tex_trans, UI.LBL_TIME).GetComponent<UILabel>().text = GetTimeCountDown(gacha_info.expireAt);
		}
	}

	private void DestoryListChild()
	{
		GetCtrl(UI.TBL_LIST).DestroyChildren();
		GetCtrl(UI.SCR_LIST_2).DestroyChildren();
	}

	private void StopLoadCoroutine()
	{
		coroutineList.ForEach(delegate(Coroutine c)
		{
			if (c != null)
			{
				this.StopCoroutine(c);
			}
		});
		coroutineList.Clear();
	}

	public void OnQuery_MAGI_GACHA()
	{
		pageIndex = 1;
		ResetView();
	}

	public void OnQuery_QUEST_GACHA()
	{
		pageIndex = 0;
		ResetView();
	}

	private void ResetView()
	{
		MonoBehaviourSingleton<UIManager>.I.SetDisableMoment();
		StopLoadCoroutine();
		DeleteModel();
		DestoryListChild();
		UpdateShowIndex(true);
		timer = 0f;
		GachaModelInfo gachaModelInfo = this.gachaModelInfo[pageIndex];
		gachaModelInfo.gachaDataInfo.ForEach(delegate(GachaModelInfo.GachaDataInfo data)
		{
			data.isFirstSkillListDirection = true;
			data.rewardIconMaterialInfoID = 0u;
		});
		SetDirty(UI.TBL_LIST);
		SetGachaListUI();
		RefreshUI();
	}

	public void OnQuery_GACHA_EQUIP_LIST()
	{
		int num = (int)GameSection.GetEventData();
		if (num >= pickUpMaterialIDs.Count || num <= -1)
		{
			GameSection.StopEvent();
		}
		else
		{
			uint num2 = pickUpMaterialIDs[num];
			GameSection.SetEventData(new object[1]
			{
				num2
			});
		}
	}

	public void OnQuery_GACHA_EQUIP_LIST_FROM_NEWS()
	{
		object[] array = (object[])GameSection.GetEventData();
		int num = (int)array[0];
		int num2 = (int)array[1];
		uint num3 = 0u;
		List<GachaModelInfo.GachaDataInfo> gachaDataInfo = gachaModelInfo[0].gachaDataInfo;
		int i = 0;
		for (int count = gachaDataInfo.Count; i < count; i++)
		{
			GachaModelInfo.GachaDataInfo gachaDataInfo2 = gachaDataInfo[i];
			if (gachaDataInfo2 != null && gachaDataInfo2.pickup != null)
			{
				int j = 0;
				for (int count2 = gachaDataInfo2.pickup.Count; j < count2; j++)
				{
					PickUpQuest pickUpQuest = gachaDataInfo2.pickup[j] as PickUpQuest;
					if (pickUpQuest != null && pickUpQuest.questID == num2)
					{
						num3 = pickUpQuest.materialID;
						break;
					}
				}
				if (num3 >= 1)
				{
					break;
				}
			}
		}
		if (num3 == 0)
		{
			GameSection.ChangeEvent("EQUIP_NOT_EXIST", null);
		}
		else
		{
			int num4 = (int)array[2];
			if (num4 >= 0)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
				EventData[] autoEvents = new EventData[1]
				{
					new EventData("GACHA_DETAIL_MAX_PARAM_FROM_NEWS", new object[2]
					{
						num3,
						num4
					})
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			}
			GameSection.SetEventData(new object[1]
			{
				num3
			});
		}
	}

	private unsafe void OnQuery_FORCE_ONCE_PURCHASE_GACHA()
	{
		string productId = GameSection.GetEventData() as string;
		GachaList.Gacha targetGacha = null;
		int targetIndex = 0;
		_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470 _003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey;
		MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach(delegate(GachaList.GachaType type)
		{
			type.groups.ForEach(delegate(GachaList.GachaGroup gr)
			{
				GachaList.Gacha gacha = gr.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).FirstOrDefault();
				if (gacha != null)
				{
					targetGacha = gacha;
					List<GachaList.Gacha> gachas = gr.gachas;
					if (_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache3 == null)
					{
						_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache3 = new Func<GachaList.Gacha, int, _003C_003E__AnonType0<GachaList.Gacha, int>>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					var source = Enumerable.Select(gachas, _003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache3).Where(new Func<_003C_003E__AnonType0<GachaList.Gacha, int>, bool>((object)_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					if (_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache4 == null)
					{
						_003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache4 = new Func<_003C_003E__AnonType0<GachaList.Gacha, int>, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					targetIndex = Enumerable.Select(source, _003COnQuery_FORCE_ONCE_PURCHASE_GACHA_003Ec__AnonStorey470._003C_003Ef__am_0024cache4).First();
				}
			});
		});
		if (targetGacha != null)
		{
			MonoBehaviourSingleton<GachaManager>.I.SelectGacha(targetGacha.gachaId, targetIndex);
			DoGachaOrSendCanPurchaseable();
		}
	}

	private void OnQuery_GACHA()
	{
		int[] array = (int[])GameSection.GetEventData();
		if (array[0] < 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<GachaManager>.I.SelectGacha(array[0], array[1]);
			if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
			{
				TutorialDoGacha();
				GameSection.ChangeEvent("GACHA_QUEST_TUTORIAL", null);
			}
			else
			{
				GachaList.Gacha selectGacha = MonoBehaviourSingleton<GachaManager>.I.selectGacha;
				if (selectGacha.IsEnd)
				{
					RemoveEndDateModel();
					ResetView();
					GameSection.ChangeEvent("GACHA_END", null);
				}
				else
				{
					string text = string.Empty;
					if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
					{
						int ticketId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId;
						int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == ticketId, 1, false);
						ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)ticketId);
						text = itemData.name + " " + MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum + StringTable.Get(STRING_CATEGORY.COMMON, 4000u) + "\n";
						if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum > itemNum)
						{
							object[] event_data = new object[2]
							{
								itemData.name,
								(MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum - itemNum).ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 4000u)
							};
							GameSection.ChangeEvent("NOT_ENOUGH_GACHA_TICKET", event_data);
							return;
						}
					}
					else if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsDirectPurchase())
					{
						selectProductId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.productId;
						if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.yenIncludeTax > 0)
						{
							pp = string.Empty;
						}
						DoGachaOrSendCanPurchaseable();
					}
					else
					{
						text = StringTable.Get(STRING_CATEGORY.COMMON, 100u) + " " + MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
					}
					GameSection.SetEventData(new object[1]
					{
						text
					});
				}
			}
		}
	}

	protected unsafe void TutorialDoGacha()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		int price_num = (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId <= 0) ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum : MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum;
		if (GameSection.CheckCrystal(price_num, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId, true))
		{
			Protocol.Force(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void OnQuery_PP_TO_BUY()
	{
		pp = (GameSection.GetEventData() as string);
		GameSection.SetEventData(null);
		DoGachaOrSendCanPurchaseable();
	}

	private void OnQuery_ShopStopper_YES()
	{
		RequestEvent("STOPPER_TO_BUY", null);
	}

	private void OnQuery_STOPPER_TO_BUY()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
		{
			GameSection.ChangeEvent("BUY_QUEST_GACHA", null);
		}
		else
		{
			GameSection.ChangeEvent("BUY_SKILL_GACHA", null);
		}
		GameSection.StayEvent();
		DoPurchase();
	}

	private void OnQuery_DETAIL_PROMOTION()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
		});
	}

	private void DoGachaOrSendCanPurchaseable()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
		{
			GameSection.ChangeEvent("BUY_QUEST_GACHA", null);
		}
		else
		{
			GameSection.ChangeEvent("BUY_SKILL_GACHA", null);
		}
		GameSection.StayEvent();
		DoGacha(delegate(Error ret)
		{
			if (ret == Error.None)
			{
				if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().oncePurchaseItemToShop != null && !string.IsNullOrEmpty(MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().oncePurchaseItemToShop.productId))
				{
					SendGachaCanPurchase();
				}
				else
				{
					GameSection.ResumeEvent(true, null, false);
				}
			}
			else
			{
				GameSection.ResumeEvent(false, null, false);
			}
		});
	}

	private void SendGachaCanPurchase()
	{
		MonoBehaviourSingleton<ShopManager>.I.SendGoldCanPurchase(selectProductId, pp, delegate(Error ret)
		{
			switch (ret)
			{
			case Error.WRN_GOLD_OVER_LIMITTER_OVERUSE:
				GameSection.ChangeStayEvent("STOPPER", null);
				GameSection.ResumeEvent(true, null, false);
				break;
			default:
				GameSection.ResumeEvent(false, null, false);
				break;
			case Error.None:
				DoPurchase();
				break;
			}
		});
	}

	private void DoPurchase()
	{
		isPurchase = true;
		Native.RequestPurchase(selectProductId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
	}

	private unsafe void OnBuyItem(string productId)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		if (!isDoGacha)
		{
			if (!string.IsNullOrEmpty(productId))
			{
				ProductData product_data = null;
				_003COnBuyItem_003Ec__AnonStorey473 _003COnBuyItem_003Ec__AnonStorey;
				MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach(delegate(GachaList.GachaType type)
				{
					type.groups.ForEach(delegate(GachaList.GachaGroup gr)
					{
						IEnumerable<GachaList.Gacha> source = gr.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003COnBuyItem_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
						if (source.Count() > 0)
						{
							GachaList.Gacha gacha = source.First();
							product_data = new ProductData
							{
								productId = productId,
								price = (double)gacha.yen,
								crystalNum = gacha.crystalNum
							};
						}
					});
				});
				if (product_data != null)
				{
					isDoGacha = true;
					DoGacha(delegate(Error ret)
					{
						isPurchase = false;
						isDoGacha = false;
						if (ret == Error.None)
						{
							GameSection.ResumeEvent(true, null, false);
						}
						else
						{
							GameSection.ResumeEvent(false, null, false);
						}
					});
				}
				else
				{
					Action onFinish = new Action((object)_003COnBuyItem_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					SendRequestCurrentCrystal(onFinish);
				}
			}
			else if (isPurchase)
			{
				GameSection.ResumeEvent(false, null, false);
			}
		}
	}

	private void OnBuyGoPayItem(GoPayDepositModel ret, Purchase purchase)
	{
		OnBuyItem(ret.result.productId);
	}

	private void SendRequestCurrentCrystal(Action onFinish)
	{
		Protocol.Send(OnceStatusInfoModel.URL, delegate(OnceStatusInfoModel result)
		{
			CheckCrystalNum(result, onFinish);
		}, string.Empty);
	}

	private void CheckCrystalNum(OnceStatusInfoModel ret, Action onFinish)
	{
		if (ret.Error == Error.None)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal = ret.result.userStatus.crystal;
			MonoBehaviourSingleton<UserInfoManager>.I.DirtyUserStatus();
			onFinish.Invoke();
		}
	}

	private void OnQuery_GACHA_BANNER()
	{
		GachaModelInfo.GachaDataInfo gachaDataInfo = GameSection.GetEventData() as GachaModelInfo.GachaDataInfo;
		if (string.IsNullOrEmpty(gachaDataInfo.bannerImg) || string.IsNullOrEmpty(gachaDataInfo.url))
		{
			GameSection.StopEvent();
		}
		GameSection.SetEventData(gachaDataInfo.url);
	}

	private void Update()
	{
		if (base.state == STATE.OPEN)
		{
			if (timer < 5f)
			{
				timer += Time.get_deltaTime();
			}
			if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ShopTop") && timer >= 5f)
			{
				timer = 0f;
				UpdateShowIndex(false);
				RefreshUI();
			}
		}
	}

	private string GetTimeCountDown(string endTime)
	{
		if (string.IsNullOrEmpty(endTime))
		{
			return null;
		}
		DateTime now = TimeManager.GetNow();
		DateTime dateTime = DateTime.Parse(endTime);
		if (dateTime.CompareTo(now) < 0)
		{
			return StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 11u);
		}
		TimeSpan timeSpan = dateTime.Subtract(now);
		StringBuilder stringBuilder = new StringBuilder();
		if (timeSpan.Days > 0)
		{
			stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9u), timeSpan.Days));
		}
		else if (timeSpan.Hours > 0)
		{
			stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10u), timeSpan.Hours));
		}
		else
		{
			stringBuilder.Append(timeSpan.Minutes + " minutes");
		}
		stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 12u));
		return stringBuilder.ToString();
	}

	private void UpdateShowIndex(bool is_reset = false)
	{
		if (gachaModelInfo != null && gachaModelInfo.Count != 0)
		{
			gachaModelInfo.ForEach(delegate(GachaModelInfo types)
			{
				if (types != null && types.gachaDataInfo != null && types.gachaDataInfo.Count != 0)
				{
					types.gachaDataInfo.ForEach(delegate(GachaModelInfo.GachaDataInfo groups)
					{
						if (!is_reset)
						{
							groups.showPickupIndex++;
							if (groups.showPickupIndex >= groups.pickup.Count)
							{
								groups.showPickupIndex = 0;
							}
						}
						else
						{
							groups.showPickupIndex = 0;
						}
					});
				}
			});
			DeleteMaterialInfo();
		}
	}

	private void OnQuery_CURRENCY()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Currency);
	}

	private void OnQuery_COMMERCIAL()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Commercial);
	}

	private void OnQuery_FUND()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Found);
	}

	private void OnQuery_PROBABILITY()
	{
		GameSection.ChangeEvent("INFO", gachaModelInfo[pageIndex].url);
	}

	private void RemoveEndDateModel()
	{
		foreach (GachaModelInfo item in gachaModelInfo)
		{
			foreach (GachaModelInfo.GachaDataInfo item2 in item.gachaDataInfo)
			{
				item2.gachas.RemoveAll((GachaList.Gacha x) => x.IsEnd);
			}
			item.gachaDataInfo.RemoveAll((GachaModelInfo.GachaDataInfo x) => x.gachas.Count == 0);
		}
	}

	private void OnQuery_GUARANTEE_GACHA_DETAIL()
	{
		string text = (string)GameSection.GetEventData();
		if (text == string.Empty)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(text);
		}
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
		{
			ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
			i.onBuyGacha = (Action<string>)Delegate.Remove(i.onBuyGacha, new Action<string>(OnBuyItem));
			ShopReceiver i2 = MonoBehaviourSingleton<ShopReceiver>.I;
			i2.onBuyItem = (Action<string>)Delegate.Remove(i2.onBuyItem, new Action<string>(OnBuyItem));
		}
		ShopReceiver i3 = MonoBehaviourSingleton<ShopReceiver>.I;
		i3.onGetProductDatas = (Action<StoreDataList>)Delegate.Remove(i3.onGetProductDatas, new Action<StoreDataList>(OnGetProductDatas));
		base.OnDestroy();
	}

	private void OnGetProductDatas(StoreDataList list)
	{
		_isFinishGetNativeProductlist = true;
		_nativeStoreList = list;
	}
}
