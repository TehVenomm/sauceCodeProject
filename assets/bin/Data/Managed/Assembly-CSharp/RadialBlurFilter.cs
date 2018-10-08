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
			if ((Object)_blurMaterial != (Object)null)
			{
				_blurMaterial.SetFloat("_Power", _strength);
			}
		}
	}

	public void SetCenter(Vector2 screenPos)
	{
		if ((Object)_blurMaterial != (Object)null)
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
		if ((Object)postEffector != (Object)null)
		{
			Object.Destroy(postEffector);
			postEffector = null;
		}
	}

	public override void PostEffectProc(RenderTexture src, RenderTexture dest)
	{
		if ((Object)_blurMaterial != (Object)null)
		{
			Graphics.Blit(src, null, _blurMaterial);
		}
		else
		{
			Graphics.Blit((Texture)src, (RenderTexture)null);
		}
	}
}
