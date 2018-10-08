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
		if ((UnityEngine.Object)grabCommand != (UnityEngine.Object)null)
		{
			grabCommand.releaseRenderTexture(base.gameObject);
		}
	}

	private IEnumerator Start()
	{
		if (infos != null)
		{
			while (!MonoBehaviourSingleton<AppMain>.IsValid() || (UnityEngine.Object)MonoBehaviourSingleton<AppMain>.I.mainCamera == (UnityEngine.Object)null)
			{
				yield return (object)null;
			}
			grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.GetComponent<GrabCommand>();
			if ((UnityEngine.Object)grabCommand == (UnityEngine.Object)null)
			{
				grabCommand = MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.AddComponent<GrabCommand>();
				grabCommand.ApplyCommandBuffer(cameraEvent);
			}
			renderTexture = grabCommand.useRenderTexture(base.gameObject);
			for (int j = 0; j < infos.Length; j++)
			{
				if (!((UnityEngine.Object)infos[j].targetRenderer == (UnityEngine.Object)null) && !string.IsNullOrEmpty(infos[j].texturePropertyName))
				{
					TextureSetInfo info = infos[j];
					Renderer renderer = info.targetRenderer;
					for (int i = 0; i < renderer.sharedMaterials.Length; i++)
					{
						if (renderer.sharedMaterials[i].HasProperty(info.texturePropertyName))
						{
							renderer.sharedMaterials[i].SetTexture(info.texturePropertyName, renderTexture);
						}
					}
				}
			}
		}
	}
}
