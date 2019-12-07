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
		return blurFilter.enabled;
	}

	public void StartBlur(float time = 1f, float strength = 0.25f, float delay = 0f)
	{
		if (!(blurFilter == null))
		{
			blurFilter.blurStrength = 0f;
			blurFilter.StartFilter();
			StartCoroutine(ChangeBlurStrength(time, 0f, strength, delay, null));
		}
	}

	public void StopBlur(float time, float delay = 0f)
	{
		if (!(blurFilter == null) && blurFilter.enabled)
		{
			if (time <= 0f)
			{
				StopBlur();
			}
			else
			{
				StartCoroutine(ChangeBlurStrength(time, blurFilter.blurStrength, 0f, delay, delegate
				{
					StopBlur();
				}));
			}
		}
	}

	public void StopBlur()
	{
		if (!(blurFilter == null) && blurFilter.enabled)
		{
			blurFilter.StopFilter();
			blurFilter.enabled = false;
		}
	}

	private IEnumerator ChangeBlurStrength(float time, float startStrength, float targetStrength, float delay, Action onComplete)
	{
		if (!(blurFilter == null))
		{
			blurFilter.enabled = true;
			yield return new WaitForSeconds(delay);
			for (float _time = 0f; _time < time; _time += Time.deltaTime)
			{
				float num = _time / time;
				blurFilter.blurStrength = startStrength * (1f - num) + targetStrength * num;
				yield return null;
			}
			blurFilter.blurStrength = targetStrength;
			onComplete?.Invoke();
		}
	}

	private void Start()
	{
		blurFilter = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<BlurFilter>();
		blurFilter.enabled = false;
	}

	public void StartTubulanceFilter(float power, Vector2 center, Action callback)
	{
		StartCoroutine(_StartTubulanceFilter(power, center, callback));
	}

	private IEnumerator _StartTubulanceFilter(float power, Vector2 center, Action callback)
	{
		if (tubulanceCamera == null)
		{
			GameObject obj = Resources.Load<GameObject>("Filter/TurbulanceFilterCamera");
			tubulanceCamera = ResourceUtility.Instantiate(obj);
		}
		BlurAndTurbulanceFilter filter = tubulanceCamera.GetComponent<BlurAndTurbulanceFilter>();
		float time = 0f;
		while (time < 2f)
		{
			time += Time.deltaTime;
			float t = Mathf.Clamp01(time / 0.6f);
			filter.SetBlurPram(Mathf.Lerp(0f, power, t), center);
			float t2 = Mathf.Clamp01((time - 0.2f) / 1f);
			filter.SetTurbulanceParam(Mathf.Lerp(0f, 0.15f, t2), Mathf.Lerp(1f, 1.4f, t2), Mathf.Lerp(0f, 1f, t2));
			yield return null;
		}
		callback?.Invoke();
	}

	public void StopTubulanceFilter()
	{
		if (tubulanceCamera != null)
		{
			UnityEngine.Object.Destroy(tubulanceCamera);
			tubulanceCamera = null;
		}
	}
}
