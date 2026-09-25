// Decompiled with JetBrains decompiler
// Type: MiniMapIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MiniMapIcon : MonoBehaviour
{
  [SerializeField]
  protected UISprite icon;
  [SerializeField]
  protected UISprite overIcon;
  private bool _isOver;
  private bool isInitialized;

  public bool isOver
  {
    set
    {
      if (this._isOver != value)
      {
        ((Component) this.icon).gameObject.SetActive(!value);
        if (Object.op_Inequality((Object) this.overIcon, (Object) null))
          ((Component) this.overIcon).gameObject.SetActive(value);
      }
      this._isOver = value;
    }
    get => this._isOver;
  }

  public Transform target { get; set; }

  public Transform _trasform { get; set; }

  private void Awake()
  {
    ((Component) this.icon).gameObject.SetActive(true);
    if (Object.op_Inequality((Object) this.overIcon, (Object) null))
      ((Component) this.overIcon).gameObject.SetActive(false);
    this._trasform = ((Component) this).transform;
  }

  public virtual void Initialize(MonoBehaviour root_object)
  {
    ((Component) this).gameObject.SetActive(false);
    this.isInitialized = true;
  }

  public void SetIconSprite(string spriteName)
  {
    if (Object.op_Inequality((Object) this.icon, (Object) null))
      this.icon.spriteName = spriteName;
    if (!Object.op_Inequality((Object) this.overIcon, (Object) null))
      return;
    this.overIcon.spriteName = spriteName;
  }

  public void UpdateIcon(float center_x, float center_y, float scaling, float ui_radius)
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    if (this.isInitialized)
    {
      ((Component) this).gameObject.SetActive(true);
      this.isInitialized = false;
    }
    bool flag = true;
    Vector3 vector3 = this.target.position;
    vector3.x = (vector3.x - center_x) * scaling;
    vector3.y = (vector3.z - center_y) * scaling;
    vector3.z = 0.0f;
    if ((double) ((Vector3) ref vector3).magnitude > (double) ui_radius)
    {
      flag = false;
      vector3 = Vector3.op_Multiply(((Vector3) ref vector3).normalized, ui_radius);
    }
    this.isOver = !flag;
    this._trasform.localPosition = vector3;
    this._trasform.localRotation = Quaternion.Inverse(this._trasform.parent.localRotation);
  }
}
