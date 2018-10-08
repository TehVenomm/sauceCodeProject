using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffectCtrl
{
	[Serializable]
	private class ColorSet
	{
		[SerializeField]
		public Color rimColor;

		[SerializeField]
		public Color innerColor;
	}

	private Transform _transform;

	[Tooltip("LOOPを指定")]
	[SerializeField]
	private Transform targetRotateRoot;

	[Tooltip("HITを指定")]
	[SerializeField]
	private Transform hitEffectRoot;

	[SerializeField]
	[Tooltip("エフェクトのク\u30fcルタイム")]
	private float effectTime = 0.5f;

	[Tooltip("1秒で回転する角度")]
	[SerializeField]
	private float rotateSpeed = 180f;

	[SerializeField]
	[Tooltip("シ\u30fcルドHPが0の時のScale")]
	private Vector3 afterScale;

	[Tooltip("Element0(HP MAX),Element1,...,ElementN(HP 0)の順でシ\u30fcルドHPに合わせて変化する")]
	[SerializeField]
	private ColorSet[] colorVariation;

	private Transform[] targetObject;

	private Material[] targetMaterial;

	private Renderer[] targetRenderer;

	private Character targetCharacter;

	private float cache_rate = -1f;

	private int ID_RIM_COLOR = -1;

	private int ID_INNER_COLOR = -1;

	private bool isSetOtherParent;

	private bool isWarping;

	private readonly Vector3 VECTOR_UP = Vector3.get_up();

	private readonly Vector3 VECTOR_ONE = Vector3.get_one();

	public ShieldEffectCtrl()
		: this()
	{
	}//IL_0030: Unknown result type (might be due to invalid IL or missing references)
	//IL_0035: Unknown result type (might be due to invalid IL or missing references)
	//IL_003b: Unknown result type (might be due to invalid IL or missing references)
	//IL_0040: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		targetCharacter = GetTargetCharacter(_transform);
		if (targetRotateRoot != null)
		{
			targetObject = targetRotateRoot.GetComponentsInChildren<Transform>();
		}
		ID_RIM_COLOR = Shader.PropertyToID("_RimColor");
		ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");
		List<Material> list = new List<Material>();
		List<Renderer> list2 = new List<Renderer>();
		if (targetObject != null)
		{
			for (int i = 0; i < targetObject.Length; i++)
			{
				Renderer component = targetObject[i].GetComponent<Renderer>();
				if (component != null)
				{
					component.set_enabled(true);
					list2.Add(component);
					if (component.get_material() != null)
					{
						list.Add(component.get_material());
					}
				}
			}
		}
		targetMaterial = list.ToArray();
		targetRenderer = list2.ToArray();
		if (rotateSpeed != 0f && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			_transform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
			isSetOtherParent = true;
		}
		_transform.set_localRotation(Quaternion.get_identity());
	}

	private void Update()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		if (targetCharacter == null || targetObject == null)
		{
			this.set_enabled(false);
			EffectManager.ReleaseEffect(this.get_gameObject(), false, false);
		}
		else
		{
			if (targetCharacter.actionID == (Character.ACTION_ID)35 && !isWarping)
			{
				SetActiveRenderer(false);
				isWarping = true;
			}
			if (targetCharacter.actionID != (Character.ACTION_ID)35 && isWarping)
			{
				SetActiveRenderer(true);
				isWarping = false;
			}
			if (targetRotateRoot != null)
			{
				if (isSetOtherParent)
				{
					_transform.set_position(targetCharacter._transform.get_position());
				}
				if (rotateSpeed != 0f)
				{
					targetRotateRoot.Rotate(VECTOR_UP, rotateSpeed * Time.get_deltaTime());
					hitEffectRoot.Rotate(VECTOR_UP, rotateSpeed * Time.get_deltaTime());
				}
			}
			float num = (float)(int)targetCharacter.ShieldHp / (float)(int)targetCharacter.ShieldHpMax;
			if (cache_rate != num)
			{
				Vector3 localScale = num * VECTOR_ONE + (1f - num) * afterScale;
				Transform[] array = targetObject;
				foreach (Transform val in array)
				{
					val.set_localScale(localScale);
				}
				if (colorVariation != null && colorVariation.Length > 1)
				{
					float num2 = 1f / (float)(colorVariation.Length - 1);
					float num3 = 1f - num;
					for (int j = 1; j < colorVariation.Length; j++)
					{
						if (num3 < num2 * (float)j)
						{
							float num4 = (num2 * (float)j - num3) / num2;
							for (int k = 0; k < targetMaterial.Length; k++)
							{
								if (targetMaterial[k].HasProperty(ID_RIM_COLOR))
								{
									Color val2 = Color.Lerp(colorVariation[j].rimColor, colorVariation[j - 1].rimColor, num4);
									targetMaterial[k].SetColor(ID_RIM_COLOR, val2);
								}
								if (targetMaterial[k].HasProperty(ID_INNER_COLOR))
								{
									Color val3 = Color.Lerp(colorVariation[j].innerColor, colorVariation[j - 1].innerColor, num4);
									targetMaterial[k].SetColor(ID_INNER_COLOR, val3);
								}
							}
							break;
						}
					}
				}
				if (num < cache_rate)
				{
					this.StartCoroutine(PlayHitEffect(hitEffectRoot.get_gameObject()));
				}
				cache_rate = num;
			}
		}
	}

	private IEnumerator PlayHitEffect(GameObject go)
	{
		if (!(go == null))
		{
			go.SetActive(true);
			yield return (object)new WaitForSeconds(effectTime);
			go.SetActive(false);
		}
	}

	private Character GetTargetCharacter(Transform child)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		Character component = child.GetComponent<Character>();
		if (component != null)
		{
			return component;
		}
		if (child.get_parent() == null)
		{
			return null;
		}
		return GetTargetCharacter(child.get_parent());
	}

	private void SetActiveRenderer(bool active)
	{
		if (!targetRenderer.IsNullOrEmpty())
		{
			for (int i = 0; i < targetRenderer.Length; i++)
			{
				if (!(targetRenderer[i] == null))
				{
					targetRenderer[i].set_enabled(active);
				}
			}
		}
	}
}
