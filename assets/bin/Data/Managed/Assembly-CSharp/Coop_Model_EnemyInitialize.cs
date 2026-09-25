// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyInitialize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Coop_Model_EnemyInitialize : Coop_Model_ObjectSyncPositionBase
{
  public int sid;
  public int hp;
  public int hpMax;
  public float hpDamageRate;
  public float downTotal;
  public int downCount;
  public float concussionTotal;
  public float concussionMax;
  public float concussionExtend;
  public BadStatus badStatusMax = new BadStatus();
  public List<Enemy.RegionWorkSyncData> regions;
  public int target_id;
  public BuffParam.BuffSyncParam buff_sync_param;
  public uint nowAngryId;
  public List<uint> execAngryIds = new List<uint>();
  public ContinusAttackParam.SyncParam cntAtkSyncParam = new ContinusAttackParam.SyncParam();
  public int barrierHp;
  public bool isHiding;
  public int shieldHp;
  public int grabHp;
  public EnemyAegisController.SetupParam aegisSetupParam;
  public Vector3[] tailPosList;
  public int bulletIndex;
  public float walkSpeedRateFromTable = 1f;
  public ELEMENT_TYPE changeElementIcon = ELEMENT_TYPE.MAX;
  public ELEMENT_TYPE changeWeakElementIcon = ELEMENT_TYPE.MAX;
  public int changeToleranceRegionId = -1;
  public int changeToleranceScroll = -1;
  public List<BlendColorCtrl.ShaderSyncParam> shaderSyncParam;
  public int deadReviveCount;
  public bool isFirstMadMode;
  public int recoveredHP;

  public Coop_Model_EnemyInitialize() => this.packetType = PACKET_TYPE.ENEMY_INITIALIZE;

  public override bool IsPromiseOverAgainCheck() => true;

  public override bool IsForceHandleBefore(StageObject owner) => true;

  public void SetRegionWorks(EnemyRegionWork[] region_works)
  {
    if (region_works == null)
      return;
    this.regions = new List<Enemy.RegionWorkSyncData>();
    int index = 0;
    for (int length = region_works.Length; index < length; ++index)
    {
      Enemy.RegionWorkSyncData regionWorkSyncData = new Enemy.RegionWorkSyncData();
      EnemyRegionWork regionWork = region_works[index];
      regionWorkSyncData.hp = (int) regionWork.hp;
      regionWorkSyncData.isBroke = regionWork.isBroke;
      regionWorkSyncData.bleedList = regionWork.bleedList;
      regionWorkSyncData.isShieldDamage = regionWork.isShieldDamage;
      regionWorkSyncData.isShieldCriticalDamage = regionWork.isShieldCriticalDamage;
      regionWorkSyncData.shadowSealingData = regionWork.shadowSealingData;
      regionWorkSyncData.bombArrowDataHistory = regionWork.bombArrowDataHistory;
      this.regions.Add(regionWorkSyncData);
    }
  }
}
