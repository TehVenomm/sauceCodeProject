// Decompiled with JetBrains decompiler
// Type: PortalMiniMapIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PortalMiniMapIcon : MiniMapIcon
{
  [SerializeField]
  protected string[] iconSpriteNames;
  [SerializeField]
  protected string[] overIconSpriteNames;
  private PortalObject portal;
  private PortalObject.VIEW_TYPE viewType;
  private bool isFull = true;

  public override void Initialize(MonoBehaviour root_object)
  {
    base.Initialize(root_object);
    this.portal = root_object as PortalObject;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.portal, (Object) null))
      return;
    if (this.viewType != this.portal.viewType || this.isFull != this.portal.isFull)
    {
      int index = (int) this.portal.viewType;
      if (this.portal.viewType == PortalObject.VIEW_TYPE.NOT_TRAVELED && !this.portal.isFull)
        index = 5;
      this.icon.spriteName = this.iconSpriteNames[index];
      if (Object.op_Inequality((Object) this.overIcon, (Object) null))
        this.overIcon.spriteName = this.overIconSpriteNames[index];
    }
    this.viewType = this.portal.viewType;
    this.isFull = this.portal.isFull;
  }
}
