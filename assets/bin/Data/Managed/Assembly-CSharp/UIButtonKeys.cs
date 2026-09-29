// Decompiled with JetBrains decompiler
// Type: UIButtonKeys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
  public UIButtonKeys selectOnClick;
  public UIButtonKeys selectOnUp;
  public UIButtonKeys selectOnDown;
  public UIButtonKeys selectOnLeft;
  public UIButtonKeys selectOnRight;

  protected override void OnEnable()
  {
    this.Upgrade();
    base.OnEnable();
  }

  public void Upgrade()
  {
    if (Object.op_Equality((Object) this.onClick, (Object) null) && Object.op_Inequality((Object) this.selectOnClick, (Object) null))
    {
      this.onClick = ((Component) this.selectOnClick).gameObject;
      this.selectOnClick = (UIButtonKeys) null;
      NGUITools.SetDirty((Object) this);
    }
    if (Object.op_Equality((Object) this.onLeft, (Object) null) && Object.op_Inequality((Object) this.selectOnLeft, (Object) null))
    {
      this.onLeft = ((Component) this.selectOnLeft).gameObject;
      this.selectOnLeft = (UIButtonKeys) null;
      NGUITools.SetDirty((Object) this);
    }
    if (Object.op_Equality((Object) this.onRight, (Object) null) && Object.op_Inequality((Object) this.selectOnRight, (Object) null))
    {
      this.onRight = ((Component) this.selectOnRight).gameObject;
      this.selectOnRight = (UIButtonKeys) null;
      NGUITools.SetDirty((Object) this);
    }
    if (Object.op_Equality((Object) this.onUp, (Object) null) && Object.op_Inequality((Object) this.selectOnUp, (Object) null))
    {
      this.onUp = ((Component) this.selectOnUp).gameObject;
      this.selectOnUp = (UIButtonKeys) null;
      NGUITools.SetDirty((Object) this);
    }
    if (!Object.op_Equality((Object) this.onDown, (Object) null) || !Object.op_Inequality((Object) this.selectOnDown, (Object) null))
      return;
    this.onDown = ((Component) this.selectOnDown).gameObject;
    this.selectOnDown = (UIButtonKeys) null;
    NGUITools.SetDirty((Object) this);
  }
}
