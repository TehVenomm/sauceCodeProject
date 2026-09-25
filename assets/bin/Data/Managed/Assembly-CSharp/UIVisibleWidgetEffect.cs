// Decompiled with JetBrains decompiler
// Type: UIVisibleWidgetEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIVisibleWidgetEffect : MonoBehaviour
{
  private UIPanel panel;
  private UIWidget widget;
  private string sectionName;
  private string effectName;
  private Transform effect;
  private int setRendererQueue = -1;

  public static void Set(
    UIPanel panel,
    UIWidget widget,
    string effect_name,
    string current_section_name)
  {
    if (Object.op_Equality((Object) widget, (Object) null))
      return;
    UIVisibleWidgetEffect visibleWidgetEffect = ((Component) widget).GetComponent<UIVisibleWidgetEffect>();
    if (effect_name == null)
    {
      if (!Object.op_Inequality((Object) visibleWidgetEffect, (Object) null))
        return;
      Object.Destroy((Object) visibleWidgetEffect);
    }
    else
    {
      if (Object.op_Equality((Object) visibleWidgetEffect, (Object) null))
        visibleWidgetEffect = ((Component) widget).gameObject.AddComponent<UIVisibleWidgetEffect>();
      visibleWidgetEffect.panel = panel;
      visibleWidgetEffect.widget = widget;
      if (string.IsNullOrEmpty(visibleWidgetEffect.sectionName))
        visibleWidgetEffect.sectionName = current_section_name ?? MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
      if (visibleWidgetEffect.effectName != effect_name)
        visibleWidgetEffect.DeleteEffect();
      visibleWidgetEffect.effectName = effect_name;
    }
  }

  public static void OneShot(
    UIPanel panel,
    UIWidget widget,
    string effect_name,
    string current_section_name)
  {
    EffectManager.GetUIEffect(effect_name, widget.cachedTransform, 0.0f, 1, widget);
  }

  private void LateUpdate()
  {
    if (this.sectionName == MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() && Object.op_Inequality((Object) MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection(), (Object) null) && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().state == UIBehaviour.STATE.OPEN && (Object.op_Equality((Object) this.panel, (Object) null) || this.panel.IsVisible(this.widget.cachedTransform.position)))
    {
      if (!Object.op_Equality((Object) this.effect, (Object) null))
        return;
      this.effect = EffectManager.GetUIEffect(this.effectName, this.widget.cachedTransform, 0.0f, 1, this.widget);
      if (Object.op_Equality((Object) this.effect, (Object) null))
        ((Behaviour) this).enabled = false;
      else
        this._SetRendererQueue();
    }
    else
      this.DeleteEffect();
  }

  private void DeleteEffect()
  {
    if (!Object.op_Inequality((Object) this.effect, (Object) null))
      return;
    EffectManager.ReleaseEffect(ref this.effect);
  }

  private void OnDisable() => this.DeleteEffect();

  private void _SetRendererQueue()
  {
    if (!Object.op_Inequality((Object) this.effect, (Object) null) || this.setRendererQueue == -1)
      return;
    foreach (Renderer componentsInChild in ((Component) this.effect).GetComponentsInChildren<Renderer>(true))
      componentsInChild.material.renderQueue = this.setRendererQueue;
  }

  public void SetRendererQueue(int setQueue)
  {
    this.setRendererQueue = setQueue;
    this._SetRendererQueue();
  }
}
