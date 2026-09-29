// Decompiled with JetBrains decompiler
// Type: UIDragCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
  public UIDraggableCamera draggableCamera;

  private void Awake()
  {
    if (!Object.op_Equality((Object) this.draggableCamera, (Object) null))
      return;
    this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(((Component) this).gameObject);
  }

  private void OnPress(bool isPressed)
  {
    if (!((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || !Object.op_Inequality((Object) this.draggableCamera, (Object) null))
      return;
    this.draggableCamera.Press(isPressed);
  }

  private void OnDrag(Vector2 delta)
  {
    if (!((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || !Object.op_Inequality((Object) this.draggableCamera, (Object) null))
      return;
    this.draggableCamera.Drag(delta);
  }

  private void OnScroll(float delta)
  {
    if (!((Behaviour) this).enabled || !NGUITools.GetActive(((Component) this).gameObject) || !Object.op_Inequality((Object) this.draggableCamera, (Object) null))
      return;
    this.draggableCamera.Scroll(delta);
  }
}
