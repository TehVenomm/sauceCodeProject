// Decompiled with JetBrains decompiler
// Type: ItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemIcon : MonoBehaviour
{
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
    "EquipIconFrame_SSS"
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
    "RarityText_SSS"
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
  public UISprite sameSkillExceedExp;
  public UISprite sameSkillExceedExpUp;
  private int itemID;
  private ulong UniqID;
  private int itemNumber;
  protected SortCompareData m_initData;
  private Texture frameTexture;
  private bool isVisible = true;
  public System.Action onIconLoaded;
  private const int ICON_SIZE = 64 /*0x40*/;
  private const int QUEST_ICON_SIZE = 104;
  private const int ICON_FRAME_SIZE = 120;
  private const int QUEST_ICON_FRAME_SIZE = 148;
  private const int QUEST_ICON_SIZE_REWARD = 78;
  private const int QUEST_ICON_FRAME_SIZE_REWARD = 112 /*0x70*/;
  private const int QUEST_ICON_SIZE_REWARD_LIST = 72;
  private const int QUEST_ICON_FRAME_SIZE_REWARD_LIST = 116;
  private const int SERIES_ARENA_ICON_SIZE = 228;
  private const int SERIES_ARENA_FRAME_SIZE = 298;
  private ItemIcon.QUEST_ICON_SIZE_TYPE questIconSizeType;

  public static ItemIcon Create(ItemIcon.ItemIconCreateParam param)
  {
    return ItemIcon.CreateIcon<ItemIcon>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, param.icon_type, param.icon_id, param.rarity, param.parent, param.element, param.magi_enable_equip_type, param.num, param.event_name, param.event_data, param.is_new, param.toggle_group, param.is_select, param.icon_under_text, param.is_equipping, param.enemy_icon_id, param.enemy_icon_id2, param.disable_rarity_text, param.questIconSizeType);
  }

  public static ItemIcon CreateEquipItemIconByEquipItemInfo(
    EquipItemInfo equipItemInfo,
    int sex,
    Transform parent,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false)
  {
    return ItemIcon.CreateEquipIconByEquipItemInfo<ItemIcon>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, equipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
  }

  public static T CreateEquipIconByEquipItemInfo<T>(
    Object prefab,
    EquipItemInfo equipItemInfo,
    int sex,
    Transform parent,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false)
    where T : ItemIcon
  {
    ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    int icon_id = -1;
    string empty = string.Empty;
    GET_TYPE getType = GET_TYPE.NONE;
    if (equipItemInfo != null && equipItemInfo.tableID != 0U && equipItemInfo.tableData != null)
    {
      EquipItemTable.EquipItemData tableData = equipItemInfo.tableData;
      icon_type = ItemIcon.GetItemIconType(tableData.type);
      rarity = new RARITY_TYPE?(tableData.rarity);
      element = equipItemInfo.GetTargetElementPriorityToTable();
      icon_id = tableData.GetIconID(sex);
      getType = tableData.getType;
    }
    return ItemIcon.CreateIcon<T>(prefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text: disable_rarity_text, getType: getType);
  }

  public static ItemIcon CreateEquipItemIconByEquipItemTable(
    EquipItemTable.EquipItemData equipItemTableData,
    int sex,
    Transform parent,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false)
  {
    return ItemIcon.CreateEquipIconByEquipItemTable<ItemIcon>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, equipItemTableData, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
  }

  private static T CreateEquipIconByEquipItemTable<T>(
    Object prefab,
    EquipItemTable.EquipItemData equipItemTableData,
    int sex,
    Transform parent,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false)
    where T : ItemIcon
  {
    ITEM_ICON_TYPE icon_type = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    int icon_id = -1;
    string empty = string.Empty;
    GET_TYPE getType = GET_TYPE.NONE;
    if (equipItemTableData != null)
    {
      icon_type = ItemIcon.GetItemIconType(equipItemTableData.type);
      rarity = new RARITY_TYPE?(equipItemTableData.rarity);
      element = equipItemTableData.GetTargetElementPriorityToTable();
      icon_id = equipItemTableData.GetIconID(sex);
      getType = equipItemTableData.getType;
    }
    return ItemIcon.CreateIcon<T>(prefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text: disable_rarity_text, getType: getType);
  }

  public static ItemIcon CreateRewardItemIcon(
    REWARD_TYPE rewardType,
    uint itemId,
    Transform parent,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false,
    ItemIcon.QUEST_ICON_SIZE_TYPE questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT)
  {
    int icon_id;
    ITEM_ICON_TYPE icon_type;
    RARITY_TYPE? rarity;
    ELEMENT_TYPE element;
    ELEMENT_TYPE element2;
    EQUIPMENT_TYPE? magi_enable_icon_type;
    int enemy_icon_id;
    int enemy_icon_id2;
    GET_TYPE getType;
    ItemIcon.GetIconShowData(rewardType, itemId, out icon_id, out icon_type, out rarity, out element, out element2, out magi_enable_icon_type, out enemy_icon_id, out enemy_icon_id2, out getType);
    return ItemIcon.CreateIcon<ItemIcon>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, questIconSizeType, getType, element2);
  }

  public static ItemIconDetail CreateAccessoryIcon(
    Object prefab,
    int _iconId,
    RARITY_TYPE _rarity,
    Transform _parent,
    string _eventName,
    int _eventData,
    bool _isNew,
    bool _isEquipping,
    GET_TYPE _getType)
  {
    return ItemIcon.CreateIcon<ItemIconDetail>(prefab, ITEM_ICON_TYPE.ACCESSORY, _iconId, new RARITY_TYPE?(_rarity), _parent, event_name: _eventName, event_data: _eventData, is_new: _isNew, is_equipping: _isEquipping, getType: _getType);
  }

  public static ItemIcon Create(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    Transform parent,
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    int enemy_icon_id = 0,
    int enemy_icon_id2 = 0,
    bool disable_rarity_text = false,
    GET_TYPE getType = GET_TYPE.PAY,
    ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX,
    bool isSameSkillExceed = false)
  {
    return ItemIcon.CreateIcon<ItemIcon>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconPrefab, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, getType: getType, element2: element2, isSameSKillExceed: isSameSkillExceed);
  }

  protected static T CreateIcon<T>(
    Object prefab,
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    Transform parent = null,
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    int enemy_icon_id = 0,
    int enemy_icon_id2 = 0,
    bool disable_rarity_text = false,
    ItemIcon.QUEST_ICON_SIZE_TYPE questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT,
    GET_TYPE getType = GET_TYPE.PAY,
    ELEMENT_TYPE element2 = ELEMENT_TYPE.MAX,
    bool isSameSKillExceed = false)
    where T : ItemIcon
  {
    T item_icon = ((Component) parent).GetComponentInChildren<T>();
    if (Object.op_Equality((Object) (object) item_icon, (Object) null))
    {
      Transform transform = ResourceUtility.Realizes(prefab, parent);
      item_icon = ((Component) transform).GetComponent<T>();
      item_icon._transform = transform;
    }
    item_icon.SetEquipGrowLimitBG(false);
    ItemIcon._Create((ItemIcon) item_icon, icon_type, icon_id, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text, questIconSizeType, getType, element2, isSameSKillExceed);
    return item_icon;
  }

  protected static void _Create(
    ItemIcon item_icon,
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    Transform parent,
    ELEMENT_TYPE element,
    EQUIPMENT_TYPE? magi_enable_icon_type,
    int num,
    string event_name,
    int event_data,
    bool is_new,
    int toggle_group,
    bool is_select,
    string icon_under_text,
    bool is_equipping,
    int enemy_icon_id,
    int enemy_icon_id2,
    bool disable_rarity_text,
    ItemIcon.QUEST_ICON_SIZE_TYPE questIconSizeType,
    GET_TYPE getType,
    ELEMENT_TYPE element2,
    bool isSameSkillExceed)
  {
    if (icon_id == 24019000)
      icon_type = ITEM_ICON_TYPE.SKILL_GROW;
    item_icon.itemID = 0;
    item_icon.iconType = icon_type;
    if (icon_id >= 0)
    {
      int iconBgid = ItemIcon.GetIconBGID(icon_type, icon_id, rarity);
      item_icon.bgID = iconBgid;
      item_icon.bg.mainTexture = (Texture) null;
      item_icon.frameTexture = (Texture) null;
      ResourceLoad.ItemIconLoadIconBGTexture(item_icon, iconBgid, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
      {
        if (_item_icon.bgID != _icon_id)
          return;
        item_icon.frameTexture = _tex;
        item_icon.bg.mainTexture = item_icon.isVisible ? item_icon.frameTexture : (Texture) null;
      }));
      item_icon.VisibleIcon(item_icon.isVisible, !string.IsNullOrEmpty(event_name));
      ((Behaviour) item_icon.bg).enabled = iconBgid >= 0;
      ItemIcon.SetupElementIcon(item_icon, icon_type, element);
      ItemIcon.SetupElementIconSub(item_icon, icon_type, element2);
      item_icon.SetRarity(icon_type, rarity, disable_rarity_text, getType);
      item_icon.enemyIconID = enemy_icon_id;
      item_icon.enemyIconItem.mainTexture = (Texture) null;
      if (enemy_icon_id != 0)
      {
        ((Behaviour) item_icon.enemyIconItem).enabled = true;
        ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon, enemy_icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _enemy_icon_id) =>
        {
          if (_item_icon.enemyIconID != _enemy_icon_id)
            return;
          _item_icon.enemyIconItem.mainTexture = _tex;
        }));
      }
      else
        ((Behaviour) item_icon.enemyIconItem).enabled = false;
      if (item_icon.enemyIconItem2 != null)
      {
        item_icon.enemyIconID2 = enemy_icon_id2;
        item_icon.enemyIconItem2.mainTexture = (Texture) null;
        if (enemy_icon_id2 != 0)
        {
          ((Behaviour) item_icon.enemyIconItem2).enabled = true;
          ResourceLoad.ItemIconLoadEnemyIconItemTexture(item_icon, enemy_icon_id2, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _enemy_icon_id2) =>
          {
            if (_item_icon.enemyIconID2 != _enemy_icon_id2)
              return;
            _item_icon.enemyIconItem2.mainTexture = _tex;
          }));
        }
        else
          ((Behaviour) item_icon.enemyIconItem2).enabled = false;
      }
      item_icon.questIconSizeType = questIconSizeType;
      item_icon.LoadIconTexture(icon_type, icon_id);
    }
    else
    {
      item_icon.icon.mainTexture = (Texture) null;
      item_icon.frameTexture = (Texture) null;
      item_icon.bg.mainTexture = item_icon.emptyTexture;
      ((Behaviour) item_icon.iconTypeSprite).enabled = false;
      if (Object.op_Inequality((Object) item_icon.iconTypeSpriteSub, (Object) null))
        ((Behaviour) item_icon.iconTypeSpriteSub).enabled = false;
      ((Behaviour) item_icon.rarityFrame).enabled = false;
      ((Behaviour) item_icon.rarityTextIcon).enabled = false;
      item_icon.enemyIconItem.mainTexture = (Texture) null;
      if (item_icon.enemyIconItem2 != null)
        item_icon.enemyIconItem2.mainTexture = (Texture) null;
    }
    if (num == -1)
      num = 1;
    item_icon.SetItemNumber(num);
    item_icon.label.text = "×" + num.ToString();
    ((Component) item_icon.label).gameObject.SetActive(num > 1);
    if (!string.IsNullOrEmpty(event_name))
    {
      ((Behaviour) item_icon.button).enabled = true;
      item_icon.sender.eventName = event_name;
      item_icon.sender.eventData = (object) event_data;
    }
    else
      ((Behaviour) item_icon.button).enabled = false;
    if (!string.IsNullOrEmpty(icon_under_text))
    {
      item_icon.textLabel.supportEncoding = true;
      item_icon.textLabel.text = icon_under_text;
      ((Component) item_icon.textLabel).gameObject.SetActive(true);
    }
    else
      ((Component) item_icon.textLabel).gameObject.SetActive(false);
    ((Component) item_icon.equippingSprite).gameObject.SetActive(is_equipping);
    if (Object.op_Inequality((Object) item_icon.favoriteSprite, (Object) null))
      ((Component) item_icon.favoriteSprite).gameObject.SetActive(false);
    item_icon.gridEquippingMark = ((Component) item_icon.equippingSprite).gameObject.GetComponentInParent<UIGrid>();
    if (Object.op_Inequality((Object) item_icon.gridEquippingMark, (Object) null))
      item_icon.gridEquippingMark.Reposition();
    if (toggle_group < 0)
    {
      ((Behaviour) item_icon.toggleSelectFrame).enabled = false;
      ((Component) item_icon.selectFrame).gameObject.SetActive(false);
    }
    else
    {
      if (!((Behaviour) item_icon.toggleSelectFrame).enabled)
        item_icon.toggleSelectFrame.activeSprite.alpha = 0.0f;
      ((Behaviour) item_icon.toggleSelectFrame).enabled = true;
      ((Component) item_icon.selectFrame).gameObject.SetActive(true);
      item_icon.toggleSelectFrame.group = toggle_group;
      item_icon.toggleSelectFrame.Set(is_select);
    }
    if (is_new)
    {
      ((Behaviour) item_icon.newIcon).enabled = true;
      ((Component) item_icon.newIcon).gameObject.SetActive(true);
    }
    else
    {
      ((Behaviour) item_icon.newIcon).enabled = false;
      ((Component) item_icon.newIcon).gameObject.SetActive(false);
    }
    item_icon.SetSkillEnableEquipIcon(magi_enable_icon_type);
    item_icon.SetRewardBG(false);
    if (Object.op_Inequality((Object) item_icon.sameSkillExceedExp, (Object) null))
      ((Component) item_icon.sameSkillExceedExp).gameObject.SetActive(isSameSkillExceed);
    if (!Object.op_Inequality((Object) item_icon.sameSkillExceedExpUp, (Object) null))
      return;
    ((Component) item_icon.sameSkillExceedExpUp).gameObject.SetActive(isSameSkillExceed && !is_equipping);
  }

  protected static void SetupElementIcon(
    ItemIcon _itemIcon,
    ITEM_ICON_TYPE _iconType,
    ELEMENT_TYPE _element)
  {
    if (Object.op_Equality((Object) _itemIcon, (Object) null) || Object.op_Equality((Object) _itemIcon.iconTypeSprite, (Object) null))
      return;
    ItemIcon._SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSprite);
  }

  protected static void SetupElementIconSub(
    ItemIcon _itemIcon,
    ITEM_ICON_TYPE _iconType,
    ELEMENT_TYPE _element)
  {
    if (Object.op_Equality((Object) _itemIcon, (Object) null) || Object.op_Equality((Object) _itemIcon.iconTypeSpriteSub, (Object) null))
      return;
    ItemIcon._SetupElementIcon(_itemIcon, _iconType, _element, _itemIcon.iconTypeSpriteSub);
  }

  private static void _SetupElementIcon(
    ItemIcon _itemIcon,
    ITEM_ICON_TYPE _iconType,
    ELEMENT_TYPE _element,
    UISprite sprite)
  {
    if (Object.op_Equality((Object) sprite, (Object) null))
      return;
    string str = ItemIcon.GetIconElementSpriteName(_element);
    if (_iconType == ITEM_ICON_TYPE.UNKNOWN)
      str = string.Empty;
    sprite.spriteName = str;
    if (string.IsNullOrEmpty(str))
    {
      ((Behaviour) sprite).enabled = false;
    }
    else
    {
      ((Component) sprite).gameObject.SetActive(((Component) _itemIcon).gameObject.activeSelf);
      ((Behaviour) sprite).enabled = _itemIcon.isVisible;
    }
  }

  public static ITEM_ICON_TYPE GetItemIconType(EQUIPMENT_TYPE type)
  {
    ITEM_ICON_TYPE itemIconType;
    switch (type)
    {
      case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        itemIconType = ITEM_ICON_TYPE.ONE_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        itemIconType = ITEM_ICON_TYPE.TWO_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.SPEAR:
        itemIconType = ITEM_ICON_TYPE.SPEAR;
        break;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        itemIconType = ITEM_ICON_TYPE.PAIR_SWORDS;
        break;
      case EQUIPMENT_TYPE.ARROW:
        itemIconType = ITEM_ICON_TYPE.ARROW;
        break;
      case EQUIPMENT_TYPE.ARMOR:
      case EQUIPMENT_TYPE.VISUAL_ARMOR:
        itemIconType = ITEM_ICON_TYPE.ARMOR;
        break;
      case EQUIPMENT_TYPE.HELM:
      case EQUIPMENT_TYPE.VISUAL_HELM:
        itemIconType = ITEM_ICON_TYPE.HELM;
        break;
      case EQUIPMENT_TYPE.ARM:
      case EQUIPMENT_TYPE.VISUAL_ARM:
        itemIconType = ITEM_ICON_TYPE.ARM;
        break;
      case EQUIPMENT_TYPE.LEG:
      case EQUIPMENT_TYPE.VISUAL_LEG:
        itemIconType = ITEM_ICON_TYPE.LEG;
        break;
      default:
        itemIconType = ITEM_ICON_TYPE.NONE;
        break;
    }
    return itemIconType;
  }

  public static ITEM_ICON_TYPE GetItemIconType(ITEM_TYPE type)
  {
    if (type == ITEM_TYPE.USE_ITEM)
      return ITEM_ICON_TYPE.USE_ITEM;
    return type == ITEM_TYPE.ABILITY_ITEM ? ITEM_ICON_TYPE.ABILITY_ITEM : ITEM_ICON_TYPE.ITEM;
  }

  public static ITEM_ICON_TYPE GetItemIconType(SKILL_SLOT_TYPE type)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    switch (type)
    {
      case SKILL_SLOT_TYPE.ATTACK:
        itemIconType = ITEM_ICON_TYPE.SKILL_ATTACK;
        break;
      case SKILL_SLOT_TYPE.SUPPORT:
        itemIconType = ITEM_ICON_TYPE.SKILL_SUPPORT;
        break;
      case SKILL_SLOT_TYPE.HEAL:
        itemIconType = ITEM_ICON_TYPE.SKILL_HEAL;
        break;
      case SKILL_SLOT_TYPE.PASSIVE:
        itemIconType = ITEM_ICON_TYPE.SKILL_PASSIVE;
        break;
      case SKILL_SLOT_TYPE.GROW:
        itemIconType = ITEM_ICON_TYPE.SKILL_GROW;
        break;
    }
    return itemIconType;
  }

  public static ITEM_ICON_TYPE GetItemIconType(QUEST_TYPE type) => ITEM_ICON_TYPE.QUEST_ITEM;

  public static int GetRemoveButtonIconID() => 90000000;

  protected static void SetTransform(ItemIcon icon, Transform t) => icon._transform = t;

  public static string GetIconElementSpriteName(ELEMENT_TYPE elem_type)
  {
    if (elem_type == ELEMENT_TYPE.MAX || elem_type >= ELEMENT_TYPE.MAX)
      return string.Empty;
    return elem_type == ELEMENT_TYPE.MULTI ? ItemIcon.ITEM_ICON_ELEMENT_SPRITE[ItemIcon.ITEM_ICON_ELEMENT_SPRITE.Length - 1] : ItemIcon.ITEM_ICON_ELEMENT_SPRITE[(int) elem_type];
  }

  public static int GetIconBGID(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity)
  {
    int iconBgid;
    if (icon_type.IsEquip())
    {
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) EquipItemTable.GetIdFromIconId(icon_id));
      iconBgid = equipItemData != null ? equipItemData.spAttackType.GetItemIconBGId() : 90000100;
    }
    else
    {
      switch (icon_type)
      {
        case ITEM_ICON_TYPE.SKILL_ATTACK:
          iconBgid = 90000001;
          break;
        case ITEM_ICON_TYPE.SKILL_SUPPORT:
          iconBgid = 90000002;
          break;
        case ITEM_ICON_TYPE.SKILL_HEAL:
          iconBgid = 90000003;
          break;
        case ITEM_ICON_TYPE.SKILL_PASSIVE:
          iconBgid = 90000004;
          break;
        case ITEM_ICON_TYPE.SKILL_GROW:
          iconBgid = 90000005;
          break;
        case ITEM_ICON_TYPE.ITEM:
        case ITEM_ICON_TYPE.ABILITY_ITEM:
        case ITEM_ICON_TYPE.ACCESSORY:
          iconBgid = 90000101;
          if (rarity.HasValue && rarity.Value < (RARITY_TYPE) ItemIcon.EQUIP_ITEM_RARITY_BG_ID.Length)
          {
            iconBgid += ItemIcon.EQUIP_ITEM_RARITY_BG_ID[(int) rarity.Value];
            break;
          }
          break;
        case ITEM_ICON_TYPE.QUEST_ITEM:
          iconBgid = -1;
          break;
        case ITEM_ICON_TYPE.CRYSTAL:
          iconBgid = 90000100;
          break;
        case ITEM_ICON_TYPE.UNKNOWN:
          iconBgid = 90000300;
          break;
        case ITEM_ICON_TYPE.COMMON:
          iconBgid = 90000100;
          break;
        default:
          iconBgid = 90000100;
          break;
      }
    }
    return iconBgid;
  }

  public static void GetIconShowData(
    REWARD_TYPE reward_type,
    uint id,
    out int icon_id,
    out ITEM_ICON_TYPE icon_type,
    out RARITY_TYPE? rarity,
    out ELEMENT_TYPE element,
    out ELEMENT_TYPE element2,
    out EQUIPMENT_TYPE? magi_enable_icon_type,
    out int enemy_icon_id,
    out int enemy_icon_id2,
    out GET_TYPE getType,
    int exceed_cnt = 0)
  {
    icon_type = ITEM_ICON_TYPE.NONE;
    icon_id = -1;
    rarity = new RARITY_TYPE?();
    element = ELEMENT_TYPE.MAX;
    element2 = ELEMENT_TYPE.MAX;
    magi_enable_icon_type = new EQUIPMENT_TYPE?();
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
      case REWARD_TYPE.ITEM:
      case REWARD_TYPE.ABILITY_ITEM:
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(id);
        if (itemData == null)
          break;
        icon_type = itemData.type == ITEM_TYPE.USE_ITEM ? ITEM_ICON_TYPE.USE_ITEM : ITEM_ICON_TYPE.ITEM;
        icon_id = itemData.iconID;
        rarity = new RARITY_TYPE?(itemData.rarity);
        enemy_icon_id = itemData.enemyIconID;
        enemy_icon_id2 = itemData.enemyIconID2;
        break;
      case REWARD_TYPE.EQUIP_ITEM:
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
        if (equipItemData == null)
          break;
        icon_type = ItemIcon.GetItemIconType(equipItemData.type);
        icon_id = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
        rarity = new RARITY_TYPE?(equipItemData.rarity);
        element = equipItemData.GetTargetElementPriorityToTable();
        getType = equipItemData.getType;
        break;
      case REWARD_TYPE.SKILL_ITEM:
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(id);
        if (skillItemData == null)
          break;
        icon_type = ItemIcon.GetItemIconType(skillItemData.type);
        icon_id = skillItemData.iconID;
        rarity = new RARITY_TYPE?(skillItemData.rarity);
        element = skillItemData.skillAtkType;
        if (skillItemData.GetAttackElementNum() > 1)
          element2 = skillItemData.GetAttackElementByIndex(1);
        magi_enable_icon_type = skillItemData.GetEnableEquipType();
        break;
      case REWARD_TYPE.QUEST_ITEM:
        QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(id);
        if (questData == null)
          break;
        icon_type = ItemIcon.GetItemIconType(questData.questType);
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
        icon_id = enemyData.iconId;
        rarity = new RARITY_TYPE?(questData.rarity);
        element = enemyData.element;
        break;
      case REWARD_TYPE.AVATAR:
      case REWARD_TYPE.COMMON:
        icon_type = ITEM_ICON_TYPE.COMMON;
        icon_id = (int) id;
        break;
      case REWARD_TYPE.STAMP:
        icon_type = ITEM_ICON_TYPE.STAMP;
        icon_id = (int) id;
        break;
      case REWARD_TYPE.DEGREE:
        icon_type = ITEM_ICON_TYPE.DEGREE;
        icon_id = (int) id;
        break;
      case REWARD_TYPE.POINT_SHOP_POINT:
        icon_type = ITEM_ICON_TYPE.POINT_SHOP_ICON;
        icon_id = (int) id;
        break;
      case REWARD_TYPE.ACCESSORY:
        AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(id);
        if (data == null)
          break;
        icon_type = ITEM_ICON_TYPE.ACCESSORY;
        icon_id = (int) id;
        rarity = new RARITY_TYPE?(data.rarity);
        getType = data.getType;
        break;
      case REWARD_TYPE.EXP:
        icon_id = 3;
        break;
    }
  }

  public Transform transform => this._transform;

  public ITEM_ICON_TYPE iconType { get; private set; }

  public int GetItemID => this.itemID;

  public void SetItemID(int id) => this.itemID = id;

  public void SetItemID(uint id) => this.itemID = (int) id;

  public ulong GetUniqID => this.UniqID;

  public void SetUniqID(ulong id) => this.UniqID = id;

  public int GetItemNumber => this.itemNumber;

  public void SetItemNumber(int num) => this.itemNumber = num;

  public SortCompareData InitData => this.m_initData;

  public void SetInitData(SortCompareData _data) => this.m_initData = _data;

  public Transform _transform { get; private set; }

  public bool isIconLoaded => Object.op_Inequality((Object) this.icon.mainTexture, (Object) null);

  private void LoadIconTexture(ITEM_ICON_TYPE icon_type, int icon_id)
  {
    this.iconID = icon_id;
    ((Behaviour) this.icon).enabled = true;
    switch (icon_type)
    {
      case ITEM_ICON_TYPE.QUEST_ITEM:
        this.icon.mainTexture = (Texture) null;
        ResourceLoad.ItemIconLoadQuestItemIconTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
        {
          if (this.iconID != _icon_id)
            return;
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth + 1;
        int monsterIconSize;
        int rarityFrameSize;
        this.SetQuestIconItemSize(this.questIconSizeType, out monsterIconSize, out rarityFrameSize);
        this.icon.width = this.icon.height = monsterIconSize;
        this.rarityFrame.width = this.rarityFrame.height = rarityFrameSize;
        this.iconTypeSprite.depth = this.icon.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      case ITEM_ICON_TYPE.UNKNOWN:
        ((Behaviour) this.icon).enabled = false;
        break;
      case ITEM_ICON_TYPE.COMMON:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadCommonTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
        {
          if (this.iconID != _icon_id)
            return;
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      case ITEM_ICON_TYPE.STAMP:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadStampTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
        {
          if (this.iconID != _icon_id)
            return;
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      case ITEM_ICON_TYPE.DEGREE:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint) icon_id);
        if (data == null)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadDegreeIconTexture(this, data.type, (Action<ItemIcon, Texture, DEGREE_TYPE>) ((_item_icon, _tex, _type_id) =>
        {
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      case ITEM_ICON_TYPE.POINT_SHOP_ICON:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadPointShopPointIconTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _type_id) =>
        {
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      case ITEM_ICON_TYPE.ACCESSORY:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadAccessoryIconTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
        {
          if (this.iconID != _icon_id)
            return;
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
      default:
        this.icon.mainTexture = (Texture) null;
        if (icon_id <= 0)
        {
          ((Behaviour) this.icon).enabled = false;
          break;
        }
        ResourceLoad.ItemIconLoadItemIconTexture(this, icon_id, (Action<ItemIcon, Texture, int>) ((_item_icon, _tex, _icon_id) =>
        {
          if (this.iconID != _icon_id)
            return;
          this.icon.mainTexture = _tex;
          this.OnIconLoaded();
        }));
        this.icon.depth = this.rarityFrame.depth - 1;
        this.icon.width = this.icon.height = 64 /*0x40*/;
        this.rarityFrame.width = this.rarityFrame.height = 120;
        this.iconTypeSprite.depth = this.rarityFrame.depth + 2;
        if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
          break;
        this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
        break;
    }
  }

  private void OnIconLoaded()
  {
    if (this.onIconLoaded == null)
      return;
    this.onIconLoaded();
  }

  public void VisibleIcon(bool is_visible, bool is_button_enable = true)
  {
    this.isVisible = is_visible;
    if (is_visible)
    {
      if (is_button_enable)
        ((Behaviour) this.button).enabled = true;
      ((Behaviour) this.icon).enabled = true;
      ((Behaviour) this.label).enabled = true;
      ((Behaviour) this.iconTypeSprite).enabled = true;
      ((Behaviour) this.rarityFrame).enabled = true;
      ((Behaviour) this.rarityTextIcon).enabled = true;
      ((Behaviour) this.newIcon).enabled = true;
      if (Object.op_Inequality((Object) this.rewardBG, (Object) null))
        ((Behaviour) this.rewardBG).enabled = true;
      this.bg.mainTexture = this.frameTexture;
      ((Behaviour) this.enemyIconItem).enabled = true;
      if (this.enemyIconItem2 != null)
        ((Behaviour) this.enemyIconItem2).enabled = true;
      ((Behaviour) this.skillEnableEquipTypeIcon).enabled = true;
    }
    else
    {
      ((Behaviour) this.button).enabled = false;
      ((Behaviour) this.icon).enabled = false;
      ((Behaviour) this.label).enabled = false;
      ((Behaviour) this.iconTypeSprite).enabled = false;
      ((Behaviour) this.rarityFrame).enabled = false;
      ((Behaviour) this.rarityTextIcon).enabled = false;
      ((Behaviour) this.newIcon).enabled = false;
      if (Object.op_Inequality((Object) this.rewardBG, (Object) null))
        ((Behaviour) this.rewardBG).enabled = false;
      this.bg.mainTexture = (Texture) null;
      ((Behaviour) this.enemyIconItem).enabled = false;
      if (this.enemyIconItem2 != null)
        ((Behaviour) this.enemyIconItem2).enabled = false;
      ((Behaviour) this.skillEnableEquipTypeIcon).enabled = false;
    }
  }

  public void SetButtonColor(bool is_enable_button, bool is_instant)
  {
    if (is_enable_button)
    {
      this.button.defaultColor = this.button.hover = Color.white;
      this.button.pressed = Color.white;
      this.button.disabledColor = Color.white;
    }
    else
    {
      this.button.defaultColor = this.button.hover = Color.gray;
      this.button.pressed = Color.gray;
      this.button.disabledColor = Color.gray;
    }
    this.button.UpdateColor(is_instant);
  }

  public bool IsSelectIcon()
  {
    return ((Behaviour) this.toggleSelectFrame).enabled && this.toggleSelectFrame.value;
  }

  public void SelectIcon(bool isSelect)
  {
    if (!((Behaviour) this.toggleSelectFrame).enabled)
      return;
    this.toggleSelectFrame.value = isSelect;
  }

  public void SetRarity(
    ITEM_ICON_TYPE icon_type,
    RARITY_TYPE? rarity_type,
    bool disable_text,
    GET_TYPE getType)
  {
    string str = ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE == null || ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE.Length == 0 ? "EquipIconFrame_CD" : ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[0];
    if (!rarity_type.HasValue && icon_type != ITEM_ICON_TYPE.QUEST_ITEM)
    {
      ((Behaviour) this.rarityFrame).enabled = true;
      this.rarityFrame.spriteName = str;
      this.rarityTextIcon.spriteName = string.Empty;
      ((Behaviour) this.rarityTextIcon).enabled = false;
    }
    else
    {
      int rarity = rarity_type.HasValue ? (int) rarity_type.Value : 0;
      ((Behaviour) this.rarityFrame).enabled = true;
      ((Behaviour) this.rarityTextIcon).enabled = true;
      switch (icon_type)
      {
        case ITEM_ICON_TYPE.SKILL_ATTACK:
        case ITEM_ICON_TYPE.SKILL_SUPPORT:
        case ITEM_ICON_TYPE.SKILL_HEAL:
        case ITEM_ICON_TYPE.SKILL_PASSIVE:
        case ITEM_ICON_TYPE.SKILL_GROW:
          int index = (int) (icon_type - 10);
          ((Behaviour) this.rarityFrame).enabled = this.isVisible;
          this.rarityFrame.spriteName = ItemIcon.ITEM_ICON_SKILL_FRAME[index] + ItemIcon.ITEM_ICON_RARITY[rarity];
          UIBehaviour.SetRarityColorType(rarity, (UIWidget) this.rarityFrame);
          ((Behaviour) this.rarityTextIcon).enabled = !disable_text && this.isVisible;
          this.rarityTextIcon.spriteName = ItemIcon.GetRarityTextSpriteName(rarity_type, getType);
          break;
        case ITEM_ICON_TYPE.USE_ITEM:
          ((Behaviour) this.rarityFrame).enabled = false;
          ((Behaviour) this.rarityTextIcon).enabled = false;
          break;
        case ITEM_ICON_TYPE.QUEST_ITEM:
          if (rarity_type.HasValue)
          {
            ((Behaviour) this.rarityFrame).enabled = this.isVisible;
            this.rarityFrame.spriteName = ItemIcon.ITEM_ICON_MONSTER_RARITY_FRAME_SPRITE[rarity];
            UIBehaviour.SetRarityColorType(rarity, (UIWidget) this.rarityFrame);
            ((Behaviour) this.rarityTextIcon).enabled = !disable_text && this.isVisible;
            this.rarityTextIcon.spriteName = ItemIcon.GetRarityTextSpriteName(rarity_type, getType);
            break;
          }
          this.rarityFrame.spriteName = ItemIcon.ITEM_ICON_MONSTER_NORMAL_FRAME_SPRITE;
          ((Behaviour) this.rarityFrame).enabled = this.isVisible;
          UIBehaviour.SetRarityColorType(-1, (UIWidget) this.rarityFrame);
          this.rarityTextIcon.spriteName = string.Empty;
          ((Behaviour) this.rarityTextIcon).enabled = false;
          break;
        case ITEM_ICON_TYPE.UNKNOWN:
          ((Behaviour) this.rarityFrame).enabled = false;
          ((Behaviour) this.rarityTextIcon).enabled = false;
          break;
        default:
          ((Behaviour) this.rarityFrame).enabled = this.isVisible;
          this.rarityFrame.spriteName = ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[rarity];
          UIBehaviour.SetRarityColorType(rarity, (UIWidget) this.rarityFrame);
          ((Behaviour) this.rarityTextIcon).enabled = !disable_text && this.isVisible;
          this.rarityTextIcon.spriteName = ItemIcon.GetRarityTextSpriteName(rarity_type, getType);
          break;
      }
      if (!((Behaviour) this.rarityFrame).enabled && !((Behaviour) this.rarityTextIcon).enabled)
      {
        ((Behaviour) this.rarityFrame).enabled = true;
        this.rarityFrame.spriteName = str;
      }
      this.ChangeRarityFrameAtlus(rarity_type);
    }
  }

  public static string GetRarityTextSpriteName(RARITY_TYPE? rarityType, GET_TYPE getType)
  {
    int index = rarityType.HasValue ? (int) rarityType.Value : 0;
    return getType == GET_TYPE.PAY ? ItemIcon.ITEM_ICON_ITEM_RARITY_ICON_SPRITE[index] : ItemIcon.ITEM_ICON_ITEM_RARITY_ICON_EVENT_SPRITE[index];
  }

  public void ChangeRarityFrameAtlus(RARITY_TYPE? rarity)
  {
    if (!rarity.HasValue || this.spriteRarityAtlas == null || this.spriteRarityAtlas.Length == 0)
      return;
    RARITY_TYPE? nullable = rarity;
    int index;
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case RARITY_TYPE.S:
        case RARITY_TYPE.SS:
        case RARITY_TYPE.SSS:
          index = 1;
          goto label_5;
      }
    }
    index = 0;
label_5:
    string spriteName1 = this.rarityFrame.spriteName;
    this.rarityFrame.atlas = this.spriteRarityAtlas[index];
    this.rarityFrame.spriteName = spriteName1;
    string spriteName2 = this.rarityTextIcon.spriteName;
    this.rarityTextIcon.atlas = this.spriteRarityAtlas[index];
    this.rarityTextIcon.spriteName = spriteName2;
  }

  private void SetSkillEnableEquipIcon(EQUIPMENT_TYPE? type)
  {
    if (Object.op_Equality((Object) this.skillEnableEquipTypeIcon, (Object) null))
      return;
    ((Component) this.skillEnableEquipTypeIcon).gameObject.SetActive(type.HasValue);
    if (!type.HasValue)
      return;
    UIBehaviour.SetSkillEquipIconKind(((Component) this.skillEnableEquipTypeIcon).transform, type.Value, true);
  }

  public void SetRewardBG(bool is_visible)
  {
    if (Object.op_Equality((Object) this.rewardBG, (Object) null))
    {
      Transform transform = ((Component) this).gameObject.transform.Find("SPR_REWARD_BG");
      if (Object.op_Inequality((Object) transform, (Object) null))
        this.rewardBG = ((Component) transform).GetComponent<UISprite>();
    }
    if (Object.op_Equality((Object) this.rewardBG, (Object) null))
      return;
    ((Component) this.rewardBG).gameObject.SetActive(is_visible);
    ((Component) this.label).gameObject.SetActive(is_visible);
  }

  public void SetRewardCategoryInfo(REWARD_CATEGORY category)
  {
  }

  public void SetEnemyIconScale(ITEM_ICON_TYPE icon_type, float rate)
  {
    if (icon_type != ITEM_ICON_TYPE.QUEST_ITEM)
      return;
    this.icon.height = (int) ((double) this.icon.height * (double) rate);
    this.icon.width = (int) ((double) this.icon.width * (double) rate);
    this.rarityFrame.height = (int) ((double) this.rarityFrame.height * (double) rate);
    this.rarityFrame.width = (int) ((double) this.rarityFrame.width * (double) rate);
  }

  public void SetEnableCollider(bool is_enable)
  {
    BoxCollider component = ((Component) this).GetComponent<BoxCollider>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    ((Collider) component).enabled = is_enable;
  }

  public void SetFavoriteIcon(bool is_favorite)
  {
    if (Object.op_Equality((Object) this.favoriteSprite, (Object) null))
      return;
    ((Component) this.favoriteSprite).gameObject.SetActive(is_favorite);
    if (!Object.op_Inequality((Object) this.gridEquippingMark, (Object) null))
      return;
    this.gridEquippingMark.Reposition();
  }

  public void SetEquipGrowLimitBG(bool active)
  {
    if (!Object.op_Implicit((Object) this.equipGrowLimitBG))
      return;
    ((Behaviour) this.equipGrowLimitBG).enabled = active;
    UITweener[] components = ((Component) this.equipGrowLimitBG).GetComponents<UITweener>();
    int index = 0;
    for (int length = components.Length; index < length; ++index)
    {
      UITweener uiTweener = components[index];
      if (Object.op_Implicit((Object) uiTweener))
        ((Behaviour) uiTweener).enabled = active;
    }
  }

  public void SetEquipExt(EquipItemInfo info, params UILabel[] levelLabels)
  {
    bool active = info.IsLevelAndEvolveMax();
    this.SetEquipGrowLimitBG(active);
    Color color = active ? new Color(0.2117647f, 1f, 0.0f) : Color.white;
    int index = 0;
    for (int length = levelLabels.Length; index < length; ++index)
      levelLabels[index].color = color;
  }

  public void SetEquipExtInvertedColor(EquipItemInfo info, UILabel levelLabel)
  {
    bool active = info.IsLevelAndEvolveMax();
    this.SetEquipGrowLimitBG(active);
    if (active)
    {
      levelLabel.color = new Color(0.2117647f, 1f, 0.0f);
      levelLabel.effectColor = new Color(0.0f, 0.0862745f, 0.090196f);
    }
    else
    {
      levelLabel.color = new Color(0.0f, 0.17647f, 0.1843137f);
      levelLabel.effectColor = Color.white;
    }
  }

  public void SetDepth(int depth)
  {
    this.icon.depth = depth - 1;
    this.rarityFrame.depth = depth;
    this.iconTypeSprite.depth = depth + 2;
    if (!Object.op_Inequality((Object) this.iconTypeSpriteSub, (Object) null))
      return;
    this.iconTypeSpriteSub.depth = this.iconTypeSprite.depth - 1;
  }

  public virtual void SetGrayout(bool isActive)
  {
  }

  public void SetQuestIconItemSize(
    ItemIcon.QUEST_ICON_SIZE_TYPE sizeType,
    out int monsterIconSize,
    out int rarityFrameSize)
  {
    monsterIconSize = 0;
    rarityFrameSize = 0;
    switch (sizeType)
    {
      case ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT:
        monsterIconSize = 104;
        rarityFrameSize = 148;
        break;
      case ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL:
        monsterIconSize = 78;
        rarityFrameSize = 112 /*0x70*/;
        break;
      case ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST:
        monsterIconSize = 72;
        rarityFrameSize = 116;
        break;
      case ItemIcon.QUEST_ICON_SIZE_TYPE.SERIES_ARENA:
        monsterIconSize = 228;
        rarityFrameSize = 298;
        break;
    }
  }

  public Transform CloneIcon()
  {
    if (Object.op_Equality((Object) this.icon, (Object) null))
      return (Transform) null;
    Transform transform = ResourceUtility.Realizes((Object) ((Component) this.icon).gameObject);
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
    }
    return transform;
  }

  public bool IsVisbleNewIcon()
  {
    return !Object.op_Equality((Object) this.newIcon, (Object) null) && this.isVisible && ((Behaviour) this.newIcon).enabled && this.newIcon.isVisible;
  }

  public void SetJackpotIcon()
  {
    ((Behaviour) this.button).enabled = false;
    ((Behaviour) this.label).enabled = false;
    ((Behaviour) this.bg).enabled = false;
    ((Behaviour) this.iconTypeSprite).enabled = false;
    ((Behaviour) this.rarityFrame).enabled = false;
    ((Behaviour) this.rarityTextIcon).enabled = false;
    ((Behaviour) this.newIcon).enabled = false;
    if (Object.op_Inequality((Object) this.rewardBG, (Object) null))
      ((Behaviour) this.rewardBG).enabled = false;
    ((Behaviour) this.enemyIconItem).enabled = false;
    if (this.enemyIconItem2 != null)
      ((Behaviour) this.enemyIconItem2).enabled = false;
    ((Behaviour) this.skillEnableEquipTypeIcon).enabled = false;
  }

  public void SetSpinLogIcon()
  {
    this.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
    ((Component) this.label).gameObject.SetActive(this.itemNumber > 1);
    ((Behaviour) this.label).enabled = true;
    ((Component) this.label).transform.localPosition = new Vector3(5f, -20f);
  }

  public void SetSpinUserLogIcon()
  {
    this.transform.localScale = Vector2.op_Implicit(new Vector2(0.9f, 0.9f));
    ((Component) this.label).gameObject.SetActive(this.itemNumber > 1);
    if (Object.op_Inequality((Object) this.wheelNumBackBG, (Object) null))
      ((Component) this.wheelNumBackBG).gameObject.SetActive(this.itemNumber > 1);
    ((Behaviour) this.label).enabled = true;
    ((Component) this.label).transform.localPosition = new Vector3(0.0f, -27f);
    this.label.alignment = NGUIText.Alignment.Center;
  }

  public void SetSpinMachineItem()
  {
    this.icon.depth = 4;
    this.SetJackpotIcon();
  }

  public void SetSpinMachineSkillItem()
  {
    this.icon.depth = 4;
    this.bg.depth = 3;
    this.rarityFrame.depth = 2;
    this.rarityTextIcon.depth = 4;
    ((Behaviour) this.button).enabled = false;
    ((Behaviour) this.label).enabled = false;
    ((Behaviour) this.iconTypeSprite).enabled = false;
    ((Behaviour) this.newIcon).enabled = false;
    if (Object.op_Inequality((Object) this.rewardBG, (Object) null))
      ((Behaviour) this.rewardBG).enabled = false;
    ((Behaviour) this.enemyIconItem).enabled = false;
    if (this.enemyIconItem2 != null)
      ((Behaviour) this.enemyIconItem2).enabled = false;
    ((Behaviour) this.skillEnableEquipTypeIcon).enabled = false;
  }

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
    public ItemIcon.QUEST_ICON_SIZE_TYPE questIconSizeType;
  }

  public enum QUEST_ICON_SIZE_TYPE
  {
    DEFAULT,
    REWARD_DELIVERY_DETAIL,
    REWARD_DELIVERY_LIST,
    SERIES_ARENA,
  }
}
