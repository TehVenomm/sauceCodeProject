using UnityEngine;

public class SceneParameter : MonoBehaviour
{
	public Texture2D[] lightmapsFar;

	public Texture2D[] lightmapsNear;

	public LightmapsMode lightmapMode;

	public LightProbes lightProbes;

	public SceneParameter()
		: this()
	{
	}

	private void OnDisable()
	{
		LightmapSettings.set_lightmaps((LightmapData[])null);
		LightmapSettings.set_lightProbes(null);
	}

	public void Apply()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		if (lightProbes != null)
		{
			LightmapSettings.set_lightProbes(lightProbes);
			ShaderGlobal.lightProbe = true;
		}
		else
		{
			ShaderGlobal.lightProbe = false;
		}
		if (lightmapsFar == null || lightmapsFar.Length <= 0)
		{
			return;
		}
		LightmapData[] array = (LightmapData[])new LightmapData[lightmapsFar.Length];
		int i = 0;
		for (int num = lightmapsFar.Length; i < num; i++)
		{
			array[i] = new LightmapData();
			array[i].set_lightmapColor(lightmapsFar[i]);
			if (i < lightmapsNear.Length)
			{
				array[i].set_lightmapDir(lightmapsNear[i]);
			}
		}
		LightmapSettings.set_lightmapsMode(lightmapMode);
		LightmapSettings.set_lightmaps(array);
	}
}
