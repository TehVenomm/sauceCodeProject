// Decompiled with JetBrains decompiler
// Type: TargetMarkerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TargetMarkerManager : MonoBehaviourSingleton<TargetMarkerManager>
{
  public const int TARGET_INFO_MAX = 80 /*0x50*/;
  private TargetMarkerManager.Function m_func;
  private List<TargetMarker> markers;
  private List<TargetMarker> markersTemp;
  private List<TargetMarker.UpdateParam> paramListPool;
  private List<TargetMarker.UpdateParam> paramList;
  private Dictionary<TargetPoint, MultiLockMarker> fieldMultiLockDic;
  private float targetingTime;
  private float targetingWeakTime;
  protected bool changeLockFlag;
  protected float changeLockTime;
  private int numTargetInfo;
  private List<TargetMarkerManager.TARGET_INFO> targetInfoList = new List<TargetMarkerManager.TARGET_INFO>();
  private TargetMarker m_cannonCriticalMarker;
  private TargetMarker m_grabMarker;
  private UIGrabStatusGizmo uiGrabStatusGizmo;
  public bool updateShadowSealingFlag;

  public InGameSettingsManager.TargetMarker parameter { get; protected set; }

  public TargetPoint targetingPoint { get; protected set; }

  public bool showMarker { get; set; }

  public bool isTargetLock { get; protected set; }

  public bool isTargetDisable { get; protected set; }

  public TargetMarkerManager() => this.isTargetLock = false;

  private void Start()
  {
    this.showMarker = true;
    this.markers = new List<TargetMarker>();
    this.markersTemp = new List<TargetMarker>();
    this.paramListPool = new List<TargetMarker.UpdateParam>();
    this.paramList = new List<TargetMarker.UpdateParam>();
    this.fieldMultiLockDic = new Dictionary<TargetPoint, MultiLockMarker>();
    for (int index = 0; index < 80 /*0x50*/; ++index)
      this.targetInfoList.Add(new TargetMarkerManager.TARGET_INFO()
      {
        enemy = (Enemy) null,
        targetPoint = (TargetPoint) null
      });
  }

  private void RegisterTargetInfo(TargetPoint targetPoint, Enemy enemy = null)
  {
    if (this.targetInfoList.Count <= this.numTargetInfo)
      return;
    this.targetInfoList[this.numTargetInfo].targetPoint = targetPoint;
    this.targetInfoList[this.numTargetInfo].enemy = enemy;
    ++this.numTargetInfo;
  }

  private void LateUpdate()
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
    {
      this.Clear();
    }
    else
    {
      TargetMarkerManager.Function func = this.m_func;
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      TargetMarkerManager.Function nextFunc = !self.IsOnCannonMode() || !Object.op_Inequality((Object) boss, (Object) null) || !boss.IsValidShield() ? TargetMarkerManager.Function.Basis : TargetMarkerManager.Function.Cannon;
      if (nextFunc != this.m_func)
        this.OnChangeFunction(nextFunc);
      this.FuncCommon();
      switch (this.m_func)
      {
        case TargetMarkerManager.Function.Basis:
          this.FuncBasisMode();
          break;
        case TargetMarkerManager.Function.Cannon:
          this.FuncCannonMode();
          break;
      }
    }
  }

  private void OnChangeFunction(TargetMarkerManager.Function nextFunc)
  {
    switch (this.m_func)
    {
      case TargetMarkerManager.Function.Basis:
        this.Clear();
        this.markersTemp.Clear();
        break;
      case TargetMarkerManager.Function.Cannon:
        if (this.m_cannonCriticalMarker != null)
          this.m_cannonCriticalMarker.UnableMarker();
        this.UnableGrabMarker();
        break;
    }
    this.m_func = nextFunc;
  }

  private void FuncBasisMode()
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    bool canTargetBoss = StageObjectManager.CanTargetBoss;
    this.parameter = !canTargetBoss ? (!self.isArrowRainShot ? (!StageObjectManager.IsBossAssimilated ? MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerLesserEnemies : MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerEnemyAssimilated) : MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerArrowRainAimLesser) : MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarker;
    if (this.changeLockFlag)
    {
      this.changeLockTime -= Time.deltaTime;
      if ((double) this.changeLockTime <= 0.0)
      {
        this.changeLockTime = 0.0f;
        this.changeLockFlag = false;
        this.isTargetLock = !this.isTargetLock;
      }
    }
    bool flag1 = false;
    Vector3 vector3_1 = self._position;
    if (self.isArrowAimLesserMode)
    {
      if (self.isArrowAimEnd)
        flag1 = true;
      else if (self.isArrowRainShot && !canTargetBoss)
        vector3_1 = Vector3.op_Addition(vector3_1, self.arrowAimLesserCursorPos);
      else if (!self.isArrowRainShot)
      {
        this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerArrowAimLesser;
        vector3_1 = Vector3.op_Addition(vector3_1, self.arrowAimLesserCursorPos);
      }
    }
    float num1 = this.parameter.targetDistance;
    float num2 = this.parameter.showTargetDistance;
    if (self.CheckAttackMode(Player.ATTACK_MODE.ARROW))
    {
      num1 = this.parameter.targetDistanceArrow;
      num2 = this.parameter.showTargetDistanceArrow;
    }
    TargetPoint targetingPoint = self.targetingPoint;
    self.targetingPointList.Clear();
    self.targetPointWithSpWeakList.Clear();
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; index1 < count; ++index1)
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList[index1];
      if (enemy.HasValidTargetPoint())
      {
        for (int index2 = 0; index2 < enemy.targetPoints.Length; ++index2)
          this.RegisterTargetInfo(enemy.targetPoints[index2], enemy);
      }
      List<IBulletObservable> bulletObservableList = enemy.GetBulletObservableList();
      if (bulletObservableList != null)
      {
        for (int index3 = 0; index3 < bulletObservableList.Count; ++index3)
        {
          AnimEventShot animEventShot = bulletObservableList[index3] as AnimEventShot;
          if (Object.op_Inequality((Object) animEventShot, (Object) null) && Object.op_Inequality((Object) animEventShot.targetPoint, (Object) null))
          {
            this.RegisterTargetInfo(animEventShot.targetPoint);
          }
          else
          {
            AttackFunnelBit attackFunnelBit = bulletObservableList[index3] as AttackFunnelBit;
            if (Object.op_Inequality((Object) attackFunnelBit, (Object) null) && Object.op_Inequality((Object) attackFunnelBit.targetPoint, (Object) null))
              this.RegisterTargetInfo(attackFunnelBit.targetPoint);
          }
        }
      }
    }
    for (int index4 = 0; index4 < MonoBehaviourSingleton<InGameManager>.I.dropItemList.Count; ++index4)
    {
      FieldDropObject dropItem = MonoBehaviourSingleton<InGameManager>.I.dropItemList[index4];
      if (Object.op_Inequality((Object) dropItem.targetPoint, (Object) null) && ((Component) dropItem).gameObject.activeInHierarchy)
        this.RegisterTargetInfo(dropItem.targetPoint);
    }
    int count1 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count;
    for (int index5 = 0; index5 < count1; ++index5)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index5] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && !player.isDead)
      {
        TargetPoint restraintTargetPoint = player.RestraintTargetPoint;
        if (Object.op_Inequality((Object) restraintTargetPoint, (Object) null))
          this.RegisterTargetInfo(restraintTargetPoint);
      }
    }
    if (this.numTargetInfo <= 0)
    {
      this.Clear();
      self.SetActionTarget((StageObject) null);
    }
    else
    {
      float num3 = this.parameter.showAngle * ((float) Math.PI / 180f);
      float num4 = num1 * num1;
      float num5 = num2 * num2;
      float num6 = this.parameter.targetAngle * ((float) Math.PI / 180f);
      TargetPoint nextTargetPoint1 = (TargetPoint) null;
      float num7 = float.MaxValue;
      TargetPoint targetPoint1 = (TargetPoint) null;
      float num8 = float.MaxValue;
      TargetPoint nextTargetPoint2 = (TargetPoint) null;
      float num9 = float.MaxValue;
      Vector2 vector2Xz1 = vector3_1.ToVector2XZ();
      Vector2 forwardXz = self.forwardXZ;
      ((Vector2) ref forwardXz).Normalize();
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 position = cameraTransform.position;
      Vector2 vector2Xz2 = position.ToVector2XZ();
      Vector3 vector3_2 = Vector2.op_Implicit(cameraTransform.forward.ToVector2XZ());
      ((Vector3) ref vector3_2).Normalize();
      bool isAutoMode = self.isAutoMode;
      bool flag2 = self.isArrowAimBossMode || self.isArrowRainShot & canTargetBoss;
      for (int index6 = 0; index6 < this.numTargetInfo; ++index6)
      {
        Enemy enemy = this.targetInfoList[index6].enemy;
        TargetPoint targetPoint2 = this.targetInfoList[index6].targetPoint;
        TargetPoint.Param obj1 = targetPoint2.param;
        obj1.isShowRange = false;
        obj1.isTargetEnable = false;
        obj1.weakState = Enemy.WEAK_STATE.NONE;
        obj1.weakSubParam = -1;
        if (((Behaviour) targetPoint2).enabled && ((Component) targetPoint2).gameObject.activeInHierarchy)
        {
          if (flag2)
          {
            if (!self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) && !targetPoint2.isAimEnable && !targetPoint2.isDispArrowSpWeak)
              continue;
          }
          else if (!targetPoint2.isTargetEnable)
            continue;
          if (Object.op_Inequality((Object) enemy, (Object) null) && targetPoint2.regionID >= 0 && targetPoint2.regionID < enemy.regionWorks.Length)
          {
            EnemyRegionWork regionWork = enemy.regionWorks[targetPoint2.regionID];
            if (regionWork.enabled)
            {
              if (!flag2 || Enemy.IsWeakStateDisplaySign(regionWork.weakState))
              {
                obj1.weakState = regionWork.weakState;
                obj1.weakSubParam = regionWork.weakSubParam;
                obj1.validElementType = regionWork.validElementType;
                if (Enemy.IsWeakStateCheckAlreadyHit(obj1.weakState) && regionWork.weakAttackIDs.Contains(self.id))
                  obj1.weakState = Enemy.WEAK_STATE.NONE;
              }
              obj1.aimMarkerScale = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy.aimMarkerBaseRate * enemy.enemyTableData.aimMarkerRate * targetPoint2.aimMarkerPointRate;
            }
            else
              continue;
          }
          if (!flag2 || !targetPoint2.isDispArrowSpWeak || (obj1.weakState == Enemy.WEAK_STATE.WEAK_SP_ATTACK || obj1.weakState == Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK) && obj1.weakSubParam == 5)
          {
            obj1.isTargetEnable = true;
            Vector3 targetPoint3 = targetPoint2.GetTargetPoint();
            Vector2 vector2Xz3 = targetPoint3.ToVector2XZ();
            Vector2 vector2_1 = Vector2.op_Subtraction(vector2Xz3, vector2Xz1);
            TargetPoint.Param obj2 = obj1;
            Vector3 vector3_3 = Vector3.op_Subtraction(position, targetPoint3);
            Vector3 vector3_4 = Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3_3).normalized, targetPoint2.scaledMarkerZShift), targetPoint3);
            obj2.markerPos = vector3_4;
            obj1.markerRot = rotation;
            obj1.targetPos = targetPoint3;
            bool flag3 = false;
            if (obj1.weakState != Enemy.WEAK_STATE.NONE && obj1.weakSubParam != 0)
              flag3 = true;
            float sqrMagnitude = ((Vector2) ref vector2_1).sqrMagnitude;
            obj1.isShowRange = (double) sqrMagnitude < (double) num5 | flag3;
            obj1.vecSqrMagnitude = sqrMagnitude;
            if (!this.isTargetDisable)
            {
              if (isAutoMode && !(targetPoint2.owner is Player))
              {
                if (Object.op_Equality((Object) nextTargetPoint2, (Object) null))
                {
                  nextTargetPoint2 = targetPoint2;
                  num9 = sqrMagnitude;
                }
                else if (Object.op_Equality((Object) targetPoint2.owner, (Object) null))
                {
                  if (Object.op_Equality((Object) nextTargetPoint2.owner, (Object) null))
                  {
                    if ((double) sqrMagnitude < (double) num9)
                    {
                      nextTargetPoint2 = targetPoint2;
                      num9 = sqrMagnitude;
                    }
                  }
                  else
                  {
                    nextTargetPoint2 = targetPoint2;
                    num9 = sqrMagnitude;
                  }
                }
                else if ((double) sqrMagnitude < (double) num9)
                {
                  nextTargetPoint2 = targetPoint2;
                  num9 = sqrMagnitude;
                }
              }
              if (flag3 || (double) sqrMagnitude <= (double) num4)
              {
                Vector2 vector2_2 = Vector2.op_Subtraction(vector2Xz3, vector2Xz2);
                float num10 = Mathf.Acos(Vector2.Dot(Vector2.op_Implicit(vector3_2), ((Vector2) ref vector2_2).normalized));
                if ((double) num10 <= (double) num3)
                {
                  bool flag4 = false;
                  if (this.parameter.enableCameraCulling && !flag1)
                  {
                    float cameraCullingMargin = this.parameter.cameraCullingMargin;
                    Vector3 viewportPoint = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToViewportPoint(targetPoint3);
                    if ((double) viewportPoint.x < 0.0 - (double) cameraCullingMargin || (double) viewportPoint.x > 1.0 + (double) cameraCullingMargin || (double) viewportPoint.y < 0.0 - (double) cameraCullingMargin || (double) viewportPoint.y > 1.0 + (double) cameraCullingMargin || (double) viewportPoint.z < 0.0)
                      flag4 = true;
                  }
                  if (!flag4)
                  {
                    if (Enemy.IsWeakStateSpAttack(obj1.weakState) && (Player.ATTACK_MODE) obj1.weakSubParam == self.attackMode)
                      self.targetPointWithSpWeakList.Add(targetPoint2);
                    if (Enemy.IsWeakStateDisplaySign(obj1.weakState))
                    {
                      double num11 = (double) Mathf.Sqrt(sqrMagnitude) - (double) this.parameter.weakMarginDistance;
                      float num12 = (float) (num11 * num11);
                      num10 = 0.0f;
                    }
                    vector3_3 = Vector3.op_Subtraction(vector3_1, targetPoint3);
                    float num13 = ((Vector3) ref vector3_3).magnitude + targetPoint2.weight;
                    if ((double) targetPoint3.y >= 0.0 && (double) targetPoint3.y < (double) self.GetIgnoreTargetHeight())
                    {
                      if (Object.op_Equality((Object) targetPoint1, (Object) null) || (double) num13 < (double) num8)
                      {
                        targetPoint1 = targetPoint2;
                        num8 = num13;
                      }
                      if ((double) num10 <= (double) num6 && (Object.op_Equality((Object) nextTargetPoint1, (Object) null) || (double) num13 < (double) num7))
                      {
                        nextTargetPoint1 = targetPoint2;
                        num7 = num13;
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      if (Object.op_Equality((Object) nextTargetPoint1, (Object) null) && Object.op_Inequality((Object) targetPoint1, (Object) null))
        nextTargetPoint1 = targetPoint1;
      if (isAutoMode)
        (self.controller as AutoSelfController).actionTargetPoint = nextTargetPoint2;
      if (flag2)
      {
        self.targetAimAfeterPoint = nextTargetPoint1;
        this.MakeTargetPointListForArrowAimBossMode(self.targetingPointList);
      }
      else if (self.isArrowRainShot)
      {
        self.SetActionTarget((StageObject) null);
        self.targetAimAfeterPoint = (TargetPoint) null;
        this.MakeTargetPointListForArrowAimBossMode(self.targetingPointList);
      }
      else
      {
        TargetPoint nowTargetPoint = this.DecideFinalTargetPoint(nextTargetPoint1, targetingPoint, self.attackMode);
        if (Object.op_Inequality((Object) nowTargetPoint, (Object) null))
        {
          self.targetingPointList.Add(nowTargetPoint);
          self.SetActionTarget(nowTargetPoint.owner);
          if (MonoBehaviourSingleton<StageObjectManager>.IsValid() & canTargetBoss)
          {
            Enemy owner = nowTargetPoint.owner as Enemy;
            if (Object.op_Inequality((Object) owner, (Object) null) && Object.op_Inequality((Object) owner, (Object) MonoBehaviourSingleton<StageObjectManager>.I.boss) && !owner.isBoss)
              nowTargetPoint.ForceDisplay();
          }
          if (isAutoMode)
          {
            AutoSelfController controller = self.controller as AutoSelfController;
            if (Object.op_Inequality((Object) nowTargetPoint, (Object) null))
            {
              if (Object.op_Equality((Object) nowTargetPoint.owner, (Object) null))
                controller.actionTargetPoint = nowTargetPoint;
              else if (Object.op_Equality((Object) nextTargetPoint2.owner, (Object) null))
              {
                TargetPoint targetPoint4 = this.DecideFinalTargetPoint(nextTargetPoint2, nowTargetPoint, self.attackMode);
                controller.actionTargetPoint = targetPoint4;
                self.targetingPointList.Add(targetPoint4);
                self.SetActionTarget(targetPoint4.owner);
              }
              else
                controller.actionTargetPoint = nowTargetPoint;
            }
            else
              controller.actionTargetPoint = nextTargetPoint2;
          }
        }
        else
          self.SetActionTarget((StageObject) null);
      }
      bool flag5 = false;
      TargetMarker targetMarker1 = (TargetMarker) null;
      this.markersTemp.Clear();
      this.markersTemp.AddRange((IEnumerable<TargetMarker>) this.markers);
      this.paramList.Clear();
      int index7 = 0;
      int count2 = this.paramListPool.Count;
      for (int index8 = 0; index8 < this.numTargetInfo; ++index8)
      {
        Enemy enemy = this.targetInfoList[index8].enemy;
        TargetPoint targetPoint5 = this.targetInfoList[index8].targetPoint;
        if (Object.op_Inequality((Object) targetPoint5, (Object) nextTargetPoint1))
        {
          targetPoint5.param.targetSelectCounter -= Time.deltaTime;
          if ((double) targetPoint5.param.targetSelectCounter < 0.0)
            targetPoint5.param.targetSelectCounter = 0.0f;
        }
        if (((Behaviour) targetPoint5).enabled && ((Component) targetPoint5).gameObject.activeInHierarchy && this.showMarker)
        {
          Enemy.WEAK_STATE weakState = targetPoint5.param.weakState;
          bool flag6 = false;
          if (self.CheckAttackMode(Player.ATTACK_MODE.ARROW) && (flag2 || self.isArrowAimLesserMode && !self.isArrowAimEnd))
            flag6 = true;
          bool flag7 = false;
          if (Enemy.IsWeakStateSpAttack(weakState) && (Player.ATTACK_MODE) targetPoint5.param.weakSubParam == self.attackMode)
            flag7 = true;
          bool flag8 = false;
          float num14 = 1f;
          if (Object.op_Inequality((Object) enemy, (Object) null) && targetPoint5.regionID >= 0 && targetPoint5.regionID < enemy.regionWorks.Length)
          {
            if (flag6 && !flag7)
              num14 = targetPoint5.param.aimMarkerScale;
            else
              flag8 = weakState != targetPoint5.param.prevWeakState && weakState != 0;
            if (!flag5)
              targetPoint5.param.prevWeakState = weakState;
          }
          bool flag9 = self.targetingPointList.Contains(targetPoint5) || this.fieldMultiLockDic.ContainsKey(targetPoint5);
          bool flag10 = (this.parameter.enableNormalMarker | flag2 || targetPoint5.IsForceDisplay) && !self.isJumpAction;
          if (flag9 & flag10 || weakState != Enemy.WEAK_STATE.NONE)
          {
            TargetMarker.UpdateParam updateParam;
            if (index7 >= count2)
            {
              updateParam = new TargetMarker.UpdateParam();
              this.paramListPool.Add(updateParam);
            }
            else
              updateParam = this.paramListPool[index7];
            updateParam.targetPoint = targetPoint5;
            updateParam.targeting = flag9;
            updateParam.isLock = this.isTargetLock;
            updateParam.weakState = weakState;
            updateParam.weakSubParam = targetPoint5.param.weakSubParam;
            updateParam.playSign = flag8;
            updateParam.spAttackType = self.spAttackType;
            updateParam.isAimArrow = flag6;
            updateParam.isAimMode = flag6 && !flag7;
            updateParam.isAimChargeMax = (double) self.GetChargingRate() >= 1.0;
            updateParam.markerScale = num14;
            updateParam.validElementType = targetPoint5.param.validElementType;
            if (self.isArrowAimLesserMode)
              updateParam.isMultiLockMax = self.isMultiLockMax();
            TargetMarker targetMarker2 = (TargetMarker) null;
            for (int index9 = 0; index9 < this.markersTemp.Count; ++index9)
            {
              if (Object.op_Equality((Object) this.markersTemp[index9].point, (Object) updateParam.targetPoint))
              {
                targetMarker2 = this.markersTemp[index9];
                this.markersTemp.RemoveAt(index9);
                break;
              }
            }
            if (targetMarker2 == null)
              this.paramList.Add(updateParam);
            else if (!flag5)
            {
              flag5 = targetMarker2.UpdateMarker(updateParam);
              if (self.isArrowAimLesserMode && self.spAttackType == SP_ATTACK_TYPE.SOUL && Object.op_Equality((Object) targetMarker2.point, (Object) self.targetingPoint))
              {
                MultiLockMarker multiLock;
                if (this.fieldMultiLockDic.ContainsKey(targetPoint5))
                {
                  multiLock = this.fieldMultiLockDic[targetPoint5];
                }
                else
                {
                  multiLock = targetMarker2.GetMultiLock();
                  this.fieldMultiLockDic.Add(targetPoint5, multiLock);
                }
                self.CheckMultiLock(multiLock);
              }
            }
            ++index7;
          }
        }
      }
      int index10 = 0;
      for (int count3 = this.paramList.Count; index10 < count3; ++index10)
      {
        targetMarker1 = (TargetMarker) null;
        TargetMarker targetMarker3;
        if (this.markersTemp.Count > 0)
        {
          targetMarker3 = this.markersTemp[0];
          this.markersTemp.RemoveAt(0);
        }
        else
        {
          targetMarker3 = new TargetMarker(this._transform);
          this.markers.Add(targetMarker3);
        }
        if (!flag5)
          targetMarker3.UpdateMarker(this.paramList[index10]);
      }
      if (this.markersTemp.Count > 0)
      {
        for (int index11 = 0; index11 < this.markersTemp.Count; ++index11)
          this.markersTemp[index11].UnableMarker();
      }
      for (int index12 = 0; index12 < this.numTargetInfo; ++index12)
      {
        this.targetInfoList[index12].enemy = (Enemy) null;
        this.targetInfoList[index12].targetPoint = (TargetPoint) null;
      }
      this.numTargetInfo = 0;
      if (!canTargetBoss || !this.updateShadowSealingFlag)
        return;
      MonoBehaviourSingleton<StageObjectManager>.I.boss.CountShadowSealingTarget();
      this.updateShadowSealingFlag = false;
      MonoBehaviourSingleton<StageObjectManager>.I.boss.CheckCounterRegion();
    }
  }

  private void FuncCannonMode()
  {
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (boss.isDead || !boss.IsValidShield())
      return;
    EnemyRegionWork enemyRegionWork = boss.SearchShieldCriticalRegionWork();
    if (enemyRegionWork == null)
    {
      Debug.LogWarning((object) "Not found RegionWork.");
    }
    else
    {
      TargetPoint targetPoint = boss.SearchTargetPoint(enemyRegionWork.regionId);
      if (Object.op_Equality((Object) targetPoint, (Object) null))
      {
        Debug.LogWarning((object) "Not found TargetPoint.");
      }
      else
      {
        if (this.m_cannonCriticalMarker == null)
          this.m_cannonCriticalMarker = new TargetMarker(this._transform);
        this.m_cannonCriticalMarker.UpdateByTargetPoint(targetPoint, "ef_btl_target_cannon_02");
      }
    }
  }

  private void FuncCommon()
  {
  }

  private void UpdateGrabMarker()
  {
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (Object.op_Equality((Object) boss, (Object) null) || boss.isDead)
      return;
    EnemyRegionWork enemyRegionWork = (EnemyRegionWork) null;
    EnemyRegionWork[] regionWorks = boss.regionWorks;
    if (regionWorks == null)
      return;
    for (int index = 0; index < regionWorks.Length; ++index)
    {
      if (regionWorks[index].weakState == Enemy.WEAK_STATE.WEAK_GRAB)
      {
        enemyRegionWork = regionWorks[index];
        break;
      }
    }
    if (enemyRegionWork == null)
    {
      this.UnableGrabMarker();
    }
    else
    {
      TargetPoint targetPoint = boss.SearchTargetPoint(enemyRegionWork.regionId);
      if (Object.op_Equality((Object) targetPoint, (Object) null))
      {
        Debug.LogWarning((object) "Not found TargetPoint.");
      }
      else
      {
        if (this.m_grabMarker == null)
          this.m_grabMarker = new TargetMarker(this._transform);
        this.m_grabMarker.UpdateByTargetPoint(targetPoint, "ef_btl_target_cannon_03");
        if (Object.op_Equality((Object) this.uiGrabStatusGizmo, (Object) null))
          this.uiGrabStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateGrab();
        this.uiGrabStatusGizmo.targetEnemy = boss;
        this.uiGrabStatusGizmo.targetPoint = targetPoint;
      }
    }
  }

  private void UnableGrabMarker()
  {
    if (this.m_grabMarker != null)
    {
      this.m_grabMarker.UnableMarker();
      this.m_grabMarker = (TargetMarker) null;
    }
    if (!Object.op_Inequality((Object) this.uiGrabStatusGizmo, (Object) null))
      return;
    this.uiGrabStatusGizmo.targetEnemy = (Enemy) null;
    this.uiGrabStatusGizmo.targetPoint = (TargetPoint) null;
    this.uiGrabStatusGizmo = (UIGrabStatusGizmo) null;
  }

  private void MakeTargetPointListForArrowAimBossMode(List<TargetPoint> targetList)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    for (int index1 = 0; index1 < this.numTargetInfo; ++index1)
    {
      TargetPoint targetPoint1 = this.targetInfoList[index1].targetPoint;
      if (targetPoint1.param.isTargetEnable && !Object.op_Equality((Object) targetPoint1.subRegionRoot, (Object) null))
      {
        Enemy enemy = this.targetInfoList[index1].enemy;
        if (!Object.op_Equality((Object) enemy, (Object) null))
        {
          for (int index2 = index1 + 1; index2 < this.numTargetInfo; ++index2)
          {
            TargetPoint targetPoint2 = this.targetInfoList[index2].targetPoint;
            if (!Object.op_Inequality((Object) this.targetInfoList[index2].enemy, (Object) enemy))
            {
              if (targetPoint2.param.isTargetEnable && !Object.op_Equality((Object) targetPoint2.subRegionRoot, (Object) null) && Object.op_Equality((Object) targetPoint1.subRegionRoot, (Object) targetPoint2.subRegionRoot))
              {
                if ((double) targetPoint1.param.vecSqrMagnitude > (double) targetPoint2.param.vecSqrMagnitude)
                  targetPoint1.param.isTargetEnable = false;
                else
                  targetPoint2.param.isTargetEnable = false;
              }
            }
            else
              break;
          }
        }
      }
    }
    for (int index = 0; index < this.numTargetInfo; ++index)
    {
      Enemy enemy = this.targetInfoList[index].enemy;
      if (Object.op_Equality((Object) enemy, (Object) null))
      {
        if (self.spAttackType == SP_ATTACK_TYPE.SOUL)
          targetList.Add(this.targetInfoList[index].targetPoint);
      }
      else
      {
        TargetPoint targetPoint = this.targetInfoList[index].targetPoint;
        if (targetPoint.param.isTargetEnable && targetPoint.regionID >= 0 && targetPoint.regionID < enemy.regionWorks.Length && enemy.regionInfos[targetPoint.regionID].isAtkColliderHit)
        {
          if (self.spAttackType == SP_ATTACK_TYPE.HEAT)
          {
            if (enemy.IsShadowSealingStuck(targetPoint.regionID) || !enemy.isBoss)
              continue;
          }
          else if (self.spAttackType == SP_ATTACK_TYPE.NONE && enemy.IsMaxLvBleedFromSelf(targetPoint.regionID))
            continue;
          targetList.Add(targetPoint);
        }
      }
    }
  }

  private TargetPoint DecideFinalTargetPoint(
    TargetPoint nextTargetPoint,
    TargetPoint nowTargetPoint,
    Player.ATTACK_MODE attackMode)
  {
    bool flag1 = false;
    if ((double) this.targetingTime == 0.0 || (double) Time.time - (double) this.targetingTime >= (double) this.parameter.changeAbleTime)
      flag1 = true;
    bool flag2 = true;
    bool flag3 = false;
    if (Object.op_Inequality((Object) nextTargetPoint, (Object) null))
    {
      TargetPoint.Param obj = nextTargetPoint.param;
      obj.targetSelectCounter += Time.deltaTime;
      if ((double) obj.targetSelectCounter < (double) this.parameter.selectAbleTime)
        flag2 = false;
      switch (obj.weakState)
      {
        case Enemy.WEAK_STATE.WEAK:
          flag3 = true;
          break;
        case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
        case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
        case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
          if ((Player.ATTACK_MODE) obj.weakSubParam == attackMode)
          {
            flag3 = true;
            break;
          }
          break;
      }
    }
    bool flag4 = false;
    if (flag3)
      flag4 = true;
    bool flag5 = false;
    if (Object.op_Inequality((Object) nowTargetPoint, (Object) null))
    {
      switch (nowTargetPoint.param.weakState)
      {
        case Enemy.WEAK_STATE.WEAK:
          flag5 = true;
          break;
        case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
        case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
        case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
          if ((Player.ATTACK_MODE) nowTargetPoint.param.weakSubParam == attackMode)
          {
            flag5 = true;
            break;
          }
          break;
      }
    }
    bool flag6 = false;
    if (flag5)
    {
      flag6 = true;
      if ((double) this.targetingWeakTime <= 0.0)
        this.targetingWeakTime = Time.time;
      if ((double) Time.time - (double) this.targetingWeakTime >= (double) this.parameter.changeWeakTime)
        flag6 = false;
    }
    else
      this.targetingWeakTime = 0.0f;
    bool flag7 = false;
    if ((flag1 & flag2 | flag4 || Object.op_Equality((Object) nowTargetPoint, (Object) null)) && !flag6)
      flag7 = true;
    bool flag8 = false;
    if (Object.op_Inequality((Object) nowTargetPoint, (Object) null))
    {
      Enemy owner = nowTargetPoint.owner as Enemy;
      if (Object.op_Inequality((Object) owner, (Object) null) && !owner.isDead && owner.enableTargetPoint)
        flag8 = true;
    }
    if (!this.isTargetLock & flag7 || !flag8 || this.isTargetDisable)
    {
      if (Object.op_Inequality((Object) nextTargetPoint, (Object) null))
      {
        if (Object.op_Inequality((Object) nowTargetPoint, (Object) nextTargetPoint))
        {
          nowTargetPoint = nextTargetPoint;
          this.targetingTime = Time.time;
          this.targetingWeakTime = 0.0f;
        }
        nextTargetPoint.param.targetSelectCounter = 0.0f;
      }
      else
        nowTargetPoint = (TargetPoint) null;
      this.isTargetLock = false;
    }
    return nowTargetPoint;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    this.Clear();
  }

  private void Clear()
  {
    this.markers.ForEach((Action<TargetMarker>) (o => o.UnableMarker()));
    this.markers.Clear();
  }

  public void OnDetachedObject(StageObject stage_object)
  {
  }

  public void SetTargetLock(bool isLock, float changeTime = 0.0f)
  {
    if (isLock || this.isTargetLock == isLock || (double) changeTime <= 0.0)
    {
      this.isTargetLock = isLock;
      this.changeLockTime = 0.0f;
      this.changeLockFlag = false;
    }
    else if (!this.changeLockFlag)
    {
      this.changeLockTime = changeTime;
      this.changeLockFlag = true;
    }
    if (isLock)
      return;
    this.fieldMultiLockDic.Clear();
  }

  public void SetTargetDisable(bool disable) => this.isTargetDisable = disable;

  public static void ClearPoolObjects()
  {
  }

  public List<TargetMarker> GetTargetMarkerList() => this.markers;

  public int GetTargetMarkerNum()
  {
    return this.markers == null ? 0 : this.markers.Count * MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockRegionMax;
  }

  public void HideMultiLock()
  {
    if (this.markers == null)
      return;
    for (int index = 0; index < this.markers.Count; ++index)
      this.markers[index].HideMultiLock();
  }

  public void ResetMultiLock()
  {
    if (this.markers == null)
      return;
    for (int index = 0; index < this.markers.Count; ++index)
      this.markers[index].ResetMultiLock();
  }

  public void EndMultiLockBoost()
  {
    if (this.markers == null)
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    bool flag = this.GetMultiLockNum() >= self.GetSoulArrowNormalLockNum();
    for (int index = 0; index < this.markers.Count; ++index)
      this.markers[index].EndMultiLockBoost(flag);
    self.SetBulletLineColor(flag);
  }

  public int GetMultiLockNum()
  {
    if (this.markers == null)
      return 0;
    int multiLockNum = 0;
    for (int index = 0; index < this.markers.Count; ++index)
      multiLockNum += this.markers[index].GetMultiLockNum();
    return multiLockNum;
  }

  public enum Function
  {
    None,
    Basis,
    Cannon,
  }

  private class TARGET_INFO
  {
    public TargetPoint targetPoint;
    public Enemy enemy;
  }
}
