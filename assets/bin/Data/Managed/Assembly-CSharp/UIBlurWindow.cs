using UnityEngine;

public class UIBlurWindow : MonoBehaviour
{
	private Material mat;

	public UIBlurWindow()
		: this()
	{
	}

	private void Start()
	{
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
