using UnityEngine;

public class UIBlurWindow
{
	private Material mat;

	public UIBlurWindow()
		: this()
	{
	}

	private void Start()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (mat == null)
		{
			Renderer component = this.GetComponent<Renderer>();
			if (component == null)
			{
				return;
			}
			mat = component.get_material();
		}
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (!(mainCamera == null))
		{
			RenderTargetCacher component2 = mainCamera.GetComponent<RenderTargetCacher>();
			if (!(component2 == null))
			{
				mat.set_mainTexture(component2.GetTexture());
			}
		}
	}

	private void Update()
	{
		Start();
	}
}
