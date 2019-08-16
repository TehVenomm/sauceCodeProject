using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderTargetSetter : MonoBehaviour
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
		renderTexture = null;
		if (grabCommand != null)
		{
			grabCommand.releaseRenderTexture(this.get_gameObject());
		}
	}

	private IEnumerator Start()
	{
		if (infos == null)
		{
			yield break;
		}
		while (!MonoBehaviourSingleton<AppMain>.IsValid() || MonoBehaviourSingleton<AppMain>.I.mainCamera == null)
		{
			yield return null;
		}
		grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().GetComponent<GrabCommand>();
		if (grabCommand == null)
		{
			grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_gameObject().AddComponent<GrabCommand>();
			grabCommand.ApplyCommandBuffer(cameraEvent);
		}
		renderTexture = grabCommand.useRenderTexture(this.get_gameObject());
		for (int i = 0; i < infos.Length; i++)
		{
			if (infos[i].targetRenderer == null || string.IsNullOrEmpty(infos[i].texturePropertyName))
			{
				continue;
			}
			TextureSetInfo textureSetInfo = infos[i];
			Renderer targetRenderer = textureSetInfo.targetRenderer;
			for (int j = 0; j < targetRenderer.get_sharedMaterials().Length; j++)
			{
				if (targetRenderer.get_sharedMaterials()[j].HasProperty(textureSetInfo.texturePropertyName))
				{
					targetRenderer.get_sharedMaterials()[j].SetTexture(textureSetInfo.texturePropertyName, renderTexture);
				}
			}
		}
	}
}
