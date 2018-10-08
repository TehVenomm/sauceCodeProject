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

	protected override void Start()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Camera component = this.get_gameObject().GetComponent<Camera>();
		source = new RenderTexture(component.get_pixelWidth(), component.get_pixelHeight(), 0, 0);
		component.set_targetTexture(source);
		destination = new RenderTexture(source.get_width(), source.get_height(), source.get_depth(), source.get_format());
		UITexture component2 = result.GetComponent<UITexture>();
		component2.mainTexture = destination;
		UIRoot component3 = GameObject.Find("UI_Root").get_gameObject().GetComponent<UIRoot>();
		component2.width = component3.manualWidth;
		component2.height = component3.manualHeight;
		base.Start();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Object.DestroyImmediate(accumTexture);
	}

	protected void OnPostRender()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
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
