using System;
using UnityEngine;

[Serializable]
public class ElectricShockInfo
{
	[Tooltip("効果時間(sec)")]
	public float duration;

	[Tooltip("インタ\u30fcバル時間(sec)")]
	public float damageInterval;

	[Tooltip("効果倍率(%)")]
	public int atkRate = 1;

	public void Copy(ElectricShockInfo src)
	{
		duration = src.duration;
		damageInterval = src.damageInterval;
		atkRate = src.atkRate;
	}
}
