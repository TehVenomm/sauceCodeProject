using System;
using UnityEngine;

public class UIRenderQueueUpdater : MonoBehaviour
{
	private const float OFFSET_GLOBAL_Z = 0.5f;

	[SerializeField]
	private UIWidget baseWidget;

	[SerializeField]
	private bool offsetBack;

	private Renderer _renderer;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();
		if (_renderer != null && baseWidget != null)
		{
			_renderer.enabled = false;
			UIWidget uIWidget = baseWidget;
			uIWidget.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget.onRender, new UIDrawCall.OnRenderCallback(OnRender));
			float z = baseWidget.transform.position.z + (offsetBack ? 0.5f : (-0.5f));
			base.transform.position = base.transform.position + new Vector3(0f, 0f, z);
		}
	}

	private void OnRender(Material mat)
	{
		if (_renderer != null)
		{
			_renderer.material.renderQueue = mat.renderQueue;
			_renderer.enabled = baseWidget.enabled;
		}
	}
}
