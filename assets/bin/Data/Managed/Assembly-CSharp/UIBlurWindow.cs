using UnityEngine;

public class UIBlurWindow : MonoBehaviour
{
	private Material mat;

	private void Start()
	{
		if ((Object)mat == (Object)null)
		{
			Renderer component = GetComponent<Renderer>();
			if ((Object)component == (Object)null)
			{
				return;
			}
			mat = component.material;
		}
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		if (!((Object)mainCamera == (Object)null))
		{
			RenderTargetCacher component2 = mainCamera.GetComponent<RenderTargetCacher>();
			if (!((Object)component2 == (Object)null))
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
