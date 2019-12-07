using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColorCtrl : MonoBehaviour
{
	[Serializable]
	public class ColorSet
	{
		public Color rimColor;

		public Color innerColor;
	}

	private int ID_RIM_COLOR;

	private int ID_INNER_COLOR;

	[SerializeField]
	private ColorSet[] colorVariation;

	[SerializeField]
	private Transform hitEffectTrans;

	[SerializeField]
	private float effectCoolTime = 0.5f;

	private Transform[] transforms;

	private Material[] materials;

	private void Start()
	{
		ID_RIM_COLOR = Shader.PropertyToID("_RimColor");
		ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");
		transforms = base.transform.GetComponentsInChildren<Transform>();
		List<Material> list = new List<Material>();
		new List<Renderer>();
		if (!transforms.IsNullOrEmpty())
		{
			int i = 0;
			for (int num = transforms.Length; i < num; i++)
			{
				Renderer component = transforms[i].GetComponent<Renderer>();
				if (component != null)
				{
					component.enabled = true;
					if (component.material != null)
					{
						list.Add(component.material);
					}
				}
			}
		}
		materials = list.ToArray();
	}

	public void UpdateColor(float rate)
	{
		if (colorVariation.IsNullOrEmpty() || colorVariation.Length <= 1)
		{
			return;
		}
		float num = 1f / (float)(colorVariation.Length - 1);
		float num2 = 1f - rate;
		int num3 = 1;
		int num4 = colorVariation.Length;
		while (true)
		{
			if (num3 < num4)
			{
				if (num2 < num * (float)num3)
				{
					break;
				}
				num3++;
				continue;
			}
			return;
		}
		float t = (num * (float)num3 - num2) / num;
		int i = 0;
		for (int num5 = materials.Length; i < num5; i++)
		{
			if (materials[i].HasProperty(ID_RIM_COLOR))
			{
				Color value = Color.Lerp(colorVariation[num3].rimColor, colorVariation[num3 - 1].rimColor, t);
				materials[i].SetColor(ID_RIM_COLOR, value);
			}
			if (materials[i].HasProperty(ID_INNER_COLOR))
			{
				Color value2 = Color.Lerp(colorVariation[num3].innerColor, colorVariation[num3 - 1].innerColor, t);
				materials[i].SetColor(ID_INNER_COLOR, value2);
			}
		}
	}

	public void PlayHitEffect()
	{
		if (!(hitEffectTrans == null))
		{
			StartCoroutine(PlayHitEffect(hitEffectTrans.gameObject));
		}
	}

	private IEnumerator PlayHitEffect(GameObject obj)
	{
		if (!(obj == null))
		{
			obj.SetActive(value: true);
			yield return new WaitForSeconds(effectCoolTime);
			obj.SetActive(value: false);
		}
	}
}
