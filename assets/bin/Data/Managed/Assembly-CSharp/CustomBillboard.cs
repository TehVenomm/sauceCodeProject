// Decompiled with JetBrains decompiler
// Type: CustomBillboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CustomBillboard : MonoBehaviour
{
  [SerializeField]
  private CustomBillboard.FIXED_AXIS fixedAxis;
  private Transform m_cachedTrans;
  private Transform m_cachedCamTrans;

  private void Awake() => this.m_cachedTrans = ((Component) this).transform;

  private void LateUpdate()
  {
    if (MonoBehaviourSingleton<AppMain>.IsValid() && Object.op_Equality((Object) this.m_cachedCamTrans, (Object) null))
    {
      Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
      if (Object.op_Inequality((Object) mainCamera, (Object) null))
        this.m_cachedCamTrans = ((Component) mainCamera).transform;
    }
    if (Object.op_Equality((Object) this.m_cachedCamTrans, (Object) null))
      return;
    Vector3 position = this.m_cachedCamTrans.position;
    switch (this.fixedAxis)
    {
      case CustomBillboard.FIXED_AXIS.X:
        position.x = this.m_cachedTrans.position.x;
        break;
      case CustomBillboard.FIXED_AXIS.Y:
        position.y = this.m_cachedTrans.position.y;
        break;
      case CustomBillboard.FIXED_AXIS.Z:
        position.z = this.m_cachedTrans.position.z;
        break;
    }
    Vector3 up = Vector3.up;
    if (this.fixedAxis == CustomBillboard.FIXED_AXIS.NONE)
      up = this.m_cachedCamTrans.up;
    this.m_cachedTrans.LookAt(position, up);
  }

  public enum FIXED_AXIS
  {
    NONE,
    X,
    Y,
    Z,
  }
}
