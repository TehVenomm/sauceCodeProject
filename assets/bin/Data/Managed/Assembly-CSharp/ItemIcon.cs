using Network;
using System;
using UnityEngine;

public class ItemIcon
{
	public class ItemIconCreateParam
	{
		public ITEM_ICON_TYPE icon_type;

		public int icon_id;

		public RARITY_TYPE? rarity;

		public Transform parent;

		public ELEMENT_TYPE element = ELEMENT_TYPE.MAX;

		public EQUIPMENT_TYPE? magi_enable_equip_type;

		public int num = -1;

		public string event_name;

		public int event_data;

		public bool is_new;

		public int toggle_group = -1;

		public bool is_select;

		public string icon_under_text;

		public bool is_equipping;

		public int enemy_icon_id;

		public int enemy_icon_id2;

		public bool disable_rarity_text;

		public QUEST_ICON_SIZE_TYPE questIconSizeType;
	}

	public enum QUEST_ICON_SIZE_TYPE
	{
		DEFAULT,
		REWARD_DELIVERY_DETAIL,
		REWARD_DELIVERY_LIST
	}

	private const int ICON_SIZE = 64;

	private const int QUEST_ICON_SIZE = 104;

	private const int ICON_FRAME_SIZE = 120;

	private const int QUEST_ICON_FRAME_SIZE = 148;

	private const int QUEST_ICON_SIZE_REWARD = 78;

	private const int QUEST_ICON_FRAME_SIZE_REWARD = 112;

	private const int QUEST_ICON_SIZE_REWARD_LIST = 72;

	private const int QUEST_ICON_FRAME_SIZE_REWARD_LIST = 116;

	protected static readonly string SPR_TYPE_ATK = "EquipStatusATK_W";

	protected static readonly string SPR_TYPE_DEF = "EquipStatusDEF_W";

	private static readonly string[] ITEM_ICON_ELEMENT_SPRITE = new string[7]
	{
		"IconElementFire",
		"IconElementWater",
		"IconElementThunder",
		"IconElementSoil",
		"IconElementLight",
		"IconElementDark",
		"IconElementall"
	};

	public static readonly string[] ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE = new string[7]
	{
		"EquipIconFrame_CD",
		"EquipIconFrame_CD",
		"EquipIconFrame_B",
		"EquipIconFrame_A",
		"EquipIconFrame_S",
		"EquipIconFrame_SS",
		"EquipIconFrame_SS"
	};

	private static readonly string[] ITEM_ICON_MONSTER_RARITY_FRAME_SPRITE = new string[7]
	{
		"MonsterFrame_CD",
		"MonsterFrame_CD",
		"MonsterFrame_B",
		"MonsterFrame_A",
		"MonsterFrame_S",
		"MonsterFrame_SS",
		"MonsterFrame_SS"
	};

	private static readonly string ITEM_ICON_MONSTER_NORMAL_FRAME_SPRITE = "MonsterCircleN";

	public static readonly string[] ITEM_ICON_ITEM_RARITY_ICON_SPRITE = new string[7]
	{
		"RarityText_D",
		"RarityText_C",
		"RarityText_B",
		"RarityText_A",
		"RarityText_S",
		"RarityText_SS",
		"RarityText_SS"
	};

	public static readonly string[] ITEM_ICON_ITEM_RARITY_ICON_EVENT_SPRITE = new string[7]
	{
		"RarityText_D",
		"RarityText_C",
		"RarityText_B",
		"RarityText_A_Event",
		"RarityText_S_Event",
		"RarityText_SS_Event",
		"RarityText_SS_Event"
	};

	private static readonly int[] EQUIP_ITEM_RARITY_BG_ID = new int[7]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		5
	};

	private static readonly string[] ITEM_ICON_SKILL_FRAME = new string[5]
	{
		"MagiIconFrame_ATTACK_",
		"MagiIconFrame_SUPPORT_",
		"MagiIconFrame_HEAL_",
		"MagiIconFrame_PASSIVE_",
		"MagiIconFrame_FRAGMENT_"
	};

	private static readonly string[] ITEM_ICON_RARITY = new string[7]
	{
		"CD",
		"CD",
		"Silver",
		"Gold1",
		"Gold2",
		"Gold3",
		"Gold3"
	};

	[HideInInspector]
	public int iconID;

	[HideInInspector]
	public int bgID;

	[HideInInspector]
	public int enemyIconID;

	[HideInInspector]
	public int enemyIconID2;

	public UITexture bg;

	public UITexture icon;

	public UILabel label;

	public UIButton button;

	public UIGameSceneEventSender sender;

	public Texture emptyTexture;

	public UILabel textLabel;

	public UISprite equippingSprite;

	public UISprite favoriteSprite;

	public UIToggle toggleSelectFrame;

	public UISprite selectFrame;

	public UISprite iconTypeSprite;

	public UISprite iconTypeSpriteSub;

	public UISprite rarityFrame;

	public UISprite rarityTextIcon;

	public UIAtlas[] spriteRarityAtlas;

	public UISprite newIcon;

	public UITexture enemyIconItem;

	public UITexture enemyIconItem2;

	public UISprite skillEnableEquipTypeIcon;

	private UISprite rewardBG;

	private UIGrid gridEquippingMark;

	public UISprite equipGrowLimitBG;

	public UISprite wheelNumBackBG;

	private int itemID;

	private ulong UniqID;

	private int itemNumber;

	protected SortCompareData m_initData;

	private Texture frameTexture;

	private bool isVisible = true;

	public Action onIconLoaded;

	private QUEST_ICON_SIZE_TYPE questIconSizeType;

	public Transform transform => _transform;

	public ITEM_ICON_TYPE iconType
	{
		get;
		private set;
	}

	public int GetItemID => itemID;

	public ulong GetUniqID => UniqID;

	public int GetItemNumber => itemNumber;

	public SortCompareData InitData => m_initData;

	public Transform _transform
	{
		get;
		private set;
	}

	public bool isIconLoaded => icon.mainTexture != null;

	public ItemIcon()
		: this()
	{
	}

	public static ItemIcon Create(ItemIconCreateParam param)
	{
		return CreateIcon<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, param.icon_type, param.icon_id, param.rarity, param.parent, param.element, param.magi_enable_equip_type, param.num, param.event_name, param.event_data, param.is_new, param.toggle_group, param.is_select, param.icon_under_text, param.is_equipping, param.enemy_icon_id, param.enemy_icon_id2, param.disable_rarity_text, param.questIconSizeType, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon CreateEquipItemIconByEquipItemInfo(EquipItemInfo equipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false)
	{
		return CreateEquipIconByEquipItemInfo<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, equipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
	}

	public static T CreateEquipIconByEquipItemInfo<T>(Object prefab, EquipItemInfo equipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false) where T : ItemIcon
	{
		ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		int icon_id = -1;
		string empty = string.Empty;
		GET_TYPE gET_TYPE = GET_TYPE.NONE;
		if (equipItemInfo != null && ((ItemInfoBase<EquipItem>)equipItemInfo).tableID != 0 && equipItemInfo.tableData != null)
		{
			EquipItemTable.EquipItemData tableData = equipItemInfo.tableData;
			icon_type = GetItemIconType(tableData.type);
			rarity = tableData.rarity;
			element = equipItemInfo.GetTargetElementPriorityToTable();
			icon_id = tableData.GetIconID(sex);
			gET_TYPE = tableData.getType;
		}
		GET_TYPE getType = gET_TYPE;
		return CreateIcon<T>(prefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, 0, 0, disable_rarity_text, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon CreateEquipItemIconByEquipItemTable(EquipItemTable.EquipItemData equipItemTableData, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false)
	{
		return CreateEquipIconByEquipItemTable<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, equipItemTableData, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
	}

	private static T CreateEquipIconByEquipItemTable<T>(Object prefab, EquipItemTable.EquipItemData equipItemTableData, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false) where T : ItemIcon
	{
		ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		int icon_id = -1;
		string empty = string.Empty;
		GET_TYPE gET_TYPE = GET_TYPE.NONE;
		if (equipItemTableData != null)
		{
			icon_type = GetItemIconType(equipItemTableData.type);
			rarity = equipItemTableData.rarity;
			element = equipItemTableData.GetTargetElementPriorityToTable();
			icon_id = equipItemTableData.GetIconID(sex);
			gET_TYPE = equipItemTableData.getType;
		}
		GET_TYPE getType = gET_TYPE;
		return CreateIcon<T>(prefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, 0, 0, disable_rarity_text, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon CreateRewardItemIcon(REWARD_TYPE rewardType, uint itemId, Transform parent, int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false, QUEST_ICON_SIZE_TYPE questIconSizeType = QUEST_ICON_SIZE_TYPE.DEFAULT)
	{
		GetIconShowData(rewardType, itemId, out int icon_id, out ITEM_ICON_TYPE icon_type, out RARITY_TYPE? rarity, out ELEMENT_TYPE element, out ELEMENT_TYPE element2, out EQUIPMENT_TYPE? magi_enable_icon_type, out int enemy_icon_id, out int enemy_icon_id2, out GET_TYPE getType, 0);
		return CreateIcon<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, questIconSizeType, getType, element2);
	}

	public static ItemIconDetail CreateAccessoryIcon(Object prefab, int _iconId, RARITY_TYPE _rarity, Transform _parent, string _eventName, int _eventData, bool _isNew, bool _isEquipping, GET_TYPE _getType)
	{
		bool is_new = _isNew;
		return CreateIcon<ItemIconDetail>(prefab, ITEM_ICON_TYPE.ACCESSORY, _iconId, _rarity, _parent, ELEMENT_TYPE.MAX, null, -1, _eventName, _eventData, is_new, -1, false, null, _isEquipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, _getType, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon Create(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent, ELEMENT_TYPE element = ELEMENT_TYPE.MAX, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, int enemy_icon_id = 0, int enemy_icon_id2 = 0, bool disable_rarity_text = false, GET_TYPE getType = GET_TYPE.PAY, ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX)
	{
		return CreateIcon<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, element2);
	}

	protected static T CreateIcon<T>(Object prefab, ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent = null, ELEMENT_TYPE element = ELEMENT_TYPE.MAX, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, int enemy_icon_id = 0, int enemy_icon_id2 = 0, bool disable_rarity_text = false, QUEST_ICON_SIZE_TYPE questIconSizeType = QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE getType = GET_TYPE.PAY, ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX) where T : ItemIcon
	{
		T val = parent.GetComponentInChildren<T>();
		if (val == null)
		{
			Transform val2 = ResourceUtility.Realizes(prefab, parent, -1);
			val = val2.GetComponent<T>();
			val._transform = val2;
		}
		val.SetEquipGrowLimitBG(false);
		_Create(val, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, questIconSizeType, getType, element2);
		return val;
	}

	protected unsafe static void _Create(ItemIcon item_icon, ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent, ELEMENT_TYPE element, EQUIPMENT_TYPE? magi_enable_icon_type, int num, string event_name, int event_data, bool is_new, int toggle_group, bool is_select, string icon_under_text, bool is_equipping, int enemy_icon_id, int enemy_icon_id2, bool disable_rarity_text, QUEST_ICON_SIZE_TYPE questIconSizeType, GET_TYPE getType, ELEMENT_TYPE element2)
	{
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		if (icon_id == 24019000)
		{
			icon_type = ITEM_ICON_TYPE.SKILL_GROW;
		}
		item_icon.itemID = 0;
		item_icon.iconType = icon_type;
		if (icon_id >= 0)
		{
			int iconBGID = GetIconBGID(icon_type, icon_id, rarity);
			item_icon.bgID = iconBGID;
			item_icon.bg.mainTexture = null;
			item_icon.frameTexture = null;
			_003C_Create_003Ec__AnonStorey7C9 _003C_Create_003Ec__AnonStorey7C;
			ResourceLoad.ItemIconLoadIconBGTexture(item_icon, iconBGID, new Action<ItemIcon, Texture, int>((object)_003C_Create_003Ec__AnonStorey7C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			item_icon.VisibleIcon(item_icon.isVisible, !string.IsNullOrEmpty(event_name));
			item_icon.bg.set_enabled(iconBGID >= 0);
			SetupElementIcon(item_icon, icon_type, element);
			SetupElementIconSub(item_icon, icon_type, element2);
			item_icon.SetRarity(icon_type, rarity, disable_rarity_text, getType);
			item_icon.enemyIconID = enemy_icon_id;
			item_icon.enemyIconItem.mainTexture = null;
			if (enemy_icon_id != 0)
			{
				item_icon.enemyIconItem.set_enabled(true);
				ItemIcon item_icon2 = item_icon;
				if (_003C_003Ef__am_0024cache31 == null)
				{
					_003C_003Ef__am_0024cache31 = new Action<ItemIcon, Texture, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon2, enemy_icon_id, _003C_003Ef__am_0024cache31);
			}
			else
			{
				item_icon.enemyIconItem.set_enabled(false);
			}
			if (!object.ReferenceEquals(item_icon.enemyIconItem2, null))
			{
				item_icon.enemyIconID2 = enemy_icon_id2;
				item_icon.enemyIconItem2.mainTexture = null;
				if (enemy_icon_id2 != 0)
				{
					item_icon.enemyIconItem2.set_enabled(true);
					ItemIcon item_icon3 = item_icon;
					if (_003C_003Ef__am_0024cache32 == null)
					{
						_003C_003Ef__am_0024cache32 = new Action<ItemIcon, Texture, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon3, enemy_icon_id2, _003C_003Ef__am_0024cache32);
				}
				else
				{
					item_icon.enemyIconItem2.set_enabled(false);
				}
			}
			item_icon.questIconSizeType = questIconSizeType;
			item_icon.LoadIconTexture(icon_type, icon_id);
		}
		else
		{
			item_icon.icon.mainTexture = null;
			item_icon.frameTexture = null;
			item_icon.bg.mainTexture = item_icon.emptyTexture;
			item_icon.iconTypeSprite.set_enabled(false);
			if (item_icon.iconTypeSpriteSub != null)
			{
				item_icon.iconTypeSpriteSub.set_enabled(false);
			}
			item_icon.rarityFrame.set_enabled(false);
			item_icon.rarityTextIcon.set_enabled(false);
			item_icon.enemyIconItem.mainTexture = null;
			if (!object.ReferenceEquals(item_icon.enemyIconItem2, null))
			{
				item_icon.enemyIconItem2.mainTexture = null;
			}
		}
		if (num == -1)
		{
			num = 1;
		}
		item_icon.SetItemNumber(num);
		item_icon.label.text = "×" + num.ToString();
		item_icon.label.get_gameObject().SetActive(num > 1);
		if (!string.IsNullOrEmpty(event_name))
		{
			item_icon.button.set_enabled(true);
			item_icon.sender.eventName = event_name;
			item_icon.sender.eventData = event_data;
		}
		else
		{
			item_icon.button.set_enabled(false);
		}
		if (!string.IsNullOrEmpty(icon_under_text))
		{
			item_icon.textLabel.supportEncoding = true;
			item_icon.textLabel.text = icon_under_text;
			item_icon.textLabel.get_gameObject().SetActive(true);
		}
		else
		{
			item_icon.textLabel.get_gameObject().SetActive(false);
		}
		item_icon.equippingSprite.get_gameObject().SetActive(is_equipping);
		if (item_icon.favoriteSprite != null)
		{
			item_icon.favoriteSprite.get_gameObject().SetActive(false);
		}
		item_icon.gridEquippingMark = item_icon.equippingSprite.get_gameObject().GetComponentInParent<UIGrid>();
		if (item_icon.gridEquippingMark != null)
		{
			item_icon.gridEquippingMark.Reposition();
		}
		if (toggle_group < 0)
		{
			item_icon.toggleSelectFrame.set_enabled(false);
			item_icon.selectFrame.get_gameObject().SetActive(false);
		}
		else
		{
			if (!item_icon.toggleSelectFrame.get_enabled())
			{
				item_icon.toggleSelectFrame.activeSprite.alpha = 0f;
			}
			item_icon.toggleSelectFrame.set_enabled(true);
			item_icon.selectFrame.get_gameObject().SetActive(true);
			item_icon.toggleSelectFrame.group = toggle_group;
			item_icon.toggleSelectFrame.Set(is_select);
		}
		if (is_new)
		{
			item_icon.newIcon.set_enabled(true);
			item_icon.newIcon.get_gameObject().SetActive(true);
		}
		else
		{
			item_icon.newIcon.set_enabled(false);
			item_icon.newIcon.get_gameObject().SetActive(false);
		}
		item_icon.SetSkillEnableEquipIcon(magi_enable_icon_type);
		item_icon.SetRewardBG(false);
	}

	protected static void SetupElementIcon(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element)
	{
		if (!(_itemIcon == null) && !(_itemIcon.iconTypeSprite == null))
		{
			_SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSprite);
		}
	}

	protected static void SetupElementIconSub(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element)
	{
		if (!(_itemIcon == null) && !(_itemIcon.iconTypeSpriteSub == null))
		{
			_SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSpriteSub);
		}
	}

	private static void _SetupElementIcon(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element, UISprite sprite)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(sprite == null))
		{
			string text = GetIconElementSpriteName(_element);
			if (_iconType == ITEM_ICON_TYPE.UNKNOWN)
			{
				text = string.Empty;
			}
			sprite.spriteName = text;
			if (string.IsNullOrEmpty(text))
			{
				sprite.set_enabled(false);
			}
			else
			{
				sprite.get_gameObject().SetActive(_itemIcon.get_gameObject().get_activeSelf());
				sprite.set_enabled(_itemIcon.isVisible);
			}
		}
	}

	public static ITEM_ICON_TYPE GetItemIconType(EQUIPMENT_TYPE type)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		switch (type)
		{
		case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			return ITEM_ICON_TYPE.ONE_HAND_SWORD;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			return ITEM_ICON_TYPE.TWO_HAND_SWORD;
		case EQUIPMENT_TYPE.SPEAR:
			return ITEM_ICON_TYPE.SPEAR;
		case EQUIPMENT_TYPE.PAIR_SWORDS:
			return ITEM_ICON_TYPE.PAIR_SWORDS;
		case EQUIPMENT_TYPE.ARROW:
			return ITEM_ICON_TYPE.ARROW;
		case EQUIPMENT_TYPE.ARMOR:
		case EQUIPMENT_TYPE.VISUAL_ARMOR:
			return ITEM_ICON_TYPE.ARMOR;
		case EQUIPMENT_TYPE.HELM:
		case EQUIPMENT_TYPE.VISUAL_HELM:
			return ITEM_ICON_TYPE.HELM;
		case EQUIPMENT_TYPE.ARM:
		case EQUIPMENT_TYPE.VISUAL_ARM:
			return ITEM_ICON_TYPE.ARM;
		case EQUIPMENT_TYPE.LEG:
		case EQUIPMENT_TYPE.VISUAL_LEG:
			return ITEM_ICON_TYPE.LEG;
		default:
			return ITEM_ICON_TYPE.NONE;
		}
	}

	public static ITEM_ICON_TYPE GetItemIconType(ITEM_TYPE type)
	{
		switch (type)
		{
		case ITEM_TYPE.USE_ITEM:
			return ITEM_ICON_TYPE.USE_ITEM;
		case ITEM_TYPE.ABILITY_ITEM:
			return ITEM_ICON_TYPE.ABILITY_ITEM;
		default:
			return ITEM_ICON_TYPE.ITEM;
		}
	}

	public static ITEM_ICON_TYPE GetItemIconType(SKILL_SLOT_TYPE type)
	{
		ITEM_ICON_TYPE result = ITEM_ICON_TYPE.NONE;
		switch (type)
		{
		case SKILL_SLOT_TYPE.ATTACK:
			result = ITEM_ICON_TYPE.SKILL_ATTACK;
			break;
		case SKILL_SLOT_TYPE.SUPPORT:
			result = ITEM_ICON_TYPE.SKILL_SUPPORT;
			break;
		case SKILL_SLOT_TYPE.HEAL:
			result = ITEM_ICON_TYPE.SKILL_HEAL;
			break;
		case SKILL_SLOT_TYPE.PASSIVE:
			result = ITEM_ICON_TYPE.SKILL_PASSIVE;
			break;
		case SKILL_SLOT_TYPE.GROW:
			result = ITEM_ICON_TYPE.SKILL_GROW;
			break;
		}
		return result;
	}

	public static ITEM_ICON_TYPE GetItemIconType(QUEST_TYPE type)
	{
		return ITEM_ICON_TYPE.QUEST_ITEM;
	}

	public static int GetRemoveButtonIconID()
	{
		return 90000000;
	}

	protected static void SetTransform(ItemIcon icon, Transform t)
	{
		icon._transform = t;
	}

	public static string GetIconElementSpriteName(ELEMENT_TYPE elem_type)
	{
		if (elem_type == ELEMENT_TYPE.MAX)
		{
			return string.Empty;
		}
		if (elem_type >= ELEMENT_TYPE.MAX)
		{
			return string.Empty;
		}
		if (elem_type == ELEMENT_TYPE.MULTI)
		{
			return ITEM_ICON_ELEMENT_SPRITE[ITEM_ICON_ELEMENT_SPRITE.Length - 1];
		}
		return ITEM_ICON_ELEMENT_SPRITE[(int)elem_type];
	}

	public static int GetIconBGID(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity)
	{
		int num;
		if (!icon_type.IsEquip())
		{
			switch (icon_type)
			{
			default:
				num = 90000100;
				break;
			case ITEM_ICON_TYPE.QUEST_ITEM:
				num = -1;
				break;
			case ITEM_ICON_TYPE.ITEM:
			case ITEM_ICON_TYPE.ABILITY_ITEM:
			case ITEM_ICON_TYPE.ACCESSORY:
				num = 90000101;
				if (rarity.HasValue && (int)rarity.Value < EQUIP_ITEM_RARITY_BG_ID.Length)
				{
					num += EQUIP_ITEM_RARITY_BG_ID[(int)rarity.Value];
				}
				break;
			case ITEM_ICON_TYPE.SKILL_ATTACK:
				num = 90000001;
				break;
			case ITEM_ICON_TYPE.SKILL_SUPPORT:
				num = 90000002;
				break;
			case ITEM_ICON_TYPE.SKILL_HEAL:
				num = 90000003;
				break;
			case ITEM_ICON_TYPE.SKILL_PASSIVE:
				num = 90000004;
				break;
			case ITEM_ICON_TYPE.SKILL_GROW:
				num = 90000005;
				break;
			case ITEM_ICON_TYPE.CRYSTAL:
				num = 90000100;
				break;
			case ITEM_ICON_TYPE.UNKNOWN:
				num = 90000300;
				break;
			case ITEM_ICON_TYPE.COMMON:
				num = 90000100;
				break;
			}
		}
		else
		{
			num = (Singleton<EquipItemTable>.I.GetEquipItemData((uint)EquipItemTable.GetIdFromIconId(icon_id))?.spAttackType.GetItemIconBGId() ?? 90000100);
		}
		return num;
	}

	public static void GetIconShowData(REWARD_TYPE reward_type, uint id, out int icon_id, out ITEM_ICON_TYPE icon_type, out RARITY_TYPE? rarity, out ELEMENT_TYPE element, out ELEMENT_TYPE element2, out EQUIPMENT_TYPE? magi_enable_icon_type, out int enemy_icon_id, out int enemy_icon_id2, out GET_TYPE getType, int exceed_cnt = 0)
	{
		icon_type = ITEM_ICON_TYPE.NONE;
		icon_id = -1;
		rarity = null;
		element = ELEMENT_TYPE.MAX;
		element2 = ELEMENT_TYPE.MAX;
		magi_enable_icon_type = null;
		enemy_icon_id = 0;
		enemy_icon_id2 = 0;
		getType = GET_TYPE.PAY;
		switch (reward_type)
		{
		case REWARD_TYPE.CRYSTAL:
			icon_id = 1;
			icon_type = ITEM_ICON_TYPE.CRYSTAL;
			break;
		case REWARD_TYPE.MONEY:
			icon_id = 2;
			break;
		case REWARD_TYPE.EXP:
			icon_id = 3;
			break;
		case REWARD_TYPE.ITEM:
		case REWARD_TYPE.ABILITY_ITEM:
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(id);
			if (itemData != null)
			{
				icon_type = ((itemData.type != ITEM_TYPE.USE_ITEM) ? ITEM_ICON_TYPE.ITEM : ITEM_ICON_TYPE.USE_ITEM);
				icon_id = itemData.iconID;
				rarity = itemData.rarity;
				enemy_icon_id = itemData.enemyIconID;
				enemy_icon_id2 = itemData.enemyIconID2;
			}
			break;
		}
		case REWARD_TYPE.EQUIP_ITEM:
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
			if (equipItemData != null)
			{
				icon_type = GetItemIconType(equipItemData.type);
				icon_id = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
				rarity = equipItemData.rarity;
				element = equipItemData.GetTargetElementPriorityToTable();
				getType = equipItemData.getType;
			}
			break;
		}
		case REWARD_TYPE.SKILL_ITEM:
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(id);
			if (skillItemData != null)
			{
				icon_type = GetItemIconType(skillItemData.type);
				icon_id = skillItemData.iconID;
				rarity = skillItemData.rarity;
				element = skillItemData.skillAtkType;
				if (skillItemData.GetAttackElementNum() > 1)
				{
					element2 = skillItemData.GetAttackElementByIndex(1);
				}
				magi_enable_icon_type = skillItemData.GetEnableEquipType();
			}
			break;
		}
		case REWARD_TYPE.QUEST_ITEM:
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(id);
			if (questData != null)
			{
				icon_type = GetItemIconType(questData.questType);
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
				icon_id = enemyData.iconId;
				rarity = questData.rarity;
				element = enemyData.element;
			}
			break;
		}
		case REWARD_TYPE.AVATAR:
		case REWARD_TYPE.COMMON:
			icon_type = ITEM_ICON_TYPE.COMMON;
			icon_id = (int)id;
			break;
		case REWARD_TYPE.STAMP:
			icon_type = ITEM_ICON_TYPE.STAMP;
			icon_id = (int)id;
			break;
		case REWARD_TYPE.DEGREE:
			icon_type = ITEM_ICON_TYPE.DEGREE;
			icon_id = (int)id;
			break;
		case REWARD_TYPE.POINT_SHOP_POINT:
			icon_type = ITEM_ICON_TYPE.POINT_SHOP_ICON;
			icon_id = (int)id;
			break;
		case REWARD_TYPE.ACCESSORY:
		{
			AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(id);
			if (data != null)
			{
				icon_type = ITEM_ICON_TYPE.ACCESSORY;
				icon_id = (int)id;
				rarity = data.rarity;
				getType = data.getType;
			}
			break;
		}
		}
	}

	public void SetItemID(int id)
	{
		itemID = id;
	}

	public void SetItemID(uint id)
	{
		itemID = (int)id;
	}

	public void SetUniqID(ulong id)
	{
		UniqID = id;
	}

	public void SetItemNumber(int num)
	{
		itemNumber = num;
	}

	public void SetInitData(SortCompareData _data)
	{
		m_initData = _data;
	}

	private unsafe void LoadIconTexture(ITEM_ICON_TYPE icon_type, int icon_id)
	{
		iconID = icon_id;
		icon.set_enabled(true);
		switch (icon_type)
		{
		case ITEM_ICON_TYPE.UNKNOWN:
			icon.set_enabled(false);
			break;
		case ITEM_ICON_TYPE.COMMON:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				ResourceLoad.ItemIconLoadCommonTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				icon.depth = rarityFrame.depth - 1;
				UITexture uITexture4 = icon;
				int num = 64;
				icon.height = num;
				uITexture4.width = num;
				UISprite uISprite4 = rarityFrame;
				num = 120;
				rarityFrame.height = num;
				uISprite4.width = num;
				iconTypeSprite.depth = rarityFrame.depth + 2;
				if (iconTypeSpriteSub != null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.STAMP:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				ResourceLoad.ItemIconLoadStampTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				icon.depth = rarityFrame.depth - 1;
				UITexture uITexture6 = icon;
				int num = 64;
				icon.height = num;
				uITexture6.width = num;
				UISprite uISprite6 = rarityFrame;
				num = 120;
				rarityFrame.height = num;
				uISprite6.width = num;
				iconTypeSprite.depth = rarityFrame.depth + 2;
				if (iconTypeSpriteSub != null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			icon.mainTexture = null;
			ResourceLoad.ItemIconLoadQuestItemIconTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			icon.depth = rarityFrame.depth + 1;
			SetQuestIconItemSize(questIconSizeType, out int monsterIconSize, out int rarityFrameSize);
			UITexture uITexture3 = icon;
			int num = monsterIconSize;
			icon.height = num;
			uITexture3.width = num;
			UISprite uISprite3 = rarityFrame;
			num = rarityFrameSize;
			rarityFrame.height = num;
			uISprite3.width = num;
			iconTypeSprite.depth = icon.depth + 2;
			if (iconTypeSpriteSub != null)
			{
				iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
			}
			break;
		}
		case ITEM_ICON_TYPE.DEGREE:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)icon_id);
				if (data == null)
				{
					icon.set_enabled(false);
				}
				else
				{
					ResourceLoad.ItemIconLoadDegreeIconTexture(this, data.type, new Action<ItemIcon, Texture, DEGREE_TYPE>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					icon.depth = rarityFrame.depth - 1;
					UITexture uITexture7 = icon;
					int num = 64;
					icon.height = num;
					uITexture7.width = num;
					UISprite uISprite7 = rarityFrame;
					num = 120;
					rarityFrame.height = num;
					uISprite7.width = num;
					iconTypeSprite.depth = rarityFrame.depth + 2;
					if (iconTypeSpriteSub != null)
					{
						iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
					}
				}
			}
			break;
		case ITEM_ICON_TYPE.POINT_SHOP_ICON:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				ResourceLoad.ItemIconLoadPointShopPointIconTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				icon.depth = rarityFrame.depth - 1;
				UITexture uITexture2 = icon;
				int num = 64;
				icon.height = num;
				uITexture2.width = num;
				UISprite uISprite2 = rarityFrame;
				num = 120;
				rarityFrame.height = num;
				uISprite2.width = num;
				iconTypeSprite.depth = rarityFrame.depth + 2;
				if (iconTypeSpriteSub != null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				ResourceLoad.ItemIconLoadAccessoryIconTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				icon.depth = rarityFrame.depth - 1;
				UITexture uITexture5 = icon;
				int num = 64;
				icon.height = num;
				uITexture5.width = num;
				UISprite uISprite5 = rarityFrame;
				num = 120;
				rarityFrame.height = num;
				uISprite5.width = num;
				iconTypeSprite.depth = rarityFrame.depth + 2;
				if (iconTypeSpriteSub != null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		default:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.set_enabled(false);
			}
			else
			{
				ResourceLoad.ItemIconLoadItemIconTexture(this, icon_id, new Action<ItemIcon, Texture, int>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				icon.depth = rarityFrame.depth - 1;
				UITexture uITexture = icon;
				int num = 64;
				icon.height = num;
				uITexture.width = num;
				UISprite uISprite = rarityFrame;
				num = 120;
				rarityFrame.height = num;
				uISprite.width = num;
				iconTypeSprite.depth = rarityFrame.depth + 2;
				if (iconTypeSpriteSub != null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		}
	}

	private void OnIconLoaded()
	{
		if (onIconLoaded != null)
		{
			onIconLoaded.Invoke();
		}
	}

	public void VisibleIcon(bool is_visible, bool is_button_enable = true)
	{
		isVisible = is_visible;
		if (is_visible)
		{
			if (is_button_enable)
			{
				button.set_enabled(true);
			}
			icon.set_enabled(true);
			label.set_enabled(true);
			iconTypeSprite.set_enabled(true);
			rarityFrame.set_enabled(true);
			rarityTextIcon.set_enabled(true);
			newIcon.set_enabled(true);
			if (rewardBG != null)
			{
				rewardBG.set_enabled(true);
			}
			bg.mainTexture = frameTexture;
			enemyIconItem.set_enabled(true);
			if (!object.ReferenceEquals(enemyIconItem2, null))
			{
				enemyIconItem2.set_enabled(true);
			}
			skillEnableEquipTypeIcon.set_enabled(true);
		}
		else
		{
			button.set_enabled(false);
			icon.set_enabled(false);
			label.set_enabled(false);
			iconTypeSprite.set_enabled(false);
			rarityFrame.set_enabled(false);
			rarityTextIcon.set_enabled(false);
			newIcon.set_enabled(false);
			if (rewardBG != null)
			{
				rewardBG.set_enabled(false);
			}
			bg.mainTexture = null;
			enemyIconItem.set_enabled(false);
			if (!object.ReferenceEquals(enemyIconItem2, null))
			{
				enemyIconItem2.set_enabled(false);
			}
			skillEnableEquipTypeIcon.set_enabled(false);
		}
	}

	public void SetButtonColor(bool is_enable_button, bool is_instant)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (is_enable_button)
		{
			button.defaultColor = (button.hover = Color.get_white());
			button.pressed = Color.get_white();
			button.disabledColor = Color.get_white();
		}
		else
		{
			button.defaultColor = (button.hover = Color.get_gray());
			button.pressed = Color.get_gray();
			button.disabledColor = Color.get_gray();
		}
		button.UpdateColor(is_instant);
	}

	public bool IsSelectIcon()
	{
		if (!toggleSelectFrame.get_enabled())
		{
			return false;
		}
		return toggleSelectFrame.value;
	}

	public void SelectIcon(bool isSelect)
	{
		if (toggleSelectFrame.get_enabled())
		{
			toggleSelectFrame.value = isSelect;
		}
	}

	public void SetRarity(ITEM_ICON_TYPE icon_type, RARITY_TYPE? rarity_type, bool disable_text, GET_TYPE getType)
	{
		if (!rarity_type.HasValue && icon_type != ITEM_ICON_TYPE.QUEST_ITEM)
		{
			rarityFrame.spriteName = string.Empty;
			rarityFrame.set_enabled(false);
			rarityTextIcon.spriteName = string.Empty;
			rarityTextIcon.set_enabled(false);
		}
		else
		{
			int num = (int)(rarity_type.HasValue ? rarity_type.Value : RARITY_TYPE.D);
			rarityFrame.set_enabled(true);
			rarityTextIcon.set_enabled(true);
			switch (icon_type)
			{
			default:
				rarityFrame.set_enabled(isVisible);
				rarityFrame.spriteName = ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[num];
				UIBehaviour.SetRarityColorType(num, rarityFrame);
				rarityTextIcon.set_enabled(!disable_text && isVisible);
				rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				break;
			case ITEM_ICON_TYPE.USE_ITEM:
				rarityFrame.set_enabled(false);
				rarityTextIcon.set_enabled(false);
				break;
			case ITEM_ICON_TYPE.QUEST_ITEM:
				if (rarity_type.HasValue)
				{
					rarityFrame.set_enabled(isVisible);
					rarityFrame.spriteName = ITEM_ICON_MONSTER_RARITY_FRAME_SPRITE[num];
					UIBehaviour.SetRarityColorType(num, rarityFrame);
					rarityTextIcon.set_enabled(!disable_text && isVisible);
					rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				}
				else
				{
					rarityFrame.spriteName = ITEM_ICON_MONSTER_NORMAL_FRAME_SPRITE;
					rarityFrame.set_enabled(isVisible);
					UIBehaviour.SetRarityColorType(-1, rarityFrame);
					rarityTextIcon.spriteName = string.Empty;
					rarityTextIcon.set_enabled(false);
				}
				break;
			case ITEM_ICON_TYPE.SKILL_ATTACK:
			case ITEM_ICON_TYPE.SKILL_SUPPORT:
			case ITEM_ICON_TYPE.SKILL_HEAL:
			case ITEM_ICON_TYPE.SKILL_PASSIVE:
			case ITEM_ICON_TYPE.SKILL_GROW:
			{
				int num2 = (int)(icon_type - 10);
				rarityFrame.set_enabled(isVisible);
				rarityFrame.spriteName = ITEM_ICON_SKILL_FRAME[num2] + ITEM_ICON_RARITY[num];
				UIBehaviour.SetRarityColorType(num, rarityFrame);
				rarityTextIcon.set_enabled(!disable_text && isVisible);
				rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				break;
			}
			case ITEM_ICON_TYPE.UNKNOWN:
				rarityFrame.set_enabled(false);
				rarityTextIcon.set_enabled(false);
				break;
			}
			ChangeRarityFrameAtlus(rarity_type);
		}
	}

	public static string GetRarityTextSpriteName(RARITY_TYPE? rarityType, GET_TYPE getType)
	{
		int num = (int)(rarityType.HasValue ? rarityType.Value : RARITY_TYPE.D);
		if (getType == GET_TYPE.PAY)
		{
			return ITEM_ICON_ITEM_RARITY_ICON_SPRITE[num];
		}
		return ITEM_ICON_ITEM_RARITY_ICON_EVENT_SPRITE[num];
	}

	public void ChangeRarityFrameAtlus(RARITY_TYPE? rarity)
	{
		if (!rarity.HasValue || spriteRarityAtlas == null || spriteRarityAtlas.Length == 0)
		{
			return;
		}
		if (rarity.HasValue)
		{
			switch (rarity.Value)
			{
			case RARITY_TYPE.S:
			case RARITY_TYPE.SS:
			case RARITY_TYPE.SSS:
				goto IL_006a;
			}
		}
		int num = 0;
		goto IL_0071;
		IL_006a:
		num = 1;
		goto IL_0071;
		IL_0071:
		string spriteName = rarityFrame.spriteName;
		rarityFrame.atlas = spriteRarityAtlas[num];
		rarityFrame.spriteName = spriteName;
		spriteName = rarityTextIcon.spriteName;
		rarityTextIcon.atlas = spriteRarityAtlas[num];
		rarityTextIcon.spriteName = spriteName;
	}

	private void SetSkillEnableEquipIcon(EQUIPMENT_TYPE? type)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		if (!(skillEnableEquipTypeIcon == null))
		{
			skillEnableEquipTypeIcon.get_gameObject().SetActive(type.HasValue);
			if (type.HasValue)
			{
				UIBehaviour.SetSkillEquipIconKind(skillEnableEquipTypeIcon.get_transform(), type.Value, true);
			}
		}
	}

	public void SetRewardBG(bool is_visible)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		if (rewardBG == null)
		{
			Transform val = this.get_gameObject().get_transform().FindChild("SPR_REWARD_BG");
			if (val != null)
			{
				rewardBG = val.GetComponent<UISprite>();
			}
		}
		if (!(rewardBG == null))
		{
			rewardBG.get_gameObject().SetActive(is_visible);
			label.get_gameObject().SetActive(is_visible);
		}
	}

	public void SetRewardCategoryInfo(REWARD_CATEGORY category)
	{
	}

	public void SetEnemyIconScale(ITEM_ICON_TYPE icon_type, float rate)
	{
		if (icon_type == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			icon.height = (int)((float)icon.height * rate);
			icon.width = (int)((float)icon.width * rate);
			rarityFrame.height = (int)((float)rarityFrame.height * rate);
			rarityFrame.width = (int)((float)rarityFrame.width * rate);
		}
	}

	public void SetEnableCollider(bool is_enable)
	{
		BoxCollider component = this.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.set_enabled(is_enable);
		}
	}

	public void SetFavoriteIcon(bool is_favorite)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!(favoriteSprite == null))
		{
			favoriteSprite.get_gameObject().SetActive(is_favorite);
			if (gridEquippingMark != null)
			{
				gridEquippingMark.Reposition();
			}
		}
	}

	public void SetEquipGrowLimitBG(bool active)
	{
		if (Object.op_Implicit(equipGrowLimitBG))
		{
			equipGrowLimitBG.set_enabled(active);
			UITweener[] components = equipGrowLimitBG.GetComponents<UITweener>();
			int i = 0;
			for (int num = components.Length; i < num; i++)
			{
				UITweener uITweener = components[i];
				if (Object.op_Implicit(uITweener))
				{
					uITweener.set_enabled(active);
				}
			}
		}
	}

	public void SetEquipExt(EquipItemInfo info, params UILabel[] levelLabels)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		bool flag = info.IsLevelAndEvolveMax();
		SetEquipGrowLimitBG(flag);
		Color color = (!flag) ? Color.get_white() : new Color(0.2117647f, 1f, 0f);
		int i = 0;
		for (int num = levelLabels.Length; i < num; i++)
		{
			levelLabels[i].color = color;
		}
	}

	public void SetEquipExtInvertedColor(EquipItemInfo info, UILabel levelLabel)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		bool flag = info.IsLevelAndEvolveMax();
		SetEquipGrowLimitBG(flag);
		if (flag)
		{
			levelLabel.color = new Color(0.2117647f, 1f, 0f);
			levelLabel.effectColor = new Color(0f, 0.0862745f, 0.090196f);
		}
		else
		{
			levelLabel.color = new Color(0f, 0.17647f, 0.1843137f);
			levelLabel.effectColor = Color.get_white();
		}
	}

	public void SetDepth(int depth)
	{
		icon.depth = depth - 1;
		rarityFrame.depth = depth;
		iconTypeSprite.depth = depth + 2;
		if (iconTypeSpriteSub != null)
		{
			iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
		}
	}

	public virtual void SetGrayout(bool isActive)
	{
	}

	public void SetQuestIconItemSize(QUEST_ICON_SIZE_TYPE sizeType, out int monsterIconSize, out int rarityFrameSize)
	{
		monsterIconSize = 0;
		rarityFrameSize = 0;
		switch (sizeType)
		{
		case QUEST_ICON_SIZE_TYPE.DEFAULT:
			monsterIconSize = 104;
			rarityFrameSize = 148;
			break;
		case QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL:
			monsterIconSize = 78;
			rarityFrameSize = 112;
			break;
		case QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST:
			monsterIconSize = 72;
			rarityFrameSize = 116;
			break;
		}
	}

	public Transform CloneIcon()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (icon == null)
		{
			return null;
		}
		Transform val = ResourceUtility.Realizes(icon.get_gameObject(), -1);
		if (val != null)
		{
			val.set_localPosition(Vector3.get_zero());
			val.set_localRotation(Quaternion.get_identity());
			val.set_localScale(Vector3.get_one());
		}
		return val;
	}

	public bool IsVisbleNewIcon()
	{
		if (newIcon == null)
		{
			return false;
		}
		return isVisible && newIcon.get_enabled() && newIcon.isVisible;
	}

	public void SetJackpotIcon()
	{
		button.set_enabled(false);
		label.set_enabled(false);
		bg.set_enabled(false);
		iconTypeSprite.set_enabled(false);
		rarityFrame.set_enabled(false);
		rarityTextIcon.set_enabled(false);
		newIcon.set_enabled(false);
		if (rewardBG != null)
		{
			rewardBG.set_enabled(false);
		}
		enemyIconItem.set_enabled(false);
		if (!object.ReferenceEquals(enemyIconItem2, null))
		{
			enemyIconItem2.set_enabled(false);
		}
		skillEnableEquipTypeIcon.set_enabled(false);
	}

	public void SetSpinLogIcon()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		transform.set_localScale(new Vector3(0.4f, 0.4f, 1f));
		label.get_gameObject().SetActive(itemNumber > 1);
		label.set_enabled(true);
		label.get_transform().set_localPosition(new Vector3(5f, -20f));
	}

	public void SetSpinUserLogIcon()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		transform.set_localScale(Vector2.op_Implicit(new Vector2(0.9f, 0.9f)));
		label.get_gameObject().SetActive(itemNumber > 1);
		if (wheelNumBackBG != null)
		{
			wheelNumBackBG.get_gameObject().SetActive(itemNumber > 1);
		}
		label.set_enabled(true);
		label.get_transform().set_localPosition(new Vector3(0f, -27f));
		label.alignment = NGUIText.Alignment.Center;
	}

	public void SetSpinMachineItem()
	{
		icon.depth = 4;
		SetJackpotIcon();
	}

	public void SetSpinMachineSkillItem()
	{
		icon.depth = 4;
		bg.depth = 3;
		rarityFrame.depth = 2;
		rarityTextIcon.depth = 4;
		button.set_enabled(false);
		label.set_enabled(false);
		iconTypeSprite.set_enabled(false);
		newIcon.set_enabled(false);
		if (rewardBG != null)
		{
			rewardBG.set_enabled(false);
		}
		enemyIconItem.set_enabled(false);
		if (!object.ReferenceEquals(enemyIconItem2, null))
		{
			enemyIconItem2.set_enabled(false);
		}
		skillEnableEquipTypeIcon.set_enabled(false);
	}
}
