// Decompiled with JetBrains decompiler
// Type: EnemyParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyParam : MonoBehaviour
{
  public StageObject.StampInfo[] stampInfos;
  public AttackHitInfo[] attackHitInfos;
  public AttackContinuationInfo[] attackContinuationInfos;
  public AttackHitInfo[] convertAttackHitInfos;
  public AttackContinuationInfo[] convertAttackContinuationInfos;
  public Enemy.RegionInfo[] regionInfos;
  public Enemy.RegionInfo[] convertRegionInfos;
  [Tooltip("最短移動回転時間（回転始動と終了のスムーズに関係")]
  public float moveRotateMinimumTime = 0.3f;
  [Tooltip("移動回転最大速度（角度/s")]
  public float moveRotateMaxSpeed = 60f;
  [Tooltip("移動停止範囲")]
  public float moveStopRange = 5f;
  [Tooltip("最短回転時間（回転始動と終了のスムーズに関係")]
  public float rotateMinimumTime = 0.1f;
  [Tooltip("回転最大速度（角度/s")]
  public float rotateMaxSpeed = 120f;
  [Tooltip("回転時のモーション無効")]
  public bool rotateDisableMotion;
  [Tooltip("頭のオブジェクト名(頭からの距離測定起点)")]
  public string headObjectName = "c_head";
  [Tooltip("尻のオブジェクト名(尻からの距離測定起点)")]
  public string hipObjectName = "c_hip";
  [Tooltip("ダウン値（蓄積最大値")]
  public int downMax = 100;
  [Tooltip("ダウン値の秒間回復量")]
  public float downHeal = 10f;
  [Tooltip("体の大きさ半径（マップとの当たり、影の大きさ")]
  public float bodyRadius = 1f;
  [Tooltip("影の大きさ")]
  public float shadowSize;
  [Tooltip("UI高さ")]
  public float uiHeight = 2f;
  [Tooltip("UI表示距離")]
  public float uiShowDistance;
  [Tooltip("麻痺耐性")]
  public float paralyzeMax = 100f;
  [Tooltip("毒耐性")]
  public float poisonMax = 100f;
  [Tooltip("凍結耐性")]
  public float freezeMax = 100f;
  [Tooltip("光輪耐性")]
  public float lightRingMax = 100f;
  [Tooltip("光輪高さoffset")]
  public float lightRingHeightOffset;
  [Tooltip("しばり拘束時間減少割合")]
  public float shadowSealingBindResist = 1f;
  [Tooltip("基本ヒット素材名(EnemyHitMaterialTable)")]
  public string baseHitMaterialName;
  [Tooltip("思考情報")]
  public BrainParam brainParam = new BrainParam();
  [Tooltip("逃げる情報の名前")]
  public string escapeParamName = string.Empty;
  [Tooltip("バリア最大HP")]
  public int barrierHpMax;
  [Tooltip("幽体化時の耐性値")]
  public AtkAttribute ghostFormParam = new AtkAttribute();
  [Tooltip("幽体化バフのシェーダ調整値")]
  public GhostFormShaderParam ghostFormShaderParam = new GhostFormShaderParam();
  [Tooltip("生成時に自動起動するバフ情報")]
  public AutoBuffParam[] autoBuffParams;
  [Tooltip("ドレイン攻撃情報")]
  public DrainAttackInfo[] drainAtkInfos;
  [Tooltip("回復ダメージ倍率")]
  public float healDamageRate;
  [Tooltip("隠れた状態で登場するか")]
  public bool isHide;
  [Tooltip("隠れた状態から姿を表す距離")]
  public float turnUpDistance;
  [Tooltip("隠れた状態の時に偽装するGatherPointViewTableのID")]
  public uint gatherPointViewId;
  [Tooltip("シールド最大HP")]
  public int shieldHpMax;
  [Tooltip("シールド有効時の耐性値")]
  public AtkAttribute shieldTolerance = new AtkAttribute();
  [Tooltip("シールド破壊時の眩暈リアクションループ時間")]
  public float dizzyReactionLoopTime;
  [Tooltip("拡張アクションID")]
  public int exActionId;
  [Tooltip("拡張アクション切り替え条件")]
  public int exActionCondition;
  [Tooltip("拡張アクション切り替え条件値")]
  public int exActionConditionValue;
  [Tooltip("掴み最大HP")]
  public int grabHpMax;
  [Tooltip("掴み弱点への砲台ダメージ値")]
  public int grabCannonDamage;
  [Tooltip("ダウンループを使用する")]
  public bool useDownLoopTime;
  [Tooltip("ダウンループ開始時間")]
  public float downLoopStartTime = -1f;
  [Tooltip("ダウンループ時間")]
  public float downLoopTime = -1f;
  [Tooltip("常駐エフェクト設定データ")]
  public SystemEffectSetting residentEffectSetting;
  [Tooltip("麻痺時間")]
  public float paralyzeLoopTime = 9f;
  [Tooltip("属性耐性テーブル情報上書き")]
  public ConverteElementToleranceTable[] converteElementToleranceTable = new ConverteElementToleranceTable[0];
  [Tooltip("魔狂化になるHP割合")]
  public int madModeHpThreshold;
  [Tooltip("魔狂化になるLv")]
  public int madModeLvThreshold;
  [Tooltip("拘束/凍結のダメージモーション指定割合(0.0〜1.0)")]
  public float stopMotionByDebuffNormalizedTime = -1f;
  [Tooltip("死から復活する回数")]
  public int deadReviveCountMax;
  [Tooltip("強制MovePoint実行フラグ")]
  public bool forceActMovePoint;
  [Header("エフェクトサイズ計算別調整")]
  [Tooltip("バフ系(凍結,腐敗等)のエフェクト大きさ調整(0の場合は無視する")]
  public float effectFreezeRadiusRate;
  [Tooltip("シャドウシーリング/コンカッションのエフェクト大きさ調整(0の場合は無視する")]
  public float effectShadowSealingRadiusRate;
  [Tooltip("光輪のエフェクト大きさ調整(0の場合は無視する)")]
  public float effectLightRingRadiusRate;

  public void SetParam(Enemy targetEnemy)
  {
    EnemyParam enemyParam = this;
    targetEnemy.SetResidentEffectSetting(enemyParam.residentEffectSetting);
    targetEnemy.SetAttackInfos(Utility.CreateMergedArray<AttackInfo>((AttackInfo[]) enemyParam.attackHitInfos, (AttackInfo[]) enemyParam.attackContinuationInfos));
    targetEnemy.convertAttackInfos = Utility.CreateMergedArray<AttackInfo>((AttackInfo[]) enemyParam.convertAttackHitInfos, (AttackInfo[]) enemyParam.convertAttackContinuationInfos);
    targetEnemy.regionInfos = enemyParam.regionInfos;
    targetEnemy.convertRegionInfos = enemyParam.convertRegionInfos;
    targetEnemy.moveRotateMinimumTime = enemyParam.moveRotateMinimumTime;
    targetEnemy.moveRotateMaxSpeed = enemyParam.moveRotateMaxSpeed;
    targetEnemy.moveStopRange = enemyParam.moveStopRange;
    targetEnemy.rotateMinimumTime = enemyParam.rotateMinimumTime;
    targetEnemy.rotateMaxSpeed = enemyParam.rotateMaxSpeed;
    targetEnemy.rotateDisableMotion = enemyParam.rotateDisableMotion;
    targetEnemy.headObjectName = enemyParam.headObjectName;
    targetEnemy.hipObjectName = enemyParam.hipObjectName;
    targetEnemy._downMax = enemyParam.downMax;
    targetEnemy.downHeal = enemyParam.downHeal;
    targetEnemy.bodyRadius = enemyParam.bodyRadius;
    targetEnemy.uiHeight = enemyParam.uiHeight;
    targetEnemy.uiShowDistance = enemyParam.uiShowDistance;
    targetEnemy.effectShadowSealingRadiusRate = enemyParam.effectShadowSealingRadiusRate;
    targetEnemy.effectLightRingRadiusRate = enemyParam.effectLightRingRadiusRate;
    targetEnemy.effectFreezeRadiusRate = enemyParam.effectFreezeRadiusRate;
    targetEnemy.badStatusMax.paralyze = enemyParam.paralyzeMax;
    targetEnemy.badStatusMax.poison = enemyParam.poisonMax;
    targetEnemy.badStatusMax.freeze = enemyParam.freezeMax;
    targetEnemy.badStatusMax.lightRing = enemyParam.lightRingMax;
    targetEnemy.lightRingHeightOffset = enemyParam.lightRingHeightOffset;
    targetEnemy.badStatusBase.Copy(targetEnemy.badStatusMax);
    targetEnemy.baseHitMaterialName = enemyParam.baseHitMaterialName;
    targetEnemy.brainParam = enemyParam.brainParam;
    targetEnemy.BarrierHpMax = enemyParam.barrierHpMax;
    targetEnemy.GhostFormParam = enemyParam.ghostFormParam;
    targetEnemy.GhostFormShaderParam = enemyParam.ghostFormShaderParam;
    targetEnemy.AutoBuffParamList = enemyParam.autoBuffParams;
    targetEnemy.drainAtkInfos = enemyParam.drainAtkInfos;
    targetEnemy.healDamageRate = enemyParam.healDamageRate;
    targetEnemy.isHideSpawn = enemyParam.isHide;
    targetEnemy.isHiding = enemyParam.isHide;
    targetEnemy.turnUpDistance = enemyParam.turnUpDistance;
    targetEnemy.gatherPointViewId = enemyParam.gatherPointViewId;
    targetEnemy.ShieldTolerance = enemyParam.shieldTolerance;
    targetEnemy.ShieldHpMax = (XorInt) enemyParam.shieldHpMax;
    targetEnemy.DizzyReactionLoopTime = enemyParam.dizzyReactionLoopTime;
    targetEnemy.ExActionID = enemyParam.exActionId;
    targetEnemy.ExActionCondition = enemyParam.exActionCondition;
    targetEnemy.ExActionConditionValue = enemyParam.exActionConditionValue;
    targetEnemy.GrabHpMax = (XorInt) enemyParam.grabHpMax;
    targetEnemy.GrabCannonDamage = (XorInt) enemyParam.grabCannonDamage;
    targetEnemy.useDownLoopTime = enemyParam.useDownLoopTime;
    targetEnemy.downLoopStartTime = enemyParam.downLoopStartTime;
    targetEnemy.downLoopTime = enemyParam.downLoopTime;
    targetEnemy.paralyzeLoopTime = enemyParam.paralyzeLoopTime;
    targetEnemy.shadowSealingBindResist = enemyParam.shadowSealingBindResist;
    targetEnemy.converteElementToleranceTable = enemyParam.converteElementToleranceTable;
    targetEnemy.madModeHpThreshold = enemyParam.madModeHpThreshold;
    targetEnemy.madModeLvThreshold = enemyParam.madModeLvThreshold;
    targetEnemy.deadReviveCountMax = enemyParam.deadReviveCountMax;
    targetEnemy.forceActMovePoint = enemyParam.forceActMovePoint;
    targetEnemy.stopMotionByDebuffNormalizedTime = enemyParam.stopMotionByDebuffNormalizedTime;
    if (enemyParam.stampInfos == null || enemyParam.stampInfos.Length == 0)
      return;
    CharacterStampCtrl characterStampCtrl = ((Component) this).gameObject.GetComponent<CharacterStampCtrl>();
    if (Object.op_Equality((Object) characterStampCtrl, (Object) null))
      characterStampCtrl = ((Component) this).gameObject.AddComponent<CharacterStampCtrl>();
    characterStampCtrl.Init(enemyParam.stampInfos, (Character) targetEnemy);
  }
}
