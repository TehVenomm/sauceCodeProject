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

	[Tooltip("侵食")]
	public float erosion;

	[Tooltip("石化")]
	public float stone;

	[Tooltip("重縛")]
	public float soilShock;

	[Tooltip("出血")]
	public float bleeding;

	[Tooltip("酸")]
	public float acid;

	[Tooltip("モ\u30fcションストップ")]
	public float damageMotionStop;

	[Tooltip("腐敗")]
	public float corruption;

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
		erosion = f;
		stone = f;
		soilShock = f;
		bleeding = f;
		acid = f;
		damageMotionStop = f;
		corruption = f;
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
		erosion = status.erosion;
		stone = status.stone;
		soilShock = status.soilShock;
		bleeding = status.bleeding;
		acid = status.acid;
		damageMotionStop = status.damageMotionStop;
		corruption = status.corruption;
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
		erosion *= val;
		stone *= val;
		soilShock *= val;
		bleeding *= val;
		acid *= val;
		damageMotionStop *= val;
		corruption *= val;
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
		erosion += status.erosion;
		stone += status.stone;
		soilShock += status.soilShock;
		bleeding += status.bleeding;
		acid += status.acid;
		damageMotionStop += status.damageMotionStop;
		corruption += status.corruption;
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
		erosion = 0f;
		stone = 0f;
		soilShock = 0f;
		bleeding = 0f;
		acid = 0f;
		damageMotionStop = 0f;
		corruption = 0f;
	}

	public bool isExist()
	{
		if (paralyze != 0f || poison != 0f || burning != 0f || speedDown != 0f || deadlyPoison != 0f || freeze != 0f || electricShock != 0f || inkSplash != 0f || slide != 0f || silence != 0f || shadowSealing != 0f || shadowSealingBind != 0f || attackSpeedDown != 0f || cantHealHp != 0f || blind != 0f || lightRing != 0f || erosion != 0f || stone != 0f || soilShock != 0f || bleeding != 0f || acid != 0f || damageMotionStop != 0f || corruption != 0f)
		{
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		return $"[BadStatus] paralyze:{paralyze} poison:{poison} burning:{burning} speedDown:{speedDown} deadlyPoison:{deadlyPoison} freeze:{freeze} electricShock:{electricShock} inkSplash:{inkSplash} slide:{slide} silence:{silence} shadowSealing:{shadowSealing} shadowSealingBind:{shadowSealingBind} attackSpeedDown:{attackSpeedDown} cantHealHp:{cantHealHp} blind:{blind} lightRing:{lightRing} erosion:{erosion} stone:{stone} soilShock:{soilShock} bleeding:{bleeding} acid:{acid} corruption:{corruption}";
	}
}
