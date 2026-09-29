// Decompiled with JetBrains decompiler
// Type: InGameSettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameSettingsManager : MonoBehaviourSingleton<InGameSettingsManager>
{
  public const float BASE_FIXED_DELTA_TIME = 0.02f;
  public InGameSettingsManager.SelfController selfController;
  public InGameSettingsManager.EnemyController enemyController;
  public InGameSettingsManager.NpcController npcController;
  public InGameSettingsManager.StageObjectParam stageObject;
  public InGameSettingsManager.Character character;
  public InGameSettingsManager.Player player;
  public InGameSettingsManager.Evolve evolve;
  public InGameSettingsManager.Enemy enemy;
  public InGameSettingsManager.TargetMarkerSettings targetMarkerSettings;
  public InGameSettingsManager.TargetMarker targetMarker;
  public InGameSettingsManager.TargetMarker targetMarkerLesserEnemies;
  public InGameSettingsManager.TargetMarker targetMarkerArrowAimLesser;
  public InGameSettingsManager.TargetMarker targetMarkerArrowRainAimLesser;
  public InGameSettingsManager.TargetMarker targetMarkerEnemyAssimilated;
  public InGameSettingsManager.DropItem dropItem;
  public InGameSettingsManager.FieldDropItem fieldDrop;
  public InGameSettingsManager.DropMaker dropMaker;
  public InGameSettingsManager.Room room;
  public InGameSettingsManager.InGameProgress inGameProgress;
  public InGameSettingsManager.Portal portal;
  public InGameSettingsManager.HappenQuestDirection happenQuestDirection;
  public InGameSettingsManager.UseResources useResourcesCommon;
  public InGameSettingsManager.UseResources useResourcesField;
  public InGameSettingsManager.UseResources useResourcesQuest;
  public InGameSettingsManager.UIParam uiparam;
  public InGameSettingsManager.BuffParamInfo buff;
  public InGameSettingsManager.DebuffParam debuff;
  public InGameSettingsManager.MadModeParam madModeParam;
  public InGameSettingsManager.PassiveParam passive;
  public InGameSettingsManager.AbilityParam abilityParam;
  public InGameSettingsManager.TutorialParam tutorialParam;
  public InGameSettingsManager.ArenaParam arenaParam;
  public InGameSettingsManager.DefenseBattleParam defenseBattleParam;
  public InGameSettingsManager.CannonParam cannonParam;
  public InGameSettingsManager.WaveMatchParam waveMatchParam;
  public InGameSettingsManager.WaveMatchParam waveMatchEventParam;
  public InGameSettingsManager.FishingParam fishingParam;

  public InGameSettingsManager.WaveMatchParam GetWaveMatchParam()
  {
    return !QuestManager.IsValidInGameWaveMatch(true) ? this.waveMatchParam : this.waveMatchEventParam;
  }

  [Serializable]
  public class SelfController
  {
    public float moveForwardSpeed = 5.5f;
    public float moveSideSpeed = 5.5f;
    public bool enableRootMotion;
    public bool alwaysTopSpeed = true;
    public float targetingDistance = 20f;
    public float targetingDistanceFar = 30f;
    [Tooltip("移動入力受付開始時間")]
    public float inputMoveStartTime = 0.15f;
    [Tooltip("先行入力有効時間（0:攻撃 1:回避 2:スキル 3:武器固有アクション 4:武器切り替え 5:採取 6:大砲搭乗 7:大砲発射 8:ソナー 9:ワープ")]
    public float[] inputCommandValidTime;
    [Tooltip("同一コマンド入力間隔時間（0:攻撃 1:回避 2:スキル 3:武器固有アクション 4:武器切り替え 5:採取 6:大砲搭乗 7:大砲発射 8:ソナー 9:ワープ")]
    public float[] inputCommandIntervalTime;
    [Tooltip("タッチ入力の攻撃判定開始時間、感度高")]
    public float inputLongTouchTimeHigh = 0.15f;
    [Tooltip("タッチ入力の攻撃判定開始時間、感度低")]
    public float inputLongTouchTimeLow = 0.35f;
    [Tooltip("ガード移動のドラッグ距離閾値")]
    public float guardMoveThreshold = 0.6f;
    [Tooltip("ガード移動の同期速度")]
    public float guardMoveSyncSpeed = 2f;
    [Tooltip("狙い開始のドラッグ量")]
    public float aimDragLength = 0.35f;
    [Tooltip("狙い開始の猶予時間")]
    public float aimDelayTime = 0.15f;
    [Tooltip("SpAttackType指定のあるアビリティを無視")]
    public bool ignoreSpAttackTypeAbility = true;
    [Tooltip("InGameでのDynamicBoneを削除するか")]
    public DYNAMICBONE_TYPE dynamicBoneType = DYNAMICBONE_TYPE.DISABLE_LOW;
    public InGameSettingsManager.SelfController.ArrowAimBossSettings arrowAimBossSettings;
    public InGameSettingsManager.SelfController.ArrowAimLesserSettings arrowAimLesserSettings;
    public InGameSettingsManager.SelfController.ArrowAimLesserSettings spearAimLesserSettings;
    public InGameSettingsManager.SelfController.ArrowAimLesserSettings arrowRainShotAimLesserSettings;
    [Tooltip("氷の上で最高速度に到達するまでの時間")]
    public float needTimeToMaxTimeOnIce = 2f;
    [Tooltip("氷の上ですべる時間")]
    public float slideTime = 1.2f;
    [Tooltip("氷の上で回避した際のすべるスピード")]
    public float avoidSpeedOnIce = 5f;
    [Tooltip("大砲の横方向操作距離に掛ける係数")]
    public float cannonDragRateX = 2f;
    [Tooltip("画面内におさめるオフセット")]
    public float screenSaftyOffset = 1.7f;

    [Serializable]
    public class ArrowAimBossSettings
    {
      [Tooltip("狙いモード継続有効")]
      public bool enableAimKeep = true;
      [Tooltip("上方向への限界角度")]
      public float angleLimitUp = 30f;
      [Tooltip("下方向への限界角度")]
      public float angleLimitDown = 10f;
      [Tooltip("横方向への限界角度")]
      public float angleLimitSide = 25f;
      [Tooltip("ドラッグ変化率X軸")]
      public float dragRateX = 15f;
      [Tooltip("ドラッグ変化率Y軸")]
      public float dragRateY = 15f;
      [Tooltip("余剰回転速度")]
      public float overSpeedRate = 1f;
      [Tooltip("余剰回転速度制限")]
      public float overSpeedLimit = 2f;
      [Tooltip("余剰回転スクリーン割合")]
      public float overScreenRate = 0.15f;
      [Tooltip("余剰回転スクリーン猶予")]
      public float overScreenMargin = 0.05f;
      [Tooltip("余剰回転速度、最小角度範囲")]
      public float overSpeedAngleMinRange = 30f;
      [Tooltip("余剰回転速度、変化角度範囲")]
      public float overSpeedAngleChangeRange = 60f;
      [Tooltip("余剰回転速度、最大倍率")]
      public float overSpeedMaxRate = 3f;
    }

    [Serializable]
    public class ArrowAimLesserSettings
    {
      [Tooltip("ドラッグ最小距離")]
      public float dragMinLength = 0.2f;
      [Tooltip("ドラッグ最大距離")]
      public float dragMaxLength = 1.7f;
      [Tooltip("カーソル移動速度")]
      public float cursorSpeed = 10f;
      [Tooltip("カーソル初速度倍率")]
      public float cursorStartSpeedRate = 2f;
      [Tooltip("カーソル初速度継続時間")]
      public float cursorStartSpeedTime = 0.5f;
      [Tooltip("カーソルターゲット中速度倍率")]
      public float cursorTargetingSpeedRate = 0.5f;
      [Tooltip("カーソル移動限界距離")]
      public float cursorMaxDistance = 15f;
      [Tooltip("カーソルサイズスケール")]
      public float cursorScale = 2f;
    }
  }

  [Serializable]
  public class EnemyController
  {
    [Tooltip("AI開始時待機時間")]
    public float startWaitTime = 3f;
    [Tooltip("リアクション後待機時間")]
    public float afterReactionWaitTime = 0.5f;
  }

  [Serializable]
  public class NpcController
  {
    [Tooltip("AI開始時待機時間")]
    public float startWaitTime = 1f;
    [Tooltip("リアクション後待機時間")]
    public float afterReactionWaitTime = 0.5f;
  }

  [Serializable]
  public class TargetMarkerSettings
  {
    [Tooltip("ターゲットマーカー用エフェクト")]
    public string[] effectNames;
  }

  [Serializable]
  public class TargetMarker
  {
    public float showAngle = 180f;
    public float targetAngle = 90f;
    [Tooltip("ターゲット可能な距離")]
    public float targetDistance = 3f;
    [Tooltip("ターゲットマークを表示できる距離")]
    public float showTargetDistance = 10f;
    [Tooltip("弓でターゲット可能な距離")]
    public float targetDistanceArrow = 30f;
    [Tooltip("弓を装備している際のターゲットマークを表示できる距離")]
    public float showTargetDistanceArrow = 30f;
    [Tooltip("新ターゲットが選択出来るまでの時間")]
    public float selectAbleTime = 0.3f;
    [Tooltip("今のターゲットが変更出来るまでの時間")]
    public float changeAbleTime = 0.5f;
    [Tooltip("ロック解除までのデフォルト時間")]
    public float defaultUnlockTime = 0.1f;
    [Tooltip("weakターゲットが変更出来るまでの時間")]
    public float changeWeakTime = 2f;
    [Tooltip("カメラ外のターゲット無効化")]
    public bool enableCameraCulling;
    [Tooltip("カメラ外のターゲット無効化割合")]
    public float cameraCullingMargin = 0.1f;
    [Tooltip("weakマーカー狙い補正距離")]
    public float weakMarginDistance = 1f;
    [Tooltip("通常マーカーの表示")]
    public bool enableNormalMarker;
  }

  [Serializable]
  public class StageObjectParam
  {
    [Tooltip("パケット実行許容時間（秒）")]
    public float packetHandleMarginTime = 10f;
    [Tooltip("連続マップヒット判定までの時間（秒）")]
    public float wallStayCheckTime = 0.5f;
    [Tooltip("パケット待ちの更新周期時間（秒）")]
    public float waitingPacketIntervalTime = 3f;
    [Tooltip("パケット待ちの更新猶予時間（秒）")]
    public float waitingPacketMarginTime = 3f;
    [Tooltip("座標計算用、マップ最大直径距離")]
    public float mapMaxDiameter = 300f;
    [Tooltip("同期待機の最大時間（秒）")]
    public float maxWaitSyncTime = 2f;
    [Tooltip("ステージのコリジョンを高くする")]
    public bool isRaiseStageCollider;
    [Tooltip("高くする場合のサイズ")]
    public float raiseStageColliderSizeY = 4f;
    [Tooltip("高くする場合のオフセット")]
    public float raiseStageColliderOffsetY;
  }

  [Serializable]
  public class Character
  {
    [Tooltip("移動同期時回転時間")]
    public float moveSyncRotateTime = 0.2f;
    [Tooltip("移動抑制距離")]
    public float moveSuppressLength = 1.5f;
    [Tooltip("移動抑制レート")]
    public float moveSuppressRate = 0.1f;
    [Tooltip("移動の定期同期時間（秒")]
    public float moveSendInterval = 1f;
    [Tooltip("ターゲットへの最大回転回数（90°ずつ回転")]
    public int rotateTargetMaxNum = 3;
    [Tooltip("モーション変更補間時間（秒")]
    public float motionTransitionTime = 0.1f;
    [Tooltip("バフの定期同期時間（秒")]
    public float buffSyncUpdateInterval = 15f;
    [Tooltip("PERIODIC_SYNC_ACTION_POSITIONの座標決定時間（秒")]
    public float periodicSyncActionPositionCheckTime = 0.166666657f;
    [Tooltip("PERIODIC_SYNC_ACTION_POSITIONの座標決定からの適用時間（秒")]
    public float periodicSyncActionPositionApplyTime = 0.166666657f;
  }

  [Serializable]
  public class Player
  {
    [Tooltip("武器情報")]
    public InGameSettingsManager.Player.WeaponInfo[] weaponInfo;
    [Tooltip("最大HP")]
    public int hpMax = 30;
    [Tooltip("秒間HP回復量")]
    public int hpHealSpeed = 1;
    [Tooltip("ダメージに対する自動回復分割合")]
    public float damegeHealRate = 0.3f;
    [Tooltip("味方からのヒットリアクションを行うか")]
    public bool playerHitReactionValid = true;
    [Tooltip("救助可能秒数")]
    public float[] rescueTimes;
    [Tooltip("討伐隊救助可能回数")]
    public int exploreRescureCount;
    [Tooltip("討伐隊救助可能秒数")]
    public float exploreRescureTime;
    [Tooltip("バリア内での救助速度倍率")]
    public float rescueSpeedRateInBarrier = 2.5f;
    [Tooltip("魔石復活可能秒数")]
    public float continueTime = 15f;
    [Tooltip("救助にかかる秒数")]
    public float revivalTime = 3f;
    [Tooltip("救助エリア範囲半径")]
    public float revivalRange = 2f;
    [Tooltip("魔石復活時の回復割合(0〜1)")]
    public float continueHealRate = 1f;
    [Tooltip("復活後の無敵時間（s")]
    public float deadStandupHitOffTime = 2f;
    [Tooltip("遠距離攻撃ライン表示")]
    public bool enableBulletLine;
    [Tooltip("UI高さ")]
    public float uiHeight;
    [Tooltip("テストスキルID")]
    public List<int> testSkillIDs;
    [Tooltip("テストスキル有効フラグ")]
    public bool enableTestSkill;
    [Tooltip("スキルゲージ毎秒回復量")]
    public float healSkillGaugePerSecond;
    [Tooltip("攻撃ヒット時のスキルゲージ回復量")]
    public float healSkillGaugeHit;
    [Tooltip("スキル効果範囲用のエフェクト名")]
    public string skillRangeEffectName;
    [Tooltip("振動のループ時間（秒")]
    public float shakeLoopTime = 2f;
    [Tooltip("タップによるスタン軽減時間（秒")]
    public float stunnedReduceTimeValue = 0.1f;
    [Tooltip("タップによるスタン軽減最大割合(0〜1)")]
    public float stunnedReduceTimeMaxRate = 1f;
    [Tooltip("スタン用エフェクト")]
    public string[] stunnedEffectList;
    [Tooltip("移動回転最大速度（角度/s")]
    public float moveRotateMaxSpeed = 720f;
    [Tooltip("バトル開始モーション後の無敵時間（s")]
    public float battleStartHitOffTime = 2f;
    [Tooltip("プレイヤー出現のモンスターとの距離")]
    public float appearPosDistance = 10f;
    [Tooltip("プレイヤー出現位置のランダム試行回数")]
    public int appearPosTryCount = 8;
    [Tooltip("連戦時の最低HP割合（0〜1")]
    public float seriesMinimumHpRate = 0.3f;
    [Tooltip("キャラ出現時のエフェクト名")]
    public string battleStartEffectName;
    [Tooltip("武器切り替えの最低時間（秒")]
    public float changeWeaponMinTime = 1f;
    [Tooltip("武器切り替え時のエフェクト名")]
    public string changeWeaponEffectName;
    [Tooltip("足踏みエフェクト距離制限")]
    public float stampDistance = 15f;
    [Tooltip("敵や壁との摩擦")]
    public float friction = 0.2f;
    [Tooltip("GetAnimatorSpeedの最大TimeRate")]
    public float animatorSpeedMaxTimeRate = 0.9f;
    [Tooltip("MaxDamageDownRate")]
    [Range(0.0f, 1f)]
    public float maxDamageDownRate = 0.6f;
    [Tooltip("武器固有アクション情報")]
    public InGameSettingsManager.Player.SpecialActionInfo specialActionInfo;
    [Tooltip("ベスト距離のヒットエフェクト名")]
    public string bestDistanceEffect = "";
    [Tooltip("両手剣固有アクション情報")]
    public InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo;
    [Tooltip("双剣固有アクション情報")]
    public InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo;
    [Tooltip("弓固有アクション情報")]
    public InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo;
    [Tooltip("片手剣固有アクション情報")]
    public InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo;
    [Tooltip("槍固有アクション情報")]
    public InGameSettingsManager.Player.SpearActionInfo spearActionInfo;
    [Tooltip("バリアが壊れたときにとるリアクション")]
    public InGameSettingsManager.Player.BarrierBrokenReaction barrierBrokenReaction;
    public InGameSettingsManager.Player.TeleportationInfo teleportationInfo;
    [NonSerialized]
    public List<AttackInfos> weaponAttackInfoList = new List<AttackInfos>();
    [NonSerialized]
    public AttackInfo[] attackInfosAll;

    [Serializable]
    public class WeaponInfo
    {
      public string name;
      [Tooltip("weakマーカーの攻撃力に対する倍率")]
      public float attackWeakRate = 1.5f;
      [Tooltip("ダウンマーカーの攻撃力に対する倍率")]
      public float attackDownRate = 1.5f;
      [Tooltip("武器ごとの防御力倍率(1で通常")]
      public float defenceRate = 1f;
      [Tooltip("weak時のダメージに対するダウン値の倍率")]
      public float downPowerWeak = 10f;
      [Tooltip("赤weak時のダメージに対するダウン値の倍率")]
      public float downPowerSimpleWeak = 10f;
      [Tooltip("(1.2.3で廃止)weakへのHIT時に怯ませるか？")]
      public bool weakFalter;
      [Tooltip("攻撃ヒット時のスキルゲージ回復量、武器種倍率")]
      public float healSkillGaugeHitRate = 1f;
      [Tooltip("スペシャルアタックヒット時のスキルゲージ回復量、武器種倍率")]
      public float healSkillGaugeHitRateSpecialAttack = 1f;
      [Tooltip("weak:属性攻撃に対する倍率")]
      public float weakRateElementAttack = 1.5f;
      [Tooltip("weak:マギ攻撃に対する倍率")]
      public float weakRateSkillAttack = 1.5f;
      [Tooltip("weak:マギ+属性攻撃に対する倍率")]
      public float weakRateElementSkillAttack = 1.5f;
      [Tooltip("weak:ヒール攻撃に対する倍率")]
      public float weakRateHealAttack = 1.5f;
      [Tooltip("weak:属性SPに対する倍率")]
      public float weakRateElementSpAttack = 1.5f;
      [Tooltip("ターゲット箇所として無視する高さ")]
      public float ignoreTargetHeight = 5f;
      [Tooltip("ターゲット箇所として無視する高さ（タイプ別）")]
      public float[] ignoreTargetHeightsByType;
    }

    [Serializable]
    public class SpecialActionInfo
    {
      [Tooltip("有効フラグ")]
      public bool enable = true;
      [Tooltip("発動エフェクト")]
      public string startEffectName;
      [Tooltip("特殊攻撃用の攻撃ID")]
      public int spAttackID;
      [Tooltip("ガードによるゲージ回復量")]
      public float guardHealGauge;
      [Tooltip("両手剣固有アクションによる弱点への攻撃力に対する倍率")]
      public float twoHandSwordWeakRate = 3f;
      [Tooltip("弓の出血秒間ダメージ割合")]
      public float arrowBleedDamageRate = 1f;
      [Tooltip("弓の出血ダメージ時間間隔")]
      public float arrowBleedTimeInterval = 5f;
      [Tooltip("弓の出血ダメージ初回スキップ残り時間レート(0〜1)")]
      public float arrowBleedSkipTimeRate = 0.2f;
      [Tooltip("弓の出血ダメージ回数")]
      public int arrowBleedCount = 1;
      [Tooltip("弓の出血継続エフェクト名")]
      public string arrowBleedEffectName;
      [Tooltip("弓の出血継続他人用エフェクト名")]
      public string arrowBleedOtherEffectName;
      [Tooltip("弓の出血ダメージエフェクト名")]
      public string arrowBleedDamageEffectName;
      [Tooltip("弓の狙いエフェクト名")]
      public string arrowChargeAimEffectName;
      [Tooltip("弓の出血ダメージ表示")]
      public bool arrowBleedShowDamage;
      [Tooltip("弓のザコ狙いカーソルエフェクト名")]
      public string arrowAimLesserCursorEffectName;
      [Tooltip("弓のザコ狙い後アンロック時間")]
      public float arrowAimLesserUnlockTime = 2f;
      public InGameSettingsManager.Player.SpecialActionInfo.ArrowBleedOther arrowBleedOther;
      [Tooltip("無属性バーストショット")]
      public string arrowBurstEffectName;
      [Tooltip("火属性バーストショット")]
      public string arrowFireBurstEffectName;
      [Tooltip("水属性バーストショット")]
      public string arrowWaterBurstEffectName;
      [Tooltip("雷属性バーストショット")]
      public string arrowThunderBurstEffectName;
      [Tooltip("地属性バーストショット")]
      public string arrowSoilBurstEffectName;
      [Tooltip("光属性バーストショット")]
      public string arrowLightrBurstEffectName;
      [Tooltip("闇属性バーストショット")]
      public string arrowDarkBurstEffectName;
      [Tooltip("バーストショットのダメージ倍率")]
      public float arrowBurstDamageRate = 4f;

      public string GetBurstEffectName(ELEMENT_TYPE type)
      {
        switch (type)
        {
          case ELEMENT_TYPE.FIRE:
            return this.arrowFireBurstEffectName;
          case ELEMENT_TYPE.WATER:
            return this.arrowWaterBurstEffectName;
          case ELEMENT_TYPE.THUNDER:
            return this.arrowThunderBurstEffectName;
          case ELEMENT_TYPE.SOIL:
            return this.arrowSoilBurstEffectName;
          case ELEMENT_TYPE.LIGHT:
            return this.arrowLightrBurstEffectName;
          case ELEMENT_TYPE.DARK:
            return this.arrowDarkBurstEffectName;
          case ELEMENT_TYPE.MAX:
            return this.arrowBurstEffectName;
          default:
            return this.arrowBurstEffectName;
        }
      }

      [Serializable]
      public class ArrowBleedOther
      {
        [Tooltip("弓継続エフェクトの他人表示有効")]
        public bool enable = true;
        [Tooltip("弓継続エフェクトの他人表示、軸回転ランダム範囲角度")]
        public float axisRandomAngle = 90f;
        [Tooltip("弓継続エフェクトの他人表示、開き回転固定角度")]
        public float openFixAngle = 15f;
        [Tooltip("弓継続エフェクトの他人表示、開き回転ランダム範囲角度")]
        public float openRandomAngle = 20f;
      }
    }

    [Serializable]
    public class TwoHandSwordActionInfo
    {
      [Tooltip("回避攻撃：できていいか")]
      public bool avoidAttackEnable = true;
      [Tooltip("両手剣[ノーマル]の闘気溜め用エフェクト名")]
      public string nameChargeExpandEffect;
      [Tooltip("両手剣[ノーマル]の闘気溜めを溜めMAXで自動解放するか")]
      public bool isChargeExpandAutoRelease = true;
      [Tooltip("両手剣[ノーマル]の闘気溜めの溜め時間")]
      public float timeChargeExpandMax = 3f;
      [Tooltip("両手剣[ノーマル]の闘気溜めの溜め時間最短の秒数")]
      public float minTimeChargeExpandMax = 0.5f;
      [Tooltip("両手剣[ノーマル]の闘気溜めによる属性ダメージ倍率最低値")]
      public float elementDamageRateMin = 1.1f;
      [Tooltip("両手剣[ノーマル]の闘気溜めによる属性ダメージ倍率")]
      public float elementDamageRate = 2f;
      [Tooltip("両手剣[ノーマル]の闘気溜めによる属性ダメージアップ倍率(MAXチャージ時)")]
      public float elementDamageRateFullCharge = 4f;
      [Tooltip("[ヒート]最大溜めHit後入力受付時間")]
      public float timeSpAttackContinueInput = 0.4f;
      [Tooltip("[ヒート]ヒートゲージ増加値：基礎")]
      public float heatGaugeIncreaseBase = 30f;
      [Tooltip("[ヒート]ブースト中の属性防御係数")]
      public float heatComboElementDefRate = 1f;
      [Tooltip("[Soul] 長い場合の通常攻撃ID")]
      public int Soul_LongAttackId = 15;
      [Tooltip("[Soul] 長い場合の通常攻撃最終ID")]
      public int Soul_LongAttackFinishId = 18;
      [Tooltip("[Soul] 長い場合の特殊アクションID")]
      public int Soul_LongSpAttackId = 95;
      [Tooltip("[ソウル]歩く速度")]
      public float soulWalkSpeed = 0.113f;
      [Tooltip("[ソウル]部位へのダメージ倍率")]
      public float soulRegionDamageRate = 2f;
      [Tooltip("[ソウル]居合 溜め時間")]
      public float soulIaiChargeTime;
      [Tooltip("[ソウル]居合 溜め時間(最小)")]
      public float soulIaiChargeTimeMin;
      [Tooltip("[ソウル]居合 溜め最大時エフェクト")]
      public string soulIaiChargeMaxEffect = "ef_btl_wsk_longsword_02_03";
      [Tooltip("[ソウル]居合 溜め最大時SEID")]
      public int soulIaiChargeMaxSeId = 40000359;
      [Tooltip("[ソウル]居合 抜刀ポーズ時間")]
      public float soulIaiInSec = 0.15f;
      [Tooltip("[ソウル]居合 抜刀移動時間")]
      public float soulIaiMoveSec = 0.2f;
      [Tooltip("[ソウル]居合 抜刀消えてる時間")]
      public float soulIaiHideSec = 0.18f;
      [Tooltip("[ソウル]居合 移動速度[0]Min[1]Max")]
      public float[] soulIaiMoveSpeed;
      [Tooltip("[ソウル]居合 無属性ダメージアップ[0]Min[1]Max")]
      public float[] soulIaiNormalDamageUp;
      [Tooltip("[ソウル]居合 ゲージ上昇量[0]Min[1]Max")]
      public float[] soulIaiGaugeIncreaseValue;
      [Tooltip("[ソウル]コンボ最終 ゲージ上昇量")]
      public float soulComboGaugeIncreaseValue = 50f;
      [Tooltip("[ソウル]ジャストタップ 許容時間")]
      public float soulJustTapEnableSec = 1f;
      [Tooltip("[ソウル]ジャストタップ ゲージ上昇割合")]
      public float soulJustTapGaugeRate = 1.5f;
      [Tooltip("[ソウル]ソウル玉 通常スケール")]
      public float soulSoulEnergyNormalScale = 100f;
      [Tooltip("[ソウル]ソウル玉 ジャストタップスケール")]
      public float soulSoulEnergyJustTapScale = 150f;
      [Tooltip("[ソウル]刀舞モード ゲージ上昇割合")]
      public float soulBoostModeGaugeRate = 0.3f;
      [Tooltip("[ソウル]刀舞モード 突入時のSE")]
      public int soulBoostSeId = 20000106;
      [Tooltip("[ソウル]刀舞モード 攻撃速度-最低")]
      public float soulBoostMinAttackSpeed = 0.1f;
      [Tooltip("[ソウル]刀舞モード 攻撃速度-加算")]
      public float soulBoostAddAttackSpeed = 0.09f;
      [Tooltip("[ソウル]刀舞モード 攻撃速度-最大")]
      public float soulBoostMaxAttackSpeed = 1f;
      [Tooltip("[ソウル]刀舞モード 属性ダメージ倍率")]
      public float soulBoostElementDamageRate = 4f;
      [Tooltip("[ソウル]刀舞モード ゲージ減少値(毎秒)")]
      public float soulBoostGaugeDecreasePerSecond = 65f;
      [Tooltip("[ソウル]刀舞モード 溜め時ゲージ減少値(毎秒)")]
      public float soulBoostChargeGaugeDecreasePerSecond = 30f;
      [Tooltip("[ソウル]刀舞モード ダメージでリセットするか")]
      public bool isSoulBoostResetTriggerDamage = true;
      [Tooltip("[ソウル]刀舞モード ダメージでリセットするか")]
      public float soulBoostWaitPacketSec = 60f;
      [Tooltip("[Burst] バースト両手剣設定")]
      public InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = new InGameSettingsManager.Player.BurstTwoHandSwordActionInfo();
      [Tooltip("[Oracle] オラクル両手剣設定")]
      public InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleTHSInfo = new InGameSettingsManager.Player.OracleTwoHandSwordActionInfo();
    }

    [Serializable]
    public class BurstTwoHandSwordActionInfo
    {
      [Tooltip("[Burst] 基本攻撃のID")]
      public int BaseAtkId = 70;
      [Tooltip("[Burst] 基本攻撃02 のID")]
      public int BaseAtkCombo02 = 71;
      [Tooltip("[Burst] 基本攻撃03 のID")]
      public int BaseAtkCombo03 = 72;
      [Tooltip("[Burst] フルバースト動作のAttackID")]
      public int FullBurstAttackID = 73;
      [Tooltip("[Burst] 射撃構えのAttackID")]
      public int ReadyForShotID = 74;
      [Tooltip("[Burst] 初回射撃のAttackID")]
      public int FirstShotAttackID = 75;
      [Tooltip("[Burst] 連続射撃のAttackID")]
      public int NextShotAttackID = 76;
      [Tooltip("[Burst] 初回ReloadアクションのID")]
      public int FirstReloadActionAttackID = 77;
      [Tooltip("[Burst] 連続ReloadアクションのID")]
      public int NextReloadActionAttackID = 78;
      [Tooltip("[Burst] 兜割り(単発射撃へ接続可能)")]
      public int BurstAvoidAttackID = 79;
      [Tooltip("[Burst] 距離減衰の最小減衰距離")]
      [SerializeField]
      private float MinAttenuationDistance = 10f;
      [Tooltip("[Burst] 距離減衰の最大減衰距離")]
      [SerializeField]
      private float MaxAttenuationDistance = 1000f;
      [Tooltip("[Burst] 距離減衰の最小ダメージレート")]
      [SerializeField]
      private float MinAttenuationDmgRate = 0.01f;
      [Tooltip("[Burst] 距離減衰の最大ダメージレート")]
      [SerializeField]
      private float MaxAttenuationDmgRate = 1f;
      [Tooltip("[Burst] 距離減衰定義(X:正規化距離[0.0 1.0] Y:正規化ダメージ補正値[0.0 1.0]")]
      [SerializeField]
      private AnimationCurve AnimCurve = Curves.CreateEaseInCurve();
      [Tooltip("[Burst] 射撃系の属性ダメージ倍率")]
      [SerializeField]
      public float SingleShotBaseDmgRate = 1f;
      [Tooltip("[Burst] 射撃系の属性ダメージ倍率")]
      [SerializeField]
      public float SingleShotElementDmgRate = 2f;
      [Tooltip("[Burst] 射撃系の属性ダメージ倍率")]
      [SerializeField]
      public float FullBurstBaseDmgRate = 2f;
      [Tooltip("[Burst] 射撃系の属性ダメージ倍率")]
      [SerializeField]
      public float FullBurstElementDmgRate = 4f;
      [Tooltip("単発ショットのヒットエフェクト(属性差分あり")]
      public string[] HitEffect_SingleShot;
      [Tooltip("フルバーストのヒットエフェクト(属性差分あり")]
      public string[] HitEffect_FullBurst;

      public float GetDistanceAttenuationRatio(float _distance)
      {
        if ((double) _distance < 0.0 || this.AnimCurve == null)
          return 0.0f;
        if ((double) _distance < (double) this.MinAttenuationDistance)
          return this.MaxAttenuationDmgRate;
        return (double) _distance > (double) this.MaxAttenuationDistance ? this.MinAttenuationDmgRate : (this.MaxAttenuationDmgRate - this.MinAttenuationDmgRate) * (Mathf.Round(this.AnimCurve.Evaluate((float) (((double) _distance - (double) this.MinAttenuationDistance) / ((double) this.MaxAttenuationDistance - (double) this.MinAttenuationDistance))) * 100f) / 100f) + this.MinAttenuationDmgRate;
      }
    }

    [Serializable]
    public class OracleTwoHandSwordActionInfo
    {
      [Tooltip("通常バーニアエフェクト名")]
      public string normalVernierEffectName;
      [Tooltip("通常バーニア点火時SEのID")]
      public int normalVernierSeId = 20000012;
      [Tooltip("各属性の最大バーニアエフェクト名")]
      public string[] maxVernierEffectNames;
      [Tooltip("最大バーニア点火時SEのID")]
      public int maxVernierSeId = 10000098;
      [Tooltip("他人のバーニアエフェクトを表示するか")]
      public bool isPlayOtherPlayerVernierEffect = true;
      [Tooltip("SP攻撃(横回転)中のダメージアップ対象のAttackInfo名リスト")]
      public string[] horizontalDamageUpAttackInfoNames;
      [Tooltip("SP攻撃(横回転)追加回転の通常時の各段階情報")]
      public InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] horizontalSpinInfoList;
      [Tooltip("SP攻撃(横回転)追加回転の最大溜め時の初速倍率")]
      public float horizontalMaxFirstSpeed = 2f;
      [Tooltip("SP攻撃(横回転)追加回転の最大溜め時の各段階情報")]
      public InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] horizontalMaxSpinInfoList;
      [Tooltip("回転を継続するのに攻撃のHitが必要か")]
      public bool needHitHorizontal = true;
      [Tooltip("回転継続時に出血ダメージが発生するか")]
      public bool isBleedingDamageOfHorizontalNext = true;
      [Tooltip("溜め通常：無属性ダメージ倍率")]
      public float chargeBaseDmgRate = 1f;
      [Tooltip("溜め通常：属性ダメージ倍率")]
      public float chargeElementDmgRate = 1f;
      [Tooltip("溜め最大：無属性ダメージ倍率")]
      public float maxChargeBaseDmgRate = 1f;
      [Tooltip("溜め最大：属性ダメージ倍率")]
      public float maxChargeElementDmgRate = 1f;
      [Tooltip("脳震盪中の敵への属性ダメージ倍率")]
      public float concussionEnemyElementDmgRate = 3f;

      [Serializable]
      public class HorizontalSpinInfo
      {
        [Tooltip("回転速度倍率")]
        public float spinSpeedRate;
        [Tooltip("ダメージ倍率")]
        public float damageRate;
      }
    }

    [Serializable]
    public class PairSwordsActionInfo
    {
      [Tooltip("ノーマル：乱舞のActionId")]
      public int wildDanceAttackID = 89;
      [Tooltip("ノーマル：溜めなし乱舞のActionId")]
      public int wildDanceNoneChargeAttackID = 88;
      [Tooltip("ノーマル：乱舞の溜め完了SeId")]
      public int wildDanceChargeMaxSeId = 10000104;
      [Tooltip("双剣[ヒート]の連刃モード中攻撃&移動速度アップ倍率")]
      public float boostAttackAndMoveSpeedUpRate = 0.5f;
      [Tooltip("双剣[ヒート]の連刃モード中回避距離アップ倍率")]
      public float boostAvoidUpRate = 0.5f;
      [Tooltip("双剣[ヒート]のヒートゲージ減少値(毎秒)")]
      public float boostGaugeDecreasePerSecond = 65f;
      [Tooltip("双剣[ヒート]のヒートゲージ増加基礎値")]
      public float boostGaugeIncreaseBase = 20f;
      [Tooltip("双剣[ヒート]のダメージアップレベルを1つ上げるのに必要な攻撃回数")]
      public int boostDamageUpLevelUpHitCount = 7;
      [Tooltip("双剣[ヒート]のダメージアップレベル最大値")]
      public int boostDamageUpLevelMax = 4;
      [Tooltip("双剣[ヒート]のダメージアップレベル1ごとに上がる倍率")]
      public float boostDamageUpRatePerLevel = 1f;
      [Tooltip("[Heat] 通常攻撃のAttackId")]
      public int Heat_AttackId = 94;
      [Tooltip("[Soul] 通常攻撃のAttackId")]
      public int Soul_AttackId = 10;
      [Tooltip("[Soul] コンボ外連続攻撃のAttackId")]
      public int Soul_AttackNextId = 12;
      [Tooltip("[Soul] SP攻撃（レーザー待機）のAttackId")]
      public int Soul_SpLaserWaitAttackId = 92;
      [Tooltip("[Soul] SP攻撃（レーザー発射）のAttackId")]
      public int Soul_SpLaserShotAttackId = 93;
      [Tooltip("[Soul] 武器エフェクト名（無属性）")]
      public string Soul_EffectForWeapon = "ef_btl_wep04_s2";
      [Tooltip("[Soul] 武器エフェクト名（属性）")]
      public string[] Soul_EffectsForWeapon;
      [Tooltip("[Soul] 魔弾エフェクト名（無属性）")]
      public string Soul_EffectForBullet = "ef_btl_wsk2_twinsword_01_02";
      [Tooltip("[Soul] 魔弾エフェクト名（属性）")]
      public string[] Soul_EffectsForBullet;
      [Tooltip("[Soul] レーザー待機エフェクト名")]
      public string Soul_EffectForWaitingLaser = "ef_btl_wsk2_twinsword_02_01";
      [Tooltip("[Soul] コンボレベルごとのレーザーのattackInfo名")]
      public string[] Soul_AttackInfoNamesForLaserByComboLv;
      [Tooltip("[Soul] コンボレベルごとのレーザーの本数")]
      public int[] Soul_NumOfLaserByComboLv;
      [Tooltip("[Soul] レーザー複数時の半径")]
      public float Soul_RadiusForLaser = 0.6f;
      [Tooltip("[Soul] 魔弾ヒット時のソウルゲージ上昇値")]
      public float Soul_SoulGaugeIncreaseValueBySoulBullet;
      [Tooltip("[Soul] 魔弾ヒット後からソウルゲージ減少が始まるまでの時間（sec）")]
      public float Soul_TimeForGaugeDecreaseAfterHit = 2f;
      [Tooltip("[Soul] 魔弾ヒット後からソウルゲージ減少が始まるまでの時間（ゲージ100%時）（sec）")]
      public float Soul_TimeForGaugeDecreaseAfterHitOnComboLvMax = 5f;
      [Tooltip("[Soul] ソウルゲージ減少量（/sec）")]
      public float Soul_GaugeDecreasePerSecond = 30f;
      [Tooltip("[Soul] ソウルゲージ減少量（レーザー待機中）（/sec）")]
      public float Soul_GaugeDecreaseWaitingLaserPerSecond = 10f;
      [Tooltip("[Soul] ソウルゲージ減少量（レーザー発射中）（/sec）")]
      public float Soul_GaugeDecreaseShootingLaserPerSecond = 200f;
      [Tooltip("[Soul] ソウルゲージ減少量（被ダメージ時）")]
      public float Soul_GaugeDecreaseByDamage = 100f;
      [Tooltip("[Soul] コンボレベル数（1からカウント）")]
      public int Soul_NumOfComboLv = 4;
      [Tooltip("[Soul] それぞれのコンボレベルに必要なソウルゲージ量（%）")]
      public float[] Soul_GaugePercentForComboLv;
      [Tooltip("[Soul] コンボレベルごとの攻撃速度アップ倍率")]
      public float[] Soul_AttackSpeedUpRatesByComboLv;
      [Tooltip("[Soul] コンボレベルごとの魔弾AtkRate補正倍率")]
      public float[] Soul_AtkRatesForBulletByComboLv;
      [Tooltip("[Soul] コンボレベルごとのレーザーAtkRate補正倍率")]
      public float[] Soul_AtkRatesForLaserByComboLv;
      [Tooltip("[Soul] SEのid(0:開始,1:ループ,2:終了)")]
      public int[] Soul_SeIds;
      [Tooltip("[Soul] 開始のSEを再生してから何秒後にループのSEを再生するか")]
      public float Soul_TimeForPlayLoopSE;
      [Tooltip("[Burst] 空中時にコライダー移動するか")]
      public bool Burst_IsUpdateAerialCollider = true;
      [Tooltip("[Burst] 左武器合体時のPos")]
      public Vector3 Burst_CombinePosition;
      [Tooltip("[Burst] 左武器合体時の回転")]
      public Vector3 Burst_CombineEuler;
      [Tooltip("[Burst] 移動速度アップ倍率")]
      public float Burst_MoveSpeedUpRate = 0.5f;
      [Tooltip("[Burst] 回避速度アップ倍率")]
      public float Burst_AvoidSpeedUpRate = 0.5f;
      [Tooltip("[Burst] ゲージ減少値(毎秒)")]
      public float Burst_BoostGaugeDecreasePerSecond = 65f;
      [Tooltip("[Burst] ゲージ増加基礎値")]
      public float Burst_BoostGaugeIncreaseBase = 20f;
      [Tooltip("[Burst] ブースト中の攻撃速度")]
      public float Burst_BoostAttackSpeedUpRate = 0.5f;
      [Tooltip("[Burst] 両刃モードの属性ダメージアップ")]
      public float Burst_CombineElementDamageUpRate = 0.5f;
      [Tooltip("[Burst] ブースト中の地上コンボの属性ダメージアップ")]
      public float Burst_BoostGroundSpElementDamageUpRate = 1f;
      [Tooltip("[Burst] 両刃モード ヒットエフェクト")]
      public string[] Burst_CombineHitEffect;
      [Tooltip("[Burst] 両刃モード ヒットエフェクトスケール")]
      public Vector3 Burst_CombineHitEffectScale;
      [Tooltip("[Oracle] ゲージ上昇量(毎秒)")]
      public float Oracle_SpGaugeIncreasePerSecond = 60f;
      [Tooltip("[Oracle] ゲージ減少量(毎秒)")]
      public float Oracle_SpGaugeDecreasePerSecond = 60f;
      [Tooltip("[Oracle] ラッシュ攻撃減少量")]
      public float Oracle_SpGaugeRushDecrease = 20f;
      [Tooltip("[Oracle] ジャスト回避ゲージ回復割合")]
      public float Oracle_JustAvoidGaugeIncreaseRate = 0.2f;
      [Tooltip("[Oracle] ラッシュ攻撃属性ダメージ倍率")]
      public float Oracle_RushElementDamageRate = 5f;
      [Tooltip("[Oracle] SP攻撃属性ダメージ倍率")]
      public float Oracle_SpElementDamageRate = 3f;
      [Tooltip("[Oracle] ジャスト回避モーションストップ時間")]
      public float Oracle_JustAvoidMotionStopTime = 0.5f;
    }

    [Serializable]
    public class ArrowActionInfo
    {
      [Tooltip("弓のライン色：通常")]
      public Color bulletLineColor = Color.yellow;
      [Tooltip("弓のライン色：ソウル")]
      public Color bulletLineColorSoul = Color.magenta;
      [Tooltip("弓のライン色：ソウル(Max)")]
      public Color bulletLineColorSoulFull = Color.red;
      [Tooltip("弓のライン参照のattackInfoの名前")]
      public string[] attackInfoNames;
      [Tooltip("弓のしゃがみ撃ち用attackInfo名")]
      public string[] attackInfoForSitShotNames;
      [Tooltip("影縫矢が刺さったエフェクト名")]
      public string shadowSealingEffectName;
      [Tooltip("影縫矢が消えるまでの秒数")]
      public float shadowSealingExistSec = 30f;
      [Tooltip("影縫矢が消えるまでの最低秒数")]
      public float shadowSealingExistMinSec = 2.5f;
      [Tooltip("影縫バフ時の弓短縮レート(1.0f以上はだめ)")]
      public float shadowSealingBuffChargeRate = 0.9f;
      [Tooltip("影縫バフ時の距離減衰上書き(0.0fで未使用)")]
      public float shadowSealingBuffDistanceRate = 1f;
      [Tooltip("しゃがみ撃ちの溜め短縮レート")]
      public float sitShotChargeSpeedUpRate;
      [Tooltip("しゃがみ撃ちの弾速アップレート")]
      public float sitShotBulletSpeedUpRate = 1f;
      [Tooltip("ヒートのしゃがみ撃ちの2本目以降射出角度")]
      public float sitShotSideAngle = 5f;
      [Tooltip("貫通のインターバル")]
      public float pierceInterval = 0.033f;
      [Tooltip("マーカーに当たった際、貫通継続するか")]
      public bool isPierceAfterTarget = true;
      [Tooltip("爆弾矢が爆発するまでの秒数")]
      public float bombArrowCountSec = 10f;
      [Tooltip("爆弾矢が即爆発するまでの刺せる回数")]
      public int bombArrowMaxLevel = 3;
      [Tooltip("[SOUL]発射インターバル")]
      public float soulShotInterval = 0.05f;
      [Tooltip("[SOUL]射出Dir")]
      public Vector3[] soulShotDirs;
      [Tooltip("[SOUL]射出逆方向係数")]
      public float soulShotPosVec = 0.7f;
      [Tooltip("[SOUL]射出逆方向係数")]
      public float soulShotDirVec = 0.9f;
      [Tooltip("[SOUL]1部位ロック回数によるAtkRate")]
      public float[] soulLockNumAtkRate;
      [Tooltip("[SOUL]ロック順によるAtkRate基本値")]
      public float soulLockOrderAtkRateBase = 0.5f;
      [Tooltip("[SOUL]ロック順によるAtkRate係数")]
      public float soulLockOrderAtkRateCoefficient = 0.1f;
      [Tooltip("[SOUL]ロック順によるAtkRate最終ロック")]
      public float soulLockOrderAtkRateMax = 2f;
      [Tooltip("[SOUL]ロック順によるAtkRate最終ロック(boost)")]
      public float soulLockOrderAtkRateMaxBoost = 4f;
      [Tooltip("[SOUL]Raycastの長さ")]
      public float soulRaycastDistance = 60f;
      [Tooltip("[SOUL]最大ロックオン数")]
      public int soulLockMax = 6;
      [Tooltip("[SOUL]ブースト時の最大ロックオン数")]
      public int soulBoostLockMax = 12;
      [Tooltip("[SOUL]1部位辺りの最大ロックオン数")]
      public int soulLockRegionMax = 3;
      [Tooltip("[SOUL]同じ部位へのロックオン間隔")]
      public float soulLockRegionInterval = 0.5f;
      [Tooltip("[SOUL]同じ部位へのロックオン間隔(boost中)")]
      public float soulBoostLockRegionInterval = 0.4f;
      [Tooltip("[SOUL]ロック限界SE")]
      public int soulLockMaxSeId = 40000358;
      [Tooltip("[SOUL]ロックオンSE")]
      public int soulLockSeId = 20000018;
      [Tooltip("[SOUL]ソウルゲージ上昇値")]
      public float soulGaugeIncreaseValue = 50f;
      [Tooltip("[SOUL]ブースト中ゲージ減少値(毎秒)")]
      public float soulBoostGaugeDecreasePerSecond = 65f;
      [Tooltip("[SOUL]ブースト中の属性ダメージアップ")]
      public float soulBoostElementDamageRate = 10f;
      [Tooltip("[BURST]爆弾矢爆発AttackInfoName")]
      public string bombArrowAttackInfoName = "PLC05_attack_bomb_";
      [Tooltip("[BURST]蒼穹撃ち: AttackId")]
      public int arrowRainShotAttackId = 99;
      [Tooltip("[BURST]蒼穹撃ち: リロードからのAttackId")]
      public int arrowReloadRainShotAttackId = 96 /*0x60*/;
      [Tooltip("[BURST]蒼穹撃ち：カーソルエフェクト名")]
      public string arrowRainShotAimLesserCursorEffectName = "ef_btl_arrow_marker_02";
      [Tooltip("[BURST]バーストゲージ上昇値リスト")]
      public List<float> burstGaugeIncreaseValueList;
      [Tooltip("[BUST]ブースト中ゲージ減少値(毎秒)")]
      public float burstBoostGaugeDecreasePerSecond = 65f;
      [Tooltip("[BURST]蒼穹撃ち: 落下する矢のAttackInfo名")]
      public string arrowRainShotAttackInfoName = "PLC05_attack_99_arrow_rain";
      [Tooltip("[BURST]爆弾矢爆発スケールリスト")]
      public List<float> bombArrowBurstEffectScaleList = new List<float>();
      [Tooltip("[BURST]ブースト中チャージMAX時通常撃ちエフェクト名")]
      public string boostArrowChargeMaxEffectName = "ef_btl_wsk3_bow_shot_01_01";
      [Tooltip("[BURST]ブースト中チャージMAX時通常撃ちAttackInfo名")]
      public string boostArrowChargeMaxAttackInfoName = "PLC05_attack_03_boost";
      [Tooltip("[BURST]爆発属性ダメージ倍率リスト")]
      public List<float> arrowBombElementDamageRateList = new List<float>();
      [Tooltip("[BURST]爆発矢が刺さったエフェクト名")]
      public string bombArrowEffectName = "ef_btl_wsk3_bow_0";
      [Tooltip("[BURST]蒼穹撃ち: ランダム角度分割幅")]
      public float arrowRainAngleDivide = 30f;
      [Tooltip("[BURST]蒼穹撃ち: ランダム距離分割幅")]
      public float arrowRainLengthDivide = 1f;
      [Tooltip("[BURST]蒼穹撃ち: 最大フレーム間隔")]
      public int arrowRainMaxFrameInterval = 3;
      [Tooltip("[BURST]爆発SEIdリスト")]
      public List<int> bombSEIdList = new List<int>();
      [Tooltip("[BURST]蒼穹撃ち：ブースト爆発レベル")]
      public int arrowRainBoostBombLevel = 1;
      [Tooltip("[BURST]蒼穹撃ち: 爆発オフセットY")]
      public float arrowRainBoostBombOffsetY = 1f;
      [Tooltip("[BURST]爆発エフェクト名")]
      public string bombEffectName = "ef_btl_bomb_arrow_burst_";
      [Tooltip("[BURST]爆弾矢が刺さった時のSEIdリスト")]
      public List<int> bombArrowSEIdList = new List<int>();
      [Tooltip("[BURST]蒼穹撃ち: 貫通ダメージ間隔")]
      public float arrowRainPierceDamageInterval = 0.01f;
      [Tooltip("[BURST]ブーストモード発動SEId")]
      public int burstBoostModeSEId = 10000051;
      [Tooltip("[BURST]爆発遅延秒数リスト")]
      public List<float> bombDelayFrameList = new List<float>();
      [Tooltip("[BURST]爆発オフセット位置リスト")]
      public List<Vector3> bombOffsetPositionList = new List<Vector3>();

      public string GetBombArrowEffectName(ELEMENT_TYPE type)
      {
        return this.bombArrowEffectName + ((int) type).ToString();
      }

      public string GetBombEffectName(ELEMENT_TYPE type)
      {
        return this.bombEffectName + ((int) type).ToString();
      }
    }

    [Serializable]
    public class OneHandSwordActionInfo
    {
      [Tooltip("[共通]ガード時のダメージ減少率")]
      public float Common_GuardDamageCutRate = 0.3f;
      [Tooltip("[共通]カウンター攻撃のマギチャージ割合")]
      public float Common_CounterHealSkillRate = 5f;
      [Tooltip("[共通]ジャストガードになる秒数")]
      public float Common_JustGuardValidSec = 0.66f;
      [Tooltip("[Normal]ガード時、未確定HP回復速度上昇率")]
      public float Normal_GuardingHealSpeedRate = 4f;
      [Tooltip("[Normal]ジャストガード時のダメージ減少率")]
      public float Normal_JustGuardDamageCutRate = 0.1f;
      [Tooltip("[Normal]蘇生時間倍率")]
      public float Normal_PrayBoostRate = 1.5f;
      [Tooltip("[Heat]ジャストガード時のマギ回復量")]
      public float Heat_JustGuardSkillHealValue = 100f;
      [Tooltip("[Heat]カウンター２段目のゲージ溜まる固定値")]
      public float Heat_RevengeCounterValue = 100f;
      [Tooltip("[Heat]ジャスガからのカウンター２段目のゲージ溜まる固定値")]
      public float Heat_RevengeJustCounterValue = 120f;
      [Tooltip("[Heat]ゲージ溜まる固定値")]
      public float Heat_RevengeValue = 400f;
      [Tooltip("[Heat]通常ガードのゲージ溜まる割合")]
      public float Heat_RevengeGuardRate = 6f;
      [Tooltip("[Heat]ジャストガードのゲージ溜まる割合")]
      public float Heat_RevengeJustGuardRate = 9f;
      [Tooltip("[Heat]リベンジアタックするために溜める時間")]
      public float Heat_RevengeAttackChargeSec = 1f;
      [Tooltip("[Soul] 魔爪攻撃の修正後AttackID")]
      public int Soul_AlteredSpAttackId = 94;
      [Tooltip("[Soul] 魔爪モード 突入時のSE")]
      public int Soul_BoostSeId = 20000106;
      [Tooltip("[Soul] 魔爪ヒット時のSE")]
      public int Soul_SnatchHitSeId = 20000106;
      [Tooltip("[Soul] スナッチヒットエフェクト")]
      public string Soul_SnatchHitEffect = "ef_btl_wsk2_sword_02_01";
      [Tooltip("[Soul] スナッチヒットエフェクト（ブーストモード中）")]
      public string Soul_SnatchHitEffectOnBoostMode = "ef_btl_wsk2_sword_02_02";
      [Tooltip("[Soul] 魔爪の敵に残るエフェクト")]
      public string Soul_SnatchHitRemainEffect = "ef_btl_wsk2_sword_01";
      [Tooltip("[Soul] 魔爪モード 属性ヒットエフェクト")]
      public string[] Soul_BoostElementHitEffect;
      [Tooltip("[Soul] 引き寄せ時の到達点マージン")]
      public float Soul_MoveStopRange = 2f;
      [Tooltip("[Soul] 引き寄せ時の速度")]
      public float Soul_SnatchMoveVelocity = 60f;
      [Tooltip("[Soul] ソウルゲージ上昇値")]
      public float Soul_ComboGaugeIncreaseValue = 100f;
      [Tooltip("[Soul] ジャストタップ ゲージ上昇割合")]
      public float Soul_JustTapGaugeRate = 1.5f;
      [Tooltip("[Soul] 魔爪モード ゲージ上昇割合")]
      public float Soul_BoostModeGaugeRate = 0.3f;
      [Tooltip("[Soul] ソウルゲージ減少量（毎秒）")]
      public float Soul_BoostGaugeDecreasePerSecond = 65f;
      [Tooltip("[Soul] ソウルゲージ減少量（毎秒）（スナッチ中）")]
      public float Soul_BoostSnatchGaugeDecreasePerSecond = 10f;
      [Tooltip("[Soul] 魔爪モード 属性ダメージ倍率")]
      public float Soul_BoostElementDamageRate = 4f;
      [Tooltip("[Soul] 魔爪モード 突き攻撃のAttackInfo名")]
      public string Soul_BoostSpAttackName = "PLC00_attack_95_02";
      [Tooltip("[Soul] 魔爪モード 突き攻撃のダウン値")]
      public int Soul_BoostSpAttackDownValue = 300;
      [Tooltip("[Soul] 魔爪によるダウンゲージ減少速度ダウン倍率")]
      public float[] Soul_DownGaugeDecreaseRates;
      [Tooltip("[Soul] AnimCtrlステート移行タイムリミット")]
      public float Soul_AnimStateTimeLimit = 3f;
      [Tooltip("[Soul] AnimCtrl突進ループタイムリミット")]
      public float Soul_AnimStateTimeLimitForMoveLoop = 1f;
      [Tooltip("[Heat] ヒート片手剣設定")]
      public InGameSettingsManager.Player.HeatOneHandSwordActionInfo heatOHSInfo = new InGameSettingsManager.Player.HeatOneHandSwordActionInfo();
      [Tooltip("[Soul] ソウル片手剣設定")]
      public InGameSettingsManager.Player.SoulOneHandSwordActionInfo soulOHSInfo = new InGameSettingsManager.Player.SoulOneHandSwordActionInfo();
      [Tooltip("[Burst] バースト片手剣設定")]
      public InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = new InGameSettingsManager.Player.BurstOneHandSwordActionInfo();
      [Tooltip("[Oracle] オラクル片手剣設定")]
      public InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOHSInfo = new InGameSettingsManager.Player.OracleOneHandSwordActionInfo();
    }

    [Serializable]
    public class HeatOneHandSwordActionInfo
    {
      [Tooltip("[Heat] カウンター攻撃ID")]
      public int counterAttackId = 98;
      [Tooltip("[Heat] リベンジストライク攻撃ID")]
      public int revengeStrikeAttackId = 92;
    }

    [Serializable]
    public class SoulOneHandSwordActionInfo
    {
      [Tooltip("[Soul] 基本攻撃のID")]
      public int BaseAtkId = 10;
    }

    [Serializable]
    public class BurstOneHandSwordActionInfo
    {
      [Tooltip("[Burst]ジャストガードになる秒数")]
      public float JustGuardValidSec = 0.66f;
      [Tooltip("[Burst] 破迅突き 属性ヒットエフェクト")]
      public string[] BoostElementHitEffect;
      [Tooltip("[Burst]破迅突きによる属性ダメージ倍率")]
      public float elementDamageRate = 2f;
      [Tooltip("[Burst]ブーストゲージの秒数")]
      public float GaugeTime = 10f;
      [Tooltip("[Burst]ブースト中の攻撃速度上昇倍率")]
      public float boostAttackSpeedUpRate = 0.5f;
      [Tooltip("[Burst]ブースト中の属性ダメージアップ")]
      public float BoostElementDamageRate = 2f;
      [Tooltip("[Burst] 基本攻撃のID")]
      public int BaseAtkId = 18;
      [Tooltip("[Burst] 回避攻撃（シールドタックル）のID")]
      public int AvoidAttackID = 22;
      [Tooltip("[Burst] 破迅突きのID")]
      public int CounterAttackId = 93;
    }

    [Serializable]
    public class OracleOneHandSwordActionInfo
    {
      [Tooltip("[Oracle] 通常攻撃SPゲージ増加量")]
      public float spGaugeIncreasingValue = 10f;
      [Tooltip("[Oracle] ブーストモード中通常攻撃SPゲージ増加量")]
      public float spGaugeIncreasingValueWhileBoost = 3f;
      [Tooltip("[Oracle] ブースト時間")]
      public float spGaugeDecreasingValue = 10f;
      [Tooltip("[Oracle] 通常コンボ開始AttackId")]
      public int comboAttackId = 24;
      [Tooltip("[Oracle] 竜撃AttackId")]
      public int specialAttackId = 28;
      [Tooltip("[Oracle] 回避攻撃")]
      public int avoidAttackId = 30;
      [Tooltip("[Oracle] 竜人エフェクトリスト")]
      public InGameSettingsManager.Player.OracleOneHandSwordActionInfo.DragonEffect[] dragonEffects;
      [Tooltip("[Oracle] 竜人化中モーション速度倍率加算値")]
      public float boostMotionSpeedAdditionRate = 0.3f;
      [Tooltip("[Oracle] 通常攻撃属性倍率")]
      public float normalAttackElementDamageRate = 1f;
      [Tooltip("[Oracle] 竜撃属性倍率")]
      public float spAttackElementDamageRate = 3f;
      [Tooltip("[Oracle] ブーストモード中通常攻撃属性倍率")]
      public float boostNormalAttackElementDamageRate = 1.5f;
      [Tooltip("[Oracle] ブーストモード中竜撃属性倍率")]
      public float boostSpAttackElementDamageRate = 6f;

      [Serializable]
      public class DragonEffect
      {
        [Tooltip("エフェクト名")]
        public string name;
        [Tooltip("エフェクトを再生するノード名")]
        public string link;
        [Tooltip("属性indexをファイル名末尾につけるか")]
        public bool hasElementVariation;

        public string GetEffectName(ELEMENT_TYPE e)
        {
          return this.hasElementVariation ? this.name + (object) (int) e : this.name;
        }
      }
    }

    [Serializable]
    public class SpearActionInfo
    {
      [Tooltip("ヒート：歩く速度")]
      public float heatWalkSpeed = 0.1f;
      [Tooltip("百烈：ループの最大秒")]
      public float hundredLoopLimitSec = 3f;
      [Tooltip("百烈：連打とみなす間隔")]
      public float hundredTapIntervalSec = 0.4f;
      [Tooltip("突進：キャンセル可能時間")]
      public float rushCancellableTime = 0.3f;
      [Tooltip("突進：ループ時間")]
      public float rushLoopTime = 0.26f;
      [Tooltip("突進：回避が可能になるフレーム")]
      public float rushCanAvoidTime = 0.1f;
      [Tooltip("突進：距離倍率")]
      public float rushDistanceRate = 1f;
      [Tooltip("突進：ループ版の特殊攻撃ID")]
      public int rushLoopAttackID = 96 /*0x60*/;
      [Tooltip("極め突き：できていいか")]
      public bool exRushEnable;
      [Tooltip("極め突き：開始有効時間")]
      public float exRushValidSec = 2f;
      [Tooltip("極め突き：溜め時間")]
      public float exRushChargeSec = 3f;
      [Tooltip("極め突き：属性ダメージ倍率最低値")]
      public float exRushElementDamageRateMin = 1.5f;
      [Tooltip("極め突き：属性ダメージ倍率最大値")]
      public float exRushElementDamageRateMax = 3f;
      [Tooltip("極め突き：属性ダメージ倍率フルチャージ")]
      public float exRushElementDamageRateFull = 6f;
      [Tooltip("極め突き：フルチャージのSE")]
      public int exRushChargeMaxSeId = 40000359;
      [Tooltip("ジャンプ：ゲージ増加基礎値")]
      public float jumpGaugeIncreaseBase = 10f;
      [Tooltip("ジャンプ：基礎溜め時間")]
      public float jumpChargeBaseSec = 2f;
      [Tooltip("ジャンプ：最小溜め時間")]
      public float jumpChargeMinSec = 0.5f;
      [Tooltip("ジャンプ：最大溜め時SE")]
      public int jumpChargeMaxSeId = 40000359;
      [Tooltip("ジャンプ：降下開始高さ")]
      public float jumpStartHeight = 7f;
      [Tooltip("ジャンプ：降下前ウェイト")]
      public float jumpFallWaitSec = 0.5f;
      [Tooltip("ジャンプ：降下スピード")]
      public float jumpFallSpeed = 80f;
      [Tooltip("ジャンプ：成功時の着地位置")]
      public float jumpRandingLength = 4f;
      [Tooltip("ジャンプ：属性ダメージ倍率")]
      public float[] jumpElementDamageRate;
      [Tooltip("ジャンプ：Lv2無属性ヒットエフェクト")]
      public string jumpHugeHitEffectName;
      [Tooltip("ジャンプ：Lv2属性ヒットエフェクト")]
      public string[] jumpHugeElementHitEffectNames;
      [Tooltip("ジャンプ：衝撃波のAttackInfoのPrefix")]
      public string jumpWaveAttackInfoPrefix;
      [Tooltip("ジャンプ：衝撃波のレベル別半径 lv0〜")]
      public float[] jumpWaveColliderRadius;
      [Tooltip("ジャンプ：衝撃波のレベル別半径 lv0〜")]
      public float[] jumpWaveScales;
      [Tooltip("ジャンプ：成功時のHitStop")]
      public float jumpHitStop = 0.05f;
      [Tooltip("ジャンプ：くるりんY制御開始時間")]
      public float jumpRandingHeightStartTime = 0.83f;
      [Tooltip("ジャンプ：くるりんY制御終了時間")]
      public float jumpRandingHeightEndTime = 1.2f;
      [Tooltip("ジャンプ：くるりんXZ制御開始時間")]
      public float jumpRandingMoveStartTime = 0.1f;
      [Tooltip("ジャンプ：くるりんXZ制御終了時間")]
      public float jumpRandingMoveEndTime = 1.3f;
      [Tooltip("[Heat] 特殊アクション攻撃ID")]
      public int Heat_SpAttackId = 97;
      [Tooltip("[Soul] 移動速度")]
      public float Soul_WalkSpeedUpRate = 0.1f;
      [Tooltip("[Soul] 通常攻撃ID")]
      public int Soul_AttackId = 14;
      [Tooltip("[Soul] 特殊アクション攻撃ID")]
      public int Soul_SpAttackId = 95;
      [Tooltip("[Soul] 特殊アクション2撃目ID")]
      public int Soul_SpAttackContinueId = 94;
      [Tooltip("[Soul] 回避攻撃アクションID")]
      public int Soul_AvoidAttackId = 23;
      [Tooltip("[Soul] ゲージ上昇値")]
      public float Soul_GaugeIncreaseValue = 100f;
      [Tooltip("[Soul] ジャストタップ ゲージ上昇割合")]
      public float Soul_JustTapGaugeRate = 1.5f;
      [Tooltip("[Soul] 魔槍モード中 ゲージ上昇割合")]
      public float Soul_BoostModeGaugeRate = 0.3f;
      [Tooltip("[Soul] 魔槍モード中 ゲージ減少値 特殊アクション溜め中（毎秒）")]
      public float Soul_BoostModeGaugeDecreasePerSecond = 100f;
      [Tooltip("[Soul] 魔槍モード中 ゲージ減少値（毎秒）")]
      public float Soul_BoostModeGaugeDecreasePerSecondOnSpActionCharging = 50f;
      [Tooltip("[Soul] 魔槍モード中 ダメージアップ倍率")]
      public float Soul_BoostElementDamageRate = 4f;
      [Tooltip("[Soul] 特殊アクション最大溜め時エフェクトオフセット")]
      public Vector3 Soul_SpAttackMaxChargeEffectOffsetPos = new Vector3(0.0f, 0.0f, -0.35f);
      [Tooltip("[Soul] HP使用or回復に対応する攻撃ID")]
      public int[] Soul_AttackIdsForSacrifice;
      [Tooltip("[Soul] HP使用割合（%）")]
      public int[] Soul_SacrificeHPPercents;
      [Tooltip("[Soul] HP回復割合（%）")]
      public int[] Soul_HealHPPercents;
      [Tooltip("[Burst] バースト情報")]
      public InGameSettingsManager.Player.BurstSpearActionInfo burstSpearInfo = new InGameSettingsManager.Player.BurstSpearActionInfo();
      [Tooltip("オラクル槍パラメータ")]
      public InGameSettingsManager.Player.SpearActionInfo.Oracle oracle = new InGameSettingsManager.Player.SpearActionInfo.Oracle();

      [Serializable]
      public class Oracle
      {
        [Tooltip("コンボ開始AttackId")]
        public int comboAttackId = 35;
        [Tooltip("予約AttackId(コンボ開始からここまで)")]
        public int reservedAttackId = 45;
        [Tooltip("防御AttackId")]
        public int guardAttackId = 39;
        [Tooltip("防御中のダメージ割合")]
        public float damageCutRateWhileGuard = 0.7f;
        [Tooltip("ジャストガード中のダメージ割合")]
        public float damageCutRateWhileJustGuard = 0.1f;
        [Tooltip("攻撃時のSPチャージベース値")]
        public float spChargingValue = 5f;
        [Tooltip("回避攻撃Id")]
        public int avoidAttackId = 40;
        [Tooltip("オラクルSPチャージ完了後の属性ダメージ倍率")]
        public float chargedSpElementDamageRate = 5f;
        [Tooltip("オラクルSPチャージ完了前の属性ダメージ倍率")]
        public float spElementDamageRate = 1f;
        [Tooltip("根性ベース時間")]
        public float gutsBaseTime = 2f;
        [Tooltip("ストック消費１つに対しての根性時間")]
        public float gutsTimePerStock = 1f;
        [Tooltip("ストック全て溜めた時の属性ダメージ割合")]
        public float elementDamageRateFullStocked = 2f;
        [Tooltip("ストック全て溜めた時の攻撃速度割合")]
        public float attackSpeedRateFullStocked = 1.5f;
        [Tooltip("防御中にカットしたダメージをSPに変換する割合")]
        public float damageConvertToSpRate = 5f;
        [Tooltip("根性発動中の属性攻撃倍率")]
        public float elementDamageRateWhileGuts = 4f;
        [Tooltip("根性発動中の攻撃速度倍率")]
        public float attackSpeedRateWhileGuts = 1.5f;
        [Tooltip("根性発動SE")]
        public int gutsSE = 10000074;
        [Tooltip("最低ジャストガード判定時間")]
        public float minJustGuardSec = 0.1f;
        [Tooltip("最高ジャストガード判定時間")]
        public float maxJustGuardSec = 1f;
        [Tooltip("ストックMAXの時のSPチャージ時間の割合")]
        public float spChargeTimeRateFullStocked = 0.8f;
      }
    }

    [Serializable]
    public class BurstSpearActionInfo
    {
      [Tooltip("[Burst] 基本攻撃ID")]
      public int baseAtkId = 30;
      [Tooltip("[Burst] 特殊アクション攻撃ID")]
      public int spAtkId = 34;
      [Tooltip("[Burst] ヒット後コンボの攻撃ID")]
      public int hitComboAttackId = 33;
      [Tooltip("[Burst] 回避攻撃ID")]
      public int avoidAtkId = 20;
      [Tooltip("[Burst] 回避距離アップ値")]
      public float avoidSpeedUpRate = 0.2f;
      [Tooltip("[Burst] 武器回転速度（最低値）")]
      public float spinSpeedMin;
      [Tooltip("[Burst] 武器回転速度（最高値）")]
      public float spinSpeedMax = 100f;
      [Tooltip("[Burst] 武器回転が最高速度に達するまでの時間（秒）")]
      public float spinTimeToMaxSpeed = 2f;
      [Tooltip("[Burst] ゲージ増加値（/秒）")]
      public float gaugeIncreasePerSecond = 10f;
      [Tooltip("[Burst] ゲージ減少値（特殊アクション時）（/秒）")]
      public float gaugeDecreaseOnSpAttackPerSecond = 10f;
      [Tooltip("[Burst] ゲージ減少値（武器回転時）（/秒）")]
      public float gaugeDecreaseOnSpinPerSecond = 3f;
      [Tooltip("[Burst] バリア内にいるときのダメージ軽減後割合（75%なら0.25）")]
      public float inBarrierDamageRate = 0.25f;
      [Tooltip("[Burst] 回転中の属性ダメージアップ倍率（倍）")]
      public float spinElementDamageRate = 2f;
      [Tooltip("[Burst] 最大速度で回転中の属性ダメージアップ倍率（倍）")]
      public float spinElementDamageRateMax = 4f;
      [Tooltip("[Burst] 回転中属性ヒットエフェクト")]
      public string[] spinElementHitEffectNames;
      [Tooltip("[Burst] 回転中属性ヒットエフェクトのスケール")]
      public Vector3 spinElementHitEffectScale;
      [Tooltip("[Burst] 回転エフェクト")]
      public string[] spinEffectNames;
      [Tooltip("[Burst] 槍投げ接地エフェクト")]
      public string throwGroundEffectName;
      [Tooltip("[Burst] 槍投げ接地中エフェクト")]
      public string[] spinThrowGroundEffectNames;
      [Tooltip("[Burst] 回転中SE")]
      public int spinSeId;
      [Tooltip("[Burst] 回転中SE（最大回転速度）")]
      public int spinMaxSpeedSeId;
    }

    [Serializable]
    public class BarrierBrokenReaction
    {
      [Tooltip("リアクションタイプ")]
      public AttackHitInfo.ToPlayer.REACTION_TYPE reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;
      [Tooltip("リアクション時間")]
      public float loopTime = 0.3f;
      [Tooltip("吹き飛ばし力")]
      public float blowForce = 100f;
      [Tooltip("吹き飛ばし角度")]
      public float blowAngle = 30f;
      [Tooltip("無敵になる秒数")]
      public float invincibleDuration = 0.3f;
    }

    [Serializable]
    public class TeleportationInfo
    {
      [Tooltip("ターゲットからどれだけ戻ったところを移動先とするか")]
      public float offsetScalar = 1f;
      [Tooltip("ターゲットがなかったときに、ちょっとだけ進む距離")]
      public float forwardScalar = 3f;
      public InGameSettingsManager.Player.TeleportationInfo.OffsetByWeaponAndAttack[] offsetByWeaponAndAttack;

      [Serializable]
      public class OffsetByWeaponAndAttack
      {
        public int attackMode;
        public int attackID;
        public float value;
      }
    }
  }

  [Serializable]
  public class Evolve
  {
    public InGameSettingsManager.Evolve.GaugeInfo[] gaugeInfo;
    public InGameSettingsManager.Evolve.Type10000 type10000;
    public InGameSettingsManager.Evolve.Type10001 type10001;

    [Serializable]
    public class GaugeInfo
    {
      public EQUIPMENT_TYPE type;
      public float value = 15f;
    }

    [Serializable]
    public class TypeAbstract
    {
      public float execSec = 15f;
      public int healValue;
      public HEAL_TYPE[] healTypes;
      public InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff[] buffs;

      [Serializable]
      public class EvolveBuff
      {
        public BuffParam.BUFFTYPE type;
        public int value;
      }
    }

    [Serializable]
    public class Type10000 : InGameSettingsManager.Evolve.TypeAbstract
    {
      public float execTime = 15f;
      public float execEffectDelay = 0.86f;
      public int rushSeId = 20000106;
      public float rushDistanceRate = 0.5f;
      public float damageRateMin = 3f;
      public float damageRateMax = 6f;
      public float damageRateFull = 12f;
    }

    [Serializable]
    public class Type10001 : InGameSettingsManager.Evolve.TypeAbstract
    {
      public float rangeUp = 2f;
      public float elementDamageRate = 4f;
      public float specialLoopSec = 1.5f;
    }
  }

  [Serializable]
  public class Enemy
  {
    [Tooltip("ヒットライト時間")]
    public float hitShockLightTime = 0.2f;
    [Tooltip("ヒットライト最大RimPower")]
    public float hitShockLightRimPower = 3f;
    [Tooltip("ヒットライト最大RimWidth")]
    public float hitShockLightRimWidth = 0.2f;
    [Tooltip("ヒットオフセット時間")]
    public float hitShockOffsetTime = 0.2f;
    [Tooltip("ヒットオフセット距離")]
    public float hitShockOffsetLength = 0.3f;
    [Tooltip("ダメージ数値表示")]
    public bool showDamageNum = true;
    [Tooltip("モンスターへの属性別ヒットSE")]
    public int[] elementHitSEIDs = new int[6];
    [Tooltip("麻痺ヒットエフェクト名")]
    public string paralyzeHitEffectName;
    [Tooltip("毒ヒットエフェクト名")]
    public string poisonHitEffectName;
    [Tooltip("他プレイヤー用簡易ヒットエフェクト名")]
    public string otherSimpleHitEffectName;
    [Tooltip("ダウン値の上昇レート")]
    public float[] downMaxRate;
    [Tooltip("狙いマーカーの標準サイズ")]
    public float aimMarkerBaseRate = 1f;
    [Tooltip("狙いマーカーの当たり半径")]
    public float aimMarkerHitRadius = 1f;
    [Tooltip("AI基本待機時間")]
    public float baseAfterWaitTime = 0.5f;
    [Tooltip("Lv別AI待機時間の閾値配列")]
    public float[] afterWaitTimeThresholdsByLv;
    [Tooltip("Lv別AI待機時間の時間配列")]
    public float[] afterWaitTimesByLv;
    [Tooltip("押し合い速度（距離/s")]
    public float jostleSpeed = 2f;
    [Tooltip("ザコ同期時の位置ズレ許容距離")]
    public float lesserEnemiesPositionMargin = 5f;
    [Tooltip("ステータスUI表示距離")]
    public float showStatusUIRange = 5f;
    [Tooltip("常時Aim有効テスト")]
    public bool testAimTarget;
    [Tooltip("足踏みエフェクト距離制限")]
    public float stampDistance = 30f;
    [Tooltip("狙い当たり判定ヒット比較許容深さ")]
    public float hitCompareAimDepthLimit = 0.8f;
    [Tooltip("当たり判定ヒット距離チェック速度")]
    public float hitCompareSpeed = 10f;
    [Tooltip("当たり判定ヒット比較許容距離")]
    public float hitCompareLengthLimit = 2f;
    [Tooltip("突進最大距離レート")]
    public float dashMaxDistanceRate = 2f;
    [Tooltip("ゲストのザコのEnemyOut呼び出し遅延時間（秒")]
    public float guestEnemyOutTime = 1f;
  }

  [Serializable]
  public class DropItem
  {
    public AnimationCurve popAnim = Curves.CreateArcHalfCurve();
    public float popAnimTime = 1f;
    public float popHeight = 30f;
    public float popSpeed = 10f;
    public float rotationSpeed = 90f;
    public float defHeight = 1f;
  }

  [Serializable]
  public class FieldDropItem
  {
    public float getDistance = 1f;
    public float popAnimTime = 1f;
    public float popHight = 4f;
    public AnimationCurve popAnim = Curves.CreateArcHalfCurve();
    public float getAnimTime = 1f;
    public AnimationCurve distanceAnim = Curves.CreateEaseInCurve();
    public float rotateSpeed = 20f;
    public AnimationCurve rotateSpeedAnim = Curves.CreateEaseInCurve();
    public AnimationCurve scaleAnim = Curves.CreateEaseInCurve();
    public AnimEventData animEventData;
    [Tooltip("アイテムを落とす際の座標のオフセットの最大値")]
    public Vector3 offsetMin;
    [Tooltip("アイテムを落とす際の座標のオフセット最小値")]
    public Vector3 offsetMax;
    public string tresureBoxOpenEffect;
  }

  [Serializable]
  public class DropMaker
  {
    public Mesh mesh;
    public Material material;
    public float animSpeed = 30f;
    public Vector3 offset;
    public Vector3 rotOffset;
    public float portalHeight;
  }

  [Serializable]
  public class Room
  {
    [Tooltip("受付終了する敵のHP割合")]
    public float entryCloseEnemyHpRate = 0.5f;
    [Tooltip("受付終了する経過時間割合")]
    public float entryCloseTimeRate = 0.5f;
    [Tooltip("受付終了する経過時間割合")]
    public float checkCharacterSyncInterval = 10f;
  }

  [Serializable]
  public class InGameProgress
  {
    [Tooltip("パケット送信チェック時間")]
    public float checkCompleteSendTimeout = 10f;
    [Tooltip("終了時の通信待ち表示までの間")]
    public float waitNetworkMarginTime = 1f;
    [Tooltip("勝利演出後のリザルトまでの時間")]
    public float victoryIntervalTime = 10f;
    [Tooltip("オーナーの通信待ちタイムアウト時間")]
    public float waitCompleteOwnerTimeout = 10f;
    [Tooltip("NPC再チェック時間")]
    public float npcCheckIntervalTime = 5f;
  }

  [Serializable]
  public class Portal
  {
    [Tooltip("ポータルエフェクト名")]
    public string[] effectNames;
    [Tooltip("ポイント取得エフェクト名")]
    public string pointGetEffectName;
    [Tooltip("アイコンエネルギー段階数")]
    public int pointRankNum = 10;
    [Tooltip("解放時取得魔石数")]
    public int clearCrystalNum = 1;
    [Tooltip("ハード解放時取得魔石数")]
    public int clearHardCrystalNum = 5;
    public InGameSettingsManager.Portal.PointEffect pointEffect;

    [Serializable]
    public class PointEffect
    {
      [Tooltip("ポイント（普通）エフェクト名")]
      public string normalEffectName;
      [Tooltip("ポイント（大）エフェクト名")]
      public string largeEffectName;
      public float popHeightAnimTime = 1f;
      public float popHeight = 4f;
      public AnimationCurve popHeightAnim = Curves.CreateEaseInCurve();
      public float getSpeedAnimTime = 1f;
      public float getSpeed = 20f;
      public AnimationCurve getSpeedAnim = Curves.CreateEaseInCurve();
      public float targetHeight = 1.5f;
    }
  }

  [Serializable]
  public class HappenQuestDirection
  {
    [Tooltip("警告時間（秒")]
    public float warningTime = 3f;
    [Tooltip("確認UI表示時間（秒")]
    public float confirmUITime = 30f;
    [Tooltip("モンスター初期位置")]
    public Vector3 enemyInitPos = Vector3.zero;
    [Tooltip("モンスター初期向き（角度")]
    public float enemyInitDir;
    [CustomArray("typeName")]
    public InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo[] enemyDisplayInfos;
    [CustomArray("typeName")]
    public InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo[] enemyDisplayInfoForDefense;

    public InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo GetEnemyDisplayInfo(
      EnemyTable.EnemyData enemyData)
    {
      InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo enemyDisplayInfo = Array.Find<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>(this.enemyDisplayInfos, (Predicate<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>) (o => o.modelID == enemyData.modelId));
      if (enemyDisplayInfo == null)
      {
        string typeName = enemyData.type.ToString();
        enemyDisplayInfo = Array.Find<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>(this.enemyDisplayInfos, (Predicate<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>) (o => o.typeName == typeName));
      }
      return enemyDisplayInfo;
    }

    public InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo GetEnemyDisplayInfoByQuestType(
      EnemyTable.EnemyData enemyData,
      QUEST_STYLE questStyle)
    {
      InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo[] enemyDisplayInfoArray = questStyle != QUEST_STYLE.DEFENSE ? this.enemyDisplayInfos : this.enemyDisplayInfoForDefense;
      if (((IList<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>) enemyDisplayInfoArray).IsNullOrEmpty<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>())
        return (InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo) null;
      InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo displayInfoByQuestType = Array.Find<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>(enemyDisplayInfoArray, (Predicate<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>) (o => o.modelID == enemyData.modelId));
      if (displayInfoByQuestType == null)
      {
        string typeName = enemyData.type.ToString();
        displayInfoByQuestType = Array.Find<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>(enemyDisplayInfoArray, (Predicate<InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo>) (o => o.typeName == typeName));
      }
      return displayInfoByQuestType;
    }

    [Serializable]
    public class EnemyDisplayInfo
    {
      public int modelID;
      public string typeName;
      public string cameraNamePortrait;
      public Vector3 cameraOffsetPortrait;
      public string cameraNameLandscape;
      public Vector3 cameraOffsetLandscape;
      public Vector3 modelOffset;
    }
  }

  [Serializable]
  public class UseResources
  {
    public string[] effects;
    public string[] uiEffects;
  }

  [Serializable]
  public class UIParam
  {
    [Tooltip("武器切り替え時に武器名が残る秒数")]
    public float weaponDecideRemainTime = 0.3f;
  }

  [Serializable]
  public class BuffParamInfo
  {
    [Tooltip("ダメージ吸収のパラメータ")]
    public InGameSettingsManager.AbsorbDamageParam absorbDamageParam;
    [Tooltip("自動復活可能回数")]
    public int autoReviveMaxCount = 1;
    [Tooltip("無効系のインターバル")]
    public float invincibleInterval = 2f;
    [Tooltip("デコイのヒットインターバル")]
    public float decoyHitInterval = 2f;
    [Tooltip("[SUBSTITUTE]高さ")]
    public float substituteHeight = 1f;
    [Tooltip("[SUBSTITUTE]追尾速さ")]
    public float substituteLerpSpeed = 5f;
    [Tooltip("[SUBSTITUTE]オフセット1")]
    public float substituteOffset1 = 0.5f;
    [Tooltip("[SUBSTITUTE]オフセット2")]
    public float substituteOffset2 = 0.35f;
    [Tooltip("強化バフ解除無効発動時の効果時間")]
    public float invincibleBuffCancellationExpandTime = 2f;
  }

  [Serializable]
  public class DebuffParam
  {
    [Tooltip("猛毒のパラメーター")]
    public InGameSettingsManager.DeadlyPoisonParam deadlyPosion;
    [Tooltip("凍結のパラメーター")]
    public InGameSettingsManager.FreezeParam freezeParam;
    [Tooltip("滑りデバフのパラメータ")]
    public InGameSettingsManager.SlideParam slideParam;
    [Tooltip("滑りデバフ（氷）のパラメータ")]
    public InGameSettingsManager.SlideParam slideIceParam;
    [Tooltip("沈黙デバフのパラメータ")]
    public InGameSettingsManager.SilenceParam silenceParam;
    [Tooltip("影縫デバフのパラメータ")]
    public InGameSettingsManager.ShadowSealingParam shadowSealingParam;
    [Tooltip("攻撃速度減少デバフのパラメータ")]
    public InGameSettingsManager.AttackSpeedDownParam attackSpeedDownParam;
    [Tooltip("沈黙デバフのパラメータ")]
    public InGameSettingsManager.CantHealHpParam cantHealHpParam;
    [Tooltip("暗闇デバフのパラメータ")]
    public InGameSettingsManager.BlindParam blindParam;
    [Tooltip("石化デバフのパラメータ")]
    public InGameSettingsManager.StoneParam stoneParam;
    [Tooltip("バフ解除で無視するBUFFTYPE")]
    public List<BuffParam.BUFFTYPE> ignoreBuffCancellation;
    [Tooltip("光輪のパラメーター")]
    public InGameSettingsManager.LightRingParam lightRingParam;
    [Tooltip("脳震盪のパラメーター")]
    public InGameSettingsManager.Concussion concussion;
    [Tooltip("出血デバフのパラメータ")]
    public InGameSettingsManager.BleedingParam bleedingParam;
    [Tooltip("酸デバフのパラメータ")]
    public InGameSettingsManager.AcidPatam acidParam;
    [Tooltip("腐敗デバフのパラメータ")]
    public InGameSettingsManager.CorruptionPatam corruptionParam;
    [Tooltip("聖痕のパラメータ")]
    public InGameSettingsManager.StigmataPatam stigmataParam;
    [Tooltip("過雷デバフのパラメータ")]
    public InGameSettingsManager.ThunderstormParam cyclonicThunderstormParam;
  }

  [Serializable]
  public class MadModeParam
  {
    [Tooltip("マギダメージにかかる係数")]
    public float skillDamagedRate = 0.25f;
    [Tooltip("状態異常蓄積値にかかる係数")]
    public float badStatusRate = 0.25f;
    [Tooltip("状態異常耐性があがるだけのBUFFTYPE")]
    public List<BuffParam.BUFFTYPE> onlyResistDebuff;
    [Tooltip("魔狂化時に矢が抜けるか")]
    public bool isClearStuckArrow;
  }

  [Serializable]
  public class PassiveParam
  {
    [Tooltip("防御力閾値(この閾値を超えた分はダメージへの影響が弱くなる)")]
    public int playerDefenseThreshold = 1250;
    [Tooltip("変化係数(閾値を超えた防御力の影響を弱めるための係数)")]
    public int playerDefenseCoefficient = 700;
  }

  [Serializable]
  public class DeadlyPoisonParam
  {
    public float duration = 20f;
    public float interval = 2f;
    public float percent = 0.1f;
  }

  [Serializable]
  public class FreezeParam
  {
    public float duration = 7f;
    public float damageRate = 1.3f;
  }

  [Serializable]
  public class SlideParam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
    [Tooltip("最高速度に到達するまでの時間")]
    public float needTimeToMaxTime = 2.5f;
    [Tooltip("滑る時間")]
    public float slideTime = 1f;
    [Tooltip("回避した際の滑るスピード")]
    public float avoidSpeed = 4f;
  }

  [Serializable]
  public class SilenceParam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
  }

  [Serializable]
  public class CantHealHpParam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
  }

  [Serializable]
  public class BlindParam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
  }

  [Serializable]
  public class StoneParam
  {
    [Tooltip("継続時間")]
    public float duration = 30f;
    [Tooltip("石化状態プレイヤーのグレースケール（0だと黒）")]
    public float grayScalePow = 0.5f;
    [Tooltip("石化状態プレイヤーのグレースケール加算レート（基本は1）")]
    public float grayScaleRate = 1f;
  }

  [Serializable]
  public class ShadowSealingParam
  {
    public float duration = 7f;
    public float minDuration = 2f;
    public float resistRate = 0.5f;
    public int startSeId = 10000082;
    public int loopSeId = 30000007;
    public int endSeId = 30000028;
    public bool isReactionDamage;
  }

  [Serializable]
  public class AttackSpeedDownParam
  {
    [Tooltip("継続時間")]
    public float duration = 10f;
    [Tooltip("減少値")]
    public int value = 50;
    [Tooltip("鈍敵エフェクトスケール")]
    public float enemyEffectSize = 1.5f;
  }

  [Serializable]
  public class LightRingParam
  {
    [Tooltip("継続時間")]
    public float duration = 9f;
    [Tooltip("光輪開始時のSE")]
    public int startSeId;
    [Tooltip("光輪中のSE")]
    public int loopSeId;
    [Tooltip("光輪終了時のSE")]
    public int endSeId;
  }

  [Serializable]
  public class Concussion
  {
    [Tooltip("継続時間")]
    public float duration = 5f;
    [Tooltip("耐性初期値")]
    public float resistBase = 1000f;
    [Tooltip("耐性上昇率")]
    public float resistRate = 1.1f;
    [Tooltip("開始時SEのID(0なら無し)")]
    public int startSeId;
    [Tooltip("ループSEのID(0なら無し)")]
    public int loopSeId;
    [Tooltip("終了時SEのID(0なら無し)")]
    public int endSeId;
  }

  [Serializable]
  public class BleedingParam
  {
    [Tooltip("基礎効果時間")]
    public float duration = 20f;
    [Tooltip("ダメージの")]
    public float damageHpRate = 0.05f;
    [Tooltip("片手剣で出血ダメージが入らないAtkIdリスト")]
    public int[] ignoreOneHandSwordAtkIds;
    [Tooltip("両手剣で出血ダメージが入らないAtkIdリスト")]
    public int[] ignoreTwoHandSwordAtkIds;
    [Tooltip("双剣で出血ダメージが入らないAtkIdリスト")]
    public int[] ignorePairSwordAtkIds;
    [Tooltip("槍で出血ダメージが入らないAtkIdリスト")]
    public int[] ignoreSpearAtkIds;
    [Tooltip("弓で出血ダメージが入らないAtkIdリスト")]
    public int[] ignoreArrowAtkIds;
    [Tooltip("出血エフェクト名")]
    public string effectName = "ef_btl_pl_blood_01";
    [Tooltip("出血エフェクトNode")]
    public string effectNodeName = "Root";
    [Tooltip("出血エフェクト座標")]
    public Vector3 effectPosition = Vector3.zero;
    [Tooltip("出血エフェクト回転")]
    public Vector3 effectRotation = Vector3.zero;
    [Tooltip("出血エフェクトサイズ")]
    public float effectScale = 1.2f;
  }

  [Serializable]
  public class AcidPatam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
    [Tooltip("ダメージ間隔")]
    public float interval = 2f;
    [Tooltip("ダメージ倍率")]
    public float percent = 0.02f;
  }

  [Serializable]
  public class CorruptionPatam
  {
    [Tooltip("継続時間")]
    public float duration = 20f;
    [Tooltip("ダメージ間隔")]
    public float interval = 2f;
    [Tooltip("ダメージ倍率")]
    public float percent = 0.02f;
  }

  [Serializable]
  public class StigmataPatam
  {
    [Tooltip("エフェクト名(敵専用)")]
    public string effectName = "ef_btl_enm_corruption_01";
  }

  [Serializable]
  public class ThunderstormParam
  {
    [Tooltip("エフェクト名(敵専用)")]
    public string effectName = "ef_btl_enm_corruption_01";
  }

  [Serializable]
  public class AbsorbDamageParam
  {
    public int limitPlayerAbsorbDamage = 200;
    public int limitPlayerHitAbsorb = 200;
    public float limitRateEnemyAbsorbDamage = 0.1f;
  }

  [Serializable]
  public class AbilityParam
  {
    public float oneHandSwordRadiusCustomRate = 1f;
  }

  [Serializable]
  public class TutorialParam
  {
    [Tooltip("チュートリアルボスID")]
    public int enemyID = 110010911;
    [Tooltip("チュートリアルボスレベル")]
    public int enemyLv = 1;
    [Tooltip("ボスの最小HPの割合(これ以下は減らない)")]
    public float bossMinHpRate = 0.2f;
    [Tooltip("ボス逃走開始HPの割合(bossMinHpRateより大きい値を設定すること)")]
    public float bossEscapeHpRate = 0.5f;
    [Tooltip("一人で戦っている間の秒数、この秒数をすぎたら強制的にスキル説明")]
    public float soloBattleTimeLimit = 30f;
    [Tooltip("スキル打たずにこの時間が経過したら次のチュートリアルへ進む")]
    public float skillWaitLimitTime = 30f;
    [Tooltip("仲間と一緒い戦っている間の秒数")]
    public float battleWithFriendTime = 35f;
    public float bossRefillHpRate = 0.25f;
    public int[] botWeaponIds = new int[2]
    {
      20160111,
      20260860
    };
    public int[] botSkillIds = new int[2]
    {
      100200100,
      100200601
    };
    public float atkIncreaseRate = 0.1f;
    public float deplayTimeNpcUseSkill = 6f;
    public int deplayTimeNpcUseSkillOffset = 10;
    public int bossMultiXHp = 5;
    public float bossScale = 1.5f;
  }

  [Serializable]
  public class ArenaParam
  {
    [Tooltip("マギ蓄積速度ダウン基礎倍率")]
    public float magiSpeedDownRateBase = 0.5f;
    [Tooltip("マギ蓄積速度ダウン係数")]
    public float magiSpeedDownRate = 0.1f;
    [Tooltip("マギ蓄積速度ダウンの効果が効かなくなる限界突破数")]
    public float magiSpeedDownRegistSkillExceedLv = 5f;
    [Tooltip("マギ蓄積速度アップ基礎倍率")]
    public float magiSpeedUpBaseRate = 0.5f;
  }

  [Serializable]
  public class DefenseBattleParam
  {
    [Tooltip("耐久力")]
    public float defenseEndurance = 1000f;
    [Tooltip("戦闘開始時大型モンスター位置オフセット")]
    public Vector3 bossAppearOffsetPos = new Vector3(57f, 0.0f, 0.0f);
    [Tooltip("戦闘開始時大型モンスターY軸回転")]
    public float bossAppearAngleY;
    [Tooltip("耐久物の名称")]
    public string enduranceObjectName;
    [Tooltip("迎撃戦ver2でのアラート開始残り時間")]
    public float remainingTimeForAlert = 60f;
  }

  [Serializable]
  public class CannonParam
  {
    [Tooltip("速射砲のクールタイム")]
    public float coolTimeForRapid = 0.1f;
    [Tooltip("速射砲の発射SEID")]
    public int seIdForRapid = 10000080;
    [Tooltip("迫撃砲のクールタイム")]
    public float coolTimeForHeavy = 0.5f;
    [Tooltip("波動砲のチャージタイム")]
    public float chargeTimeMaxForSpecial = 2f;
    [Tooltip("波動砲のチャージSEID")]
    public int seIdForSpecialCharge = 10000099;
    [Tooltip("波動砲の発射SEID")]
    public int seIdForSpecial = 10000101;
    [Tooltip("波動砲の始動SEID")]
    public int seIdForSpecialOnBoard = 10000098;
    [Tooltip("波動砲のゲージ上昇SEID")]
    public int seIdForSpecialChargeMax = 10000100;
    [Tooltip("波動砲のカメラ切替ディレイ")]
    public float delayChangeCameraForSpecial = 0.5f;
    [Tooltip("フィールド砲のクールタイム")]
    public float coolTimeForField = 0.1f;
    [Tooltip("フィールド砲の発射SEID")]
    public int seIdForField = 10000094;
  }

  [Serializable]
  public class WaveMatchParam
  {
    [Tooltip("WAVE_EVENTか")]
    public bool isEvent;
    [Tooltip("マギゲージ上昇タイプ")]
    public InGameSettingsManager.WaveMatchParam.eGaugeType skillGaugeType = InGameSettingsManager.WaveMatchParam.eGaugeType.Rate;
    [Tooltip("マギゲージ上昇量")]
    public float skillGaugeValue = 0.3f;
    [Tooltip("SP/アストラルゲージ上昇タイプ")]
    public InGameSettingsManager.WaveMatchParam.eGaugeType spGaugeType = InGameSettingsManager.WaveMatchParam.eGaugeType.Rate;
    [Tooltip("SP/アストラルゲージ上昇量")]
    public float spGaugeValue = 0.3f;
    [Tooltip("雑魚敵のダメージ")]
    public int enemyNormalDamage = 100;
    [Tooltip("ボス敵のダメージ")]
    public int enemyBossDamage = 500;
    [Tooltip("アニメーション切り替わり")]
    public string targetChangeAnimEffect = "ef_btl_wyvern_downsmoke_01";
    [Tooltip("ターゲットHitEffect")]
    public string targetHitEffect = "ef_btl_wyvern_downsmoke_01";
    [Tooltip("ターゲットHitEffectSclae")]
    public Vector3 targetHitEffectScale;
    [Tooltip("ターゲットHitSe")]
    public int targetHitSeId = 10000041;
    [Tooltip("ターゲットBreakSe")]
    public int targetBreakSeId = 10000029;
    [Tooltip("Wave開始Se")]
    public int waveJingleId = 40000069;
    [Tooltip("ホストリタイヤのディレイ")]
    public float hostRetireDelay = 1f;
    [Tooltip("コンテンツ防衛戦の場合カメラをあげる")]
    public float cameraFieldOffsetY;

    public enum eGaugeType
    {
      Normal,
      Zero,
      Rate,
      Constant,
    }
  }

  [Serializable]
  public class FishingParam
  {
    [Tooltip("予兆開始時間（0:min, 1:max)")]
    public float[] waitSec;
    [Tooltip("予兆インターバル")]
    public float omenInterval = 1f;
    [Tooltip("最大予兆回数")]
    public int maxOmenNum = 3;
    [Tooltip("！がでてる時間")]
    public float hookSec = 2f;
    [Tooltip("Effect:予兆")]
    public string[] omenEffect;
    [Tooltip("Effect:ばちゃばちゃ")]
    public string[] hookEffect;
    [Tooltip("SE:水面に落ちる(ぽちゃん)")]
    public int[] se0Id;
    [Tooltip("SE:予兆(ばしゃ)")]
    public int[] se1Id;
    [Tooltip("SE:リール巻いてる(ばしゃばしゃ)LOOP")]
    public int[] se2Id;
    [Tooltip("SE:水面から引き上げる(ばしゃーん)")]
    public int[] se3Id;
    [Tooltip("SE:！のSE")]
    public int hookSeId = 40000013;
    [Tooltip("SE:釣った(0:スカ、1:アイテム、2:魚、3:モンスター)")]
    public int[] hitSeIds;
    [Tooltip("通信モーション：最低保証")]
    public float sendMinSec = 0.5f;
    [Tooltip("通信モーション:釣った(0:スカ、1:アイテム、2:魚、3:モンスター)")]
    public float[] sendSec;
    [Tooltip("通信モーション：冠タイプ(0:通常、1:銀、2:金")]
    public float[] sendCrownTypeSec;
    [Tooltip("通信モーション：レアの時")]
    public float sendRareSec = 1f;
    [Tooltip("敵釣り時インフォメーションディレイ")]
    public float delayEnemyFishing = 5f;
    [Tooltip("敵釣り時のアニメーション時間")]
    public float hitEnemyMoveSec = 0.5f;
    [Tooltip("共釣りゲージの最大値")]
    public float coopFishingGaugeMax = 100f;
    [Tooltip("共釣りゲージの初期値")]
    public float coopFishingGaugeInitial = 30f;
    [Tooltip("共釣りゲージの減少値（/sec）")]
    public float coopFishingGaugeDecreasePerSec = 10f;
    [Tooltip("自分による共釣りゲージの増加量（/1タップ）")]
    public float coopFishingGaugeIncreasePerTapBySelf = 4f;
    [Tooltip("他人による共釣りゲージの増加量（/1タップ")]
    public float coopFishingGaugeIncreasePerTapByOther = 3f;
    [Tooltip("共釣りゲージの減少開始までの時間（最後の増加からの秒数）")]
    public float coopFishingGaugeMarginSecToStartDecrease = 1f;
    [Tooltip("共釣りゲージの赤になるまでの時間（最後の増加からの秒数）")]
    public float coopFishingGaugeMarginToStartChangeRed = 1f;
    [Tooltip("共釣りホストが出すスタンプID")]
    public int coopFishingStampId = -1;
    [Tooltip("共釣りホストがスタンプ出すインターバル")]
    public float coopFishingStampRoutineSec = 5f;
    [Tooltip("共釣りゲストが出すスタンプID")]
    public int coopFishingGuestStampId = -1;
    [Tooltip("共釣りゲストがスタンプ出すインターバル")]
    public float coopFishingGuestStampRoutineSec = 5f;
  }
}
