using System;
using UnityEngine;

[Serializable]
public class AttackInfo
{
	[Serializable]
	public class TimeChange
	{
		[Tooltip("開始時間（秒")]
		public float startTime;

		[Tooltip("変化時間間隔（秒")]
		public float intervalTime;

		[Tooltip("変化前係数")]
		public float startRate = 1f;

		[Tooltip("変化後係数")]
		public float endRate = 1f;
	}

	[Tooltip("名前")]
	public string name;

	[Tooltip("倍率変化時攻撃情報の名前")]
	public string rateInfoName;

	[Tooltip("弾の終了時生成用の攻撃情報の名前")]
	public string nextBulletInfoName;

	[Tooltip("スキル参照")]
	public bool isSkillReference;

	[Tooltip("複属性スキルの場合どの属性を使うか")]
	public int skillElementIndex;

	[Tooltip("時間変化パラメ\u30fcタ")]
	public TimeChange timeChange = new TimeChange();

	public BulletData bulletData;

	public bool isBulletSkillReference;

	public float rateInfoRate
	{
		get;
		set;
	}

	public AttackInfo()
	{
		rateInfoRate = 0f;
	}

	public virtual AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
	{
		if (rate_info == null)
		{
			return this;
		}
		if (rate <= 0f)
		{
			return this;
		}
		AttackInfo attackInfo = CreateInfo();
		attackInfo.name = name;
		attackInfo.rateInfoName = rateInfoName;
		attackInfo.nextBulletInfoName = nextBulletInfoName;
		attackInfo.isSkillReference = isSkillReference;
		attackInfo.skillElementIndex = skillElementIndex;
		if (bulletData != null)
		{
			attackInfo.bulletData = bulletData.GetRateBulletData(rate_info.bulletData, rate);
		}
		else
		{
			attackInfo.bulletData = rate_info.bulletData;
		}
		attackInfo.isBulletSkillReference = isBulletSkillReference;
		attackInfo.rateInfoRate = rate;
		return attackInfo;
	}

	protected virtual AttackInfo CreateInfo()
	{
		return new AttackInfo();
	}

	public AttackInfo Duplicate()
	{
		AttackInfo rInfo = CreateInfo();
		Copy(ref rInfo);
		return rInfo;
	}

	public virtual void Copy(ref AttackInfo rInfo)
	{
		rInfo.name = name;
		rInfo.rateInfoName = rateInfoName;
		rInfo.nextBulletInfoName = nextBulletInfoName;
		rInfo.isSkillReference = isSkillReference;
		rInfo.skillElementIndex = skillElementIndex;
		rInfo.timeChange.startTime = timeChange.startTime;
		rInfo.timeChange.intervalTime = timeChange.intervalTime;
		rInfo.timeChange.startRate = timeChange.startRate;
		rInfo.timeChange.endRate = timeChange.endRate;
		rInfo.bulletData = bulletData;
		rInfo.isBulletSkillReference = isBulletSkillReference;
		rInfo.rateInfoRate = rateInfoRate;
	}

	public static int GetRateValue(int val_a, int val_b, float rate)
	{
		return (int)((float)val_a + ((float)val_b - (float)val_a) * rate);
	}

	public static float GetRateValue(float val_a, float val_b, float rate)
	{
		return val_a + (val_b - val_a) * rate;
	}

	public static bool GetRateValue(bool val_a, bool val_b, float rate)
	{
		if (!(rate < 1f))
		{
			return val_b;
		}
		return val_a;
	}

	public static Vector3 GetRateValue(Vector3 val_a, Vector3 val_b, float rate)
	{
		return val_a + (val_b - val_a) * rate;
	}
}
