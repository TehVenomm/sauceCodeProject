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

	private const int RECORD_MAX = 20;

	private SpanTimer triggerSpan = new SpanTimer(0.2f);

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

	public static DangerRader Create(Brain brain, float radius)
	{
		DangerRader componentInChildren = brain.gameObject.GetComponentInChildren<DangerRader>();
		if ((Object)componentInChildren != (Object)null)
		{
			return componentInChildren;
		}
		int layer = brain.owner.gameObject.layer;
		componentInChildren = (DangerRader)Utility.CreateGameObjectAndComponent("DangerRader", brain.transform, layer);
		if ((Object)componentInChildren == (Object)null)
		{
			return null;
		}
		componentInChildren.SetRadius(radius);
		return componentInChildren;
	}

	public void SetRadius(float radius)
	{
		SphereCollider sphereCollider = _collider as SphereCollider;
		if ((Object)sphereCollider != (Object)null)
		{
			sphereCollider.radius = radius;
		}
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		if ((Object)_collider == (Object)null)
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.center = new Vector3(0f, 0f, 0f);
			_collider = sphereCollider;
		}
		if ((Object)_collider != (Object)null)
		{
			_collider.isTrigger = true;
			if ((Object)_rigidbody == (Object)null)
			{
				_rigidbody = base.gameObject.AddComponent<Rigidbody>();
			}
			_rigidbody.isKinematic = true;
		}
	}

	private void Start()
	{
		brain = base.gameObject.GetComponentInParent<Brain>();
	}

	private void Update()
	{
		if (triggerSpan.IsReady())
		{
			_collider.enabled = false;
			_collider.enabled = true;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!((Object)brain == (Object)null) && !((Object)_collider == (Object)null) && _collider.enabled && collider.isTrigger && !((Object)collider.gameObject == (Object)base.gameObject))
		{
			StageObject stageObject = null;
			BulletObject component = collider.gameObject.GetComponent<BulletObject>();
			stageObject = ((!((Object)component != (Object)null)) ? collider.gameObject.GetComponentInParent<StageObject>() : component.stageObject);
			if (!((Object)stageObject == (Object)null) && !((Object)stageObject == (Object)brain.owner))
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
	}

	private ColliderRecord RecordCollider(Collider collider, StageObject obj, BulletObject bullet)
	{
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
		colliderRecord.time = Time.time;
		colliderRecord.pos = collider.transform.position;
		colliderRecord.forward = collider.transform.forward;
		colliderRecord.angle = AIUtility.GetAngle360OfTargetPos(brain.owner, colliderRecord.pos);
		colliderRecord.radius = 1f;
		if (collider is SphereCollider)
		{
			colliderRecord.radius = (collider as SphereCollider).radius;
		}
		else if (collider is CapsuleCollider)
		{
			CapsuleCollider capsuleCollider = collider as CapsuleCollider;
			colliderRecord.radius = Mathf.Max(capsuleCollider.radius, capsuleCollider.height);
		}
		colliderRecord.isDash = false;
		Enemy enemy = obj as Enemy;
		if ((Object)enemy != (Object)null)
		{
			colliderRecord.isDash = enemy.enableDash;
		}
		colliderRecord.isBullet = ((Object)bullet != (Object)null);
		colliderRecord.isWillHit = false;
		if (colliderRecord.isMove)
		{
			nearMoveTime = colliderRecord.time;
			int opponentMask = AIUtility.GetOpponentMask(obj);
			colliderRecord.isWillHit = AIUtility.IsHitObjectFromMoveObject(collider.transform, brain.owner.transform, colliderRecord.radius, opponentMask);
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
		float num = Time.time - pass;
		return firstRecord != null && firstRecord.time >= num;
	}

	public bool AskDangerMove(float pass = 0.2f)
	{
		float num = Time.time - pass;
		return nearMoveTime >= num;
	}

	public bool AskWillHit(float pass = 0.2f)
	{
		return AskWillDashHit(pass) || AskWillBulletHit(pass);
	}

	public bool AskWillBulletHit(float pass = 0.2f)
	{
		float num = Time.time - pass;
		return nearWillBulletHitTime >= num;
	}

	public bool AskWillDashHit(float pass = 0.2f)
	{
		float num = Time.time - pass;
		return nearWillDashHitTime >= num;
	}

	public bool AskDangerPosition(Vector3 pos, float pass = 0.2f)
	{
		if (records.Count <= 0)
		{
			return false;
		}
		float num = Time.time - pass;
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
