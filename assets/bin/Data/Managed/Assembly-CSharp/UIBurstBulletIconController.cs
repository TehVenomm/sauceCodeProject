// Decompiled with JetBrains decompiler
// Type: UIBurstBulletIconController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIBurstBulletIconController : MonoBehaviour
{
  private readonly Vector3 BULLET_ICON_POS_INTERVAL = new Vector3(22f, 0.0f, 0.0f);
  [SerializeField]
  private UISprite m_baseIcon;
  [SerializeField]
  private UISprite m_bulletIcon;

  public bool Initialize(UIBurstBulletIconController.InitParam _param)
  {
    if (_param == null || Object.op_Equality((Object) this.m_baseIcon, (Object) null) || Object.op_Equality((Object) this.m_bulletIcon, (Object) null))
      return false;
    this.m_baseIcon.depth = _param.DepthOffset + _param.IconIndex * 2;
    this.m_bulletIcon.depth = _param.DepthOffset + _param.IconIndex * 2 + 1;
    ((Component) this).transform.localPosition = Vector3.op_Multiply(this.BULLET_ICON_POS_INTERVAL, (float) _param.IconIndex);
    ((Component) this).transform.localRotation = Quaternion.Euler(Vector3.zero);
    ((Component) this).transform.localScale = Vector3.one;
    return true;
  }

  public bool SetVisibleAllIcon()
  {
    return this.SwitchVisibleIcon(this.m_baseIcon, true) && this.SwitchVisibleIcon(this.m_bulletIcon, true);
  }

  public bool SetInvisibleAllIcon()
  {
    return this.SwitchVisibleIcon(this.m_baseIcon, false) && this.SwitchVisibleIcon(this.m_bulletIcon, false);
  }

  public bool SetVisibleBulletIcon() => this.SwitchVisibleIcon(this.m_bulletIcon, true);

  public bool SetInvisibleBulletIcon() => this.SwitchVisibleIcon(this.m_bulletIcon, false);

  private bool SwitchVisibleIcon(UISprite _sprite, bool _isVisible)
  {
    if (Object.op_Equality((Object) _sprite, (Object) null) || ((Behaviour) _sprite).enabled == _isVisible)
      return false;
    ((Behaviour) _sprite).enabled = _isVisible;
    return true;
  }

  public class InitParam
  {
    public int IconIndex;
    public int DepthOffset;
  }
}
