using UnityEngine;

public class PostEffector
{
	[SerializeField]
	private FilterBase filter;

	public PostEffector()
		: this()
	{
	}

	public void SetFilter(FilterBase filter)
	{
		this.filter = filter;
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (filter == null)
		{
			Graphics.Blit(src, dest);
		}
		else
		{
			filter.PostEffectProc(src, dest);
		}
	}
}
