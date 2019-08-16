using System.Collections.Generic;
using UnityEngine;

public class DangerRader : MonoBehaviour
{
	public class ColliderRecord
	{
		public float time;

		public Vector3 pos;

		public float angle;

		public Vector3 forward;

		public float radius;

		public bool isDash;

		public bool isBullet;

		public bool isWillHit;

		public bool isMove => isDash || isBullet;
	}

	public const float RADER_SPAN = 0.2f;

	private SpanTimer triggerSpan = new SpanTimer(0.2f);

	private const int RECORD_MAX = 20;

	public LinkedList<ColliderRecord> records = new LinkedList<ColliderRecord>();

	private float nearMoveTime;

	private float nearWillBulletHitTime;

	private float nearWillDashHitTime;

	public Brain brain
	{
		get;
		private set;
	}

	public Rigidbody _rigidbody
	{
		get;
		private set;
	}

	public Collider _collider
	{
		get;
		private set;
	}

	public ColliderRecord firstRecord => (records.Count > 0) ? records.First.Value : null;

	public DangerRader()
		: this()
	{
	}

	public static DangerRader Create(Brain brain, float radius)
	{
		DangerRader componentInChildren = brain.get_gameObject().GetComponentInChildren<DangerRader>();
		if (componentInChildren != null)
		{
			return componentInChildren;
		}
		int layer = brain.owner.get_gameObject().get_layer();
		componentInChildren = (DangerRader)Utility.CreateGameObjectAndComponent("DangerRader", brain.get_transform(), layer);
		if (componentInChildren == null)
		{
			return null;
		}
		componentInChildren.SetRadius(radius);
		return componentInChildren;
	}

	public void SetRadius(float radius)
	{
		SphereCollider val = _collider as SphereCollider;
		if (val != null)
		{
			val.set_radius(radius);
		}
	}

	private void Awake()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		_rigidbody = this.GetComponent<Rigidbody>();
		_collider = this.GetComponent<Collider>();
		if (_collider == null)
		{
			SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			_collider = val;
		}
		if (_collider != null)
		{
			_collider.set_isTrigger(true);
			if (_rigidbody == null)
			{
				_rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
			}
			_rigidbody.set_isKinematic(true);
		}
	}

	private void Start()
	{
		brain = this.get_gameObject().GetComponentInParent<Brain>();
	}

	private void Update()
	{
		if (triggerSpan.IsReady())
		{
			_collider.set_enabled(false);
			_collider.set_enabled(true);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (brain == null || _collider == null || !_collider.get_enabled() || !collider.get_isTrigger() || collider.get_gameObject() == this.get_gameObject())
		{
			return;
		}
		StageObject stageObject = null;
		BulletObject component = collider.get_gameObject().GetComponent<BulletObject>();
		stageObject = ((!(component != null)) ? collider.get_gameObject().GetComponentInParent<StageObject>() : component.stageObject);
		if (!(stageObject == null) && !(stageObject == brain.owner))
		{
			ColliderRecord colliderRecord = RecordCollider(collider, stageObject, component);
			if (colliderRecord.isMove)
			{
				brain.HandleEvent(BRAIN_EVENT.BULLET_CATCH, component);
			}
			else
			{
				brain.HandleEvent(BRAIN_EVENT.COLLIDER_CATCH, stageObject);
			}
		}
	}

	private ColliderRecord RecordCollider(Collider collider, StageObject obj, BulletObject bullet)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		ColliderRecord colliderRecord = null;
		if (records.Count >= 20)
		{
			colliderRecord = records.Last.Value;
			records.RemoveLast();
		}
		else
		{
			colliderRecord = new ColliderRecord();
		}
		colliderRecord.time = Time.get_time();
		colliderRecord.pos = collider.get_transform().get_position();
		colliderRecord.forward = collider.get_transform().get_forward();
		colliderRecord.angle = AIUtility.GetAngle360OfTargetPos(brain.owner, colliderRecord.pos);
		colliderRecord.radius = 1f;
		if (collider is SphereCollider)
		{
			colliderRecord.radius = (collider as SphereCollider).get_radius();
		}
		else if (collider is CapsuleCollider)
		{
			CapsuleCollider val = collider as CapsuleCollider;
			colliderRecord.radius = Mathf.Max(val.get_radius(), val.get_height());
		}
		colliderRecord.isDash = false;
		Enemy enemy = obj as Enemy;
		if (enemy != null)
		{
			colliderRecord.isDash = enemy.enableDash;
		}
		colliderRecord.isBullet = (bullet != null);
		colliderRecord.isWillHit = false;
		if (colliderRecord.isMove)
		{
			nearMoveTime = colliderRecord.time;
			int opponentMask = AIUtility.GetOpponentMask(obj);
			colliderRecord.isWillHit = AIUtility.IsHitObjectFromMoveObject(collider.get_transform(), brain.owner.get_transform(), colliderRecord.radius, opponentMask);
			if (colliderRecord.isWillHit)
			{
				if (colliderRecord.isDash)
				{
					nearWillDashHitTime = colliderRecord.time;
				}
				if (colliderRecord.isBullet)
				{
					nearWillBulletHitTime = colliderRecord.time;
				}
			}
		}
		records.AddFirst(colliderRecord);
		return colliderRecord;
	}

	public PLACE GetSafetyPlace()
	{
		if (firstRecord == null)
		{
			return PLACE.BACK;
		}
		return AIUtility.GetPlaceOfAngle360(firstRecord.angle).Reverse();
	}

	public PLACE GetSafetySide()
	{
		if (firstRecord == null)
		{
			return PLACE.LEFT;
		}
		return AIUtility.GetSideOfAngle360(firstRecord.angle).Reverse();
	}

	public bool AskDanger(float pass = 0.2f)
	{
		float num = Time.get_time() - pass;
		return firstRecord != null && firstRecord.time >= num;
	}

	public bool AskDangerMove(float pass = 0.2f)
	{
		float num = Time.get_time() - pass;
		return nearMoveTime >= num;
	}

	public bool AskWillHit(float pass = 0.2f)
	{
		return AskWillDashHit(pass) || AskWillBulletHit(pass);
	}

	public bool AskWillBulletHit(float pass = 0.2f)
	{
		float num = Time.get_time() - pass;
		return nearWillBulletHitTime >= num;
	}

	public bool AskWillDashHit(float pass = 0.2f)
	{
		float num = Time.get_time() - pass;
		return nearWillDashHitTime >= num;
	}

	public bool AskDangerPosition(Vector3 pos, float pass = 0.2f)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (records.Count <= 0)
		{
			return false;
		}
		float num = Time.get_time() - pass;
		foreach (ColliderRecord record in records)
		{
			if (record.time < num)
			{
				break;
			}
			float lengthWithBetweenPosition = AIUtility.GetLengthWithBetweenPosition(record.pos, pos);
			if (lengthWithBetweenPosition < record.radius)
			{
				return true;
			}
		}
		return false;
	}
}
