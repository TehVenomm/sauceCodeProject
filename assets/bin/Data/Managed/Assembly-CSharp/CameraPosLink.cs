// Decompiled with JetBrains decompiler
// Type: CameraPosLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CameraPosLink : MonoBehaviour
{
  public Camera targetCamera;
  public bool y0;
  public float cameraOffsetZ;
  private Transform _transform;

  private void Start()
  {
    if (Object.op_Equality((Object) this.targetCamera, (Object) null))
      this.targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    this._transform = ((Component) this).transform;
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.targetCamera, (Object) null))
      return;
    Vector3 vector3_1 = ((Component) this.targetCamera).transform.position;
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<InGameCameraManager>.I.target, (Object) null))
    {
      Vector3 vector3_2 = Vector3.op_Subtraction(((Component) MonoBehaviourSingleton<InGameCameraManager>.I.target).transform.position, vector3_1);
      ((Vector3) ref vector3_2).Normalize();
      Vector3 vector3_3 = Vector3.op_Multiply(vector3_2, this.cameraOffsetZ);
      vector3_1 = Vector3.op_Addition(vector3_1, vector3_3);
    }
    if (this.y0)
      vector3_1.y = 0.0f;
    this._transform.position = vector3_1;
  }
}
