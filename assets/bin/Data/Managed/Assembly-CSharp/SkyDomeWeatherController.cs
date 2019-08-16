using UnityEngine;

public class SkyDomeWeatherController : MonoBehaviour
{
	[SerializeField]
	private Renderer[] originalRenderer;

	[SerializeField]
	private Renderer[] afterRenderer;

	private int MATERIALCOLOR_PROPERTY_KEY;

	public SkyDomeWeatherController()
		: this()
	{
	}

	private void Awake()
	{
		MATERIALCOLOR_PROPERTY_KEY = Shader.PropertyToID("_MainColor");
		UpdateRenderers(0f);
	}

	public void UpdateRenderers(float rate)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (originalRenderer != null)
		{
			for (int i = 0; i < originalRenderer.Length; i++)
			{
				if (originalRenderer[i] == null)
				{
					continue;
				}
				Material[] sharedMaterials = originalRenderer[i].get_sharedMaterials();
				foreach (Material val in sharedMaterials)
				{
					if (!(val == null) && val.HasProperty(MATERIALCOLOR_PROPERTY_KEY))
					{
						Color color = val.GetColor(MATERIALCOLOR_PROPERTY_KEY);
						color.a = 1f - rate;
						val.SetColor(MATERIALCOLOR_PROPERTY_KEY, color);
					}
				}
			}
		}
		if (afterRenderer == null)
		{
			return;
		}
		for (int k = 0; k < afterRenderer.Length; k++)
		{
			if (afterRenderer[k] == null)
			{
				continue;
			}
			Material[] materials = afterRenderer[k].get_materials();
			for (int l = 0; l < afterRenderer[k].get_materials().Length; l++)
			{
				Material val2 = afterRenderer[k].get_sharedMaterials()[l];
				if (!(val2 == null) && val2.HasProperty(MATERIALCOLOR_PROPERTY_KEY))
				{
					Color color2 = val2.GetColor(MATERIALCOLOR_PROPERTY_KEY);
					color2.a = rate;
					val2.SetColor(MATERIALCOLOR_PROPERTY_KEY, color2);
				}
			}
		}
	}
}
