// Decompiled with JetBrains decompiler
// Type: UIDragScrollView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
  public UIScrollView scrollView;
  [HideInInspector]
  [SerializeField]
  private UIScrollView draggablePanel;
  private Transform mTrans;
  private UIScrollView mScroll;
  private bool mAutoFind;
  private bool mStarted;

  private void OnEnable()
  {
    this.mTrans = ((Component) this).transform;
    if (Object.op_Equality((Object) this.scrollView, (Object) null) && Object.op_Inequality((Object) this.draggablePanel, (Object) null))
    {
      this.scrollView = this.draggablePanel;
      this.draggablePanel = (UIScrollView) null;
    }
    if (!this.mStarted || !this.mAutoFind && !Object.op_Equality((Object) this.mScroll, (Object) null))
      return;
    this.FindScrollView();
  }

  private void Start()
  {
    this.mStarted = true;
    this.FindScrollView();
    this.AttachUIButtonEffect();
  }

  private void FindScrollView()
  {
    UIScrollView inParents = NGUITools.FindInParents<UIScrollView>(this.mTrans);
    if (Object.op_Equality((Object) this.scrollView, (Object) null) || this.mAutoFind && Object.op_Inequality((Object) inParents, (Object) this.scrollView))
    {
      this.scrollView = inParents;
      this.mAutoFind = true;
    }
    else if (Object.op_Equality((Object) this.scrollView, (Object) inParents))
      this.mAutoFind = true;
    this.mScroll = this.scrollView;
  }

  private void AttachUIButtonEffect()
  {
    UIButton component = ((Component) this).GetComponent<UIButton>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Equality((Object) ((Component) this).GetComponent<UINoAuto>(), (Object) null) || !Object.op_Equality((Object) ((Component) component).GetComponent<UIButtonEffect>(), (Object) null))
      return;
    ((Component) component).gameObject.AddComponent<UIButtonEffect>().isSimple = true;
  }

  private void OnPress(bool pressed)
  {
    if (this.mAutoFind && Object.op_Inequality((Object) this.mScroll, (Object) this.scrollView))
    {
      this.mScroll = this.scrollView;
      this.mAutoFind = false;
    }
    if (!Object.op_Implicit((Object) this.scrollView) || !((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject))
      return;
    this.scrollView.Press(pressed);
    if (pressed || !this.mAutoFind)
      return;
    this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
    this.mScroll = this.scrollView;
  }

  private void OnDrag(Vector2 delta)
  {
    if (!Object.op_Implicit((Object) this.scrollView) || !NGUITools.GetActive((Behaviour) this))
      return;
    this.scrollView.Drag();
  }

  private void OnScroll(float delta)
  {
    if (!Object.op_Implicit((Object) this.scrollView) || !NGUITools.GetActive((Behaviour) this))
      return;
    this.scrollView.Scroll(delta);
  }

  public void OnPan(Vector2 delta)
  {
    if (!Object.op_Implicit((Object) this.scrollView) || !NGUITools.GetActive((Behaviour) this))
      return;
    this.scrollView.OnPan(delta);
  }
}
