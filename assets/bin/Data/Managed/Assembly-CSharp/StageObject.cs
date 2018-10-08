using System;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : ControlObject, IBulletObserver
{
	public enum OBJECT_TYPE
	{
		STAGE_OBJECT,
		CHARACTER,
		PLAYER,
		ENEMY,
		SELF,
		DECOY,
		WAVE_TARGET
	}

	public class AttackedContinuationStatus
	{
		public AttackContinuationInfo attackInfo;

		public Collider fromCollider;

		public StageObject fromObject;

		public float hitTime;

		public float hitStartTime;
	}

	public class HitIntervalStatus
	{
		public Collider fromCollider;

		public float hitInterval;

		public float hitIntervalTimer;

		public bool enable;

		public HitIntervalStatus(Collider fromCollider, float hitInterval)
		{
			this.fromCollider = fromCollider;
			this.hitInterval = hitInterval;
			hitIntervalTimer = hitInterval;
			enable = true;
		}
	}

	[Serializable]
	public class StampInfo
	{
		[Tooltip("カメラ揺れ大きさ")]
		public float shakeCameraPercent;

		[Tooltip("カメラ揺れ周期（0で共通設定")]
		public float shakeCycleTime;

		[Tooltip("足踏みエフェクト名")]
		public string effectName;

		[Tooltip("足踏みエフェクトスケ\u30fcル")]
		public float effectScale = 1f;

		[Tooltip("足踏みSEID")]
		public int seID;
	}

	public enum COOP_MODE_TYPE
	{
		NONE,
		ORIGINAL,
		MIRROR,
		PUPPET
	}

	[Flags]
	public enum HIT_OFF_FLAG
	{
		NONE = 0x0,
		FORCE = 0x1,
		OPEN_MENU = 0x2,
		INVICIBLE = 0x4,
		DEAD = 0x8,
		LOAD = 0x10,
		INITIALIZE = 0x20,
		BATTLE_START = 0x40,
		DEAD_STANDUP = 0x80,
		PLAY_MOTION = 0x100,
		TUTORIAL = 0x200,
		UNLOCK_EVENT = 0x400,
		TEST = 0x800,
		GRAB = 0x1000
	}

	protected class HitOffTimer
	{
		public HIT_OFF_FLAG hitOffFlag;

		public float endTime;
	}

	public enum WAITING_PACKET
	{
		CHARACTER_MOVE_VELOCITY,
		CHARACTER_UPDATE_ACTION_POSITION,
		CHARACTER_UPDATE_DIRECTION,
		PLAYER_CHARGE_RELEASE,
		PLAYER_PRAYER_END,
		PLAYER_APPLY_CHANGE_WEAPON,
		ENEMY_WARP,
		ENEMY_UPDATE_BLEED_DAMAGE,
		ENEMY_UPDATE_SHADOWSEALING,
		PLAYER_JUMP_END,
		PLAYER_SOUL_BOOST,
		EVOLVE,
		PLAYER_PAIR_SWORDS_LASER_END,
		PLAYER_ONE_HAND_SWORD_MOVE_END,
		PLAYER_GATHER_GIMMICK,
		NUM
	}

	protected class WaitingPacketParam
	{
		public WAITING_PACKET type;

		public float startTime;

		public bool keepSync;

		public float addMarginTime;
	}

	protected class NodeTable : StringKeyTable<Transform>
	{
		public Item GetNodeItem(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			List<Item> list = GetList(key);
			if (list == null)
			{
				return null;
			}
			return GetItem(list, key);
		}
	}

	private class CastHitInfo
	{
		public float distance;

		public bool faceToTarget;

		public Collider collider;

		public bool enable = true;

		public bool checkCollider;
	}

	private int _id;

	protected List<AttackedContinuationStatus> continuationList = new List<AttackedContinuationStatus>();

	protected List<HitIntervalStatus> hitIntervalList = new List<HitIntervalStatus>();

	protected uint voiceChannel;

	public HIT_OFF_FLAG hitOffFlag;

	protected List<HitOffTimer> hitOffTimers = new List<HitOffTimer>();

	private Collider[] ignoreColliders;

	public List<int> loopSeForceEndList = new List<int>();

	protected bool isWallStay;

	protected float wallStayTimer;

	protected WaitingPacketParam[] waitingPacketParams = new WaitingPacketParam[15];

	protected NodeTable nodeCache = new NodeTable();

	protected AttackedHitStatus nowAttackedHitStatus;

	private bool isRegisteredStageObjectManager;

	protected List<IBulletObservable> bulletObservableList = new List<IBulletObservable>();

	protected List<int> bulletObservableIdList = new List<int>();

	public int bulletIndex;

	public InGameSettingsManager.StageObjectParam objectParameter
	{
		get;
		private set;
	}

	public OBJECT_TYPE objectType
	{
		get;
		protected set;
	}

	public virtual int id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	public ControllerBase controller
	{
		get;
		set;
	}

	public bool isInitialized
	{
		get;
		protected set;
	}

	public bool isLoading
	{
		get;
		private set;
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

	public ObjectPacketReceiver packetReceiver
	{
		get;
		protected set;
	}

	public ObjectPacketSender packetSender
	{
		get;
		protected set;
	}

	public COOP_MODE_TYPE coopMode
	{
		get;
		protected set;
	}

	public int coopClientId
	{
		get;
		protected set;
	}

	public bool isCoopInitialized
	{
		get;
		set;
	}

	public List<Collider> ignoreHitAttackColliders
	{
		get;
		protected set;
	}

	public bool isDestroyWaitFlag
	{
		get;
		protected set;
	}

	public bool IsRegisteredStageObjectManager => isRegisteredStageObjectManager;

	public Vector2 positionXZ
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position = _position;
			return new Vector2(position.x, position.z);
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position = _position;
			position.x = value.x;
			position.z = value.y;
			_position = position;
		}
	}

	public Vector2 forwardXZ
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			Vector3 forward = _forward;
			return new Vector2(forward.x, forward.z);
		}
	}

	public StageObject()
	{
		objectType = OBJECT_TYPE.STAGE_OBJECT;
		id = 0;
		isInitialized = false;
		coopMode = COOP_MODE_TYPE.NONE;
		coopClientId = 0;
		isCoopInitialized = false;
		hitOffFlag = HIT_OFF_FLAG.NONE;
		ignoreHitAttackColliders = new List<Collider>();
	}

	public bool IsCoopNone()
	{
		return coopMode == COOP_MODE_TYPE.NONE;
	}

	public bool IsOriginal()
	{
		return coopMode == COOP_MODE_TYPE.ORIGINAL;
	}

	public bool IsMirror()
	{
		return coopMode == COOP_MODE_TYPE.MIRROR;
	}

	public bool IsPuppet()
	{
		return coopMode == COOP_MODE_TYPE.PUPPET;
	}

	public bool IsWallStay()
	{
		if (objectParameter == null)
		{
			return false;
		}
		return wallStayTimer >= objectParameter.wallStayCheckTime;
	}

	public void AddController<T>() where T : ControllerBase
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (controller == null)
		{
			if (!CoopStageObjectUtility.CanControll(this))
			{
				Log.Error(LOG.INGAME, "StageObject::AddController. field block obj({0},{1}) to {2}", this, coopMode, typeof(T));
			}
			else
			{
				this.get_gameObject().AddComponent<T>();
			}
		}
	}

	public void RemoveController()
	{
		if (controller != null)
		{
			controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.DEFAULT);
			Object.Destroy(controller);
		}
	}

	public virtual void LookAt(Vector3 pos, bool isBlindEnable = false)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = _position;
		pos.y = position.y;
		_LookAt(pos);
	}

	protected virtual void OnEnable()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			SetNotifyMaster(MonoBehaviourSingleton<StageObjectManager>.I);
			isRegisteredStageObjectManager = true;
		}
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Detach(this);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			objectParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.stageObject;
		}
		else
		{
			objectParameter = new InGameSettingsManager.StageObjectParam();
		}
		if (packetReceiver == null)
		{
			packetReceiver = ObjectPacketReceiver.SetupComponent(this);
		}
		if (packetSender == null)
		{
			packetSender = ObjectPacketSender.SetupComponent(this);
		}
	}

	protected virtual void Start()
	{
	}

	protected virtual void Clear()
	{
	}

	public virtual void OnLoadStart()
	{
		isLoading = true;
		Clear();
		hitOffFlag |= HIT_OFF_FLAG.LOAD;
	}

	public virtual void OnLoadComplete()
	{
		nodeCache.Clear();
		hitOffFlag &= ~HIT_OFF_FLAG.LOAD;
		isLoading = false;
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
		if (!isInitialized)
		{
			Initialize();
		}
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
	}

	protected virtual void Initialize()
	{
		voiceChannel = GetVoiceChannel();
		isInitialized = true;
	}

	protected virtual uint GetVoiceChannel()
	{
		return 0u;
	}

	protected virtual bool EnablePlaySound()
	{
		return true;
	}

	public virtual bool DestroyObject()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		isDestroyWaitFlag = false;
		if (packetSender != null)
		{
			packetSender.OnDestroyObject();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(this);
		}
		Object.Destroy(this.get_gameObject());
		return true;
	}

	protected virtual void Update()
	{
		int num = 0;
		while (num < hitOffTimers.Count)
		{
			if (hitOffTimers[num].endTime <= Time.get_time())
			{
				hitOffFlag &= ~hitOffTimers[num].hitOffFlag;
				hitOffTimers.RemoveAt(num);
			}
			else
			{
				num++;
			}
		}
		if (packetReceiver != null)
		{
			packetReceiver.OnUpdate();
		}
		if (packetSender != null)
		{
			packetSender.OnUpdate();
		}
		UpdateWaitingPacket();
		int i = 0;
		for (int count = continuationList.Count; i < count; i++)
		{
			OnAttackedContinuationUpdate(continuationList[i]);
		}
		if (!hitIntervalList.IsNullOrEmpty())
		{
			hitIntervalList.RemoveAll((HitIntervalStatus item) => !item.enable);
		}
	}

	protected virtual void LateUpdate()
	{
	}

	protected virtual void FixedUpdate()
	{
		if (isWallStay)
		{
			wallStayTimer += Time.get_deltaTime();
		}
		else
		{
			wallStayTimer -= Time.get_deltaTime() * 0.5f;
			if (wallStayTimer < 0f)
			{
				wallStayTimer = 0f;
			}
		}
		isWallStay = false;
		for (int i = 0; i < continuationList.Count; i++)
		{
			if (!object.ReferenceEquals(continuationList[i], null))
			{
				OnAttackedContinuationFixedUpdate(continuationList[i]);
			}
		}
		if (!hitIntervalList.IsNullOrEmpty())
		{
			int j = 0;
			for (int count = hitIntervalList.Count; j < count; j++)
			{
				hitIntervalList[j].hitIntervalTimer -= Time.get_deltaTime();
			}
		}
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
	}

	protected virtual void OnCollisionStay(Collision collision)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (collision.get_gameObject().get_layer() == 9 || collision.get_gameObject().get_layer() == 17 || collision.get_gameObject().get_layer() == 18)
		{
			isWallStay = true;
		}
	}

	protected virtual void OnCollisionExit(Collision collision)
	{
	}

	public virtual void OnAnimatorMove()
	{
	}

	public virtual void OnDetachedObject(StageObject stage_object)
	{
		if (stage_object is Enemy && (stage_object as Enemy).colliders == ignoreColliders)
		{
			ResetIgnoreColliders();
		}
	}

	public virtual bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		return true;
	}

	public virtual void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		hit_param.toObject.OnAttackedHit(info, hit_param);
	}

	public virtual AttackHitColliderProcessor.HitParam SelectHitCollider(AttackHitColliderProcessor processor, List<AttackHitColliderProcessor.HitParam> hit_params)
	{
		return hit_params[0];
	}

	public virtual void OnAttackedHit(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		AttackedHitStatus attackedHitStatus = new AttackedHitStatus();
		attackedHitStatus.hitParam = hit_param;
		attackedHitStatus.attackInfo = info;
		attackedHitStatus.fromObjectID = hit_param.fromObject.id;
		attackedHitStatus.fromObject = hit_param.fromObject;
		attackedHitStatus.fromType = hit_param.fromObject.objectType;
		attackedHitStatus.fromPos = hit_param.fromObject._position;
		attackedHitStatus.hitPos = hit_param.point;
		attackedHitStatus.distanceXZ = hit_param.distanceXZ;
		attackedHitStatus.hitTime = hit_param.time;
		attackedHitStatus.isSpAttackHit = hit_param.isSpAttackHit;
		attackedHitStatus.attackMode = hit_param.attackMode;
		attackedHitStatus.damageDistanceData = hit_param.damageDistanceData;
		attackedHitStatus.exHitPos = hit_param.exHitPos;
		nowAttackedHitStatus = attackedHitStatus;
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			attackedHitStatus.fromClientID = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
		}
		if (attackedHitStatus.fromType == OBJECT_TYPE.SELF)
		{
			attackedHitStatus.fromType = OBJECT_TYPE.PLAYER;
		}
		OnAttackedHitDirection(new AttackedHitStatusDirection(attackedHitStatus));
		if (IsValidAttackedHit(hit_param.fromObject) && !IsPuppet() && !hit_param.fromObject.IsPuppet())
		{
			OnAttackedHitLocal(new AttackedHitStatusLocal(attackedHitStatus));
			if (IsMirror() || IsPuppet())
			{
				if (packetSender != null)
				{
					packetSender.OnAttackedHitOwner(new AttackedHitStatusOwner(attackedHitStatus));
				}
			}
			else if (IsEnableAttackedHitOwner())
			{
				OnAttackedHitOwner(new AttackedHitStatusOwner(attackedHitStatus));
				AttackedHitStatusFix status = new AttackedHitStatusFix(attackedHitStatus);
				OnAttackedHitFix(status);
				if (packetSender != null)
				{
					packetSender.OnAttackedHitFix(status);
				}
			}
		}
	}

	protected virtual bool IsValidAttackedHit(StageObject from_object)
	{
		return true;
	}

	protected virtual void OnAttackedHitDirection(AttackedHitStatusDirection status)
	{
		if (!CheckStatusForHitEffect(status))
		{
			OnIgnoreHitAttack();
		}
		else
		{
			status.fromObject.OnAttackFromHitDirection(status, this);
			OnPlayAttackedHitEffect(status);
		}
	}

	protected virtual void OnIgnoreHitAttack()
	{
	}

	protected virtual bool CheckStatusForHitEffect(AttackedHitStatusDirection status)
	{
		return true;
	}

	protected virtual void OnAttackFromHitDirection(AttackedHitStatusDirection status, StageObject to_object)
	{
	}

	protected virtual void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
	{
	}

	protected virtual void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
	}

	public virtual void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
	{
	}

	public virtual void AbsorptionProcByBuff(AttackedHitStatusLocal status)
	{
	}

	public virtual bool CutAndAbsorbDamageByBuff(Character targetCharacter, AttackedHitStatusLocal status)
	{
		return false;
	}

	public virtual bool ChargeSkillWhenDamagedByBuff()
	{
		return false;
	}

	public virtual void GetAtk(AttackHitInfo info, ref AtkAttribute atk)
	{
		if (info != null)
		{
			atk.Add(info.atk);
		}
	}

	public virtual void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
	}

	public virtual bool IsEnableAttackedHitOwner()
	{
		return true;
	}

	public virtual void OnAttackedHitFix(AttackedHitStatusFix status)
	{
	}

	public virtual bool OnContinuationEnter(AttackContinuationInfo info, StageObject from_object, Collider from_collider, float time)
	{
		int i = 0;
		for (int count = continuationList.Count; i < count; i++)
		{
			if (continuationList[i].attackInfo == info && continuationList[i].fromCollider == from_collider)
			{
				return false;
			}
		}
		AttackedContinuationStatus attackedContinuationStatus = new AttackedContinuationStatus();
		attackedContinuationStatus.attackInfo = info;
		attackedContinuationStatus.fromObject = from_object;
		attackedContinuationStatus.fromCollider = from_collider;
		attackedContinuationStatus.hitTime = time;
		attackedContinuationStatus.hitStartTime = Time.get_time();
		continuationList.Add(attackedContinuationStatus);
		OnAttackedContinuationStart(attackedContinuationStatus);
		return true;
	}

	public virtual void OnContinuationExit(AttackContinuationInfo info, Collider from_collider)
	{
		int num = 0;
		int count = continuationList.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if (continuationList[num].attackInfo == info && continuationList[num].fromCollider == from_collider)
			{
				break;
			}
			num++;
		}
		OnAttackedContinuationEnd(continuationList[num]);
		continuationList.RemoveAt(num);
	}

	protected virtual void OnAttackedContinuationStart(AttackedContinuationStatus status)
	{
	}

	protected virtual void OnAttackedContinuationUpdate(AttackedContinuationStatus status)
	{
	}

	protected virtual void OnAttackedContinuationFixedUpdate(AttackedContinuationStatus status)
	{
	}

	protected virtual void OnAttackedContinuationEnd(AttackedContinuationStatus status)
	{
	}

	protected float GetContinuationTimeChangeRate(AttackedContinuationStatus status)
	{
		if (status.attackInfo == null)
		{
			return 1f;
		}
		float num = status.hitTime + Time.get_time() - status.hitStartTime;
		float result = 1f;
		AttackInfo.TimeChange timeChange = status.attackInfo.timeChange;
		if (timeChange.intervalTime > 0f)
		{
			float num2 = (num - timeChange.startTime) / timeChange.intervalTime;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			result = timeChange.startRate + (timeChange.endRate - timeChange.startRate) * num2;
		}
		return result;
	}

	public virtual Vector3 GetCameraTargetPos()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return _position + new Vector3(0f, 1f, 0f);
	}

	protected void IgnoreColliders(Collider[] colliders)
	{
		if (!(_collider == null) && colliders != null)
		{
			if (ignoreColliders != null)
			{
				Utility.IgnoreCollision(_collider, ignoreColliders, false);
			}
			Utility.IgnoreCollision(_collider, colliders, true);
			ignoreColliders = colliders;
		}
	}

	protected void ResetIgnoreColliders()
	{
		if (!(_collider == null) && ignoreColliders != null)
		{
			Utility.IgnoreCollision(_collider, ignoreColliders, false);
			ignoreColliders = null;
		}
	}

	public void SetCoopMode(COOP_MODE_TYPE coop_mode, int client_id)
	{
		if ((coop_mode == COOP_MODE_TYPE.NONE || coop_mode == COOP_MODE_TYPE.ORIGINAL) && client_id != 0)
		{
			Log.Error(LOG.INGAME, "StageObject::SetCoopMode() Err ( client_id is invalid. )");
		}
		if (coop_mode == COOP_MODE_TYPE.ORIGINAL && !CoopStageObjectUtility.CanControll(this))
		{
			Log.Error(LOG.INGAME, "StageObject::SetCoopMode. field block obj({0}) to {1}", this, coop_mode);
		}
		else
		{
			if (coopMode != 0)
			{
				bool flag = false;
				if (CoopManager.IsValidInCoop())
				{
					flag = true;
				}
				if (!flag)
				{
					Log.Error(LOG.INGAME, "StageObject::SetCoopMode() Err ( not coop )");
					return;
				}
			}
			coopMode = coop_mode;
			coopClientId = client_id;
		}
	}

	public virtual Transform FindNode(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return base._transform;
		}
		StringKeyTableBase.Item nodeItem = nodeCache.GetNodeItem(name);
		if (nodeItem != null)
		{
			return nodeItem.value as Transform;
		}
		Transform val = Utility.Find(base._transform, name);
		nodeCache.Add(name, val);
		return val;
	}

	public virtual void OnAnimEvent(AnimEventData.EventData data)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		switch (data.id)
		{
		case AnimEventFormat.ID.SHAKE_CAMERA:
		{
			float percent = data.floatArgs[0];
			float cycle_time = (data.floatArgs.Length <= 1) ? 0f : data.floatArgs[1];
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(_position, percent, cycle_time);
			}
			return;
		}
		case AnimEventFormat.ID.INVICIBLE_ON:
			hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
			return;
		case AnimEventFormat.ID.INVICIBLE_OFF:
			hitOffFlag &= ~HIT_OFF_FLAG.INVICIBLE;
			return;
		case AnimEventFormat.ID.SE_ONESHOT:
		{
			int num2 = data.intArgs[0];
			string name2 = data.stringArgs[0];
			if (num2 != 0)
			{
				if (EnablePlaySound())
				{
					SoundManager.PlayOneShotSE(num2, this, FindNode(name2));
				}
				return;
			}
			break;
		}
		case AnimEventFormat.ID.SE_LOOP_PLAY:
		{
			int num = data.intArgs[0];
			if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
			{
				loopSeForceEndList.Add(num);
			}
			string name = data.stringArgs[0];
			if (EnablePlaySound())
			{
				SoundManager.PlayLoopSE(num, this, FindNode(name));
			}
			return;
		}
		case AnimEventFormat.ID.SE_LOOP_STOP:
		{
			int se_id = data.intArgs[0];
			SoundManager.StopLoopSE(se_id, this);
			return;
		}
		}
		Log.Error(LOG.INGAME, "AnimEvent Error! Event={0} Object={1}", data.name, this.get_name());
	}

	public virtual AttackInfo[] GetAttackInfos()
	{
		return null;
	}

	public virtual float GetAttackInfoRate()
	{
		return 0f;
	}

	public virtual AttackInfo FindAttackInfo(string name, bool fix_rate = true, bool isDuplicate = false)
	{
		AttackInfo[] attackInfos = GetAttackInfos();
		return _FindAttackInfo(attackInfos, name, fix_rate, GetAttackInfoRate(), isDuplicate);
	}

	public virtual AttackInfo FindAttackInfoExternal(string name, bool fix_rate, float rate)
	{
		AttackInfo[] attackInfos = GetAttackInfos();
		return _FindAttackInfo(attackInfos, name, fix_rate, rate, false);
	}

	protected virtual AttackInfo _FindAttackInfo(AttackInfo[] attack_infos, string name, bool fix_rate, float rate, bool isDuplicate = false)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		if (attack_infos == null)
		{
			return null;
		}
		AttackInfo attackInfo = null;
		int i = 0;
		for (int num = attack_infos.Length; i < num; i++)
		{
			AttackInfo attackInfo2 = attack_infos[i];
			if (attackInfo2.name == name)
			{
				if (fix_rate && !string.IsNullOrEmpty(attackInfo2.rateInfoName) && rate != 0f)
				{
					AttackInfo rate_info = _FindAttackInfo(attack_infos, attackInfo2.rateInfoName, false, 0f, false);
					attackInfo = attackInfo2.GetRateAttackInfo(rate_info, rate);
				}
				else
				{
					attackInfo = attackInfo2;
				}
				break;
			}
		}
		if (attackInfo == null)
		{
			Log.Error(LOG.INGAME, "FindAttackInfo not found. name : " + name);
			attackInfo = attack_infos[0];
		}
		if (isDuplicate)
		{
			return attackInfo.Duplicate();
		}
		return attackInfo;
	}

	public virtual SkillInfo.SkillParam GetSkillParam(int index)
	{
		return null;
	}

	public virtual void SetHitOffTimer(HIT_OFF_FLAG flag, float time)
	{
		if (!(time <= 0f) && flag != 0)
		{
			hitOffFlag |= flag;
			float num = Time.get_time() + time;
			int i = 0;
			for (int count = hitOffTimers.Count; i < count; i++)
			{
				if (hitOffTimers[i].hitOffFlag == flag)
				{
					if (hitOffTimers[i].endTime < num)
					{
						hitOffTimers[i].endTime = num;
					}
					return;
				}
			}
			HitOffTimer hitOffTimer = new HitOffTimer();
			hitOffTimer.endTime = num;
			hitOffTimer.hitOffFlag = flag;
			hitOffTimers.Add(hitOffTimer);
		}
	}

	public virtual void StartWaitingPacket(WAITING_PACKET type, bool keep_sync, float add_margin_time = 0f)
	{
		if (!IsCoopNone())
		{
			WaitingPacketParam waitingPacketParam = new WaitingPacketParam();
			waitingPacketParam.type = type;
			waitingPacketParam.startTime = Time.get_time();
			waitingPacketParam.keepSync = keep_sync;
			waitingPacketParam.addMarginTime = add_margin_time;
			waitingPacketParams[(int)type] = waitingPacketParam;
		}
	}

	public virtual bool IsValidWaitingPacket(WAITING_PACKET type)
	{
		if (IsCoopNone())
		{
			return false;
		}
		return waitingPacketParams[(int)type] != null;
	}

	public virtual void UpdateWaitingPacket()
	{
		if (!IsCoopNone())
		{
			int num = 0;
			int num2 = 15;
			while (true)
			{
				if (num >= num2)
				{
					return;
				}
				WaitingPacketParam waitingPacketParam = waitingPacketParams[num];
				if (waitingPacketParam != null)
				{
					if (waitingPacketParam.startTime <= 0f)
					{
						break;
					}
					if (IsOriginal())
					{
						if (waitingPacketParam.keepSync && Time.get_time() >= waitingPacketParam.startTime + objectParameter.waitingPacketIntervalTime)
						{
							KeepWaitingPacket(waitingPacketParam.type);
						}
					}
					else if (IsPuppet() || IsMirror())
					{
						float num3 = objectParameter.waitingPacketMarginTime + waitingPacketParam.addMarginTime;
						if (waitingPacketParam.keepSync)
						{
							num3 += objectParameter.waitingPacketIntervalTime;
						}
						if (Time.get_time() > waitingPacketParam.startTime + num3)
						{
							OnFailedWaitingPacket(waitingPacketParam.type);
						}
					}
				}
				num++;
			}
			Log.Error("StageObject::UpdateWaitingPacket() Err ( waitingPacketStartTime <= 0.0f )");
		}
	}

	public void KeepWaitingPacket(WAITING_PACKET type)
	{
		WaitingPacketParam waitingPacketParam = waitingPacketParams[(int)type];
		if (waitingPacketParam != null)
		{
			waitingPacketParam.startTime = Time.get_time();
			if (packetSender != null)
			{
				packetSender.OnKeepWaitingPacket(waitingPacketParam.type);
			}
		}
	}

	public virtual void OnFailedWaitingPacket(WAITING_PACKET type)
	{
		EndWaitingPacket(type);
	}

	public virtual void EndWaitingPacket(WAITING_PACKET type)
	{
		waitingPacketParams[(int)type] = null;
	}

	public static Vector3 GetAppearToTargetPos(Vector3 from_pos, Vector3 target_pos, Vector3 col_offset, float col_radius, float appear_distance, float appear_margin)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		bool just_appear;
		return _GetAppearToTargetPos(from_pos, target_pos, col_offset, col_radius, appear_distance, appear_margin, true, true, out just_appear);
	}

	private static Vector3 _GetAppearToTargetPos(Vector3 from_pos, Vector3 target_pos, Vector3 col_offset, float col_radius, float appear_distance, float appear_margin, bool from_inside, bool target_inside, out bool just_appear)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		just_appear = false;
		Vector3 val = target_pos - from_pos;
		float magnitude = val.get_magnitude();
		if (magnitude <= 0f)
		{
			just_appear = true;
			return from_pos;
		}
		if (appear_distance >= magnitude)
		{
			return from_pos;
		}
		if (col_offset.y <= 0f)
		{
			col_offset.y = 0.1f;
		}
		List<CastHitInfo> list = new List<CastHitInfo>();
		RaycastHit[] array = Physics.RaycastAll(target_pos + col_offset, -val, magnitude, 393728);
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			CastHitInfo castHitInfo = new CastHitInfo();
			castHitInfo.distance = array[i].get_distance() - col_radius;
			castHitInfo.faceToTarget = true;
			castHitInfo.collider = array[i].get_collider();
			list.Add(castHitInfo);
		}
		CastHitInfo castHitInfo2 = new CastHitInfo();
		castHitInfo2.distance = 0f;
		castHitInfo2.faceToTarget = !target_inside;
		castHitInfo2.collider = null;
		list.Add(castHitInfo2);
		array = Physics.RaycastAll(from_pos + col_offset, val, magnitude, 393728);
		int j = 0;
		for (int num2 = array.Length; j < num2; j++)
		{
			CastHitInfo castHitInfo3 = new CastHitInfo();
			castHitInfo3.distance = magnitude - array[j].get_distance() + col_radius;
			castHitInfo3.faceToTarget = false;
			castHitInfo3.collider = array[j].get_collider();
			list.Add(castHitInfo3);
		}
		CastHitInfo castHitInfo4 = new CastHitInfo();
		castHitInfo4.distance = magnitude;
		castHitInfo4.faceToTarget = from_inside;
		castHitInfo4.collider = null;
		list.Add(castHitInfo4);
		list.Sort(delegate(CastHitInfo a, CastHitInfo b)
		{
			float num6 = a.distance - b.distance;
			if (num6 == 0f)
			{
				if (a.faceToTarget == b.faceToTarget)
				{
					return 0;
				}
				return a.faceToTarget ? 1 : (-1);
			}
			return (num6 > 0f) ? 1 : (-1);
		});
		int k = 0;
		for (int count = list.Count; k < count; k++)
		{
			CastHitInfo castHitInfo5 = list[k];
			if (!castHitInfo5.checkCollider && !(castHitInfo5.collider == null))
			{
				int num3 = k;
				while (0 <= num3 && num3 < count)
				{
					CastHitInfo castHitInfo6 = list[num3];
					if (num3 != k)
					{
						if (castHitInfo6.collider == castHitInfo5.collider)
						{
							castHitInfo6.checkCollider = true;
							break;
						}
						castHitInfo6.enable = false;
					}
					num3 = ((!castHitInfo5.faceToTarget) ? (num3 - 1) : (num3 + 1));
				}
			}
		}
		float num4 = magnitude;
		for (int num5 = list.Count - 2; num5 >= 0; num5--)
		{
			CastHitInfo castHitInfo7 = list[num5];
			CastHitInfo castHitInfo8 = list[num5 + 1];
			if (castHitInfo7.enable && castHitInfo8.enable && castHitInfo7.distance != castHitInfo8.distance && !castHitInfo7.faceToTarget && castHitInfo8.faceToTarget)
			{
				if (castHitInfo7.distance <= appear_distance && castHitInfo8.distance >= appear_distance)
				{
					num4 = appear_distance;
					just_appear = true;
					break;
				}
				if (castHitInfo7.distance >= appear_distance)
				{
					num4 = castHitInfo7.distance;
				}
				else if (castHitInfo8.distance >= appear_distance - appear_margin)
				{
					num4 = castHitInfo8.distance;
					break;
				}
			}
		}
		if (num4 == magnitude)
		{
			return from_pos;
		}
		return target_pos - val.get_normalized() * num4;
	}

	public virtual Vector3 GetPredictivePosition()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if ((IsPuppet() || IsMirror()) && packetReceiver != null && packetReceiver.GetPredictivePosition(out Vector3 pos))
		{
			return pos;
		}
		return _position;
	}

	public virtual Vector3 GetTargetPosition(StageObject target)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			return Vector3.get_zero();
		}
		return target._position;
	}

	public virtual void ApplySyncPosition(Vector3 pos, float dir, bool force_sync = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		_rotation = Quaternion.AngleAxis(dir, Vector3.get_up());
		_position = pos;
	}

	public virtual bool isProgressStop()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		bool result = false;
		if (MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			result = true;
		}
		else if (IsCoopNone() || IsOriginal())
		{
			if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
			{
				result = true;
			}
		}
		else if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(coopClientId);
			if (coopClient != null && !coopClient.IsBattleStart())
			{
				result = true;
			}
		}
		return result;
	}

	public virtual AttackHitChecker ReferenceAttackHitChecker()
	{
		return null;
	}

	public List<IBulletObservable> GetBulletObservableList()
	{
		return bulletObservableList;
	}

	public virtual int GetObservedID()
	{
		return -1;
	}

	public virtual void RegisterObservable(IBulletObservable observable)
	{
		if (!bulletObservableList.Contains(observable))
		{
			bulletObservableList.Add(observable);
			if (IsCoopNone() || IsOriginal())
			{
				RegisterObservableID(observable.GetObservedID());
			}
		}
	}

	public void RegisterObservableID(int observedID)
	{
		if (!bulletObservableIdList.Contains(observedID))
		{
			bulletObservableIdList.Add(observedID);
			if (packetSender != null)
			{
				packetSender.OnBulletObservableSet(observedID);
			}
		}
	}

	public virtual void OnBreak(int brokenBulletID, bool isSendOnlyOriginal)
	{
	}

	public virtual void OnBulletDestroy(int observedID)
	{
	}

	public bool IsIgnoreByHitInterval(Collider fromCollider)
	{
		if (hitIntervalList.IsNullOrEmpty())
		{
			return false;
		}
		int i = 0;
		for (int count = hitIntervalList.Count; i < count; i++)
		{
			if (hitIntervalList[i].fromCollider == fromCollider)
			{
				if (hitIntervalList[i].hitIntervalTimer > 0f)
				{
					return true;
				}
				hitIntervalList[i].enable = false;
				return false;
			}
		}
		return false;
	}

	public void SetHitIntervalStatus(Collider fromCollider, float hitInterval)
	{
		int i = 0;
		for (int count = hitIntervalList.Count; i < count; i++)
		{
			if (hitIntervalList[i].fromCollider == fromCollider)
			{
				return;
			}
		}
		hitIntervalList.Add(new HitIntervalStatus(fromCollider, hitInterval));
	}

	public virtual void OnRecvSetCoopMode(Coop_Model_ObjectCoopInfo model, CoopPacket packet)
	{
	}
}
