using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RenderTargetCacher
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

	public RenderTargetCacher()
		: this()
	{
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
		cam = this.GetComponent<Camera>();
		CreateTexture();
	}

	private void CreateTexture()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (!(renderTexture != null))
		{
			renderTexture = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height());
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (!(cam == null))
		{
			if ((int)cam.get_clearFlags() == 2)
			{
				GL.Clear(true, true, Color.get_black());
			}
			if (!(renderTexture == null))
			{
				if (renderTexture.get_width() != Screen.get_width() || renderTexture.get_height() != Screen.get_height())
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
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (!cacheAfter)
		{
			Graphics.Blit(src, renderTexture);
		}
		if (postEffectProc != null)
		{
			postEffectProc.Invoke(src, dest);
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
