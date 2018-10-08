using System.Collections.Generic;
using UnityEngine;

public class TargetMarkerManager : MonoBehaviourSingleton<TargetMarkerManager>
{
	public enum Function
	{
		None,
		Basis,
		Cannon
	}

	private class TARGET_INFO
	{
		public TargetPoint targetPoint;

		public Enemy enemy;
	}

	public const int TARGET_INFO_MAX = 80;

	private Function m_func;

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

	private List<TARGET_INFO> targetInfoList = new List<TARGET_INFO>();

	private TargetMarker m_cannonCriticalMarker;

	private TargetMarker m_grabMarker;

	private UIGrabStatusGizmo uiGrabStatusGizmo;

	public bool updateShadowSealingFlag;

	public InGameSettingsManager.TargetMarker parameter
	{
		get;
		protected set;
	}

	public TargetPoint targetingPoint
	{
		get;
		protected set;
	}

	public bool showMarker
	{
		get;
		set;
	}

	public bool isTargetLock
	{
		get;
		protected set;
	}

	public bool isTargetDisable
	{
		get;
		protected set;
	}

	public TargetMarkerManager()
	{
		isTargetLock = false;
	}

	private void Start()
	{
		showMarker = true;
		markers = new List<TargetMarker>();
		markersTemp = new List<TargetMarker>();
		paramListPool = new List<TargetMarker.UpdateParam>();
		paramList = new List<TargetMarker.UpdateParam>();
		fieldMultiLockDic = new Dictionary<TargetPoint, MultiLockMarker>();
		for (int i = 0; i < 80; i++)
		{
			TARGET_INFO tARGET_INFO = new TARGET_INFO();
			tARGET_INFO.enemy = null;
			tARGET_INFO.targetPoint = null;
			targetInfoList.Add(tARGET_INFO);
		}
	}

	private void RegisterTargetInfo(TargetPoint targetPoint, Enemy enemy = null)
	{
		if (targetInfoList.Count > numTargetInfo)
		{
			targetInfoList[numTargetInfo].targetPoint = targetPoint;
			targetInfoList[numTargetInfo].enemy = enemy;
			numTargetInfo++;
		}
	}

	private void LateUpdate()
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if ((Object)self == (Object)null)
		{
			Clear();
		}
		else
		{
			Function func = m_func;
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			func = ((!self.IsOnCannonMode() || !((Object)boss != (Object)null) || !boss.IsValidShield()) ? Function.Basis : Function.Cannon);
			if (func != m_func)
			{
				OnChangeFunction(func);
			}
			FuncCommon();
			switch (m_func)
			{
			case Function.Basis:
				FuncBasisMode();
				break;
			case Function.Cannon:
				FuncCannonMode();
				break;
			}
		}
	}

	private void OnChangeFunction(Function nextFunc)
	{
		switch (m_func)
		{
		case Function.Basis:
			Clear();
			markersTemp.Clear();
			break;
		case Function.Cannon:
			if (m_cannonCriticalMarker != null)
			{
				m_cannonCriticalMarker.UnableMarker();
			}
			UnableGrabMarker();
			break;
		}
		m_func = nextFunc;
	}

	private void FuncBasisMode()
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if ((Object)MonoBehaviourSingleton<StageObjectManager>.I.boss != (Object)null)
		{
			parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarker;
		}
		else
		{
			parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerLesserEnemies;
		}
		if (changeLockFlag)
		{
			changeLockTime -= Time.deltaTime;
			if (changeLockTime <= 0f)
			{
				changeLockTime = 0f;
				changeLockFlag = false;
				isTargetLock = !isTargetLock;
			}
		}
		bool flag = false;
		Vector3 vector = self._position;
		if (self.isArrowAimLesserMode)
		{
			if (self.isArrowAimEnd)
			{
				flag = true;
			}
			else
			{
				parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerArrowAimLesser;
				vector += self.arrowAimLesserCursorPos;
			}
		}
		float num = parameter.targetDistance;
		float num2 = parameter.showTargetDistance;
		if (self.isLongAttackMode)
		{
			num = parameter.targetDistanceArrow;
			num2 = parameter.showTargetDistanceArrow;
		}
		TargetPoint targetingPoint = self.targetingPoint;
		self.targetingPointList.Clear();
		self.targetPointWithSpWeakList.Clear();
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList[i];
			if (enemy.HasValidTargetPoint())
			{
				for (int j = 0; j < enemy.targetPoints.Length; j++)
				{
					RegisterTargetInfo(enemy.targetPoints[j], enemy);
				}
			}
			List<IBulletObservable> bulletObservableList = enemy.GetBulletObservableList();
			if (bulletObservableList != null)
			{
				for (int k = 0; k < bulletObservableList.Count; k++)
				{
					AnimEventShot animEventShot = bulletObservableList[k] as AnimEventShot;
					if ((Object)animEventShot != (Object)null && (Object)animEventShot.targetPoint != (Object)null)
					{
						RegisterTargetInfo(animEventShot.targetPoint, null);
					}
					else
					{
						AttackFunnelBit attackFunnelBit = bulletObservableList[k] as AttackFunnelBit;
						if ((Object)attackFunnelBit != (Object)null && (Object)attackFunnelBit.targetPoint != (Object)null)
						{
							RegisterTargetInfo(attackFunnelBit.targetPoint, null);
						}
					}
				}
			}
		}
		for (int l = 0; l < MonoBehaviourSingleton<InGameManager>.I.dropItemList.Count; l++)
		{
			FieldDropObject fieldDropObject = MonoBehaviourSingleton<InGameManager>.I.dropItemList[l];
			if ((Object)fieldDropObject.targetPoint != (Object)null && fieldDropObject.gameObject.activeInHierarchy)
			{
				RegisterTargetInfo(fieldDropObject.targetPoint, null);
			}
		}
		int count2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count;
		for (int m = 0; m < count2; m++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[m] as Player;
			if (!((Object)player == (Object)null) && !player.isDead)
			{
				TargetPoint restraintTargetPoint = player.RestraintTargetPoint;
				if ((Object)restraintTargetPoint != (Object)null)
				{
					RegisterTargetInfo(restraintTargetPoint, null);
				}
			}
		}
		if (numTargetInfo <= 0)
		{
			Clear();
			self.SetActionTarget(null, true);
		}
		else
		{
			float num3 = parameter.showAngle * 0.0174532924f;
			float num4 = num * num;
			float num5 = num2 * num2;
			float num6 = parameter.targetAngle * 0.0174532924f;
			TargetPoint targetPoint = null;
			float num7 = 3.40282347E+38f;
			TargetPoint targetPoint2 = null;
			float num8 = 3.40282347E+38f;
			TargetPoint targetPoint3 = null;
			float num9 = 3.40282347E+38f;
			Vector2 b = vector.ToVector2XZ();
			self.forwardXZ.Normalize();
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Quaternion rotation = cameraTransform.rotation;
			Vector3 position = cameraTransform.position;
			Vector2 b2 = position.ToVector2XZ();
			Vector3 v = cameraTransform.forward.ToVector2XZ();
			v.Normalize();
			bool isAutoMode = self.isAutoMode;
			for (int n = 0; n < numTargetInfo; n++)
			{
				Enemy enemy2 = targetInfoList[n].enemy;
				TargetPoint targetPoint4 = targetInfoList[n].targetPoint;
				TargetPoint.Param param = targetPoint4.param;
				param.isShowRange = false;
				param.isTargetEnable = false;
				param.weakState = Enemy.WEAK_STATE.NONE;
				param.weakSubParam = -1;
				if (targetPoint4.enabled && targetPoint4.gameObject.activeInHierarchy)
				{
					if (self.isArrowAimBossMode)
					{
						if (!self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) && !targetPoint4.isAimEnable && !targetPoint4.isDispArrowSpWeak)
						{
							continue;
						}
					}
					else if (!targetPoint4.isTargetEnable)
					{
						continue;
					}
					if ((Object)enemy2 != (Object)null && targetPoint4.regionID >= 0 && targetPoint4.regionID < enemy2.regionWorks.Length)
					{
						EnemyRegionWork enemyRegionWork = enemy2.regionWorks[targetPoint4.regionID];
						if (!enemyRegionWork.enabled)
						{
							continue;
						}
						if (!self.isArrowAimBossMode || Enemy.IsWeakStateDisplaySign(enemyRegionWork.weakState))
						{
							param.weakState = enemyRegionWork.weakState;
							param.weakSubParam = enemyRegionWork.weakSubParam;
							param.validElementType = enemyRegionWork.validElementType;
							if (Enemy.IsWeakStateCheckAlreadyHit(param.weakState) && enemyRegionWork.weakAttackIDs.Contains(self.id))
							{
								param.weakState = Enemy.WEAK_STATE.NONE;
							}
						}
						param.aimMarkerScale = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy.aimMarkerBaseRate * enemy2.enemyTableData.aimMarkerRate * targetPoint4.aimMarkerPointRate;
					}
					if (!self.isArrowAimBossMode || !targetPoint4.isDispArrowSpWeak || ((param.weakState == Enemy.WEAK_STATE.WEAK_SP_ATTACK || param.weakState == Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK) && param.weakSubParam == 5))
					{
						param.isTargetEnable = true;
						Vector3 targetPoint5 = targetPoint4.GetTargetPoint();
						Vector2 a = targetPoint5.ToVector2XZ();
						Vector2 vector2 = a - b;
						param.markerPos = (position - targetPoint5).normalized * targetPoint4.scaledMarkerZShift + targetPoint5;
						param.markerRot = rotation;
						param.targetPos = targetPoint5;
						bool flag2 = false;
						if (param.weakState != 0 && param.weakSubParam != 0)
						{
							flag2 = true;
						}
						float sqrMagnitude = vector2.sqrMagnitude;
						param.isShowRange = (sqrMagnitude < num5 || flag2);
						param.vecSqrMagnitude = sqrMagnitude;
						if (!isTargetDisable)
						{
							if (isAutoMode && !(targetPoint4.owner is Player))
							{
								if ((Object)targetPoint3 == (Object)null)
								{
									targetPoint3 = targetPoint4;
									num9 = sqrMagnitude;
								}
								else if ((Object)targetPoint4.owner == (Object)null)
								{
									if ((Object)targetPoint3.owner == (Object)null)
									{
										if (sqrMagnitude < num9)
										{
											targetPoint3 = targetPoint4;
											num9 = sqrMagnitude;
										}
									}
									else
									{
										targetPoint3 = targetPoint4;
										num9 = sqrMagnitude;
									}
								}
								else if (sqrMagnitude < num9)
								{
									targetPoint3 = targetPoint4;
									num9 = sqrMagnitude;
								}
							}
							if (flag2 || !(sqrMagnitude > num4))
							{
								Vector2 vector3 = a - b2;
								float num10 = Mathf.Acos(Vector2.Dot(v, vector3.normalized));
								if (!(num10 > num3))
								{
									bool flag3 = false;
									if (parameter.enableCameraCulling && !flag)
									{
										float cameraCullingMargin = parameter.cameraCullingMargin;
										Vector3 vector4 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToViewportPoint(targetPoint5);
										if (vector4.x < 0f - cameraCullingMargin || vector4.x > 1f + cameraCullingMargin || vector4.y < 0f - cameraCullingMargin || vector4.y > 1f + cameraCullingMargin || vector4.z < 0f)
										{
											flag3 = true;
										}
									}
									if (!flag3)
									{
										if (Enemy.IsWeakStateSpAttack(param.weakState) && param.weakSubParam == (int)self.attackMode)
										{
											self.targetPointWithSpWeakList.Add(targetPoint4);
										}
										if (Enemy.IsWeakStateDisplaySign(param.weakState))
										{
											float num11 = Mathf.Sqrt(sqrMagnitude) - parameter.weakMarginDistance;
											sqrMagnitude = num11 * num11;
											num10 = 0f;
										}
										sqrMagnitude = (vector - targetPoint5).magnitude;
										sqrMagnitude += targetPoint4.weight;
										if (targetPoint5.y >= 0f && targetPoint5.y < self.GetIgnoreTargetHeight())
										{
											if ((Object)targetPoint2 == (Object)null || sqrMagnitude < num8)
											{
												targetPoint2 = targetPoint4;
												num8 = sqrMagnitude;
											}
											if (num10 <= num6 && ((Object)targetPoint == (Object)null || sqrMagnitude < num7))
											{
												targetPoint = targetPoint4;
												num7 = sqrMagnitude;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if ((Object)targetPoint == (Object)null && (Object)targetPoint2 != (Object)null)
			{
				targetPoint = targetPoint2;
			}
			if (isAutoMode)
			{
				AutoSelfController autoSelfController = self.controller as AutoSelfController;
				autoSelfController.actionTargetPoint = targetPoint3;
			}
			if (self.isArrowAimBossMode)
			{
				self.targetAimAfeterPoint = targetPoint;
				MakeTargetPointListForArrowAimBossMode(self.targetingPointList);
			}
			else
			{
				TargetPoint targetPoint6 = DecideFinalTargetPoint(targetPoint, targetingPoint, self.attackMode);
				if ((Object)targetPoint6 != (Object)null)
				{
					self.targetingPointList.Add(targetPoint6);
					self.SetActionTarget(targetPoint6.owner, true);
					if (isAutoMode)
					{
						AutoSelfController autoSelfController2 = self.controller as AutoSelfController;
						if ((Object)targetPoint6 != (Object)null)
						{
							if ((Object)targetPoint6.owner == (Object)null)
							{
								autoSelfController2.actionTargetPoint = targetPoint6;
							}
							else if ((Object)targetPoint3.owner == (Object)null)
							{
								targetPoint6 = (autoSelfController2.actionTargetPoint = DecideFinalTargetPoint(targetPoint3, targetPoint6, self.attackMode));
								self.targetingPointList.Add(targetPoint6);
								self.SetActionTarget(targetPoint6.owner, true);
							}
							else
							{
								autoSelfController2.actionTargetPoint = targetPoint6;
							}
						}
						else
						{
							autoSelfController2.actionTargetPoint = targetPoint3;
						}
					}
				}
				else
				{
					self.SetActionTarget(null, true);
				}
			}
			bool flag4 = false;
			TargetMarker targetMarker = null;
			markersTemp.Clear();
			markersTemp.AddRange(markers);
			paramList.Clear();
			int num12 = 0;
			int count3 = paramListPool.Count;
			for (int num13 = 0; num13 < numTargetInfo; num13++)
			{
				Enemy enemy3 = targetInfoList[num13].enemy;
				TargetPoint targetPoint7 = targetInfoList[num13].targetPoint;
				if ((Object)targetPoint7 != (Object)targetPoint)
				{
					targetPoint7.param.targetSelectCounter -= Time.deltaTime;
					if (targetPoint7.param.targetSelectCounter < 0f)
					{
						targetPoint7.param.targetSelectCounter = 0f;
					}
				}
				if (targetPoint7.enabled && targetPoint7.gameObject.activeInHierarchy && showMarker)
				{
					Enemy.WEAK_STATE weakState = targetPoint7.param.weakState;
					bool flag5 = false;
					if (self.CheckAttackMode(Player.ATTACK_MODE.ARROW) && (self.isArrowAimBossMode || (self.isArrowAimLesserMode && !self.isArrowAimEnd)))
					{
						flag5 = true;
					}
					bool flag6 = false;
					if (Enemy.IsWeakStateSpAttack(weakState) && targetPoint7.param.weakSubParam == (int)self.attackMode)
					{
						flag6 = true;
					}
					bool playSign = false;
					float markerScale = 1f;
					if ((Object)enemy3 != (Object)null && targetPoint7.regionID >= 0 && targetPoint7.regionID < enemy3.regionWorks.Length)
					{
						if (flag5 && !flag6)
						{
							markerScale = targetPoint7.param.aimMarkerScale;
						}
						else
						{
							playSign = (weakState != targetPoint7.param.prevWeakState && weakState != Enemy.WEAK_STATE.NONE);
						}
						if (!flag4)
						{
							targetPoint7.param.prevWeakState = weakState;
						}
					}
					bool flag7 = self.targetingPointList.Contains(targetPoint7) || fieldMultiLockDic.ContainsKey(targetPoint7);
					bool flag8 = (parameter.enableNormalMarker || self.isArrowAimBossMode || targetPoint7.IsForceDisplay) && !self.isJumpAction;
					if ((flag7 && flag8) || weakState != 0)
					{
						TargetMarker.UpdateParam updateParam = null;
						if (num12 >= count3)
						{
							updateParam = new TargetMarker.UpdateParam();
							paramListPool.Add(updateParam);
						}
						else
						{
							updateParam = paramListPool[num12];
						}
						updateParam.targetPoint = targetPoint7;
						updateParam.targeting = flag7;
						updateParam.isLock = isTargetLock;
						updateParam.weakState = weakState;
						updateParam.weakSubParam = targetPoint7.param.weakSubParam;
						updateParam.playSign = playSign;
						updateParam.spAttackType = self.spAttackType;
						updateParam.isAimArrow = flag5;
						updateParam.isAimMode = (flag5 && !flag6);
						updateParam.isAimChargeMax = (self.GetChargingRate() >= 1f);
						updateParam.markerScale = markerScale;
						updateParam.validElementType = targetPoint7.param.validElementType;
						if (self.isArrowAimLesserMode)
						{
							updateParam.isMultiLockMax = self.isMultiLockMax();
						}
						targetMarker = null;
						for (int num14 = 0; num14 < markersTemp.Count; num14++)
						{
							if ((Object)markersTemp[num14].point == (Object)updateParam.targetPoint)
							{
								targetMarker = markersTemp[num14];
								markersTemp.RemoveAt(num14);
								break;
							}
						}
						if (targetMarker == null)
						{
							paramList.Add(updateParam);
						}
						else if (!flag4)
						{
							flag4 = targetMarker.UpdateMarker(updateParam);
							if (self.isArrowAimLesserMode && self.spAttackType == SP_ATTACK_TYPE.SOUL && (Object)targetMarker.point == (Object)self.targetingPoint)
							{
								MultiLockMarker multiLockMarker = null;
								if (fieldMultiLockDic.ContainsKey(targetPoint7))
								{
									multiLockMarker = fieldMultiLockDic[targetPoint7];
								}
								else
								{
									multiLockMarker = targetMarker.GetMultiLock();
									fieldMultiLockDic.Add(targetPoint7, multiLockMarker);
								}
								self.CheckMultiLock(multiLockMarker);
							}
						}
						num12++;
					}
				}
			}
			int num15 = 0;
			for (int count4 = paramList.Count; num15 < count4; num15++)
			{
				targetMarker = null;
				if (markersTemp.Count > 0)
				{
					targetMarker = markersTemp[0];
					markersTemp.RemoveAt(0);
				}
				else
				{
					targetMarker = new TargetMarker(base._transform);
					markers.Add(targetMarker);
				}
				if (!flag4)
				{
					targetMarker.UpdateMarker(paramList[num15]);
				}
			}
			if (markersTemp.Count > 0)
			{
				for (int num16 = 0; num16 < markersTemp.Count; num16++)
				{
					markersTemp[num16].UnableMarker();
				}
			}
			for (int num17 = 0; num17 < numTargetInfo; num17++)
			{
				targetInfoList[num17].enemy = null;
				targetInfoList[num17].targetPoint = null;
			}
			numTargetInfo = 0;
			if ((Object)MonoBehaviourSingleton<StageObjectManager>.I.boss != (Object)null && updateShadowSealingFlag)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.boss.CountShadowSealingTarget();
				updateShadowSealingFlag = false;
				MonoBehaviourSingleton<StageObjectManager>.I.boss.CheckCounterRegion();
			}
		}
	}

	private void FuncCannonMode()
	{
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (!boss.isDead && boss.IsValidShield())
		{
			EnemyRegionWork enemyRegionWork = boss.SearchShieldCriticalRegionWork();
			if (enemyRegionWork == null)
			{
				Debug.LogWarning("Not found RegionWork.");
			}
			else
			{
				TargetPoint targetPoint = boss.SearchTargetPoint(enemyRegionWork.regionId);
				if ((Object)targetPoint == (Object)null)
				{
					Debug.LogWarning("Not found TargetPoint.");
				}
				else
				{
					if (m_cannonCriticalMarker == null)
					{
						m_cannonCriticalMarker = new TargetMarker(base._transform);
					}
					m_cannonCriticalMarker.UpdateByTargetPoint(targetPoint, "ef_btl_target_cannon_02");
				}
			}
		}
	}

	private void FuncCommon()
	{
	}

	private void UpdateGrabMarker()
	{
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (!((Object)boss == (Object)null) && !boss.isDead)
		{
			EnemyRegionWork enemyRegionWork = null;
			EnemyRegionWork[] regionWorks = boss.regionWorks;
			if (regionWorks != null)
			{
				for (int i = 0; i < regionWorks.Length; i++)
				{
					if (regionWorks[i].weakState == Enemy.WEAK_STATE.WEAK_GRAB)
					{
						enemyRegionWork = regionWorks[i];
						break;
					}
				}
				if (enemyRegionWork == null)
				{
					UnableGrabMarker();
				}
				else
				{
					TargetPoint targetPoint = boss.SearchTargetPoint(enemyRegionWork.regionId);
					if ((Object)targetPoint == (Object)null)
					{
						Debug.LogWarning("Not found TargetPoint.");
					}
					else
					{
						if (m_grabMarker == null)
						{
							m_grabMarker = new TargetMarker(base._transform);
						}
						m_grabMarker.UpdateByTargetPoint(targetPoint, "ef_btl_target_cannon_03");
						if ((Object)uiGrabStatusGizmo == (Object)null)
						{
							uiGrabStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateGrab();
						}
						uiGrabStatusGizmo.targetEnemy = boss;
						uiGrabStatusGizmo.targetPoint = targetPoint;
					}
				}
			}
		}
	}

	private void UnableGrabMarker()
	{
		if (m_grabMarker != null)
		{
			m_grabMarker.UnableMarker();
			m_grabMarker = null;
		}
		if ((Object)uiGrabStatusGizmo != (Object)null)
		{
			uiGrabStatusGizmo.targetEnemy = null;
			uiGrabStatusGizmo.targetPoint = null;
			uiGrabStatusGizmo = null;
		}
	}

	private void MakeTargetPointListForArrowAimBossMode(List<TargetPoint> targetList)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (!((Object)self == (Object)null))
		{
			for (int i = 0; i < numTargetInfo; i++)
			{
				TargetPoint targetPoint = targetInfoList[i].targetPoint;
				if (targetPoint.param.isTargetEnable && !((Object)targetPoint.subRegionRoot == (Object)null))
				{
					Enemy enemy = targetInfoList[i].enemy;
					if (!((Object)enemy == (Object)null))
					{
						for (int j = i + 1; j < numTargetInfo; j++)
						{
							TargetPoint targetPoint2 = targetInfoList[j].targetPoint;
							Enemy enemy2 = targetInfoList[j].enemy;
							if ((Object)enemy2 != (Object)enemy)
							{
								break;
							}
							if (targetPoint2.param.isTargetEnable && !((Object)targetPoint2.subRegionRoot == (Object)null) && (Object)targetPoint.subRegionRoot == (Object)targetPoint2.subRegionRoot)
							{
								if (targetPoint.param.vecSqrMagnitude > targetPoint2.param.vecSqrMagnitude)
								{
									targetPoint.param.isTargetEnable = false;
								}
								else
								{
									targetPoint2.param.isTargetEnable = false;
								}
							}
						}
					}
				}
			}
			for (int k = 0; k < numTargetInfo; k++)
			{
				Enemy enemy3 = targetInfoList[k].enemy;
				if ((Object)enemy3 == (Object)null)
				{
					if (self.spAttackType == SP_ATTACK_TYPE.SOUL)
					{
						targetList.Add(targetInfoList[k].targetPoint);
					}
				}
				else
				{
					TargetPoint targetPoint3 = targetInfoList[k].targetPoint;
					if (targetPoint3.param.isTargetEnable && targetPoint3.regionID >= 0 && targetPoint3.regionID < enemy3.regionWorks.Length && enemy3.regionInfos[targetPoint3.regionID].isAtkColliderHit)
					{
						if (self.spAttackType == SP_ATTACK_TYPE.HEAT)
						{
							if (enemy3.IsShadowSealingStuck(targetPoint3.regionID))
							{
								continue;
							}
						}
						else if (self.spAttackType == SP_ATTACK_TYPE.NONE && enemy3.IsMaxLvBleedFromSelf(targetPoint3.regionID))
						{
							continue;
						}
						targetList.Add(targetPoint3);
					}
				}
			}
		}
	}

	private TargetPoint DecideFinalTargetPoint(TargetPoint nextTargetPoint, TargetPoint nowTargetPoint, Player.ATTACK_MODE attackMode)
	{
		bool flag = false;
		if (targetingTime == 0f || Time.time - targetingTime >= parameter.changeAbleTime)
		{
			flag = true;
		}
		bool flag2 = true;
		bool flag3 = false;
		if ((Object)nextTargetPoint != (Object)null)
		{
			TargetPoint.Param param = nextTargetPoint.param;
			param.targetSelectCounter += Time.deltaTime;
			if (param.targetSelectCounter < parameter.selectAbleTime)
			{
				flag2 = false;
			}
			switch (param.weakState)
			{
			case Enemy.WEAK_STATE.WEAK:
				flag3 = true;
				break;
			case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
			case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
			case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
				if (param.weakSubParam == (int)attackMode)
				{
					flag3 = true;
				}
				break;
			}
		}
		bool flag4 = false;
		if (flag3)
		{
			flag4 = true;
		}
		flag3 = false;
		if ((Object)nowTargetPoint != (Object)null)
		{
			switch (nowTargetPoint.param.weakState)
			{
			case Enemy.WEAK_STATE.WEAK:
				flag3 = true;
				break;
			case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
			case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
			case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
				if (nowTargetPoint.param.weakSubParam == (int)attackMode)
				{
					flag3 = true;
				}
				break;
			}
		}
		bool flag5 = false;
		if (flag3)
		{
			flag5 = true;
			if (targetingWeakTime <= 0f)
			{
				targetingWeakTime = Time.time;
			}
			if (Time.time - targetingWeakTime >= parameter.changeWeakTime)
			{
				flag5 = false;
			}
		}
		else
		{
			targetingWeakTime = 0f;
		}
		bool flag6 = false;
		if (((flag && flag2) || flag4 || (Object)nowTargetPoint == (Object)null) && !flag5)
		{
			flag6 = true;
		}
		bool flag7 = false;
		if ((Object)nowTargetPoint != (Object)null)
		{
			Enemy enemy = nowTargetPoint.owner as Enemy;
			if ((Object)enemy != (Object)null && !enemy.isDead && enemy.enableTargetPoint)
			{
				flag7 = true;
			}
		}
		if ((!isTargetLock && flag6) || !flag7 || isTargetDisable)
		{
			if ((Object)nextTargetPoint != (Object)null)
			{
				if ((Object)nowTargetPoint != (Object)nextTargetPoint)
				{
					nowTargetPoint = nextTargetPoint;
					targetingTime = Time.time;
					targetingWeakTime = 0f;
				}
				nextTargetPoint.param.targetSelectCounter = 0f;
			}
			else
			{
				nowTargetPoint = null;
			}
			isTargetLock = false;
		}
		return nowTargetPoint;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Clear();
	}

	private void Clear()
	{
		markers.ForEach(delegate(TargetMarker o)
		{
			o.UnableMarker();
		});
		markers.Clear();
	}

	public void OnDetachedObject(StageObject stage_object)
	{
	}

	public void SetTargetLock(bool isLock, float changeTime = 0f)
	{
		if (isLock || isTargetLock == isLock || changeTime <= 0f)
		{
			isTargetLock = isLock;
			changeLockTime = 0f;
			changeLockFlag = false;
		}
		else if (!changeLockFlag)
		{
			changeLockTime = changeTime;
			changeLockFlag = true;
		}
		if (!isLock)
		{
			fieldMultiLockDic.Clear();
		}
	}

	public void SetTargetDisable(bool disable)
	{
		isTargetDisable = disable;
	}

	public static void ClearPoolObjects()
	{
	}

	public List<TargetMarker> GetTargetMarkerList()
	{
		return markers;
	}

	public int GetTargetMarkerNum()
	{
		if (markers == null)
		{
			return 0;
		}
		return markers.Count * MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockRegionMax;
	}

	public void HideMultiLock()
	{
		if (markers != null)
		{
			for (int i = 0; i < markers.Count; i++)
			{
				markers[i].HideMultiLock();
			}
		}
	}

	public void ResetMultiLock()
	{
		if (markers != null)
		{
			for (int i = 0; i < markers.Count; i++)
			{
				markers[i].ResetMultiLock();
			}
		}
	}

	public void EndMultiLockBoost()
	{
		if (markers != null)
		{
			int multiLockNum = GetMultiLockNum();
			bool flag = multiLockNum >= MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMax;
			for (int i = 0; i < markers.Count; i++)
			{
				markers[i].EndMultiLockBoost(flag);
			}
			MonoBehaviourSingleton<StageObjectManager>.I.self.SetBulletLineColor(flag);
		}
	}

	public int GetMultiLockNum()
	{
		if (markers == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < markers.Count; i++)
		{
			num += markers[i].GetMultiLockNum();
		}
		return num;
	}
}
