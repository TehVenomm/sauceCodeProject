// Decompiled with JetBrains decompiler
// Type: EffectColorCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EffectColorCtrl : MonoBehaviour
{
  private int ID_RIM_COLOR;
  private int ID_INNER_COLOR;
  [SerializeField]
  private EffectColorCtrl.ColorSet[] colorVariation;
  [SerializeField]
  private Transform hitEffectTrans;
  [SerializeField]
  private float effectCoolTime = 0.5f;
  private Transform[] transforms;
  private Material[] materials;

  private void Start()
  {
    this.ID_RIM_COLOR = Shader.PropertyToID("_RimColor");
    this.ID_INNER_COLOR = Shader.PropertyToID("_InnerColor");
    this.transforms = ((Component) ((Component) this).transform).GetComponentsInChildren<Transform>();
    List<Material> materialList = new List<Material>();
    List<Renderer> rendererList = new List<Renderer>();
    if (!((IList<Transform>) this.transforms).IsNullOrEmpty<Transform>())
    {
      int index = 0;
      for (int length = this.transforms.Length; index < length; ++index)
      {
        Renderer component = ((Component) this.transforms[index]).GetComponent<Renderer>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.enabled = true;
          if (Object.op_Inequality((Object) component.material, (Object) null))
            materialList.Add(component.material);
        }
      }
    }
    this.materials = materialList.ToArray();
  }

  public void UpdateColor(float rate)
  {
    if (((IList<EffectColorCtrl.ColorSet>) this.colorVariation).IsNullOrEmpty<EffectColorCtrl.ColorSet>() || this.colorVariation.Length <= 1)
      return;
    float num1 = 1f / (float) (this.colorVariation.Length - 1);
    float num2 = 1f - rate;
    int index1 = 1;
    for (int length1 = this.colorVariation.Length; index1 < length1; ++index1)
    {
      if ((double) num2 < (double) num1 * (double) index1)
      {
        float num3 = (num1 * (float) index1 - num2) / num1;
        int index2 = 0;
        for (int length2 = this.materials.Length; index2 < length2; ++index2)
        {
          if (this.materials[index2].HasProperty(this.ID_RIM_COLOR))
          {
            Color color = Color.Lerp(this.colorVariation[index1].rimColor, this.colorVariation[index1 - 1].rimColor, num3);
            this.materials[index2].SetColor(this.ID_RIM_COLOR, color);
          }
          if (this.materials[index2].HasProperty(this.ID_INNER_COLOR))
          {
            Color color = Color.Lerp(this.colorVariation[index1].innerColor, this.colorVariation[index1 - 1].innerColor, num3);
            this.materials[index2].SetColor(this.ID_INNER_COLOR, color);
          }
        }
        break;
      }
    }
  }

  public void PlayHitEffect()
  {
    if (Object.op_Equality((Object) this.hitEffectTrans, (Object) null))
      return;
    this.StartCoroutine(this.PlayHitEffect(((Component) this.hitEffectTrans).gameObject));
  }

  private IEnumerator PlayHitEffect(GameObject obj)
  {
    if (!Object.op_Equality((Object) obj, (Object) null))
    {
      obj.SetActive(true);
      yield return (object) new WaitForSeconds(this.effectCoolTime);
      obj.SetActive(false);
    }
  }

  [Serializable]
  public class ColorSet
  {
    public Color rimColor;
    public Color innerColor;
  }
}
