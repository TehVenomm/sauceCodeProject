using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffectCtrl : MonoBehaviour
{
	[Serializable]
	private class ColorSet
	{
		[SerializeField]
		public Color rimColor = Color.black;

		[SerializeField]
		public Color innerColor = Color.black;
	}

	private Transform _transform;

	[SerializeField]
	[Tooltip("LOOPを指定")]
	private Transform targetRotateRoot;

	[SerializeField]
	[Tooltip("HITを指定")]
	private Transform hitEffectRoot;

	[SerializeField]
	[Tooltip("エフェクトのク\u30fcルタイム")]
	private float effectTime = 0.5f;

	[SerializeField]
	[Tooltip("1秒で回転する角度")]
	private float rotateSpeed = 180f;

	[SerializeField]
	[Tooltip("シ\u30fcルドHPが0の時のScale")]
	private Vector3 afterScale = Vector3.zero;

	[SerializeField]
	[Tooltip("Element0(HP MAX),Element1,...,ElementN(HP 0)の順でシ\u30fcルドHPに合わせて変化する")]
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

	private readonly Vector3 VECTOR_UP = Vector3.up;

	private readonly Vector3 VECTOR_ONE = Vector3.one;

	private void Start()
	{
		_transform = base.transform;
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
					component.enabled = true;
					list2.Add(component);
					if (component.material != null)
					{
						list.Add(component.material);
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
		_transform.localRotation = Quaternion.identity;
	}

	private void Update()
	{
		if (targetCharacter == null || targetObject == null)
		{
			base.enabled = false;
			EffectManager.ReleaseEffect(base.gameObject, isPlayEndAnimation: false);
			return;
		}
		if (targetCharacter.actionID == (Character.ACTION_ID)36 && !isWarping)
		{
			SetActiveRenderer(active: false);
			isWarping = true;
		}
		if (targetCharacter.actionID != (Character.ACTION_ID)36 && isWarping)
		{
			SetActiveRenderer(active: true);
			isWarping = false;
		}
		if (targetRotateRoot != null)
		{
			if (isSetOtherParent)
			{
				_transform.position = targetCharacter._transform.position;
			}
			if (rotateSpeed != 0f)
			{
				targetRotateRoot.Rotate(VECTOR_UP, rotateSpeed * Time.deltaTime);
				hitEffectRoot.Rotate(VECTOR_UP, rotateSpeed * Time.deltaTime);
			}
		}
		float num = (float)(int)targetCharacter.ShieldHp / (float)(int)targetCharacter.ShieldHpMax;
		if (cache_rate == num)
		{
			return;
		}
		Vector3 localScale = num * VECTOR_ONE + (1f - num) * afterScale;
		Transform[] array = targetObject;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].localScale = localScale;
		}
		if (colorVariation != null && colorVariation.Length > 1)
		{
			float num2 = 1f / (float)(colorVariation.Length - 1);
			float num3 = 1f - num;
			for (int j = 1; j < colorVariation.Length; j++)
			{
				if (!(num3 < num2 * (float)j))
				{
					continue;
				}
				float t = (num2 * (float)j - num3) / num2;
				for (int k = 0; k < targetMaterial.Length; k++)
				{
					if (targetMaterial[k].HasProperty(ID_RIM_COLOR))
					{
						Color value = Color.Lerp(colorVariation[j].rimColor, colorVariation[j - 1].rimColor, t);
						targetMaterial[k].SetColor(ID_RIM_COLOR, value);
					}
					if (targetMaterial[k].HasProperty(ID_INNER_COLOR))
					{
						Color value2 = Color.Lerp(colorVariation[j].innerColor, colorVariation[j - 1].innerColor, t);
						targetMaterial[k].SetColor(ID_INNER_COLOR, value2);
					}
				}
				break;
			}
		}
		if (num < cache_rate)
		{
			StartCoroutine(PlayHitEffect(hitEffectRoot.gameObject));
		}
		cache_rate = num;
	}

	private IEnumerator PlayHitEffect(GameObject go)
	{
		if (!(go == null))
		{
			go.SetActive(value: true);
			yield return new WaitForSeconds(effectTime);
			go.SetActive(value: false);
		}
	}

	private Character GetTargetCharacter(Transform child)
	{
		Character component = child.GetComponent<Character>();
		if (component != null)
		{
			return component;
		}
		if (child.parent == null)
		{
			return null;
		}
		return GetTargetCharacter(child.parent);
	}

	private void SetActiveRenderer(bool active)
	{
		if (targetRenderer.IsNullOrEmpty())
		{
			return;
		}
		for (int i = 0; i < targetRenderer.Length; i++)
		{
			if (!(targetRenderer[i] == null))
			{
				targetRenderer[i].enabled = active;
			}
		}
	}
}
