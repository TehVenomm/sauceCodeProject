using System.Collections;
using UnityEngine;

public class TransformInterpolator : MonoBehaviour
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

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Update()
	{
		if (lookAt != null)
		{
			if ((bool)(lookAt as Transform))
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
				_transform.localPosition = translate.Update();
			}
			if (rotate != null && rotate.IsPlaying())
			{
				_transform.localEulerAngles = rotate.Update();
			}
			if (scaling != null && scaling.IsPlaying())
			{
				_transform.localScale = scaling.Update();
			}
		}
	}

	public TransformInterpolator Translate(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null)
	{
		if (_time > 0f)
		{
			if (translate == null)
			{
				translate = new Vector3Interpolator();
			}
			translate.Set(_time, _transform.localPosition, target, ease_curve, add_value, add_curve);
			translate.Play();
		}
		else
		{
			translate = null;
			_transform.localPosition = target;
		}
		return this;
	}

	public TransformInterpolator Rotate(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null, bool lerp_angle = true)
	{
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
			rotate.Set(_time, _transform.localEulerAngles, target, ease_curve, add_value, add_curve);
			rotate.Play();
		}
		else
		{
			rotate = null;
			_transform.localEulerAngles = target;
		}
		return this;
	}

	public TransformInterpolator Scaling(float _time, Vector3 target, AnimationCurve ease_curve = null, Vector3 add_value = default(Vector3), AnimationCurve add_curve = null)
	{
		if (_time > 0f)
		{
			if (scaling == null)
			{
				scaling = new Vector3Interpolator();
			}
			scaling.Set(_time, _transform.localScale, target, ease_curve, add_value, add_curve);
			scaling.Play();
		}
		else
		{
			scaling = null;
			_transform.localScale = target;
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
		return StartCoroutine(DoWait());
	}

	private IEnumerator DoWait()
	{
		while (IsPlaying())
		{
			yield return null;
		}
	}
}
