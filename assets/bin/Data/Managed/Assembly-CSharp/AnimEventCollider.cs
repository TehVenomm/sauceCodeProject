using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCollider
{
	public class AtkColliderHiter : MonoBehaviour, IAttackCollider
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

		protected Vector3 fixPos = Vector3.zero;

		protected Quaternion fixRot = Quaternion.identity;

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
				if (!(capsule != null))
				{
					return false;
				}
				return capsule.enabled;
			}
			protected set
			{
				if (capsule != null)
				{
					capsule.enabled = value;
				}
			}
		}

		public bool isReleased => (colliderInfo & COLLIDER_INFO.RELEASED) != 0;

		public void ValidTriggerStay()
		{
			if (colliderProcessor != null)
			{
				colliderProcessor.ValidTriggerStay();
			}
		}

		protected void Awake()
		{
			capsule = base.gameObject.AddComponent<CapsuleCollider>();
		}

		private void Update()
		{
			timeCount += Time.deltaTime;
		}

		private void FixedUpdate()
		{
			if (isReleased)
			{
				return;
			}
			if (isUpdateFixTrans)
			{
				base.transform.position = fixPos;
				base.transform.rotation = fixRot;
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

		public void SetColliderInfo(StageObject _stageObject, Transform parent, AttackInfo info, Vector3 pos, Vector3 rot, float radius, float height)
		{
			stageObject = _stageObject;
			attackInfo = info;
			base.transform.parent = parent;
			base.transform.localPosition = pos;
			base.transform.localEulerAngles = rot;
			base.transform.localScale = Vector3.one;
			fixPos = base.transform.position;
			fixRot = base.transform.rotation;
			capsule.direction = 2;
			capsule.radius = radius;
			capsule.height = height;
			capsule.enabled = true;
			capsule.isTrigger = true;
			stageObject._rigidbody.WakeUp();
			timeCount = 0f;
			colliderInfo = COLLIDER_INFO.NONE;
			if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
			{
				colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(attackInfo, stageObject, capsule, this);
				attackHitChecker = stageObject.ReferenceAttackHitChecker();
			}
			AttackContinuationInfo attackContinuationInfo = attackInfo as AttackContinuationInfo;
			if (attackContinuationInfo != null && attackContinuationInfo.disableUpdateFixTrans)
			{
				isUpdateFixTrans = false;
			}
		}

		public void ForceUpdateCurrentTransformInfo()
		{
			fixPos = base.transform.position;
			fixRot = base.transform.rotation;
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
			Vector3 result = from_collider.bounds.center;
			Character character = stageObject as Character;
			if (character != null && character.rootNode != null)
			{
				result = character.rootNode.position;
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

	public AttackInfo attackInfo
	{
		get
		{
			if (!(colliderHiter != null))
			{
				return null;
			}
			return colliderHiter.attackInfo;
		}
	}

	public bool isReleased
	{
		get
		{
			if (colliderHiter != null)
			{
				return colliderHiter.isReleased;
			}
			return false;
		}
	}

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
		if (data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && data.id != AnimEventFormat.ID.CONTINUS_ATTACK && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI)
		{
			return false;
		}
		if ((object)stgObj == null)
		{
			return false;
		}
		Player player = stgObj as Player;
		int layer = 13;
		float num = 1f;
		if ((object)player != null)
		{
			layer = 12;
			num = player.GetRadiusCustomRate();
		}
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.name = "AnimEventCollider";
			gameObject.layer = layer;
			colliderHiter = gameObject.AddComponent<AtkColliderHiter>();
		}
		Transform parent = stgObj.gameObject.transform;
		if (!string.IsNullOrEmpty(data.stringArgs[1]))
		{
			Transform transform = stgObj.FindNode(data.stringArgs[1]);
			if (transform != null)
			{
				parent = transform;
			}
		}
		Vector3 pos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Vector3 rot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
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
		UnityEngine.Object.Destroy(gameObject);
		gameObject = null;
	}

	public void InitTransformSettings(StageObject stgObj, AnimEventData.EventData _eventData)
	{
		if (!(stgObj == null) && !(colliderHiter == null) && _eventData != null && _eventData.intArgs != null && _eventData.floatArgs != null && _eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI && _eventData.intArgs.Length >= 2)
		{
			Vector3 vector = new Vector3(_eventData.floatArgs[8], _eventData.floatArgs[9], _eventData.floatArgs[10]);
			AnimEventFormat.MULTI_COL_GENERATE_CONDITION mULTI_COL_GENERATE_CONDITION = (AnimEventFormat.MULTI_COL_GENERATE_CONDITION)_eventData.intArgs[0];
			if (mULTI_COL_GENERATE_CONDITION == AnimEventFormat.MULTI_COL_GENERATE_CONDITION.IN_CORN)
			{
				float num = UnityEngine.Random.Range(0f - vector.x, vector.x);
				float num2 = UnityEngine.Random.Range(0f - vector.y, vector.y);
				float f = UnityEngine.Random.Range(0f, 360f) * ((float)Math.PI / 180f);
				Vector3 right = stgObj.transform.right;
				Vector3 up = stgObj.transform.up;
				colliderHiter.transform.position += num * Mathf.Cos(f) * right + num2 * Mathf.Sin(f) * up;
				colliderHiter.transform.LookAt(stgObj.transform.position);
				colliderHiter.ForceUpdateCurrentTransformInfo();
			}
		}
	}

	public void OverwriteObjectLayer(int _layer)
	{
		if (!(colliderHiter == null))
		{
			colliderHiter.gameObject.layer = _layer;
		}
	}
}
