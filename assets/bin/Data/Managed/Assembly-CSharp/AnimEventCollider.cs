using System.Collections.Generic;
using UnityEngine;

public class AnimEventCollider
{
	public class AtkColliderHiter : IAttackCollider
	{
		protected enum COLLIDER_INFO
		{
			NONE = 0,
			FIXED_UPDATE = 1,
			RESERVE_RELEASE = 2,
			RELEASED = 4
		}

		protected StageObject stageObject;

		protected CapsuleCollider capsule;

		protected Vector3 fixPos = Vector3.get_zero();

		protected Quaternion fixRot = Quaternion.get_identity();

		protected AttackColliderProcessor colliderProcessor;

		protected AttackHitChecker attackHitChecker;

		public bool checkFixedUpdate = true;

		public bool isUpdateFixTrans = true;

		protected COLLIDER_INFO colliderInfo
		{
			get;
			set;
		}

		public StageObject fromObject => stageObject;

		public AttackInfo attackInfo
		{
			get;
			protected set;
		}

		public float timeCount
		{
			get;
			protected set;
		}

		public bool enabledCollider
		{
			get
			{
				return capsule != null && capsule.get_enabled();
			}
			protected set
			{
				if (capsule != null)
				{
					capsule.set_enabled(value);
				}
			}
		}

		public bool isReleased => (colliderInfo & COLLIDER_INFO.RELEASED) != COLLIDER_INFO.NONE;

		public AtkColliderHiter()
			: this()
		{
		}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)


		public void ValidTriggerStay()
		{
			if (colliderProcessor != null)
			{
				colliderProcessor.ValidTriggerStay();
			}
		}

		protected void Awake()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			capsule = this.get_gameObject().AddComponent<CapsuleCollider>();
		}

		private void Update()
		{
			timeCount += Time.get_deltaTime();
		}

		private void FixedUpdate()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (!isReleased)
			{
				if (isUpdateFixTrans)
				{
					this.get_transform().set_position(fixPos);
					this.get_transform().set_rotation(fixRot);
				}
				if ((colliderInfo & COLLIDER_INFO.RESERVE_RELEASE) != 0 && (colliderInfo & COLLIDER_INFO.FIXED_UPDATE) != 0)
				{
					enabledCollider = false;
				}
				if (!enabledCollider)
				{
					if (colliderProcessor != null && !colliderProcessor.IsBusy())
					{
						colliderProcessor.OnDestroy();
						colliderProcessor = null;
					}
					if (colliderProcessor == null)
					{
						colliderInfo |= COLLIDER_INFO.RELEASED;
					}
				}
				if (checkFixedUpdate)
				{
					colliderInfo |= COLLIDER_INFO.FIXED_UPDATE;
				}
			}
		}

		public void SetColliderInfo(StageObject _stageObject, Transform parent, AttackInfo info, Vector3 pos, Vector3 rot, float radius, float height)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			stageObject = _stageObject;
			attackInfo = info;
			this.get_transform().set_parent(parent);
			this.get_transform().set_localPosition(pos);
			this.get_transform().set_localEulerAngles(rot);
			this.get_transform().set_localScale(Vector3.get_one());
			fixPos = this.get_transform().get_position();
			fixRot = this.get_transform().get_rotation();
			capsule.set_direction(2);
			capsule.set_radius(radius);
			capsule.set_height(height);
			capsule.set_enabled(true);
			capsule.set_isTrigger(true);
			stageObject._rigidbody.WakeUp();
			timeCount = 0f;
			colliderInfo = COLLIDER_INFO.NONE;
			if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
			{
				colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(attackInfo, stageObject, capsule, this, Player.ATTACK_MODE.NONE, null);
				attackHitChecker = stageObject.ReferenceAttackHitChecker();
			}
		}

		public void ForceUpdateCurrentTransformInfo()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			fixPos = this.get_transform().get_position();
			fixRot = this.get_transform().get_rotation();
		}

		public void ReserveRelease()
		{
			colliderInfo |= COLLIDER_INFO.RESERVE_RELEASE;
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (colliderProcessor != null)
			{
				colliderProcessor.OnTriggerEnter(collider);
			}
		}

		private void OnTriggerStay(Collider collider)
		{
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

		public virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
		{
		}

		public virtual float GetTime()
		{
			return timeCount;
		}

		public virtual bool IsEnable()
		{
			return true;
		}

		public virtual void SortHitStackList(List<AttackHitColliderProcessor.HitResult> stack_list)
		{
		}

		public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			Bounds bounds = from_collider.get_bounds();
			Vector3 result = bounds.get_center();
			Character character = stageObject as Character;
			if (character != null && character.rootNode != null)
			{
				result = character.rootNode.get_position();
			}
			return result;
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
		}

		public AttackInfo GetAttackInfo()
		{
			return colliderProcessor.attackInfo;
		}

		public StageObject GetFromObject()
		{
			return colliderProcessor.fromObject;
		}
	}

	protected GameObject gameObject;

	protected AtkColliderHiter colliderHiter;

	public AttackInfo attackInfo => (!(colliderHiter != null)) ? null : colliderHiter.attackInfo;

	public bool isReleased => colliderHiter != null && colliderHiter.isReleased;

	public void SetFixedUpdateFlag(bool flag)
	{
		if (colliderHiter != null)
		{
			colliderHiter.checkFixedUpdate = flag;
		}
	}

	public void SetFixTransformUpdateFlag(bool flag)
	{
		if (colliderHiter != null)
		{
			colliderHiter.isUpdateFixTrans = flag;
		}
	}

	public void ValidTriggerStay()
	{
		if (colliderHiter != null)
		{
			colliderHiter.ValidTriggerStay();
		}
	}

	public bool Initialize(StageObject stgObj, AnimEventData.EventData data, AttackInfo atkInfo)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		if (data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && data.id != AnimEventFormat.ID.CONTINUS_ATTACK && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI)
		{
			return false;
		}
		if (object.ReferenceEquals(stgObj, null))
		{
			return false;
		}
		Player player = stgObj as Player;
		int layer = 13;
		float num = 1f;
		if (!object.ReferenceEquals(player, null))
		{
			layer = 12;
			num = player.GetRadiusCustomRate();
		}
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.set_name("AnimEventCollider");
			gameObject.set_layer(layer);
			colliderHiter = gameObject.AddComponent<AtkColliderHiter>();
		}
		Transform parent = stgObj.get_gameObject().get_transform();
		if (!string.IsNullOrEmpty(data.stringArgs[1]))
		{
			Transform val = stgObj.FindNode(data.stringArgs[1]);
			if (val != null)
			{
				parent = val;
			}
		}
		Vector3 pos = default(Vector3);
		pos._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Vector3 rot = default(Vector3);
		rot._002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		float radius = data.floatArgs[6] * num;
		float height = data.floatArgs[7];
		colliderHiter.SetColliderInfo(stgObj, parent, atkInfo, pos, rot, radius, height);
		return true;
	}

	public void ReserveRelease()
	{
		colliderHiter.ReserveRelease();
	}

	public void Destroy()
	{
		colliderHiter = null;
		Object.Destroy(gameObject);
		gameObject = null;
	}

	public void InitTransformSettings(StageObject stgObj, AnimEventData.EventData _eventData)
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		if (!(stgObj == null) && !(colliderHiter == null) && _eventData != null && _eventData.intArgs != null && _eventData.floatArgs != null && _eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI && _eventData.intArgs.Length >= 2)
		{
			Vector3 val = default(Vector3);
			val._002Ector(_eventData.floatArgs[8], _eventData.floatArgs[9], _eventData.floatArgs[10]);
			AnimEventFormat.MULTI_COL_GENERATE_CONDITION mULTI_COL_GENERATE_CONDITION = (AnimEventFormat.MULTI_COL_GENERATE_CONDITION)_eventData.intArgs[0];
			AnimEventFormat.MULTI_COL_GENERATE_CONDITION mULTI_COL_GENERATE_CONDITION2 = mULTI_COL_GENERATE_CONDITION;
			if (mULTI_COL_GENERATE_CONDITION2 == AnimEventFormat.MULTI_COL_GENERATE_CONDITION.IN_CORN)
			{
				float num = Random.Range(0f - val.x, val.x);
				float num2 = Random.Range(0f - val.y, val.y);
				float num3 = Random.Range(0f, 360f) * 0.0174532924f;
				Vector3 right = stgObj.get_transform().get_right();
				Vector3 up = stgObj.get_transform().get_up();
				Transform transform = colliderHiter.get_transform();
				transform.set_position(transform.get_position() + (num * Mathf.Cos(num3) * right + num2 * Mathf.Sin(num3) * up));
				colliderHiter.get_transform().LookAt(stgObj.get_transform().get_position());
				colliderHiter.ForceUpdateCurrentTransformInfo();
			}
		}
	}

	public void OverwriteObjectLayer(int _layer)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!(colliderHiter == null))
		{
			colliderHiter.get_gameObject().set_layer(_layer);
		}
	}
}
