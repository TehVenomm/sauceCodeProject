using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderTargetSetter
{
	[Serializable]
	public class TextureSetInfo
	{
		public Renderer targetRenderer;

		public string texturePropertyName;
	}

	[SerializeField]
	private TextureSetInfo[] infos;

	[SerializeField]
	private CameraEvent cameraEvent = 11;

	private RenderTexture renderTexture;

	private GrabCommand grabCommand;

	public RenderTargetSetter()
		: this()
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)


	private void OnDestroy()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		renderTexture = null;
		if (grabCommand != null)
		{
			grabCommand.releaseRenderTexture(this.get_gameObject());
		}
	}

	private IEnumerator Start()
	{
		if (infos != null)
		{
			while (!MonoBehaviourSingleton<AppMain>.IsValid() || MonoBehaviourSingleton<AppMain>.I.mainCamera == null)
			{
				yield return (object)null;
			}
			grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().GetComponent<GrabCommand>();
			if (grabCommand == null)
			{
				grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().AddComponent<GrabCommand>();
				grabCommand.ApplyCommandBuffer(cameraEvent);
			}
			renderTexture = grabCommand.useRenderTexture(this.get_gameObject());
			for (int j = 0; j < infos.Length; j++)
			{
				if (!(infos[j].targetRenderer == null) && !string.IsNullOrEmpty(infos[j].texturePropertyName))
				{
					TextureSetInfo info = infos[j];
					Renderer renderer = info.targetRenderer;
					for (int i = 0; i < renderer.get_sharedMaterials().Length; i++)
					{
						if (renderer.get_sharedMaterials()[i].HasProperty(info.texturePropertyName))
						{
							renderer.get_sharedMaterials()[i].SetTexture(info.texturePropertyName, renderTexture);
						}
					}
				}
			}
		}
	}
}
