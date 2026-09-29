// Decompiled with JetBrains decompiler
// Type: AttackHitInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class AttackHitInfo : AttackInfo
{
  [Tooltip("攻撃力倍率")]
  public float atkRate = 1f;
  [Tooltip("攻撃力")]
  public AtkAttribute atk = new AtkAttribute();
  [Tooltip("自身の攻撃力を無視する")]
  public bool isAtkIgnore;
  [Tooltip("ダウン値")]
  public float down;
  [Tooltip("ダウン中にヒットしてもダウン値を増やすか")]
  public bool isForceDown;
  [Tooltip("状態異常値")]
  public BadStatus badStatus = new BadStatus();
  [Tooltip("モーションストップ時間")]
  public float motionStopTime;
  [Tooltip("カメラ揺れ大きさ")]
  public float shakeCameraPercent;
  [Tooltip("カメラ揺れ周期（0で共通設定")]
  public float shakeCycleTime;
  [Tooltip("多重ヒット無効フラグ")]
  public bool enableIdentityCheck = true;
  [Tooltip("ヒットの中に居続けても連続してダメージを受けるか")]
  public bool isValidTriggerStay;
  [Tooltip("ヒット間隔（秒。多重ヒット有効時")]
  public float hitIntervalTime = 1f;
  [Tooltip("最大ヒット数（0で無制限。多重ヒット有効時")]
  public int hitCountMax;
  [Tooltip("ヒットエフェクト名")]
  public string hitEffectName;
  [Tooltip("共通SEを再生するか")]
  public bool playCommonHitSe;
  [Tooltip("ヒットSE ID")]
  public int hitSEID;
  [Tooltip("共通Effectを再生するか")]
  public bool playCommonHitEffect;
  [Tooltip("継続して残るエフェクト名(属性差分がある場合はエンジニアに相談してください")]
  public string remainEffectName;
  [Tooltip("HP吸収率 Playerは与えたダメージの割合,敵は最大HPの割合")]
  public float absorptance;
  [Tooltip("即死攻撃かどうか")]
  public bool isImmediateDeath;
  [Tooltip("掴み状態にダメージを与えるかどうか")]
  public bool canAttackGrabbedPlayer;
  [Tooltip("外からのダメージのオフセット用")]
  public int damageNumAddGroup;
  [Tooltip("この攻撃ではゲージ増えない")]
  public bool dontIncreaseGauge;
  [Tooltip("この攻撃で破壊可能オブジェクトを壊せるか")]
  public bool isBreakObject;
  [Tooltip("攻撃タイプ（特殊な攻撃指定用")]
  public AttackHitInfo.ATTACK_TYPE attackType;
  [Tooltip("属性 (攻撃力で指定できない場合に使用)")]
  public ELEMENT_TYPE elementType = ELEMENT_TYPE.MAX;
  [Tooltip("武器SPアクションタイプ")]
  public SP_ATTACK_TYPE spAttackType;
  [Tooltip("ダメージアップ処理用ラベル設定（複数設定可）")]
  public string[] damageUpLabels;
  public AttackHitInfo.ToPlayer toPlayer = new AttackHitInfo.ToPlayer();
  public AttackHitInfo.ToEnemy toEnemy = new AttackHitInfo.ToEnemy();
  public GrabInfo grabInfo = new GrabInfo();
  public RestraintInfo restraintInfo = new RestraintInfo();
  public ElectricShockInfo electricShockInfo = new ElectricShockInfo();
  public InkSplashInfo inkSplashInfo = new InkSplashInfo();
  public ElectricShockInfo soilShockInfo = new ElectricShockInfo();
  public uint[] buffIDs;
  [Tooltip("バリアに対する攻撃力")]
  public int toShieldAtk;
  [Tooltip("バリアに対するクリティカル時のレート")]
  public float toShieldCriticalRate;
  [Tooltip("バリアに対する属性のクリティカル時のレート")]
  public float toShieldElementCriticalRate;
  [Tooltip("ヒット時に回復する割合")]
  public float hitHealRate;

  public override AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
  {
    AttackHitInfo rateAttackInfo = base.GetRateAttackInfo(rate_info, rate) as AttackHitInfo;
    if (rateAttackInfo == this)
      return (AttackInfo) rateAttackInfo;
    if (!(rate_info is AttackHitInfo attackHitInfo))
      return (AttackInfo) rateAttackInfo;
    rateAttackInfo.atkRate = AttackInfo.GetRateValue(this.atkRate, attackHitInfo.atkRate, rate);
    rateAttackInfo.atk.normal = AttackInfo.GetRateValue(this.atk.normal, attackHitInfo.atk.normal, rate);
    rateAttackInfo.atk.fire = AttackInfo.GetRateValue(this.atk.fire, attackHitInfo.atk.fire, rate);
    rateAttackInfo.atk.water = AttackInfo.GetRateValue(this.atk.water, attackHitInfo.atk.water, rate);
    rateAttackInfo.atk.thunder = AttackInfo.GetRateValue(this.atk.thunder, attackHitInfo.atk.thunder, rate);
    rateAttackInfo.atk.soil = AttackInfo.GetRateValue(this.atk.soil, attackHitInfo.atk.soil, rate);
    rateAttackInfo.atk.light = AttackInfo.GetRateValue(this.atk.light, attackHitInfo.atk.light, rate);
    rateAttackInfo.atk.dark = AttackInfo.GetRateValue(this.atk.dark, attackHitInfo.atk.dark, rate);
    rateAttackInfo.isAtkIgnore = this.isAtkIgnore;
    rateAttackInfo.down = AttackInfo.GetRateValue(this.down, attackHitInfo.down, rate);
    rateAttackInfo.isForceDown = AttackInfo.GetRateValue(this.isForceDown, attackHitInfo.isForceDown, rate);
    rateAttackInfo.badStatus.paralyze = AttackInfo.GetRateValue(this.badStatus.paralyze, attackHitInfo.badStatus.paralyze, rate);
    rateAttackInfo.badStatus.poison = AttackInfo.GetRateValue(this.badStatus.poison, attackHitInfo.badStatus.poison, rate);
    rateAttackInfo.badStatus.burning = AttackInfo.GetRateValue(this.badStatus.burning, attackHitInfo.badStatus.burning, rate);
    rateAttackInfo.badStatus.speedDown = AttackInfo.GetRateValue(this.badStatus.speedDown, attackHitInfo.badStatus.speedDown, rate);
    rateAttackInfo.badStatus.attackSpeedDown = AttackInfo.GetRateValue(this.badStatus.attackSpeedDown, attackHitInfo.badStatus.attackSpeedDown, rate);
    rateAttackInfo.badStatus.lightRing = AttackInfo.GetRateValue(this.badStatus.lightRing, attackHitInfo.badStatus.lightRing, rate);
    rateAttackInfo.badStatus.erosion = AttackInfo.GetRateValue(this.badStatus.erosion, attackHitInfo.badStatus.erosion, rate);
    rateAttackInfo.badStatus.stone = AttackInfo.GetRateValue(this.badStatus.stone, attackHitInfo.badStatus.stone, rate);
    rateAttackInfo.shakeCameraPercent = AttackInfo.GetRateValue(this.shakeCameraPercent, attackHitInfo.shakeCameraPercent, rate);
    rateAttackInfo.shakeCycleTime = AttackInfo.GetRateValue(this.shakeCycleTime, attackHitInfo.shakeCycleTime, rate);
    rateAttackInfo.enableIdentityCheck = this.enableIdentityCheck;
    rateAttackInfo.hitEffectName = (double) rate < 1.0 ? this.hitEffectName : attackHitInfo.hitEffectName;
    rateAttackInfo.playCommonHitSe = (double) rate < 1.0 ? this.playCommonHitSe : attackHitInfo.playCommonHitSe;
    rateAttackInfo.hitSEID = (double) rate < 1.0 ? this.hitSEID : attackHitInfo.hitSEID;
    rateAttackInfo.playCommonHitEffect = (double) rate < 1.0 ? this.playCommonHitEffect : attackHitInfo.playCommonHitEffect;
    rateAttackInfo.remainEffectName = (double) rate < 1.0 ? this.remainEffectName : attackHitInfo.remainEffectName;
    rateAttackInfo.damageNumAddGroup = this.damageNumAddGroup;
    rateAttackInfo.dontIncreaseGauge = this.dontIncreaseGauge;
    rateAttackInfo.isBreakObject = this.isBreakObject;
    rateAttackInfo.attackType = (double) rate < 1.0 ? this.attackType : attackHitInfo.attackType;
    rateAttackInfo.spAttackType = (double) rate < 1.0 ? this.spAttackType : attackHitInfo.spAttackType;
    rateAttackInfo.hitIntervalTime = AttackInfo.GetRateValue(this.hitIntervalTime, attackHitInfo.hitIntervalTime, rate);
    rateAttackInfo.enableIdentityCheck = (double) rate <= 0.0 ? this.enableIdentityCheck : attackHitInfo.enableIdentityCheck;
    rateAttackInfo.isValidTriggerStay = (double) rate <= 0.0 ? this.isValidTriggerStay : attackHitInfo.isValidTriggerStay;
    rateAttackInfo.toPlayer.reactionType = (double) rate < 1.0 ? this.toPlayer.reactionType : attackHitInfo.toPlayer.reactionType;
    rateAttackInfo.toPlayer.reactionLoopTime = AttackInfo.GetRateValue(this.toPlayer.reactionLoopTime, attackHitInfo.toPlayer.reactionLoopTime, rate);
    rateAttackInfo.toPlayer.reactionBlowForce = AttackInfo.GetRateValue(this.toPlayer.reactionBlowForce, attackHitInfo.toPlayer.reactionBlowForce, rate);
    rateAttackInfo.toPlayer.reactionBlowAngle = AttackInfo.GetRateValue(this.toPlayer.reactionBlowAngle, attackHitInfo.toPlayer.reactionBlowAngle, rate);
    rateAttackInfo.toEnemy.reactionType = (double) rate < 1.0 ? this.toEnemy.reactionType : attackHitInfo.toEnemy.reactionType;
    rateAttackInfo.toEnemy.hitStopTime = AttackInfo.GetRateValue(this.toEnemy.hitStopTime, attackHitInfo.toEnemy.hitStopTime, rate);
    rateAttackInfo.toEnemy.enemyHitStopTime = AttackInfo.GetRateValue(this.toEnemy.enemyHitStopTime, attackHitInfo.toEnemy.enemyHitStopTime, rate);
    rateAttackInfo.toEnemy.hitTypeName = (double) rate < 1.0 ? this.toEnemy.hitTypeName : attackHitInfo.toEnemy.hitTypeName;
    rateAttackInfo.toEnemy.isSpecialAttack = (double) rate < 1.0 ? this.toEnemy.isSpecialAttack : attackHitInfo.toEnemy.isSpecialAttack;
    rateAttackInfo.toEnemy.concussion = AttackInfo.GetRateValue(this.toEnemy.concussion, attackHitInfo.toEnemy.concussion, rate);
    string[] strArray = (double) rate < 1.0 ? this.damageUpLabels : attackHitInfo.damageUpLabels;
    if (strArray != null)
    {
      int length = strArray.Length;
      rateAttackInfo.damageUpLabels = new string[length];
      if (length > 0)
        strArray.CopyTo((Array) rateAttackInfo.damageUpLabels, 0);
    }
    return (AttackInfo) rateAttackInfo;
  }

  protected override AttackInfo CreateInfo() => (AttackInfo) new AttackHitInfo();

  public override void Copy(ref AttackInfo rInfo)
  {
    base.Copy(ref rInfo);
    if (!(rInfo is AttackHitInfo attackHitInfo))
      return;
    attackHitInfo.atkRate = this.atkRate;
    attackHitInfo.isAtkIgnore = this.isAtkIgnore;
    attackHitInfo.down = this.down;
    attackHitInfo.isForceDown = this.isForceDown;
    attackHitInfo.shakeCameraPercent = this.shakeCameraPercent;
    attackHitInfo.shakeCycleTime = this.shakeCycleTime;
    attackHitInfo.enableIdentityCheck = this.enableIdentityCheck;
    attackHitInfo.isValidTriggerStay = this.isValidTriggerStay;
    attackHitInfo.hitIntervalTime = this.hitIntervalTime;
    attackHitInfo.hitEffectName = this.hitEffectName;
    attackHitInfo.playCommonHitSe = this.playCommonHitSe;
    attackHitInfo.hitSEID = this.hitSEID;
    attackHitInfo.playCommonHitEffect = this.playCommonHitEffect;
    attackHitInfo.remainEffectName = this.remainEffectName;
    attackHitInfo.absorptance = this.absorptance;
    attackHitInfo.isImmediateDeath = this.isImmediateDeath;
    attackHitInfo.canAttackGrabbedPlayer = this.canAttackGrabbedPlayer;
    attackHitInfo.damageNumAddGroup = this.damageNumAddGroup;
    attackHitInfo.dontIncreaseGauge = this.dontIncreaseGauge;
    attackHitInfo.isBreakObject = this.isBreakObject;
    attackHitInfo.attackType = this.attackType;
    attackHitInfo.elementType = this.elementType;
    attackHitInfo.spAttackType = this.spAttackType;
    attackHitInfo.toShieldAtk = this.toShieldAtk;
    attackHitInfo.toShieldCriticalRate = this.toShieldCriticalRate;
    attackHitInfo.toShieldElementCriticalRate = this.toShieldElementCriticalRate;
    attackHitInfo.hitHealRate = this.hitHealRate;
    attackHitInfo.atk.Copy(this.atk);
    attackHitInfo.badStatus.Copy(this.badStatus);
    attackHitInfo.toPlayer.Copy(this.toPlayer);
    attackHitInfo.toEnemy.Copy(this.toEnemy);
    attackHitInfo.grabInfo.Copy(this.grabInfo);
    attackHitInfo.restraintInfo.Copy(this.restraintInfo);
    attackHitInfo.electricShockInfo.Copy(this.electricShockInfo);
    attackHitInfo.soilShockInfo.Copy(this.soilShockInfo);
    attackHitInfo.inkSplashInfo.Copy(this.inkSplashInfo);
    if (this.damageUpLabels != null)
    {
      int length = this.damageUpLabels.Length;
      attackHitInfo.damageUpLabels = new string[length];
      if (length > 0)
        this.damageUpLabels.CopyTo((Array) attackHitInfo.damageUpLabels, 0);
    }
    if (this.buffIDs == null)
      return;
    int length1 = this.buffIDs.Length;
    attackHitInfo.buffIDs = new uint[this.buffIDs.Length];
    if (length1 <= 0)
      return;
    this.buffIDs.CopyTo((Array) attackHitInfo.buffIDs, 0);
  }

  public bool IsReferenceAtkValue => !this.isAtkIgnore;

  public enum ATTACK_TYPE
  {
    NORMAL,
    SOUNDWAVE,
    TWO_HAND_SWORD_SP,
    HEAL_ATTACK,
    SPEAR_SP,
    CANNON_BALL,
    BOMBROCK,
    JUMP,
    FROM_AVOID,
    COUNTER,
    COUNTER2,
    REVENGE,
    CANNON_BALL_DIRECT,
    THS_HEAT_COMBO,
    SNATCH,
    BURST_THS_COMBO03,
    BURST_THS_SINGLE_SHOT,
    BURST_THS_FULL_BURST,
    GIMMICK_GENERATED,
    COUNTER_BURST,
    JUSTGUARD_ATTACK,
    BURST_SPEAR_COMBO3,
    SHIELD_REFLECT,
    BOOST_BOMB_ARROW,
    BOMB,
    BOOST_ARROW_RAIN,
    ARROW_RAIN,
    THS_ORACLE_HORIZONTAL,
    THS_ORACLE_CHARGE,
    THS_ORACLE_CHARGE_MAX,
    BOOST_BOMB,
    OHS_ORACLE_SP,
    SPEAR_ORACLE_SP,
    SPEAR_ORACLE_SP_CHARGED,
    PAIR_SWORDS_ORACLE_RUSH,
    PAIR_SWORDS_ORACLE_RUSH_BOOST,
    PAIR_SWORDS_ORACLE_SP,
  }

  [Serializable]
  public class ToPlayer
  {
    [Tooltip("リアクションタイプ")]
    public AttackHitInfo.ToPlayer.REACTION_TYPE reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.DAMAGE;
    [Tooltip("リアクション時間（秒")]
    public float reactionLoopTime;
    [Tooltip("吹き飛ばし力")]
    public float reactionBlowForce;
    [Tooltip("吹き飛ばし角度")]
    public float reactionBlowAngle;
    [Tooltip("Buff解除")]
    public bool isBuffCancellation;
    [Tooltip("パリィ・カウンター不可")]
    public bool disableCounter;
    [Tooltip("ガード不可")]
    public bool disableGuard;

    public void Copy(AttackHitInfo.ToPlayer src)
    {
      this.reactionType = src.reactionType;
      this.reactionLoopTime = src.reactionLoopTime;
      this.reactionBlowForce = src.reactionBlowForce;
      this.reactionBlowAngle = src.reactionBlowAngle;
      this.isBuffCancellation = src.isBuffCancellation;
      this.disableCounter = src.disableCounter;
      this.disableGuard = src.disableGuard;
    }

    public enum REACTION_TYPE
    {
      NONE,
      DAMAGE,
      BLOW,
      STUNNED_BLOW,
      STUMBLE,
      ___,
      FALL_BLOW,
      SHAKE,
      CHARM_BLOW,
    }
  }

  [Serializable]
  public class ToEnemy
  {
    [Tooltip("リアクションタイプ")]
    public AttackHitInfo.ToEnemy.REACTION_TYPE reactionType;
    [Tooltip("ヒットストップ時間（プレイヤー）")]
    public float hitStopTime;
    [Tooltip("ヒットストップ時間（モンスター）")]
    public float enemyHitStopTime;
    [Tooltip("ヒットタイプ名(EnemyHitTypeTable)")]
    public string hitTypeName;
    [Tooltip("武器固有攻撃")]
    public bool isSpecialAttack;
    [Tooltip("WEAKに当たったときに怯む")]
    public bool isWeakHitReaction;
    [Tooltip("脳震盪蓄積値(WEAKヒット時)")]
    public float concussion;
    [Tooltip("部位へのダメージアップ情報")]
    public AttackHitInfo.ToEnemy.DamageToRegionInfo damageToRegionInfo = new AttackHitInfo.ToEnemy.DamageToRegionInfo();
    [Tooltip("リアクションの情報")]
    public AttackHitInfo.ToEnemy.ReactionInfo reactionInfo = new AttackHitInfo.ToEnemy.ReactionInfo();
    [Tooltip("イージスへのダメージ上昇")]
    public float aegisDamageRate = 1f;

    public void Copy(AttackHitInfo.ToEnemy src)
    {
      this.reactionType = src.reactionType;
      this.hitStopTime = src.hitStopTime;
      this.hitTypeName = src.hitTypeName;
      this.isSpecialAttack = src.isSpecialAttack;
      this.damageToRegionInfo = src.damageToRegionInfo;
      this.reactionInfo = src.reactionInfo;
      this.aegisDamageRate = src.aegisDamageRate;
    }

    public enum REACTION_TYPE
    {
      NONE,
      DAMAGE,
      DOWN,
      BIND,
    }

    [Serializable]
    public class DamageToRegionInfo
    {
      [Tooltip("部位へのダメージアップするか")]
      public bool isDamageUp;
      [Tooltip("部位へのダメージアップ倍率（N%アップ）")]
      public int damageUpPercent;
      [Tooltip("竜装へのダメージアップ倍率")]
      public float dragonArmorDamageRate = 1f;
      [Tooltip("黒霧(バリア)へのダメージアップ倍率")]
      public float barrierDamageRate = 1f;
      [Tooltip("弱点属性防御力無視")]
      public bool ignoreWeakElementDefence;
    }

    [Serializable]
    public class ReactionInfo
    {
      [Tooltip("リアクション時間（秒）")]
      public float reactionLoopTime;
    }
  }
}
