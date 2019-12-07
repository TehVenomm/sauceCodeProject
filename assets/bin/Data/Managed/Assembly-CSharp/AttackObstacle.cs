using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttackObstacle : StageObject
{
	private Collider[] _colliderList;

	private AnimEventShot _animEventShot;

	private float breakEnableTime;

	private bool breakEnebleFlag;

	private float keika;

	private bool isBreak;

	private bool isHitBreakEnable = true;

	protected override void Awake()
	{
		base.Awake();
	}

	public void Initialize(AnimEventShot aminEventShot, float time)
	{
		_animEventShot = aminEventShot;
		int layer = 18;
		Utility.SetLayerWithChildren(base.transform, layer);
		base._rigidbody = GetComponent<Rigidbody>();
		base._rigidbody.useGravity = false;
		base._rigidbody.isKinematic = true;
		_colliderList = GetComponentsInChildren<Collider>();
		if (_colliderList.Length == 0)
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(0f, 0f, 0f);
			capsuleCollider.direction = 2;
			_colliderList = new Collider[1];
			_colliderList[0] = capsuleCollider;
		}
		breakEnableTime = time;
		keika = 0f;
		if (breakEnableTime > 0f)
		{
			breakEnebleFlag = false;
		}
		else
		{
			breakEnebleFlag = true;
		}
		int i = 0;
		for (int num = _colliderList.Length; i < num; i++)
		{
			_colliderList[i].isTrigger = false;
			if (!breakEnebleFlag)
			{
				_colliderList[i].enabled = false;
			}
		}
		isBreak = false;
		isHitBreakEnable = _animEventShot.bulletData.dataObstacle.isHitBreak;
	}

	protected override void Update()
	{
		base.Update();
		if (breakEnebleFlag)
		{
			return;
		}
		keika += Time.deltaTime;
		if (!(keika > breakEnableTime))
		{
			return;
		}
		breakEnebleFlag = true;
		int i = 0;
		for (int num = _colliderList.Length; i < num; i++)
		{
			if (_colliderList[i] != null)
			{
				_colliderList[i].enabled = true;
			}
		}
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (!(from_object is Enemy))
		{
			return false;
		}
		return base.IsValidAttackedHit(from_object);
	}

	protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
		base.OnAttackedHitLocal(status);
		status.damage = (int)status.attackInfo.atk.normal;
		status.damage += (int)status.attackInfo.atk.fire;
		status.damage += (int)status.attackInfo.atk.water;
		status.damage += (int)status.attackInfo.atk.thunder;
		status.damage += (int)status.attackInfo.atk.soil;
		status.damage += (int)status.attackInfo.atk.light;
		status.damage += (int)status.attackInfo.atk.dark;
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		base.OnAttackedHitFix(status);
		if (status.damage <= 0 || status.fromType != OBJECT_TYPE.ENEMY || !isHitBreakEnable || !breakEnebleFlag || isBreak)
		{
			return;
		}
		isBreak = true;
		int i = 0;
		for (int num = _colliderList.Length; i < num; i++)
		{
			if (_colliderList[i] != null)
			{
				_colliderList[i].enabled = false;
			}
		}
		_animEventShot.OnDestroy();
	}
}
