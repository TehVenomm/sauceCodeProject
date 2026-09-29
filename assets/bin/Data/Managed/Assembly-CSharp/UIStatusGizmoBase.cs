// Decompiled with JetBrains decompiler
// Type: UIStatusGizmoBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIStatusGizmoBase : MonoBehaviour
{
  protected static List<UIStatusGizmoBase> uiList = new List<UIStatusGizmoBase>();
  protected static int listUpdateCount = 0;
  private UIPanel basePanel;
  protected float screenZ;
  public int depthOffset;
  protected Transform transform;
  [HideInInspector]
  public bool isHostPlayer;

  protected virtual void SortAll()
  {
    UIStatusGizmoBase.uiList.Sort((Comparison<UIStatusGizmoBase>) ((a, b) =>
    {
      if ((double) a.screenZ == (double) b.screenZ)
        return 0;
      return (double) a.screenZ >= (double) b.screenZ ? -1 : 1;
    }));
    int index = 0;
    for (int count = UIStatusGizmoBase.uiList.Count; index < count; ++index)
    {
      UIStatusGizmoBase ui = UIStatusGizmoBase.uiList[index];
      int num = index * 10;
      if (ui.depthOffset != num)
      {
        UIStatusGizmoBase.AdjustDepth(((Component) ui).gameObject, num - ui.depthOffset);
        ui.depthOffset = num;
      }
    }
  }

  protected static void AdjustDepth(GameObject set_object, int depth)
  {
    if (depth == 0 || Object.op_Equality((Object) set_object, (Object) null))
      return;
    set_object.GetComponentsInChildren<UIWidget>(true, Temporary.uiWidgetList);
    int index = 0;
    for (int count = Temporary.uiWidgetList.Count; index < count; ++index)
      Temporary.uiWidgetList[index].depth += depth;
    Temporary.uiWidgetList.Clear();
  }

  public UIPanel BasePanel
  {
    get => this.basePanel ?? (this.basePanel = ((Component) this).GetComponent<UIPanel>());
  }

  public float ScreenZ => this.screenZ;

  protected virtual void OnEnable()
  {
    UIStatusGizmoBase.uiList.Add(this);
    this.transform = ((Component) this).gameObject.transform;
  }

  protected virtual void OnDisable() => UIStatusGizmoBase.uiList.Remove(this);

  private void Update() => UIStatusGizmoBase.listUpdateCount = 0;

  private void LateUpdate()
  {
    this.UpdateParam();
    ++UIStatusGizmoBase.listUpdateCount;
    if (UIStatusGizmoBase.listUpdateCount < UIStatusGizmoBase.uiList.Count)
      return;
    this.SortAll();
  }

  protected virtual void UpdateParam()
  {
  }

  protected void SetActiveSafe(GameObject target, bool active)
  {
    if (Object.op_Equality((Object) target, (Object) null) || target.activeSelf == active)
      return;
    target.SetActive(active);
  }
}
