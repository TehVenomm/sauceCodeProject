using UnityEngine;

public class PortalPointEffect
{
	protected bool isDelete;

	protected InGameSettingsManager.Portal.PointEffect parameter;

	protected FloatInterpolator anim = new FloatInterpolator();

	protected int animStep = -1;

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

	public PortalObject targetPortal
	{
		get;
		protected set;
	}

	public Coop_Model_EnemyDefeat defeatModel
	{
		get;
		protected set;
	}

	public PortalPointEffect()
		: this()
	{
	}

	public static PortalPointEffect Create(PortalObject portal_object, Coop_Model_EnemyDefeat model)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (model == null)
		{
			return null;
		}
		string effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.normalEffectName;
		if (model.ppt > 1)
		{
			effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.largeEffectName;
		}
		Transform effect = EffectManager.GetEffect(effect_name, null);
		if (effect == null)
		{
			return null;
		}
		PortalPointEffect portalPointEffect = effect.get_gameObject().AddComponent<PortalPointEffect>();
		if (portalPointEffect != null)
		{
			portalPointEffect.Drop(portal_object, model);
		}
		return portalPointEffect;
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		_rigidbody = this.GetComponent<Rigidbody>();
		if (_rigidbody == null)
		{
			_rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
		}
		int layer = 19;
		Utility.SetLayerWithChildren(_transform, layer);
		this.get_gameObject().SetActive(false);
	}

	public void Drop(PortalObject portal_object, Coop_Model_EnemyDefeat model)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		targetPortal = portal_object;
		defeatModel = model;
		this.get_gameObject().SetActive(true);
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect;
		Vector3 position = default(Vector3);
		position._002Ector((float)model.x, 0f, (float)model.z);
		_rigidbody.set_useGravity(false);
		_transform.set_position(position);
		anim.Set(parameter.popHeightAnimTime, 0f, parameter.popHeight, parameter.popHeightAnim, 0f, null);
		anim.Play();
		anim.Update(0f);
		position.y = anim.Get();
		_transform.set_position(position);
		animStep = 0;
	}

	private void FixedUpdate()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		if (!isDelete && Object.op_Implicit(targetPortal))
		{
			switch (animStep)
			{
			case 0:
				if (anim.IsPlaying())
				{
					Vector3 position2 = _transform.get_position();
					position2.y = anim.Update();
					if (anim.IsPlaying())
					{
						_transform.set_position(position2);
					}
				}
				else
				{
					animStep++;
					anim.Set(parameter.getSpeedAnimTime, 0f, parameter.getSpeed, parameter.getSpeedAnim, 0f, null);
					anim.Play();
					anim.Update(0f);
					Vector3 position3 = targetPortal._transform.get_position();
					position3.y += parameter.targetHeight;
					Vector3 val2 = position3 - _transform.get_position();
					_rigidbody.set_velocity(val2.get_normalized() * anim.Get());
					SoundManager.PlayOneShotUISE(40000070);
				}
				break;
			case 1:
			{
				Vector3 position = targetPortal._transform.get_position();
				position.y += parameter.targetHeight;
				Vector3 val = position - _transform.get_position();
				float num = anim.Update();
				if (num * Time.get_fixedDeltaTime() >= val.get_magnitude())
				{
					OnHitTarget();
				}
				else
				{
					float num2 = Vector3.Dot(val, _rigidbody.get_velocity());
					if (num2 < 0f)
					{
						OnHitTarget();
					}
					else
					{
						_rigidbody.set_velocity(val.get_normalized() * num);
					}
				}
				break;
			}
			}
		}
	}

	private void OnHitTarget()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		if (!isDelete)
		{
			_rigidbody.set_velocity(Vector3.get_zero());
			if (targetPortal != null)
			{
				Vector3 position = targetPortal._transform.get_position();
				position.y += parameter.targetHeight;
				_transform.set_position(position);
				targetPortal.OnGetPortalPoint(defeatModel.ppt);
			}
			EffectManager.ReleaseEffect(this.get_gameObject(), true, false);
			isDelete = true;
		}
	}
}
