// Decompiled with JetBrains decompiler
// Type: UIEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
  public object parameter;
  public UIEventListener.VoidDelegate onSubmit;
  public UIEventListener.VoidDelegate onClick;
  public UIEventListener.VoidDelegate onDoubleClick;
  public UIEventListener.BoolDelegate onHover;
  public UIEventListener.BoolDelegate onPress;
  public UIEventListener.BoolDelegate onSelect;
  public UIEventListener.FloatDelegate onScroll;
  public UIEventListener.VoidDelegate onDragStart;
  public UIEventListener.VectorDelegate onDrag;
  public UIEventListener.VoidDelegate onDragOver;
  public UIEventListener.VoidDelegate onDragOut;
  public UIEventListener.VoidDelegate onDragEnd;
  public UIEventListener.ObjectDelegate onDrop;
  public UIEventListener.KeyCodeDelegate onKey;
  public UIEventListener.BoolDelegate onTooltip;

  private bool isColliderEnabled
  {
    get
    {
      Collider component1 = ((Component) this).GetComponent<Collider>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        return component1.enabled;
      Collider2D component2 = ((Component) this).GetComponent<Collider2D>();
      return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
    }
  }

  private void OnSubmit()
  {
    if (!this.isColliderEnabled || this.onSubmit == null)
      return;
    this.onSubmit(((Component) this).gameObject);
  }

  private void OnClick()
  {
    if (!this.isColliderEnabled || this.onClick == null)
      return;
    this.onClick(((Component) this).gameObject);
  }

  private void OnDoubleClick()
  {
    if (!this.isColliderEnabled || this.onDoubleClick == null)
      return;
    this.onDoubleClick(((Component) this).gameObject);
  }

  private void OnHover(bool isOver)
  {
    if (!this.isColliderEnabled || this.onHover == null)
      return;
    this.onHover(((Component) this).gameObject, isOver);
  }

  private void OnPress(bool isPressed)
  {
    if (!this.isColliderEnabled || this.onPress == null)
      return;
    this.onPress(((Component) this).gameObject, isPressed);
  }

  private void OnSelect(bool selected)
  {
    if (!this.isColliderEnabled || this.onSelect == null)
      return;
    this.onSelect(((Component) this).gameObject, selected);
  }

  private void OnScroll(float delta)
  {
    if (!this.isColliderEnabled || this.onScroll == null)
      return;
    this.onScroll(((Component) this).gameObject, delta);
  }

  private void OnDragStart()
  {
    if (this.onDragStart == null)
      return;
    this.onDragStart(((Component) this).gameObject);
  }

  private void OnDrag(Vector2 delta)
  {
    if (this.onDrag == null)
      return;
    this.onDrag(((Component) this).gameObject, delta);
  }

  private void OnDragOver()
  {
    if (!this.isColliderEnabled || this.onDragOver == null)
      return;
    this.onDragOver(((Component) this).gameObject);
  }

  private void OnDragOut()
  {
    if (!this.isColliderEnabled || this.onDragOut == null)
      return;
    this.onDragOut(((Component) this).gameObject);
  }

  private void OnDragEnd()
  {
    if (this.onDragEnd == null)
      return;
    this.onDragEnd(((Component) this).gameObject);
  }

  private void OnDrop(GameObject go)
  {
    if (!this.isColliderEnabled || this.onDrop == null)
      return;
    this.onDrop(((Component) this).gameObject, go);
  }

  private void OnKey(KeyCode key)
  {
    if (!this.isColliderEnabled || this.onKey == null)
      return;
    this.onKey(((Component) this).gameObject, key);
  }

  private void OnTooltip(bool show)
  {
    if (!this.isColliderEnabled || this.onTooltip == null)
      return;
    this.onTooltip(((Component) this).gameObject, show);
  }

  public static UIEventListener Get(GameObject go)
  {
    UIEventListener uiEventListener = go.GetComponent<UIEventListener>();
    if (Object.op_Equality((Object) uiEventListener, (Object) null))
      uiEventListener = go.AddComponent<UIEventListener>();
    return uiEventListener;
  }

  public delegate void VoidDelegate(GameObject go);

  public delegate void BoolDelegate(GameObject go, bool state);

  public delegate void FloatDelegate(GameObject go, float delta);

  public delegate void VectorDelegate(GameObject go, Vector2 delta);

  public delegate void ObjectDelegate(GameObject go, GameObject obj);

  public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
