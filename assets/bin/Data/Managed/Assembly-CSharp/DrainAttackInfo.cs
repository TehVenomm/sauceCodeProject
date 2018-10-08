using System;
using UnityEngine;

[Serializable]
public class DrainAttackInfo
{
	[Tooltip("ドレイン攻撃ID")]
	public int id;

	[Tooltip("継続ダメ\u30fcジ間隔(sec)")]
	public float damageInterval;

	[Tooltip("継続ダメ\u30fcジ値(0-100指定)")]
	public float damageRate;

	[Tooltip("ドレイン攻撃によるHP回復間隔(sec)")]
	public float recoverInterval;

	[Tooltip("ドレイン攻撃によるHP回復値(0-100指定)")]
	public float recoverRate;

	public void Copy(DrainAttackInfo src)
	{
		id = src.id;
		damageInterval = src.damageInterval;
		damageRate = src.damageRate;
		recoverInterval = src.recoverInterval;
		recoverRate = src.recoverRate;
	}
}
