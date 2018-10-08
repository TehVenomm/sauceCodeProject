using System;
using UnityEngine;

[Serializable]
public class RestraintInfo
{
	[Tooltip("有効かどうか")]
	public bool enable;

	[Tooltip("拘束時間(sec)")]
	public float duration;

	[Tooltip("継続ダメ\u30fcジ間隔(sec)")]
	public float damageInterval;

	[Tooltip("継続ダメ\u30fcジ値(0-100指定)")]
	public int damageRate;

	[Tooltip("フリックによる減少時間(sec)")]
	public float reduceTimeByFlick;

	[Tooltip("衝突半径")]
	public float radius;

	[Tooltip("エフェクト名")]
	public string effectName = string.Empty;

	[Tooltip("モ\u30fcションを停止させるか？")]
	public bool isStopMotion;

	public void Copy(RestraintInfo src)
	{
		enable = src.enable;
		duration = src.duration;
		damageInterval = src.damageInterval;
		damageRate = src.damageRate;
		reduceTimeByFlick = src.reduceTimeByFlick;
		radius = src.radius;
		effectName = src.effectName;
		isStopMotion = src.isStopMotion;
	}
}
