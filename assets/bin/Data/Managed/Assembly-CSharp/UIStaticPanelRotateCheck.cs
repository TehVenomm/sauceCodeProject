// Decompiled with JetBrains decompiler
// Type: UIStaticPanelRotateCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIStaticPanelRotateCheck : MonoBehaviour
{
  [SerializeField]
  protected UIPanel panel;
  private int updateCount;
  private bool updateAnchors = true;
  protected UIAnchor[] anchors;

  private void Awake()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.anchors = ((Component) this).GetComponentsInChildren<UIAnchor>();
    this.updateCount = 0;
    this.updateAnchors = true;
    this.panel.widgetsAreStatic = false;
  }

  private void OnEnable()
  {
    this.updateCount = 0;
    this.updateAnchors = true;
    this.panel.widgetsAreStatic = false;
  }

  private void OnDestroy()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  protected virtual void Update()
  {
    if (this.panel.widgetsAreStatic)
      return;
    ++this.updateCount;
    if (this.updateCount < 3)
      return;
    if (this.updateAnchors)
    {
      int index1 = 0;
      for (int length = this.anchors.Length; index1 < length; ++index1)
        ((Behaviour) this.anchors[index1]).enabled = true;
      ((Component) this).GetComponentsInChildren<UIRect>(true, Temporary.uiRectList);
      int index2 = 0;
      for (int count = Temporary.uiRectList.Count; index2 < count; ++index2)
        Temporary.uiRectList[index2].UpdateAnchors();
      Temporary.uiRectList.Clear();
      this.updateAnchors = false;
    }
    else
      this.panel.widgetsAreStatic = true;
  }

  private void OnScreenRotate(bool is_portrait)
  {
    this.updateCount = 0;
    this.panel.widgetsAreStatic = false;
    this.panel.ForceUpDate();
  }
}
