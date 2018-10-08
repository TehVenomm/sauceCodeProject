using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GrabCommand
{
	public const float TEXTURE_REDUCE_RATE_FOR_IPHONE5 = 0.5f;

	public const int DEPTH_BUFFER = 0;

	private Camera _camera;

	private CommandBuffer _commandBuffer;

	private RenderTexture renderTexture;

	private CameraEvent cameraEvent = 11;

	private List<GameObject> referenceObjects = new List<GameObject>(10);

	public GrabCommand()
		: this()
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)


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
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = null;
		}
		if (_camera != null && _commandBuffer != null)
		{
			_camera.RemoveCommandBuffer(cameraEvent, _commandBuffer);
		}
	}

	private void CreateTexture()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		if (!(renderTexture != null))
		{
			int width = Screen.get_width();
			int height = Screen.get_height();
			renderTexture = RenderTexture.GetTemporary(width, height);
		}
	}

	public void ApplyCommandBuffer(CameraEvent _cameraEvent)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		cameraEvent = _cameraEvent;
		_camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		CreateTexture();
		_commandBuffer = new CommandBuffer();
		_commandBuffer.set_name("Grab texture");
		_commandBuffer.Blit(RenderTargetIdentifier.op_Implicit(1), RenderTargetIdentifier.op_Implicit(renderTexture));
		_camera.AddCommandBuffer(cameraEvent, _commandBuffer);
	}
}
