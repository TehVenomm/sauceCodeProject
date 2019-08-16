using UnityEngine;

public class BlurAndTurbulanceFilter : MonoBehaviour
{
	[SerializeField]
	private Material blurMaterial;

	[SerializeField]
	private Material turbulanceMaterial;

	[SerializeField]
	private Vector2 center;

	[SerializeField]
	private Vector2 scroll;

	[SerializeField]
	private float blurPower;

	[SerializeField]
	private float turbulancePower;

	[SerializeField]
	private float scale = 1f;

	[SerializeField]
	private float brightness;

	public BlurAndTurbulanceFilter()
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

	public void SetTurbulanceParam(float _power, float _scale, float _brightness)
	{
		turbulancePower = _power;
		scale = _scale;
		brightness = _brightness;
	}

	private void Awake()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		if (blurMaterial == null || turbulanceMaterial == null || blurPower <= 0.01f)
		{
			Graphics.Blit(src, dst);
			return;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 0, 4);
		blurMaterial.SetVector("_Origin", new Vector4(center.x, center.y, 0f, 0f));
		blurMaterial.SetFloat("_Power", blurPower);
		Graphics.Blit(src, temporary, blurMaterial);
		turbulanceMaterial.SetTextureOffset("_WarpTex", scroll);
		turbulanceMaterial.SetFloat("_Power", turbulancePower);
		turbulanceMaterial.SetFloat("_ScaleRate", scale);
		turbulanceMaterial.SetFloat("_Bright", brightness);
		Graphics.Blit(temporary, null, turbulanceMaterial);
		RenderTexture.ReleaseTemporary(temporary);
	}
}
