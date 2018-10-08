using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RenderTargetCacher : MonoBehaviour
{
	[SerializeField]
	private RenderTexture renderTexture;

	public bool cacheAfter;

	private Camera cam;

	public Action<RenderTexture> onUpdateTexture;

	public Action<RenderTexture, RenderTexture> postEffectProc
	{
		get;
		set;
	}

	public RenderTexture GetTexture()
	{
		if ((UnityEngine.Object)renderTexture == (UnityEngine.Object)null)
		{
			CreateTexture();
		}
		return renderTexture;
	}

	private void Start()
	{
		cam = GetComponent<Camera>();
		CreateTexture();
	}

	private void CreateTexture()
	{
		if (!((UnityEngine.Object)renderTexture != (UnityEngine.Object)null))
		{
			renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
			if (onUpdateTexture != null)
			{
				onUpdateTexture(renderTexture);
			}
		}
	}

	private void OnDestroy()
	{
		if ((UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = null;
		}
	}

	private void OnPreRender()
	{
		if (!((UnityEngine.Object)cam == (UnityEngine.Object)null))
		{
			if (cam.clearFlags == CameraClearFlags.Color)
			{
				GL.Clear(true, true, Color.black);
			}
			if (!((UnityEngine.Object)renderTexture == (UnityEngine.Object)null))
			{
				if (renderTexture.width != Screen.width || renderTexture.height != Screen.height)
				{
					renderTexture.Release();
					renderTexture = null;
					CreateTexture();
				}
				if ((UnityEngine.Object)renderTexture != (UnityEngine.Object)null)
				{
					renderTexture.DiscardContents();
				}
			}
		}
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (!cacheAfter)
		{
			Graphics.Blit(src, renderTexture);
		}
		if (postEffectProc != null)
		{
			postEffectProc(src, dest);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
		if (cacheAfter)
		{
			Graphics.Blit(dest, renderTexture);
		}
	}
}
