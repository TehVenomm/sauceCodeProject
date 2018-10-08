using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColorCtrl
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

	public EffectColorCtrl()
		: this()
	{
	}

	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		transforms = this.get_transform().GetComponentsInChildren<Transform>();
		List<Material> list = new List<Material>();
		List<Renderer> list2 = new List<Renderer>();
		if (!transforms.IsNullOrEmpty())
		{
			int i = 0;
			for (int num = transforms.Length; i < num; i++)
			{
				Renderer component = transforms[i].GetComponent<Renderer>();
				if (component != null)
				{
					component.set_enabled(true);
					if (component.get_material() != null)
					{
						list.Add(component.get_material());
					}
				}
			}
		}
		materials = list.ToArray();
	}

	public void UpdateColor(float rate)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
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
			float num5 = (num * (float)num3 - num2) / num;
			int i = 0;
			for (int num6 = materials.Length; i < num6; i++)
			{
				if (materials[i].HasProperty(ID_RIM_COLOR))
				{
					Color val = Color.Lerp(colorVariation[num3].rimColor, colorVariation[num3 - 1].rimColor, num5);
					materials[i].SetColor(ID_RIM_COLOR, val);
				}
				if (materials[i].HasProperty(ID_INNER_COLOR))
				{
					Color val2 = Color.Lerp(colorVariation[num3].innerColor, colorVariation[num3 - 1].innerColor, num5);
					materials[i].SetColor(ID_INNER_COLOR, val2);
				}
			}
		}
	}

	public void PlayHitEffect()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (!(hitEffectTrans == null))
		{
			this.StartCoroutine(PlayHitEffect(hitEffectTrans.get_gameObject()));
		}
	}

	private IEnumerator PlayHitEffect(GameObject obj)
	{
		if (!(obj == null))
		{
			obj.SetActive(true);
			yield return (object)new WaitForSeconds(effectCoolTime);
			obj.SetActive(false);
		}
	}
}
