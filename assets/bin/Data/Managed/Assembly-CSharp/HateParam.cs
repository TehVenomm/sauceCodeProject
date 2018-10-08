using System;
using UnityEngine;

[Serializable]
public class HateParam
{
	[Serializable]
	public class CategoryParam
	{
		[Tooltip("このカテゴリ\u30fcのヘイトを重要視する割合")]
		public float importance;

		[Tooltip("このカテゴリ\u30fcのヘイトの揮発率")]
		public float volatilizeRate = 0.7f;

		[Tooltip("攻撃があたった際のこのカテゴリ\u30fcのヘイトの揮発率")]
		public float atackedVolatizeRate = 0.4f;
	}

	public const int HATE_MAX_VALUE = 1000;

	public const float NPC_HATE_RATE = 0.5f;

	[Tooltip("ヘイトサイクルの最大タ\u30fcン数")]
	public int cycleTurnMax = 8;

	[Tooltip("対象を見失わない蓄積量の最大HP係数")]
	public float missTargetUnderStockPerMaxHp = 0.02f;

	[Tooltip("対象に連続で行動したら見失う数")]
	public int missTargetFromContinuousLockNum = 2;

	[Tooltip("スキル使用時に増加するヘイト")]
	public int skillHate = 100;

	[Tooltip("弱点攻撃時に増加させるヘイト")]
	public int attackedWeakPointHate = 100;

	public int[] distanceHateParams = new int[4];

	public float[] distanceAttackRatio = new float[4];

	public CategoryParam[] categoryParam = new CategoryParam[7];

	public static HateParam GetDefault()
	{
		HateParam hateParam = new HateParam();
		for (int i = 0; i < 7; i++)
		{
			hateParam.categoryParam[i] = new CategoryParam();
		}
		for (int j = 0; j < 4; j++)
		{
			hateParam.distanceAttackRatio[j] = 1f;
		}
		hateParam.categoryParam[2].importance = 1f;
		hateParam.categoryParam[2].volatilizeRate = 0.9f;
		hateParam.categoryParam[2].atackedVolatizeRate = 0.9f;
		return hateParam;
	}
}
