using System;
using UnityEngine;

public class WarpingEffect : MonoBehaviour
{
	[SerializeField]
	protected UISprite sprite;

	[SerializeField]
	protected RenderTargetCacher cacher;

	[SerializeField]
	protected Material atlasMaterial;

	public WarpingEffect()
		: this()
	{
	}

	private void Awake()
	{
		if (atlasMaterial != null && sprite != null && sprite.atlas != null && sprite.atlas.spriteMaterial != null && atlasMaterial != sprite.atlas.spriteMaterial)
		{
			atlasMaterial = sprite.atlas.spriteMaterial;
		}
	}

	protected void OnUpdateTexture(RenderTexture rt)
	{
		atlasMaterial.SetTexture("_SrcTex", rt);
		foreach (UIDrawCall active in UIDrawCall.activeList)
		{
			if (active.baseMaterial == atlasMaterial)
			{
				active.dynamicMaterial.SetTexture("_SrcTex", rt);
			}
		}
	}

	protected virtual void OnDestroy()
	{
		if (cacher != null)
		{
			RenderTargetCacher renderTargetCacher = cacher;
			renderTargetCacher.onUpdateTexture = (Action<RenderTexture>)Delegate.Remove(renderTargetCacher.onUpdateTexture, new Action<RenderTexture>(OnUpdateTexture));
		}
	}
}
