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
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		_animEventShot = aminEventShot;
		int layer = 18;
		Utility.SetLayerWithChildren(this.get_transform(), layer);
		base._rigidbody = this.GetComponent<Rigidbody>();
		base._rigidbody.set_useGravity(false);
		base._rigidbody.set_isKinematic(true);
		_colliderList = this.GetComponentsInChildren<Collider>();
		if (_colliderList.Length == 0)
		{
			CapsuleCollider val = this.get_gameObject().AddComponent<CapsuleCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			val.set_direction(2);
			_colliderList = (Collider[])new Collider[1];
			_colliderList[0] = val;
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
			_colliderList[i].set_isTrigger(false);
			if (!breakEnebleFlag)
			{
				_colliderList[i].set_enabled(false);
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
		keika += Time.get_deltaTime();
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
				_colliderList[i].set_enabled(true);
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
				_colliderList[i].set_enabled(false);
			}
		}
		_animEventShot.OnDestroy();
	}
}
