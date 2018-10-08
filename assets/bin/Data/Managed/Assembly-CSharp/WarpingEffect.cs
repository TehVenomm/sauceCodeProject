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

	private void Awake()
	{
		if ((UnityEngine.Object)atlasMaterial != (UnityEngine.Object)null && (UnityEngine.Object)sprite != (UnityEngine.Object)null && (UnityEngine.Object)sprite.atlas != (UnityEngine.Object)null && (UnityEngine.Object)sprite.atlas.spriteMaterial != (UnityEngine.Object)null && (UnityEngine.Object)atlasMaterial != (UnityEngine.Object)sprite.atlas.spriteMaterial)
		{
			atlasMaterial = sprite.atlas.spriteMaterial;
		}
	}

	protected void OnUpdateTexture(RenderTexture rt)
	{
		atlasMaterial.SetTexture("_SrcTex", rt);
		foreach (UIDrawCall active in UIDrawCall.activeList)
		{
			if ((UnityEngine.Object)active.baseMaterial == (UnityEngine.Object)atlasMaterial)
			{
				active.dynamicMaterial.SetTexture("_SrcTex", rt);
			}
		}
	}

	protected virtual void OnDestroy()
	{
		if ((UnityEngine.Object)cacher != (UnityEngine.Object)null)
		{
			RenderTargetCacher renderTargetCacher = cacher;
			renderTargetCacher.onUpdateTexture = (Action<RenderTexture>)Delegate.Remove(renderTargetCacher.onUpdateTexture, new Action<RenderTexture>(OnUpdateTexture));
		}
	}
}
