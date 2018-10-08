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

	public void SetBlurPram(float _power, Vector2 _center)
	{
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
		blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if ((Object)blurMaterial == (Object)null || (Object)turbulanceMaterial == (Object)null || blurPower <= 0.01f)
		{
			Graphics.Blit(src, dst);
		}
		else
		{
			RenderTexture temporary = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.RGB565);
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
}
