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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (_blurMaterial != null)
		{
			_blurMaterial.SetVector("_Origin", Vector4.op_Implicit(screenPos));
		}
	}

	private void Awake()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		_blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
	}

	public override void StartFilter()
	{
		postEffector = this.get_gameObject().AddComponent<PostEffector>();
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
