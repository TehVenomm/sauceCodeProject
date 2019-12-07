using UnityEngine;

public class FilterBase : MonoBehaviour
{
	[SerializeField]
	private RenderTargetCacher cacher;

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
