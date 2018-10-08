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

	private static readonly int ID_RIM_COLOR = Shader.PropertyToID("_RimColor");

	private static readonly int ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");

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
		transforms = base.transform.GetComponentsInChildren<Transform>();
		List<Material> list = new List<Material>();
		List<Renderer> list2 = new List<Renderer>();
		if (!transforms.IsNullOrEmpty())
		{
			int i = 0;
			for (int num = transforms.Length; i < num; i++)
			{
				Renderer component = transforms[i].GetComponent<Renderer>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					component.enabled = true;
					if ((UnityEngine.Object)component.material != (UnityEngine.Object)null)
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
		if (!colorVariation.IsNullOrEmpty() && colorVariation.Length > 1)
		{
			float num = 1f / (float)(colorVariation.Length - 1);
			float num2 = 1f - rate;
			int num3 = 1;
			int num4 = colorVariation.Length;
			while (true)
			{
				if (num3 >= num4)
				{
					return;
				}
				if (num2 < num * (float)num3)
				{
					break;
				}
				num3++;
			}
			float t = (num * (float)num3 - num2) / num;
			int i = 0;
			for (int num5 = materials.Length; i < num5; i++)
			{
				if (materials[i].HasProperty(ID_RIM_COLOR))
				{
					Color color = Color.Lerp(colorVariation[num3].rimColor, colorVariation[num3 - 1].rimColor, t);
					materials[i].SetColor(ID_RIM_COLOR, color);
				}
				if (materials[i].HasProperty(ID_INNER_COLOR))
				{
					Color color2 = Color.Lerp(colorVariation[num3].innerColor, colorVariation[num3 - 1].innerColor, t);
					materials[i].SetColor(ID_INNER_COLOR, color2);
				}
			}
		}
	}

	public void PlayHitEffect()
	{
		if (!((UnityEngine.Object)hitEffectTrans == (UnityEngine.Object)null))
		{
			StartCoroutine(PlayHitEffect(hitEffectTrans.gameObject));
		}
	}

	private IEnumerator PlayHitEffect(GameObject obj)
	{
		if (!((UnityEngine.Object)obj == (UnityEngine.Object)null))
		{
			obj.SetActive(true);
			yield return (object)new WaitForSeconds(effectCoolTime);
			obj.SetActive(false);
		}
	}
}
