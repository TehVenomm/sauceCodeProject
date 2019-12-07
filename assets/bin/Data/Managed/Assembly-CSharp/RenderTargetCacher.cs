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
		if (renderTexture == null)
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
		if (!(renderTexture != null))
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
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = null;
		}
	}

	private void OnPreRender()
	{
		if (cam == null)
		{
			return;
		}
		if (cam.clearFlags == CameraClearFlags.Color)
		{
			GL.Clear(clearDepth: true, clearColor: true, Color.black);
		}
		if (!(renderTexture == null))
		{
			if (renderTexture.width != Screen.width || renderTexture.height != Screen.height)
			{
				renderTexture.Release();
				renderTexture = null;
				CreateTexture();
			}
			if (renderTexture != null)
			{
				renderTexture.DiscardContents();
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
