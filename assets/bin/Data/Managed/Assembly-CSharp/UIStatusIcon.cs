using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIStatusIcon
{
	public enum STATUS_TYPE
	{
		PARALYZE,
		POISON,
		BURNING,
		MOVE_SPEED_DOWN,
		DEADLY_POISON,
		INVINCIBLECOUNT,
		ATTACK_SPEED_UP,
		MOVE_SPEED_UP,
		ATTACK_NORMAL,
		ATTACK_FIRE,
		ATTACK_WATER,
		ATTACK_THUNDER,
		ATTACK_SOIL,
		ATTACK_LIGHT,
		ATTACK_DARK,
		ATTACK_ALLELEMENT,
		DEFENCE_NORMAL,
		DEFENCE_FIRE,
		DEFENCE_WATER,
		DEFENCE_THUNDER,
		DEFENCE_SOIL,
		DEFENCE_LIGHT,
		DEFENCE_DARK,
		DEFENCE_ALLELEMENT,
		REGENERATE,
		FREEZE,
		ELECTRIC_SHOCK,
		INK_SPLASH,
		POISON_DAMAGE_DOWN,
		POISON_GUARD,
		BURNING_DAMAGE_DOWN,
		BURNING_GUARD,
		SUPER_ARMOR,
		DEFENCE_DOWN,
		SHIELD,
		SLIDE,
		PARALYZE_GUARD,
		SILENCE,
		SHADOWSEALING,
		REGENERATE_PROPORTION,
		ABSORB_NORMAL,
		ABSORB_FIRE,
		ABSORB_WATER,
		ABSORB_THUNDER,
		ABSORB_SOIL,
		ABSORB_LIGHT,
		ABSORB_DARK,
		ABSORB_ALL_ELEMENT,
		DEF_DOWN_FIRE,
		DEF_DOWN_WATER,
		DEF_DOWN_THUNDER,
		DEF_DOWN_SOIL,
		DEF_DOWN_LIGHT,
		DEF_DOWN_DARK,
		DEF_DOWN_ALLELEMENT,
		ATTACK_SPEED_DOWN,
		AUTO_REVIVE,
		WARP_BY_AVOID,
		INVINCIBLE_FIRE,
		INVINCIBLE_WATER,
		INVINCIBLE_THUNDER,
		INVINCIBLE_SOIL,
		DAMAGE_UP_NORMAL,
		DAMAGE_UP_FROM_AVOID,
		SKILL_HEAL_SPEEDUP,
		GAUGE_INCREASE_UP,
		SKILL_CHARGE_WHEN_DAMAGED,
		INVINCIBLE_LIGHT,
		INVINCIBLE_DARK,
		CANT_HEAL_HP,
		BLIND,
		INVINCIBLE_BADSTATUS,
		SLIDE_ICE,
		LIGHT_RING,
		EROSION,
		STONE,
		SOIL_SHOCK,
		CONCUSSION,
		ATTACK_ALL,
		BLEEDING,
		INVINCIBLE_NORMAL,
		INVINCIBLE_ALLELEMENT,
		INVINCIBLE_ALL,
		ACID,
		DAMAGE_MOTION_STOP,
		CORRUPTION,
		STIGMATA,
		CYCLONIC_THUNDERSTORM,
		MAX
	}

	[Serializable]
	public class IconInfo
	{
		public UISprite icon;

		[SerializeField]
		private TweenColor tween;

		[HideInInspector]
		public bool isFieldBuff;

		public void SetTweenEnable(bool enable)
		{
			if ((object)tween != null)
			{
				tween.enabled = enable;
			}
		}
	}

	public static readonly STATUS_TYPE[] NON_BUFF_STATUS = new STATUS_TYPE[6]
	{
		STATUS_TYPE.PARALYZE,
		STATUS_TYPE.FREEZE,
		STATUS_TYPE.SHADOWSEALING,
		STATUS_TYPE.LIGHT_RING,
		STATUS_TYPE.CONCUSSION,
		STATUS_TYPE.DAMAGE_MOTION_STOP
	};

	private readonly Color defaultTintColor = Color.white;

	private readonly Color fieldBuffTintColor = new Color32(159, 104, 104, byte.MaxValue);

	[SerializeField]
	protected IconInfo[] statusIcons;

	[SerializeField]
	protected string[] spriteNames = new string[88];

	public Character target;

	public void UpDateStatusIcon()
	{
		if (target == null)
		{
			return;
		}
		int num = statusIcons.Length;
		int num2 = 0;
		for (int i = 0; i < 88; i++)
		{
			if (CheckStatus((STATUS_TYPE)i, isFieldBuff: true) && _SetStatusIcon((STATUS_TYPE)i, num2, isFieldBuff: true) && ++num2 >= num)
			{
				break;
			}
		}
		for (int j = 0; j < 88; j++)
		{
			if (CheckStatus((STATUS_TYPE)j) && _SetStatusIcon((STATUS_TYPE)j, num2, isFieldBuff: false) && ++num2 >= num)
			{
				break;
			}
		}
		for (int k = num2; k < num; k++)
		{
			statusIcons[k].isFieldBuff = false;
			statusIcons[k].SetTweenEnable(enable: false);
			statusIcons[k].icon.color = defaultTintColor;
			statusIcons[k].icon.gameObject.SetActive(value: false);
		}
	}

	private bool _SetStatusIcon(STATUS_TYPE type, int index, bool isFieldBuff)
	{
		string iconSpriteNameByStatusType = GetIconSpriteNameByStatusType(type);
		if (statusIcons[index].icon.atlas.GetSprite(iconSpriteNameByStatusType) == null)
		{
			return false;
		}
		statusIcons[index].icon.spriteName = iconSpriteNameByStatusType;
		statusIcons[index].icon.color = defaultTintColor;
		statusIcons[index].SetTweenEnable(isFieldBuff);
		statusIcons[index].icon.gameObject.SetActive(value: true);
		return true;
	}

	public int RotatedUpdateStatusIcon(int checkFirstStatus, BuffParam buffParam, List<int> nonBuff)
	{
		int result = checkFirstStatus;
		int num = statusIcons.Length;
		int num2 = 0;
		for (int i = 0; i < 88; i++)
		{
			if (num2 >= num)
			{
				break;
			}
			int num3 = i + checkFirstStatus;
			if (num3 >= 88)
			{
				num3 %= 88;
			}
			if (CheckStatus((STATUS_TYPE)num3, buffParam, nonBuff))
			{
				IconInfo obj = statusIcons[num2];
				string iconSpriteNameByStatusType = GetIconSpriteNameByStatusType((STATUS_TYPE)num3);
				obj.icon.spriteName = iconSpriteNameByStatusType;
				obj.icon.gameObject.SetActive(value: true);
				num2++;
				result = num3;
			}
		}
		for (int j = num2; j < num; j++)
		{
			statusIcons[j].icon.gameObject.SetActive(value: false);
		}
		return result;
	}

	public bool HasActiveMultipleBuffIcon(BuffParam buffParam, List<int> nonBuff)
	{
		HashSet<string> hashSet = new HashSet<string>();
		int num = 0;
		for (int i = 0; i < 88; i++)
		{
			if (!CheckStatus((STATUS_TYPE)i, buffParam, nonBuff))
			{
				continue;
			}
			string iconSpriteNameByStatusType = GetIconSpriteNameByStatusType((STATUS_TYPE)i);
			if (!string.IsNullOrEmpty(iconSpriteNameByStatusType) && !hashSet.Contains(iconSpriteNameByStatusType))
			{
				hashSet.Add(iconSpriteNameByStatusType);
				num++;
				if (num > 1)
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool CheckStatus(STATUS_TYPE type, bool isFieldBuff = false)
	{
		return CheckStatus(type, target, isFieldBuff);
	}

	public static bool CheckStatus(STATUS_TYPE type, Character character, bool isFieldBuff)
	{
		switch (type)
		{
		case STATUS_TYPE.PARALYZE:
			if (character.IsParalyze())
			{
				return !isFieldBuff;
			}
			return false;
		case STATUS_TYPE.FREEZE:
			if (character.IsFreeze())
			{
				return !isFieldBuff;
			}
			return false;
		case STATUS_TYPE.SHADOWSEALING:
			if (character.IsDebuffShadowSealing())
			{
				return !isFieldBuff;
			}
			return false;
		case STATUS_TYPE.LIGHT_RING:
			if (character.IsLightRing())
			{
				return !isFieldBuff;
			}
			return false;
		case STATUS_TYPE.CONCUSSION:
			if (character.IsConcussion())
			{
				return !isFieldBuff;
			}
			return false;
		default:
			return CheckStatus(type, character.buffParam, isFieldBuff);
		}
	}

	public static bool CheckStatus(STATUS_TYPE type, BuffParam buffParam, List<int> nonBuffStatus)
	{
		if (!CheckStatus(type, buffParam))
		{
			if (nonBuffStatus != null)
			{
				return CheckStatus(type, nonBuffStatus);
			}
			return false;
		}
		return true;
	}

	private static bool CheckStatus(STATUS_TYPE type, List<int> nonBuffStatus)
	{
		return nonBuffStatus.Contains((int)type);
	}

	private static bool CheckStatus(STATUS_TYPE type, BuffParam buffParam, bool isFieldBuff = false)
	{
		switch (type)
		{
		case STATUS_TYPE.POISON:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON, isFieldBuff);
		case STATUS_TYPE.BLEEDING:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BLEEDING, isFieldBuff);
		case STATUS_TYPE.BURNING:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURNING, isFieldBuff);
		case STATUS_TYPE.DEADLY_POISON:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEADLY_POISON, isFieldBuff);
		case STATUS_TYPE.INVINCIBLECOUNT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_BADSTATUS:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS, isFieldBuff);
		case STATUS_TYPE.ATTACK_SPEED_UP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_UP, isFieldBuff);
		case STATUS_TYPE.ATTACK_SPEED_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, isFieldBuff);
		case STATUS_TYPE.MOVE_SPEED_UP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_UP, isFieldBuff);
		case STATUS_TYPE.MOVE_SPEED_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, isFieldBuff);
		case STATUS_TYPE.ATTACK_NORMAL:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_NORMAL, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_FIRE:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_FIRE, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_FIRE, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_WATER:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_WATER, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_WATER, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_THUNDER:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_THUNDER, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_SOIL:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SOIL, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_SOIL, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_LIGHT:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_LIGHT, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_DARK:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_DARK, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_DARK, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_ALLELEMENT:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.ATTACK_ALL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_ALL, isFieldBuff);
		case STATUS_TYPE.DEFENCE_NORMAL:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_NORMAL, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_FIRE:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_FIRE, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_FIRE, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_WATER:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_WATER, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_WATER, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_THUNDER:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_THUNDER, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_SOIL:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_SOIL, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_SOIL, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_LIGHT:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_LIGHT, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_DARK:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_DARK, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_DARK, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_ALLELEMENT:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.REGENERATE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.REGENERATE, isFieldBuff);
		case STATUS_TYPE.ELECTRIC_SHOCK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ELECTRIC_SHOCK, isFieldBuff);
		case STATUS_TYPE.SOIL_SHOCK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SOIL_SHOCK, isFieldBuff);
		case STATUS_TYPE.INK_SPLASH:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INK_SPLASH, isFieldBuff);
		case STATUS_TYPE.POISON_DAMAGE_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON_DAMAGE_DOWN, isFieldBuff);
		case STATUS_TYPE.POISON_GUARD:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.POISON_GUARD, isFieldBuff);
		case STATUS_TYPE.BURNING_DAMAGE_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURN_DAMAGE_DOWN, isFieldBuff);
		case STATUS_TYPE.BURNING_GUARD:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURN_GUARD, isFieldBuff);
		case STATUS_TYPE.SUPER_ARMOR:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SUPER_ARMOR, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.DEFENCE_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_FIRE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_WATER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_THUNDER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_SOIL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK, isFieldBuff);
		case STATUS_TYPE.DEF_DOWN_ALLELEMENT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT, isFieldBuff);
		case STATUS_TYPE.SHIELD:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SHIELD, isFieldBuff);
		case STATUS_TYPE.SLIDE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SLIDE, isFieldBuff);
		case STATUS_TYPE.PARALYZE_GUARD:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.PARALYZE_GUARD, isFieldBuff);
		case STATUS_TYPE.SILENCE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SILENCE, isFieldBuff);
		case STATUS_TYPE.REGENERATE_PROPORTION:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.REGENERATE_PROPORTION, isFieldBuff);
		case STATUS_TYPE.ABSORB_NORMAL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_NORMAL, isFieldBuff);
		case STATUS_TYPE.ABSORB_FIRE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_FIRE, isFieldBuff);
		case STATUS_TYPE.ABSORB_WATER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_WATER, isFieldBuff);
		case STATUS_TYPE.ABSORB_THUNDER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_THUNDER, isFieldBuff);
		case STATUS_TYPE.ABSORB_SOIL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_SOIL, isFieldBuff);
		case STATUS_TYPE.ABSORB_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_LIGHT, isFieldBuff);
		case STATUS_TYPE.ABSORB_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_DARK, isFieldBuff);
		case STATUS_TYPE.ABSORB_ALL_ELEMENT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ABSORB_ALL_ELEMENT, isFieldBuff);
		case STATUS_TYPE.AUTO_REVIVE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.AUTO_REVIVE, isFieldBuff);
		case STATUS_TYPE.WARP_BY_AVOID:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.WARP_BY_AVOID, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_FIRE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_FIRE, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_WATER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_WATER, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_THUNDER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_THUNDER, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_SOIL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_SOIL, isFieldBuff);
		case STATUS_TYPE.DAMAGE_UP_NORMAL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DAMAGE_UP_NORMAL, isFieldBuff);
		case STATUS_TYPE.DAMAGE_UP_FROM_AVOID:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DAMAGE_UP_FROM_AVOID, isFieldBuff);
		case STATUS_TYPE.SKILL_HEAL_SPEEDUP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SKILL_HEAL_SPEEDUP, isFieldBuff);
		case STATUS_TYPE.GAUGE_INCREASE_UP:
			if (!buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_UP, isFieldBuff))
			{
				return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_UP, isFieldBuff);
			}
			return true;
		case STATUS_TYPE.SKILL_CHARGE_WHEN_DAMAGED:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_LIGHT, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_DARK, isFieldBuff);
		case STATUS_TYPE.CANT_HEAL_HP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP, isFieldBuff);
		case STATUS_TYPE.BLIND:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BLIND, isFieldBuff);
		case STATUS_TYPE.SLIDE_ICE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE, isFieldBuff);
		case STATUS_TYPE.EROSION:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.EROSION, isFieldBuff);
		case STATUS_TYPE.STONE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.STONE, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_NORMAL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_NORMAL, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_ALLELEMENT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_ALL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_ALL, isFieldBuff);
		case STATUS_TYPE.ACID:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ACID, isFieldBuff);
		case STATUS_TYPE.CORRUPTION:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CORRUPTION, isFieldBuff);
		case STATUS_TYPE.STIGMATA:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.STIGMATA, isFieldBuff);
		case STATUS_TYPE.CYCLONIC_THUNDERSTORM:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM, isFieldBuff);
		default:
			return false;
		}
	}

	private string GetIconSpriteNameByStatusType(STATUS_TYPE statusType)
	{
		switch (statusType)
		{
		case STATUS_TYPE.PARALYZE:
			return "Pala";
		case STATUS_TYPE.POISON:
			return "Poizon";
		case STATUS_TYPE.BLEEDING:
			return "Bleeding";
		case STATUS_TYPE.BURNING:
			return "Burn";
		case STATUS_TYPE.DEADLY_POISON:
			return "DeadlyPoison";
		case STATUS_TYPE.INVINCIBLECOUNT:
		case STATUS_TYPE.INVINCIBLE_BADSTATUS:
			return string.Empty;
		case STATUS_TYPE.ATTACK_SPEED_UP:
			return "AtkSpeedUp";
		case STATUS_TYPE.ATTACK_SPEED_DOWN:
			return "AtkSpeedDown";
		case STATUS_TYPE.MOVE_SPEED_UP:
			return "SpeedUp";
		case STATUS_TYPE.MOVE_SPEED_DOWN:
			return "SpeedDown";
		case STATUS_TYPE.ATTACK_NORMAL:
		case STATUS_TYPE.ATTACK_FIRE:
		case STATUS_TYPE.ATTACK_WATER:
		case STATUS_TYPE.ATTACK_THUNDER:
		case STATUS_TYPE.ATTACK_SOIL:
		case STATUS_TYPE.ATTACK_LIGHT:
		case STATUS_TYPE.ATTACK_DARK:
		case STATUS_TYPE.ATTACK_ALLELEMENT:
		case STATUS_TYPE.DAMAGE_UP_NORMAL:
		case STATUS_TYPE.DAMAGE_UP_FROM_AVOID:
		case STATUS_TYPE.ATTACK_ALL:
			return "AtkUp";
		case STATUS_TYPE.DEFENCE_NORMAL:
		case STATUS_TYPE.DEFENCE_FIRE:
		case STATUS_TYPE.DEFENCE_WATER:
		case STATUS_TYPE.DEFENCE_THUNDER:
		case STATUS_TYPE.DEFENCE_SOIL:
		case STATUS_TYPE.DEFENCE_LIGHT:
		case STATUS_TYPE.DEFENCE_DARK:
		case STATUS_TYPE.DEFENCE_ALLELEMENT:
			return "DefUp";
		case STATUS_TYPE.REGENERATE:
		case STATUS_TYPE.REGENERATE_PROPORTION:
			return "Recovery";
		case STATUS_TYPE.FREEZE:
			return "Frozen";
		case STATUS_TYPE.ELECTRIC_SHOCK:
			return "ElectricShock";
		case STATUS_TYPE.SOIL_SHOCK:
			return "SoilShock";
		case STATUS_TYPE.INK_SPLASH:
			return "InkSplash";
		case STATUS_TYPE.POISON_DAMAGE_DOWN:
			return "PoisonDamageDown";
		case STATUS_TYPE.POISON_GUARD:
			return "PoisonGuard";
		case STATUS_TYPE.BURNING_DAMAGE_DOWN:
			return "BurningDamageDown";
		case STATUS_TYPE.BURNING_GUARD:
			return "BurningGuard";
		case STATUS_TYPE.SUPER_ARMOR:
			return "SuperArmor";
		case STATUS_TYPE.DEFENCE_DOWN:
		case STATUS_TYPE.DEF_DOWN_ALLELEMENT:
			return "DefDown";
		case STATUS_TYPE.DEF_DOWN_FIRE:
			return "DefDownFire";
		case STATUS_TYPE.DEF_DOWN_WATER:
			return "DefDownWater";
		case STATUS_TYPE.DEF_DOWN_THUNDER:
			return "DefDownThunder";
		case STATUS_TYPE.DEF_DOWN_SOIL:
			return "DefDownSoil";
		case STATUS_TYPE.DEF_DOWN_LIGHT:
			return "DefDownLight";
		case STATUS_TYPE.DEF_DOWN_DARK:
			return "DefDownDark";
		case STATUS_TYPE.SHIELD:
			return "Shield";
		case STATUS_TYPE.SLIDE:
			return "Slide";
		case STATUS_TYPE.PARALYZE_GUARD:
			return "ParalyzeGuard";
		case STATUS_TYPE.SILENCE:
			return "Silence";
		case STATUS_TYPE.SHADOWSEALING:
			return "ShadowSealing";
		case STATUS_TYPE.ABSORB_FIRE:
			return "AbsorbFire";
		case STATUS_TYPE.ABSORB_WATER:
			return "AbsorbWater";
		case STATUS_TYPE.ABSORB_THUNDER:
			return "AbsorbThunder";
		case STATUS_TYPE.ABSORB_SOIL:
			return "AbsorbSoil";
		case STATUS_TYPE.ABSORB_LIGHT:
			return "AbsorbLight";
		case STATUS_TYPE.ABSORB_DARK:
			return "AbsorbDark";
		case STATUS_TYPE.AUTO_REVIVE:
			return "AutoRevive";
		case STATUS_TYPE.WARP_BY_AVOID:
			return "Warp";
		case STATUS_TYPE.INVINCIBLE_FIRE:
			return "dmgOffFire";
		case STATUS_TYPE.INVINCIBLE_WATER:
			return "dmgOffWater";
		case STATUS_TYPE.INVINCIBLE_THUNDER:
			return "dmgOffThunder";
		case STATUS_TYPE.INVINCIBLE_SOIL:
			return "dmgOffSoil";
		case STATUS_TYPE.SKILL_HEAL_SPEEDUP:
		case STATUS_TYPE.GAUGE_INCREASE_UP:
		case STATUS_TYPE.SKILL_CHARGE_WHEN_DAMAGED:
			return "GaugeIncreaseUp";
		case STATUS_TYPE.INVINCIBLE_LIGHT:
			return "dmgOffLight";
		case STATUS_TYPE.INVINCIBLE_DARK:
			return "dmgOffDark";
		case STATUS_TYPE.CANT_HEAL_HP:
			return "CantHealHp";
		case STATUS_TYPE.BLIND:
			return "Blind";
		case STATUS_TYPE.SLIDE_ICE:
			return "Slide";
		case STATUS_TYPE.LIGHT_RING:
			return "LightRing";
		case STATUS_TYPE.EROSION:
			return "Erosion";
		case STATUS_TYPE.STONE:
			return "Stone";
		case STATUS_TYPE.CONCUSSION:
			return "Enemy_Stan";
		case STATUS_TYPE.INVINCIBLE_NORMAL:
			return "dmgOffNone";
		case STATUS_TYPE.INVINCIBLE_ALLELEMENT:
			return "dmgOffElementall";
		case STATUS_TYPE.INVINCIBLE_ALL:
			return "dmgOffAll";
		case STATUS_TYPE.ACID:
			return "Acid";
		case STATUS_TYPE.CORRUPTION:
			return "Corruption";
		case STATUS_TYPE.STIGMATA:
			return "Stigmata";
		case STATUS_TYPE.CYCLONIC_THUNDERSTORM:
			return "CyclonicThunderstorm";
		default:
			return string.Empty;
		}
	}
}
