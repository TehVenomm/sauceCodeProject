using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour, IAttackCollider, StageObjectManager.IDetachedNotify, IBulletObservable
{
	public AtkAttribute masterAtk = new AtkAttribute();

	public SkillInfo.SkillParam masterSkill;

	public bool isShotArrow;

	public bool isAimMode;

	public bool isBossPierceArrow;

	private StageObject m_targetObject;

	private const float IS_LAND_HIT_MARGIN = 1f;

	protected AttackColliderProcessor colliderProcessor;

	protected BulletControllerBase controller;

	protected bool isColliderCreate;

	public AtkAttribute m_exAtk;

	public Player.ATTACK_MODE m_attackMode;

	private int m_endBulletSkillIndex = -1;

	protected CapsuleCollider capsuleCollider;

	protected bool isDestroyed;

	protected bool isLandHitDelete;

	protected Vector3 landHitPosition = Vector3.zero;

	protected Quaternion landHitRotation = Quaternion.identity;

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

	public void SetEndBulletSkillIndex(int skillIndex)
	{
		m_endBulletSkillIndex = skillIndex;
	}

	public void SetDisablePlayEndAnim()
	{
		m_isDisablePlayEndAnim = true;
	}

	public BulletObject()
	{
		prevPosition = Vector3.zero;
		dispOffset = Vector3.zero;
		dispRotation = Vector3.zero;
		offset = Vector3.zero;
		baseScale = Vector3.one;
		appearTime = 0f;
		radius = 0f;
		timeStartScale = Vector3.one;
		timeEndScale = Vector3.one;
		isCharacterHitDelete = true;
		isObjectHitDelete = true;
		isLandHit = false;
		isBulletTakeoverTarget = false;
		endVec = Vector3.zero;
		capsuleHeight = 0f;
		boxSize = Vector3.zero;
	}

	protected virtual void Awake()
	{
		_transform = base.transform;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		if (_rigidbody == null)
		{
			_rigidbody = base.gameObject.AddComponent<Rigidbody>();
		}
		if (_collider == null)
		{
			_collider = base.gameObject.GetComponentInChildren<Collider>();
		}
		if (_collider != null)
		{
			_collider.isTrigger = true;
			capsuleCollider = (_collider as CapsuleCollider);
		}
		base.gameObject.SetActive(value: false);
		_rigidbody.useGravity = false;
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
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		timeCount += Time.deltaTime;
		float t = timeCount / appearTime;
		if (appearTime >= 0f && appearTime < timeCount)
		{
			OnDestroy();
		}
		else
		{
			if (timeStartScale != timeEndScale)
			{
				Vector3 scale = Vector3.Lerp(timeStartScale, timeEndScale, t);
				SetScale(scale);
			}
			if (isLandHit)
			{
				Vector3 position = _transform.position;
				float num = StageManager.GetHeight(position);
				if (!isShotArrow)
				{
					num += radius;
				}
				if (num - 1f >= position.y)
				{
					Vector3 a = position - prevPosition;
					float d = 0f;
					if (a.y != 0f)
					{
						d = (num - prevPosition.y) / a.y;
					}
					if (controller != null)
					{
						controller.OnLandHit();
					}
					isLandHitDelete = true;
					landHitPosition = a * d + prevPosition;
					landHitRotation = Quaternion.identity;
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

	protected virtual void FixedUpdate()
	{
		endVec = Vector3.zero;
		if (capsuleHeight <= 0f)
		{
			float num = Vector3.Distance(prevPosition, _transform.position);
			num /= _transform.localScale.x;
			if (num > 0f && capsuleCollider != null)
			{
				Vector3 offset = this.offset;
				offset.z -= num * 0.5f;
				capsuleCollider.center = offset;
				capsuleCollider.height = num + capsuleCollider.radius * 2f;
				endVec = _transform.rotation * (Vector3.back * (num * 0.5f + capsuleCollider.radius));
			}
		}
		prevPosition = _transform.position;
	}

	protected virtual bool IsLoopEnd()
	{
		return false;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (isObjectHitDelete && (collider.gameObject.layer == 9 || collider.gameObject.layer == 21))
		{
			isLandHitDelete = true;
			landHitPosition = Utility.ClosestPointOnCollider(collider, prevPosition);
			landHitRotation = _transform.rotation * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			_rigidbody.Sleep();
			return;
		}
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

	public void OnTriggerExit(Collider collider)
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
		stack_list.Sort((AttackHitColliderProcessor.HitResult a, AttackHitColliderProcessor.HitResult b) => ((a.target._position - prevPosition).sqrMagnitude - (b.target._position - prevPosition).sqrMagnitude >= 0f) ? 1 : (-1));
	}

	public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
	{
		Vector3 center = from_collider.bounds.center;
		if (endVec != Vector3.zero)
		{
			center += endVec * 2f;
		}
		return center;
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
		if (attackHitChecker != null)
		{
			attackHitChecker.OnHitAttack(info, hit_param);
		}
		if (isCharacterHitDelete && hit_param.toObject is Character)
		{
			isLandHitDelete = false;
			OnDestroy();
		}
		else if ((isCharacterHitDelete || !isShotArrow) && (!(hit_param.fromObject is Player) || !(hit_param.toObject is BarrierBulletObject)) && isObjectHitDelete)
		{
			isLandHitDelete = true;
			landHitPosition = Utility.ClosestPointOnCollider(hit_param.toCollider, prevPosition);
			landHitRotation = _transform.rotation * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
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
		if (AppMain.isApplicationQuit || isDestroyed)
		{
			return;
		}
		isDestroyed = true;
		if (_rigidbody != null)
		{
			_rigidbody.Sleep();
		}
		if (_collider != null)
		{
			_collider.enabled = false;
		}
		capsuleCollider = null;
		if (controller != null)
		{
			controller.DestroyBulletObject();
			controller.enabled = false;
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
			Transform effect = EffectManager.GetEffect(landHitEfect);
			if (effect != null)
			{
				effect.localPosition = landHitPosition;
				effect.localRotation = landHitRotation;
			}
		}
		Transform parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
		if (bulletEffect != null)
		{
			bulletEffect.parent = parent;
			EffectManager.ReleaseEffect(bulletEffect.gameObject, !m_isDisablePlayEndAnim);
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveNotifyInterface(this);
		}
		NotifyDestroy();
		Object.Destroy(base.gameObject);
	}

	private void CreateEndBullet()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		if (string.IsNullOrEmpty(endBullet.name))
		{
			Log.Error("endBullet.name is empty, so can't create EndBullet!!");
			return;
		}
		if (endBullet.type == BulletData.BULLET_TYPE.ICE_FLOOR)
		{
			Enemy enemy = stageObject as Enemy;
			if (!(enemy == null))
			{
				List<Vector3> list = new List<Vector3>(1);
				List<Quaternion> rotList = new List<Quaternion>();
				list.Add(_transform.position);
				enemy.ActCreateIceFloor(endBullet, list, rotList);
			}
			return;
		}
		if (endBullet.IsDecoy())
		{
			CreateEndBulletDecoy();
			return;
		}
		AttackInfo endBulletAttackInfo = GetEndBulletAttackInfo();
		if (endBulletAttackInfo == null)
		{
			Log.Error("AttackInfo is null!!");
		}
		else
		{
			if (stageObject == null)
			{
				return;
			}
			Transform transform = MonoBehaviourSingleton<StageObjectManager>.I._transform;
			if (transform == null)
			{
				Log.Error("parentTrans is null, so can't create EndBullet!!");
				return;
			}
			if (type == BulletData.BULLET_TYPE.HIGH_EXPLOSIVE)
			{
				HighExplosiveSettings(endBulletAttackInfo, transform);
				return;
			}
			BulletObject bulletObject = ShotEndBullet(endBulletAttackInfo, transform);
			BulletData.BulletHoming dataHoming = endBullet.dataHoming;
			if (dataHoming != null && dataHoming.isTakeOverTarget && m_targetObject != null)
			{
				bulletObject.SetTarget(m_targetObject);
			}
			if (type != BulletData.BULLET_TYPE.BREAKABLE || bulletData.dataBreakable == null)
			{
				return;
			}
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

	private void CreateEndBulletDecoy()
	{
		Self self = stageObject as Self;
		if (!(self == null))
		{
			AnimEventData.EventData eventData = new AnimEventData.EventData();
			eventData.stringArgs = new string[1]
			{
				endBullet.name
			};
			eventData.floatArgs = new float[3]
			{
				_transform.position.x,
				0f,
				_transform.position.z
			};
			eventData.intArgs = new int[1]
			{
				(masterSkill != null) ? masterSkill.skillIndex : (-1)
			};
			self.EventShotDecoy(eventData);
		}
	}

	protected void HighExplosiveSettings(AttackInfo _atkInfo, Transform _parentTrans)
	{
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletHighExplosive dataHighExplosive = bulletData.dataHighExplosive;
		if (dataHighExplosive == null || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
		if (playerList == null || playerList.Count <= 0)
		{
			return;
		}
		switch (dataHighExplosive.targetingType)
		{
		case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS:
		{
			for (int j = 0; j < playerList.Count; j++)
			{
				BulletObject bulletObject2 = ShotEndBullet(_atkInfo, _parentTrans);
				if (endBullet.dataHoming != null || endBullet.dataHealingHomingBullet != null || endBullet.dataResurrectionHomingBullet != null)
				{
					bulletObject2.SetTarget(playerList[j]);
				}
			}
			break;
		}
		case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS_EXCEPT_ME:
		{
			int i = 0;
			for (int count = playerList.Count; i < count; i++)
			{
				if (!(playerList[i] == stageObject))
				{
					BulletObject bulletObject = ShotEndBullet(_atkInfo, _parentTrans);
					if (endBullet.dataHoming != null || endBullet.dataHealingHomingBullet != null || endBullet.dataResurrectionHomingBullet != null)
					{
						bulletObject.SetTarget(playerList[i]);
					}
				}
			}
			break;
		}
		}
	}

	private List<StageObject> GetSortedListByPlayerDistance(List<StageObject> allPlayerList, StageObject me)
	{
		if (allPlayerList == null || allPlayerList.Count < 1 || me == null)
		{
			return null;
		}
		List<StageObject> list = new List<StageObject>(allPlayerList);
		list.Remove(me);
		Vector3 targetPos = me.transform.position;
		list.Sort((StageObject a, StageObject b) => Mathf.RoundToInt((a.transform.position - targetPos).magnitude - (b.transform.position - targetPos).magnitude));
		return list;
	}

	private BulletObject ShotEndBullet(AttackInfo atkInfo, Transform parentTrans)
	{
		Transform transform = Utility.CreateGameObject(endBullet.name, parentTrans);
		if (transform == null)
		{
			Log.Error("Failed to create Bullet!! name:" + endBullet.name);
			return null;
		}
		BulletObject bulletObject = transform.gameObject.AddComponent(GetType()) as BulletObject;
		if (bulletObject == null)
		{
			Object.Destroy(transform.gameObject);
			return null;
		}
		bulletObject.SetBaseScale(baseScale);
		if (masterSkill != null)
		{
			bulletObject.SetEndBulletSkillIndex(masterSkill.skillIndex);
		}
		bulletObject.Shot(stageObject, atkInfo, endBullet, _transform.position, _transform.rotation, null, reference_attack: false, m_exAtk, m_attackMode);
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
		AttackInfo attackInfo2 = stageObject.FindAttackInfo(attackInfo.nextBulletInfoName);
		if (attackInfo2 == null)
		{
			Log.Error("Not found AttackInfo for EndBullet!! nextBulletInfoName:" + attackInfo.nextBulletInfoName);
			return attackInfo;
		}
		return attackInfo2;
	}

	public void SetBaseScale(Vector3 _scale)
	{
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
				capsuleCollider.radius = radius;
				capsuleCollider.height = radius * 2f;
			}
		}
	}

	public void SetScale(Vector3 scale)
	{
		_transform.localScale = Vector3.Scale(baseScale, scale);
	}

	public void SetHitOffset(Vector3 _offset)
	{
		offset = _offset;
		if (capsuleCollider != null)
		{
			capsuleCollider.center = _offset;
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
			capsuleCollider.direction = direction;
		}
	}

	public void SetCapsuleHeight(float height)
	{
		if (isColliderCreate && !(height <= 0f) && capsuleCollider != null)
		{
			capsuleCollider.height = height;
			capsuleHeight = height;
		}
	}

	public virtual void Shot(StageObject master, AttackInfo atkInfo, BulletData bulletData, Vector3 pos, Quaternion rot, string exEffectName = null, bool reference_attack = true, AtkAttribute exAtk = null, Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE, DamageDistanceTable.DamageDistanceData damageDistanceData = null, SkillInfo.SkillParam exSkillParam = null)
	{
		Player player = master as Player;
		base.gameObject.SetActive(value: true);
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
				bulletEffect.localPosition = bulletData.data.dispOffset;
				bulletEffect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
				bulletEffect.localScale = Vector3.one;
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
			base.gameObject.AddComponent<AttackObstacle>().Initialize(this as AnimEventShot, bulletData.dataObstacle.colliderStartTime);
		}
		else if (bulletData.type == BulletData.BULLET_TYPE.BARRIER)
		{
			base.gameObject.AddComponent<BarrierBulletObject>().Initialize(this);
		}
		else if (bulletData.type != BulletData.BULLET_TYPE.HEALING_HOMING && bulletData.type != BulletData.BULLET_TYPE.ENEMY_PRESENT && bulletData.type != BulletData.BULLET_TYPE.SPEAR_BARRIER && bulletData.type != BulletData.BULLET_TYPE.RESURRECTION_HOMING)
		{
			int layer = (master is Player) ? 14 : 15;
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
			if (bulletData.type == BulletData.BULLET_TYPE.CRASH_BIT || (attackHitInfo != null && attackHitInfo.isValidTriggerStay))
			{
				colliderProcessor.ValidTriggerStay();
				colliderProcessor.ValidMultiHitInterval();
			}
		}
		Vector3 b = Vector3.zero;
		if (_collider is BoxCollider)
		{
			b = (_collider as BoxCollider).center;
		}
		else if (_collider is SphereCollider)
		{
			b = (_collider as SphereCollider).center;
		}
		else if (_collider is CapsuleCollider)
		{
			b = (_collider as CapsuleCollider).center;
		}
		startColliderPos = _transform.position + b;
		isDestroyed = false;
		prevPosition = pos;
		if (controller != null)
		{
			controller.OnShot();
		}
	}

	protected virtual void SetBulletData(BulletData bullet, SkillInfo.SkillParam _skillParam, Vector3 pos, Quaternion rot)
	{
		if (bullet == null)
		{
			return;
		}
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
			controller = base.gameObject.AddComponent<BulletControllerFall>();
			break;
		case BulletData.BULLET_TYPE.HOMING:
			controller = base.gameObject.AddComponent<BulletControllerHoming>();
			break;
		case BulletData.BULLET_TYPE.CURVE:
			controller = base.gameObject.AddComponent<BulletControllerCurve>();
			break;
		case BulletData.BULLET_TYPE.BREAKABLE:
			controller = base.gameObject.AddComponent<BulletControllerBreakable>();
			break;
		case BulletData.BULLET_TYPE.OBSTACLE_CYLINDER:
			controller = base.gameObject.AddComponent<BulletControllerObstacleCylinder>();
			break;
		case BulletData.BULLET_TYPE.SNATCH:
			controller = base.gameObject.AddComponent<BulletControllerSnatch>();
			break;
		case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
			controller = base.gameObject.AddComponent<BulletControllerPairSwordsSoul>();
			break;
		case BulletData.BULLET_TYPE.PAIR_SWORDS_LASER:
			controller = base.gameObject.AddComponent<BulletControllerPairSwordsLaser>();
			break;
		case BulletData.BULLET_TYPE.HEALING_HOMING:
			controller = base.gameObject.AddComponent<BulletControllerHealingHoming>();
			break;
		case BulletData.BULLET_TYPE.ARROW_SOUL:
			controller = base.gameObject.AddComponent<BulletControllerArrowSoul>();
			break;
		case BulletData.BULLET_TYPE.ENEMY_PRESENT:
			controller = base.gameObject.AddComponent<BulletControllerEnemyPresent>();
			break;
		case BulletData.BULLET_TYPE.CRASH_BIT:
			controller = base.gameObject.AddComponent<BulletControllerCrashBit>();
			break;
		case BulletData.BULLET_TYPE.BARRIER:
			controller = base.gameObject.AddComponent<BulletControllerBarrier>();
			break;
		case BulletData.BULLET_TYPE.ROTATE_BIT:
			controller = base.gameObject.AddComponent<BulletControllerRotateBit>();
			break;
		case BulletData.BULLET_TYPE.RESURRECTION_HOMING:
			controller = base.gameObject.AddComponent<BulletControllerResurrectionHoming>();
			break;
		case BulletData.BULLET_TYPE.SPEAR_BARRIER:
			controller = base.gameObject.AddComponent<BulletControllerSpearBarrier>();
			break;
		case BulletData.BULLET_TYPE.SEARCH:
			controller = base.gameObject.AddComponent<BulletControllerSearch>();
			break;
		case BulletData.BULLET_TYPE.TURRET_BIT:
			controller = base.gameObject.AddComponent<BulletControllerTurretBit>();
			break;
		case BulletData.BULLET_TYPE.RANDOM_HOMING:
			controller = base.gameObject.AddComponent<BulletControllerRondomHoming>();
			break;
		case BulletData.BULLET_TYPE.ORACLE_SPEAR_SP:
			controller = base.gameObject.AddComponent<BulletControllerOracleSpearSp>();
			break;
		default:
			controller = base.gameObject.AddComponent<BulletControllerBase>();
			break;
		}
		if (controller != null)
		{
			controller.Initialize(bullet, _skillParam, pos, rot);
			controller.RegisterBulletObject(this);
			controller.RegisterFromObject(stageObject);
			switch (bullet.type)
			{
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
			case BulletData.BULLET_TYPE.ENEMY_PRESENT:
			case BulletData.BULLET_TYPE.BARRIER:
			case BulletData.BULLET_TYPE.SEARCH:
			case BulletData.BULLET_TYPE.TURRET_BIT:
				RegisterObserver();
				break;
			}
			controller.PostInitialize();
		}
		Character character = stageObject as Character;
		if (character != null)
		{
			SetTarget(character.actionTarget);
		}
	}

	public void SetTarget(StageObject obj)
	{
		m_targetObject = obj;
		if (stageObject != null)
		{
			Character character = stageObject as Character;
			if (character != null && character.IsValidBuffBlind())
			{
				m_targetObject = null;
			}
		}
		controller.RegisterTargetObject(m_targetObject);
	}

	public void SetTarget(TargetPoint targetPoint)
	{
		BulletControllerArrowSoul bulletControllerArrowSoul = controller as BulletControllerArrowSoul;
		if (!(bulletControllerArrowSoul == null))
		{
			bulletControllerArrowSoul.SetTarget(targetPoint);
		}
	}

	public void SetPuppetTargetPos(Vector3 pos)
	{
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

	public void NotifyBroken(bool isSendOnlyOriginal = true)
	{
		for (int i = 0; i < bulletObserverList.Count; i++)
		{
			bulletObserverList[i].OnBreak(observedID, isSendOnlyOriginal);
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

	public void SetSearchTarget(int targetID)
	{
		if (!(controller == null))
		{
			BulletControllerSearch bulletControllerSearch = controller as BulletControllerSearch;
			if (!(bulletControllerSearch == null))
			{
				bulletControllerSearch.SetTargetId(targetID);
			}
		}
	}

	public void SetTurretBitTarget(int targetID, int regionID)
	{
		if (!(controller == null))
		{
			BulletControllerTurretBit bulletControllerTurretBit = controller as BulletControllerTurretBit;
			if (!(bulletControllerTurretBit == null))
			{
				bulletControllerTurretBit.SetTargetId(targetID, regionID);
			}
		}
	}
}
