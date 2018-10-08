using UnityEngine;

public class SkyDomeWeatherController : MonoBehaviour
{
	[SerializeField]
	private Renderer[] originalRenderer;

	[SerializeField]
	private Renderer[] afterRenderer;

	private int MATERIALCOLOR_PROPERTY_KEY;

	private void Awake()
	{
		MATERIALCOLOR_PROPERTY_KEY = Shader.PropertyToID("_MainColor");
		UpdateRenderers(0f);
	}

	public void UpdateRenderers(float rate)
	{
		if (originalRenderer != null)
		{
			for (int i = 0; i < originalRenderer.Length; i++)
			{
				if (!((Object)originalRenderer[i] == (Object)null))
				{
					Material[] sharedMaterials = originalRenderer[i].sharedMaterials;
					foreach (Material material in sharedMaterials)
					{
						if (!((Object)material == (Object)null) && material.HasProperty(MATERIALCOLOR_PROPERTY_KEY))
						{
							Color color = material.GetColor(MATERIALCOLOR_PROPERTY_KEY);
							color.a = 1f - rate;
							material.SetColor(MATERIALCOLOR_PROPERTY_KEY, color);
						}
					}
				}
			}
		}
		if (afterRenderer != null)
		{
			for (int k = 0; k < afterRenderer.Length; k++)
			{
				if (!((Object)afterRenderer[k] == (Object)null))
				{
					Material[] materials = afterRenderer[k].materials;
					for (int l = 0; l < afterRenderer[k].materials.Length; l++)
					{
						Material material2 = afterRenderer[k].sharedMaterials[l];
						if (!((Object)material2 == (Object)null) && material2.HasProperty(MATERIALCOLOR_PROPERTY_KEY))
						{
							Color color2 = material2.GetColor(MATERIALCOLOR_PROPERTY_KEY);
							color2.a = rate;
							material2.SetColor(MATERIALCOLOR_PROPERTY_KEY, color2);
						}
					}
				}
			}
		}
	}
}
