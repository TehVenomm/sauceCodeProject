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

	public UIRenderQueueUpdater()
		: this()
	{
	}

	private void Awake()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		_renderer = this.GetComponent<Renderer>();
		if (_renderer != null && baseWidget != null)
		{
			_renderer.set_enabled(false);
			UIWidget uIWidget = baseWidget;
			uIWidget.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget.onRender, new UIDrawCall.OnRenderCallback(OnRender));
			Vector3 position = baseWidget.get_transform().get_position();
			float num = position.z + ((!offsetBack) ? (-0.5f) : 0.5f);
			this.get_transform().set_position(this.get_transform().get_position() + new Vector3(0f, 0f, num));
		}
	}

	private void OnRender(Material mat)
	{
		if (_renderer != null)
		{
			_renderer.get_material().set_renderQueue(mat.get_renderQueue());
			_renderer.set_enabled(baseWidget.get_enabled());
		}
	}
}
