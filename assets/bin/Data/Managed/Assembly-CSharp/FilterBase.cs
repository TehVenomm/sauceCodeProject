using UnityEngine;

public class FilterBase
{
	[SerializeField]
	private RenderTargetCacher cacher;

	public FilterBase()
		: this()
	{
	}

	public virtual void PostEffectProc(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest);
	}

	public virtual void StartFilter()
	{
	}

	public virtual void StopFilter()
	{
	}
}
