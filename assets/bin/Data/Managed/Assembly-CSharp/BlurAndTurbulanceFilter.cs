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
}
