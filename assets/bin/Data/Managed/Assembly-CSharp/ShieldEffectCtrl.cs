// Decompiled with JetBrains decompiler
// Type: ShieldEffectCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ShieldEffectCtrl : MonoBehaviour
{
  private Transform _transform;
  [SerializeField]
  [Tooltip("LOOPを指定")]
  private Transform targetRotateRoot;
  [SerializeField]
  [Tooltip("HITを指定")]
  private Transform hitEffectRoot;
  [SerializeField]
  [Tooltip("エフェクトのクールタイム")]
  private float effectTime = 0.5f;
  [SerializeField]
  [Tooltip("1秒で回転する角度")]
  private float rotateSpeed = 180f;
  [SerializeField]
  [Tooltip("シールドHPが0の時のScale")]
  private Vector3 afterScale = Vector3.zero;
  [SerializeField]
  [Tooltip("Element0(HP MAX),Element1,...,ElementN(HP 0)の順でシールドHPに合わせて変化する")]
  private ShieldEffectCtrl.ColorSet[] colorVariation;
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
    this._transform = ((Component) this).transform;
    this.targetCharacter = this.GetTargetCharacter(this._transform);
    if (Object.op_Inequality((Object) this.targetRotateRoot, (Object) null))
      this.targetObject = ((Component) this.targetRotateRoot).GetComponentsInChildren<Transform>();
    this.ID_RIM_COLOR = Shader.PropertyToID("_RimColor");
    this.ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");
    List<Material> materialList = new List<Material>();
    List<Renderer> rendererList = new List<Renderer>();
    if (this.targetObject != null)
    {
      for (int index = 0; index < this.targetObject.Length; ++index)
      {
        Renderer component = ((Component) this.targetObject[index]).GetComponent<Renderer>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.enabled = true;
          rendererList.Add(component);
          if (Object.op_Inequality((Object) component.material, (Object) null))
            materialList.Add(component.material);
        }
      }
    }
    this.targetMaterial = materialList.ToArray();
    this.targetRenderer = rendererList.ToArray();
    if ((double) this.rotateSpeed != 0.0 && MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      this._transform.SetParent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
      this.isSetOtherParent = true;
    }
    this._transform.localRotation = Quaternion.identity;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.targetCharacter, (Object) null) || this.targetObject == null)
    {
      ((Behaviour) this).enabled = false;
      EffectManager.ReleaseEffect(((Component) this).gameObject, false);
    }
    else
    {
      if (this.targetCharacter.actionID == (Character.ACTION_ID) 36 && !this.isWarping)
      {
        this.SetActiveRenderer(false);
        this.isWarping = true;
      }
      if (this.targetCharacter.actionID != (Character.ACTION_ID) 36 && this.isWarping)
      {
        this.SetActiveRenderer(true);
        this.isWarping = false;
      }
      if (Object.op_Inequality((Object) this.targetRotateRoot, (Object) null))
      {
        if (this.isSetOtherParent)
          this._transform.position = this.targetCharacter._transform.position;
        if ((double) this.rotateSpeed != 0.0)
        {
          this.targetRotateRoot.Rotate(this.VECTOR_UP, this.rotateSpeed * Time.deltaTime);
          this.hitEffectRoot.Rotate(this.VECTOR_UP, this.rotateSpeed * Time.deltaTime);
        }
      }
      float num1 = (float) (int) this.targetCharacter.ShieldHp / (float) (int) this.targetCharacter.ShieldHpMax;
      if ((double) this.cache_rate == (double) num1)
        return;
      Vector3 vector3 = Vector3.op_Addition(Vector3.op_Multiply(num1, this.VECTOR_ONE), Vector3.op_Multiply(1f - num1, this.afterScale));
      foreach (Transform transform in this.targetObject)
        transform.localScale = vector3;
      if (this.colorVariation != null && this.colorVariation.Length > 1)
      {
        float num2 = 1f / (float) (this.colorVariation.Length - 1);
        float num3 = 1f - num1;
        for (int index1 = 1; index1 < this.colorVariation.Length; ++index1)
        {
          if ((double) num3 < (double) num2 * (double) index1)
          {
            float num4 = (num2 * (float) index1 - num3) / num2;
            for (int index2 = 0; index2 < this.targetMaterial.Length; ++index2)
            {
              if (this.targetMaterial[index2].HasProperty(this.ID_RIM_COLOR))
              {
                Color color = Color.Lerp(this.colorVariation[index1].rimColor, this.colorVariation[index1 - 1].rimColor, num4);
                this.targetMaterial[index2].SetColor(this.ID_RIM_COLOR, color);
              }
              if (this.targetMaterial[index2].HasProperty(this.ID_INNER_COLOR))
              {
                Color color = Color.Lerp(this.colorVariation[index1].innerColor, this.colorVariation[index1 - 1].innerColor, num4);
                this.targetMaterial[index2].SetColor(this.ID_INNER_COLOR, color);
              }
            }
            break;
          }
        }
      }
      if ((double) num1 < (double) this.cache_rate)
        this.StartCoroutine(this.PlayHitEffect(((Component) this.hitEffectRoot).gameObject));
      this.cache_rate = num1;
    }
  }

  private IEnumerator PlayHitEffect(GameObject go)
  {
    if (!Object.op_Equality((Object) go, (Object) null))
    {
      go.SetActive(true);
      yield return (object) new WaitForSeconds(this.effectTime);
      go.SetActive(false);
    }
  }

  private Character GetTargetCharacter(Transform child)
  {
    Character component = ((Component) child).GetComponent<Character>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component;
    return Object.op_Equality((Object) child.parent, (Object) null) ? (Character) null : this.GetTargetCharacter(child.parent);
  }

  private void SetActiveRenderer(bool active)
  {
    if (((IList<Renderer>) this.targetRenderer).IsNullOrEmpty<Renderer>())
      return;
    for (int index = 0; index < this.targetRenderer.Length; ++index)
    {
      if (!Object.op_Equality((Object) this.targetRenderer[index], (Object) null))
        this.targetRenderer[index].enabled = active;
    }
  }

  [Serializable]
  private class ColorSet
  {
    [SerializeField]
    public Color rimColor = Color.black;
    [SerializeField]
    public Color innerColor = Color.black;
  }
}
