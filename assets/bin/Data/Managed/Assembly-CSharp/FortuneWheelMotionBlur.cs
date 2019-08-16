using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
public class FortuneWheelMotionBlur : ImageEffectBase
{
	[Range(0f, 0.92f)]
	public float blurAmount = 0.8f;

	public GameObject result;

	private Camera _camera;

	private UITexture _resultTexture;

	private UIRoot _root;

	private RenderTexture _srcRT;

	private RenderTexture _dstRT;

	protected override void Start()
	{
		_camera = this.get_gameObject().GetComponent<Camera>();
		_resultTexture = result.GetComponent<UITexture>();
		_root = GameObject.Find("UI_Root").get_gameObject().GetComponent<UIRoot>();
		base.Start();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Object.DestroyImmediate(_dstRT);
		_dstRT = null;
		Object.DestroyImmediate(_srcRT);
		_srcRT = null;
	}

	private void OnDestroy()
	{
		Object.DestroyImmediate(_dstRT);
		_dstRT = null;
		Object.DestroyImmediate(_srcRT);
		_srcRT = null;
	}

	private void OnPreCull()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		RenderTexture.set_active(_camera.get_targetTexture());
		GL.Begin(4);
		GL.Clear(true, true, Color.get_clear());
		GL.End();
		RenderTexture.set_active(null);
		Transform parent = this.get_gameObject().get_transform().get_parent();
		SetLayerRecursive(parent.get_gameObject(), 4);
		_resultTexture.width = _root.manualWidth;
		_resultTexture.height = _root.manualHeight;
		if (_srcRT == null || _srcRT.get_width() != _camera.get_pixelWidth() || _srcRT.get_height() != _camera.get_pixelHeight())
		{
			_srcRT = new RenderTexture(_camera.get_pixelWidth(), _camera.get_pixelHeight(), 24);
			_srcRT.set_hideFlags(61);
			_srcRT.Create();
		}
		_camera.set_targetTexture(_srcRT);
		if (_dstRT == null || _dstRT.get_width() != _srcRT.get_width() || _dstRT.get_height() != _srcRT.get_height())
		{
			_dstRT = new RenderTexture(_camera.get_pixelWidth(), _camera.get_pixelHeight(), 24);
			_dstRT.set_hideFlags(61);
			_dstRT.Create();
			Graphics.Blit(_srcRT, _dstRT);
		}
		_resultTexture.mainTexture = _dstRT;
	}

	private void OnPostRender()
	{
		blurAmount = Mathf.Clamp(blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", _dstRT);
		base.material.SetFloat("_AccumOrig", 1f - blurAmount);
		_dstRT.MarkRestoreExpected();
		Graphics.Blit(_srcRT, _dstRT, base.material);
	}

	private void SetLayerRecursive(GameObject obj, int layer)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (!(obj == null))
		{
			obj.set_layer(layer);
			IEnumerator enumerator = obj.get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform val = enumerator.Current;
					SetLayerRecursive(val.get_gameObject(), layer);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
