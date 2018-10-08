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
		if (self == null)
		{
			Clear();
		}
		else
		{
			Function func = m_func;
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			func = ((!self.IsOnCannonMode() || !(boss != null) || !boss.IsValidShield()) ? Function.Basis : Function.Cannon);
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
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarker;
		}
		else
		{
			parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerLesserEnemies;
		}
		if (changeLockFlag)
		{
			changeLockTime -= Time.get_deltaTime();
			if (changeLockTime <= 0f)
			{
				changeLockTime = 0f;
				changeLockFlag = false;
				isTargetLock = !isTargetLock;
			}
		}
		bool flag = false;
		Vector3 val = self._position;
		if (self.isArrowAimLesserMode)
		{
			if (self.isArrowAimEnd)
			{
				flag = true;
			}
			else
			{
				parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerArrowAimLesser;
				val += self.arrowAimLesserCursorPos;
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
					if (!(animEventShot == null) && !(animEventShot.targetPoint == null))
					{
						RegisterTargetInfo(animEventShot.targetPoint, null);
					}
				}
			}
		}
		for (int l = 0; l < MonoBehaviourSingleton<InGameManager>.I.dropItemList.Count; l++)
		{
			FieldDropObject fieldDropObject = MonoBehaviourSingleton<InGameManager>.I.dropItemList[l];
			if (fieldDropObject.targetPoint != null && fieldDropObject.get_gameObject().get_activeInHierarchy())
			{
				RegisterTargetInfo(fieldDropObject.targetPoint, null);
			}
		}
		int count2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count;
		for (int m = 0; m < count2; m++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[m] as Player;
			if (!(player == null) && !player.isDead)
			{
				TargetPoint restraintTargetPoint = player.RestraintTargetPoint;
				if (restraintTargetPoint != null)
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
			Vector2 val2 = val.ToVector2XZ();
			Vector2 forwardXZ = self.forwardXZ;
			forwardXZ.Normalize();
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Quaternion rotation = cameraTransform.get_rotation();
			Vector3 position = cameraTransform.get_position();
			Vector2 val3 = position.ToVector2XZ();
			Vector3 val4 = Vector2.op_Implicit(cameraTransform.get_forward().ToVector2XZ());
			val4.Normalize();
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
				if (targetPoint4.get_enabled() && targetPoint4.get_gameObject().get_activeInHierarchy())
				{
					if (self.isArrowAimBossMode)
					{
						if (!self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) && !targetPoint4.isAimEnable)
						{
							continue;
						}
					}
					else if (!targetPoint4.isTargetEnable)
					{
						continue;
					}
					if (enemy2 != null && targetPoint4.regionID >= 0 && targetPoint4.regionID < enemy2.regionWorks.Length)
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
					param.isTargetEnable = true;
					Vector3 targetPoint5 = targetPoint4.GetTargetPoint();
					Vector2 val5 = targetPoint5.ToVector2XZ();
					Vector2 val6 = val5 - val2;
					TargetPoint.Param param2 = param;
					Vector3 val7 = position - targetPoint5;
					param2.markerPos = val7.get_normalized() * targetPoint4.scaledMarkerZShift + targetPoint5;
					param.markerRot = rotation;
					param.targetPos = targetPoint5;
					bool flag2 = false;
					if (param.weakState != 0 && param.weakSubParam != 0)
					{
						flag2 = true;
					}
					float sqrMagnitude = val6.get_sqrMagnitude();
					param.isShowRange = (sqrMagnitude < num5 || flag2);
					param.vecSqrMagnitude = sqrMagnitude;
					if (!isTargetDisable)
					{
						if (isAutoMode && !(targetPoint4.owner is Player))
						{
							if (targetPoint3 == null)
							{
								targetPoint3 = targetPoint4;
								num9 = sqrMagnitude;
							}
							else if (targetPoint4.owner == null)
							{
								if (targetPoint3.owner == null)
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
							Vector2 val8 = val5 - val3;
							float num10 = Mathf.Acos(Vector2.Dot(Vector2.op_Implicit(val4), val8.get_normalized()));
							if (!(num10 > num3))
							{
								bool flag3 = false;
								if (parameter.enableCameraCulling && !flag)
								{
									float cameraCullingMargin = parameter.cameraCullingMargin;
									Vector3 val9 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToViewportPoint(targetPoint5);
									if (val9.x < 0f - cameraCullingMargin || val9.x > 1f + cameraCullingMargin || val9.y < 0f - cameraCullingMargin || val9.y > 1f + cameraCullingMargin || val9.z < 0f)
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
									Vector3 val10 = val - targetPoint5;
									sqrMagnitude = val10.get_magnitude();
									sqrMagnitude += targetPoint4.weight;
									if (targetPoint5.y >= 0f && targetPoint5.y < self.GetIgnoreTargetHeight())
									{
										if (targetPoint2 == null || sqrMagnitude < num8)
										{
											targetPoint2 = targetPoint4;
											num8 = sqrMagnitude;
										}
										if (num10 <= num6 && (targetPoint == null || sqrMagnitude < num7))
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
			if (targetPoint == null && targetPoint2 != null)
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
				if (targetPoint6 != null)
				{
					self.targetingPointList.Add(targetPoint6);
					self.SetActionTarget(targetPoint6.owner, true);
					if (isAutoMode)
					{
						AutoSelfController autoSelfController2 = self.controller as AutoSelfController;
						if (targetPoint6 != null)
						{
							if (targetPoint6.owner == null)
							{
								autoSelfController2.actionTargetPoint = targetPoint6;
							}
							else if (targetPoint3.owner == null)
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
				if (targetPoint7 != targetPoint)
				{
					targetPoint7.param.targetSelectCounter -= Time.get_deltaTime();
					if (targetPoint7.param.targetSelectCounter < 0f)
					{
						targetPoint7.param.targetSelectCounter = 0f;
					}
				}
				if (targetPoint7.get_enabled() && targetPoint7.get_gameObject().get_activeInHierarchy() && showMarker)
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
					if (enemy3 != null && targetPoint7.regionID >= 0 && targetPoint7.regionID < enemy3.regionWorks.Length)
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
							if (markersTemp[num14].point == updateParam.targetPoint)
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
							if (self.isArrowAimLesserMode && self.spAttackType == SP_ATTACK_TYPE.SOUL && targetMarker.point == self.targetingPoint)
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
			if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null && updateShadowSealingFlag)
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
				Debug.LogWarning((object)"Not found RegionWork.");
			}
			else
			{
				TargetPoint targetPoint = boss.SearchTargetPoint(enemyRegionWork.regionId);
				if (targetPoint == null)
				{
					Debug.LogWarning((object)"Not found TargetPoint.");
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
		if (!(boss == null) && !boss.isDead)
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
					if (targetPoint == null)
					{
						Debug.LogWarning((object)"Not found TargetPoint.");
					}
					else
					{
						if (m_grabMarker == null)
						{
							m_grabMarker = new TargetMarker(base._transform);
						}
						m_grabMarker.UpdateByTargetPoint(targetPoint, "ef_btl_target_cannon_03");
						if (uiGrabStatusGizmo == null)
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
		if (uiGrabStatusGizmo != null)
		{
			uiGrabStatusGizmo.targetEnemy = null;
			uiGrabStatusGizmo.targetPoint = null;
			uiGrabStatusGizmo = null;
		}
	}

	private void MakeTargetPointListForArrowAimBossMode(List<TargetPoint> targetList)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (!(self == null))
		{
			for (int i = 0; i < numTargetInfo; i++)
			{
				TargetPoint targetPoint = targetInfoList[i].targetPoint;
				if (targetPoint.param.isTargetEnable && !(targetPoint.subRegionRoot == null))
				{
					Enemy enemy = targetInfoList[i].enemy;
					if (!(enemy == null))
					{
						for (int j = i + 1; j < numTargetInfo; j++)
						{
							TargetPoint targetPoint2 = targetInfoList[j].targetPoint;
							Enemy enemy2 = targetInfoList[j].enemy;
							if (enemy2 != enemy)
							{
								break;
							}
							if (targetPoint2.param.isTargetEnable && !(targetPoint2.subRegionRoot == null) && targetPoint.subRegionRoot == targetPoint2.subRegionRoot)
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
				if (enemy3 == null)
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
		if (targetingTime == 0f || Time.get_time() - targetingTime >= parameter.changeAbleTime)
		{
			flag = true;
		}
		bool flag2 = true;
		bool flag3 = false;
		if (nextTargetPoint != null)
		{
			TargetPoint.Param param = nextTargetPoint.param;
			param.targetSelectCounter += Time.get_deltaTime();
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
		if (nowTargetPoint != null)
		{
			switch (nowTargetPoint.param.weakState)
			{
			case Enemy.WEAK_STATE.WEAK:
				flag3 = true;
				break;
			case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
			case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
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
				targetingWeakTime = Time.get_time();
			}
			if (Time.get_time() - targetingWeakTime >= parameter.changeWeakTime)
			{
				flag5 = false;
			}
		}
		else
		{
			targetingWeakTime = 0f;
		}
		bool flag6 = false;
		if (((flag && flag2) || flag4 || nowTargetPoint == null) && !flag5)
		{
			flag6 = true;
		}
		bool flag7 = false;
		if (nowTargetPoint != null)
		{
			Enemy enemy = nowTargetPoint.owner as Enemy;
			if (enemy != null && !enemy.isDead && enemy.enableTargetPoint)
			{
				flag7 = true;
			}
		}
		if ((!isTargetLock && flag6) || !flag7 || isTargetDisable)
		{
			if (nextTargetPoint != null)
			{
				if (nowTargetPoint != nextTargetPoint)
				{
					nowTargetPoint = nextTargetPoint;
					targetingTime = Time.get_time();
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
