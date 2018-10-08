using Network;
using System;
using UnityEngine;

public class ItemIcon : MonoBehaviour
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

	private int itemID;

	private ulong UniqID;

	protected SortCompareData m_initData;

	private Texture frameTexture;

	private bool isVisible = true;

	public Action onIconLoaded;

	private QUEST_ICON_SIZE_TYPE questIconSizeType;

	public new Transform transform => _transform;

	public ITEM_ICON_TYPE iconType
	{
		get;
		private set;
	}

	public int GetItemID => itemID;

	public ulong GetUniqID => UniqID;

	public SortCompareData InitData => m_initData;

	public Transform _transform
	{
		get;
		private set;
	}

	public bool isIconLoaded => (UnityEngine.Object)icon.mainTexture != (UnityEngine.Object)null;

	public static ItemIcon Create(ItemIconCreateParam param)
	{
		return CreateIcon<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, param.icon_type, param.icon_id, param.rarity, param.parent, param.element, param.magi_enable_equip_type, param.num, param.event_name, param.event_data, param.is_new, param.toggle_group, param.is_select, param.icon_under_text, param.is_equipping, param.enemy_icon_id, param.enemy_icon_id2, param.disable_rarity_text, param.questIconSizeType, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon CreateEquipItemIconByEquipItemInfo(EquipItemInfo equipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false)
	{
		return CreateEquipIconByEquipItemInfo<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, equipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
	}

	public static T CreateEquipIconByEquipItemInfo<T>(UnityEngine.Object prefab, EquipItemInfo equipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false) where T : ItemIcon
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

	private static T CreateEquipIconByEquipItemTable<T>(UnityEngine.Object prefab, EquipItemTable.EquipItemData equipItemTableData, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false) where T : ItemIcon
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

	public static ItemIconDetail CreateAccessoryIcon(UnityEngine.Object prefab, int _iconId, RARITY_TYPE _rarity, Transform _parent, string _eventName, int _eventData, bool _isNew, bool _isEquipping, GET_TYPE _getType)
	{
		bool is_new = _isNew;
		return CreateIcon<ItemIconDetail>(prefab, ITEM_ICON_TYPE.ACCESSORY, _iconId, _rarity, _parent, ELEMENT_TYPE.MAX, null, -1, _eventName, _eventData, is_new, -1, false, null, _isEquipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, _getType, ELEMENT_TYPE.MAX);
	}

	public static ItemIcon Create(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent, ELEMENT_TYPE element = ELEMENT_TYPE.MAX, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, int enemy_icon_id = 0, int enemy_icon_id2 = 0, bool disable_rarity_text = false, GET_TYPE getType = GET_TYPE.PAY, ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX)
	{
		return CreateIcon<ItemIcon>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, element2);
	}

	protected static T CreateIcon<T>(UnityEngine.Object prefab, ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent = null, ELEMENT_TYPE element = ELEMENT_TYPE.MAX, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, int enemy_icon_id = 0, int enemy_icon_id2 = 0, bool disable_rarity_text = false, QUEST_ICON_SIZE_TYPE questIconSizeType = QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE getType = GET_TYPE.PAY, ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX) where T : ItemIcon
	{
		T val = parent.GetComponentInChildren<T>();
		if ((UnityEngine.Object)val == (UnityEngine.Object)null)
		{
			Transform transform = ResourceUtility.Realizes(prefab, parent, -1);
			val = transform.GetComponent<T>();
			val._transform = transform;
		}
		val.SetEquipGrowLimitBG(false);
		_Create(val, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, questIconSizeType, getType, element2);
		return val;
	}

	protected static void _Create(ItemIcon item_icon, ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent, ELEMENT_TYPE element, EQUIPMENT_TYPE? magi_enable_icon_type, int num, string event_name, int event_data, bool is_new, int toggle_group, bool is_select, string icon_under_text, bool is_equipping, int enemy_icon_id, int enemy_icon_id2, bool disable_rarity_text, QUEST_ICON_SIZE_TYPE questIconSizeType, GET_TYPE getType, ELEMENT_TYPE element2)
	{
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
			ResourceLoad.ItemIconLoadIconBGTexture(item_icon, iconBGID, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
			{
				if (_item_icon.bgID == _icon_id)
				{
					item_icon.frameTexture = _tex;
					item_icon.bg.mainTexture = ((!item_icon.isVisible) ? null : item_icon.frameTexture);
				}
			});
			item_icon.VisibleIcon(item_icon.isVisible, !string.IsNullOrEmpty(event_name));
			item_icon.bg.enabled = (iconBGID >= 0);
			SetupElementIcon(item_icon, icon_type, element);
			SetupElementIconSub(item_icon, icon_type, element2);
			item_icon.SetRarity(icon_type, rarity, disable_rarity_text, getType);
			item_icon.enemyIconID = enemy_icon_id;
			item_icon.enemyIconItem.mainTexture = null;
			if (enemy_icon_id != 0)
			{
				item_icon.enemyIconItem.enabled = true;
				ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon, enemy_icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _enemy_icon_id)
				{
					if (_item_icon.enemyIconID == _enemy_icon_id)
					{
						_item_icon.enemyIconItem.mainTexture = _tex;
					}
				});
			}
			else
			{
				item_icon.enemyIconItem.enabled = false;
			}
			if (!object.ReferenceEquals(item_icon.enemyIconItem2, null))
			{
				item_icon.enemyIconID2 = enemy_icon_id2;
				item_icon.enemyIconItem2.mainTexture = null;
				if (enemy_icon_id2 != 0)
				{
					item_icon.enemyIconItem2.enabled = true;
					ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon, enemy_icon_id2, delegate(ItemIcon _item_icon, Texture _tex, int _enemy_icon_id2)
					{
						if (_item_icon.enemyIconID2 == _enemy_icon_id2)
						{
							_item_icon.enemyIconItem2.mainTexture = _tex;
						}
					});
				}
				else
				{
					item_icon.enemyIconItem2.enabled = false;
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
			item_icon.iconTypeSprite.enabled = false;
			if ((UnityEngine.Object)item_icon.iconTypeSpriteSub != (UnityEngine.Object)null)
			{
				item_icon.iconTypeSpriteSub.enabled = false;
			}
			item_icon.rarityFrame.enabled = false;
			item_icon.rarityTextIcon.enabled = false;
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
		item_icon.label.text = "×" + num.ToString();
		item_icon.label.gameObject.SetActive(num > 1);
		if (!string.IsNullOrEmpty(event_name))
		{
			item_icon.button.enabled = true;
			item_icon.sender.eventName = event_name;
			item_icon.sender.eventData = event_data;
		}
		else
		{
			item_icon.button.enabled = false;
		}
		if (!string.IsNullOrEmpty(icon_under_text))
		{
			item_icon.textLabel.supportEncoding = true;
			item_icon.textLabel.text = icon_under_text;
			item_icon.textLabel.gameObject.SetActive(true);
		}
		else
		{
			item_icon.textLabel.gameObject.SetActive(false);
		}
		item_icon.equippingSprite.gameObject.SetActive(is_equipping);
		if ((UnityEngine.Object)item_icon.favoriteSprite != (UnityEngine.Object)null)
		{
			item_icon.favoriteSprite.gameObject.SetActive(false);
		}
		item_icon.gridEquippingMark = item_icon.equippingSprite.gameObject.GetComponentInParent<UIGrid>();
		if ((UnityEngine.Object)item_icon.gridEquippingMark != (UnityEngine.Object)null)
		{
			item_icon.gridEquippingMark.Reposition();
		}
		if (toggle_group < 0)
		{
			item_icon.toggleSelectFrame.enabled = false;
			item_icon.selectFrame.gameObject.SetActive(false);
		}
		else
		{
			if (!item_icon.toggleSelectFrame.enabled)
			{
				item_icon.toggleSelectFrame.activeSprite.alpha = 0f;
			}
			item_icon.toggleSelectFrame.enabled = true;
			item_icon.selectFrame.gameObject.SetActive(true);
			item_icon.toggleSelectFrame.group = toggle_group;
			item_icon.toggleSelectFrame.Set(is_select);
		}
		if (is_new)
		{
			item_icon.newIcon.enabled = true;
			item_icon.newIcon.gameObject.SetActive(true);
		}
		else
		{
			item_icon.newIcon.enabled = false;
			item_icon.newIcon.gameObject.SetActive(false);
		}
		item_icon.SetSkillEnableEquipIcon(magi_enable_icon_type);
		item_icon.SetRewardBG(false);
	}

	protected static void SetupElementIcon(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element)
	{
		if (!((UnityEngine.Object)_itemIcon == (UnityEngine.Object)null) && !((UnityEngine.Object)_itemIcon.iconTypeSprite == (UnityEngine.Object)null))
		{
			_SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSprite);
		}
	}

	protected static void SetupElementIconSub(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element)
	{
		if (!((UnityEngine.Object)_itemIcon == (UnityEngine.Object)null) && !((UnityEngine.Object)_itemIcon.iconTypeSpriteSub == (UnityEngine.Object)null))
		{
			_SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSpriteSub);
		}
	}

	private static void _SetupElementIcon(ItemIcon _itemIcon, ITEM_ICON_TYPE _iconType, ELEMENT_TYPE _element, UISprite sprite)
	{
		if (!((UnityEngine.Object)sprite == (UnityEngine.Object)null))
		{
			string text = GetIconElementSpriteName(_element);
			if (_iconType == ITEM_ICON_TYPE.UNKNOWN)
			{
				text = string.Empty;
			}
			sprite.spriteName = text;
			if (string.IsNullOrEmpty(text))
			{
				sprite.enabled = false;
			}
			else
			{
				sprite.gameObject.SetActive(_itemIcon.gameObject.activeSelf);
				sprite.enabled = _itemIcon.isVisible;
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

	public void SetInitData(SortCompareData _data)
	{
		m_initData = _data;
	}

	private void LoadIconTexture(ITEM_ICON_TYPE icon_type, int icon_id)
	{
		iconID = icon_id;
		icon.enabled = true;
		switch (icon_type)
		{
		case ITEM_ICON_TYPE.UNKNOWN:
			icon.enabled = false;
			break;
		case ITEM_ICON_TYPE.COMMON:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.enabled = false;
			}
			else
			{
				ResourceLoad.ItemIconLoadCommonTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
				{
					if (iconID == _icon_id)
					{
						icon.mainTexture = _tex;
						OnIconLoaded();
					}
				});
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
				if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.STAMP:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.enabled = false;
			}
			else
			{
				ResourceLoad.ItemIconLoadStampTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
				{
					if (iconID == _icon_id)
					{
						icon.mainTexture = _tex;
						OnIconLoaded();
					}
				});
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
				if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			icon.mainTexture = null;
			ResourceLoad.ItemIconLoadQuestItemIconTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
			{
				if (iconID == _icon_id)
				{
					icon.mainTexture = _tex;
					OnIconLoaded();
				}
			});
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
			if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
			{
				iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
			}
			break;
		}
		case ITEM_ICON_TYPE.DEGREE:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.enabled = false;
			}
			else
			{
				DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)icon_id);
				if (data == null)
				{
					icon.enabled = false;
				}
				else
				{
					ResourceLoad.ItemIconLoadDegreeIconTexture(this, data.type, delegate(ItemIcon _item_icon, Texture _tex, DEGREE_TYPE _type_id)
					{
						icon.mainTexture = _tex;
						OnIconLoaded();
					});
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
					if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
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
				icon.enabled = false;
			}
			else
			{
				ResourceLoad.ItemIconLoadPointShopPointIconTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _type_id)
				{
					icon.mainTexture = _tex;
					OnIconLoaded();
				});
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
				if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		case ITEM_ICON_TYPE.ACCESSORY:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.enabled = false;
			}
			else
			{
				ResourceLoad.ItemIconLoadAccessoryIconTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
				{
					if (iconID == _icon_id)
					{
						icon.mainTexture = _tex;
						OnIconLoaded();
					}
				});
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
				if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
				{
					iconTypeSpriteSub.depth = iconTypeSprite.depth - 1;
				}
			}
			break;
		default:
			icon.mainTexture = null;
			if (icon_id <= 0)
			{
				icon.enabled = false;
			}
			else
			{
				ResourceLoad.ItemIconLoadItemIconTexture(this, icon_id, delegate(ItemIcon _item_icon, Texture _tex, int _icon_id)
				{
					if (iconID == _icon_id)
					{
						icon.mainTexture = _tex;
						OnIconLoaded();
					}
				});
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
				if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
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
			onIconLoaded();
		}
	}

	public void VisibleIcon(bool is_visible, bool is_button_enable = true)
	{
		isVisible = is_visible;
		if (is_visible)
		{
			if (is_button_enable)
			{
				button.enabled = true;
			}
			icon.enabled = true;
			label.enabled = true;
			iconTypeSprite.enabled = true;
			rarityFrame.enabled = true;
			rarityTextIcon.enabled = true;
			newIcon.enabled = true;
			if ((UnityEngine.Object)rewardBG != (UnityEngine.Object)null)
			{
				rewardBG.enabled = true;
			}
			bg.mainTexture = frameTexture;
			enemyIconItem.enabled = true;
			if (!object.ReferenceEquals(enemyIconItem2, null))
			{
				enemyIconItem2.enabled = true;
			}
			skillEnableEquipTypeIcon.enabled = true;
		}
		else
		{
			button.enabled = false;
			icon.enabled = false;
			label.enabled = false;
			iconTypeSprite.enabled = false;
			rarityFrame.enabled = false;
			rarityTextIcon.enabled = false;
			newIcon.enabled = false;
			if ((UnityEngine.Object)rewardBG != (UnityEngine.Object)null)
			{
				rewardBG.enabled = false;
			}
			bg.mainTexture = null;
			enemyIconItem.enabled = false;
			if (!object.ReferenceEquals(enemyIconItem2, null))
			{
				enemyIconItem2.enabled = false;
			}
			skillEnableEquipTypeIcon.enabled = false;
		}
	}

	public void SetButtonColor(bool is_enable_button, bool is_instant)
	{
		if (is_enable_button)
		{
			button.defaultColor = (button.hover = Color.white);
			button.pressed = Color.white;
			button.disabledColor = Color.white;
		}
		else
		{
			button.defaultColor = (button.hover = Color.gray);
			button.pressed = Color.gray;
			button.disabledColor = Color.gray;
		}
		button.UpdateColor(is_instant);
	}

	public bool IsSelectIcon()
	{
		if (!toggleSelectFrame.enabled)
		{
			return false;
		}
		return toggleSelectFrame.value;
	}

	public void SelectIcon(bool isSelect)
	{
		if (toggleSelectFrame.enabled)
		{
			toggleSelectFrame.value = isSelect;
		}
	}

	public void SetRarity(ITEM_ICON_TYPE icon_type, RARITY_TYPE? rarity_type, bool disable_text, GET_TYPE getType)
	{
		if (!rarity_type.HasValue && icon_type != ITEM_ICON_TYPE.QUEST_ITEM)
		{
			rarityFrame.spriteName = string.Empty;
			rarityFrame.enabled = false;
			rarityTextIcon.spriteName = string.Empty;
			rarityTextIcon.enabled = false;
		}
		else
		{
			int num = (int)(rarity_type.HasValue ? rarity_type.Value : RARITY_TYPE.D);
			rarityFrame.enabled = true;
			rarityTextIcon.enabled = true;
			switch (icon_type)
			{
			default:
				rarityFrame.enabled = isVisible;
				rarityFrame.spriteName = ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[num];
				UIBehaviour.SetRarityColorType(num, rarityFrame);
				rarityTextIcon.enabled = (!disable_text && isVisible);
				rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				break;
			case ITEM_ICON_TYPE.USE_ITEM:
				rarityFrame.enabled = false;
				rarityTextIcon.enabled = false;
				break;
			case ITEM_ICON_TYPE.QUEST_ITEM:
				if (rarity_type.HasValue)
				{
					rarityFrame.enabled = isVisible;
					rarityFrame.spriteName = ITEM_ICON_MONSTER_RARITY_FRAME_SPRITE[num];
					UIBehaviour.SetRarityColorType(num, rarityFrame);
					rarityTextIcon.enabled = (!disable_text && isVisible);
					rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				}
				else
				{
					rarityFrame.spriteName = ITEM_ICON_MONSTER_NORMAL_FRAME_SPRITE;
					rarityFrame.enabled = isVisible;
					UIBehaviour.SetRarityColorType(-1, rarityFrame);
					rarityTextIcon.spriteName = string.Empty;
					rarityTextIcon.enabled = false;
				}
				break;
			case ITEM_ICON_TYPE.SKILL_ATTACK:
			case ITEM_ICON_TYPE.SKILL_SUPPORT:
			case ITEM_ICON_TYPE.SKILL_HEAL:
			case ITEM_ICON_TYPE.SKILL_PASSIVE:
			case ITEM_ICON_TYPE.SKILL_GROW:
			{
				int num2 = (int)(icon_type - 10);
				rarityFrame.enabled = isVisible;
				rarityFrame.spriteName = ITEM_ICON_SKILL_FRAME[num2] + ITEM_ICON_RARITY[num];
				UIBehaviour.SetRarityColorType(num, rarityFrame);
				rarityTextIcon.enabled = (!disable_text && isVisible);
				rarityTextIcon.spriteName = GetRarityTextSpriteName(rarity_type, getType);
				break;
			}
			case ITEM_ICON_TYPE.UNKNOWN:
				rarityFrame.enabled = false;
				rarityTextIcon.enabled = false;
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
		if (!((UnityEngine.Object)skillEnableEquipTypeIcon == (UnityEngine.Object)null))
		{
			skillEnableEquipTypeIcon.gameObject.SetActive(type.HasValue);
			if (type.HasValue)
			{
				UIBehaviour.SetSkillEquipIconKind(skillEnableEquipTypeIcon.transform, type.Value, true);
			}
		}
	}

	public void SetRewardBG(bool is_visible)
	{
		if ((UnityEngine.Object)rewardBG == (UnityEngine.Object)null)
		{
			Transform transform = base.gameObject.transform.FindChild("SPR_REWARD_BG");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				rewardBG = transform.GetComponent<UISprite>();
			}
		}
		if (!((UnityEngine.Object)rewardBG == (UnityEngine.Object)null))
		{
			rewardBG.gameObject.SetActive(is_visible);
			label.gameObject.SetActive(is_visible);
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
		BoxCollider component = GetComponent<BoxCollider>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.enabled = is_enable;
		}
	}

	public void SetFavoriteIcon(bool is_favorite)
	{
		if (!((UnityEngine.Object)favoriteSprite == (UnityEngine.Object)null))
		{
			favoriteSprite.gameObject.SetActive(is_favorite);
			if ((UnityEngine.Object)gridEquippingMark != (UnityEngine.Object)null)
			{
				gridEquippingMark.Reposition();
			}
		}
	}

	public void SetEquipGrowLimitBG(bool active)
	{
		if ((bool)equipGrowLimitBG)
		{
			equipGrowLimitBG.enabled = active;
			UITweener[] components = equipGrowLimitBG.GetComponents<UITweener>();
			int i = 0;
			for (int num = components.Length; i < num; i++)
			{
				UITweener uITweener = components[i];
				if ((bool)uITweener)
				{
					uITweener.enabled = active;
				}
			}
		}
	}

	public void SetEquipExt(EquipItemInfo info, params UILabel[] levelLabels)
	{
		bool flag = info.IsLevelAndEvolveMax();
		SetEquipGrowLimitBG(flag);
		Color color = (!flag) ? Color.white : new Color(0.2117647f, 1f, 0f);
		int i = 0;
		for (int num = levelLabels.Length; i < num; i++)
		{
			levelLabels[i].color = color;
		}
	}

	public void SetEquipExtInvertedColor(EquipItemInfo info, UILabel levelLabel)
	{
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
			levelLabel.effectColor = Color.white;
		}
	}

	public void SetDepth(int depth)
	{
		icon.depth = depth - 1;
		rarityFrame.depth = depth;
		iconTypeSprite.depth = depth + 2;
		if ((UnityEngine.Object)iconTypeSpriteSub != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)icon == (UnityEngine.Object)null)
		{
			return null;
		}
		Transform transform = ResourceUtility.Realizes(icon.gameObject, -1);
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
		return transform;
	}

	public bool IsVisbleNewIcon()
	{
		if ((UnityEngine.Object)newIcon == (UnityEngine.Object)null)
		{
			return false;
		}
		return isVisible && newIcon.enabled && newIcon.isVisible;
	}

	public void SetJackpotIcon()
	{
		bg.enabled = false;
		button.enabled = false;
		label.enabled = false;
		iconTypeSprite.enabled = false;
		rarityFrame.enabled = false;
		rarityTextIcon.enabled = false;
		newIcon.enabled = false;
		if ((UnityEngine.Object)rewardBG != (UnityEngine.Object)null)
		{
			rewardBG.enabled = false;
		}
		bg.mainTexture = null;
		enemyIconItem.enabled = false;
		if (!object.ReferenceEquals(enemyIconItem2, null))
		{
			enemyIconItem2.enabled = false;
		}
		skillEnableEquipTypeIcon.enabled = false;
	}

	public void SetSpinLogIcon()
	{
		transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		SetJackpotIcon();
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
		button.enabled = false;
		label.enabled = false;
		iconTypeSprite.enabled = false;
		newIcon.enabled = false;
		if ((UnityEngine.Object)rewardBG != (UnityEngine.Object)null)
		{
			rewardBG.enabled = false;
		}
		enemyIconItem.enabled = false;
		if (!object.ReferenceEquals(enemyIconItem2, null))
		{
			enemyIconItem2.enabled = false;
		}
		skillEnableEquipTypeIcon.enabled = false;
	}
}
