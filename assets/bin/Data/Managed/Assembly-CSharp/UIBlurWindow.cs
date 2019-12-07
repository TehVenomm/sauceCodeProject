using UnityEngine;

public class UIBlurWindow : MonoBehaviour
{
	private Material mat;

	private void Start()
	{
		if (mat == null)
		{
			Renderer component = GetComponent<Renderer>();
			if (component == null)
			{
				return;
			}
			mat = component.material;
		}
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (!(mainCamera == null))
		{
			RenderTargetCacher component2 = mainCamera.GetComponent<RenderTargetCacher>();
			if (!(component2 == null))
			{
				mat.mainTexture = component2.GetTexture();
			}
		}
	}

	private void Update()
	{
		Start();
	}
}
