using UnityEngine;

public class PortalPointEffect : MonoBehaviour
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

	public static PortalPointEffect Create(PortalObject portal_object, Coop_Model_EnemyDefeat model)
	{
		if (model == null)
		{
			return null;
		}
		string effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.normalEffectName;
		if (model.ppt > 1)
		{
			effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect.largeEffectName;
		}
		Transform effect = EffectManager.GetEffect(effect_name);
		if (effect == null)
		{
			return null;
		}
		PortalPointEffect portalPointEffect = effect.gameObject.AddComponent<PortalPointEffect>();
		if (portalPointEffect != null)
		{
			portalPointEffect.Drop(portal_object, model);
		}
		return portalPointEffect;
	}

	private void Awake()
	{
		_transform = base.transform;
		_rigidbody = GetComponent<Rigidbody>();
		if (_rigidbody == null)
		{
			_rigidbody = base.gameObject.AddComponent<Rigidbody>();
		}
		int layer = 19;
		Utility.SetLayerWithChildren(_transform, layer);
		base.gameObject.SetActive(value: false);
	}

	public void Drop(PortalObject portal_object, Coop_Model_EnemyDefeat model)
	{
		targetPortal = portal_object;
		defeatModel = model;
		base.gameObject.SetActive(value: true);
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.portal.pointEffect;
		Vector3 position = new Vector3(model.x, 0f, model.z);
		_rigidbody.useGravity = false;
		_transform.position = position;
		anim.Set(parameter.popHeightAnimTime, 0f, parameter.popHeight, parameter.popHeightAnim, 0f);
		anim.Play();
		anim.Update(0f);
		position.y = anim.Get();
		_transform.position = position;
		animStep = 0;
	}

	private void FixedUpdate()
	{
		if (isDelete || !targetPortal)
		{
			return;
		}
		switch (animStep)
		{
		case 0:
			if (anim.IsPlaying())
			{
				Vector3 position2 = _transform.position;
				position2.y = anim.Update();
				if (anim.IsPlaying())
				{
					_transform.position = position2;
				}
			}
			else
			{
				animStep++;
				anim.Set(parameter.getSpeedAnimTime, 0f, parameter.getSpeed, parameter.getSpeedAnim, 0f);
				anim.Play();
				anim.Update(0f);
				Vector3 position3 = targetPortal._transform.position;
				position3.y += parameter.targetHeight;
				Vector3 vector = position3 - _transform.position;
				_rigidbody.velocity = vector.normalized * anim.Get();
				SoundManager.PlayOneShotUISE(40000070);
			}
			break;
		case 1:
		{
			Vector3 position = targetPortal._transform.position;
			position.y += parameter.targetHeight;
			Vector3 lhs = position - _transform.position;
			float num = anim.Update();
			if (num * Time.fixedDeltaTime >= lhs.magnitude)
			{
				OnHitTarget();
			}
			else if (Vector3.Dot(lhs, _rigidbody.velocity) < 0f)
			{
				OnHitTarget();
			}
			else
			{
				_rigidbody.velocity = lhs.normalized * num;
			}
			break;
		}
		}
	}

	private void OnHitTarget()
	{
		if (!isDelete)
		{
			_rigidbody.velocity = Vector3.zero;
			if (targetPortal != null)
			{
				Vector3 position = targetPortal._transform.position;
				position.y += parameter.targetHeight;
				_transform.position = position;
				targetPortal.OnGetPortalPoint(defeatModel.ppt);
			}
			EffectManager.ReleaseEffect(base.gameObject);
			isDelete = true;
		}
	}
}
