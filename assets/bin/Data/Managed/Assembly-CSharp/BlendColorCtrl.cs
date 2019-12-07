using System.Collections.Generic;
using UnityEngine;

public class BlendColorCtrl
{
	private class floatUpdater
	{
		public float nValue;

		public float tValue;

		public float speed;

		public bool isEnd;

		public void CalcSpeed(float sec)
		{
			speed = (tValue - nValue) / sec;
		}

		public void Update()
		{
			if (isEnd)
			{
				return;
			}
			float num = speed * Time.deltaTime;
			nValue += num;
			if (num > 0f)
			{
				if (nValue >= tValue)
				{
					nValue = tValue;
					isEnd = true;
				}
			}
			else if (nValue <= tValue)
			{
				nValue = tValue;
				isEnd = true;
			}
		}
	}

	private class ShaderParam
	{
		public string shaderName = "";

		public string propetyName = "";

		public bool forceEndFlag;

		public bool aliveFlag;

		public bool isColor;

		public floatUpdater[] fColor = new floatUpdater[3];

		public bool isBlend;

		public floatUpdater fBlend = new floatUpdater();

		public bool isBlendEnable;

		public bool blendEnable;

		public void Init()
		{
			for (int i = 0; i < 3; i++)
			{
				fColor[i] = new floatUpdater();
			}
		}

		public ShaderSyncParam GetSyncParam()
		{
			return new ShaderSyncParam
			{
				shaderName = shaderName,
				propetyName = propetyName,
				isColor = isColor,
				color = new Color(fColor[0].tValue, fColor[1].tValue, fColor[2].tValue),
				isBlendRate = isBlend,
				blendRate = fBlend.tValue,
				isBlendEnable = isBlendEnable,
				blendEnable = blendEnable
			};
		}
	}

	public class ShaderSyncParam
	{
		public string shaderName;

		public string propetyName;

		public bool isColor;

		public Color color;

		public bool isBlendRate;

		public float blendRate;

		public bool isBlendEnable;

		public bool blendEnable;
	}

	public const string kDefaultShaderName = "enemy_custamaizable_blend";

	public const string kDefaultPropetyName = "_BlendColor";

	private const int kColorElementNum = 3;

	private Color cacheColor = Color.white;

	private List<Material> materialList = new List<Material>();

	private Dictionary<string, ShaderParam> shaderParams = new Dictionary<string, ShaderParam>();

	public void Enable(AnimEventData.EventData data, bool enable, SkinnedMeshRenderer[] renderers)
	{
		string text = "enemy_custamaizable_blend";
		if (!data.stringArgs.IsNullOrEmpty())
		{
			text = data.stringArgs[0];
		}
		ShaderParam shaderParam;
		if (shaderParams.ContainsKey(text))
		{
			shaderParam = shaderParams[text];
		}
		else
		{
			shaderParam = new ShaderParam();
			shaderParam.Init();
			shaderParams.Add(text, shaderParam);
		}
		shaderParam.shaderName = text;
		shaderParam.isBlendEnable = true;
		shaderParam.blendEnable = enable;
		int i = 0;
		for (int num = renderers.Length; i < num; i++)
		{
			int j = 0;
			for (int num2 = renderers[i].materials.Length; j < num2; j++)
			{
				Material material = renderers[i].materials[j];
				if (material.shader.name.Contains(text))
				{
					material.SetFloat("_BlendEnable", enable ? 1f : 0f);
				}
			}
		}
	}

	public void Change(AnimEventData.EventData data, SkinnedMeshRenderer[] renderers)
	{
		if (renderers.IsNullOrEmpty())
		{
			return;
		}
		string text = "enemy_custamaizable_blend";
		string propetyName = "_BlendColor";
		if (!data.stringArgs.IsNullOrEmpty())
		{
			text = data.stringArgs[0];
			if (data.stringArgs.Length > 1)
			{
				propetyName = data.stringArgs[1];
			}
		}
		ShaderParam shaderParam;
		if (shaderParams.ContainsKey(text))
		{
			shaderParam = shaderParams[text];
		}
		else
		{
			shaderParam = new ShaderParam();
			shaderParam.Init();
			shaderParams.Add(text, shaderParam);
		}
		shaderParam.shaderName = text;
		shaderParam.propetyName = propetyName;
		for (int i = 0; i < 3; i++)
		{
			bool isEnd = false;
			float num = (float)data.intArgs[i] / 255f;
			if (num < 0f)
			{
				num = 0f;
				isEnd = true;
			}
			if (num > 1f)
			{
				num = 1f;
				isEnd = true;
			}
			shaderParam.fColor[i].isEnd = isEnd;
			shaderParam.fColor[i].tValue = num;
		}
		if (data.intArgs.Length > 3)
		{
			shaderParam.forceEndFlag = ((data.intArgs[3] != 0) ? true : false);
		}
		bool isEnd2 = false;
		float num2 = data.floatArgs[0];
		if (num2 < -1f)
		{
			num2 = -1f;
			isEnd2 = true;
		}
		if (num2 > 1f)
		{
			num2 = 1f;
			isEnd2 = true;
		}
		shaderParam.fBlend.isEnd = isEnd2;
		shaderParam.fBlend.tValue = num2;
		float num3 = data.floatArgs[1];
		shaderParam.aliveFlag = false;
		materialList.Clear();
		bool flag = true;
		if (num3 == 0f)
		{
			Color value = Color.white;
			int j = 0;
			for (int num4 = renderers.Length; j < num4; j++)
			{
				int k = 0;
				for (int num5 = renderers[j].materials.Length; k < num5; k++)
				{
					Material material = renderers[j].materials[k];
					if (!material.shader.name.Contains(shaderParam.shaderName))
					{
						continue;
					}
					if (flag)
					{
						value = material.GetColor(shaderParam.propetyName);
						if (!shaderParam.fColor[0].isEnd)
						{
							value.r = shaderParam.fColor[0].tValue;
						}
						if (!shaderParam.fColor[1].isEnd)
						{
							value.g = shaderParam.fColor[1].tValue;
						}
						if (!shaderParam.fColor[2].isEnd)
						{
							value.b = shaderParam.fColor[2].tValue;
						}
						flag = false;
					}
					material.SetColor(shaderParam.propetyName, value);
					if (!shaderParam.fBlend.isEnd)
					{
						material.SetFloat("_BlendRate", shaderParam.fBlend.tValue);
					}
				}
			}
		}
		else
		{
			int l = 0;
			for (int num6 = renderers.Length; l < num6; l++)
			{
				int m = 0;
				for (int num7 = renderers[l].materials.Length; m < num7; m++)
				{
					Material material2 = renderers[l].materials[m];
					if (!material2.shader.name.Contains(shaderParam.shaderName))
					{
						continue;
					}
					if (flag)
					{
						Color color = material2.GetColor(shaderParam.propetyName);
						shaderParam.fColor[0].nValue = color.r;
						if (shaderParam.fColor[0].isEnd)
						{
							shaderParam.fColor[0].tValue = shaderParam.fColor[0].nValue;
						}
						shaderParam.fColor[1].nValue = color.g;
						if (shaderParam.fColor[1].isEnd)
						{
							shaderParam.fColor[1].tValue = shaderParam.fColor[1].nValue;
						}
						shaderParam.fColor[2].nValue = color.b;
						if (shaderParam.fColor[2].isEnd)
						{
							shaderParam.fColor[2].tValue = shaderParam.fColor[2].nValue;
						}
						shaderParam.fBlend.nValue = material2.GetFloat("_BlendRate");
						if (shaderParam.fBlend.isEnd)
						{
							shaderParam.fBlend.tValue = shaderParam.fBlend.nValue;
						}
					}
					materialList.Add(material2);
					break;
				}
			}
			if (materialList.Count == 0)
			{
				Debug.LogError("not shader [" + shaderParam.shaderName + "]");
				return;
			}
			if (shaderParam.fColor[0].nValue == shaderParam.fColor[0].tValue && shaderParam.fColor[1].nValue == shaderParam.fColor[1].tValue && shaderParam.fColor[2].nValue == shaderParam.fColor[2].tValue && shaderParam.fBlend.tValue == shaderParam.fBlend.nValue && shaderParam.fBlend.tValue >= 0f)
			{
				return;
			}
			shaderParam.aliveFlag = true;
			for (int n = 0; n < 3; n++)
			{
				shaderParam.fColor[n].CalcSpeed(num3);
			}
			shaderParam.fBlend.CalcSpeed(num3);
		}
		shaderParam.isColor = true;
		if (!shaderParam.fBlend.isEnd)
		{
			shaderParam.isBlend = true;
		}
	}

	public void ForceEnd()
	{
		foreach (KeyValuePair<string, ShaderParam> shaderParam in shaderParams)
		{
			ShaderParam value = shaderParam.Value;
			if (value.aliveFlag && value.forceEndFlag)
			{
				value.aliveFlag = false;
			}
		}
	}

	public void Update()
	{
		if (!materialList.IsNullOrEmpty())
		{
			foreach (KeyValuePair<string, ShaderParam> shaderParam in shaderParams)
			{
				ShaderParam value = shaderParam.Value;
				if (value.aliveFlag)
				{
					for (int i = 0; i < 3; i++)
					{
						value.fColor[i].Update();
					}
					value.fBlend.Update();
					cacheColor.r = value.fColor[0].nValue;
					cacheColor.g = value.fColor[1].nValue;
					cacheColor.b = value.fColor[2].nValue;
					int j = 0;
					for (int count = materialList.Count; j < count; j++)
					{
						Material material = materialList[j];
						material.SetColor(value.propetyName, cacheColor);
						material.SetFloat("_BlendRate", value.fBlend.nValue);
					}
					if (value.fColor[0].isEnd && value.fColor[1].isEnd && value.fColor[2].isEnd && value.fBlend.isEnd)
					{
						value.aliveFlag = false;
					}
				}
			}
		}
	}

	public List<ShaderSyncParam> GetShaderParamList()
	{
		if (shaderParams.Count == 0)
		{
			return null;
		}
		List<ShaderSyncParam> list = new List<ShaderSyncParam>();
		foreach (KeyValuePair<string, ShaderParam> shaderParam in shaderParams)
		{
			list.Add(shaderParam.Value.GetSyncParam());
		}
		return list;
	}

	public void Sync(SkinnedMeshRenderer[] renderers, List<ShaderSyncParam> shaderParamList)
	{
		if (renderers.IsNullOrEmpty() || shaderParamList.IsNullOrEmpty())
		{
			return;
		}
		for (int i = 0; i < shaderParamList.Count; i++)
		{
			ShaderSyncParam shaderSyncParam = shaderParamList[i];
			ShaderParam shaderParam;
			if (shaderParams.ContainsKey(shaderSyncParam.shaderName))
			{
				shaderParam = shaderParams[shaderSyncParam.shaderName];
			}
			else
			{
				shaderParam = new ShaderParam();
				shaderParam.Init();
				shaderParam.shaderName = shaderSyncParam.shaderName;
				shaderParam.propetyName = shaderSyncParam.propetyName;
				shaderParams.Add(shaderSyncParam.shaderName, shaderParam);
			}
			shaderParam.shaderName = shaderSyncParam.shaderName;
			shaderParam.propetyName = shaderSyncParam.propetyName;
			shaderParam.isColor = shaderSyncParam.isColor;
			if (shaderParam.isColor)
			{
				shaderParam.fColor[0].tValue = shaderSyncParam.color.r;
				shaderParam.fColor[0].isEnd = true;
				shaderParam.fColor[1].tValue = shaderSyncParam.color.g;
				shaderParam.fColor[1].isEnd = true;
				shaderParam.fColor[2].tValue = shaderSyncParam.color.b;
				shaderParam.fColor[2].isEnd = true;
			}
			shaderParam.isBlend = shaderSyncParam.isBlendRate;
			if (shaderParam.isBlend)
			{
				shaderParam.fBlend.tValue = shaderSyncParam.blendRate;
				shaderParam.fBlend.isEnd = true;
			}
			shaderParam.isBlendEnable = shaderSyncParam.isBlendEnable;
			shaderParam.blendEnable = shaderSyncParam.blendEnable;
			int j = 0;
			for (int num = renderers.Length; j < num; j++)
			{
				int k = 0;
				for (int num2 = renderers[j].materials.Length; k < num2; k++)
				{
					Material material = renderers[j].materials[k];
					if (material.shader.name.Contains(shaderParam.shaderName))
					{
						if (shaderParam.isColor)
						{
							material.SetColor(shaderParam.propetyName, shaderSyncParam.color);
						}
						if (shaderParam.isBlend)
						{
							material.SetFloat("_BlendRate", shaderSyncParam.blendRate);
						}
						if (shaderParam.isBlendEnable)
						{
							material.SetFloat("_BlendEnable", shaderParam.blendEnable ? 1f : 0f);
						}
					}
				}
			}
		}
	}
}
