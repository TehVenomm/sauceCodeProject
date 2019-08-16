using System;
using System.Collections;
using UnityEngine;

public class FilterManager : MonoBehaviourSingleton<FilterManager>
{
	private BlurFilter blurFilter;

	public GameObject tubulanceCamera
	{
		get;
		set;
	}

	public bool IsEnabledBlur()
	{
		if (blurFilter == null)
		{
			return false;
		}
		return blurFilter.get_enabled();
	}

	public void StartBlur(float time = 1f, float strength = 0.25f, float delay = 0f)
	{
		if (!(blurFilter == null))
		{
			blurFilter.blurStrength = 0f;
			blurFilter.StartFilter();
			this.StartCoroutine(ChangeBlurStrength(time, 0f, strength, delay, null));
		}
	}

	public void StopBlur(float time, float delay = 0f)
	{
		if (!(blurFilter == null) && blurFilter.get_enabled())
		{
			if (time <= 0f)
			{
				StopBlur();
			}
			else
			{
				this.StartCoroutine(ChangeBlurStrength(time, blurFilter.blurStrength, 0f, delay, delegate
				{
					StopBlur();
				}));
			}
		}
	}

	public void StopBlur()
	{
		if (!(blurFilter == null) && blurFilter.get_enabled())
		{
			blurFilter.StopFilter();
			blurFilter.set_enabled(false);
		}
	}

	private IEnumerator ChangeBlurStrength(float time, float startStrength, float targetStrength, float delay, Action onComplete)
	{
		if (!(blurFilter == null))
		{
			blurFilter.set_enabled(true);
			yield return (object)new WaitForSeconds(delay);
			for (float _time = 0f; _time < time; _time += Time.get_deltaTime())
			{
				float t = _time / time;
				blurFilter.blurStrength = startStrength * (1f - t) + targetStrength * t;
				yield return null;
			}
			blurFilter.blurStrength = targetStrength;
			onComplete?.Invoke();
		}
	}

	private void Start()
	{
		blurFilter = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<BlurFilter>();
		blurFilter.set_enabled(false);
	}

	public void StartTubulanceFilter(float power, Vector2 center, Action callback)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_StartTubulanceFilter(power, center, callback));
	}

	private IEnumerator _StartTubulanceFilter(float power, Vector2 center, Action callback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (tubulanceCamera == null)
		{
			GameObject obj = Resources.Load<GameObject>("Filter/TurbulanceFilterCamera");
			tubulanceCamera = ResourceUtility.Instantiate<GameObject>(obj);
		}
		BlurAndTurbulanceFilter filter = tubulanceCamera.GetComponent<BlurAndTurbulanceFilter>();
		float time = 0f;
		while (time < 2f)
		{
			time += Time.get_deltaTime();
			float blurT = Mathf.Clamp01(time / 0.6f);
			filter.SetBlurPram(Mathf.Lerp(0f, power, blurT), center);
			float turbulanceT = Mathf.Clamp01((time - 0.2f) / 1f);
			filter.SetTurbulanceParam(Mathf.Lerp(0f, 0.15f, turbulanceT), Mathf.Lerp(1f, 1.4f, turbulanceT), Mathf.Lerp(0f, 1f, turbulanceT));
			yield return null;
		}
		callback?.Invoke();
	}

	public void StopTubulanceFilter()
	{
		if (tubulanceCamera != null)
		{
			Object.Destroy(tubulanceCamera);
			tubulanceCamera = null;
		}
	}
}
