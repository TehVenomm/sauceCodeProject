// Decompiled with JetBrains decompiler
// Type: EffectViewShift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EffectViewShift : MonoBehaviour
{
  public float shiftValue = -0.25f;
  public Transform targetCamera;
  private Transform _transform;
  private Vector3 defaultLocalPos;

  private void Start()
  {
    this._transform = ((Component) this).transform;
    this.defaultLocalPos = this._transform.localPosition;
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.targetCamera, (Object) null))
    {
      this.UpdateTargetCamera();
      if (Object.op_Equality((Object) this.targetCamera, (Object) null))
        return;
    }
    this._transform.localPosition = this.defaultLocalPos;
    Vector3 position = this._transform.position;
    Transform transform = this._transform;
    Vector3 vector3_1 = Vector3.op_Subtraction(position, this.targetCamera.position);
    Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, this.shiftValue), position);
    transform.position = vector3_2;
  }

  private void UpdateTargetCamera()
  {
    if (MonoBehaviourSingleton<AppMain>.IsValid())
    {
      if (!AppMain.isInitialized)
        return;
      this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    }
    else
    {
      if (!Object.op_Equality((Object) this.targetCamera, (Object) null) || !Object.op_Inequality((Object) Camera.main, (Object) null))
        return;
      this.targetCamera = ((Component) Camera.main).transform;
    }
  }
}
