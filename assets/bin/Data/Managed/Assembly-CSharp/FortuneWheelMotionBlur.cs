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
		_camera = base.gameObject.GetComponent<Camera>();
		_resultTexture = result.GetComponent<UITexture>();
		_root = GameObject.Find("UI_Root").gameObject.GetComponent<UIRoot>();
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
		RenderTexture.active = _camera.targetTexture;
		GL.Begin(4);
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		GL.End();
		RenderTexture.active = null;
		Transform parent = base.gameObject.transform.parent;
		SetLayerRecursive(parent.gameObject, 4);
		_resultTexture.width = _root.manualWidth;
		_resultTexture.height = _root.manualHeight;
		if (_srcRT == null || _srcRT.width != _camera.pixelWidth || _srcRT.height != _camera.pixelHeight)
		{
			_srcRT = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24);
			_srcRT.hideFlags = HideFlags.HideAndDontSave;
			_srcRT.Create();
		}
		_camera.targetTexture = _srcRT;
		if (_dstRT == null || _dstRT.width != _srcRT.width || _dstRT.height != _srcRT.height)
		{
			_dstRT = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24);
			_dstRT.hideFlags = HideFlags.HideAndDontSave;
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
		if (!(obj == null))
		{
			obj.layer = layer;
			foreach (Transform item in obj.transform)
			{
				SetLayerRecursive(item.gameObject, layer);
			}
		}
	}
}
