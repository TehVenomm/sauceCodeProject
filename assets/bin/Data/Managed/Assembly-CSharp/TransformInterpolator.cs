using System.Collections;
using UnityEngine;

public class TransformInterpolator
{
	public bool play = true;

	public Vector3Interpolator translate;

	public Vector3Interpolator rotate;

	public Vector3Interpolator scaling;

	public object lookAt;

	private bool rotateAngleMode;

	public Transform _transform
	{
		get;
		private set;
	}

	public TransformInterpolator()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
	}

	private void Update()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		if (lookAt != null)
		{
			if (Object.op_Implicit(lookAt as Transform))
			{
				_transform.LookAt(lookAt as Transform);
			}
			else
			{
				_transform.LookAt((Vector3)lookAt);
			}
		}
		if (play)
		{
			if (translate != null && translate.IsPlaying())
			{
				_transform.set_localPosition(translate.Update());
			}
			if (rotate != null && rotate.IsPlaying())
			{
				_transform.set_localEulerAngles(rotate.Update());
			}
			if (scaling != null && scaling.IsPlaying())
			{
				_transform.set_localScale(scaling.Update());
			}
		}
	}

	public TransformInterpolator Translate(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (_time > 0f)
		{
			if (translate == null)
			{
				translate = new Vector3Interpolator();
			}
			translate.Set(_time, _transform.get_localPosition(), target, ease_curve, add_value, add_curve);
			translate.Play();
		}
		else
		{
			translate = null;
			_transform.set_localPosition(target);
		}
		return this;
	}

	public TransformInterpolator Rotate(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null, bool lerp_angle = true)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (_time > 0f)
		{
			if (rotate == null || rotateAngleMode != lerp_angle)
			{
				rotateAngleMode = lerp_angle;
				if (lerp_angle)
				{
					rotate = new AngleVector3Interpolator();
				}
				else
				{
					rotate = new Vector3Interpolator();
				}
			}
			rotate.Set(_time, _transform.get_localEulerAngles(), target, ease_curve, add_value, add_curve);
			rotate.Play();
		}
		else
		{
			rotate = null;
			_transform.set_localEulerAngles(target);
		}
		return this;
	}

	public TransformInterpolator Scaling(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (_time > 0f)
		{
			if (scaling == null)
			{
				scaling = new Vector3Interpolator();
			}
			scaling.Set(_time, _transform.get_localScale(), target, ease_curve, add_value, add_curve);
			scaling.Play();
		}
		else
		{
			scaling = null;
			_transform.set_localScale(target);
		}
		return this;
	}

	public TransformInterpolator LookAt(Transform target)
	{
		lookAt = target;
		return this;
	}

	public TransformInterpolator LookAt(Vector3 target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		lookAt = target;
		return this;
	}

	public bool IsPlaying()
	{
		if (play)
		{
			if (translate != null && translate.IsPlaying())
			{
				return true;
			}
			if (rotate != null && rotate.IsPlaying())
			{
				return true;
			}
			if (scaling != null && scaling.IsPlaying())
			{
				return true;
			}
		}
		return false;
	}

	public Coroutine Wait()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		return this.StartCoroutine(DoWait());
	}

	private IEnumerator DoWait()
	{
		while (IsPlaying())
		{
			yield return (object)null;
		}
	}
}
