// Decompiled with JetBrains decompiler
// Type: EffectStock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class EffectStock : MonoBehaviour
{
  public bool stocking;
  private Vector3 defaultPosition;
  private Quaternion defaultRotation;
  private Vector3 defaultScale;
  private int defaultLayer;
  private List<Component> defaultComponents = new List<Component>();
  private rymFX fx;
  private EffectCtrl ctrl;

  public bool IsLoop()
  {
    if (Object.op_Inequality((Object) this.fx, (Object) null))
      return this.fx.IsLoop();
    return Object.op_Inequality((Object) this.ctrl, (Object) null) && this.ctrl.loop;
  }

  private void Awake()
  {
    Transform transform = ((Component) this).transform;
    this.defaultPosition = transform.localPosition;
    this.defaultRotation = transform.localRotation;
    this.defaultScale = transform.localScale;
    this.defaultLayer = ((Component) this).gameObject.layer;
    ((Component) this).GetComponentsInChildren<Component>(true, this.defaultComponents);
    this.fx = ((Component) this).GetComponent<rymFX>();
    this.ctrl = ((Component) this).GetComponent<EffectCtrl>();
  }

  public void Stock()
  {
    if (this.stocking)
      return;
    ((Component) this).GetComponentsInChildren<Component>(true, Temporary.componentList);
    int num = 0;
    int count1 = this.defaultComponents.Count;
    int index1 = 0;
    for (int count2 = Temporary.componentList.Count; index1 < count2; ++index1)
    {
      Component component = Temporary.componentList[index1];
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        int index2;
        for (index2 = num; index2 < count1; ++index2)
        {
          if (Object.op_Equality((Object) this.defaultComponents[index2], (Object) component))
          {
            if (index2 == num)
            {
              ++num;
              break;
            }
            break;
          }
        }
        if (index2 == count1)
        {
          if (component is Transform)
            Object.DestroyImmediate((Object) component.gameObject);
          else
            Object.DestroyImmediate((Object) component);
        }
        Temporary.componentList[index1] = (Component) null;
      }
    }
    Temporary.componentList.Clear();
    if (Object.op_Inequality((Object) this.fx, (Object) null) && rymFXManager.DestroyFxDelegate != null)
      rymFXManager.DestroyFxDelegate.Invoke(this.fx);
    this.stocking = true;
  }

  public void Recycle(Transform parent, int layer = -1)
  {
    Transform transform = ((Component) this).transform;
    transform.parent = (Transform) null;
    ((Component) this).gameObject.SetActive(true);
    if (layer == -1)
      layer = this.defaultLayer;
    Utility.SetLayerWithChildren(transform, layer);
    int index = 0;
    for (int count = this.defaultComponents.Count; index < count; ++index)
    {
      Component defaultComponent = this.defaultComponents[index];
      switch (defaultComponent)
      {
        case Trail _:
          Trail trail = defaultComponent as Trail;
          ((Behaviour) trail).enabled = true;
          trail.Reset();
          trail.emit = true;
          break;
        case Renderer _:
          (defaultComponent as Renderer).enabled = true;
          break;
        case Animator _:
          Animator animator = defaultComponent as Animator;
          ((Behaviour) animator).enabled = true;
          RuntimeAnimatorController animatorController = animator.runtimeAnimatorController;
          animator.runtimeAnimatorController = (RuntimeAnimatorController) null;
          animator.runtimeAnimatorController = animatorController;
          break;
      }
    }
    transform.localPosition = this.defaultPosition;
    transform.localRotation = this.defaultRotation;
    transform.localScale = this.defaultScale;
    Utility.Attach(parent, transform);
    if (Object.op_Inequality((Object) this.fx, (Object) null))
    {
      this.fx.LoopEnd = false;
      rymFXManager.GetTextureFunc getTextureDelegate = rymFXManager.GetTextureDelegate;
      // ISSUE: method pointer
      rymFXManager.GetTextureDelegate = new rymFXManager.GetTextureFunc((object) this, __methodptr(GetTexture));
      this.fx.ResetImmediate();
      rymFXManager.GetTextureDelegate = getTextureDelegate;
    }
    if (Object.op_Inequality((Object) this.ctrl, (Object) null))
      this.ctrl.Reset();
    this.stocking = false;
  }

  private Texture GetTexture(string name)
  {
    ResourceLink component = ((Component) this.fx).GetComponent<ResourceLink>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(name);
      if (!string.IsNullOrEmpty(withoutExtension))
        return component.Get<Texture>(withoutExtension);
    }
    return (Texture) null;
  }
}
