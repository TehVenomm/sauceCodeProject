// Decompiled with JetBrains decompiler
// Type: ChatSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatSlider : MonoBehaviour
{
  private BoxCollider m_Collider;
  private Transform m_Trans;

  public BoxCollider Collider
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Collider, (Object) null))
        this.m_Collider = ((Component) this).GetComponent<BoxCollider>();
      return this.m_Collider;
    }
  }

  private void OnDrag(Vector2 delta)
  {
  }

  private void OnDragStart()
  {
  }

  private void OnClick()
  {
  }

  public Transform Trans
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Trans, (Object) null))
        this.m_Trans = ((Component) this).transform;
      return this.m_Trans;
    }
  }
}
