// Decompiled with JetBrains decompiler
// Type: EnemyRegionWork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyRegionWork
{
  public Enemy.RegionInfo regionInfo;
  public List<Enemy.BleedData> bleedList = new List<Enemy.BleedData>();
  public List<Enemy.BleedWork> bleedWorkList = new List<Enemy.BleedWork>();
  public List<int> weakAttackIDs = new List<int>();
  public Enemy.WEAK_STATE weakState;
  public string deleteAtkName = string.Empty;
  public XorInt hp = (XorInt) 10;
  public int weakSubParam = -1;
  public int parentRegionID = -1;
  public float breakTime = -1f;
  public float displayTimer;
  public bool isBroke;
  public bool enabled = true;
  public int NumOfAttackedByMagi;
  public int validElementType = -1;
  public int regionId;
  public bool isShieldDamage;
  public bool isShieldCriticalDamage;
  public Enemy.ShadowSealingData shadowSealingData = new Enemy.ShadowSealingData();
  public Transform shadowSealingEffect;
  public List<Enemy.BombArrowData> bombArrowDataHistory = new List<Enemy.BombArrowData>();
  public Transform bombArrowEffect;

  public void Initialize(Enemy.RegionInfo info, int parentRegionId, int _regionId)
  {
    this.regionInfo = info;
    this.hp = (XorInt) info.maxHP;
    this.isShieldDamage = info.isEnableShieldDamage;
    this.regionId = _regionId;
    if (parentRegionId <= 0)
      return;
    this.parentRegionID = parentRegionId;
    this.enabled = false;
  }

  public void Update()
  {
    if ((double) this.displayTimer <= 0.0)
      return;
    this.displayTimer -= Time.deltaTime;
    if ((double) this.displayTimer > 0.0)
      return;
    this.ResetWeakState();
  }

  public void ResetWeakState()
  {
    this.weakState = Enemy.WEAK_STATE.NONE;
    this.weakSubParam = -1;
    this.displayTimer = 0.0f;
    this.deleteAtkName = string.Empty;
    this.validElementType = -1;
  }

  public void SetupWeakPoint(
    int weakType,
    Player.ATTACK_MODE attackMode,
    float displayTime = 0.0f,
    string deleteAttackName = "",
    int validElement = -1)
  {
    this.displayTimer = displayTime;
    this.deleteAtkName = deleteAttackName;
    this.validElementType = validElement;
    switch (weakType)
    {
      case 0:
        this.weakState = Enemy.WEAK_STATE.WEAK;
        break;
      case 1:
        this.weakState = Enemy.WEAK_STATE.DOWN;
        break;
      case 2:
        this.weakState = Enemy.WEAK_STATE.WEAK_SP_ATTACK;
        break;
      case 3:
        this.weakState = Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX;
        break;
      case 5:
        this.weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK;
        break;
      case 6:
        this.weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK;
        break;
      case 7:
        this.weakState = Enemy.WEAK_STATE.WEAK_SKILL_ATTACK;
        break;
      case 8:
        this.weakState = Enemy.WEAK_STATE.WEAK_HEAL_ATTACK;
        break;
      case 9:
        this.weakState = Enemy.WEAK_STATE.WEAK_GRAB;
        break;
      case 10:
        this.weakState = Enemy.WEAK_STATE.WEAK_CANNON;
        break;
      case 11:
        this.weakState = Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK;
        break;
    }
    if (attackMode != Player.ATTACK_MODE.NONE)
      this.weakSubParam = (int) attackMode;
    if (Enemy.IsWeakStateSpAttack(this.weakState) && this.weakSubParam < 0)
      this.ResetWeakState();
    this.weakAttackIDs.Clear();
  }

  public void OnReviveRegion(int reviveRegionId)
  {
    if (this.parentRegionID < 0 || this.parentRegionID != reviveRegionId)
      return;
    this.enabled = false;
  }

  public void OnBreakRegion(int breakRegionId)
  {
    if (this.parentRegionID < 0 || this.parentRegionID != breakRegionId)
      return;
    this.enabled = true;
  }

  public float GetBarrierToleranceRate() => this.regionInfo.barrierToleranceRate;

  public bool IsValidDisplayTimer => (double) this.displayTimer > 0.0;

  public void CopyFrom(EnemyRegionWork regionWork)
  {
    this.hp = regionWork.hp;
    this.breakTime = regionWork.breakTime;
    this.isBroke = regionWork.isBroke;
    this.isShieldCriticalDamage = regionWork.isShieldCriticalDamage;
    this.isShieldDamage = regionWork.isShieldDamage;
  }

  public bool IsBombArrowLevelMax()
  {
    return this.bombArrowDataHistory.Count >= MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowMaxLevel;
  }

  public void StackBombArrow(Enemy.BombArrowData data)
  {
    if (this.IsBombArrowLevelMax())
      return;
    this.bombArrowDataHistory.Add(data);
  }

  public Enemy.BombArrowData GetBombArrowData()
  {
    return this.bombArrowDataHistory.Count > 0 ? this.bombArrowDataHistory[this.bombArrowDataHistory.Count - 1] : (Enemy.BombArrowData) null;
  }
}
