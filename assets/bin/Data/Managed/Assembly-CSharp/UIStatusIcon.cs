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
			if (!object.ReferenceEquals(tween, null))
			{
				tween.set_enabled(enable);
			}
		}
	}

	public static readonly STATUS_TYPE[] NON_BUFF_STATUS = new STATUS_TYPE[3]
	{
		STATUS_TYPE.PARALYZE,
		STATUS_TYPE.FREEZE,
		STATUS_TYPE.SHADOWSEALING
	};

	private readonly Color defaultTintColor = Color.get_white();

	private readonly Color fieldBuffTintColor = Color32.op_Implicit(new Color32((byte)159, (byte)104, (byte)104, byte.MaxValue));

	[SerializeField]
	protected IconInfo[] statusIcons;

	[SerializeField]
	protected string[] spriteNames = new string[69];

	public Character target;

	public void UpDateStatusIcon()
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null))
		{
			int num = statusIcons.Length;
			int num2 = 0;
			for (int i = 0; i < 69; i++)
			{
				if (CheckStatus((STATUS_TYPE)i, true) && _SetStatusIcon((STATUS_TYPE)i, num2, true) && ++num2 >= num)
				{
					break;
				}
			}
			for (int j = 0; j < 69; j++)
			{
				if (CheckStatus((STATUS_TYPE)j, false) && _SetStatusIcon((STATUS_TYPE)j, num2, false) && ++num2 >= num)
				{
					break;
				}
			}
			for (int k = num2; k < num; k++)
			{
				statusIcons[k].isFieldBuff = false;
				statusIcons[k].SetTweenEnable(false);
				statusIcons[k].icon.color = defaultTintColor;
				statusIcons[k].icon.get_gameObject().SetActive(false);
			}
		}
	}

	private bool _SetStatusIcon(STATUS_TYPE type, int index, bool isFieldBuff)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		string iconSpriteNameByStatusType = GetIconSpriteNameByStatusType(type);
		if (statusIcons[index].icon.atlas.GetSprite(iconSpriteNameByStatusType) == null)
		{
			return false;
		}
		statusIcons[index].icon.spriteName = iconSpriteNameByStatusType;
		statusIcons[index].icon.color = defaultTintColor;
		statusIcons[index].SetTweenEnable(isFieldBuff);
		statusIcons[index].icon.get_gameObject().SetActive(true);
		return true;
	}

	public int RotatedUpdateStatusIcon(int checkFirstStatus, BuffParam buffParam, List<int> nonBuff)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		int result = checkFirstStatus;
		int num = statusIcons.Length;
		int num2 = 0;
		for (int i = 0; i < 69; i++)
		{
			if (num2 >= num)
			{
				break;
			}
			int num3 = i + checkFirstStatus;
			if (num3 >= 69)
			{
				num3 %= 69;
			}
			if (CheckStatus((STATUS_TYPE)num3, buffParam, nonBuff))
			{
				IconInfo iconInfo = statusIcons[num2];
				string iconSpriteNameByStatusType = GetIconSpriteNameByStatusType((STATUS_TYPE)num3);
				iconInfo.icon.spriteName = iconSpriteNameByStatusType;
				iconInfo.icon.get_gameObject().SetActive(true);
				num2++;
				result = num3;
			}
		}
		for (int j = num2; j < num; j++)
		{
			statusIcons[j].icon.get_gameObject().SetActive(false);
		}
		return result;
	}

	public bool HasActiveMultipleBuffIcon(BuffParam buffParam, List<int> nonBuff)
	{
		HashSet<string> hashSet = new HashSet<string>();
		int num = 0;
		for (int i = 0; i < 69; i++)
		{
			if (CheckStatus((STATUS_TYPE)i, buffParam, nonBuff))
			{
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
			return character.IsParalyze() && !isFieldBuff;
		case STATUS_TYPE.FREEZE:
			return character.IsFreeze() && !isFieldBuff;
		case STATUS_TYPE.SHADOWSEALING:
			return character.IsDebuffShadowSealing() && !isFieldBuff;
		default:
			return CheckStatus(type, character.buffParam, isFieldBuff);
		}
	}

	public static bool CheckStatus(STATUS_TYPE type, BuffParam buffParam, List<int> nonBuffStatus)
	{
		return CheckStatus(type, buffParam, false) || (nonBuffStatus != null && CheckStatus(type, nonBuffStatus));
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
		case STATUS_TYPE.BURNING:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.BURNING, isFieldBuff);
		case STATUS_TYPE.DEADLY_POISON:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEADLY_POISON, isFieldBuff);
		case STATUS_TYPE.INVINCIBLECOUNT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT, isFieldBuff);
		case STATUS_TYPE.ATTACK_SPEED_UP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_UP, isFieldBuff);
		case STATUS_TYPE.ATTACK_SPEED_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, isFieldBuff);
		case STATUS_TYPE.MOVE_SPEED_UP:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_UP, isFieldBuff);
		case STATUS_TYPE.MOVE_SPEED_DOWN:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, isFieldBuff);
		case STATUS_TYPE.ATTACK_NORMAL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_NORMAL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL, isFieldBuff);
		case STATUS_TYPE.ATTACK_FIRE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_FIRE, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_FIRE, isFieldBuff);
		case STATUS_TYPE.ATTACK_WATER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_WATER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_WATER, isFieldBuff);
		case STATUS_TYPE.ATTACK_THUNDER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_THUNDER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER, isFieldBuff);
		case STATUS_TYPE.ATTACK_SOIL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_SOIL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_SOIL, isFieldBuff);
		case STATUS_TYPE.ATTACK_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_LIGHT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT, isFieldBuff);
		case STATUS_TYPE.ATTACK_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_DARK, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_DARK, isFieldBuff);
		case STATUS_TYPE.ATTACK_ALLELEMENT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT, isFieldBuff);
		case STATUS_TYPE.DEFENCE_NORMAL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_NORMAL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL, isFieldBuff);
		case STATUS_TYPE.DEFENCE_FIRE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_FIRE, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_FIRE, isFieldBuff);
		case STATUS_TYPE.DEFENCE_WATER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_WATER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_WATER, isFieldBuff);
		case STATUS_TYPE.DEFENCE_THUNDER:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_THUNDER, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER, isFieldBuff);
		case STATUS_TYPE.DEFENCE_SOIL:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_SOIL, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_SOIL, isFieldBuff);
		case STATUS_TYPE.DEFENCE_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_LIGHT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT, isFieldBuff);
		case STATUS_TYPE.DEFENCE_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_DARK, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_DARK, isFieldBuff);
		case STATUS_TYPE.DEFENCE_ALLELEMENT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT, isFieldBuff);
		case STATUS_TYPE.REGENERATE:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.REGENERATE, isFieldBuff);
		case STATUS_TYPE.ELECTRIC_SHOCK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.ELECTRIC_SHOCK, isFieldBuff);
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
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SUPER_ARMOR, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR, isFieldBuff);
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
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_UP, isFieldBuff) || buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_UP, isFieldBuff);
		case STATUS_TYPE.SKILL_CHARGE_WHEN_DAMAGED:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_LIGHT:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_LIGHT, isFieldBuff);
		case STATUS_TYPE.INVINCIBLE_DARK:
			return buffParam.IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE.INVINCIBLE_DARK, isFieldBuff);
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
		case STATUS_TYPE.BURNING:
			return "Burn";
		case STATUS_TYPE.DEADLY_POISON:
			return "DeadlyPoison";
		case STATUS_TYPE.INVINCIBLECOUNT:
			return "Barrior";
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
		default:
			return string.Empty;
		}
	}
}
