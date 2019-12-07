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
	private CameraEvent cameraEvent = CameraEvent.AfterForwardOpaque;

	private RenderTexture renderTexture;

	private GrabCommand grabCommand;

	private void OnDestroy()
	{
		renderTexture = null;
		if (grabCommand != null)
		{
			grabCommand.releaseRenderTexture(base.gameObject);
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
		grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.GetComponent<GrabCommand>();
		if (grabCommand == null)
		{
			grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.AddComponent<GrabCommand>();
			grabCommand.ApplyCommandBuffer(cameraEvent);
		}
		renderTexture = grabCommand.useRenderTexture(base.gameObject);
		for (int i = 0; i < infos.Length; i++)
		{
			if (infos[i].targetRenderer == null || string.IsNullOrEmpty(infos[i].texturePropertyName))
			{
				continue;
			}
			TextureSetInfo textureSetInfo = infos[i];
			Renderer targetRenderer = textureSetInfo.targetRenderer;
			for (int j = 0; j < targetRenderer.sharedMaterials.Length; j++)
			{
				if (targetRenderer.sharedMaterials[j].HasProperty(textureSetInfo.texturePropertyName))
				{
					targetRenderer.sharedMaterials[j].SetTexture(textureSetInfo.texturePropertyName, renderTexture);
				}
			}
		}
	}
}
