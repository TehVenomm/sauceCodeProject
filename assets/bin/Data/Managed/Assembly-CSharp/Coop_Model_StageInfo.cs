// Decompiled with JetBrains decompiler
// Type: Coop_Model_StageInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Coop_Model_StageInfo : Coop_Model_Base
{
  public float elapsedTime;
  public List<Coop_Model_StageInfo.GimmickInfo> gimmicks = new List<Coop_Model_StageInfo.GimmickInfo>();
  public List<Coop_Model_StageInfo.FieldCarriableGimmickInfo> carriableGimmickInfos = new List<Coop_Model_StageInfo.FieldCarriableGimmickInfo>();
  public List<Coop_Model_StageInfo.FieldSupplyGimmickInfo> supplyGimmickInfos = new List<Coop_Model_StageInfo.FieldSupplyGimmickInfo>();
  public Vector3 enemyPos = Vector3.zero;
  public float rushLimitTime = -1f;
  public bool isInFieldEnemyBossBattle;
  public bool isInFieldFishingEnemyBattle;
  public List<Coop_Model_StageInfo.WaveTargetInfo> waveTargets = new List<Coop_Model_StageInfo.WaveTargetInfo>();
  public Coop_Model_WaveMatchInfo firstWaveMatchInfo;
  public float firstWaveMatchPopSec;

  public Coop_Model_StageInfo() => this.packetType = PACKET_TYPE.STAGE_INFO;

  [Serializable]
  public class GimmickInfo
  {
    public int id;
    public bool enable;
  }

  public class WaveTargetInfo
  {
    public int id;
    public int hp;
  }

  public class FieldCarriableGimmickInfo
  {
    public int pointId;
    public bool enable;
    public int currentLv;
    public Vector3 position;
  }

  public class FieldSupplyGimmickInfo
  {
    public int pointId;
    public bool canUse;
    public int suppliedCount;
  }
}
