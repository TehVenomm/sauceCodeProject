using UnityEngine;

public class SceneParameter : MonoBehaviour
{
	public Texture2D[] lightmapsFar;

	public Texture2D[] lightmapsNear;

	public LightmapsMode lightmapMode;

	public LightProbes lightProbes;

	private void OnDisable()
	{
		LightmapSettings.lightmaps = null;
		LightmapSettings.lightProbes = null;
	}

	public void Apply()
	{
		if ((Object)lightProbes != (Object)null)
		{
			LightmapSettings.lightProbes = lightProbes;
			ShaderGlobal.lightProbe = true;
		}
		else
		{
			ShaderGlobal.lightProbe = false;
		}
		if (lightmapsFar != null && lightmapsFar.Length > 0)
		{
			LightmapData[] array = new LightmapData[lightmapsFar.Length];
			int i = 0;
			for (int num = lightmapsFar.Length; i < num; i++)
			{
				array[i] = new LightmapData();
				array[i].lightmapFar = lightmapsFar[i];
				if (i < lightmapsNear.Length)
				{
					array[i].lightmapNear = lightmapsNear[i];
				}
			}
			LightmapSettings.lightmapsMode = lightmapMode;
			LightmapSettings.lightmaps = array;
		}
	}
}
