// Decompiled with JetBrains decompiler
// Type: HateParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class HateParam
{
  [Tooltip("ヘイトサイクルの最大ターン数")]
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
  public HateParam.CategoryParam[] categoryParam = new HateParam.CategoryParam[7];
  public const int HATE_MAX_VALUE = 1000;
  public const float NPC_HATE_RATE = 0.5f;

  public static HateParam GetDefault()
  {
    HateParam hateParam = new HateParam();
    for (int index = 0; index < 7; ++index)
      hateParam.categoryParam[index] = new HateParam.CategoryParam();
    for (int index = 0; index < 4; ++index)
      hateParam.distanceAttackRatio[index] = 1f;
    hateParam.categoryParam[2].importance = 1f;
    hateParam.categoryParam[2].volatilizeRate = 0.9f;
    hateParam.categoryParam[2].atackedVolatizeRate = 0.9f;
    return hateParam;
  }

  [Serializable]
  public class CategoryParam
  {
    [Tooltip("このカテゴリーのヘイトを重要視する割合")]
    public float importance;
    [Tooltip("このカテゴリーのヘイトの揮発率")]
    public float volatilizeRate = 0.7f;
    [Tooltip("攻撃があたった際のこのカテゴリーのヘイトの揮発率")]
    public float atackedVolatizeRate = 0.4f;
  }
}
