using System;
using UnityEngine;

[Serializable]
public class BadStatus
{
	[Tooltip("麻痺")]
	public float paralyze;

	[Tooltip("毒")]
	public float poison;

	[Tooltip("燃焼")]
	public float burning;

	[Tooltip("移動速度低下")]
	public float speedDown;

	[Tooltip("猛毒")]
	public float deadlyPoison;

	[Tooltip("凍結")]
	public float freeze;

	[Tooltip("感電")]
	public float electricShock;

	[Tooltip("墨汚れ")]
	public float inkSplash;

	[Tooltip("滑り")]
	public float slide;

	[Tooltip("沈黙")]
	public float silence;

	[Tooltip("影縫矢存在時間減少割合")]
	public float shadowSealing;

	[Tooltip("影縫拘束時間減少割合")]
	public float shadowSealingBind;

	[Tooltip("攻撃速度低下")]
	public float attackSpeedDown;

	[Tooltip("回復無効")]
	public float cantHealHp;

	[Tooltip("暗闇")]
	public float blind;

	[Tooltip("光輪")]
	public float lightRing;

	public BadStatus()
	{
	}

	public BadStatus(float f)
	{
		paralyze = (poison = (burning = (speedDown = (deadlyPoison = (freeze = f)))));
		electricShock = f;
		inkSplash = f;
		slide = f;
		silence = f;
		shadowSealing = f;
		shadowSealingBind = f;
		attackSpeedDown = f;
		cantHealHp = f;
		blind = f;
		lightRing = f;
	}

	public void Copy(BadStatus status)
	{
		paralyze = status.paralyze;
		poison = status.poison;
		burning = status.burning;
		speedDown = status.speedDown;
		deadlyPoison = status.deadlyPoison;
		freeze = status.freeze;
		electricShock = status.electricShock;
		inkSplash = status.inkSplash;
		slide = status.slide;
		silence = status.silence;
		shadowSealing = status.shadowSealing;
		shadowSealingBind = status.shadowSealingBind;
		attackSpeedDown = status.attackSpeedDown;
		cantHealHp = status.cantHealHp;
		blind = status.blind;
		lightRing = status.lightRing;
	}

	public void Mul(float val)
	{
		paralyze *= val;
		poison *= val;
		burning *= val;
		speedDown *= val;
		deadlyPoison *= val;
		freeze *= val;
		electricShock *= val;
		inkSplash *= val;
		slide *= val;
		silence *= val;
		shadowSealing *= val;
		shadowSealingBind *= val;
		attackSpeedDown *= val;
		cantHealHp *= val;
		blind *= val;
		lightRing *= val;
	}

	public void Add(BadStatus status)
	{
		paralyze += status.paralyze;
		poison += status.poison;
		burning += status.burning;
		speedDown += status.speedDown;
		deadlyPoison += status.deadlyPoison;
		freeze += status.freeze;
		electricShock += status.electricShock;
		inkSplash += status.inkSplash;
		slide += status.slide;
		silence += status.silence;
		shadowSealing += status.shadowSealing;
		shadowSealingBind += status.shadowSealingBind;
		attackSpeedDown += status.attackSpeedDown;
		cantHealHp += status.cantHealHp;
		blind += status.blind;
		lightRing += status.lightRing;
	}

	public void Reset()
	{
		paralyze = 0f;
		poison = 0f;
		burning = 0f;
		speedDown = 0f;
		deadlyPoison = 0f;
		freeze = 0f;
		electricShock = 0f;
		inkSplash = 0f;
		slide = 0f;
		silence = 0f;
		shadowSealing = 0f;
		shadowSealingBind = 0f;
		attackSpeedDown = 0f;
		cantHealHp = 0f;
		blind = 0f;
		lightRing = 0f;
	}

	public bool isExist()
	{
		if (paralyze != 0f || poison != 0f || burning != 0f || speedDown != 0f || deadlyPoison != 0f || freeze != 0f || electricShock != 0f || inkSplash != 0f || slide != 0f || silence != 0f || shadowSealing != 0f || shadowSealingBind != 0f || attackSpeedDown != 0f || cantHealHp != 0f || blind != 0f || lightRing != 0f)
		{
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		return $"[BadStatus] paralyze:{paralyze} poison:{poison} burning:{burning} speedDown:{speedDown} deadlyPoison:{deadlyPoison} freeze:{freeze} electricShock:{electricShock} inkSplash:{inkSplash} slide:{slide} silence:{silence} shadowSealing:{shadowSealing} shadowSealingBind:{shadowSealingBind} attackSpeedDown:{attackSpeedDown} cantHealHp:{cantHealHp} blind:{blind} lightRing:{lightRing}";
	}
}
