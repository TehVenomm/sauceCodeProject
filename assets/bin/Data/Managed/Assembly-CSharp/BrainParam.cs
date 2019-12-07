using System;
using UnityEngine;

[Serializable]
public class BrainParam
{
	[Serializable]
	public class SensorParam
	{
		[Tooltip("視野角")]
		public float viewAngle = 110f;

		[Tooltip("視野距離")]
		public float viewDistance = 40f;

		[Tooltip("聞こえる範囲")]
		public float hearingRange = 20f;

		[Tooltip("内円半径")]
		public float internalRadius = 1f;

		[Tooltip("短距離(未満で超短い距離)")]
		public float shortDistance = 3f;

		[Tooltip("中距離(未満で短い距離)")]
		public float middleDistance = 6f;

		[Tooltip("長距離(未満で中くらいの距離、以上で長い距離)")]
		public float longDistance = 10f;

		[Tooltip("近距離チェック距離")]
		public float nearCheckDistance = 5f;
	}

	[Serializable]
	public class MoveParam
	{
		[Tooltip("回転をモ\u30fcションで行うか")]
		public bool motionRotate = true;

		[Tooltip("移動最大距離")]
		public float moveMaxLength = 10f;

		[Tooltip("対象が逃げたときの追う距離")]
		public float moveOverDistance = 2f;

		[Tooltip("行動前の移動をホ\u30fcミングタイプにする")]
		public bool enableActionMoveHoming;

		[Tooltip("ホ\u30fcミング移動最大距離")]
		public float moveHomingMaxLength = 20f;
	}

	[Serializable]
	public class ThinkParam
	{
		[Tooltip("対象の情報更新間隔")]
		public float opponentMemorySpan = 1f;

		[Tooltip("対象の変更更新間隔")]
		public float targetUpdateSpan = 10f;
	}

	public class ScountingParam
	{
		public float scountigRangeSqr;

		public float scoutingSightCos;

		public float scoutingAudibilitySqr;

		public const int ACTIVATE_HATE_VALUE = 100;

		public bool IsScouted(Transform self, Transform target)
		{
			Vector3 vector = target.position - self.position;
			if (vector.sqrMagnitude >= scountigRangeSqr)
			{
				return false;
			}
			if (Vector3.Dot(self.forward, vector.normalized) < scoutingSightCos)
			{
				return false;
			}
			return true;
		}
	}

	public SensorParam sensorParam = new SensorParam();

	public MoveParam moveParam = new MoveParam();

	public ThinkParam thinkParam = new ThinkParam();

	public ScountingParam scoutParam;
}
