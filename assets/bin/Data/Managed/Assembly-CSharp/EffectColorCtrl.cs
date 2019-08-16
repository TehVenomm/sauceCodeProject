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

	public EffectColorCtrl()
		: this()
	{
	}

	private void Start()
	{
		ID_RIM_COLOR = Shader.PropertyToID("_RimColor");
		ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");
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
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
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

	public void PlayHitEffect()
	{
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
