using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GrabCommand : MonoBehaviour
{
	public const float TEXTURE_REDUCE_RATE_FOR_IPHONE5 = 0.5f;

	public const int DEPTH_BUFFER = 0;

	private Camera _camera;

	private CommandBuffer _commandBuffer;

	private RenderTexture renderTexture;

	private CameraEvent cameraEvent = CameraEvent.AfterForwardOpaque;

	private List<GameObject> referenceObjects = new List<GameObject>(10);

	public RenderTexture useRenderTexture(GameObject parent)
	{
		if (!referenceObjects.Contains(parent))
		{
			referenceObjects.Add(parent);
		}
		return renderTexture;
	}

	public void releaseRenderTexture(GameObject parent)
	{
		referenceObjects.Remove(parent);
		if (referenceObjects.Count == 0)
		{
			Object.Destroy(this);
		}
	}

	private void OnDestroy()
	{
		if ((Object)renderTexture != (Object)null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = null;
		}
		if ((Object)_camera != (Object)null && _commandBuffer != null)
		{
			_camera.RemoveCommandBuffer(cameraEvent, _commandBuffer);
		}
	}

	private void CreateTexture()
	{
		if (!((Object)renderTexture != (Object)null))
		{
			int width = Screen.width;
			int height = Screen.height;
			renderTexture = RenderTexture.GetTemporary(width, height);
		}
	}

	public void ApplyCommandBuffer(CameraEvent _cameraEvent)
	{
		cameraEvent = _cameraEvent;
		_camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		CreateTexture();
		_commandBuffer = new CommandBuffer();
		_commandBuffer.name = "Grab texture";
		_commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, renderTexture);
		_camera.AddCommandBuffer(cameraEvent, _commandBuffer);
	}
}
