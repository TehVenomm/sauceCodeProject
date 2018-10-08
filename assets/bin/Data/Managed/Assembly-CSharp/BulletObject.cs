using System.Collections.Generic;
using UnityEngine;

public class BulletObject : IAttackCollider, IBulletObservable, StageObjectManager.IDetachedNotify
{
	private const float IS_LAND_HIT_MARGIN = 1f;

	public AtkAttribute masterAtk = new AtkAttribute();

	public SkillInfo.SkillParam masterSkill;

	public bool isShotArrow;

	public bool isAimBossMode;

	public bool isBossPierceArrow;

	private StageObject m_targetObject;

	protected AttackColliderProcessor colliderProcessor;

	protected BulletControllerBase controller;

	protected bool isColliderCreate;

	public AtkAttribute m_exAtk;

	public Player.ATTACK_MODE m_attackMode;

	private int m_endBulletSkillIndex = -1;

	protected CapsuleCollider capsuleCollider;

	protected bool isDestroyed;

	protected bool isLandHitDelete;

	protected Vector3 landHitPosition = Vector3.get_zero();

	protected Quaternion landHitRotation = Quaternion.get_identity();

	public Transform bulletEffect;

	protected AttackHitChecker attackHitChecker;

	protected bool m_isDisablePlayEndAnim;

	private int observedID;

	private List<IBulletObserver> bulletObserverList = new List<IBulletObserver>();

	public Transform _transform
	{
		get;
		protected set;
	}

	public Rigidbody _rigidbody
	{
		get;
		protected set;
	}

	public Collider _collider
	{
		get;
		protected set;
	}

	public StageObject stageObject
	{
		get;
		protected set;
	}

	public float timeCount
	{
		get;
		protected set;
	}

	public Vector3 endVec
	{
		get;
		private set;
	}

	public Vector3 prevPosition
	{
		get;
		protected set;
	}

	public bool HasEndBulletSkillIndex => m_endBulletSkillIndex > -1;

	public BulletData bulletData
	{
		get;
		private set;
	}

	protected BulletData.BULLET_TYPE type
	{
		get;
		private set;
	}

	protected Vector3 dispOffset
	{
		get;
		private set;
	}

	protected Vector3 dispRotation
	{
		get;
		private set;
	}

	protected Vector3 offset
	{
		get;
		private set;
	}

	protected Vector3 baseScale
	{
		get;
		private set;
	}

	protected float appearTime
	{
		get;
		private set;
	}

	protected Vector3 timeStartScale
	{
		get;
		private set;
	}

	protected Vector3 timeEndScale
	{
		get;
		private set;
	}

	protected BulletData endBullet
	{
		get;
		private set;
	}

	protected bool isCharacterHitDelete
	{
		get;
		private set;
	}

	protected bool isObjectHitDelete
	{
		get;
		private set;
	}

	protected bool isLandHit
	{
		get;
		private set;
	}

	protected string landHitEfect
	{
		get;
		private set;
	}

	protected bool isBulletTakeoverTarget
	{
		get;
		private set;
	}

	public float radius
	{
		get;
		private set;
	}

	public float capsuleHeight
	{
		get;
		private set;
	}

	public Vector3 boxSize
	{
		get;
		private set;
	}

	public Vector3 startColliderPos
	{
		get;
		private set;
	}

	public BulletObject()
		: this()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		prevPosition = Vector3.get_zero();
		dispOffset = Vector3.get_zero();
		dispRotation = Vector3.get_zero();
		offset = Vector3.get_zero();
		baseScale = Vector3.get_one();
		appearTime = 0f;
		radius = 0f;
		timeStartScale = Vector3.get_one();
		timeEndScale = Vector3.get_one();
		isCharacterHitDelete = true;
		isObjectHitDelete = true;
		isLandHit = false;
		isBulletTakeoverTarget = false;
		endVec = Vector3.get_zero();
		capsuleHeight = 0f;
		boxSize = Vector3.get_zero();
	}

	public void SetEndBulletSkillIndex(int skillIndex)
	{
		m_endBulletSkillIndex = skillIndex;
	}

	public void SetDisablePlayEndAnim()
	{
		m_isDisablePlayEndAnim = true;
	}

	protected virtual void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
		if (_rigidbody == null)
		{
			_rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
		}
		if (_collider == null)
		{
			_collider = this.get_gameObject().GetComponentInChildren<Collider>();
		}
		if (_collider != null)
		{
			_collider.set_isTrigger(true);
			capsuleCollider = (_collider as CapsuleCollider);
		}
		this.get_gameObject().SetActive(false);
		_rigidbody.set_useGravity(false);
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.AddNotifyInterface(this);
		}
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeSelf())
		{
			timeCount += Time.get_deltaTime();
			float num = timeCount / appearTime;
			if (appearTime < timeCount)
			{
				OnDestroy();
			}
			else
			{
				if (timeStartScale != timeEndScale)
				{
					Vector3 scale = Vector3.Lerp(timeStartScale, timeEndScale, num);
					SetScale(scale);
				}
				if (isLandHit)
				{
					Vector3 position = _transform.get_position();
					float num2 = StageManager.GetHeight(position);
					if (!isShotArrow)
					{
						num2 += radius;
					}
					if (num2 - 1f >= position.y)
					{
						Vector3 val = position - this.prevPosition;
						float num3 = 0f;
						if (val.y != 0f)
						{
							float num4 = num2;
							Vector3 prevPosition = this.prevPosition;
							num3 = (num4 - prevPosition.y) / val.y;
						}
						if (controller != null)
						{
							controller.OnLandHit();
						}
						isLandHitDelete = true;
						landHitPosition = val * num3 + this.prevPosition;
						landHitRotation = Quaternion.get_identity();
						position.y = 0f;
						OnDestroy();
						return;
					}
				}
			}
			if (isLandHitDelete && !isDestroyed)
			{
				OnDestroy();
			}
		}
	}

	protected virtual void FixedUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		endVec = Vector3.get_zero();
		if (capsuleHeight <= 0f)
		{
			float num = Vector3.Distance(prevPosition, _transform.get_position());
			float num2 = num;
			Vector3 localScale = _transform.get_localScale();
			num = num2 / localScale.x;
			if (num > 0f && capsuleCollider != null)
			{
				Vector3 offset = this.offset;
				offset.z -= num * 0.5f;
				capsuleCollider.set_center(offset);
				capsuleCollider.set_height(num + capsuleCollider.get_radius() * 2f);
				endVec = _transform.get_rotation() * (Vector3.get_back() * (num * 0.5f + capsuleCollider.get_radius()));
			}
		}
		prevPosition = _transform.get_position();
	}

	protected virtual bool IsLoopEnd()
	{
		return false;
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (isObjectHitDelete && (collider.get_gameObject().get_layer() == 9 || collider.get_gameObject().get_layer() == 21))
		{
			isLandHitDelete = true;
			landHitPosition = Utility.ClosestPointOnCollider(collider, prevPosition);
			landHitRotation = _transform.get_rotation() * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			_rigidbody.Sleep();
		}
		else
		{
			if (controller != null)
			{
				if (!controller.IsHit(collider))
				{
					return;
				}
				controller.OnHit(collider);
				if (controller.IsBreak(collider))
				{
					NotifyBroken();
					endBullet = null;
					OnDestroy();
					return;
				}
			}
			if (colliderProcessor != null)
			{
				colliderProcessor.OnTriggerEnter(collider);
			}
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (controller != null)
		{
			if (!controller.IsHit(collider))
			{
				return;
			}
			controller.OnHitStay(collider);
		}
		if (colliderProcessor != null)
		{
			colliderProcessor.OnTriggerStay(collider);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (colliderProcessor != null)
		{
			colliderProcessor.OnTriggerExit(collider);
		}
	}

	public virtual void OnDetachedObject(StageObject stage_object)
	{
		if (stageObject == stage_object)
		{
			stageObject = null;
		}
	}

	public virtual float GetTime()
	{
		return timeCount;
	}

	public virtual bool IsEnable()
	{
		return !isDestroyed;
	}

	public virtual void SortHitStackList(List<AttackHitColliderProcessor.HitResult> stack_list)
	{
		stack_list.Sort(delegate(AttackHitColliderProcessor.HitResult a, AttackHitColliderProcessor.HitResult b)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = a.target._position - prevPosition;
			float sqrMagnitude = val.get_sqrMagnitude();
			Vector3 val2 = b.target._position - prevPosition;
			float num = sqrMagnitude - val2.get_sqrMagnitude();
			return (num >= 0f) ? 1 : (-1);
		});
	}

	public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Bounds bounds = from_collider.get_bounds();
		Vector3 val = bounds.get_center();
		if (endVec != Vector3.get_zero())
		{
			val += endVec * 2f;
		}
		return val;
	}

	public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		if (attackHitChecker != null && !attackHitChecker.CheckHitAttack(info, to_collider, to_object))
		{
			return false;
		}
		return true;
	}

	public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if (attackHitChecker != null)
		{
			attackHitChecker.OnHitAttack(info, hit_param);
		}
		if (isCharacterHitDelete && hit_param.toObject is Character)
		{
			isLandHitDelete = false;
			OnDestroy();
		}
		else if ((isCharacterHitDelete || !isShotArrow) && isObjectHitDelete)
		{
			isLandHitDelete = true;
			landHitPosition = Utility.ClosestPointOnCollider(hit_param.toCollider, prevPosition);
			landHitRotation = _transform.get_rotation() * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			_rigidbody.Sleep();
		}
	}

	public AttackInfo GetAttackInfo()
	{
		return colliderProcessor.attackInfo;
	}

	public StageObject GetFromObject()
	{
		return colliderProcessor.fromObject;
	}

	public virtual void OnDestroy()
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		if (!AppMain.isApplicationQuit && !isDestroyed)
		{
			isDestroyed = true;
			if (_rigidbody != null)
			{
				_rigidbody.Sleep();
			}
			if (_collider != null)
			{
				_collider.set_enabled(false);
			}
			capsuleCollider = null;
			if (controller != null)
			{
				controller.set_enabled(false);
			}
			if (colliderProcessor != null)
			{
				if (endBullet != null)
				{
					CreateEndBullet();
				}
				colliderProcessor.OnDestroy();
				colliderProcessor = null;
			}
			if (isLandHitDelete && !string.IsNullOrEmpty(landHitEfect))
			{
				Transform effect = EffectManager.GetEffect(landHitEfect, null);
				if (effect != null)
				{
					effect.set_localPosition(landHitPosition);
					effect.set_localRotation(landHitRotation);
				}
			}
			Transform val = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
			if (bulletEffect != null)
			{
				bulletEffect.set_parent(val);
				EffectManager.ReleaseEffect(bulletEffect.get_gameObject(), !m_isDisablePlayEndAnim, false);
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				MonoBehaviourSingleton<StageObjectManager>.I.RemoveNotifyInterface(this);
			}
			NotifyDestroy();
			Object.Destroy(this.get_gameObject());
		}
	}

	private void CreateEndBullet()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			if (string.IsNullOrEmpty(endBullet.get_name()))
			{
				Log.Error("endBullet.name is empty, so can't create EndBullet!!");
			}
			else if (endBullet.type == BulletData.BULLET_TYPE.ICE_FLOOR)
			{
				Enemy enemy = stageObject as Enemy;
				if (!(enemy == null))
				{
					List<Vector3> list = new List<Vector3>(1);
					List<Quaternion> rotList = new List<Quaternion>();
					list.Add(_transform.get_position());
					enemy.ActCreateIceFloor(endBullet, list, rotList);
				}
			}
			else
			{
				AttackInfo endBulletAttackInfo = GetEndBulletAttackInfo();
				if (endBulletAttackInfo == null)
				{
					Log.Error("AttackInfo is null!!");
				}
				else
				{
					Transform transform = MonoBehaviourSingleton<StageObjectManager>.I._transform;
					if (transform == null)
					{
						Log.Error("parentTrans is null, so can't create EndBullet!!");
					}
					else if (type == BulletData.BULLET_TYPE.HIGH_EXPLOSIVE)
					{
						HighExplosiveSettings(endBulletAttackInfo, transform);
					}
					else
					{
						BulletObject bulletObject = ShotEndBullet(endBulletAttackInfo, transform);
						BulletData.BulletHoming dataHoming = endBullet.dataHoming;
						if (dataHoming != null && dataHoming.isTakeOverTarget && m_targetObject != null)
						{
							bulletObject.SetTarget(m_targetObject);
						}
						if (type == BulletData.BULLET_TYPE.BREAKABLE && bulletData.dataBreakable != null)
						{
							if (bulletData.dataBreakable.isTakeOverTarget && m_targetObject != null)
							{
								bulletObject.SetTarget(m_targetObject);
							}
							if (bulletData.dataBreakable.isTakeOverHitCount)
							{
								BulletControllerBreakable bulletControllerBreakable = controller as BulletControllerBreakable;
								BulletControllerBreakable bulletControllerBreakable2 = bulletObject.controller as BulletControllerBreakable;
								if (bulletControllerBreakable != null && bulletControllerBreakable2 != null)
								{
									bulletControllerBreakable2.SetHitCount(bulletControllerBreakable.GetHitCount());
								}
							}
						}
					}
				}
			}
		}
	}

	protected void HighExplosiveSettings(AttackInfo _atkInfo, Transform _parentTrans)
	{
		if (!(bulletData == null))
		{
			BulletData.BulletHighExplosive dataHighExplosive = bulletData.dataHighExplosive;
			if (dataHighExplosive != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				if (playerList != null && playerList.Count > 0)
				{
					switch (dataHighExplosive.targetingType)
					{
					case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS:
						for (int j = 0; j < playerList.Count; j++)
						{
							BulletObject bulletObject2 = ShotEndBullet(_atkInfo, _parentTrans);
							if (endBullet.dataHoming != null || endBullet.dataHealingHomingBullet != null)
							{
								bulletObject2.SetTarget(playerList[j]);
							}
						}
						break;
					case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS_EXCEPT_ME:
					{
						int i = 0;
						for (int count = playerList.Count; i < count; i++)
						{
							if (!(playerList[i] == stageObject))
							{
								BulletObject bulletObject = ShotEndBullet(_atkInfo, _parentTrans);
								if (endBullet.dataHoming != null || endBullet.dataHealingHomingBullet != null)
								{
									bulletObject.SetTarget(playerList[i]);
								}
							}
						}
						break;
					}
					}
				}
			}
		}
	}

	private List<StageObject> GetSortedListByPlayerDistance(List<StageObject> allPlayerList, StageObject me)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (allPlayerList == null || allPlayerList.Count < 1 || me == null)
		{
			return null;
		}
		List<StageObject> list = new List<StageObject>(allPlayerList);
		list.Remove(me);
		Vector3 targetPos = me.get_transform().get_position();
		list.Sort(delegate(StageObject a, StageObject b)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = a.get_transform().get_position() - targetPos;
			float magnitude = val.get_magnitude();
			Vector3 val2 = b.get_transform().get_position() - targetPos;
			return Mathf.RoundToInt(magnitude - val2.get_magnitude());
		});
		return list;
	}

	private BulletObject ShotEndBullet(AttackInfo atkInfo, Transform parentTrans)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Transform val = Utility.CreateGameObject(endBullet.get_name(), parentTrans, -1);
		if (val == null)
		{
			Log.Error("Failed to create Bullet!! name:" + endBullet.get_name());
			return null;
		}
		BulletObject bulletObject = val.get_gameObject().AddComponent(GetType()) as BulletObject;
		if (bulletObject == null)
		{
			Object.Destroy(val.get_gameObject());
			return null;
		}
		bulletObject.SetBaseScale(baseScale);
		if (masterSkill != null)
		{
			bulletObject.SetEndBulletSkillIndex(masterSkill.skillIndex);
		}
		bulletObject.Shot(stageObject, atkInfo, endBullet, _transform.get_position(), _transform.get_rotation(), null, false, m_exAtk, m_attackMode, null, null);
		bulletObject.attackHitChecker = attackHitChecker;
		if (isBulletTakeoverTarget)
		{
			bulletObject.SetTarget(m_targetObject);
		}
		return bulletObject;
	}

	private AttackInfo GetEndBulletAttackInfo()
	{
		AttackInfo attackInfo = colliderProcessor.attackInfo;
		if (attackInfo == null)
		{
			return null;
		}
		if (stageObject == null)
		{
			return attackInfo;
		}
		if (string.IsNullOrEmpty(attackInfo.nextBulletInfoName))
		{
			return attackInfo;
		}
		AttackInfo attackInfo2 = stageObject.FindAttackInfo(attackInfo.nextBulletInfoName, true, false);
		if (attackInfo2 == null)
		{
			Log.Error("Not found AttackInfo for EndBullet!! nextBulletInfoName:" + attackInfo.nextBulletInfoName);
			return attackInfo;
		}
		return attackInfo2;
	}

	public void SetBaseScale(Vector3 _scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		baseScale = _scale;
	}

	public void SetRadius(float _radius)
	{
		radius = _radius;
		if (isColliderCreate && capsuleCollider != null)
		{
			if (radius <= 0f)
			{
				Object.Destroy(_collider);
				_collider = null;
			}
			else
			{
				capsuleCollider.set_radius(radius);
				capsuleCollider.set_height(radius * 2f);
			}
		}
	}

	public void SetScale(Vector3 scale)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		_transform.set_localScale(Vector3.Scale(baseScale, scale));
	}

	public void SetHitOffset(Vector3 _offset)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		offset = _offset;
		if (capsuleCollider != null)
		{
			capsuleCollider.set_center(_offset);
		}
	}

	public void SetCapsuleAxis(BulletData.AXIS axis)
	{
		if (isColliderCreate && capsuleCollider != null)
		{
			int direction = 2;
			if (axis != BulletData.AXIS.NONE)
			{
				direction = (int)axis;
			}
			capsuleCollider.set_direction(direction);
		}
	}

	public void SetCapsuleHeight(float height)
	{
		if (isColliderCreate && !(height <= 0f) && capsuleCollider != null)
		{
			capsuleCollider.set_height(height);
			capsuleHeight = height;
		}
	}

	public virtual void Shot(StageObject master, AttackInfo atkInfo, BulletData bulletData, Vector3 pos, Quaternion rot, string exEffectName = null, bool reference_attack = true, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null, SkillInfo.SkillParam exSkillParam = null)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		Player player = master as Player;
		this.get_gameObject().SetActive(true);
		stageObject = master;
		m_exAtk = exAtk;
		m_attackMode = attackMode;
		string text = bulletData.data.GetEffectName(player);
		if (!string.IsNullOrEmpty(exEffectName))
		{
			text = exEffectName;
		}
		if (!string.IsNullOrEmpty(text))
		{
			bulletEffect = EffectManager.GetEffect(text, _transform);
			if (bulletEffect != null)
			{
				bulletEffect.set_localPosition(bulletData.data.dispOffset);
				bulletEffect.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
				bulletEffect.set_localScale(Vector3.get_one());
			}
		}
		AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
		if (exAtk != null)
		{
			masterAtk = exAtk;
		}
		else if (attackHitInfo != null)
		{
			if (player != null && HasEndBulletSkillIndex)
			{
				int skillIndex = player.skillInfo.skillIndex;
				player.skillInfo.skillIndex = m_endBulletSkillIndex;
				master.GetAtk(attackHitInfo, ref masterAtk);
				player.skillInfo.skillIndex = skillIndex;
			}
			else
			{
				master.GetAtk(attackHitInfo, ref masterAtk);
			}
		}
		masterSkill = null;
		if (player != null)
		{
			if (exSkillParam != null)
			{
				masterSkill = exSkillParam;
			}
			else
			{
				masterSkill = player.skillInfo.actSkillParam;
				if (player.TrackingTargetBullet != null && player.TrackingTargetBullet.IsReplaceSkill && atkInfo.isSkillReference)
				{
					masterSkill = player.TrackingTargetBullet.SkillParamForBullet;
				}
				if (HasEndBulletSkillIndex)
				{
					masterSkill = player.GetSkillParam(m_endBulletSkillIndex);
				}
			}
		}
		if (bulletData.data.isEmitGround)
		{
			pos.y = 0f;
		}
		SetBulletData(bulletData, masterSkill, pos, rot);
		if (bulletData.type == BulletData.BULLET_TYPE.OBSTACLE)
		{
			AttackObstacle attackObstacle = this.get_gameObject().AddComponent<AttackObstacle>();
			attackObstacle.Initialize(this as AnimEventShot, bulletData.dataObstacle.colliderStartTime);
		}
		else if (bulletData.type != BulletData.BULLET_TYPE.HEALING_HOMING)
		{
			int layer = (!(master is Player)) ? 15 : 14;
			Utility.SetLayerWithChildren(_transform, layer);
		}
		timeCount = 0f;
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(atkInfo, stageObject, _collider, this, attackMode, damageDistanceData);
			if (reference_attack)
			{
				attackHitChecker = stageObject.ReferenceAttackHitChecker();
			}
			if (bulletData.type == BulletData.BULLET_TYPE.SNATCH || bulletData.type == BulletData.BULLET_TYPE.PAIR_SWORDS_LASER)
			{
				colliderProcessor.ValidTriggerStay();
			}
		}
		Vector3 val = Vector3.get_zero();
		if (_collider is BoxCollider)
		{
			val = (_collider as BoxCollider).get_center();
		}
		else if (_collider is SphereCollider)
		{
			val = (_collider as SphereCollider).get_center();
		}
		else if (_collider is CapsuleCollider)
		{
			val = (_collider as CapsuleCollider).get_center();
		}
		startColliderPos = _transform.get_position() + val;
		isDestroyed = false;
		prevPosition = pos;
		if (controller != null)
		{
			controller.OnShot();
		}
	}

	protected virtual void SetBulletData(BulletData bullet, SkillInfo.SkillParam _skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		if (!(bullet == null))
		{
			bulletData = bullet;
			type = bullet.type;
			appearTime = bullet.data.appearTime;
			dispOffset = bulletData.data.dispOffset;
			dispRotation = bulletData.data.dispRotation;
			SetRadius(bullet.data.radius);
			SetCapsuleHeight(bullet.data.capsuleHeight);
			SetCapsuleAxis(bullet.data.capsuleAxis);
			SetScale(bullet.data.timeStartScale);
			SetHitOffset(bullet.data.hitOffset);
			timeStartScale = bullet.data.timeStartScale;
			timeEndScale = bullet.data.timeEndScale;
			isCharacterHitDelete = bullet.data.isCharacterHitDelete;
			isObjectHitDelete = bullet.data.isObjectHitDelete;
			isLandHit = bullet.data.isLandHit;
			landHitEfect = bullet.data.landHiteffectName;
			endBullet = bullet.data.endBullet;
			isBulletTakeoverTarget = bullet.data.isBulletTakeoverTarget;
			switch (bullet.type)
			{
			case BulletData.BULLET_TYPE.FALL:
				controller = this.get_gameObject().AddComponent<BulletControllerFall>();
				break;
			case BulletData.BULLET_TYPE.HOMING:
				controller = this.get_gameObject().AddComponent<BulletControllerHoming>();
				break;
			case BulletData.BULLET_TYPE.CURVE:
				controller = this.get_gameObject().AddComponent<BulletControllerCurve>();
				break;
			case BulletData.BULLET_TYPE.BREAKABLE:
				controller = this.get_gameObject().AddComponent<BulletControllerBreakable>();
				break;
			case BulletData.BULLET_TYPE.OBSTACLE_CYLINDER:
				controller = this.get_gameObject().AddComponent<BulletControllerObstacleCylinder>();
				break;
			case BulletData.BULLET_TYPE.SNATCH:
				controller = this.get_gameObject().AddComponent<BulletControllerSnatch>();
				break;
			case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
				controller = this.get_gameObject().AddComponent<BulletControllerPairSwordsSoul>();
				break;
			case BulletData.BULLET_TYPE.PAIR_SWORDS_LASER:
				controller = this.get_gameObject().AddComponent<BulletControllerPairSwordsLaser>();
				break;
			case BulletData.BULLET_TYPE.HEALING_HOMING:
				controller = this.get_gameObject().AddComponent<BulletControllerHealingHoming>();
				break;
			case BulletData.BULLET_TYPE.ARROW_SOUL:
				controller = this.get_gameObject().AddComponent<BulletControllerArrowSoul>();
				break;
			default:
				controller = this.get_gameObject().AddComponent<BulletControllerBase>();
				break;
			}
			if (controller != null)
			{
				controller.Initialize(bullet, _skillParam, pos, rot);
				controller.RegisterBulletObject(this);
				switch (bullet.type)
				{
				case BulletData.BULLET_TYPE.SNATCH:
				{
					BulletControllerSnatch bulletControllerSnatch = controller as BulletControllerSnatch;
					if (bulletControllerSnatch != null)
					{
						bulletControllerSnatch.SetBulletObject(this);
						bulletControllerSnatch.SetFromObject(stageObject);
					}
					break;
				}
				case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
				{
					IObservable observable = controller as IObservable;
					if (observable != null)
					{
						Player player = stageObject as Player;
						if (player != null)
						{
							observable.RegisterObserver(player.pairSwordsCtrl);
						}
					}
					break;
				}
				case BulletData.BULLET_TYPE.BREAKABLE:
					RegisterObserver();
					break;
				}
			}
			Character character = stageObject as Character;
			if (character != null)
			{
				SetTarget(character.actionTarget);
			}
		}
	}

	public void SetTarget(StageObject obj)
	{
		m_targetObject = obj;
		BulletControllerHoming bulletControllerHoming = controller as BulletControllerHoming;
		if (bulletControllerHoming != null)
		{
			bulletControllerHoming.SetTarget(obj);
		}
		BulletControllerBreakable bulletControllerBreakable = controller as BulletControllerBreakable;
		if (bulletControllerBreakable != null)
		{
			bulletControllerBreakable.SetTarget(obj);
		}
	}

	public void SetTarget(Transform trans)
	{
		BulletControllerArrowSoul bulletControllerArrowSoul = controller as BulletControllerArrowSoul;
		if (!(bulletControllerArrowSoul == null))
		{
			bulletControllerArrowSoul.SetTarget(trans);
		}
	}

	public void SetPuppetTargetPos(Vector3 pos)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		BulletControllerArrowSoul bulletControllerArrowSoul = controller as BulletControllerArrowSoul;
		if (!(bulletControllerArrowSoul == null))
		{
			bulletControllerArrowSoul.SetPuppetTargetPos(pos);
		}
	}

	public void EndBossPierceArrow()
	{
		isBossPierceArrow = false;
		isCharacterHitDelete = false;
	}

	public int GetObservedID()
	{
		return observedID;
	}

	public void SetObservedID(int id)
	{
		observedID = id;
	}

	public void RegisterObserver()
	{
		if (!bulletObserverList.Contains(stageObject))
		{
			bulletObserverList.Add(stageObject);
			SetObservedID(stageObject.GetObservedID());
			stageObject.RegisterObservable(this);
		}
	}

	public void NotifyBroken()
	{
		for (int i = 0; i < bulletObserverList.Count; i++)
		{
			bulletObserverList[i].OnBreak(observedID);
		}
	}

	public void NotifyDestroy()
	{
		for (int i = 0; i < bulletObserverList.Count; i++)
		{
			bulletObserverList[i].OnBulletDestroy(observedID);
		}
	}

	public void ForceBreak()
	{
		endBullet = null;
		OnDestroy();
	}
}
