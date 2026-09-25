// Decompiled with JetBrains decompiler
// Type: SortBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public abstract class SortBase : GameSection
{
  protected SortSettings baseSortOrder;
  protected SortSettings sortOrder;

  public override void Initialize()
  {
    this.sortOrder = (SortSettings) GameSection.GetEventData();
    this.baseSortOrder = this.sortOrder.Clone();
    base.Initialize();
  }

  protected void OnQueryEvent_Rarity(out int _index, out bool _is_enable)
  {
    _index = (int) GameSection.GetEventData();
    int num = 1 << _index;
    if ((this.sortOrder.rarity & num) == 0)
    {
      _is_enable = true;
      this.sortOrder.rarity += num;
    }
    else
    {
      _is_enable = false;
      this.sortOrder.rarity -= num;
    }
  }

  protected void OnQueryEvent_Type(out int _index, out bool _is_enable)
  {
    _index = (int) GameSection.GetEventData();
    int num = 1 << _index;
    if ((this.sortOrder.type & num) == 0)
    {
      _is_enable = true;
      this.sortOrder.type += num;
    }
    else
    {
      _is_enable = false;
      this.sortOrder.type -= num;
    }
  }

  protected void OnQueryEvent_EquipFilter(out int _index, out bool _is_enable)
  {
    _index = (int) GameSection.GetEventData();
    int num = 1 << _index;
    if ((this.sortOrder.equipFilter & num) == 0)
    {
      _is_enable = true;
      this.sortOrder.equipFilter += num;
    }
    else
    {
      _is_enable = false;
      this.sortOrder.equipFilter -= num;
    }
  }

  protected void OnQueryEvent_Element(out int _index, out bool _is_enable)
  {
    _index = (int) GameSection.GetEventData();
    int num = 1 << _index;
    if ((this.sortOrder.element & num) == 0)
    {
      _is_enable = true;
      this.sortOrder.element += num;
    }
    else
    {
      _is_enable = false;
      this.sortOrder.element -= num;
    }
  }

  protected void OnQuery_REQUIREMENT()
  {
    this.sortOrder.requirement = (SortBase.SORT_REQUIREMENT) GameSection.GetEventData();
  }

  protected void OnQuery_ORDER_TYPE()
  {
    this.sortOrder.orderTypeAsc = (int) GameSection.GetEventData() == 0;
  }

  protected void OnQuery_BACK()
  {
    GameSection.SetEventData((object) null);
    GameSection.BackSection();
  }

  protected void OnQuery_SORTING()
  {
    GameSaveData.instance.SetSortBit(this.sortOrder);
    GameSaveData.Save();
    GameSection.SetEventData((object) this.sortOrder);
    GameSection.BackSection();
  }

  public enum DIALOG_TYPE
  {
    WEAPON,
    ARMOR,
    SKILL,
    STORAGE_EQUIP,
    STORAGE_SKILL,
    USE_ITEM,
    MATERIAL,
    SMITH_CREATE_WEAPON,
    SMITH_CREATE_ARMOR,
    SMITH_CREATE_PICKUP_WEAPON,
    SMITH_CREATE_PICKUP_ARMOR,
    QUEST,
    ABILITY_ITEM,
    ACCESSORY,
    TYPE_FILTERABLE_WEAPON,
    TYPE_FILTERABLE_ARMOR,
  }

  [Flags]
  public enum RARITY
  {
    D = 1,
    C = 2,
    B = 4,
    A = 8,
    S = 16, // 0x00000010
    SS = 32, // 0x00000020
    SSS = 64, // 0x00000040
    ALL = SSS | SS | S | A | B | C | D, // 0x0000007F
  }

  [Flags]
  public enum ELEMENT
  {
    FIRE = 1,
    WATER = 2,
    THUNDER = 4,
    SOIL = 8,
    LIGHT = 16, // 0x00000010
    DARK = 32, // 0x00000020
    NONE = 64, // 0x00000040
    ALL = NONE | DARK | LIGHT | SOIL | THUNDER | WATER | FIRE, // 0x0000007F
  }

  [Flags]
  public enum TYPE
  {
    NONE = 0,
    ONE_HAND_SWORD = 1,
    TWO_HAND_SWORD = 2,
    SPEAR = 4,
    PAIR_SWORDS = 8,
    ARROW = 16, // 0x00000010
    ARMOR = 32, // 0x00000020
    HELM = 64, // 0x00000040
    ARM = 128, // 0x00000080
    LEG = 256, // 0x00000100
    WEAPON_ALL = ARROW | PAIR_SWORDS | SPEAR | TWO_HAND_SWORD | ONE_HAND_SWORD, // 0x0000001F
    ARMOR_ALL = LEG | ARM | HELM | ARMOR, // 0x000001E0
    EQUIP_ALL = ARMOR_ALL | WEAPON_ALL, // 0x000001FF
    FIRE = ONE_HAND_SWORD, // 0x00000001
    WARTER = TWO_HAND_SWORD, // 0x00000002
    THUNDER = SPEAR, // 0x00000004
    SOIL = PAIR_SWORDS, // 0x00000008
    LIGHT = ARROW, // 0x00000010
    DARK = ARMOR, // 0x00000020
    ELEMENT_ALL = DARK | LIGHT | SOIL | THUNDER | WARTER | FIRE, // 0x0000003F
    SKILL_ATTACK = FIRE, // 0x00000001
    SKILL_SUPPORT = WARTER, // 0x00000002
    SKILL_HEAL = THUNDER, // 0x00000004
    SKILL_RESTRAINT = SOIL, // 0x00000008
    SKILL_SPECIAL = LIGHT, // 0x00000010
    SKILL_BUFF = DARK, // 0x00000020
    SKILL_SKILL_BUFF = HELM, // 0x00000040
    SKILL_PASSIVE = ARM, // 0x00000080
    SKILL_MOTION = LEG, // 0x00000100
    SKILL_LIMITED = 512, // 0x00000200
    SKILL_GROW = 1024, // 0x00000400
    SKILL_ALL = SKILL_GROW | SKILL_PASSIVE | SKILL_HEAL | SKILL_SUPPORT | SKILL_ATTACK, // 0x00000487
    MATERIAL_COMMON = SKILL_ATTACK, // 0x00000001
    MATERIAL_UNIQUE = SKILL_SUPPORT, // 0x00000002
    MATERIAL_LITHOGRAPH = SKILL_HEAL, // 0x00000004
    MATERIAL_EQUIP = SKILL_RESTRAINT, // 0x00000008
    MATERIAL_METAL = SKILL_SPECIAL, // 0x00000010
    MATERIAL_ALL = MATERIAL_METAL | MATERIAL_EQUIP | MATERIAL_LITHOGRAPH | MATERIAL_UNIQUE | MATERIAL_COMMON, // 0x0000001F
    ENEMY_GORILLA = MATERIAL_COMMON, // 0x00000001
    ENEMY_RABBIT = MATERIAL_UNIQUE, // 0x00000002
    ENEMY_CHIMERA = MATERIAL_LITHOGRAPH, // 0x00000004
    ENEMY_WOLF = MATERIAL_EQUIP, // 0x00000008
    ENEMY_DRAGON = MATERIAL_METAL, // 0x00000010
    ENEMY_DRAKE = SKILL_BUFF, // 0x00000020
    ENEMY_WYVERN = SKILL_SKILL_BUFF, // 0x00000040
    ENEMY_UNDEAD_KNIGHT = SKILL_PASSIVE, // 0x00000080
    ENEMY_WRAITH = SKILL_MOTION, // 0x00000100
    ENEMY_GIANT = SKILL_LIMITED, // 0x00000200
    ENEMY_GOLEM = SKILL_GROW, // 0x00000400
    ENEMY_ELEMENTAL = 2048, // 0x00000800
    ENEMY_CHICKEN = 4096, // 0x00001000
    ENEMY_MUSHROOM = 8192, // 0x00002000
    ENEMY_COW = 16384, // 0x00004000
    ENEMY_FROG = 32768, // 0x00008000
    ENEMY_BAT = 65536, // 0x00010000
    ENEMY_SLIME = 131072, // 0x00020000
    ENEMY_SAHUAGIN = 262144, // 0x00040000
    ENEMY_ALL = ENEMY_SAHUAGIN | ENEMY_SLIME | ENEMY_BAT | ENEMY_FROG | ENEMY_COW | ENEMY_MUSHROOM | ENEMY_CHICKEN | ENEMY_ELEMENTAL | ENEMY_GOLEM | ENEMY_GIANT | ENEMY_WRAITH | ENEMY_UNDEAD_KNIGHT | ENEMY_WYVERN | ENEMY_DRAKE | ENEMY_DRAGON | ENEMY_WOLF | ENEMY_CHIMERA | ENEMY_RABBIT | ENEMY_GORILLA, // 0x0007FFFF
  }

  [Flags]
  public enum SORT_REQUIREMENT
  {
    ID = 1,
    NUM = 2,
    GET = 4,
    RARITY = 8,
    LV = 16, // 0x00000010
    ATK = 32, // 0x00000020
    DEF = 64, // 0x00000040
    SALE = 128, // 0x00000080
    SOCKET = 256, // 0x00000100
    PRICE = 512, // 0x00000200
    DIFFICULTY = 1024, // 0x00000400
    ENEMY = 2048, // 0x00000800
    HP = 4096, // 0x00001000
    ELEMENT = 8192, // 0x00002000
    ELEM_ATK = 16384, // 0x00004000
    ELEM_DEF = 32768, // 0x00008000
    SKILL_TYPE = 65536, // 0x00010000
    REQUIREMENT_ALL_EQUIP_BIT = ELEMENT | SOCKET | SALE | LV | RARITY | GET, // 0x0000219C
    REQUIREMENT_WEAPON_BIT = REQUIREMENT_ALL_EQUIP_BIT | ELEM_ATK | ATK, // 0x000061BC
    REQUIREMENT_ARMORS_BIT = REQUIREMENT_ALL_EQUIP_BIT | ELEM_DEF | DEF, // 0x0000A1DC
    REQUIREMENT_ITEM_BIT = SALE | RARITY | NUM, // 0x0000008A
    REQUIREMENT_CREATE_WEAPON_BIT = ELEMENT | SOCKET | ATK | RARITY, // 0x00002128
    REQUIREMENT_CREATE_PICKUP_WEAPON_BIT = REQUIREMENT_CREATE_WEAPON_BIT | ID, // 0x00002129
    REQUIREMENT_CREATE_ARMORS_BIT = ELEMENT | SOCKET | DEF | RARITY, // 0x00002148
    REQUIREMENT_CREATE_PICKUP_ARMORS_BIT = REQUIREMENT_CREATE_ARMORS_BIT | ID, // 0x00002149
    REQUIREMENT_SKILL_BIT = SKILL_TYPE | HP | SALE | DEF | ATK | LV | RARITY | GET, // 0x000110FC
    REQUIREMENT_QUEST_BIT = DIFFICULTY | RARITY | NUM | ID, // 0x0000040B
  }

  [Flags]
  public enum EQUIP_FILTER
  {
    PAY = 1,
    NO_PAY = 2,
    PAY_ALL = NO_PAY | PAY, // 0x00000003
    CREATABLE = 4,
    NO_CREATABLE = 8,
    CREATABLE_ALL = NO_CREATABLE | CREATABLE, // 0x0000000C
    OBTAINED = 16, // 0x00000010
    NO_OBTAINED = 32, // 0x00000020
    OBTAINED_ALL = NO_OBTAINED | OBTAINED, // 0x00000030
    GET_EQUIP_FILTER_ALL = OBTAINED_ALL | CREATABLE_ALL | PAY_ALL, // 0x0000003F
  }
}
