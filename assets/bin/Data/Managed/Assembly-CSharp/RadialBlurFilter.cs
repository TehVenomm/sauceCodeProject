using UnityEngine;

public class RadialBlurFilter : FilterBase
{
	[SerializeField]
	private Material _blurMaterial;

	[SerializeField]
	private float _strength;

	private PostEffector postEffector;

	public Material blurMaterial => _blurMaterial;

	public float strength
	{
		get
		{
			return _strength;
		}
		set
		{
			_strength = value;
			if (_blurMaterial != null)
			{
				_blurMaterial.SetFloat("_Power", _strength);
			}
		}
	}

	public void SetCenter(Vector2 screenPos)
	{
		if (_blurMaterial != null)
		{
			_blurMaterial.SetVector("_Origin", screenPos);
		}
	}

	private void Awake()
	{
		_blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
	}

	public override void StartFilter()
	{
		postEffector = base.gameObject.AddComponent<PostEffector>();
		postEffector.SetFilter(this);
	}

	public override void StopFilter()
	{
		if (postEffector != null)
		{
			Object.Destroy(postEffector);
			postEffector = null;
		}
	}

	public override void PostEffectProc(RenderTexture src, RenderTexture dest)
	{
		if (_blurMaterial != null)
		{
			Graphics.Blit(src, dest, _blurMaterial);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}
}
