using System;
using System.Collections;
using UnityEngine;

public class ZoomBlurFilter
{
	[SerializeField]
	private Material blurMaterial;

	[SerializeField]
	private Vector2 center;

	[SerializeField]
	private float blurPower;

	[SerializeField]
	private RenderTexture _cachedTexture;

	[SerializeField]
	private RenderTexture _filteredTexture;

	private bool chacheTarget;

	private Action onCompleteChecheTarget;

	private RenderTargetCacher cacher;

	private bool requestBlitFilterTexture;

	public RenderTexture filteredTexture
	{
		get
		{
			return _filteredTexture;
		}
		private set
		{
			filteredTexture = value;
		}
	}

	public ZoomBlurFilter()
		: this()
	{
	}

	public void SetBlurPram(float _power, Vector2 _center)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		blurPower = _power;
		center = _center;
	}

	public void CacheRenderTarget(Action onComplete, bool reqWithFilter = false)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		chacheTarget = true;
		onCompleteChecheTarget = onComplete;
		GameObject val = MonoBehaviourSingleton<UIManager>.I.uiCamera.get_gameObject();
		cacher = val.GetComponent<RenderTargetCacher>();
		if (null == cacher)
		{
			cacher = val.AddComponent<RenderTargetCacher>();
		}
		requestBlitFilterTexture = reqWithFilter;
	}

	private void Awake()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
		Restore();
	}

	private void OnDestroy()
	{
		if (_filteredTexture != null)
		{
			RenderTexture.ReleaseTemporary(_filteredTexture);
			_filteredTexture = null;
		}
		if (blurMaterial != null)
		{
			Object.Destroy(blurMaterial);
			blurMaterial = null;
		}
		if (null != cacher)
		{
			Object.Destroy(cacher);
			cacher = null;
		}
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		if (chacheTarget && null != cacher)
		{
			Graphics.Blit(cacher.GetTexture(), _cachedTexture);
			Graphics.Blit(src, dst);
			chacheTarget = false;
			Object.Destroy(cacher);
			cacher = null;
			if (requestBlitFilterTexture)
			{
				requestBlitFilterTexture = false;
				Graphics.Blit(_cachedTexture, filteredTexture);
			}
			if (onCompleteChecheTarget != null)
			{
				onCompleteChecheTarget.Invoke();
				onCompleteChecheTarget = null;
			}
		}
		else if (blurMaterial == null || blurPower <= 0.01f)
		{
			Graphics.Blit(src, dst);
		}
		else
		{
			blurMaterial.SetVector("_Origin", new Vector4(center.x, center.y, 0f, 0f));
			blurMaterial.SetFloat("_Power", blurPower);
			_filteredTexture.DiscardContents(true, true);
			if (_cachedTexture != null)
			{
				Graphics.Blit(_cachedTexture, _filteredTexture, blurMaterial);
			}
			else
			{
				Graphics.Blit(src, _filteredTexture, blurMaterial);
			}
			Graphics.Blit(src, dst);
		}
	}

	public void Restore()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		RenderTextureFormat val = 4;
		_filteredTexture = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 0, val);
		_cachedTexture = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 0, val);
	}

	public void StartBlurFilter(float powerStart, float powerEnd, float duration, Vector2 blurCenter, Action onComplete)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(BlurFilterImpl(powerStart, powerEnd, duration, blurCenter, onComplete));
	}

	private IEnumerator BlurFilterImpl(float powerStart, float powerEnd, float duration, Vector2 blurCenter, Action onComplete)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		float timer = 0f;
		while (timer < duration)
		{
			timer += Time.get_deltaTime();
			float currentPower = Mathf.Lerp(powerStart, powerEnd, timer / duration);
			SetBlurPram(currentPower, blurCenter);
			yield return (object)null;
		}
		if (onComplete != null)
		{
			onComplete.Invoke();
		}
	}
}
