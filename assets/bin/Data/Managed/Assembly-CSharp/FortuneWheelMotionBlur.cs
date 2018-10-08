using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
public class FortuneWheelMotionBlur : ImageEffectBase
{
	[Range(0f, 0.92f)]
	public float blurAmount = 0.8f;

	public bool extraBlur;

	public GameObject result;

	private RenderTexture accumTexture;

	private RenderTexture source;

	private RenderTexture destination;

	private bool isFocus;

	private bool isOnFocusing;

	protected override void Start()
	{
		Init();
		base.Start();
	}

	protected void Init()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Camera component = this.get_gameObject().GetComponent<Camera>();
		UITexture component2 = result.GetComponent<UITexture>();
		if (source == null)
		{
			source = new RenderTexture(component.get_pixelWidth(), component.get_pixelHeight(), 0, 0);
		}
		if (destination == null)
		{
			destination = new RenderTexture(component.get_pixelWidth(), component.get_pixelHeight(), 0, 0);
		}
		component.set_targetTexture(source);
		component2.mainTexture = destination;
		UIRoot component3 = GameObject.Find("UI_Root").get_gameObject().GetComponent<UIRoot>();
		component2.width = component3.manualWidth;
		component2.height = component3.manualHeight;
	}

	protected void ClearRenderTexture()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Object.DestroyImmediate(accumTexture);
		Object.DestroyImmediate(source);
		Object.DestroyImmediate(destination);
		Camera component = this.get_gameObject().GetComponent<Camera>();
		component.set_targetTexture(null);
		UITexture component2 = result.GetComponent<UITexture>();
		component2.mainTexture = null;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		ClearRenderTexture();
	}

	protected void OnApplicationFocus(bool focus)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (isFocus != focus)
		{
			isFocus = focus;
			if (isFocus)
			{
				this.StartCoroutine(OnFocus());
			}
			else
			{
				Camera component = this.get_gameObject().GetComponent<Camera>();
				component.set_enabled(false);
				ClearRenderTexture();
			}
		}
	}

	protected IEnumerator OnFocus()
	{
		Camera camera = this.get_gameObject().GetComponent<Camera>();
		camera.set_enabled(true);
		camera.set_clearFlags(2);
		camera.set_backgroundColor(Color.get_clear());
		isOnFocusing = true;
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		camera.set_clearFlags(3);
		isOnFocusing = false;
	}

	protected void OnPreRender()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		Transform val = this.get_gameObject().get_transform().get_parent();
		SetLayerRecursive(val.get_gameObject(), 24);
		Init();
	}

	protected void OnPostRender()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		if (!isOnFocusing)
		{
			if (accumTexture == null || accumTexture.get_width() != source.get_width() || accumTexture.get_height() != source.get_height())
			{
				Object.DestroyImmediate(accumTexture);
				accumTexture = new RenderTexture(source.get_width(), source.get_height(), 0);
				accumTexture.set_hideFlags(61);
				Graphics.Blit(source, accumTexture);
			}
			if (extraBlur)
			{
				RenderTexture val = RenderTexture.GetTemporary(source.get_width() / 4, source.get_height() / 4, 0);
				accumTexture.MarkRestoreExpected();
				Graphics.Blit(accumTexture, val);
				Graphics.Blit(val, accumTexture);
				RenderTexture.ReleaseTemporary(val);
			}
			blurAmount = Mathf.Clamp(blurAmount, 0f, 0.92f);
			base.material.SetTexture("_MainTex", accumTexture);
			base.material.SetFloat("_AccumOrig", 1f - blurAmount);
			accumTexture.MarkRestoreExpected();
			Graphics.Blit(source, accumTexture, base.material);
			Graphics.Blit(accumTexture, destination);
		}
	}

	private void SetLayerRecursive(GameObject obj, int layer)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (!(obj == null))
		{
			obj.set_layer(layer);
			foreach (Transform item in obj.get_transform())
			{
				Transform val = item;
				SetLayerRecursive(val.get_gameObject(), layer);
			}
		}
	}
}
