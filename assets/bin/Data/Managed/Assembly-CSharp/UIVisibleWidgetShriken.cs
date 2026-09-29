// Decompiled with JetBrains decompiler
// Type: UIVisibleWidgetShriken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIVisibleWidgetShriken : MonoBehaviour
{
  private UIPanel panel;
  private UIWidget widget;
  private string sectionName;
  private Transform effect;
  private Transform[] children;

  public static void Set(UIPanel panel, UIWidget widget)
  {
    UIVisibleWidgetShriken.Set(panel, widget, (string) null);
  }

  public static void Set(UIPanel panel, UIWidget widget, string current_section_name)
  {
    if (Object.op_Equality((Object) widget, (Object) null))
      return;
    UIVisibleWidgetShriken visibleWidgetShriken = ((Component) widget).GetComponent<UIVisibleWidgetShriken>();
    if (Object.op_Equality((Object) visibleWidgetShriken, (Object) null))
      visibleWidgetShriken = ((Component) widget).gameObject.AddComponent<UIVisibleWidgetShriken>();
    visibleWidgetShriken.panel = panel;
    visibleWidgetShriken.widget = widget;
    if (string.IsNullOrEmpty(visibleWidgetShriken.sectionName))
      visibleWidgetShriken.sectionName = current_section_name ?? MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    if (((Component) visibleWidgetShriken).transform.childCount <= 0)
      return;
    visibleWidgetShriken.children = new Transform[((Component) visibleWidgetShriken).transform.childCount];
    for (int index = 0; index < ((Component) visibleWidgetShriken).transform.childCount; ++index)
      visibleWidgetShriken.children[index] = ((Component) visibleWidgetShriken).transform.GetChild(index);
  }

  public static void Remove(UIWidget widget)
  {
    if (Object.op_Equality((Object) widget, (Object) null))
      return;
    UIVisibleWidgetShriken component = ((Component) widget).GetComponent<UIVisibleWidgetShriken>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    Object.Destroy((Object) component);
  }

  private void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.panel, (Object) null) || this.children == null)
      return;
    bool flag = this.IsVisibleCompletely(this.panel, this.widget);
    for (int index = 0; index < this.children.Length; ++index)
      ((Component) this.children[index]).gameObject.SetActive(flag);
  }

  public bool IsVisibleCompletely(UIPanel p, UIWidget w)
  {
    if (Object.op_Equality((Object) p, (Object) null) || Object.op_Equality((Object) w, (Object) null))
      return true;
    for (int index = 0; index < 4; ++index)
    {
      if (!p.IsVisible(this.widget.worldCorners[index]))
        return false;
    }
    return true;
  }

  private void OnDisable()
  {
  }
}
