using System;
using UnityEngine;

[Serializable]
public class InkSplashInfo
{
	[Tooltip("効果時間(sec)")]
	public float duration;

	[Tooltip("フリックによる減少時間(sec)")]
	public float reduceTimeByFlick;

	[Tooltip("フリックアイコンの座標")]
	public Vector3 flickIconPos = new Vector3(0f, 0f, 1f);

	public void Copy(InkSplashInfo src)
	{
		duration = src.duration;
		reduceTimeByFlick = src.reduceTimeByFlick;
		flickIconPos = src.flickIconPos;
	}
}
